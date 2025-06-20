using RimWorld;
using Verse;

namespace AutopsyTable;

public class Building_AutopsyTable : Building_WorkTable
{
    private const int MaxHours = 24;

    public int getMaxHours()
    {
        return MaxHours;
    }

    private void changeHours()
    {
        Log.Message("click");
    }
}