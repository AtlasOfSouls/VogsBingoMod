/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VogsBingoMod.UI
{
    internal class UIGoalColors : MonoBehaviour
    {
        Texture2D? texture;
        Texture2D Texture => texture != null ? texture : texture = this.gameObject.GetComponent<Image>().sprite.texture;
        List<int> currentColorIDs = new List<int>();

        internal void AddColor(int colorID)
        {
            if (currentColorIDs.BinarySearch(colorID) < 0){
                currentColorIDs.Add(colorID);
                currentColorIDs.Sort();
                RefreshColors();
            }
        }

        internal void RemoveColor(int colorID)
        {
            currentColorIDs.Remove(colorID);
            RefreshColors();
        }

        internal void SetColors(int[] colorIDs)
        {
            currentColorIDs.Clear();
            foreach (int colorID in colorIDs)
            {
                currentColorIDs.Add(colorID);
                currentColorIDs.Sort();
            }
            RefreshColors();
        }

        internal bool HasColor(int colorID)
        {
            return currentColorIDs.Contains(colorID);
        }

        internal void ResetColors()
        {
            currentColorIDs.Clear();
            TextureHandler.ClearTexture(Texture);
        }

        internal void SetOpacity(float opacity)
        {
            Image image = this.gameObject.GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
        }

        void RefreshColors()
        {
            if (currentColorIDs.Count <= 0)
            {
                ResetColors();
                return;
            }
            int colorSegmentSize = (Texture.width + Texture.height) / currentColorIDs.Count;
            for (int i = 0; i < Texture.width; i++)
            {
                for (int j = 0; j < Texture.height; j++)
                {
                    int colorIndex = ((i + Texture.height - 1 - j) / colorSegmentSize);
                    if (colorIndex >= currentColorIDs.Count)
                    {
                        colorIndex = currentColorIDs.Count-1;
                    }
                    Texture.SetPixel(i, j, GoalColors.colorsList[currentColorIDs[colorIndex]]);
                }
            }
            Texture.Apply();
        }
    }
}
