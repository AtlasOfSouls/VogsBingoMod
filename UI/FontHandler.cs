/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using UnityEngine;

namespace SilksongBingoMod.UI
{
    internal static class FontHandler
    {
        static Dictionary<string, Font> fonts = new Dictionary<string, Font>();
        internal static Font GetFont(string fontName)
        {
            if (!fonts.ContainsKey(fontName))
            {
                Font[] foundFonts = Resources.FindObjectsOfTypeAll<Font>();
                for (int i = 0; i < foundFonts.Length; i++)
                {
                    if (foundFonts[i].name.Equals(fontName))
                    {
                        fonts.Add(fontName, foundFonts[i]);
                        break;
                    }
                }
            }
            return fonts[fontName];
        }
    }
}
