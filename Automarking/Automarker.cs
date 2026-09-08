/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VogsBingoMod.UI;
using UnityEngine;
using System;

namespace VogsBingoMod.Automarking
{
    internal class Automarker : MonoBehaviour
    {
        static float currentTimer = 1;
        static Dictionary<string, bool> supportedGoals = GetSupportedGoals();
        static Dictionary<int, int> currentGoals = new Dictionary<int, int>();

        internal static void SetGoalNames(string[] goalNames)
        {
            currentGoals.Clear();
            for (int i = 0; i < goalNames.Length; i++)
            {
                int key = GoalHelper.NameToID(goalNames[i].ToLower());
                if (currentGoals.ContainsKey(key))
                {
                    key = -i;
                }
                currentGoals.Add(key, i);
            }
        }

        internal static string[] AddSupportNotations(string[] goalNames)
        {
            for (int i = 0; i < goalNames.Length; i++)
            {
                string goalStr = goalNames[i].ToLower();
                if (supportedGoals.TryGetValue(goalStr, out bool supported))
                {
                    if (!supported)
                    {
                        goalNames[i] = $"{goalNames[i]} (U)";
                    }
                } else
                {
                    goalNames[i] = $"{goalNames[i]} (M)";
                }
            }
            return goalNames;
        }

        internal static bool BoardHasGoal(int goalID)
        {
            return currentGoals.ContainsKey(goalID);
        }

        internal static bool BoardHasGoal(GoalID goalID)
        {
            return BoardHasGoal((int)goalID);
        }

        internal static void MarkIfAvailable(int goalID)
        {
            if (goalID != (int)GoalID.MeetCaravanattheGrandGateTwelve && goalID != (int)GoalID.MeettheCaravaninGreymoor)
            {
                VogsBingoModPlugin.LogInfo($"Checking if goal {(GoalID)goalID} is on the board");
            }
            if (BoardHasGoal(goalID))
            {
                VogsBingoModPlugin.LogInfo("the goal is on the board, checking if it is unmarked so far");
                UIHelper.MarkIfUnmarkedGoal(currentGoals[goalID]);
            }
            return;
        }

        internal static void MarkIfAvailable(GoalID goalID)
        {
            MarkIfAvailable((int)goalID);
            return;
        }

        internal static string GetGoalsJson()
        {
            Assembly executeAssembly = Assembly.GetExecutingAssembly();
            Stream stream = executeAssembly.GetManifestResourceStream($"VogsBingoMod.Automarking.Goals.json");
            if (stream == null)
            {
                VogsBingoModPlugin.LogError($"Could not find the goals resource.");
                return "";
            }
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            stream.Dispose();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Dispose();
            return Encoding.UTF8.GetString(bytes);
        }

        static Dictionary<string, bool> GetSupportedGoals()
        {
            string json = GetGoalsJson();
            return JsonHelper.GetSupportedGoals(json);
        }

        internal static void UpdateSilkSkills()
        {
            int skillCount = 0;
            if (PlayerData.instance.hasNeedleThrow)
            {
                skillCount++;
            }
            if (PlayerData.instance.hasThreadSphere)
            {
                skillCount++;
                MarkIfAvailable(GoalID.ThreadStorm);
            }
            if (PlayerData.instance.hasSilkCharge)
            {
                skillCount++;
                MarkIfAvailable(GoalID.Sharpdart);
            }
            if (PlayerData.instance.hasParry)
            {
                skillCount++;
            }
            if (PlayerData.instance.hasSilkBomb)
            {
                skillCount++;
            }
            if (PlayerData.instance.hasSilkBossNeedle)
            {
                skillCount++;
                MarkIfAvailable(GoalID.PaleNails);
            }

            if (skillCount >= 3)
            {
                MarkIfAvailable(GoalID.ThreeSilkSkills);
                if (skillCount >= 4)
                {
                    MarkIfAvailable(GoalID.FourSilkSkills);
                }
            }
        }

