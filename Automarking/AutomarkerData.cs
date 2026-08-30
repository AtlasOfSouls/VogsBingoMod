/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections.Generic;

namespace SilksongBingoMod.Automarking
{
    internal static class AutomarkerData
    {
        internal static List<AutomarkerDataBool> bools = new List<AutomarkerDataBool>();
        internal static List<AutomarkerDataInt> ints = new List<AutomarkerDataInt>();
        internal static List<AutomarkerDataBitmask> bitmasks = new List<AutomarkerDataBitmask>();   
        internal static AutomarkerDataBool StraightPin = new([GoalID.StraightThreefoldandLongPin], "StraightPin"); 
        internal static AutomarkerDataBool Longpin = new([GoalID.StraightThreefoldandLongPin], "Longpin");
        internal static AutomarkerDataBool ThreefoldPin = new([GoalID.StraightThreefoldandLongPin], "ThreefoldPin");
        internal static AutomarkerDataBool DeadBugsPurse = new([GoalID.DeadBugsPurseaSilkeater], "DeadBugsPurse");
        internal static AutomarkerDataBool BarbedBracelet = new([GoalID.BarbedBraceletFracturedMask], "BarbedBracelet");
        internal static AutomarkerDataBool FracturedMask = new([GoalID.BarbedBraceletFracturedMask], "FracturedMask");
        internal static AutomarkerDataBool MagnetiteDice = new([GoalID.MagnetiteDiceMagnetiteBrooch], "MagnetiteDice");
        internal static AutomarkerDataBool MagnetiteBrooch = new([GoalID.CompassPendantBrooch, GoalID.MagnetiteDiceMagnetiteBrooch], "MagnetiteBrooch");
        internal static AutomarkerDataBool Compass = new([GoalID.CompassPendantBrooch], "Compass");
        internal static AutomarkerDataBool ShardPendant = new([GoalID.CompassPendantBrooch], "ShardPendant");
        internal static AutomarkerDataBool WardingBell = new([GoalID.WardingBellClawMirror, GoalID.WardingBellSawtoothCirclet], "WardingBell");
        internal static AutomarkerDataBool ClawMirror = new([GoalID.WardingBellClawMirror], "ClawMirror");
        internal static AutomarkerDataBool SawtoothCirclet = new([GoalID.WardingBellSawtoothCirclet], "SawtoothCirclet");
        internal static AutomarkerDataBool MagmaBell = new([GoalID.MagmaBellCurveclaw], "MagmaBell");
        internal static AutomarkerDataBool Curveclaw = new([GoalID.MagmaBellCurveclaw], "Curveclaw");
        internal static AutomarkerDataBool WeightedBelt = new([GoalID.WeightedBeltAscendantsGrip], "WeightedBelt");
        internal static AutomarkerDataBool AscendantsGrip = new([GoalID.WeightedBeltAscendantsGrip], "AscendantsGrip");
        internal static AutomarkerDataBool Scuttlebrace = new([GoalID.ScuttlebraceSilkspeed], "Scuttlebrace");
        internal static AutomarkerDataBool SilkspeedAnklets = new([GoalID.ScuttlebraceSilkspeed], "SilkspeedAnklets");
        internal static AutomarkerDataBool Pimpillo = new([GoalID.PimpilloVoltvessels], "Pimpillo");
        internal static AutomarkerDataBool Voltvessels = new([GoalID.PimpilloVoltvessels], "Voltvessels");
        internal static AutomarkerDataBool PollipPouch = new([GoalID.PollipPouchStingShard], "PollipPouch");
        internal static AutomarkerDataBool StingShard = new([GoalID.PollipPouchStingShard], "StingShard");
        internal static AutomarkerDataBool Weavelight = new([GoalID.WeavelightInjectorBand], "Weavelight");
        internal static AutomarkerDataBool InjectorBand = new([GoalID.WeavelightInjectorBand], "InjectorBand");
        internal static AutomarkerDataBool BlastedSilkeater = new([GoalID.BlastedStepsSilkeaterCraftmetal], "BlastedSilkeater");
        internal static AutomarkerDataBool BlastedCraftmetal = new([GoalID.BlastedStepsSilkeaterCraftmetal], "BlastedCraftmetal");
        internal static AutomarkerDataBool SinnersKey = new([GoalID.BothFreeSimpleKeys], "SinnersKey");
        internal static AutomarkerDataBool KarakKey = new([GoalID.BothFreeSimpleKeys], "KarakKey");
        internal static AutomarkerDataBool SilkeaterBool = new([GoalID.DeadBugsPurseaSilkeater], "SilkeaterBool");
        internal static AutomarkerDataBool ShellwoodMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "ShellwoodMaskShard");
        internal static AutomarkerDataBool VaultsMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "VaultsMaskShard");
        internal static AutomarkerDataBool HasJournal = new([
            GoalID.ImobaJournalEntry,
            GoalID.SquirrmJournalEntry,
            GoalID.GromlingJournalEntry,
            GoalID.CraggliteJournalEntry,
            GoalID.DeepDiverJournalEntry,
            GoalID.SkullwingJournalEntry,
            GoalID.MiteMotherJournalEntry,
            GoalID.UnravelledJournalEntry,
            GoalID.ShadowChargerJournalEntry,
            GoalID.CogworkClapperJournalEntry,
            GoalID.MassiveMossgrubJournalEntry
            ], "HasJournal");
        internal static AutomarkerDataBool CogworkClapperKilled = new([GoalID.CogworkClapperJournalEntry], "CogworkClapperKilled");
        internal static AutomarkerDataBool SquirrmKilled = new([GoalID.SquirrmJournalEntry], "SquirrmKilled");
        internal static AutomarkerDataBool SkullwingKilled = new([GoalID.SkullwingJournalEntry], "SkullwingKilled");
        internal static AutomarkerDataBool CraggliteKilled = new([GoalID.CraggliteJournalEntry], "CraggliteKilled");
        internal static AutomarkerDataBool GromlingKilled = new([GoalID.GromlingJournalEntry], "GromlingKilled");
        internal static AutomarkerDataBool MiteMotherKilled = new([GoalID.MiteMotherJournalEntry], "MiteMotherKilled");
        internal static AutomarkerDataBool MassiveMossgrubKilled = new([GoalID.MassiveMossgrubJournalEntry], "MassiveMossgrubKilled");
        internal static AutomarkerDataBool DeepDiverKilled = new([GoalID.DeepDiverJournalEntry], "DeepDiverKilled");
        internal static AutomarkerDataBool UnravelledKilled = new([GoalID.UnravelledJournalEntry], "UnravelledKilled");
        internal static AutomarkerDataBool ShadowChargerKilled = new([GoalID.ShadowChargerJournalEntry], "ShadowChargerKilled");
        internal static AutomarkerDataBool ImobaKilled = new([GoalID.ImobaJournalEntry], "ImobaKilled");
        internal static AutomarkerDataBool HuntressSpokenTo = new([GoalID.TalktoStyxHuntress], "HuntressSpokenTo");
        internal static AutomarkerDataBool StyxSpokenTo = new([GoalID.TalktoStyxHuntress], "StyxSpokenTo");
        internal static AutomarkerDataBool ForgeDaughterSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "ForgeDaughterSpokenTo");
        internal static AutomarkerDataBool TwelfthArchitectSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "TwelfthArchitectSpokenTo");
        internal static AutomarkerDataBool GreymoorStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "GreymoorStyxWallBroken");
        internal static AutomarkerDataBool SinnersStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "SinnersStyxWallBroken");
        internal static AutomarkerDataBool JubilanaKeyBought = new([GoalID.BothVendorSimpleKeys], "JubilanaKeyBought");
        internal static AutomarkerDataBool PebbKeyBought = new([GoalID.BothVendorSimpleKeys], "PebbKeyBought");
        internal static AutomarkerDataBool FleaCrawLake = new([GoalID.GreymoorFleasTwoKratt], "FleaCrawLake");
        internal static AutomarkerDataBool FleaKratt = new([GoalID.GreymoorFleasTwoKratt], "FleaKratt");
        internal static AutomarkerDataBool FleaGreymoorTower = new([GoalID.GreymoorFleasTwoKratt], "FleaGreymoorTower");
        internal static AutomarkerDataBool FleaFarFieldsCage = new([GoalID.FarFieldsFleasTwo], "FleaFarFieldsCage");
        internal static AutomarkerDataBool FleaPilgrimsRest = new([GoalID.FarFieldsFleasTwo], "FleaPilgrimsRest");
        internal static AutomarkerDataBool FleaShellwood = new([GoalID.ShellwoodBellhartFleasTwo], "FleaShellwood");
        internal static AutomarkerDataBool FleaBellvein = new([GoalID.ShellwoodBellhartFleasTwo], "FleaBellvein");
        internal static AutomarkerDataBool FleaSwiftStep = new([GoalID.DeepDocksFleasThree], "FleaSwiftStep");
        internal static AutomarkerDataBool FleaDeepDocksBellway = new([GoalID.DeepDocksFleasThree], "FleaDeepDocksBellway");
        internal static AutomarkerDataBool FleaDeeperDocks = new([GoalID.DeepDocksFleasThree], "FleaDeeperDocks");
        internal static AutomarkerDataBool FleaWormways = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaWormways");
        internal static AutomarkerDataBool FleaBlastedSteps = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaBlastedSteps");
        internal static AutomarkerDataBool FleaUnderworksCauldron = new([GoalID.UnderworksFleasTwo], "FleaUnderworksCauldron");
        internal static AutomarkerDataBool FleaUnderworksWispThicket = new([GoalID.UnderworksFleasTwo], "FleaUnderworksWispThicket");
        internal static AutomarkerDataBool FleaBilewaterThieves = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaBilewaterThieves");
        internal static AutomarkerDataBool FleaHuntersMarch = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaHuntersMarch");
        internal static AutomarkerDataBool FleaSinnersRoad = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaSinnersRoad");
        internal static AutomarkerDataBool FleaVaults = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaVaults");
        internal static AutomarkerDataBool DeepDocksSpoolFragNearSpa = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeepDocksSpoolFragNearSpa");
        internal static AutomarkerDataBool DeeperDocksSpoolFrag = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeeperDocksSpoolFrag");
        internal static AutomarkerDataInt SilkSkills = new([new(){markValue = 3, goalToMark = GoalID.ThreeSilkSkills}, new(){markValue = 4, goalToMark = GoalID.FourSilkSkills}], "SilkSkills");
        internal static AutomarkerDataInt Crests = new([new(){markValue = 3, goalToMark = GoalID.ThreeNonHunterCrests}], "Crests");
        internal static AutomarkerDataInt ExtraMasks = new([new(){markValue = 1, goalToMark = GoalID.OneExtraMask}, new(){markValue = 2, goalToMark = GoalID.TwoExtraMasks}], "ExtraMasks");
        internal static AutomarkerDataInt SpoolFragments = new([new(){markValue = 2, goalToMark = GoalID.OneSpoolUpgrade}, new(){markValue = 4, goalToMark = GoalID.TwoSpoolUpgrades}, new(){markValue = 6, goalToMark = GoalID.ThreeSpoolUpgrades}], "SpoolFragments");
        internal static AutomarkerDataInt SilkHearts = new([new(){markValue = 2, goalToMark = GoalID.TwoSilkHearts}], "SilkHearts");
        internal static AutomarkerDataInt WishesCompleted = new([new(){markValue = 5, goalToMark = GoalID.CompleteFiveWishes}, new(){markValue = 7, goalToMark = GoalID.CompleteSevenWishes}], "WishesCompleted");
        internal static AutomarkerDataInt HuntWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoHuntWishes}, new(){markValue = 4, goalToMark = GoalID.FourHuntWishes}], "HuntWishes");
        internal static AutomarkerDataInt GatherWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoGatherWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeGatherWishes}], "GatherWishes");
        internal static AutomarkerDataInt WayfarerWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoWayfarerWishes}, new(){markValue = 4, goalToMark = GoalID.FourWayfarerWishes}], "WayfarerWishes");
        internal static AutomarkerDataInt DonationWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoDonationWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeDonationWishes}], "DonationWishes");
        internal static AutomarkerDataInt Silkeaters = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeSilkeaters}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveSilkeaters}], "Silkeaters");    
        internal static AutomarkerDataInt Craftmetal = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeCraftmetal}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveCraftmetal}], "Craftmetal");
        internal static AutomarkerDataInt RuneHarps = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoRuneHarps}], "RuneHarps");
        internal static AutomarkerDataInt BoneScrolls = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeBoneScrolls}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBoneScrolls}], "BoneScrolls");
        internal static AutomarkerDataInt BeastShards = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoBeastShards}, new(){markValue = 3, goalToMark = GoalID.ObtainThreeBeastShards}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBeastShards}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveBeastShards}], "BeastShards");
        internal static AutomarkerDataInt CogheartPieces = new([new(){markValue = 1, goalToMark = GoalID.OneCogheartPiece}, new(){markValue = 2, goalToMark = GoalID.TwoCogheartPieces}], "CogheartPieces");
        internal static AutomarkerDataInt MapCount = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMaps}, new(){markValue = 8, goalToMark = GoalID.ObtainEightMaps}], "MapCount");
        internal static AutomarkerDataInt CraftKitToolPouchCount = new([new(){markValue = 3, goalToMark = GoalID.ThreeToolPouchUpgradesCraftingKits}], "CraftKitToolPouchCount");
        internal static AutomarkerDataInt FleasSaved = new([new(){markValue = 8, goalToMark = GoalID.FreeEightFleas},new(){markValue = 10, goalToMark = GoalID.FreeTenFleas},new(){markValue = 12, goalToMark = GoalID.FreeTwelveFleas},new(){markValue = 14, goalToMark = GoalID.FreeFourteenFleas}], "FleasSaved");
        internal static AutomarkerDataInt CitadelFleas = new([new(){markValue = 3, goalToMark = GoalID.ThreeCitadelFleas}], "CitadelFleas");
        internal static AutomarkerDataInt MemoryLockets = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMemoryLockets}], "MemoryLockets");
        internal static AutomarkerDataInt NonPurchasedShardBundles = new([], "NonPurchasedShardBundles");
        internal static AutomarkerDataInt NonPurchasedBrokenShardBundles = new([new(){markValue = 4, goalToMark = GoalID.BreakFourShardBundlesnopurchasing}, new(){markValue = 6, goalToMark = GoalID.BreakSixShardBundlesnopurchasing}], "NonPurchasedBrokenShardBundles");
        internal static AutomarkerDataInt CurrentPurchasedRosaryNecklaces = new([], "CurrentPurchasedRosaryNecklaces");
        internal static AutomarkerDataInt NonPurchasedRosaryNecklaces = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeRosaryNecklacesnopurchasing}], "NonPurchasedRosaryNecklaces");
        internal static AutomarkerDataInt VoidMassesKilled = new([new(){markValue = 3, goalToMark = GoalID.ThreeVoidMasses},new(){markValue = 6, goalToMark = GoalID.SixVoidMasses}], "VoidMassesKilled");
        internal static AutomarkerDataInt DuoMossMothers = new([new(){markValue = 2, goalToMark = GoalID.MossMotherDuo}], "DuoMossMothers");
        internal static AutomarkerDataInt VerdaniaFountainOrbs = new([new(){markValue = 5, goalToMark = GoalID.VerdaniaFountainOrbs}], "VerdaniaFountainOrbs");
        internal static AutomarkerDataInt MementosObtained = new([new(){markValue = 2, goalToMark = GoalID.TwoMementos}], "MementosObtained");
        internal static AutomarkerDataBitmask ShakraLocations = new([new(){markValue = 5, goalToMark = GoalID.TalktoShakraatFiveLocations}], "ShakraLocations");
        internal static AutomarkerDataBitmask RelicTypesObtained = new([new(){markValue = 4, goalToMark = GoalID.ObtainFourDifferentTypesofRelic}], "RelicTypesObtained");
        internal static AutomarkerDataBitmask RelicTypesCurrentlyHeld = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeDifferentTypesofRelic}], "RelicTypesCurrentlyHeld");
    
        internal static void SetDefaultData()
        {
            foreach (AutomarkerDataBool boolData in AutomarkerData.bools)
            {
                boolData.ResetToDefault();
            }
            foreach (AutomarkerDataInt intData in AutomarkerData.ints)
            {
                intData.ResetToDefault();
            }
            foreach (AutomarkerDataBitmask bitmaskData in AutomarkerData.bitmasks)
            {
                bitmaskData.ResetFlags();
            }
            SilksongBingoModPlugin.LogInfo($"Automarker Data has been reset.");
        }

        internal static void SaveData(int saveSlotIndex)
        {
            foreach (AutomarkerDataBool dataBool in AutomarkerData.bools)
            {
                string persistentSceneName = SilksongBingoModPlugin.PersistentName;
                string persistentID = $"{saveSlotIndex}_{dataBool.Name}";
                SceneData.instance.PersistentBools.SetValue(new PersistentItemData<bool>
                {
                    SceneName = persistentSceneName,
                    ID = persistentID,
                    IsSemiPersistent = false,
                    Value = dataBool.Value
                });
            }
            foreach (AutomarkerDataInt dataInt in AutomarkerData.ints)
            {
                string persistentSceneName = SilksongBingoModPlugin.PersistentName;
                string persistentID = $"{saveSlotIndex}_{dataInt.Name}";
                SceneData.instance.PersistentInts.SetValue(new PersistentItemData<int>
                {
                    SceneName = persistentSceneName,
                    ID = persistentID,
                    IsSemiPersistent = false,
                    Value = dataInt.Value
                });
            }
            foreach (AutomarkerDataBitmask dataBitmask in AutomarkerData.bitmasks)
            {
                string persistentSceneName = SilksongBingoModPlugin.PersistentName;
                string persistentID = $"{saveSlotIndex}_{dataBitmask.Name}";
                SceneData.instance.PersistentInts.SetValue(new PersistentItemData<int>
                {
                    SceneName = persistentSceneName,
                    ID = persistentID,
                    IsSemiPersistent = false,
                    Value = (int)dataBitmask.bitmask
                });
            }
            SilksongBingoModPlugin.LogInfo($"Finished saving to saveSlot {saveSlotIndex}.");
        }

        internal static void LoadData(int saveSlotIndex)
        {
            SilksongBingoModPlugin.LogInfo($"Loading automarker data from saveSlot {saveSlotIndex}.");
            foreach (AutomarkerDataBool dataBool in AutomarkerData.bools)
            {
                string persistentSceneName = SilksongBingoModPlugin.PersistentName;
                string persistentID = $"{saveSlotIndex}_{dataBool.Name}";
                bool currentPersistentData = SceneData.instance.PersistentBools.GetValueOrDefault(persistentSceneName, persistentID);
                dataBool.Value = currentPersistentData;
            }
            foreach (AutomarkerDataInt dataInt in AutomarkerData.ints)
            {
                string persistentSceneName = SilksongBingoModPlugin.PersistentName;
                string persistentID = $"{saveSlotIndex}_{dataInt.Name}";
                int currentPersistentData = SceneData.instance.PersistentInts.GetValueOrDefault(persistentSceneName, persistentID);
                dataInt.Value = currentPersistentData;
            }
            foreach (AutomarkerDataBitmask dataBitmask in AutomarkerData.bitmasks)
            {
                string persistentSceneName = SilksongBingoModPlugin.PersistentName;
                string persistentID = $"{saveSlotIndex}_{dataBitmask.Name}";
                int currentPersistentData = SceneData.instance.PersistentInts.GetValueOrDefault(persistentSceneName, persistentID);
                dataBitmask.bitmask = (uint)currentPersistentData;
            }
        }
    }

    [Flags]
    internal enum ShakraLocationFlags
    {
        BoneBottom = 1 << 0,
        Marrow = 1 << 1,
        DeepDocks = 1 << 2,
        HuntersMarch = 1 << 3,
        FarFields = 1 << 4,
        GreymoorBell = 1 << 5,
        GreymoorDuel = 1 << 6,
        GreymoorAct3 = 1 << 7,
        Bellhart = 1 << 8,
        ShellwoodNormal = 1 << 9,
        ShellwoodAct3 = 1 << 10,
        Wormways = 1 << 11,
        BlastedSteps = 1 << 12,
        SinnersRoad = 1 << 13,
        Bilewater = 1 << 14,
        SandsOfKarak = 1 << 15,
        MountFay = 1 << 16,
        TrailsEnd = 1 << 17,
        HighHallsArena = 1 << 18
    }

    [Flags]
    internal enum RelicTypeFlags
    {
        ChoralCommandment = 1 << 0,
        WeaverEffigy = 1 << 1,
        RuneHarp = 1 << 2,
        BoneScroll = 1 << 3,
        ArcaneEgg = 1 << 4
    }

    struct AutomarkIntValue
    {
        internal int markValue;
        internal GoalID goalToMark;
    }
}
