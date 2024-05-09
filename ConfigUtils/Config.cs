using BepInEx.Bootstrap;
using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalSettings.UI;
using LethalSettings.UI.Components;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace RandomMoons.ConfigUtils
{

    public class SyncConfig
    {
        public static ConfigEntry<bool> AutoStart;
        public static ConfigEntry<bool> AutoExplore;
        public static ConfigEntry<bool> CheckIfVisitedDuringQuota;
        public static ConfigEntry<bool> RestrictedCommandUsage;
        public static ConfigEntry<MoonSelection> MoonSelectionType;


        public SyncConfig(ConfigFile cfg)
        {
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
        }

    }

    
}