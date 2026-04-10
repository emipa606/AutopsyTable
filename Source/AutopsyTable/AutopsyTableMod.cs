using Mlie;
using RimWorld;
using UnityEngine;
using Verse;

namespace AutopsyTable;

[StaticConstructorOnStartup]
internal class AutopsyTableMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static AutopsyTableMod instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public AutopsyTableMod(ModContentPack content) : base(content)
    {
        instance = this;
        Settings = GetSettings<AutopsyTableSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal AutopsyTableSettings Settings { get; }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Autopsy Table";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        Settings.MaximumDeathTime = (int)listingStandard.SliderLabeled(
            "AUT.MaximumDeathTime".Translate(Settings.MaximumDeathTime.ToStringTicksToPeriod()),
            Settings.MaximumDeathTime, 0, GenDate.TicksPerQuadrum, tooltip: "AUT.MaximumDeathTimeTT".Translate());

        listingStandard.CheckboxLabeled("AUT.ConsumeInputOnNormal".Translate(), ref Settings.ConsumeCorpse,
            "AUT.ConsumeInputOnNormalTT".Translate());

        if (!Settings.ConsumeCorpse)
        {
            listingStandard.CheckboxLabeled("AUT.AllowRepeatUntilDepleted".Translate(),
                ref Settings.AllowRepeatUntilDepleted,
                "AUT.AllowRepeatUntilDepletedTT".Translate());
        }
        else
        {
            Settings.AllowRepeatUntilDepleted = false;
        }

        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("AUT.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}