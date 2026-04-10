using System.Collections.Generic;
using HarmonyLib;
using Verse;
using Verse.AI;

namespace AutopsyTable;

[HarmonyPatch(typeof(Toils_Recipe), "ConsumeIngredients")]
public static class Toils_Recipe_ConsumeIngredients
{
    public static void Prefix(ref List<Thing> ingredients, RecipeDef recipe)
    {
        if (recipe.defName != "AutopsyHumanoid" || AutopsyTableMod.instance?.Settings?.ConsumeCorpse == true)
        {
            return;
        }

        ingredients.RemoveAll(thing => thing is Corpse);
    }
}