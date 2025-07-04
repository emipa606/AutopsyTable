﻿using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AutopsyTable;

[HarmonyPatch(typeof(Corpse), nameof(Corpse.ButcherProducts))]
public static class Corpse_ButcherProducts
{
    public static bool Prefix(ref IEnumerable<Thing> __result, ref Corpse __instance, Pawn butcher)
    {
        if (butcher.CurJob?.GetTarget(TargetIndex.A).Thing == null ||
            butcher.CurJob.GetTarget(TargetIndex.A).Thing.def.defName != "TableAutopsy")
        {
            return true;
        }

        if (butcher.CurJob?.RecipeDef?.defName != "AutopsyHumanoid")
        {
            return true;
        }

        var table = butcher.CurJob.GetTarget(TargetIndex.A).Thing as Building_WorkTable;
        __result = __instance.detachValuableItems(table, butcher).ToList();
        if (!__instance.IsDessicated() && __instance.InnerPawn.RaceProps.BloodDef != null)
        {
            FilthMaker.TryMakeFilth(butcher.Position, butcher.Map, __instance.InnerPawn.RaceProps.BloodDef,
                __instance.InnerPawn.LabelIndefinite());
        }

        return false;
    }

    public static void Postfix(ref IEnumerable<Thing> __result, ref Corpse __instance, Pawn butcher)
    {
        if (butcher.CurJob?.GetTarget(TargetIndex.A).Thing == null ||
            butcher.CurJob.GetTarget(TargetIndex.A).Thing.def.defName != "TableAutopsy")
        {
            return;
        }

        if (butcher.CurJob?.RecipeDef?.defName == "AutopsyHumanoid")
        {
            return;
        }

        if (__instance.GetRotStage() != RotStage.Fresh)
        {
            __result = __result.Where(thing => thing.def.IsMeat || thing.def.IsLeather);
        }

        if (__instance.IsDessicated())
        {
            __result = new List<Thing>();
        }

        var table = butcher.CurJob?.GetTarget(TargetIndex.A).Thing as Building_WorkTable;
        var valuableItems = __instance.detachValuableItems(table, butcher).ToList();
        valuableItems.AddRange(__result);
        if (!__instance.IsDessicated() && __instance.InnerPawn.RaceProps.BloodDef != null)
        {
            FilthMaker.TryMakeFilth(butcher.Position, butcher.Map, __instance.InnerPawn.RaceProps.BloodDef,
                __instance.InnerPawn.LabelIndefinite());
        }

        __result = valuableItems;
    }

    private static float harvestPartChance(bool bionic, Building_WorkTable table, Pawn butcher, Pawn corpse)
    {
        if (bionic)
        {
            return 1f;
        }

        var baseFactor = 0.5f;
        // TABLE
        float tableQualityFactor = 1f, tableStuffFactor = 1f;
        table.TryGetQuality(out var qc);
        var tableQuality = qc.GetLabel();
        string tableStuff = table.Stuff.LabelCap;
        switch (tableQuality)
        {
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

        switch (tableStuff)
        {
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
        var infectionChance = table.GetRoom().GetStat(RoomStatDefOf.InfectionChanceFactor);
        var roomInfectionFactor = 1 - infectionChance + 0.5f;
        // BUTCHER
        var skill = butcher.skills.GetSkill(SkillDefOf.Medicine);
        var skillLevel = 0;
        if (skill != null)
        {
            skillLevel = skill.Level;
        }

        var doctorSkillFactor = ((skillLevel * skillLevel) + 1f) / 300f;

        // CORPSE
        corpse.Corpse.Age.TicksToPeriod(out var years, out var quadrums, out var days, out var hours);
        var corpseAge = hours + (24 * days) + (15 * 24 * quadrums) + (4 * 15 * 24 * years);
        var corpseAgeFactor = 1.01f;
        switch (corpseAge)
        {
            case > 12 and < 24:
                corpseAgeFactor = ((24f - corpseAge) / 20f) + 0.01f;
                break;
            case >= 24:
                corpseAgeFactor = 0.01f;
                break;
        }

        var chance = baseFactor * tableQualityFactor * tableStuffFactor * roomInfectionFactor * doctorSkillFactor *
                     corpseAgeFactor;
        return chance;
    }

    private static bool harvestPart(float livingChance, float bionicChance, bool bionic)
    {
        var rnd = Random.value;
        if (bionic)
        {
            if (Prefs.DevMode)
            {
                Log.Message($"TRY: {rnd} < {bionicChance}");
            }

            return rnd < bionicChance;
        }

        if (Prefs.DevMode)
        {
            Log.Message($"TRY: {rnd} < {livingChance}");
        }

        return rnd < livingChance;
    }

    private static IEnumerable<Thing> detachValuableItems(this Corpse corpse, Building_WorkTable table, Pawn butcher)
    {
        var pawn = corpse.InnerPawn;
        var bionicChance = harvestPartChance(true, table, butcher, pawn);
        var livingChance = harvestPartChance(false, table, butcher, pawn);
        var parts = pawn.health.hediffSet.GetNotMissingParts();
        foreach (var record in parts)
        {
            var hediffs = from x in pawn.health.hediffSet.hediffs
                where x.Part == record
                select x;
            var hediffArray = hediffs as Hediff[] ?? hediffs.ToArray();
            if (hediffArray.Any())
            {
                foreach (var hediff in hediffArray)
                {
                    if (hediff.def.spawnThingOnRemoved == null)
                    {
                        continue;
                    }

                    if ((corpse.GetRotStage() != RotStage.Fresh || corpse.IsDessicated() || pawn.IsMutant ||
                         pawn.IsEntity) && !hediff.def.spawnThingOnRemoved.isTechHediff)
                    {
                        continue;
                    }

                    if (harvestPart(livingChance, bionicChance, true))
                    {
                        yield return ThingMaker.MakeThing(hediff.def.spawnThingOnRemoved);
                    }
                }
            }

            if (record.def.defName == "Skull" && ModLister.IdeologyInstalled)
            {
                pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, record));
                yield return ThingMaker.MakeThing(ThingDefOf.Skull);
                continue;
            }

            if (record.def.spawnThingOnRemoved == null)
            {
                continue;
            }

            if (corpse.GetRotStage() != RotStage.Fresh || corpse.IsDessicated() || pawn.IsMutant || pawn.IsEntity)
            {
                continue;
            }

            if (!harvestPart(livingChance, bionicChance, !record.def.alive))
            {
                continue;
            }

            pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, record));
            yield return ThingMaker.MakeThing(record.def.spawnThingOnRemoved);
        }
    }
}