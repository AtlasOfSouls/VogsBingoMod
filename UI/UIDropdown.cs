/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using UnityEngine.UI;

namespace SilksongBingoMod.UI
{
    internal class UIDropdown : Dropdown
    {
        const int defaultValue = 1;
        internal void SetOptions(List<Dropdown.OptionData> options)
        {
            this.ClearOptions();
            this.AddOptions(options);
            this.SetValueWithoutNotify(defaultValue);
        }

        internal int IsOpen()
        {
            return (int)this.currentSelectionState;
        }
    }
}