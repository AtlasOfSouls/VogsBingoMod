/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using UnityEngine;

namespace VogsBingoMod
{
    internal class AudioHelper : MonoBehaviour
    {
        // Could pull these values from the header but I don't have multiple audio types to the point where I need to worry about it
        const int wavHeaderSize = 46;
        const int wavSampleRate = 44100;
        const int wavLengthToPlayInSeconds = 1;
        const int wavChannels = 2;

        AudioClip? _teamMarkClip;
        AudioClip? TeamMarkClip => _teamMarkClip ?? (_teamMarkClip = GetAudioClip("TeamMark.wav"));
        AudioClip? _opponentMarkClip;
        AudioClip? OpponentMarkClip => _opponentMarkClip ?? (_opponentMarkClip = GetAudioClip("OpponentMark.wav"));
        AudioSource audioSource;
        static AudioHelper? _instance;
        internal static AudioHelper Instance => _instance ?? new();


        internal AudioHelper()
        {
            GameObject audioHelperObj = new GameObject();
            GameObject.DontDestroyOnLoad(audioHelperObj);
            audioSource = audioHelperObj.AddComponent<AudioSource>();
            _instance = this;
        }
        
        internal void PlayTeamMarkSound()
        {
            if (TeamMarkClip != null)
            {
                if (VogsBingoModPlugin.instance.teamMarkSoundsVolume.Value != AudioVolume.Off && (!audioSource.isPlaying || audioSource.clip != TeamMarkClip)){
                    audioSource.clip = TeamMarkClip;
                    audioSource.volume = (int)(VogsBingoModPlugin.instance.teamMarkSoundsVolume.Value) / 10f;
                    audioSource.Play();
                }
            } else
            {
                VogsBingoModPlugin.LogError("Could not find the audio clip \"TeamMark.wav\".");
            }
        }

        internal void PlayOpponentMarkSound()
        {
            if (OpponentMarkClip != null)
            {
                if (VogsBingoModPlugin.instance.opponentMarkSoundsVolume.Value != AudioVolume.Off && (!audioSource.isPlaying || audioSource.clip != OpponentMarkClip)){
                    audioSource.clip = OpponentMarkClip;
                    audioSource.volume = (int)(VogsBingoModPlugin.instance.opponentMarkSoundsVolume.Value) / 10f;
                    audioSource.Play();
                }
            } else
            {
                VogsBingoModPlugin.LogError("Could not find the audio clip \"OpponentMark.wav\".");
            }
        }

        static AudioClip? GetAudioClip(string fileName)
        {
            VogsBingoModPlugin.LogInfo($"getting audio clip {fileName}");
            byte[]? bytes = Resources.GetResourceAsByteArray($"VogsBingoMod.Assets.{fileName}");
            if (bytes == null)
            {
                return null;
            }
            float[] floats = new float[(bytes.Length - wavHeaderSize)/sizeof(float)];
            int floatIndex = 0;
            for (int i = wavHeaderSize; i < bytes.Length; i += sizeof(float))
            {
                byte[] temp = {
                    bytes[i],
                    bytes[i+1],
                    bytes[i+2],
                    bytes[i+3]
                };
                floats[floatIndex] = BitConverter.ToSingle(temp);
                floatIndex++;
            }
            AudioClip clip = AudioClip.Create($"{fileName}AudioClip", wavSampleRate * wavLengthToPlayInSeconds, wavChannels, wavSampleRate, false);
            clip.SetData(floats, 0);
            return clip;
        }
    }

    internal enum AudioVolume
    {
        Off = 0,
        Low = 3,
        Medium = 5,
        High = 7,
        Max = 10
    }
}
