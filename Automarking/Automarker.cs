/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using VogsBingoMod.UI;
using UnityEngine;

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
            VogsBingoModPlugin.LogInfo($"Checking if goal {(GoalID)goalID} is on the board");
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

        internal static void UpdateTools(IEnumerable<ToolItem> tools)
        {
            VogsBingoModPlugin.LogInfo("updating tools");
            int redToolCount = 0;
            int blueToolCount = 0;
            int yellowToolCount = 0;
            foreach (ToolItem tool in tools)
            {
                UpdateTool(tool.name);
                switch (tool.type)
                {
                    case ToolItemType.Red:
                        redToolCount++;
                        break;
                    case ToolItemType.Blue:
                        blueToolCount++;
                        break;
                    case ToolItemType.Yellow:
                        yellowToolCount++;
                        break;
                    default:
                        break;
                }
            }
            switch (redToolCount)
            {
                case >= 5:
                    MarkIfAvailable(GoalID.FiveRedTools);
                    break;
                case >= 3:
                    MarkIfAvailable(GoalID.ThreeRedTools);
                    break;
                default:
                    break;
            }
            if (blueToolCount >= 3)
            {
                MarkIfAvailable(GoalID.ThreeBlueTools);
            }
            switch (yellowToolCount)
            {
                case >= 5:
                    MarkIfAvailable(GoalID.FiveYellowTools);
                    break;
                case >= 3:
                    MarkIfAvailable(GoalID.ThreeYellowTools);
                    break;
                default:
                    break;
            }
            if (redToolCount >= 2 && blueToolCount >= 2 && yellowToolCount >= 2)
            {
                MarkIfAvailable(GoalID.Twoofeachtooltype);
            }
        }

        static void UpdateTool(string toolName)
        {
            switch (toolName)
            {
                case "Straight Pin":
                    VogsBingoModPlugin.instance.SaveData.StraightPin.Value = true;
                    break;
                case "Tri Pin":
                    VogsBingoModPlugin.instance.SaveData.ThreefoldPin.Value = true;
                    break;
                case "Sting Shard":
                    VogsBingoModPlugin.instance.SaveData.StingShard.Value = true;
                    break;
                case "Tack":
                    MarkIfAvailable(GoalID.Tacks);
                    break;
                case "Harpoon":
                    VogsBingoModPlugin.instance.SaveData.Longpin.Value = true;
                    break;
                case "Curve Claws":
                    VogsBingoModPlugin.instance.SaveData.Curveclaw.Value = true;
                    break;
                case "Pimpilo":
                    VogsBingoModPlugin.instance.SaveData.Pimpillo.Value = true;
                    break;
                case "Conch Drill":
                    MarkIfAvailable(GoalID.Conchcutter);
                    break;
                case "WebShot Forge":
                    MarkIfAvailable(GoalID.RepairSilkshot);
                    break;
                case "WebShot Architect":
                    MarkIfAvailable(GoalID.RepairSilkshot);
                    break;
                case "WebShot Weaver":
                    MarkIfAvailable(GoalID.RepairSilkshot);
                    break;
                case "Screw Attack":
                    MarkIfAvailable(GoalID.DelversDrill);
                    break;
                case "Cogwork Flier":
                    MarkIfAvailable(GoalID.Cogfly);
                    break;
                case "Rosary Cannon":
                    MarkIfAvailable(GoalID.RosaryCannon);
                    break;
                case "Lightning Rod":
                    VogsBingoModPlugin.instance.SaveData.Voltvessels.Value = true;
                    break;
                case "Flintstone":
                    MarkIfAvailable(GoalID.Flintslate);
                    break;
                case "Silk Snare":
                    MarkIfAvailable(GoalID.SnareSetter);
                    break;
                case "Lifeblood Syringe":
                    MarkIfAvailable(GoalID.PlasmiumPhial);
                    break;
                case "Mosscreep Tool 2":
                    MarkIfAvailable(GoalID.UpgradeDruidsEye);
                    break;
                case "Lava Charm":
                    VogsBingoModPlugin.instance.SaveData.MagmaBell.Value = true;
                    break;
                case "Bell Bind":
                    VogsBingoModPlugin.instance.SaveData.WardingBell.Value = true;
                    break;
                case "Poison Pouch":
                    VogsBingoModPlugin.instance.SaveData.PollipPouch.Value = true;
                    MarkIfAvailable(GoalID.PollipPouch);
                    break;
                case "Fractured Mask":
                    VogsBingoModPlugin.instance.SaveData.FracturedMask.Value = true;
                    break;
                case "Multibind":
                    MarkIfAvailable(GoalID.Multibinder);
                    break;
                case "White Ring":
                    VogsBingoModPlugin.instance.SaveData.Weavelight.Value = true;
                    break;
                case "Brolly Spike":
                    VogsBingoModPlugin.instance.SaveData.SawtoothCirclet.Value = true;
                    break;
                case "Quickbind":
                    VogsBingoModPlugin.instance.SaveData.InjectorBand.Value = true;
                    break;
                case "Dazzle Bind":
                    VogsBingoModPlugin.instance.SaveData.ClawMirror.Value = true;
                    break;
                case "Revenge Crystal":
                    MarkIfAvailable(GoalID.MemoryCrystal);
                    break;
                case "Quick Sling":
                    MarkIfAvailable(GoalID.QuickSling);
                    break;
                case "Maggot Charm":
                    MarkIfAvailable(GoalID.WreathofPurity);
                    break;
                case "Pinstress Tool":
                    MarkIfAvailable(GoalID.PinBadge);
                    break;
                case "Compass":
                    VogsBingoModPlugin.instance.SaveData.Compass.Value = true;
                    break;
                case "Bone Necklace":
                    VogsBingoModPlugin.instance.SaveData.ShardPendant.Value = true;
                    break;
                case "Rosary Magnet":
                    VogsBingoModPlugin.instance.SaveData.MagnetiteBrooch.Value = true;
                    break;
                case "Weighted Anklet":
                    VogsBingoModPlugin.instance.SaveData.WeightedBelt.Value = true;
                    break;
                case "Barbed Wire":
                    VogsBingoModPlugin.instance.SaveData.BarbedBracelet.Value = true;
                    break;
                case "Dead Mans Purse":
                    VogsBingoModPlugin.instance.SaveData.DeadBugsPurse.Value = true;
                    break;
                case "Magnetite Dice":
                    VogsBingoModPlugin.instance.SaveData.MagnetiteDice.Value = true;
                    break;
                case "Scuttlebrace":
                    VogsBingoModPlugin.instance.SaveData.Scuttlebrace.Value = true;
                    break;
                case "Wallcling":
                    VogsBingoModPlugin.instance.SaveData.AscendantsGrip.Value = true;
                    break;
                case "Sprintmaster":
                    VogsBingoModPlugin.instance.SaveData.SilkspeedAnklets.Value = true;
                    break;
                default:
                    break;
            }
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
            
            VogsBingoModPlugin.instance.SaveData.SilkSkills.Value = skillCount;
        }

        internal static void CheckIfGoalCompleted(GoalID goalID)
        {
            bool goalCompleted = false;
            switch (goalID)
            {
                case GoalID.CompassPendantBrooch:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.ShardPendant && VogsBingoModPlugin.instance.SaveData.Compass && VogsBingoModPlugin.instance.SaveData.MagnetiteBrooch;
                    break;
                case GoalID.StraightThreefoldandLongPin:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.StraightPin && VogsBingoModPlugin.instance.SaveData.ThreefoldPin && VogsBingoModPlugin.instance.SaveData.Longpin;
                    break;
                case GoalID.DeadBugsPurseaSilkeater:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.DeadBugsPurse && VogsBingoModPlugin.instance.SaveData.Silkeaters >= 1;
                    break;
                case GoalID.BarbedBraceletFracturedMask:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.BarbedBracelet && VogsBingoModPlugin.instance.SaveData.FracturedMask;
                    break;
                case GoalID.MagnetiteDiceMagnetiteBrooch:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.MagnetiteDice && VogsBingoModPlugin.instance.SaveData.MagnetiteBrooch;
                    break;
                case GoalID.WardingBellClawMirror:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.WardingBell && VogsBingoModPlugin.instance.SaveData.ClawMirror;
                    break;
                case GoalID.WardingBellSawtoothCirclet:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.WardingBell && VogsBingoModPlugin.instance.SaveData.SawtoothCirclet;
                    break;
                case GoalID.MagmaBellCurveclaw:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.MagmaBell && VogsBingoModPlugin.instance.SaveData.Curveclaw;
                    break;
                case GoalID.WeightedBeltAscendantsGrip:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.WeightedBelt && VogsBingoModPlugin.instance.SaveData.AscendantsGrip;
                    break;
                case GoalID.ScuttlebraceSilkspeed:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.Scuttlebrace && VogsBingoModPlugin.instance.SaveData.SilkspeedAnklets;
                    break;
                case GoalID.PimpilloVoltvessels:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.Pimpillo && VogsBingoModPlugin.instance.SaveData.Voltvessels;
                    break;
                case GoalID.PollipPouchStingShard:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.PollipPouch && VogsBingoModPlugin.instance.SaveData.StingShard;
                    break;
                case GoalID.WeavelightInjectorBand:
                    goalCompleted = VogsBingoModPlugin.instance.SaveData.Weavelight && VogsBingoModPlugin.instance.SaveData.InjectorBand;
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
                    goalCompleted = (AutomarkRosaryStringHandler.GetNonPurchasedStringsCurrentlyHeld() + AutomarkRosaryStringHandler.GetFrayedStringsCurrentlyHeld()) >= 6;
                    break;
                case GoalID.BreakEightRosaryStringsnopurchasing:
                    goalCompleted = (AutomarkRosaryStringHandler.GetNonPurchasedStringsBroken() + AutomarkRosaryStringHandler.GetFrayedStringsBroken()) >= 8;
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
                VogsBingoModPlugin.instance.SaveData.FleaSwiftStep.Value = PlayerData.instance.SavedFlea_Bone_06;
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
    }
}
