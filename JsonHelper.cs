/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections.Generic;
using SilksongBingoMod.UI;

namespace SilksongBingoMod
{
    internal static class JsonHelper
    {
        internal static string[] GetNamesFromBoard(string boardJson)
        {
            string[] jsonObjects = boardJson.Split("}, {");
            string[] goalNames = new string[jsonObjects.Length];
            for (int i = 0; i < jsonObjects.Length; i++)
            {
                int slotIndex = int.Parse(GetStringValueOfKey("slot", jsonObjects[i]).Substring(4)) - 1;
                goalNames[slotIndex] = GetStringValueOfKey("name", jsonObjects[i]);
            }
            return goalNames;
        }

        internal static int[][] GetColorsFromBoard(string boardJson)
        {
            string[] jsonObjects = boardJson.Split("}, {");
            int[][] colorIDs = new int[jsonObjects.Length][];
            for (int i = 0; i < jsonObjects.Length; i++)
            {
                string[] currentColors = GetStringValueOfKey("colors", jsonObjects[i]).Split(" ");
                if (currentColors.Length == 1 && currentColors[0].Equals("blank"))
                {
                    colorIDs[i] = new int[0];
                } else {
                    colorIDs[i] = new int[currentColors.Length];
                }
                for (int j = 0; j < colorIDs[i].Length; j++)
                {
                    colorIDs[i][j] = GoalColors.NameToID(currentColors[j]);
                }
            }
            return colorIDs;
        }

        internal static string GetStringValueOfKey(string key, string jsonObj)
        {
            int keyIndex = jsonObj.IndexOf(key);
            int valueStartIndex = keyIndex + key.Length + 4;
            int valueEndIndex = jsonObj.IndexOf("\"", valueStartIndex);
            int valueLength = valueEndIndex - valueStartIndex;
            string value = jsonObj.Substring(valueStartIndex, valueLength);
            return value;
        }

        internal static bool GetBoolValueOfKey(string key, string jsonObj)
        {
            int keyIndex = jsonObj.IndexOf(key);
            int valueStartIndex = keyIndex + key.Length + 3;
            if (jsonObj.Substring(valueStartIndex, 4).Equals("true"))
            {
                return true;
            } else if (jsonObj.Substring(valueStartIndex, 5).Equals("false"))
            {
                return false;
            } else
            {
                throw new Exception($"The value of the key you requested ({key}) was not a boolean.");
            }
        }

        internal static string GetObjectValueOfKey(string key, string jsonObj)
        {
            int keyIndex = jsonObj.IndexOf(key);
            int valueStartIndex = keyIndex + key.Length + 4;
            int valueEndIndex = 0;
            int numLeftBraces = 1;
            int crtIndex = valueStartIndex;
            while (crtIndex < jsonObj.Length)
            {
                if (jsonObj[crtIndex] == '{')
                {
                    numLeftBraces++;
                } else if (jsonObj[crtIndex] == '}')
                {
                    numLeftBraces--;
                    if (numLeftBraces <= 0)
                    {
                        valueEndIndex = crtIndex;
                        break;
                    }
                }
                crtIndex++;
            }
            int valueLength = valueEndIndex - valueStartIndex;
            string value = jsonObj.Substring(valueStartIndex, valueLength);
            return value;
        }

        internal static double GetDoubleValueFromKey(string key, string jsonObj)
        {
            int keyIndex = jsonObj.IndexOf(key);
            int valueStartIndex = keyIndex + key.Length + 3;
            int valueEndIndex = valueStartIndex;
            for (int i = valueStartIndex; i < jsonObj.Length; i++)
            {
                if ((jsonObj[i] < '0' || jsonObj[i] > '9') && jsonObj[i] != '.')
                {
                    valueEndIndex = i;
                    break;
                }
            }
            int valueLength = valueEndIndex - valueStartIndex;
            double value = double.Parse(jsonObj.Substring(valueStartIndex, valueLength));
            return value;
        }

        internal static string CreateGoalMarkJson(int slotIndex, string roomCode, string color, bool remove, RoomType roomType)
        {
            if (roomType == RoomType.Bingosync)
            {
                return $"{{\"slot\": {slotIndex}, \"color\": \"{color}\", \"remove_color\": {remove.ToString().ToLower()}, \"room\": \"{roomCode}\"}}";
            } else
            {
                return $"{{\"slot\": {slotIndex}, \"color\": \"{color}\", \"remove_color\": {remove.ToString().ToLower()}, \"room\": \"{roomCode}\"}}";
            }
        }

        internal static string CreateColorSwitchJson(int colorID, string roomCode, RoomType roomType)
        {
            return $"{{\"room\": \"{roomCode}\", \"color\": \"{GoalColors.IDToName(colorID)}\"}}";
        }

        internal static string CreateRevealCardJson(string roomCode)
        {
            return $"{{\"room\": \"{roomCode}\"}}";
        }

        internal static string[] GetGoalNamesFromList(string json)
        {
            string[] goals = json.Split("},");
            for (int i = 0; i < goals.Length; i++)
            {
                goals[i] = GetStringValueOfKey("name", goals[i]).ToLower();
            }
            return goals;
        }

        internal static Dictionary<string, bool> GetSupportedGoals(string json)
        {
            Dictionary<string, bool> goals = new Dictionary<string, bool>();
            string[] jsonObjs = json.Split("},");
            for (int i = 0; i < jsonObjs.Length-1; i++)
            {
                string name = GetStringValueOfKey("name", jsonObjs[i]).ToLower();
                goals.Add(name, GetBoolValueOfKey("automarking_support", jsonObjs[i]));
            }
            return goals;
        }
    }
}