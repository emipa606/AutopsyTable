using RimWorld;
using Verse;

namespace AutopsyTable;

public class Building_AutopsyTable : Building_WorkTable
{
    private readonly int MAX_HOURS = 24;

    public int getMaxHours()
    {
        return MAX_HOURS;
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

    private void changeHours()
    {
        Log.Message("click");
    }

    //public override string GetInspectString()
    //{
    //    var sb = new StringBuilder();
    //    sb.AppendLine(base.GetInspectString());
    //    this.TryGetQuality(out var qc);
    //    var unused = qc.GetLabel();
    //    string unused1 = Stuff.LabelCap;
    //    var unused2 = this.GetRoom().GetStat(RoomStatDefOf.InfectionChanceFactor);
    //    // sb.AppendLine ("Room quality factor: " + Calculation.roomFactor(infectionFactor, infectionFactor));
    //    // sb.AppendLine ("Table material & quality: " + Calculation.tableQualityFactor (tq) * Calculation.tableStuffFactor (ts));

    //    return sb.ToString().TrimEndNewlines();
    //}
}