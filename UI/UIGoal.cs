/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using UnityEngine;
using UnityEngine.UI;

namespace VogsBingoMod.UI
{
    internal class UIGoal : MonoBehaviour
    {
        internal UIGoalColors goalColors;
        internal int slotIndex = -1;
        UIText? goalText;
        bool _isHighlighted = false;
        internal bool isHighlighted {get => _isHighlighted; set{if (value != _isHighlighted){_isHighlighted = value; this.UpdateHighlightStatus();}}}
        Image imageComponent => this.gameObject.GetComponent<Image>();

        internal void SetGoalName(string goalName)
        {
            if (goalText == null)
            {
                goalText = this.gameObject.GetComponentInChildren<UIText>(true);
            }
            goalText.SetText(goalName);
        }

        internal void MarkGoal(int colorID)
        {
            goalColors.AddColor(colorID);
        }

        internal void UnmarkGoal(int colorID)
        {
            goalColors.RemoveColor(colorID);
        }

        internal void SetColors(int[] colorIDs)
        {
            goalColors.SetColors(colorIDs);
        }

        internal void ResetColors()
        {
            goalColors.ResetColors();
        }

        internal bool HasColor(int colorID)
        {
            return goalColors.HasColor(GoalColors.myColorID);
        }

        internal void OnLeftClick()
        {
            UIHelper.MarkGoal(slotIndex, HasColor(GoalColors.myColorID));
        }

        internal void OnRightClick()
        {
            isHighlighted = !isHighlighted;
        }

        void UpdateHighlightStatus()
        {
            if (isHighlighted)
            {
                TextureHandler.SetSprite(TextureHandler.standardBackgroundHighlightName, this.imageComponent);
            } else
            {
                TextureHandler.SetSprite(TextureHandler.standardBackgroundName, this.imageComponent);
            }
        }

        internal void SetPosition(int xPos, int yPos)
        {
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.right * xPos + Vector2.up * yPos;
        }

        internal void SetOpacity(float opacity)
        {
            Image image = this.gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            goalColors.SetOpacity(opacity);
        }
    }
}
