using System.Runtime.Serialization;
using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalSettings.UI;
using LethalSettings.UI.Components;

namespace RandomMoons.ConfigUtils {
    [DataContract]
    public class SyncConfig : SyncedConfig<SyncConfig>
    {
        //Old configs without Csync
        /*
        // Should we start the level when traveling to a new moon ? 
        public static ConfigEntry<bool> AutoStart; 

        // Should we explore a new moon once the level has ended ? 
        public static ConfigEntry<bool> AutoExplore;

        // Should we be able to visit the same moon twice during a quota ?
        public static ConfigEntry<bool> CheckIfVisitedDuringQuota;

        // Should the player be able to explore multiples times ?
        public static ConfigEntry<bool> RestrictedCommandUsage;

        // What kind of moons can be selected ?
        public static ConfigEntry<MoonSelection> MoonSelectionType;
        */

        // Should we start the level when traveling to a new moon ? 
        [DataMember] public static SyncedEntry<bool> AutoStart;

        // Should we explore a new moon once the level has ended ? 
        [DataMember] public static SyncedEntry<bool> AutoExplore;

        // Should we be able to visit the same moon twice during a quota ?
        [DataMember] public static SyncedEntry<bool> CheckIfVisitedDuringQuota;

        // Should the player be able to explore multiples times ?
        [DataMember] public static SyncedEntry<bool> RestrictedCommandUsage;

        [DataMember] public static SyncedEntry<bool> IsBlacklistOn;

        // What kind of moons can be selected ?
        [DataMember] public static SyncedEntry<MoonSelection> MoonSelectionType;

        //Test variable, usefull for now, so it stays
        [DataMember] public static SyncedEntry<int> Synced_var { get; private set; }

        // Bind config entries
        public SyncConfig(ConfigFile cfg) : base("InnohVateur.RandomMoons")
        {
            ConfigManager.Register(this);

            //All of these variable should be synced when Owen's bux fixed will be apllied.
            //KeyWord : "SHOULD"

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

            IsBlacklistOn = cfg.BindSyncedEntry(
                new ConfigDefinition("General", "BlacklistOn"),
                false,
                new ConfigDescription("Is the blacklist on ? (Dont modify this, and this should not appear anywhere but R2's config manager")
                );
            
            // Synced variable try
            Synced_var = cfg.BindSyncedEntry(
                new ConfigDefinition("Please work", "Please work2"),
                4,
                new ConfigDescription("Please be synced for the love of god")
            );

            //lethal config stuff

            var Synced_var_input = new IntSliderConfigItem(Synced_var.Entry, new IntSliderOptions{
                RequiresRestart = false,
                Min = 0,
                Max = 100
            });

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

            LethalConfigManager.AddConfigItem(Synced_var_input);
            LethalConfigManager.AddConfigItem(AutoStart_input);
            LethalConfigManager.AddConfigItem(AutoExplore_input);
            LethalConfigManager.AddConfigItem(CheckIfVisitedDuringQuota_input);
            LethalConfigManager.AddConfigItem(RestrictedCommandUsage_input);
            LethalConfigManager.AddConfigItem(moonSelectionType_input);
            LethalConfigManager.SkipAutoGenFor(IsBlacklistOn.Entry);





        }
    }
}