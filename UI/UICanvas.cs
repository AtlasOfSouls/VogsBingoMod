/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VogsBingoMod.UI
{
    internal class UICanvas : MonoBehaviour
    {
        const string infoTextString = "How to Use VOG'S BINGO MOD\n\nTo use the bingo mod, you need to create a bingo room. There is a brief guide to using BingyFlea (a discord bot that makes rooms for you) on youtube titled \"How to Set up Silksong Bingo\".\n\nOnce you have the room link, you put it the room link field as well as your nickname and password (The default password is \"fast\").\n\nYou can change the board opacity, show/hide this mod's UI, and reveal the board with keybinds that can be set in options->mods->Vogs Bingo Mod. If you don't have that menu, you may need to install the ModMenu mod or use a different way of changing BepInEx configs.\nThe config also has options for sound cues and UI size, so make sure to check those out as well.\n\nLastly, when you've revealed a bingo board, if a goal doesn't mark automatically, you can click the goal to manually mark it. Goals that end with \"(M)\" will always need to be marked manually.\n\nHave fun playing Bingo!\n\n(Click this box or the \"How to Use\" button to close the How to Use information)";
        const int goalSpacing = 110;
        const int goalsXOffset = 90;
        const int goalsYOffset = -10;
        const int sortOrder = 10000;
        const int defaultBoardSize = 5;
        const int defaultcolorOptions = 10;
        const float defaultUIScale = 1;
        const float smallUIScale = 0.8f;
        const float tinyUIScale = 0.6f;

        internal GameObject boardObj;
        internal UIButton? revealCardButton;
        internal UIButton? revealHandModeButton;
        internal UIButton? joinRoomButton;
        internal UIButton? exitRoomButton;
        internal UIButton? infoButton;
        internal UITextInput? roomUrlInputField;
        internal UITextInput? nicknameInputField;
        internal UITextInput? passwordInputField;
        internal UIDropdown? teamColorsDropdown;
        internal UIText? loadingCardText;
        internal UIText? errorText;
        internal UIText? connectingErrorText;
        internal UIText? timerText;
        internal UIButton? infoText;
        internal GameObject? connectionPendingIcon;
        internal List<Dropdown.OptionData> bingosyncColorOptions;
        internal List<Dropdown.OptionData> caravanColorOptions;
        internal float currentScale {get; private set;}
        internal float CurrentOpacity {get => opacityOptions[(int)VogsBingoModPlugin.instance.opacitySetting.Value];}
        internal float CurrentTextOpacity {get => textOpacityOptions[(int)VogsBingoModPlugin.instance.opacitySetting.Value];}
        internal bool CanMarkGoals => (revealCardButton != null && !revealCardButton.gameObject.activeSelf) || (revealHandModeButton != null && !revealHandModeButton.gameObject.activeSelf);
        internal bool areGoalsVisible = false;
        static UICanvas? instance;
        float[] opacityOptions = {1, 0.8f, 0.5f, 0.3f, 0.1f};
        float[] textOpacityOptions = {1, 1, 0.9f, 0.7f, 0.5f};
        UIGoal[] uiGoals;
        float currentTimerSegment = 0f;
        float timerStartPoint;
        float previousTimerAmount = 0f;
        float previousGoalMarkTime = 0f;
        bool isTimerPaused = true;

        internal static UICanvas GetInstance()
        {
            if (instance == null)
            {
                instance = new GameObject("BingoCanvas").AddComponent<UICanvas>();
            }
            return instance;
        }

        internal void FirstAwake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            this.boardObj = UIHelper.CreateUIObject(this.transform, "BoardContainer", anchor: UIAnchor.TopRight);
            SceneManager.activeSceneChanged += this.OnSceneChange;
            this.gameObject.SetActive(false);
        }

        internal void Initialize()
        {
            Canvas canvasComponent = this.gameObject.AddComponent<Canvas>();
            CanvasScaler scaler = this.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = Vector2.right * 1920 + Vector2.up * 1080;
            scaler.matchWidthOrHeight = 1;
            GraphicRaycaster gRaycaster = this.gameObject.AddComponent<GraphicRaycaster>();
            canvasComponent.sortingOrder = sortOrder;
            canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
            gRaycaster.blockingMask = (LayerMask)0;
            
            revealCardButton = UIHelper.CreateUIButton(boardObj.transform, "RevealCardButton", RevealCard, null, "Reveal Card", -230, -200);
            revealCardButton.gameObject.SetActive(false);
            revealHandModeButton = UIHelper.CreateUIButton(revealCardButton.transform, "RevealCardInHandModeButton", RevealCardInHandMode, null, "Automark With Card Hidden", 0, -110);
            loadingCardText = UIHelper.CreateUIText(boardObj.transform, "LoadingCardText", "Loading Card...", 30, -230, -200, 300);
            loadingCardText.gameObject.SetActive(false);
            errorText = UIHelper.CreateUIText(this.transform, "ErrorText", "An error occurred while communicating with the room.", 30 , -20, -760, 420, color: Color.red, canvasAnchor: UIAnchor.TopRight);
            errorText.gameObject.SetActive(false);
            connectionPendingIcon = UIHelper.CreateUIImage(this.boardObj.transform, "ConnectionPendingIcon", "ConnectionPendingIcon");
            connectionPendingIcon.SetActive(false);
            SetupGoals(defaultBoardSize);
            HideGoals();

            this.joinRoomButton = UIHelper.CreateUIButton(this.transform, "JoinRoomButton", JoinRoomButtonClicked, null, "Join Room", -20, -655, UIAnchor.TopRight);
            this.connectingErrorText = UIHelper.CreateUIText(joinRoomButton.transform, "ConnectionErrorText", "An error occurred while connecting to the room. Check that the room link and password are correct.", 30, 0, 160, 420, 200, color: Color.red);
            this.connectingErrorText.gameObject.SetActive(false);
            this.exitRoomButton = UIHelper.CreateUIButton(this.transform, "ExitRoomButton", ExitRoomButtonClicked, null, "Exit Room", -20, -675, UIAnchor.TopRight);
            this.exitRoomButton.gameObject.SetActive(false);
            this.roomUrlInputField = UIHelper.CreateUITextInput(this.joinRoomButton.gameObject.transform, "RoomLinkInput", "Enter the room link...", 0, -100);
            this.nicknameInputField = UIHelper.CreateUITextInput(this.joinRoomButton.gameObject.transform, "NicknameInput", "Enter your nickname...", 0, -180, startingText: VogsBingoModPlugin.instance.nameAutofill.Value);
            this.passwordInputField = UIHelper.CreateUITextInput(this.joinRoomButton.gameObject.transform, "PasswordInput", "Enter the room password...", 0, -260, true, VogsBingoModPlugin.instance.passwordAutofill.Value);
            UIHelper.SetupJoinRoomFieldNavigation();
            this.infoButton = UIHelper.CreateUIButton(joinRoomButton.gameObject.transform, "InfoButton", ToggleInfoText, null, "How to Use", 59, -325, width: 300, height: 50);
            this.infoText = UIHelper.CreateUIButton(UICanvas.GetInstance().transform, "InformationText", ToggleInfoText, null, infoTextString, width: 1000, height: 1000);
            this.timerText = UIHelper.CreateUIText(boardObj.transform, "TimerText", "00:00:00\n00:00:00", 30, -830, 10, 400, 200, anchor: TextAnchor.MiddleRight);
            this.ShowHideTimerIfAllowed(false);
            this.infoText.transform.GetChild(0).gameObject.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            this.ToggleInfoText();
            bingosyncColorOptions = new List<Dropdown.OptionData>();
            caravanColorOptions = new List<Dropdown.OptionData>();
            for (int i = 0; i < defaultcolorOptions; i++)
            {
                bingosyncColorOptions.Add(new Dropdown.OptionData($"  {GoalColors.ColorOptions[i+GoalColors.BingosyncColorsFirstID]}"));
                caravanColorOptions.Add(new Dropdown.OptionData($"  {GoalColors.ColorOptions[i+GoalColors.CaravanColorsFirstID]}"));
            }
            teamColorsDropdown = UIHelper.CreateUIDropdown(this.exitRoomButton.transform, "TeamSelector", TeamChanged, bingosyncColorOptions, -325, 30);
            UpdateUIScale();
        }

        internal void FixedUpdate()
        {
            if (connectionPendingIcon != null && connectionPendingIcon.activeInHierarchy)
            {
                connectionPendingIcon.transform.Rotate(Vector3.forward * 3, Space.Self);
            }
        }

        internal void Update()
        {
            if (!isTimerPaused)
            {
                currentTimerSegment = Mathf.Abs(Time.realtimeSinceStartup - timerStartPoint);
            }
            string currentTime = TimeSpan.FromSeconds(Mathf.RoundToInt(previousTimerAmount + currentTimerSegment)).ToString();
            string lastGoalMarkTime = TimeSpan.FromSeconds(Mathf.RoundToInt(previousGoalMarkTime)).ToString();
            timerText?.textComponent.text = $"Last Goal Marked: {lastGoalMarkTime}\n Current: {currentTime}";
        }

        internal void ShowGoals()
        {
            revealCardButton?.gameObject.SetActive(false);
            this.ShowHideTimerIfAllowed(true);
            for (int i = 0; i < uiGoals.Length; i++)
            {
                uiGoals[i].gameObject.SetActive(true);
            }
            areGoalsVisible = true;
            ShowHideTimerIfAllowed(true);
        }

        internal void HideGoals()
        {
            this.ShowHideTimerIfAllowed(false);
            for (int i = 0; i < uiGoals.Length; i++)
            {
                uiGoals[i].gameObject.SetActive(false);
            }
            areGoalsVisible = false;
            ShowHideTimerIfAllowed(false);
        }

        internal void OnSceneChange(Scene oldScene, Scene newScene)
        {
            if (newScene.name.Equals("Menu_Title"))
            {
                this.Initialize();
                this.gameObject.SetActive(true);
                SceneManager.activeSceneChanged -= this.OnSceneChange;
            }
        }

        internal void JoinRoomButtonClicked()
        {
            VogsBingoModPlugin.LogInfo($"The room link that was entered was: {roomUrlInputField?.GetText()}");
            if (NetworkHandler.CanConnectToRoom && roomUrlInputField != null && nicknameInputField != null && passwordInputField != null)
            {
                NetworkHandler.JoinRoom(roomUrlInputField.GetText().Trim(), nicknameInputField.GetText().Trim(), passwordInputField.GetText());
            }
        }

        internal void ExitRoomButtonClicked()
        {
            if (NetworkHandler.CanDisconnectFromRoom)
            {
                this.exitRoomButton?.gameObject.SetActive(false);
                this.revealCardButton?.gameObject.SetActive(false);
                HideGoals();
                NetworkHandler.ExitRoom();
            }
        }

        internal void SetGoalNames(string[] goalNames, int boardSize)
        {
            SetupGoals(boardSize);
            for (int i = 0; i < goalNames.Length; i++)
            {
                uiGoals[i].SetGoalName(goalNames[i]);
            }
        }

        internal void SetBoardColors(int[][] colorIDs)
        {
            for (int i = 0; i < colorIDs.Length; i++)
            {
                uiGoals[i].SetColors(colorIDs[i]);
                uiGoals[i].isHighlighted = false;
            }
        }

        internal void MarkGoal(int colorID, int slotIndex)
        {
            uiGoals[slotIndex].MarkGoal(colorID);
        }

        internal void UnmarkGoal(int colorID, int slotIndex)
        {
            uiGoals[slotIndex].UnmarkGoal(colorID);
        }

        internal void ResetBoard()
        {
            for (int i = 0; i < uiGoals.Length; i++)
            {
                uiGoals[i].ResetColors();
            }
        }

        internal void TeamChanged(int newColorID)
        {
            if (NetworkHandler.roomType == RoomType.Bingosync)
            {
                newColorID++;
            } else
            {
                newColorID+=11;
            }
            GoalColors.myColorID = newColorID;
            VogsBingoModPlugin.LogInfo($"Switched color to {newColorID}, AKA {GoalColors.IDToName(newColorID)}");
            NetworkHandler.SetMyColor(newColorID);
        }

        internal void RevealCard()
        {
            NetworkHandler.SendRevealCardMessage();
            this.HideRevealCardButton();
            this.ShowGoals();
            if (isTimerPaused && VogsBingoModPlugin.instance.startTimerOnReveal.Value)
            {
                this.StartStopTimer();
            }
        }

        internal void RevealCardInHandMode()
        {
            this.revealHandModeButton?.gameObject.SetActive(false);
            UIHelper.CycleVisibility(UIHelper.VisibilityState.Nothing);
            if (isTimerPaused && VogsBingoModPlugin.instance.startTimerOnAutomarkWithHidden.Value)
            {
                this.StartStopTimer();
            }
        }

        internal void UnrevealCard()
        {
            this.ShowRevealCardButton();
            this.HideGoals();
        }

        internal void ShowRevealCardButton()
        {
            this.revealCardButton?.gameObject.SetActive(true);
            this.revealHandModeButton?.gameObject.SetActive(true);
        }

        internal void HideRevealCardButton()
        {
            this.revealCardButton?.gameObject.SetActive(false);
        }

        internal void ShowCardLoadingText()
        {
            this.loadingCardText?.gameObject.SetActive(true);
            HideRevealCardButton();
        }

        internal void HideCardLoadingText()
        {
            this.loadingCardText?.gameObject.SetActive(false);
        }

        internal void CycleOpacity()
        {
            int currentOpacity = (int)VogsBingoModPlugin.instance.opacitySetting.Value + 1;
            if (currentOpacity >= Enum.GetValues(typeof(OpacityOptions)).Length)
            {
                currentOpacity = (int)OpacityOptions.Opaque;
            }
            VogsBingoModPlugin.instance.opacitySetting.Value = (OpacityOptions)currentOpacity;
            UpdateOpacity();
        }

        internal void UpdateOpacity()
        {
            Image[] images = gameObject.GetComponentsInChildren<Image>(true);
            foreach (Image image in images)
            {
                if (image.gameObject.name != "Blocker")
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, this.CurrentOpacity);
                }
            }
            Text[] textArr = gameObject.GetComponentsInChildren<Text>(true);
            foreach (Text textComp in textArr)
            {
                textComp.color = new Color(textComp.color.r, textComp.color.g, textComp.color.b, this.CurrentTextOpacity);
            }
        }

        internal bool HasColor(int slotIndex, int colorID)
        {
            return uiGoals[slotIndex].HasColor(colorID);
        }

        internal void SetDropdownOptions(RoomType roomType)
        {
            if (roomType == RoomType.Bingosync)
            {
                this.teamColorsDropdown?.SetOptions(bingosyncColorOptions);
            } else
            {
                this.teamColorsDropdown?.SetOptions(caravanColorOptions);
            }
        }

        internal void UpdateUIScale()
        {
            float scale = GetUIScaleFromConfig();
            boardObj.transform.localScale = Vector3.one * scale;
            joinRoomButton?.gameObject.transform.localScale = Vector3.one * scale;
            exitRoomButton?.gameObject.transform.localScale = Vector3.one * scale;
            currentScale = scale;
        }

        internal void UpdateGoalTextColor()
        {
            foreach (UIGoal goal in uiGoals)
            {
                goal.UpdateTextColor();
            }
        }

        internal void CloseInfoTextIfOpen()
        {
            if (infoText != null && infoText.gameObject.activeSelf)
            {
                infoText.gameObject.SetActive(false);
            }
        }

        internal void ShowHideTimerIfAllowed(bool shouldShow)
        {
            if (VogsBingoModPlugin.instance.showTimer.Value && shouldShow && areGoalsVisible)
            {
                this.timerText?.gameObject.SetActive(true);
            } else
            {
                this.timerText?.gameObject.SetActive(false);
            }
        }

        internal void StartStopTimer()
        {
            if (isTimerPaused)
            {
                isTimerPaused = false;
                timerStartPoint = Time.realtimeSinceStartup;
            } else
            {
                previousTimerAmount += currentTimerSegment;
                currentTimerSegment = 0f;
                isTimerPaused = true;
            }
        }

        internal void ResetTimer()
        {
            if (!isTimerPaused)
            {
                StartStopTimer();
            }
            currentTimerSegment = 0f;
            previousTimerAmount = 0f;
            previousGoalMarkTime = 0f;
            timerText?.textComponent.text = "00:00:00";
        }

        internal void UpdateGoalMarkTime()
        {
            previousGoalMarkTime = previousTimerAmount + currentTimerSegment;

        }

        float GetUIScaleFromConfig() =>(VogsBingoModPlugin.UIScaleOptions)VogsBingoModPlugin.instance.uiScaleConfig.BoxedValue
        switch
        {
            VogsBingoModPlugin.UIScaleOptions.Default => defaultUIScale,
            VogsBingoModPlugin.UIScaleOptions.Small => smallUIScale,
            VogsBingoModPlugin.UIScaleOptions.Tiny => tinyUIScale,
            _ => 1.0f
        };

        void SetupGoals(int boardSize)
        {
            UIGoal[] newGoals = new UIGoal[boardSize * boardSize];
            int i = 0;
            if (uiGoals != null)
            {
                for (; i < newGoals.Length && i < uiGoals.Length; i++)
                {
                    int xPos = i%boardSize * goalSpacing + goalsXOffset - goalSpacing*boardSize;
                    int yPos = i/boardSize * -goalSpacing + goalsYOffset;
                    newGoals[i] = uiGoals[i];
                    newGoals[i].SetPosition(xPos, yPos);
                }
            }
            for (; i < newGoals.Length; i++)
            {
                int xPos = i%boardSize * goalSpacing + goalsXOffset - goalSpacing*boardSize;
                int yPos = i/boardSize * -goalSpacing + goalsYOffset;
                newGoals[i] = UIHelper.CreateUIGoal(this.boardObj.transform, $"Goal{i}", xPos, yPos, i);
            }
            uiGoals = newGoals;
            this.connectionPendingIcon?.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (-goalSpacing * boardSize) + Vector2.up * -35;
            HideGoals();
        }

        void ToggleInfoText()
        {
            if (infoText != null)
            {
                infoText.gameObject.SetActive(!infoText.gameObject.activeSelf);
            }
        }
    }
}
