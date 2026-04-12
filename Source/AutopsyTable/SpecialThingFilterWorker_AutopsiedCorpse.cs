using Verse;

namespace AutopsyTable;

public class SpecialThingFilterWorker_AutopsiedCorpse : SpecialThingFilterWorker
{
    public override bool Matches(Thing t)
    {
        if (AutopsyTableMod.instance?.Settings?.ConsumeCorpse == true)
        {
            return false;
        }

        return t is Corpse corpse && Corpse_ButcherProducts.HasAnyHarvestableAutopsyProducts(corpse);
    }

    public override bool CanEverMatch(ThingDef def)
    {
        return typeof(Corpse).IsAssignableFrom(def.thingClass);
    }
}