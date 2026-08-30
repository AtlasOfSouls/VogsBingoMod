/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using UnityEngine;

namespace SilksongBingoMod.UI
{
    internal enum GoalColorID
    {
        none,
        pink,
        red,
        orange,
        brown,
        yellow,
        green,
        teal,
        blue,
        navy,
        purple,
        beastling,
        hornet,
        vespa,
        scrounge,
        sherma,
        huntress,
        gilly,  //The goat
        magnetite,
        plasmium,
        architect
    }

    internal static class GoalColors
    {
        internal const int BingosyncColorsFirstID = 1;
        internal const int CaravanColorsFirstID = 11;
        internal static int myColorID = (int)GoalColorID.red;
        internal static string MyColorName => IDToName(myColorID);
        internal static string[] ColorOptions => new string[]
        {
            "none",
            "pink",
            "red",
            "orange",
            "brown",
            "yellow",
            "green",
            "teal",
            "blue",
            "navy",
            "purple",
            "beastling",
            "hornet",
            "vespa",
            "scrounge",
            "sherma",
            "huntress",
            "gilly",
            "magnetite",
            "plasmium",
            "architect"
        };

        internal static Color[] colorsList =
        {
            new Color(0, 0, 0, 0),  //None
            new Color(1, 0.7f, 1, 0.7f),  //Pink
            new Color(1, 0, 0, 0.7f),  //Red
            new Color(1, 0.4f, 0, 0.7f),  //Orange
            new Color(0.5f, 0.3f, 0, 0.7f),  //Brown
            new Color(1, 1, 0, 0.7f),  //Yellow
            new Color(0, 1, 0, 0.7f),  //Green
            new Color(0.2f, 0.5f, 0.5f, 0.7f),  //Teal
            new Color(0, 0.8f, 1, 0.7f),  //Blue
            new Color(0, 0f, 1f, 0.7f),  //Navy
            new Color(0.7f, 0.3f, 1, 0.7f),  //Purple
            new Color(1f, 0.5f, 0.6f, 0.7f),  //Beastling
            new Color(0.7f, 0.1f, 0.2f, 0.7f),  //Hornet
            new Color(0.7f, 0.3f, 0, 0.7f),  //Vespa
            new Color(0.9f, 0.7f, 0.4f, 0.7f),  //Scrounge
            new Color(0.9f, 0.8f, 0.1f, 0.7f),  //Sherma
            new Color(0.5f, 0.8f, 0.5f, 0.7f),  //Huntress
            new Color(0.1f, 0.5f, 0.2f, 0.7f),  //Gilly
            new Color(0.5f, 0.7f, 0.9f, 0.7f),  //Magnetite
            new Color(0f, 0.3f, 0.7f, 0.7f),  //Plasmium
            new Color(0.3f, 0.1f, 0.4f, 0.7f),  //Architect
            
        };

        internal static int NameToID(string colorName) => colorName.ToLower()
        switch
        {
            "pink" => 1,
            "red" => 2,
            "orange" => 3,
            "brown" => 4,
            "yellow" => 5,
            "green" => 6,
            "teal" => 7,
            "blue" => 8,
            "navy" => 9,
            "purple" => 10,
            "beastling" => 11,
            "hornet" => 12,
            "vespa" => 13,
            "scrounge" => 14,
            "sherma" => 15,
            "huntress" => 16,
            "gilly" => 17,
            "magnetite" => 18,
            "plasmium" => 19,
            "architect" => 20,
            _ => 0
        };

        internal static string IDToName(int colorID) => ColorOptions[colorID];

        internal static void SetMyColorToDefault(RoomType roomType)
        {
            if (roomType == RoomType.Bingosync)
            {
                myColorID = (int)GoalColorID.red;
            } else
            {
                myColorID = (int)GoalColorID.hornet;
            }
        }
    }
}
