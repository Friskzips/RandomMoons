﻿using HarmonyLib;
using RandomMoons.ConfigUtils;
using RandomMoons.Utils;
using System;
using System.Linq;

namespace RandomMoons.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    public class TerminalPatch
    {
        [HarmonyPatch("QuitTerminal")]
        [HarmonyPrefix]
        public static void clearTerminalInteraction(Terminal __instance)
        {
            if(States.isInteracting)
            {
                States.closedUponConfirmation = true;
            }
        }

        [HarmonyPatch("BeginUsingTerminal")]
        [HarmonyPrefix]
        public static void registeringMoons(Terminal __instance)
        {
            if(SyncConfig.Instance.moonSelectionType.Value == MoonSelection.MODDED)
            {
                foreach(SelectableLevel lvl in __instance.moonsCatalogueList) {
                    if(!States.vanillaMoons.Contains(lvl.sceneName)) { return; }
                }

                SyncConfig.Instance.moonSelectionType.Value = MoonSelection.ALL;
            }
        }
    }
}