        internal static void CheckIfGoalCompleted(GoalID goalID, string context = "")
        {
            bool goalCompleted = false;
            switch (goalID)
            {
                case GoalID.CompassPendantBrooch:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Compass", "Bone Necklace", "Rosary Magnet");
                    break;
                case GoalID.StraightThreefoldandLongPin:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Straight Pin", "Tri Pin", "Harpoon");
                    break;
                case GoalID.DeadBugsPurseaSilkeater:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Dead Mans Purse") && VogsBingoModPlugin.instance.SaveData.SilkeaterBool;
                    break;
                case GoalID.BarbedBraceletFracturedMask:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Fractured Mask", "Barbed Wire");
                    break;
                case GoalID.MagnetiteDiceMagnetiteBrooch:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Rosary Magnet", "Magnetite Dice");
                    break;
                case GoalID.WardingBellClawMirror:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Bell Bind", "Dazzle Bind");
                    break;
                case GoalID.WardingBellSawtoothCirclet:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Bell Bind", "Brolly Spike");
                    break;
                case GoalID.MagmaBellCurveclaw:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Lava Charm", "Curve Claws");
                    break;
                case GoalID.WeightedBeltAscendantsGrip:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Weighted Anklet", "Wallcling");
                    break;
                case GoalID.ScuttlebraceSilkspeed:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Scuttlebrace", "Sprintmaster");
                    break;
                case GoalID.PimpilloVoltvessels:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Lightning Rod", "Pimpilo");
                    break;
                case GoalID.PollipPouchStingShard:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Sting Shard", "Poison Pouch");
                    break;
                case GoalID.WeavelightInjectorBand:
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "White Ring", "Quickbind");
                    break;
                case GoalID.BlastedStepsSilkeaterCraftmetal:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.BlastedSilkeater && VogsBingoModPlugin.instance.SaveData.BlastedCraftmetal;
                    break;
                case GoalID.BothFreeSimpleKeys:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.SinnersKey && VogsBingoModPlugin.instance.SaveData.KarakKey;
                    break;
                case GoalID.ShellwoodVaultsMaskShards:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.ShellwoodMaskShard && VogsBingoModPlugin.instance.SaveData.VaultsMaskShard;
                    break;
                case GoalID.CogworkClapperJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.CogworkClapperKilled;
                    break;
                case GoalID.ImobaJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.ImobaKilled;
                    break;
                case GoalID.SquirrmJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.SquirrmKilled;
                    break;
                case GoalID.GromlingJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.GromlingKilled;
                    break;
                case GoalID.CraggliteJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.CraggliteKilled;
                    break;
                case GoalID.DeepDiverJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.DeepDiverKilled;
                    break;
                case GoalID.SkullwingJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.SkullwingKilled;
                    break;
                case GoalID.MiteMotherJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.MiteMotherKilled;
                    break;
                case GoalID.UnravelledJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.UnravelledKilled;
                    break;
                case GoalID.ShadowChargerJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.ShadowChargerKilled;
                    break;
                case GoalID.MassiveMossgrubJournalEntry:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HasJournal && VogsBingoModPlugin.instance.SaveData.MassiveMossgrubKilled;
                    break;
                case GoalID.TalktoStyxHuntress:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.HuntressSpokenTo && VogsBingoModPlugin.instance.SaveData.StyxSpokenTo;
                    break;
                case GoalID.TalktoForgeDaughterandTwelfthArchitect:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.TwelfthArchitectSpokenTo && VogsBingoModPlugin.instance.SaveData.ForgeDaughterSpokenTo;
                    break;
                case GoalID.BreakBothofStyxsOneways:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.SinnersStyxWallBroken && VogsBingoModPlugin.instance.SaveData.GreymoorStyxWallBroken;
                    break;
                case GoalID.BothVendorSimpleKeys:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.JubilanaKeyBought && VogsBingoModPlugin.instance.SaveData.PebbKeyBought;
                    break;
                case GoalID.GreymoorFleasTwoKratt:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaCrawLake && VogsBingoModPlugin.instance.SaveData.FleaGreymoorTower && VogsBingoModPlugin.instance.SaveData.FleaKratt;
                    break;
                case GoalID.FarFieldsFleasTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaFarFieldsCage && VogsBingoModPlugin.instance.SaveData.FleaPilgrimsRest;
                    break;
                case GoalID.ShellwoodBellhartFleasTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaShellwood && VogsBingoModPlugin.instance.SaveData.FleaBellvein;
                    break;
                case GoalID.DeepDocksFleasThree:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaSwiftStep && VogsBingoModPlugin.instance.SaveData.FleaDeeperDocks && VogsBingoModPlugin.instance.SaveData.FleaDeepDocksBellway;
                    break;
                case GoalID.WormwaysBlastedStepsFleasTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaWormways && VogsBingoModPlugin.instance.SaveData.FleaBlastedSteps;
                    break;
                case GoalID.UnderworksFleasTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaUnderworksCauldron && VogsBingoModPlugin.instance.SaveData.FleaUnderworksWispThicket;
                    break;
                case GoalID.LowerBilewaterHuntersMarchFleasTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaBilewaterThieves && VogsBingoModPlugin.instance.SaveData.FleaHuntersMarch;
                    break;
                case GoalID.SinnersRoadVaultsFleasTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.FleaSinnersRoad && VogsBingoModPlugin.instance.SaveData.FleaVaults;
                    break;
                case GoalID.DeepDocksSpoolFragmentsTwo:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.DeepDocksSpoolFragNearSpa && VogsBingoModPlugin.instance.SaveData.DeeperDocksSpoolFrag;
                    break;
                case GoalID.HaveSixRosaryStringsnopurchasing:
                    goalCompleted = (VogsBingoModPlugin.instance.SaveData.automarkRosaryStringHandler.GetNonPurchasedStringsCurrentlyHeld() + VogsBingoModPlugin.instance.SaveData.automarkRosaryStringHandler.GetFrayedStringsCurrentlyHeld()) >= 6;
                    break;
                case GoalID.BreakEightRosaryStringsnopurchasing:
                    goalCompleted = (VogsBingoModPlugin.instance.SaveData.automarkRosaryStringHandler.GetNonPurchasedStringsBroken() + VogsBingoModPlugin.instance.SaveData.automarkRosaryStringHandler.GetFrayedStringsBroken()) >= 8;
                    break;
                default:
                    VogsBingoModPlugin.LogError($"There isn't a check for goalID {goalID}");
                    break;
            }
            if (goalCompleted)
            {
                MarkIfAvailable(goalID);
            }
        }

