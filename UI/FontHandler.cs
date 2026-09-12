/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using UnityEngine;

namespace VogsBingoMod.UI
{
    internal static class FontHandler
    {
        internal static Font? GetFont(string fontName)
        {
            return UnityEngine.Resources.GetBuiltinResource<Font>($"{fontName}.ttf");
        }
    }
}
