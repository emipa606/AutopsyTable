using HarmonyLib;
using RimWorld;
using Verse;

namespace AutopsyTable;

[HarmonyPatch(typeof(WorkGiver_DoBill), "IsUsableIngredient")]
public static class WorkGiver_DoBill_IsUsableIngredient_Patch
{
    public static void Postfix(ref bool __result, Thing t, Bill bill)
    {
        if (!__result || bill?.recipe?.defName != "AutopsyHumanoid")
        {
            return;
        }

        if (t is Corpse corpse && !Corpse_ButcherProducts.HasAnyHarvestableAutopsyProducts(corpse))
        {
            __result = false;
        }
    }
}