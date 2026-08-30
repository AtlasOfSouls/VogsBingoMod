/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace VogsBingoMod.UI
{
    internal class UIButton : MonoBehaviour, IPointerClickHandler
    {
        internal Action? leftClickCallback {get; private set;}
        internal Action? rightClickCallback {get; private set;}

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                this.LeftClick();
            } else if (eventData.button == PointerEventData.InputButton.Right)
            {
                this.RightClick();
            }
        }

        internal void SetupCallbacks(Action? leftClickAction, Action? rightClickAction)
        {
            leftClickCallback = leftClickAction;
            rightClickCallback = rightClickAction;
        }

        internal void LeftClick()
        {
            if (leftClickCallback != null)
            {
                leftClickCallback.Invoke();
            }
        }

        internal void RightClick()
        {
            if (rightClickCallback != null)
            {
                rightClickCallback.Invoke();
            }
        }
    }
}
