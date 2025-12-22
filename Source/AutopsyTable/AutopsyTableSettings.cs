using RimWorld;
using Verse;

namespace AutopsyTable;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class AutopsyTableSettings : ModSettings
{
    public int MaximumDeathTime = GenDate.TicksPerDay;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref MaximumDeathTime, "MaximumDeathTime", GenDate.TicksPerDay);
    }
}