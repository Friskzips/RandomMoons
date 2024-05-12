using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalAPI.LibTerminal;
using LethalAPI.LibTerminal.Models;
using LethalConfig;
using RandomMoons.Commands;
using RandomMoons.ConfigUtils;
using RandomMoons.Patches;

namespace RandomMoons;

/// <summary>
/// Main plugin class
/// </summary>
[BepInPlugin(modGUID, modName, modVersion)]
[BepInDependency("LethalAPI.Terminal")] // TODO: update version Thunderstore mod id : LethalAPI-LethalAPI_Terminal-1.0.1
[BepInDependency("ainavt.lc.lethalconfig", BepInDependency.DependencyFlags.SoftDependency)] // TODO: update version Thunderstore mod id : AinaVT-LethalConfig-1.3.4
[BepInDependency("com.willis.lc.lethalsettings", BepInDependency.DependencyFlags.SoftDependency)] // TODO: update version Thunderstore mod id : willis81808-LethalSettings-1.4.0
[BepInDependency("io.github.CSync")]
public class RandomMoons : BaseUnityPlugin
{
    // Basic mod infos
    internal const string modGUID = "InnohVateur.RandomMoons";
    internal const string modName = "RandomMoons";
    internal const string modVersion = "1.3.0";

    // Harmony instance
    readonly Harmony harmony = new(modGUID);

    // Plugin instance
    static RandomMoons Instance;

    // Terminal API Registry Instance
    TerminalModRegistry Commands;

    internal static new ManualLogSource Logger;
    public static new RMConfig Config { get; private set; }

    // Executed at start
    private void Awake()
    {
        // Instantiates the Plugin
        if (Instance == null)
            Instance = this;

        Logger = base.Logger;
        Config = new(base.Config);

        // Instantiates the Terminal API Registry
        Commands = TerminalRegistry.CreateTerminalRegistry();

        // Registers the commands
        Commands.RegisterFrom(new ExploreCommand());

        try {
            Logger.LogInfo("Applying patches...");
            ApplyPluginPatches();

            Logger.LogInfo("Patches applied!");
        } catch(Exception e) {
            Logger.LogError(e);
        }

        // Plugin loaded!
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded and operational. Have fun!");
    }

    // Apply harmony patches
    private void ApplyPluginPatches()
    {
        harmony.PatchAll(typeof(RandomMoons));
        Logger.LogInfo("Patched RandomMoons");

        harmony.PatchAll(typeof(TerminalPatch));
        Logger.LogInfo("Patched Terminal");

        harmony.PatchAll(typeof(StartOfRoundPatch));
        Logger.LogInfo("Patched StartOfRound");
    }
}