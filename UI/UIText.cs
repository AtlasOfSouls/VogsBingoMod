/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using UnityEngine;
using UnityEngine.UI;

namespace VogsBingoMod.UI
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

        void Update()
        {
            if (textComponent != null && textComponent.font == null)
            {
                textComponent.font = FontHandler.GetFont("ARIAL");
            }
        }
    }
}
