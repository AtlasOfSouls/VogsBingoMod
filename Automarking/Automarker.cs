/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SilksongBingoMod.UI;
using UnityEngine;

namespace SilksongBingoMod.Automarking
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
            Assembly executeAssembly = Assembly.GetExecutingAssembly();
            Stream stream = executeAssembly.GetManifestResourceStream($"SilksongBingoMod.Automarking.Goals.json");
            if (stream == null)
            {
                SilksongBingoModPlugin.LogError($"Could not find the goals resource.");
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
                    AutomarkerData.StraightPin.Value = true;
                    break;
                case "Tri Pin":
                    AutomarkerData.ThreefoldPin.Value = true;
                    break;
                case "Sting Shard":
                    AutomarkerData.StingShard.Value = true;
                    break;
                case "Tack":
                    MarkIfAvailable(GoalID.Tacks);
                    break;
                case "Harpoon":
                    AutomarkerData.Longpin.Value = true;
                    break;
                case "Curve Claws":
                    AutomarkerData.Curveclaw.Value = true;
                    break;
                case "Pimpilo":
                    AutomarkerData.Pimpillo.Value = true;
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
                    AutomarkerData.Voltvessels.Value = true;
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
                    AutomarkerData.MagmaBell.Value = true;
                    break;
                case "Bell Bind":
                    AutomarkerData.WardingBell.Value = true;
                    break;
                case "Poison Pouch":
                    AutomarkerData.PollipPouch.Value = true;
                    MarkIfAvailable(GoalID.PollipPouch);
                    break;
                case "Fractured Mask":
                    AutomarkerData.FracturedMask.Value = true;
                    break;
                case "Multibind":
                    MarkIfAvailable(GoalID.Multibinder);
                    break;
                case "White Ring":
                    AutomarkerData.Weavelight.Value = true;
                    break;
                case "Brolly Spike":
                    AutomarkerData.SawtoothCirclet.Value = true;
                    break;
                case "Quickbind":
                    AutomarkerData.InjectorBand.Value = true;
                    break;
                case "Dazzle Bind":
                    AutomarkerData.ClawMirror.Value = true;
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
                    AutomarkerData.Compass.Value = true;
                    break;
                case "Bone Necklace":
                    AutomarkerData.ShardPendant.Value = true;
                    break;
                case "Rosary Magnet":
                    AutomarkerData.MagnetiteBrooch.Value = true;
                    break;
                case "Weighted Anklet":
                    AutomarkerData.WeightedBelt.Value = true;
                    break;
                case "Barbed Wire":
                    AutomarkerData.BarbedBracelet.Value = true;
                    break;
                case "Dead Mans Purse":
                    AutomarkerData.DeadBugsPurse.Value = true;
                    break;
                case "Magnetite Dice":
                    AutomarkerData.MagnetiteDice.Value = true;
                    break;
                case "Scuttlebrace":
                    AutomarkerData.Scuttlebrace.Value = true;
                    break;
                case "Wallcling":
                    AutomarkerData.AscendantsGrip.Value = true;
                    break;
                case "Sprintmaster":
                    AutomarkerData.SilkspeedAnklets.Value = true;
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
            
            AutomarkerData.SilkSkills.Value = skillCount;
        }

        internal static void CheckIfGoalCompleted(GoalID goalID)
        {
            bool goalCompleted = false;
            switch (goalID)
            {
                case GoalID.CompassPendantBrooch:
                    goalCompleted = AutomarkerData.ShardPendant && AutomarkerData.Compass && AutomarkerData.MagnetiteBrooch;
                    break;
                case GoalID.StraightThreefoldandLongPin:
                    goalCompleted = AutomarkerData.StraightPin && AutomarkerData.ThreefoldPin && AutomarkerData.Longpin;
                    break;
                case GoalID.DeadBugsPurseaSilkeater:
                    goalCompleted = AutomarkerData.DeadBugsPurse && AutomarkerData.Silkeaters >= 1;
                    break;
                case GoalID.BarbedBraceletFracturedMask:
                    goalCompleted = AutomarkerData.BarbedBracelet && AutomarkerData.FracturedMask;
                    break;
                case GoalID.MagnetiteDiceMagnetiteBrooch:
                    goalCompleted = AutomarkerData.MagnetiteDice && AutomarkerData.MagnetiteBrooch;
                    break;
                case GoalID.WardingBellClawMirror:
                    goalCompleted = AutomarkerData.WardingBell && AutomarkerData.ClawMirror;
                    break;
                case GoalID.WardingBellSawtoothCirclet:
                    goalCompleted = AutomarkerData.WardingBell && AutomarkerData.SawtoothCirclet;
                    break;
                case GoalID.MagmaBellCurveclaw:
                    goalCompleted = AutomarkerData.MagmaBell && AutomarkerData.Curveclaw;
                    break;
                case GoalID.WeightedBeltAscendantsGrip:
                    goalCompleted = AutomarkerData.WeightedBelt && AutomarkerData.AscendantsGrip;
                    break;
                case GoalID.ScuttlebraceSilkspeed:
                    goalCompleted = AutomarkerData.Scuttlebrace && AutomarkerData.SilkspeedAnklets;
                    break;
                case GoalID.PimpilloVoltvessels:
                    goalCompleted = AutomarkerData.Pimpillo && AutomarkerData.Voltvessels;
                    break;
                case GoalID.PollipPouchStingShard:
                    goalCompleted = AutomarkerData.PollipPouch && AutomarkerData.StingShard;
                    break;
                case GoalID.WeavelightInjectorBand:
                    goalCompleted = AutomarkerData.Weavelight && AutomarkerData.InjectorBand;
                    break;
                case GoalID.BlastedStepsSilkeaterCraftmetal:
                    goalCompleted = AutomarkerData.BlastedSilkeater && AutomarkerData.BlastedCraftmetal;
                    break;
                case GoalID.BothFreeSimpleKeys:
                    goalCompleted = AutomarkerData.SinnersKey && AutomarkerData.KarakKey;
                    break;
                case GoalID.ShellwoodVaultsMaskShards:
                    goalCompleted = AutomarkerData.ShellwoodMaskShard && AutomarkerData.VaultsMaskShard;
                    break;
                case GoalID.CogworkClapperJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.CogworkClapperKilled;
                    break;
                case GoalID.ImobaJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.ImobaKilled;
                    break;
                case GoalID.SquirrmJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.SquirrmKilled;
                    break;
                case GoalID.GromlingJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.GromlingKilled;
                    break;
                case GoalID.CraggliteJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.CraggliteKilled;
                    break;
                case GoalID.DeepDiverJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.DeepDiverKilled;
                    break;
                case GoalID.SkullwingJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.SkullwingKilled;
                    break;
                case GoalID.MiteMotherJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.MiteMotherKilled;
                    break;
                case GoalID.UnravelledJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.UnravelledKilled;
                    break;
                case GoalID.ShadowChargerJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.ShadowChargerKilled;
                    break;
                case GoalID.MassiveMossgrubJournalEntry:
                    goalCompleted = AutomarkerData.HasJournal && AutomarkerData.MassiveMossgrubKilled;
                    break;
                case GoalID.TalktoStyxHuntress:
                    goalCompleted = AutomarkerData.HuntressSpokenTo && AutomarkerData.StyxSpokenTo;
                    break;
                case GoalID.TalktoForgeDaughterandTwelfthArchitect:
                    goalCompleted = AutomarkerData.TwelfthArchitectSpokenTo && AutomarkerData.ForgeDaughterSpokenTo;
                    break;
                case GoalID.BreakBothofStyxsOneways:
                    goalCompleted = AutomarkerData.SinnersStyxWallBroken && AutomarkerData.GreymoorStyxWallBroken;
                    break;
                case GoalID.BothVendorSimpleKeys:
                    goalCompleted = AutomarkerData.JubilanaKeyBought && AutomarkerData.PebbKeyBought;
                    break;
                case GoalID.GreymoorFleasTwoKratt:
                    goalCompleted = AutomarkerData.FleaCrawLake && AutomarkerData.FleaGreymoorTower && AutomarkerData.FleaKratt;
                    break;
                case GoalID.FarFieldsFleasTwo:
                    goalCompleted = AutomarkerData.FleaFarFieldsCage && AutomarkerData.FleaPilgrimsRest;
                    break;
                case GoalID.ShellwoodBellhartFleasTwo:
                    goalCompleted = AutomarkerData.FleaShellwood && AutomarkerData.FleaBellvein;
                    break;
                case GoalID.DeepDocksFleasThree:
                    goalCompleted = AutomarkerData.FleaSwiftStep && AutomarkerData.FleaDeeperDocks && AutomarkerData.FleaDeepDocksBellway;
                    break;
                case GoalID.WormwaysBlastedStepsFleasTwo:
                    goalCompleted = AutomarkerData.FleaWormways && AutomarkerData.FleaBlastedSteps;
                    break;
                case GoalID.UnderworksFleasTwo:
                    goalCompleted = AutomarkerData.FleaUnderworksCauldron && AutomarkerData.FleaUnderworksWispThicket;
                    break;
                case GoalID.LowerBilewaterHuntersMarchFleasTwo:
                    goalCompleted = AutomarkerData.FleaBilewaterThieves && AutomarkerData.FleaHuntersMarch;
                    break;
                case GoalID.SinnersRoadVaultsFleasTwo:
                    goalCompleted = AutomarkerData.FleaSinnersRoad && AutomarkerData.FleaVaults;
                    break;
                case GoalID.DeepDocksSpoolFragmentsTwo:
                    goalCompleted = AutomarkerData.DeepDocksSpoolFragNearSpa && AutomarkerData.DeeperDocksSpoolFrag;
                    break;
                case GoalID.HaveSixRosaryStringsnopurchasing:
                    goalCompleted = (AutomarkRosaryStringHandler.GetNonPurchasedStringsCurrentlyHeld() + AutomarkRosaryStringHandler.GetFrayedStringsCurrentlyHeld()) >= 6;
                    break;
                case GoalID.BreakEightRosaryStringsnopurchasing:
                    goalCompleted = (AutomarkRosaryStringHandler.GetNonPurchasedStringsBroken() + AutomarkRosaryStringHandler.GetFrayedStringsBroken()) >= 8;
                    break;
                default:
                    SilksongBingoModPlugin.LogError($"There isn't a check for goalID {goalID}");
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
            if (currentValue >= markValue.markValue)
            {
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
                    if (PlayerData.instance.mapBoolList != null)
                    {
                        AutomarkerData.MapCount.Value = PlayerData.instance.mapBoolList.HasCount;
                    }
                }
            }
        }

        internal static void UpdateFleas()
        {
            if (PlayerData.instance != null)
            {
                int fleaCount =  PlayerData.instance.SavedFleasCount;
                AutomarkerData.CitadelFleas.Value = 0;
                if (PlayerData.instance.SavedFlea_Song_11)
                    AutomarkerData.CitadelFleas.Value++;
                if (PlayerData.instance.SavedFlea_Song_14)
                    AutomarkerData.CitadelFleas.Value++;
                if (PlayerData.instance.SavedFlea_Library_09)
                    AutomarkerData.CitadelFleas.Value++;
                if (PlayerData.instance.tamedGiantFlea)
                {
                    AutomarkerData.CitadelFleas.Value++;
                    MarkIfAvailable(GoalID.BeatBigFlea);
                    fleaCount++;
                }
                if (PlayerData.instance.SavedFlea_Library_01)
                {
                    AutomarkerData.CitadelFleas.Value++;
                    AutomarkerData.FleaVaults.Value = true;
                }
                AutomarkerData.FleaBellvein.Value = PlayerData.instance.SavedFlea_Belltown_04;
                AutomarkerData.FleaBilewaterThieves.Value = PlayerData.instance.SavedFlea_Shadow_28;
                AutomarkerData.FleaBlastedSteps.Value = PlayerData.instance.SavedFlea_Coral_35;
                AutomarkerData.FleaCrawLake.Value = PlayerData.instance.SavedFlea_Greymoor_15b;
                AutomarkerData.FleaDeepDocksBellway.Value = PlayerData.instance.SavedFlea_Dock_16;
                AutomarkerData.FleaDeeperDocks.Value = PlayerData.instance.SavedFlea_Dock_03d;
                AutomarkerData.FleaFarFieldsCage.Value = PlayerData.instance.SavedFlea_Bone_East_17b;
                AutomarkerData.FleaGreymoorTower.Value = PlayerData.instance.SavedFlea_Greymoor_06;
                AutomarkerData.FleaHuntersMarch.Value = PlayerData.instance.SavedFlea_Ant_03;
                AutomarkerData.FleaPilgrimsRest.Value = PlayerData.instance.SavedFlea_Bone_East_10_Church;
                AutomarkerData.FleaShellwood.Value = PlayerData.instance.SavedFlea_Shellwood_03;
                AutomarkerData.FleaSinnersRoad.Value = PlayerData.instance.SavedFlea_Dust_12;
                AutomarkerData.FleaSwiftStep.Value = PlayerData.instance.SavedFlea_Bone_06;
                AutomarkerData.FleaUnderworksCauldron.Value = PlayerData.instance.SavedFlea_Under_21;
                AutomarkerData.FleaUnderworksWispThicket.Value = PlayerData.instance.SavedFlea_Under_23;
                AutomarkerData.FleaWormways.Value = PlayerData.instance.SavedFlea_Crawl_06;
                if(PlayerData.instance.SavedFlea_Dust_09)
                    MarkIfAvailable(GoalID.ExhaustOrganFleaOne);
                if(PlayerData.instance.SavedFlea_Peak_05c)
                    MarkIfAvailable(GoalID.MtFayFleaOne);
                if(PlayerData.instance.SavedFlea_Coral_24)
                    MarkIfAvailable(GoalID.SandsofKarakFleaOne);
                if (PlayerData.instance.CaravanLechSaved)
                {
                    AutomarkerData.FleaKratt.Value = true;
                    fleaCount++;
                }
                if (PlayerData.instance.MetTroupeHunterWild)
                {
                    MarkIfAvailable(GoalID.TalktoVog);
                    fleaCount++;
                }
                AutomarkerData.FleasSaved.Value = fleaCount;
            }
        }
    }
}
