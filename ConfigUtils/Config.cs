using System.Runtime.Serialization;
using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalSettings.UI;
using LethalSettings.UI.Components;

namespace RandomMoons.ConfigUtils;

[DataContract]
public class RMConfig : SyncedConfig<RMConfig>
{
    // Should we start the level when traveling to a new moon ? 
    public ConfigEntry<bool> AutoStart { get; private set; }

    // Should we explore a new moon once the level has ended ? 
    public ConfigEntry<bool> AutoExplore { get; private set; }

    // Should we be able to visit the same moon twice during a quota ?
    public ConfigEntry<bool> CheckIfVisitedDuringQuota { get; private set; }

    // Should the player be able to explore multiples times ?
    public ConfigEntry<bool> RestrictedCommandUsage { get; private set; }

    // What kind of moons can be selected ?
    public ConfigEntry<MoonSelection> MoonSelectionType { get; private set; }

    // Variable to try and sync
    [DataMember] public SyncedEntry<int> SyncedVar { get; private set; }

    // Bind config entries
    public RMConfig(ConfigFile cfg) : base("InnohVateur.RandomMoons")
    {
        ConfigManager.Register(this);

        AutoStart = cfg.Bind(
            "General",
            "AutoStart",
            false,
            "Automatically starts the level upon travelling to a random moon"
        );

        AutoExplore = cfg.Bind(
            "General",
            "AutoExplore",
            false,
            "Automatically explore to a random moon upon leaving the level"
        );

        CheckIfVisitedDuringQuota = cfg.Bind(
            "General",
            "RegisterTravels",
            false,
            "The same moon can't be chosen twice while the quota hasn't changed"
        );

        RestrictedCommandUsage = cfg.Bind(
            "General",
            "PreventMultipleTravels",
            true,
            "Prevents the players to execute explore multiple times without landing"
        );

        MoonSelectionType = cfg.Bind(
            "General",
            "MoonSelection",
            MoonSelection.ALL,
            "Can have three values : vanilla, modded or all, to change the moons that can be chosen. (Note : modded input without modded moons would do the same as all)"
        );
            
        SyncedVar = cfg.BindSyncedEntry(
            new ConfigDefinition("Please work", "Please work2"),
            4,
            new ConfigDescription("Please be synced for the love of god")
        );

        var SyncedVarInput = new IntSliderConfigItem(SyncedVar.Entry, new IntSliderOptions{
            RequiresRestart = false,
            Min = 0,
            Max = 100
        });

        LethalConfigManager.AddConfigItem(SyncedVarInput);
    }
}