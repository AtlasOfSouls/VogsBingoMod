/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VogsBingoMod.UI
{
    internal class UICanvas : MonoBehaviour
    {
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
        internal UITextInput? roomUrlInputField;
        internal UITextInput? nicknameInputField;
        internal UITextInput? passwordInputField;
        internal UIDropdown? teamColorsDropdown;
        internal UIText? loadingCardText;
        internal UIText? errorText;
        internal UIText? connectingErrorText;
        internal GameObject? connectionPendingIcon;
        internal List<Dropdown.OptionData> bingosyncColorOptions;
        internal List<Dropdown.OptionData> caravanColorOptions;
        internal float currentScale {get; private set;}
        static UICanvas? instance;
        float[] opacityOptions = {1, 0.8f, 0.5f, 0.3f};
        int currentOpacityIndex = 0;
        // int boardSize;
        UIGoal[] uiGoals;

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
            this.nicknameInputField = UIHelper.CreateUITextInput(this.joinRoomButton.gameObject.transform, "NicknameInput", "Enter your nickname...", 0, -180);
            this.passwordInputField = UIHelper.CreateUITextInput(this.joinRoomButton.gameObject.transform, "PasswordInput", "Enter the room password...", 0, -260, true);
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

        internal void ShowGoals()
        {
            revealCardButton?.gameObject.SetActive(false);
            for (int i = 0; i < uiGoals.Length; i++)
            {
                uiGoals[i].gameObject.SetActive(true);
            }
        }

        internal void HideGoals()
        {
            for (int i = 0; i < uiGoals.Length; i++)
            {
                uiGoals[i].gameObject.SetActive(false);
            }
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
                NetworkHandler.JoinRoom(roomUrlInputField.GetText(), nicknameInputField.GetText(), passwordInputField.GetText());
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
        }

        internal void RevealCardInHandMode()
        {
            NetworkHandler.SendRevealCardMessage();
            this.HideRevealCardButton();
            this.ShowGoals();
            UIHelper.CycleVisibility(UIHelper.VisibilityState.Nothing);
        }

        internal void UnrevealCard()
        {
            this.ShowRevealCardButton();
            this.HideGoals();
        }

        internal void ShowRevealCardButton()
        {
            this.revealCardButton?.gameObject.SetActive(true);
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
            currentOpacityIndex++;
            if (currentOpacityIndex >= opacityOptions.Length){
                currentOpacityIndex = 0;
            }
            Image[] images = gameObject.GetComponentsInChildren<Image>(true);
            foreach (Image image in images)
            {
                if (image.gameObject.name != "Blocker")
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, opacityOptions[currentOpacityIndex]);
                }
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
            boardObj.gameObject.transform.localScale = Vector3.one * scale;
            joinRoomButton?.gameObject.transform.localScale = Vector3.one * scale;
            exitRoomButton?.gameObject.transform.localScale = Vector3.one * scale;
            currentScale = scale;
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
                newGoals[i].SetOpacity(opacityOptions[currentOpacityIndex]);
            }
            uiGoals = newGoals;
            this.connectionPendingIcon?.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (-goalSpacing * boardSize) + Vector2.up * 25;
            HideGoals();
        }
    }
}
