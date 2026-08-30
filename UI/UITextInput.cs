/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using UnityEngine;
using UnityEngine.UI;

namespace SilksongBingoMod.UI
{
    internal class UITextInput : MonoBehaviour
    {
        internal GameObject placeholderObj;
        InputField? inputComponent;
        InputField InputComponent => inputComponent != null ? inputComponent : inputComponent = this.gameObject.GetComponent<InputField>();

        internal string GetText()
        {
            return InputComponent.text;
        }

        internal void UpdatePlaceholder(string currentText)
        {
            if (currentText.Equals(""))
            {
                placeholderObj.SetActive(true);
            } else
            {
                placeholderObj.SetActive(false);
            }
        }

        internal bool IsFocused()
        {
            return InputComponent.isFocused;
        }
    }
}