        internal static void CheckIfGoalsCompleted(GoalID[] goalIDs)
        {
            foreach (GoalID goalID in goalIDs)
            {
                CheckIfGoalCompleted(goalID);
            }
        }

        internal static void CheckIfGoalsCompleted(AutomarkIntValue[] values, int currentValue)
        {
            foreach (AutomarkIntValue markValue in values)
            {
                CheckIfGoalCompleted(markValue, currentValue);
            }
        }

        internal static void CheckIfGoalCompleted(AutomarkIntValue markValue, int currentValue)
        {
            VogsBingoModPlugin.LogInfo($"Checking if goal {markValue.goalToMark} is finished");
            if (currentValue >= markValue.markValue)
            {
                VogsBingoModPlugin.LogInfo($"Goal {markValue.goalToMark} is finished, checking if the goal is on the board...");
                MarkIfAvailable(markValue.goalToMark);
            }
        }

        internal static void RunFrameChecks()
        {
            currentTimer -= Time.unscaledDeltaTime;
            if (currentTimer < 0)
            {
                currentTimer = 1;
                if (PlayerData.instance != null)
                {
                    if (PlayerData.instance.CaravanTroupeLocation == GlobalEnums.CaravanTroupeLocations.CoralJudge)
                    {
                        MarkIfAvailable(GoalID.MeetCaravanattheGrandGateTwelve);
                    } else if (PlayerData.instance.CaravanTroupeLocation == GlobalEnums.CaravanTroupeLocations.Greymoor)
                    {
                        MarkIfAvailable(GoalID.MeettheCaravaninGreymoor);
                    }
                    if(PlayerData.instance.HasMelodyArchitect)
                        MarkIfAvailable(GoalID.ArchitectsMelody);
                    if(PlayerData.instance.HasMelodyConductor)
                        MarkIfAvailable(GoalID.ConductorsMelody);
                    if (PlayerData.instance.HasMelodyLibrarian)
                        MarkIfAvailable(GoalID.VaultkeepersMelody);
                    if(PlayerData.instance.act3_wokeUp)
                        MarkIfAvailable(GoalID.EnterActThree);
                    if (PlayerData.instance.mapBoolList != null && VogsBingoModPlugin.instance.SaveData.MapCount.Value != PlayerData.instance.mapBoolList.HasCount)
                    {
                        VogsBingoModPlugin.instance.SaveData.MapCount.Value = PlayerData.instance.mapBoolList.HasCount;
                    }
                }
            }
        }

