/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using VogsBingoMod.Automarking;
using VogsBingoMod.UI;
using UnityEngine;
using Silksong.DataManager;
using System.Diagnostics.CodeAnalysis;

namespace VogsBingoMod;

[BepInAutoPlugin(id: "io.github.atlasofsouls.VogsBingoMod")]
[BepInDependency(Silksong.DataManager.DataManagerPlugin.Id)]
/// I apologize in advance if you are here to read through my code.
public partial class VogsBingoModPlugin : BaseUnityPlugin, ISaveDataMod<SaveData>
{
    internal const string PersistentName = "VogsBingoMod";
    internal static VogsBingoModPlugin instance;
    ManualLogSource logger;
    ConfigEntry<KeyCode> toggleUIVisibility;
    ConfigEntry<KeyCode> toggleUIOpacity;
    ConfigEntry<KeyCode> revealBoardKeybind;
    internal ConfigEntry<string> nameAutofill;
    internal ConfigEntry<string> passwordAutofill;
    internal ConfigEntry<UIScaleOptions> uiScaleConfig;
    internal ConfigEntry<AudioVolume> teamMarkSoundsVolume;
    internal ConfigEntry<AudioVolume> opponentMarkSoundsVolume;
    SaveData _saveData = new SaveData();

    [AllowNull]
    public SaveData SaveData
    {
        get => _saveData;
        set => _saveData = value == null ? new SaveData() : value;
    }

    internal enum UIScaleOptions
    {
        Default,
        Small,
        Tiny
    }

    internal static void LogInfo(string str)
    {
        try{
        instance.logger.LogInfo(str);
        } catch (Exception){}
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

        this.toggleUIVisibility = Config.Bind<KeyCode>("Keybinds","6: Toggle UI", KeyCode.B,"Cycles the currently active UI elements, allowing the user to show or hide the board as necessary.");
        this.toggleUIOpacity = Config.Bind<KeyCode>("Keybinds","7: Toggle Opacity", KeyCode.O,"Changes how transparent the UI is over the game.");
        this.revealBoardKeybind = Config.Bind<KeyCode>("Keybinds","8: Reveal Card", KeyCode.None,"Reveals the current bingo card.");
        this.uiScaleConfig = Config.Bind<UIScaleOptions>("UI Settings","5: UI Scale",UIScaleOptions.Default,"Change the size of the UI, such as the Bingo board.");
        this.nameAutofill = Config.Bind<string>("Autofill Options", "1: Default Name", "", "The nickname field for entering rooms will default to this value, helpful for reusing names.");
        this.passwordAutofill = Config.Bind<string>("Autofill Options", "2: Default Password", "fast", "The password field for entering rooms will default to this value. The default is \"fast\".");
        this.teamMarkSoundsVolume = Config.Bind<AudioVolume>("Audio Options", "3: Team Mark Sound Volume", AudioVolume.Medium, "Plays a sound when your color marks a goal.");
        this.opponentMarkSoundsVolume = Config.Bind<AudioVolume>("Audio Options", "4: Opponent Mark Sound Volume", AudioVolume.Medium, "Plays a sound when any color other than yours marks a goal.");
        uiScaleConfig.SettingChanged += UIScaleChanged;
        nameAutofill.SettingChanged += NameAutofillChanged;
        passwordAutofill.SettingChanged += PasswordAutofillChanged;
        teamMarkSoundsVolume.SettingChanged += TeamMarkVolumeChanged;
        opponentMarkSoundsVolume.SettingChanged += OpponentMarkVolumeChanged;
        Harmony harmony = new Harmony(Id);
        harmony.PatchAll();

        UIHelper.uiCanvas.FirstAwake();
        Coroutiner.Initialize();
    }

    void OnDisable()
    {
        NetworkHandler.Dispose();
    }

    void UIScaleChanged(object sender, EventArgs args)
    {
        UIHelper.UpdateUIScale();
    }

    void NameAutofillChanged(object sender, EventArgs args)
    {
        UICanvas instance = UICanvas.GetInstance();
        if (instance.nicknameInputField != null)
        {
            instance.nicknameInputField.InputComponent.text = nameAutofill.Value;
        }
    }

    void PasswordAutofillChanged(object sender, EventArgs args)
    {
        UICanvas instance = UICanvas.GetInstance();
        if (instance.passwordInputField != null)
        {
            instance.passwordInputField.InputComponent.text = passwordAutofill.Value;
        }
    }

    void TeamMarkVolumeChanged(object sender, EventArgs args)
    {
        AudioHelper.Instance.PlayTeamMarkSound(true);
    }

    void OpponentMarkVolumeChanged(object sender, EventArgs args)
    {
        AudioHelper.Instance.PlayOpponentMarkSound(true);
    }
}
