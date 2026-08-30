/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections;
using UnityEngine;

namespace SilksongBingoMod
{
    internal class Coroutiner : MonoBehaviour
    {
        static GameObject? coroutineObj;
        static Coroutiner? coroutineComponent;

        internal static void CreateCoroutine(IEnumerator enumerator)
        {
            if (coroutineComponent != null){
                coroutineComponent.StartCoroutine(enumerator);
            } else
            {
                SilksongBingoModPlugin.LogError("Failed to start coroutine.");
            }
        }

        internal static void Initialize()
        {
            coroutineObj = new GameObject("BingoModCoroutineObject");
            DontDestroyOnLoad(coroutineObj);
            coroutineComponent = coroutineObj.AddComponent<Coroutiner>();
        }
    }
}
