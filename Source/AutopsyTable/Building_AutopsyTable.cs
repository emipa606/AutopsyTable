using System.Text;
using Verse;
using RimWorld;

namespace AutopsyTable
{
    public class Building_AutopsyTable : Building_WorkTable
    {
		private readonly int MAX_HOURS = 24;

		public Building_AutopsyTable ()
		{
		}

		public int getMaxHours ()
		{
			return this.MAX_HOURS;
		}

		/*public override IEnumerable<Gizmo> GetGizmos ()
		{
			yield return new Command_Action {
				action = new Action (this.changeHours),
				defaultLabel = "TestLabel",
				defaultDesc = "TestDesc",
				icon = ContentFinder<Texture2D>.Get ("Things/Building/Art/Sculpturesmall/SculptureSmallAbstractA", true)
			};
		}*/

		private void changeHours ()
		{
			Log.Message ("click");
		}

		public override string GetInspectString ()
		{
			StringBuilder sb = new StringBuilder ();
			sb.AppendLine (base.GetInspectString ());
            this.TryGetQuality(out QualityCategory qc);
            string tq = qc.GetLabel ();
			string ts = this.Stuff.LabelCap;
			float infectionFactor = this.GetRoom (RegionType.Set_Passable).GetStat (RoomStatDefOf.InfectionChanceFactor);
			// sb.AppendLine ("Room quality factor: " + Calculation.roomFactor(infectionFactor, infectionFactor));
			// sb.AppendLine ("Table material & quality: " + Calculation.tableQualityFactor (tq) * Calculation.tableStuffFactor (ts));

			return sb.ToString ().TrimEndNewlines ();
		}
	}
}

