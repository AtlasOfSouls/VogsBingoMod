/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;

namespace SilksongBingoMod.UI
{
    internal static class TextureHandler
    {
        const int goalPngWidth = 100;
        const int goalPngHeight = goalPngWidth;
        internal const string standardBackgroundName = "StandardBackground";
        internal const string standardBackgroundHighlightName = "StandardBackgroundHighlight";
        static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        static Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        internal static Texture2D GetTexture(string imgName)
        {
            imgName = $"SilksongBingoMod.Assets.{imgName}.png";
            if (textures.Count <= 0)
            {
                LoadTextures();
            }

            if (!textures.ContainsKey(imgName))
            {
                SilksongBingoModPlugin.LogError($"could not find sprite \"{imgName}\"");
            }
            return textures[imgName];
        }

        internal static void SetSprite(string spriteName, Image imgComponent)
        {
            imgComponent.sprite = GetSprite(spriteName);
        }

        internal static Sprite CreateGoalColorsSprite()
        {
            Texture2D texture = new Texture2D(goalPngWidth - 5, goalPngHeight - 5);
            ClearTexture(texture);
            return Sprite.Create(texture, new Rect(0, 0, goalPngWidth - 5, goalPngHeight - 5), Vector2.one * 0.5f);
        }

        internal static void ClearTexture(Texture2D texture)
        {
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    texture.SetPixel(i, j, GoalColors.colorsList[(int)GoalColorID.none]);
                }
            }
            texture.Apply();
        }

        internal static bool ShouldSliceSprite(string imgName)
        {
            return imgName.Equals(standardBackgroundName) || imgName.Equals(standardBackgroundHighlightName);
        }

        static byte[] LoadEmbeddedImage(string imgName)
        {
            Assembly executeAssembly = Assembly.GetExecutingAssembly();
            Stream stream = executeAssembly.GetManifestResourceStream($"{imgName}");
            if (stream == null)
            {
                SilksongBingoModPlugin.LogError($"Could not find resource: {imgName}");
                return new byte[0];
            }
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            stream.Dispose();
            memoryStream.Dispose();
            return bytes;
        }

        internal static void LoadTextures()
        {
            foreach(string name in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (name.StartsWith("SilksongBingoMod.Assets."))
                {
                    Texture2D texture = new Texture2D(1,1);
                    byte[] buf;
                    buf = LoadEmbeddedImage(name);
                    texture.LoadImage(buf);
                    textures.Add(name, texture);
                }
            }
        }

        static Sprite GetSprite(string spriteName)
        {
            if (!sprites.ContainsKey(spriteName))
            {
                Texture2D texture = TextureHandler.GetTexture(spriteName);
                Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f, 100, 0, SpriteMeshType.FullRect, Vector4.one * 5);
                sprites.Add(spriteName, newSprite);
            }
            return sprites[spriteName];
        }
    }
}