        internal static void UpdateFleas()
        {
            if (PlayerData.instance != null)
            {
                int fleaCount =  PlayerData.instance.SavedFleasCount;
                VogsBingoModPlugin.instance.SaveData.CitadelFleas.Value = 0;
                if (PlayerData.instance.SavedFlea_Song_11)
                    VogsBingoModPlugin.instance.SaveData.CitadelFleas.Value++;
                if (PlayerData.instance.SavedFlea_Song_14)
                    VogsBingoModPlugin.instance.SaveData.CitadelFleas.Value++;
                if (PlayerData.instance.SavedFlea_Library_09)
                    VogsBingoModPlugin.instance.SaveData.CitadelFleas.Value++;
                if (PlayerData.instance.tamedGiantFlea)
                {
                    VogsBingoModPlugin.instance.SaveData.CitadelFleas.Value++;
                    MarkIfAvailable(GoalID.BeatBigFlea);
                    fleaCount++;
                }
                if (PlayerData.instance.SavedFlea_Library_01)
                {
                    VogsBingoModPlugin.instance.SaveData.CitadelFleas.Value++;
                    VogsBingoModPlugin.instance.SaveData.FleaVaults.Value = true;
                }
                VogsBingoModPlugin.instance.SaveData.FleaBellvein.Value = PlayerData.instance.SavedFlea_Belltown_04;
                VogsBingoModPlugin.instance.SaveData.FleaBilewaterThieves.Value = PlayerData.instance.SavedFlea_Shadow_28;
                VogsBingoModPlugin.instance.SaveData.FleaBlastedSteps.Value = PlayerData.instance.SavedFlea_Coral_35;
                VogsBingoModPlugin.instance.SaveData.FleaCrawLake.Value = PlayerData.instance.SavedFlea_Greymoor_15b;
                VogsBingoModPlugin.instance.SaveData.FleaDeepDocksBellway.Value = PlayerData.instance.SavedFlea_Dock_16;
                VogsBingoModPlugin.instance.SaveData.FleaDeeperDocks.Value = PlayerData.instance.SavedFlea_Dock_03d;
                VogsBingoModPlugin.instance.SaveData.FleaFarFieldsCage.Value = PlayerData.instance.SavedFlea_Bone_East_17b;
                VogsBingoModPlugin.instance.SaveData.FleaGreymoorTower.Value = PlayerData.instance.SavedFlea_Greymoor_06;
                VogsBingoModPlugin.instance.SaveData.FleaHuntersMarch.Value = PlayerData.instance.SavedFlea_Ant_03;
                VogsBingoModPlugin.instance.SaveData.FleaPilgrimsRest.Value = PlayerData.instance.SavedFlea_Bone_East_10_Church;
                VogsBingoModPlugin.instance.SaveData.FleaShellwood.Value = PlayerData.instance.SavedFlea_Shellwood_03;
                VogsBingoModPlugin.instance.SaveData.FleaSinnersRoad.Value = PlayerData.instance.SavedFlea_Dust_12;
                VogsBingoModPlugin.instance.SaveData.FleaSwiftStep.Value = PlayerData.instance.SavedFlea_Bone_East_05;
                VogsBingoModPlugin.instance.SaveData.FleaUnderworksCauldron.Value = PlayerData.instance.SavedFlea_Under_21;
                VogsBingoModPlugin.instance.SaveData.FleaUnderworksWispThicket.Value = PlayerData.instance.SavedFlea_Under_23;
                VogsBingoModPlugin.instance.SaveData.FleaWormways.Value = PlayerData.instance.SavedFlea_Crawl_06;
                if(PlayerData.instance.SavedFlea_Dust_09)
                    MarkIfAvailable(GoalID.ExhaustOrganFleaOne);
                if(PlayerData.instance.SavedFlea_Peak_05c)
                    MarkIfAvailable(GoalID.MtFayFleaOne);
                if(PlayerData.instance.SavedFlea_Coral_24)
                    MarkIfAvailable(GoalID.SandsofKarakFleaOne);
                if (PlayerData.instance.CaravanLechSaved)
                {
                    VogsBingoModPlugin.instance.SaveData.FleaKratt.Value = true;
                    fleaCount++;
                }
                if (PlayerData.instance.MetTroupeHunterWild)
                {
                    MarkIfAvailable(GoalID.TalktoVog);
                    fleaCount++;
                }
                VogsBingoModPlugin.instance.SaveData.FleasSaved.Value = fleaCount;
            }
        }

        static bool CheckIfToolsObtained(string toolToIgnore, params string[] toolNames)
        {
            foreach (string toolName in toolNames)
            {
                try
                {
                    if (!(toolName.Equals(toolToIgnore) || ToolItemManager.Instance.toolItems.GetByName(toolName).IsUnlocked))
                    {
                        return false;
                    }
                } catch (Exception)
                {
                    VogsBingoModPlugin.LogError($"Tool named \"{toolName}\" could not be found in the tool list.");
                }
            }
            return true;
        }
    }
}
