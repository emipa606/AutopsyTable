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

    private void changeHours()
    {
        Log.Message("click");
    }
}