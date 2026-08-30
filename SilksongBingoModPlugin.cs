/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SilksongBingoMod.Automarking;
using SilksongBingoMod.UI;
using UnityEngine;

namespace SilksongBingoMod;

[BepInAutoPlugin(id: "io.github.atlasofsouls.silksongbingomod")]
/// I apologize in advance if you are here to read through my code.
public partial class SilksongBingoModPlugin : BaseUnityPlugin
{
    internal const string PersistentName = "SilksongBingoMod";
    internal static SilksongBingoModPlugin instance;
    ManualLogSource logger;
    ConfigEntry<KeyCode> toggleUIVisibility;
    ConfigEntry<KeyCode> toggleUIOpacity;
    ConfigEntry<KeyCode> revealBoardKeybind;
    internal ConfigEntry<UIScaleOptions> uiScaleConfig;
    internal enum UIScaleOptions
    {
        Default,
        Small,
        Tiny
    }

    internal static void LogInfo(string str)
    {
        instance.logger.LogInfo(str);
    }

    internal static void LogError(string str)
    {
        instance.logger.LogError(str);
    }

    internal static void LogError(Exception e)
    {
        instance.logger.LogError($"{e.Message}, {e.StackTrace}");
    }

    void Update()
    {
        Automarker.RunFrameChecks();
        if (!UIHelper.IsTyping())
        {
            if (Input.GetKeyDown(toggleUIVisibility.Value))
            {
                UIHelper.CycleVisibility();
            }
            if (Input.GetKeyDown(toggleUIOpacity.Value))
            {
                UIHelper.CycleOpacity();
            }
            if (Input.GetKeyDown(revealBoardKeybind.Value))
            {
                UIHelper.RevealCardPressed();
            }
        }
        NetworkHandler.Update();
    }

    void Awake()
    {
        instance = this;
        this.logger = Logger;

        this.toggleUIVisibility = Config.Bind<KeyCode>("Keybinds","Toggle UI", KeyCode.B,"Cycles the currently active UI elements, allowing the user to show or hide the board as necessary.");
        this.toggleUIOpacity = Config.Bind<KeyCode>("Keybinds","Toggle Opacity", KeyCode.O,"Changes how transparent the UI is over the game.");
        this.revealBoardKeybind = Config.Bind<KeyCode>("Keybinds","Reveal Card", KeyCode.None,"Reveals the current bingo card.");
        this.uiScaleConfig = Config.Bind<UIScaleOptions>("UI Settings","UI Scale",UIScaleOptions.Default,"Change the size of the UI, such as the Bingo board.");
        uiScaleConfig.SettingChanged += UIScaleChanged;
        Harmony harmony = new Harmony(Id);
        harmony.PatchAll();

        UIHelper.uiCanvas.FirstAwake();
        Coroutiner.Initialize();
    }

    void OnDisable()
    {
        NetworkHandler.Dispose();
    }

    void UIScaleChanged(object? sender, EventArgs args)
    {
        UIHelper.UpdateUIScale();
    }
}
