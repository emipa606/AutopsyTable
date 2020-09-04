using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using HarmonyLib;
using Verse.AI;

namespace AutopsyTable
{
    [HarmonyPatch (typeof(Pawn), "ButcherProducts")]
	public static class Harvest
	{
		public static void Postfix (ref IEnumerable<Thing> __result, ref Pawn __instance, Pawn butcher, float efficiency)
		{
            try
            {
                Building_WorkTable table = butcher.CurJob.GetTarget(TargetIndex.A).Thing as Building_WorkTable;
                if (!(table.def.defName == "TableAutopsy")) return;
                __result = __result.CompackedItems(__instance, table, butcher);
            } catch (System.Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
            }
		}

		private static IEnumerable<Thing> CompackedItems (this IEnumerable<Thing> list, Pawn pawn, Building_WorkTable table, Pawn butcher)
		{
			foreach (Thing thing in list)
				yield return thing;
			foreach (Thing thing in pawn.DetachValuableItems(table, butcher))
				yield return thing;
		}

		private static float HarvestPartChance (bool bionic, Building_WorkTable table, Pawn butcher, Pawn corpse)
		{
			if (bionic) {
				return 1f;
			}
			float baseFactor = 0.5f;
			// TABLE
			float tableQualityFactor = 1f, tableStuffFactor = 1f;
			QualityCategory qc;
			table.TryGetQuality (out qc);
			string tableQuality = qc.GetLabel ();
			string tableStuff = table.Stuff.LabelCap;
			switch (tableQuality) {
			case "awful":
				tableQualityFactor = 0.7f;
				break;
			case "shoddy":
				tableQualityFactor = 0.8f;
				break;
			case "poor":
				tableQualityFactor = 0.9f;
				break;
			case "normal":
				tableQualityFactor = 1f;
				break;
			case "good":
				tableQualityFactor = 1.1f;
				break;
			case "superior":
				tableQualityFactor = 1.2f;
				break;
			case "excellent":
				tableQualityFactor = 1.3f;
				break;
			case "masterwork":
				tableQualityFactor = 1.4f;
				break;
			case "legendary":
				tableQualityFactor = 1.5f;
				break;
			}
			switch (tableStuff) {
			case "Silver":
				tableStuffFactor = 1.2f;
				break;
			case "Steel":
				tableStuffFactor = 1.1f;
				break;
			case "Wood":
				tableStuffFactor = 0.9f;
				break;
			}

			// ROOM
			float infectionChance = table.GetRoom (RegionType.Set_Passable).GetStat (RoomStatDefOf.InfectionChanceFactor);
			float roomInfectionFactor = (1 - infectionChance) + 0.5f;
			// BUTCHER
			SkillRecord skill = butcher.skills.GetSkill (SkillDefOf.Medicine);
			int skillLevel = 0;
			if (skill != null) {
				skillLevel = skill.Level;
			}
			float doctorSkillFactor = (skillLevel * skillLevel + 1f) / 300f;

			// CORPSE
			int years = 0, quadrums = 0, days = 0;
			float hours = 0f;
			GenDate.TicksToPeriod (corpse.Corpse.Age, out years, out quadrums, out days, out hours);
			float corpseAge = hours + 24 * days + 15 * 24 * quadrums + 4 * 15 * 24 * years;
			float corpseAgeFactor = 1.01f;
			if (corpseAge > 12 && corpseAge < 24) {
				corpseAgeFactor = (24f - corpseAge) / 20f + 0.01f;
			} else if (corpseAge >= 24) {
				corpseAgeFactor = 0.01f;
			}
			float chance = baseFactor * tableQualityFactor * tableStuffFactor * roomInfectionFactor * doctorSkillFactor * corpseAgeFactor;
			if (Prefs.DevMode) {
				Log.Message ("Base chance: " + baseFactor);
				Log.Message ("Table quality: " + tableQuality + " = " + tableQualityFactor);
				Log.Message ("Table stuff: " + tableStuff + " = " + tableStuffFactor);
				Log.Message ("Infection chance: " + infectionChance + " = " + roomInfectionFactor);
				Log.Message ("Doctor skill: " + skillLevel + " = " + doctorSkillFactor);
				Log.Message ("Corpse age: " + corpseAge + "h" + " = " + corpseAgeFactor);
				Log.Message ("Total: " + chance);
			}
			return chance;
		}

		private static bool HarvestPart (float livingChance, float bionicChance, bool bionic)
		{
			float rnd = UnityEngine.Random.value;
			if (bionic) {
				if (Prefs.DevMode) {
					Log.Message ("TRY: " + rnd + " < " + bionicChance);
				}
				return rnd < bionicChance;
			} else {
				if (Prefs.DevMode) {
					Log.Message ("TRY: " + rnd + " < " + livingChance);
				}
				return rnd < livingChance;
			}
		}

		private static IEnumerable<Thing> DetachValuableItems (this Pawn corpse, Building_WorkTable table, Pawn butcher)
		{
			float bionicChance = HarvestPartChance (true, table, butcher, corpse);
			float livingChance = HarvestPartChance (false, table, butcher, corpse);
			IEnumerable<BodyPartRecord> parts = corpse.health.hediffSet.GetNotMissingParts (BodyPartHeight.Undefined, BodyPartDepth.Undefined);
			foreach (BodyPartRecord record in parts) {
				IEnumerable<Hediff> hediffs = from x in corpse.health.hediffSet.hediffs
				                              where x.Part == record
				                              select x;
				if (hediffs.Any ()) {
					// bionic parts
					foreach (Hediff hediff in hediffs) {
						if (hediff.def.spawnThingOnRemoved != null && HarvestPart (livingChance, bionicChance, true)) {
							yield return ThingMaker.MakeThing (hediff.def.spawnThingOnRemoved, null);
						}
					}
				} else {
					if (record.def.spawnThingOnRemoved != null) {
						if (HarvestPart (livingChance, bionicChance, !record.def.alive)) {
							corpse.health.AddHediff (HediffMaker.MakeHediff (HediffDefOf.MissingBodyPart, corpse, record), null, null);
							yield return ThingMaker.MakeThing (record.def.spawnThingOnRemoved, null);
						}
					}
				}
			}
		}
	}
}

