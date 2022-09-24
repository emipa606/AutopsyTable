using System.Reflection;
using HarmonyLib;
using Verse;

namespace AutopsyTable;

[StaticConstructorOnStartup]
public static class Main
{
    static Main()
    {
        new Harmony("mlie.autopsytable").PatchAll(Assembly.GetExecutingAssembly());
    }
}