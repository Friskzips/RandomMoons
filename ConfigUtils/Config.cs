using BepInEx.Bootstrap;
using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalSettings.UI;
using LethalSettings.UI.Components;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace RandomMoons.ConfigUtils
{
    [DataContract]
    public class SyncConfig : SyncedConfig<SyncConfig>
    {
        //Basic config stuff

        //Shouled we start the level when traveling to a new moon ? 
        public static ConfigEntry<bool> AutoStart; 

        //Should we explore a new moon once the level has ended ? 
        public static ConfigEntry<bool> AutoExplore;

        //Should we be able to visit the same moon twice during a quota ?
        public static ConfigEntry<bool> CheckIfVisitedDuringQuota;

        //Should the player be able to explore multiples times ?
        public static ConfigEntry<bool> RestrictedCommandUsage;

        //What kind of moons can be selected ?
        public static ConfigEntry<MoonSelection> MoonSelectionType;

        //Variable to try and sync
       [DataMember] public static SyncedEntry<int> Synced_var { get; private set; }

        [Obsolete]
        //Binding the configs
        public SyncConfig(ConfigFile cfg) :  base("InnohVateur.RandomMoons")
        {
            ConfigManager.Register(this);
            // Entry binding
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
            
            //Synced variable try
            Synced_var = cfg.BindSyncedEntry(
                new ConfigDefinition("Please work", "Please work2"),
                4,
                new ConfigDescription("Please be synced for the love of god")
                );

            var configEntry = cfg.Bind("General", "Example", 0, "This is an example component!");

            var Synced_var_input = new IntSliderConfigItem(Synced_var.Entry, new IntSliderOptions
            {
                RequiresRestart = false,
                Min = 0,
                Max = 100
            } );
            LethalConfigManager.AddConfigItem(Synced_var_input);

            
        }

    }

    
}