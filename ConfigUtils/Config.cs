using System.Runtime.Serialization;
using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;


namespace RandomMoons.ConfigUtils;

[DataContract]
public class RMConfig : SyncedConfig<RMConfig>
{
    // Should we start the level when traveling to a new moon ? 
    [DataMember] public SyncedEntry<bool> AutoStart { get; private set; }

    // Should we explore a new moon once the level has ended ? 
    [DataMember] public SyncedEntry<bool> AutoExplore { get; private set; }

    // Should we be able to visit the same moon twice during a quota ?
    [DataMember] public SyncedEntry<bool> CheckIfVisitedDuringQuota { get; private set; }

    // Should the player be able to explore multiples times ?
    [DataMember] public SyncedEntry<bool> RestrictedCommandUsage { get; private set; }

    // What kind of moons can be selected ?
    [DataMember] public SyncedEntry<MoonSelection> MoonSelectionType { get; private set; }

    // Variable to try and sync
    [DataMember] public SyncedEntry<int> SyncedVar { get; private set; }

    // Bind config entries
    public RMConfig(ConfigFile cfg) : base("InnohVateur.RandomMoons")
    {
        ConfigManager.Register(this);

        AutoStart = cfg.BindSyncedEntry(
            new ConfigDefinition("General","AutoStart"),
            false,
            new ConfigDescription("Automatically starts the level upon travelling to a random moon")
        );

        AutoExplore = cfg.BindSyncedEntry(
            new ConfigDefinition("General","AutoExplore"),
            false,
            new ConfigDescription("Automatically explore to a random moon upon leaving the level")
        );

        CheckIfVisitedDuringQuota = cfg.BindSyncedEntry(
            new ConfigDefinition("General","RegisterTravels"),
            false,
            new ConfigDescription("The same moon can't be chosen twice while the quota hasn't changed")
        );

        RestrictedCommandUsage = cfg.BindSyncedEntry(
            new ConfigDefinition("General","PreventMultipleTravels"),
            true,
            new ConfigDescription("Prevents the players to execute explore multiple times without landing")
        );

        MoonSelectionType = cfg.BindSyncedEntry(
            new ConfigDefinition("General","MoonSelection"),
            MoonSelection.ALL,
            new ConfigDescription("Can have three values : vanilla, modded or all, to change the moons that can be chosen. (Note : modded input without modded moons would do the same as all)")
        );
            
        SyncedVar = cfg.BindSyncedEntry(
            new ConfigDefinition("Please work", "Please work2"),
            4,
            new ConfigDescription("Please be synced for the love of god")
        );

        var AutoStart_input = new BoolCheckBoxConfigItem(AutoStart.Entry, new BoolCheckBoxOptions
        {
            RequiresRestart = false
        });

        var AutoExplore_input = new BoolCheckBoxConfigItem(AutoExplore.Entry, new BoolCheckBoxOptions
        {
            RequiresRestart = false
        });

        var CheckIfVisitedDuringQuota_input = new BoolCheckBoxConfigItem(CheckIfVisitedDuringQuota.Entry, new BoolCheckBoxOptions
        {
            RequiresRestart = false
        });

        var RestrictedCommandUsage_input = new BoolCheckBoxConfigItem(RestrictedCommandUsage.Entry, new BoolCheckBoxOptions
        {
            RequiresRestart = false
        });


        EnumDropDownConfigItem<MoonSelection> moonSelectionType_input = new EnumDropDownConfigItem<MoonSelection>(MoonSelectionType.Entry, false);

        LethalConfigManager.AddConfigItem(AutoStart_input);
        LethalConfigManager.AddConfigItem(AutoExplore_input);
        LethalConfigManager.AddConfigItem(CheckIfVisitedDuringQuota_input);
        LethalConfigManager.AddConfigItem(RestrictedCommandUsage_input);
        LethalConfigManager.AddConfigItem(moonSelectionType_input);

    }
    }
