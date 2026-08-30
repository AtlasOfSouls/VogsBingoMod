/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections;
using System.Collections.Generic;
using SilksongBingoMod.Automarking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SilksongBingoMod.UI
{
    internal static class UIHelper
    {
        const int defaultDropdownOptionWidth = 200;
        const int defaultDropdownOptionHeight = 35;
        const int defaultDropdownFontSize = 25;
        const int defaultButtonFontSize = 30;
        const int defaultTextInputFontSize = 30;
        const int defaultMaxTextResize = 25;
        const int defaultMinTextResize = 14;
        const int defaultButtonHeight = 100;
        const int defaultButtonWidth = 418;
        const int defaultTextInputHeight = 66;
        const int defaultTextInputWidth = 418;
        const int defaultGoalWidth = 100;
        const int defaultGoalHeight = 100;
        internal static UICanvas uiCanvas = UICanvas.GetInstance();
        static VisibilityState visibilityState = VisibilityState.Everything;
        internal enum VisibilityState
        {
            Everything,
            JustBoard,
            Nothing
        }

        // _Main methods are primarily for NetworkHandler to perform operations on the main thread to avoid rendering bugs
        internal static IEnumerator NotifyOfRoomEnter_Main(RoomType roomType)
        {
            yield return null;
            uiCanvas.exitRoomButton?.gameObject.SetActive(true);
            uiCanvas.SetDropdownOptions(roomType);
            GoalColors.SetMyColorToDefault(roomType);
        }

        internal static IEnumerator SetBoard_Main(string boardJson, RoomType roomType)
        {
            yield return null;
            string[] goalNames = JsonHelper.GetNamesFromBoard(boardJson);
            try{
                Automarker.SetGoalNames(goalNames);
                uiCanvas.SetGoalNames(Automarker.AddSupportNotations(goalNames), roomType == RoomType.Bingosync ? 5 : 6);
                Texture2D texture = new Texture2D(1, 1);
            } catch (Exception e)
            {
                SilksongBingoModPlugin.LogError(e);
            }
            uiCanvas.SetBoardColors(JsonHelper.GetColorsFromBoard(boardJson));
            uiCanvas.HideGoals();
            uiCanvas.HideCardLoadingText();
            uiCanvas.ShowRevealCardButton();
        }

        internal static IEnumerator NotifyOfConnectingToRoom_Main()
        {
            yield return null;
            uiCanvas.joinRoomButton?.gameObject.SetActive(false);
        }

        internal static IEnumerator NotifyOfConnectingToRoomCancel_Main(int errorCode = 0)
        {
            yield return null;
            uiCanvas.joinRoomButton?.gameObject.SetActive(true);
            string errorText = "An error occurred while connecting to the room ";
            switch (errorCode)
            {
                default: case 0:
                    errorText += "(0). Check that the room link and password are correct.";
                    break;
                case 1:
                    errorText += "(1). Check that your internet connection is working.";
                    break;
            }
            TriggerErrorText(uiCanvas.connectingErrorText, errorText);
        }

        internal static IEnumerator NotifyOfRoomExit_Main(int errorCode = 0)
        {
            yield return null;
            uiCanvas.joinRoomButton?.gameObject.SetActive(true);
            uiCanvas.HideGoals();
            uiCanvas.exitRoomButton?.gameObject.SetActive(false);
            uiCanvas.HideRevealCardButton();
            if (errorCode > 0)
            {
                TriggerErrorText(uiCanvas.connectingErrorText, $"An error occurred while trying to communicate with the room. ({errorCode})");
            }
        }

        internal static IEnumerator UpdateGoal_Main(int colorID, bool shouldRemoveColor, int slotIndex)
        {
            yield return null;
            if (shouldRemoveColor)
            {
                uiCanvas.UnmarkGoal(colorID, slotIndex);
            } else {
                uiCanvas.MarkGoal(colorID, slotIndex);
            }
        }

        internal static IEnumerator UnrevealCard_Main()
        {
            yield return null;
            uiCanvas.HideGoals();
            uiCanvas.ShowCardLoadingText();
        }

        internal static IEnumerator TriggerErrorText_Main()
        {
            yield return null;
            uiCanvas.errorText?.gameObject.SetActive(true);
            Coroutiner.CreateCoroutine(DisableErrorText(uiCanvas.errorText?.gameObject));
        }

        internal static IEnumerator SetConnectionPendingActive_Main(bool setActive)
        {
            yield return null;
            uiCanvas.connectionPendingIcon?.SetActive(setActive);
        }

        internal static IEnumerator DisableErrorText(GameObject? errorObject)
        {
            yield return new WaitForSecondsRealtime(3);
            errorObject?.SetActive(false);
        }

        internal static void CycleVisibility()
        {
            switch (visibilityState)
            {
                case VisibilityState.Everything:
                    if (NetworkHandler.connectState == ConnectionState.Connected)
                    {
                        uiCanvas.exitRoomButton?.gameObject.SetActive(false);
                        visibilityState = VisibilityState.JustBoard;
                    } else if (NetworkHandler.connectState == ConnectionState.NotConnected)
                    {
                        uiCanvas.joinRoomButton?.gameObject.SetActive(false);
                        visibilityState = VisibilityState.Nothing;
                    }
                    break;
                case VisibilityState.JustBoard:
                    uiCanvas.boardObj.gameObject.SetActive(false);
                    visibilityState = VisibilityState.Nothing;
                    break;
                case VisibilityState.Nothing:
                    if (NetworkHandler.connectState == ConnectionState.Connected)
                    {
                        uiCanvas.boardObj.SetActive(true);
                        uiCanvas.exitRoomButton?.gameObject.SetActive(true);
                        visibilityState = VisibilityState.Everything;
                    } else
                    {
                        uiCanvas.boardObj.SetActive(true);
                        uiCanvas.joinRoomButton?.gameObject.SetActive(true);
                        visibilityState = VisibilityState.Everything;
                    }
                    break;
                default:
                    break;
            }
        }

        internal static void CycleVisibility(VisibilityState stateToCycleTo)
        {
            VisibilityState startState = visibilityState;
            while (visibilityState != stateToCycleTo)
            {
                CycleVisibility();
                if (visibilityState == startState)
                {
                    //After doing a full cycle, the goal state could not be found
                    return;
                }
            }
        }

        internal static UIGoal CreateUIGoal(Transform parent, string objName, float xOffset, float yOffset, int slotIndex, float width = defaultGoalWidth, float height = defaultGoalHeight)
        {
            GameObject newObj = CreateUIObject(parent, objName, xOffset, yOffset, anchor: UIAnchor.TopRight);
            newObj.AddComponent<CanvasRenderer>();
            Image imageComponent = SetupImageComponent(newObj, TextureHandler.standardBackgroundName, width, height);
            UIGoal uiGoal = newObj.AddComponent<UIGoal>();
            uiGoal.slotIndex = slotIndex;
            uiGoal.goalColors = CreateUIGoalColorsObject(newObj.transform, $"{objName}Colors");
            try{
                Text textComponent = CreateUIText(newObj.transform, $"{objName}Text", "Talk to Forge Daughter and Twelfth Architect", width: width - 10, height: height - 10).textComponent;
                textComponent.resizeTextForBestFit = true;
                textComponent.resizeTextMaxSize = defaultMaxTextResize;
                textComponent.resizeTextMinSize = defaultMinTextResize;
            } catch(Exception e)
            {
                SilksongBingoModPlugin.LogError(e);
            }
            newObj.AddComponent<UIButton>().SetupCallbacks(uiGoal.OnLeftClick, uiGoal.OnRightClick);
            return uiGoal;
        }

        internal static UIText CreateUIText(Transform parent, string objName, string text = "", int fontSize = 14, float xOffset = 0, float yOffset = 0, float width = 100, float height = 100, bool supportRichText = true, TextAnchor anchor = TextAnchor.MiddleCenter, Color? color = null, UIAnchor canvasAnchor = UIAnchor.MiddleCenter)
        {
            GameObject newObj = CreateUIObject(parent, objName, xOffset, yOffset, width, height, canvasAnchor);
            newObj.AddComponent<CanvasRenderer>();
            Text textComponent = newObj.AddComponent<Text>();
            textComponent.text = text;
            textComponent.font = FontHandler.GetFont("LegacyRuntime");
            textComponent.alignment = anchor;
            textComponent.fontSize = fontSize;
            textComponent.supportRichText = supportRichText;
            if (color == null)
            {
                color = Color.white;
            }
            textComponent.color = (Color)color;
            UIText uiText = newObj.AddComponent<UIText>();
            uiText.textComponent = textComponent;
            return uiText;
        }

        internal static GameObject CreateUIObject(Transform parent, string objName, float xOffset = 0, float yOffset = 0, float width = 100, float height = 100, UIAnchor anchor = UIAnchor.MiddleCenter)
        {
            GameObject newObj = new GameObject(objName);
            RectTransform rectTransform = newObj.AddComponent<RectTransform>();
            newObj.transform.SetParent(parent);
            rectTransform.localScale = Vector3.one;
            if (anchor == UIAnchor.TopRight)
            {
                rectTransform.anchorMin = Vector2.one;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.pivot = Vector2.one;
            }
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            rectTransform.anchoredPosition = Vector2.right * xOffset + Vector2.up * yOffset;
            return newObj;
        }

        internal static UIButton CreateUIButton(Transform parent, string objName, Action? leftClickCallback, Action? rightClickCallback, string displayText, float xOffset = 0, float yOffset = 0,  UIAnchor anchor = UIAnchor.MiddleCenter, float width = defaultButtonWidth, float height = defaultButtonHeight)
        {
            GameObject newObj = CreateUIObject(parent, objName, xOffset, yOffset, anchor: anchor);
            newObj.AddComponent<CanvasRenderer>();
            SetupImageComponent(newObj, TextureHandler.standardBackgroundName, width, height);
            UIButton buttonComponent = newObj.AddComponent<UIButton>();
            buttonComponent.SetupCallbacks(leftClickCallback, rightClickCallback);
            CreateUIText(newObj.transform, $"{objName}Text", displayText, defaultButtonFontSize, width: newObj.GetComponent<RectTransform>().rect.width);
            return buttonComponent;
        }

        internal static UITextInput CreateUITextInput(Transform parent, string objName, string textWhenEmpty, float xOffset = 0, float yOffset = 0)
        {
            GameObject newObj = CreateUIObject(parent, objName, xOffset, yOffset);
            SetupImageComponent(newObj, TextureHandler.standardBackgroundName, defaultTextInputWidth, defaultTextInputHeight);
            CreateUIText(newObj.transform, $"{objName}Text", string.Empty, defaultTextInputFontSize, width: newObj.GetComponent<RectTransform>().rect.width);
            UITextInput uiTextInput = newObj.AddComponent<UITextInput>();
            InputField inputComponent = newObj.AddComponent<InputField>();
            inputComponent.textComponent = newObj.GetComponentInChildren<Text>(true);
            inputComponent.onValueChanged.AddListener(uiTextInput.UpdatePlaceholder);
            uiTextInput.placeholderObj = CreateUIText(newObj.transform, $"{objName}Placeholder", textWhenEmpty, defaultTextInputFontSize, width: defaultTextInputWidth, height: defaultTextInputHeight, color: Color.grey).gameObject;
            return uiTextInput;
        }

        internal static UIDropdown CreateUIDropdown(Transform parent, string objName, UnityAction<int> callbackOnValueChanged, List<Dropdown.OptionData> optionData, float xOffset = 0, float yOffset = 0)
        {
            int numOptions = optionData.Count;
            float optionWidth = defaultDropdownOptionWidth;
            float optionHeight = defaultDropdownOptionHeight;
            GameObject dropdownObj = CreateUIObject(parent, objName, xOffset, yOffset);
            dropdownObj.AddComponent<CanvasRenderer>();
            Image dropdownImage = SetupImageComponent(dropdownObj, TextureHandler.standardBackgroundName, width: defaultDropdownOptionWidth, height: defaultDropdownOptionHeight);
            UIDropdown dropdownComp = dropdownObj.AddComponent<UIDropdown>();
            UIText dropdownLabel = CreateUIText(dropdownObj.transform, "Label", "  red", defaultDropdownFontSize, width: optionWidth, height: optionHeight, anchor: TextAnchor.MiddleLeft);
            dropdownComp.captionText = dropdownLabel.textComponent;
            GameObject template = CreateUIObject(dropdownObj.transform, "Template", 0, -(numOptions + 1) * optionHeight / 2, optionWidth, optionHeight);
            template.SetActive(false);
            template.AddComponent<CanvasRenderer>();
            Image imgComponent = template.AddComponent<Image>();
            TextureHandler.SetSprite("DropdownFullPanel", imgComponent);
            dropdownComp.template = template.GetComponent<RectTransform>();
            GameObject toggle = new GameObject("Toggle");
            RectTransform toggleTransform = toggle.AddComponent<RectTransform>();
            toggle.transform.SetParent(template.transform);
            toggleTransform.anchoredPosition = Vector2.zero;
            toggleTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, optionHeight);
            toggle.AddComponent<Toggle>();
            UIText optionText = CreateUIText(toggle.transform, "Item Text", "Placeholder Option", defaultDropdownFontSize, width: optionWidth, height: optionHeight, anchor: TextAnchor.MiddleLeft);
            dropdownComp.itemText = optionText.textComponent;
            dropdownComp.SetOptions(optionData);
            dropdownComp.onValueChanged.AddListener(callbackOnValueChanged);
            return dropdownComp;
        }

        internal static GameObject CreateUIImage(Transform parent, string objName, string imgName, float xOffset = 0, float yOffset = 0, float width = -1, float height = -1, UIAnchor anchor = UIAnchor.MiddleCenter)
        {
            GameObject newObj = CreateUIObject(parent, objName, xOffset, yOffset, anchor: anchor);
            SetupImageComponent(newObj, imgName, width, height);
            return newObj;
        }

        internal static void RevealCardPressed()
        {
            if (uiCanvas.revealCardButton != null && uiCanvas.revealCardButton.gameObject.activeInHierarchy)
            {
                uiCanvas.revealCardButton.LeftClick();
            }
        }

        internal static void CycleOpacity()
        {
            if (!IsTyping())
            {
                uiCanvas.CycleOpacity();
            }
        }

        internal static void MarkGoal(int slotIndex, bool remove)
        {
            NetworkHandler.MarkGoal(slotIndex, remove, GoalColors.MyColorName);
        }

        internal static void MarkIfUnmarkedGoal(int slotIndex)
        {
            if (uiCanvas.revealCardButton == null)
            {
                return;
            }
            if (!uiCanvas.revealCardButton.gameObject.activeSelf && !uiCanvas.HasColor(slotIndex, GoalColors.myColorID))
            {
                UIHelper.MarkGoal(slotIndex, false);
            }
        }

        internal static void UpdateUIScale()
        {
            uiCanvas.UpdateUIScale();
        }

        static Image SetupImageComponent(GameObject objToAttachTo, string imageName = "", float width = -1, float height = -1)
        {
            RectTransform rectTransform = objToAttachTo.GetComponent<RectTransform>();
            Image imageComponent = objToAttachTo.AddComponent<Image>();
            TextureHandler.SetSprite(imageName, imageComponent);
            float widthToSet = imageComponent.sprite.rect.width;
            float heightToSet = imageComponent.sprite.rect.height;
            if (width >= 0)
            {
                widthToSet = width;
            }
            if (height >= 0)
            {
                heightToSet = height;
            }
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthToSet);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightToSet);
            if (TextureHandler.ShouldSliceSprite(imageName))
            {
                imageComponent.type = Image.Type.Sliced;
                imageComponent.fillCenter = true;
            }
            return imageComponent;
        }

        static UIGoalColors CreateUIGoalColorsObject(Transform parent, string objName)
        {
            GameObject newObj = CreateUIObject(parent, objName);
            newObj.AddComponent<CanvasRenderer>();
            Image imageComponent = newObj.AddComponent<Image>();
            Sprite colorsSprite = TextureHandler.CreateGoalColorsSprite();
            imageComponent.sprite = colorsSprite;
            RectTransform rectTransform = newObj.GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, colorsSprite.rect.width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, colorsSprite.rect.height);
            return newObj.AddComponent<UIGoalColors>();
        }

        internal static bool IsTyping()
        {
            if (uiCanvas.roomUrlInputField != null && uiCanvas.nicknameInputField != null && uiCanvas.passwordInputField != null)
            {
                return uiCanvas.roomUrlInputField.IsFocused() || uiCanvas.nicknameInputField.IsFocused() || uiCanvas.passwordInputField.IsFocused();
            }
            return true;
        }

        internal static void TriggerErrorText(UIText? errorText, string textToDisplay)
        {
            errorText?.SetText(textToDisplay);
            errorText?.gameObject.SetActive(true);
            Coroutiner.CreateCoroutine(DisableErrorText(errorText?.gameObject));
        }
    }
}
