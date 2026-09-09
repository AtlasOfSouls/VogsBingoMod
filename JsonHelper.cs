/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using VogsBingoMod.UI;

namespace VogsBingoMod
{
    internal static class JsonHelper
    {
        internal static string[] GetNamesFromBoard(string boardJson)
        {
            JsonGoal[]? goals = JsonConvert.DeserializeObject<JsonGoal[]>(boardJson);
            if (goals == null)
            {
                return [];
            }
            string[] goalNames = new string[goals.Length];
            for (int i = 0; i < goals.Length; i++)
            {
                int slotIndex = int.Parse(goals[i].slot.Substring(4)) - 1;
                goalNames[slotIndex] = goals[i].name;
            }
            return goalNames;
        }

        internal static int[][] GetColorsFromBoard(string boardJson)
        {
            JsonGoal[]? goals = JsonConvert.DeserializeObject<JsonGoal[]>(boardJson);
            if (goals == null)
            {
                return [];
            }
            int[][] colorIDs = new int[goals.Length][];
            for (int i = 0; i < goals.Length; i++)
            {
                string[] currentColors = goals[i].colors.Split(" ");
                if (currentColors.Length == 1 && currentColors[0].Equals("blank"))
                {
                    colorIDs[i] = [];
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

        internal static string CreateGoalMarkJson(int slotIndex, string roomCode, string color, bool remove, RoomType roomType)
        {
            return $"{{\"slot\": {slotIndex}, \"color\": \"{color}\", \"remove_color\": {remove.ToString().ToLower()}, \"room\": \"{roomCode}\"}}";
        }

        internal static string CreateColorSwitchJson(int colorID, string roomCode, RoomType roomType)
        {
            return $"{{\"room\": \"{roomCode}\", \"color\": \"{GoalColors.IDToName(colorID)}\"}}";
        }

        internal static string CreateRevealCardJson(string roomCode)
        {
            return $"{{\"room\": \"{roomCode}\"}}";
        }

        internal static string[] GetGoalNamesFromJson(string json)
        {
            string[] goalNames = GetSupportedGoals(json).Keys.ToArray();
            for (int i = 0; i < goalNames.Length; i++)
            {
                goalNames[i] = goalNames[i].ToLower();
            }
            return goalNames;
        }

        internal static Dictionary<string, bool> GetSupportedGoals(string json)
        {
            Dictionary<string, bool> goals = new Dictionary<string, bool>();
            JsonSupportedGoal[]? jsonSupportedGoals = JsonConvert.DeserializeObject<JsonSupportedGoal[]>(json);
            for (int i = 0; i < jsonSupportedGoals?.Length; i++)
            {
                string name = jsonSupportedGoals[i].name.ToLower();
                goals.Add(name, jsonSupportedGoals[i].automarking_support);
            }
            return goals;
        }
    }

    internal class JsonGoalMarkedMessage
    {
        public string type = "";
        public JsonPlayer player = new();
        public JsonSquare square = new();
        public string player_color = "";
        public string color = "";
        public bool remove = default;
        public double timestamp = default;
        public string room = "";
    }

    internal class JsonPlayer
    {
        public string uuid = "";
        public string name = "";
        public string color = "";
        public bool is_spectator = default;
    }

    internal class JsonSquare
    {
        public string name = "";
        public string slot = "";
        public string colors = "";
    }

    internal class JsonSupportedGoal
    {
        public string name = "";
        public bool automarking_support = default;
    }

    internal class JsonMessageType
    {
        public string type = "";
    }

    internal class JsonGoal
    {
        public string name = "";
        public string slot = "";
        public string colors = "";
    }
}