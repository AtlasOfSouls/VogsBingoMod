/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using UnityEngine;
using UnityEngine.UI;

namespace SilksongBingoMod.UI
{
    internal class UIText : MonoBehaviour
    {
        internal Text textComponent;
        
        internal void SetText(string text)
        {
            if (textComponent == null)
            {
                textComponent = this.gameObject.GetComponentInChildren<Text>(true);
            }
            textComponent.text = text;
        }
    }
}
