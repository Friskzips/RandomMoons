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
    [DataContract]
    public class SyncConfig : SyncedConfig2<SyncConfig>
    {
        public ConfigEntry<float> DISPLAY_DEBUG_INFO { get; private set; }

        [DataMember] public SyncedEntry<float> EXAMPLE_VAR { get; private set; }

        [Obsolete] //And idk how to fix it
        public SyncConfig(ConfigFile cfg) : base("InnohVateur.RandomMoons") {
            ConfigManager.Register(this);

            EXAMPLE_VAR = cfg.BindSyncedEntry("General", "fExampleVar", 4.1f,
                "An example of a float value that will be synced."

            );

            }


        }



    }

}