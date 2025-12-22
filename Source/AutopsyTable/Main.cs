using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace AutopsyTable;

[StaticConstructorOnStartup]
public static class Main
{
    static Main()
    {
        new Harmony("mlie.autopsytable").PatchAll(Assembly.GetExecutingAssembly());
    }

    public static float GetMaxHours()
    {
        return AutopsyTableMod.instance.Settings.MaximumDeathTime / (float)GenDate.TicksPerHour;
    }
}