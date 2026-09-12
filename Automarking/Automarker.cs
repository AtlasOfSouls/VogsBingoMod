/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
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
                if (key < 0)
                {
                    key = -i - 1;
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
            if (BoardHasGoal(goalID))
            {
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
            byte[]? bytes = Resources.GetResourceAsByteArray("VogsBingoMod.Automarking.Goals.json");
            if (bytes == null)
            {
                VogsBingoModPlugin.LogError("Could not retrieve the goal support json.");
            }
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
                    goalCompleted = CheckIfToolsObtained(toolToIgnore: context, "Dead Mans Purse") && SaveData.Silkeaters > 0;
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
                    try
                    {
                        goalCompleted = SaveData.BlastedSilkeater && (context.Equals("craftmetal") || SceneData.instance.persistentBools.scenes["Coral_32"]["Collectable Item Pickup - Tool Metal"].Value);
                    } catch (Exception){}
                    break;
                case GoalID.BothFreeSimpleKeys:
                    goalCompleted = SaveData.SinnersKey && SaveData.KarakKey;
                    break;
                case GoalID.ShellwoodVaultsMaskShards:
                    goalCompleted = SaveData.ShellwoodMaskShard && SaveData.VaultsMaskShard;
                    break;
                case GoalID.CogworkClapperJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.CogworkClapperKilled;
                    break;
                case GoalID.ImobaJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.ImobaKilled;
                    break;
                case GoalID.SquirrmJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.SquirrmKilled;
                    break;
                case GoalID.GromlingJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.GromlingKilled;
                    break;
                case GoalID.CraggliteJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.CraggliteKilled;
                    break;
                case GoalID.DeepDiverJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.DeepDiverKilled;
                    break;
                case GoalID.SkullwingJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.SkullwingKilled;
                    break;
                case GoalID.MiteMotherJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.MiteMotherKilled;
                    break;
                case GoalID.UnravelledJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.UnravelledKilled;
                    break;
                case GoalID.ShadowChargerJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.ShadowChargerKilled;
                    break;
                case GoalID.MassiveMossgrubJournalEntry:
                    goalCompleted = SaveData.HasJournal && SaveData.MassiveMossgrubKilled;
                    break;
                case GoalID.TalktoStyxHuntress:
                    goalCompleted = SaveData.HuntressSpokenTo && SaveData.StyxSpokenTo;
                    break;
                case GoalID.TalktoForgeDaughterandTwelfthArchitect:
                    goalCompleted = SaveData.TwelfthArchitectSpokenTo && SaveData.ForgeDaughterSpokenTo;
                    break;
                case GoalID.BreakBothofStyxsOneways:
                    goalCompleted = SaveData.SinnersStyxWallBroken && SaveData.GreymoorStyxWallBroken;
                    break;
                case GoalID.BothVendorSimpleKeys:
                    goalCompleted = SaveData.JubilanaKeyBought && SaveData.PebbKeyBought;
                    break;
                case GoalID.GreymoorFleasTwoKratt:
                    goalCompleted = SaveData.FleaCrawLake && SaveData.FleaGreymoorTower && SaveData.FleaKratt;
                    break;
                case GoalID.FarFieldsFleasTwo:
                    goalCompleted = SaveData.FleaFarFieldsCage && SaveData.FleaPilgrimsRest;
                    break;
                case GoalID.ShellwoodBellhartFleasTwo:
                    goalCompleted = SaveData.FleaShellwood && SaveData.FleaBellvein;
                    break;
                case GoalID.DeepDocksFleasThree:
                    goalCompleted = SaveData.FleaSwiftStep && SaveData.FleaDeeperDocks && SaveData.FleaDeepDocksBellway;
                    break;
                case GoalID.WormwaysBlastedStepsFleasTwo:
                    goalCompleted = SaveData.FleaWormways && SaveData.FleaBlastedSteps;
                    break;
                case GoalID.UnderworksFleasTwo:
                    goalCompleted = SaveData.FleaUnderworksCauldron && SaveData.FleaUnderworksWispThicket;
                    break;
                case GoalID.LowerBilewaterHuntersMarchFleasTwo:
                    goalCompleted = SaveData.FleaBilewaterThieves && SaveData.FleaHuntersMarch;
                    break;
                case GoalID.SinnersRoadVaultsFleasTwo:
                    goalCompleted = SaveData.FleaSinnersRoad && SaveData.FleaVaults;
                    break;
                case GoalID.DeepDocksSpoolFragmentsTwo:
                    goalCompleted = SaveData.DeepDocksSpoolFragNearSpa && SaveData.DeeperDocksSpoolFrag;
                    break;
                case GoalID.UnderworksSpoolFragmentsTwo:
                    goalCompleted = SaveData.UnderworksArenaSpoolFrag && SaveData.UnderworksLibrary_11bSpoolFrag;
                    break;
                case GoalID.HaveSixRosaryStringsnopurchasing:
                    goalCompleted = (SaveData.automarkRosaryStringHandler.GetNonPurchasedStringsCurrentlyHeld() + SaveData.automarkRosaryStringHandler.GetFrayedStringsCurrentlyHeld()) >= 6;
                    break;
                case GoalID.BreakEightRosaryStringsnopurchasing:
                    goalCompleted = (SaveData.automarkRosaryStringHandler.GetNonPurchasedStringsBroken() + SaveData.automarkRosaryStringHandler.GetFrayedStringsBroken()) >= 8;
                    break;
                case GoalID.TalktoGrishkinandGilly:
                    goalCompleted = SaveData.GillySpokenTo && SaveData.GrishkinSpokenTo;
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
                    if (PlayerData.instance.mapBoolList != null && SaveData.MapCount.Value != PlayerData.instance.mapBoolList.HasCount)
                    {
                        SaveData.MapCount.Value = PlayerData.instance.mapBoolList.HasCount;
                    }
                }
            }
        }

        internal static void UpdateFleas(bool bigFleaBeaten = false)
        {
            if (PlayerData.instance != null)
            {
                int fleaCount =  PlayerData.instance.SavedFleasCount;
                SaveData.CitadelFleas.Value = 0;
                if (PlayerData.instance.SavedFlea_Song_11)
                    SaveData.CitadelFleas.Value++;
                if (PlayerData.instance.SavedFlea_Song_14)
                    SaveData.CitadelFleas.Value++;
                if (PlayerData.instance.SavedFlea_Library_09)
                    SaveData.CitadelFleas.Value++;
                if (bigFleaBeaten || PlayerData.instance.tamedGiantFlea)
                {
                    SaveData.CitadelFleas.Value++;
                    fleaCount++;
                }
                if (PlayerData.instance.SavedFlea_Library_01)
                {
                    SaveData.CitadelFleas.Value++;
                    SaveData.FleaVaults.Value = true;
                }
                SaveData.FleaBellvein.Value = PlayerData.instance.SavedFlea_Belltown_04;
                SaveData.FleaBilewaterThieves.Value = PlayerData.instance.SavedFlea_Shadow_28;
                SaveData.FleaBlastedSteps.Value = PlayerData.instance.SavedFlea_Coral_35;
                SaveData.FleaCrawLake.Value = PlayerData.instance.SavedFlea_Greymoor_15b;
                SaveData.FleaDeepDocksBellway.Value = PlayerData.instance.SavedFlea_Dock_16;
                SaveData.FleaDeeperDocks.Value = PlayerData.instance.SavedFlea_Dock_03d;
                SaveData.FleaFarFieldsCage.Value = PlayerData.instance.SavedFlea_Bone_East_17b;
                SaveData.FleaGreymoorTower.Value = PlayerData.instance.SavedFlea_Greymoor_06;
                SaveData.FleaHuntersMarch.Value = PlayerData.instance.SavedFlea_Ant_03;
                SaveData.FleaPilgrimsRest.Value = PlayerData.instance.SavedFlea_Bone_East_10_Church;
                SaveData.FleaShellwood.Value = PlayerData.instance.SavedFlea_Shellwood_03;
                SaveData.FleaSinnersRoad.Value = PlayerData.instance.SavedFlea_Dust_12;
                SaveData.FleaSwiftStep.Value = PlayerData.instance.SavedFlea_Bone_East_05;
                SaveData.FleaUnderworksCauldron.Value = PlayerData.instance.SavedFlea_Under_21;
                SaveData.FleaUnderworksWispThicket.Value = PlayerData.instance.SavedFlea_Under_23;
                SaveData.FleaWormways.Value = PlayerData.instance.SavedFlea_Crawl_06;
                
                if(PlayerData.instance.SavedFlea_Dust_09)
                    MarkIfAvailable(GoalID.ExhaustOrganFleaOne);
                if(PlayerData.instance.SavedFlea_Peak_05c)
                    MarkIfAvailable(GoalID.MtFayFleaOne);
                if(PlayerData.instance.SavedFlea_Coral_24)
                    MarkIfAvailable(GoalID.SandsofKarakFleaOne);
                if(PlayerData.instance.SavedFlea_Shadow_10)
                    MarkIfAvailable(GoalID.UpperBilewaterFlea);
                if (PlayerData.instance.CaravanLechSaved)
                {
                    SaveData.FleaKratt.Value = true;
                    fleaCount++;
                }
                if (PlayerData.instance.MetTroupeHunterWild)
                {
                    MarkIfAvailable(GoalID.TalktoVog);
                    fleaCount++;
                }
                SaveData.FleasSaved.Value = fleaCount;
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
