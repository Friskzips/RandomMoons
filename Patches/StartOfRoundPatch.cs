using System.Threading;
using HarmonyLib;
using RandomMoons.Commands;
using RandomMoons.ConfigUtils;
using RandomMoons.Utils;
using UnityEngine;

namespace RandomMoons.Patches;

/// <summary>
/// Patches StartOfRound. Learn to read
/// </summary>
[HarmonyPatch(typeof(StartOfRound))]
internal class StartOfRoundPatch
{
    private static Terminal terminal = Object.FindObjectOfType<Terminal>(); // Find script Terminal.cs

    // Uses basically all the states
    [HarmonyPatch("Update")]
    [HarmonyPrefix]
    public static void UpdatePatch(StartOfRound __instance)
    {
        // Add moon to visitedMoons
        if (__instance.shipHasLanded && States.hasGambled)
        {
            States.hasGambled = false;
            States.visitedMoons.Add(States.lastVisitedMoon);
        }

        // Reset visitedMoons when game over
        if (__instance.suckingPlayersOutOfShip)
        {
            States.visitedMoons = [];
        }

        // Confirms the auto start
        if (__instance.travellingToNewLevel && States.startUponArriving)
        {
            States.confirmedAutostart = true;
            States.startUponArriving = false;
        }

        // Performs auto start
        //if (!__instance.travellingToNewLevel && States.confirmedAutostart)
        //{
                
        //}

        if (__instance.CanChangeLevels() && States.exploreASAP) // Performs auto explore
        {
            // TODO: redo the way the configs works
            if (RMConfig.Instance.AutoStart.Value)
                States.startUponArriving = true;

            // If there are more than 0 days left, perform the same as explore command, else travel to Gordion (Company Building)
            if (TimeOfDay.Instance.daysUntilDeadline > 0 || __instance.currentLevelID == States.companyBuildingLevelID)
            {
                SelectableLevel moon = ExploreCommand.ChooseRandomMoon(terminal.moonsCatalogueList);
                __instance.ChangeLevelServerRpc(moon.levelID, terminal.groupCredits);
                States.lastVisitedMoon = moon.PlanetName;
                States.hasGambled = true;
            }
            else {
                __instance.ChangeLevelServerRpc(States.companyBuildingLevelID, terminal.groupCredits);
            }
        }
    }

    [HarmonyPatch("ArriveAtLevel")]
    [HarmonyPostfix]
    public static void ArriveAtLevelPatch()
    {
        if (States.confirmedAutostart) return;

        Thread.Sleep(1000);
        States.confirmedAutostart = false;

        GameObject startLever = GameObject.Find("StartGameLever"); // Find ship's level game object
        if (startLever == null) return;

        StartMatchLever startMatchLever = startLever.GetComponent<StartMatchLever>(); // Find script component for the game object
        if (startMatchLever == null) return;

        startMatchLever.PullLever(); // Pulls the lever
        startMatchLever.LeverAnimation(); // Plays the animation
        startMatchLever.StartGame(); // Starts the level
    }

    [HarmonyPatch("ChangeLevel")]
    [HarmonyPrefix]
    public static void ChangeLevelPatch()
    {
        if (States.exploreASAP)
            States.exploreASAP = false;
    }

    [HarmonyPatch("EndOfGame")]
    [HarmonyPostfix]
    public static void EndOfGamePatch()
    {
        States.exploreASAP = true;
    }
}