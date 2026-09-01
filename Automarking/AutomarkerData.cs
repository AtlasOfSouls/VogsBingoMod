/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections.Generic;

namespace VogsBingoMod.Automarking
{
    public class SaveData
    {
        internal SaveDataBool StraightPin = new([GoalID.StraightThreefoldandLongPin], "StraightPin"); 
        internal SaveDataBool Longpin = new([GoalID.StraightThreefoldandLongPin], "Longpin");
        internal SaveDataBool ThreefoldPin = new([GoalID.StraightThreefoldandLongPin], "ThreefoldPin");
        internal SaveDataBool DeadBugsPurse = new([GoalID.DeadBugsPurseaSilkeater], "DeadBugsPurse");
        internal SaveDataBool BarbedBracelet = new([GoalID.BarbedBraceletFracturedMask], "BarbedBracelet");
        internal SaveDataBool FracturedMask = new([GoalID.BarbedBraceletFracturedMask], "FracturedMask");
        internal SaveDataBool MagnetiteDice = new([GoalID.MagnetiteDiceMagnetiteBrooch], "MagnetiteDice");
        internal SaveDataBool MagnetiteBrooch = new([GoalID.CompassPendantBrooch, GoalID.MagnetiteDiceMagnetiteBrooch], "MagnetiteBrooch");
        internal SaveDataBool Compass = new([GoalID.CompassPendantBrooch], "Compass");
        internal SaveDataBool ShardPendant = new([GoalID.CompassPendantBrooch], "ShardPendant");
        internal SaveDataBool WardingBell = new([GoalID.WardingBellClawMirror, GoalID.WardingBellSawtoothCirclet], "WardingBell");
        internal SaveDataBool ClawMirror = new([GoalID.WardingBellClawMirror], "ClawMirror");
        internal SaveDataBool SawtoothCirclet = new([GoalID.WardingBellSawtoothCirclet], "SawtoothCirclet");
        internal SaveDataBool MagmaBell = new([GoalID.MagmaBellCurveclaw], "MagmaBell");
        internal SaveDataBool Curveclaw = new([GoalID.MagmaBellCurveclaw], "Curveclaw");
        internal SaveDataBool WeightedBelt = new([GoalID.WeightedBeltAscendantsGrip], "WeightedBelt");
        internal SaveDataBool AscendantsGrip = new([GoalID.WeightedBeltAscendantsGrip], "AscendantsGrip");
        internal SaveDataBool Scuttlebrace = new([GoalID.ScuttlebraceSilkspeed], "Scuttlebrace");
        internal SaveDataBool SilkspeedAnklets = new([GoalID.ScuttlebraceSilkspeed], "SilkspeedAnklets");
        internal SaveDataBool Pimpillo = new([GoalID.PimpilloVoltvessels], "Pimpillo");
        internal SaveDataBool Voltvessels = new([GoalID.PimpilloVoltvessels], "Voltvessels");
        internal SaveDataBool PollipPouch = new([GoalID.PollipPouchStingShard], "PollipPouch");
        internal SaveDataBool StingShard = new([GoalID.PollipPouchStingShard], "StingShard");
        internal SaveDataBool Weavelight = new([GoalID.WeavelightInjectorBand], "Weavelight");
        internal SaveDataBool InjectorBand = new([GoalID.WeavelightInjectorBand], "InjectorBand");
        internal SaveDataBool BlastedSilkeater = new([GoalID.BlastedStepsSilkeaterCraftmetal], "BlastedSilkeater");
        internal SaveDataBool BlastedCraftmetal = new([GoalID.BlastedStepsSilkeaterCraftmetal], "BlastedCraftmetal");
        internal SaveDataBool SinnersKey = new([GoalID.BothFreeSimpleKeys], "SinnersKey");
        internal SaveDataBool KarakKey = new([GoalID.BothFreeSimpleKeys], "KarakKey");
        internal SaveDataBool SilkeaterBool = new([GoalID.DeadBugsPurseaSilkeater], "SilkeaterBool");
        internal SaveDataBool ShellwoodMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "ShellwoodMaskShard");
        internal SaveDataBool VaultsMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "VaultsMaskShard");
        internal SaveDataBool HasJournal = new([
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
        internal SaveDataBool CogworkClapperKilled = new([GoalID.CogworkClapperJournalEntry], "CogworkClapperKilled");
        internal SaveDataBool SquirrmKilled = new([GoalID.SquirrmJournalEntry], "SquirrmKilled");
        internal SaveDataBool SkullwingKilled = new([GoalID.SkullwingJournalEntry], "SkullwingKilled");
        internal SaveDataBool CraggliteKilled = new([GoalID.CraggliteJournalEntry], "CraggliteKilled");
        internal SaveDataBool GromlingKilled = new([GoalID.GromlingJournalEntry], "GromlingKilled");
        internal SaveDataBool MiteMotherKilled = new([GoalID.MiteMotherJournalEntry], "MiteMotherKilled");
        internal SaveDataBool MassiveMossgrubKilled = new([GoalID.MassiveMossgrubJournalEntry], "MassiveMossgrubKilled");
        internal SaveDataBool DeepDiverKilled = new([GoalID.DeepDiverJournalEntry], "DeepDiverKilled");
        internal SaveDataBool UnravelledKilled = new([GoalID.UnravelledJournalEntry], "UnravelledKilled");
        internal SaveDataBool ShadowChargerKilled = new([GoalID.ShadowChargerJournalEntry], "ShadowChargerKilled");
        internal SaveDataBool ImobaKilled = new([GoalID.ImobaJournalEntry], "ImobaKilled");
        internal SaveDataBool HuntressSpokenTo = new([GoalID.TalktoStyxHuntress], "HuntressSpokenTo");
        internal SaveDataBool StyxSpokenTo = new([GoalID.TalktoStyxHuntress], "StyxSpokenTo");
        internal SaveDataBool ForgeDaughterSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "ForgeDaughterSpokenTo");
        internal SaveDataBool TwelfthArchitectSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "TwelfthArchitectSpokenTo");
        internal SaveDataBool GreymoorStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "GreymoorStyxWallBroken");
        internal SaveDataBool SinnersStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "SinnersStyxWallBroken");
        internal SaveDataBool JubilanaKeyBought = new([GoalID.BothVendorSimpleKeys], "JubilanaKeyBought");
        internal SaveDataBool PebbKeyBought = new([GoalID.BothVendorSimpleKeys], "PebbKeyBought");
        internal SaveDataBool FleaCrawLake = new([GoalID.GreymoorFleasTwoKratt], "FleaCrawLake");
        internal SaveDataBool FleaKratt = new([GoalID.GreymoorFleasTwoKratt], "FleaKratt");
        internal SaveDataBool FleaGreymoorTower = new([GoalID.GreymoorFleasTwoKratt], "FleaGreymoorTower");
        internal SaveDataBool FleaFarFieldsCage = new([GoalID.FarFieldsFleasTwo], "FleaFarFieldsCage");
        internal SaveDataBool FleaPilgrimsRest = new([GoalID.FarFieldsFleasTwo], "FleaPilgrimsRest");
        internal SaveDataBool FleaShellwood = new([GoalID.ShellwoodBellhartFleasTwo], "FleaShellwood");
        internal SaveDataBool FleaBellvein = new([GoalID.ShellwoodBellhartFleasTwo], "FleaBellvein");
        internal SaveDataBool FleaSwiftStep = new([GoalID.DeepDocksFleasThree], "FleaSwiftStep");
        internal SaveDataBool FleaDeepDocksBellway = new([GoalID.DeepDocksFleasThree], "FleaDeepDocksBellway");
        internal SaveDataBool FleaDeeperDocks = new([GoalID.DeepDocksFleasThree], "FleaDeeperDocks");
        internal SaveDataBool FleaWormways = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaWormways");
        internal SaveDataBool FleaBlastedSteps = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaBlastedSteps");
        internal SaveDataBool FleaUnderworksCauldron = new([GoalID.UnderworksFleasTwo], "FleaUnderworksCauldron");
        internal SaveDataBool FleaUnderworksWispThicket = new([GoalID.UnderworksFleasTwo], "FleaUnderworksWispThicket");
        internal SaveDataBool FleaBilewaterThieves = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaBilewaterThieves");
        internal SaveDataBool FleaHuntersMarch = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaHuntersMarch");
        internal SaveDataBool FleaSinnersRoad = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaSinnersRoad");
        internal SaveDataBool FleaVaults = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaVaults");
        internal SaveDataBool DeepDocksSpoolFragNearSpa = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeepDocksSpoolFragNearSpa");
        internal SaveDataBool DeeperDocksSpoolFrag = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeeperDocksSpoolFrag");
        internal SaveDataInt SilkSkills = new([new(){markValue = 3, goalToMark = GoalID.ThreeSilkSkills}, new(){markValue = 4, goalToMark = GoalID.FourSilkSkills}], "SilkSkills");
        internal SaveDataInt Crests = new([new(){markValue = 3, goalToMark = GoalID.ThreeNonHunterCrests}], "Crests");
        internal SaveDataInt ExtraMasks = new([new(){markValue = 1, goalToMark = GoalID.OneExtraMask}, new(){markValue = 2, goalToMark = GoalID.TwoExtraMasks}], "ExtraMasks");
        internal SaveDataInt SpoolFragments = new([new(){markValue = 2, goalToMark = GoalID.OneSpoolUpgrade}, new(){markValue = 4, goalToMark = GoalID.TwoSpoolUpgrades}, new(){markValue = 6, goalToMark = GoalID.ThreeSpoolUpgrades}], "SpoolFragments");
        internal SaveDataInt SilkHearts = new([new(){markValue = 2, goalToMark = GoalID.TwoSilkHearts}], "SilkHearts");
        internal SaveDataInt WishesCompleted = new([new(){markValue = 5, goalToMark = GoalID.CompleteFiveWishes}, new(){markValue = 7, goalToMark = GoalID.CompleteSevenWishes}], "WishesCompleted");
        internal SaveDataInt HuntWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoHuntWishes}, new(){markValue = 4, goalToMark = GoalID.FourHuntWishes}], "HuntWishes");
        internal SaveDataInt GatherWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoGatherWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeGatherWishes}], "GatherWishes");
        internal SaveDataInt WayfarerWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoWayfarerWishes}, new(){markValue = 4, goalToMark = GoalID.FourWayfarerWishes}], "WayfarerWishes");
        internal SaveDataInt DonationWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoDonationWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeDonationWishes}], "DonationWishes");
        internal SaveDataInt Silkeaters = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeSilkeaters}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveSilkeaters}], "Silkeaters");    
        internal SaveDataInt Craftmetal = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeCraftmetal}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveCraftmetal}], "Craftmetal");
        internal SaveDataInt RuneHarps = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoRuneHarps}], "RuneHarps");
        internal SaveDataInt BoneScrolls = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeBoneScrolls}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBoneScrolls}], "BoneScrolls");
        internal SaveDataInt BeastShards = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoBeastShards}, new(){markValue = 3, goalToMark = GoalID.ObtainThreeBeastShards}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBeastShards}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveBeastShards}], "BeastShards");
        internal SaveDataInt CogheartPieces = new([new(){markValue = 1, goalToMark = GoalID.OneCogheartPiece}, new(){markValue = 2, goalToMark = GoalID.TwoCogheartPieces}], "CogheartPieces");
        internal SaveDataInt MapCount = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMaps}, new(){markValue = 8, goalToMark = GoalID.ObtainEightMaps}], "MapCount");
        internal SaveDataInt CraftKitToolPouchCount = new([new(){markValue = 3, goalToMark = GoalID.ThreeToolPouchUpgradesCraftingKits}], "CraftKitToolPouchCount");
        internal SaveDataInt FleasSaved = new([new(){markValue = 8, goalToMark = GoalID.FreeEightFleas},new(){markValue = 10, goalToMark = GoalID.FreeTenFleas},new(){markValue = 12, goalToMark = GoalID.FreeTwelveFleas},new(){markValue = 14, goalToMark = GoalID.FreeFourteenFleas}], "FleasSaved");
        internal SaveDataInt CitadelFleas = new([new(){markValue = 3, goalToMark = GoalID.ThreeCitadelFleas}], "CitadelFleas");
        internal SaveDataInt MemoryLockets = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMemoryLockets}], "MemoryLockets");
        internal SaveDataInt NonPurchasedShardBundles = new([], "NonPurchasedShardBundles");
        internal SaveDataInt NonPurchasedBrokenShardBundles = new([new(){markValue = 4, goalToMark = GoalID.BreakFourShardBundlesnopurchasing}, new(){markValue = 6, goalToMark = GoalID.BreakSixShardBundlesnopurchasing}], "NonPurchasedBrokenShardBundles");
        internal SaveDataInt CurrentPurchasedRosaryNecklaces = new([], "CurrentPurchasedRosaryNecklaces");
        internal SaveDataInt NonPurchasedRosaryNecklaces = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeRosaryNecklacesnopurchasing}], "NonPurchasedRosaryNecklaces");
        internal SaveDataInt VoidMassesKilled = new([new(){markValue = 3, goalToMark = GoalID.ThreeVoidMasses},new(){markValue = 6, goalToMark = GoalID.SixVoidMasses}], "VoidMassesKilled");
        internal SaveDataInt DuoMossMothers = new([new(){markValue = 2, goalToMark = GoalID.MossMotherDuo}], "DuoMossMothers");
        internal SaveDataInt VerdaniaFountainOrbs = new([new(){markValue = 5, goalToMark = GoalID.VerdaniaFountainOrbs}], "VerdaniaFountainOrbs");
        internal SaveDataInt MementosObtained = new([new(){markValue = 2, goalToMark = GoalID.TwoMementos}], "MementosObtained");
        internal SaveDataBitmask ShakraLocations = new([new(){markValue = 5, goalToMark = GoalID.TalktoShakraatFiveLocations}], "ShakraLocations");
        internal SaveDataBitmask RelicTypesObtained = new([new(){markValue = 4, goalToMark = GoalID.ObtainFourDifferentTypesofRelic}], "RelicTypesObtained");
        internal SaveDataBitmask RelicTypesCurrentlyHeld = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeDifferentTypesofRelic}], "RelicTypesCurrentlyHeld");
        // internal static List<SaveDataBool> bools = new List<SaveDataBool>();
        // internal static List<SaveDataInt> ints = new List<SaveDataInt>();
        // internal static List<SaveDataBitmask> bitmasks = new List<SaveDataBitmask>();   
    
        // internal static void SetDefaultData()
        // {
        //     foreach (SaveDataBool boolData in SaveData.bools)
        //     {
        //         boolData.ResetToDefault();
        //         VogsBingoModPlugin.LogInfo($"data reset: {boolData.Name}, {boolData.Value}");
        //     }
        //     foreach (SaveDataInt intData in SaveData.ints)
        //     {
        //         intData.ResetToDefault();
        //         VogsBingoModPlugin.LogInfo($"data reset: {intData.Name}, {intData.Value}");
        //     }
        //     foreach (SaveDataBitmask bitmaskData in SaveData.bitmasks)
        //     {
        //         bitmaskData.ResetFlags();
        //         VogsBingoModPlugin.LogInfo($"data reset: {bitmaskData.Name}, {bitmaskData.bitmask}");
        //     }
        //     VogsBingoModPlugin.LogInfo($"Automarker Data has been reset.");
        // }

        // internal static void SaveData(int saveSlotIndex)
        // {
        //     foreach (SaveDataBool dataBool in SaveData.bools)
        //     {
        //         string persistentSceneName = VogsBingoModPlugin.PersistentName;
        //         string persistentID = $"{saveSlotIndex}_{dataBool.Name}";
        //         SceneData.instance.PersistentBools.SetValue(new PersistentItemData<bool>
        //         {
        //             SceneName = persistentSceneName,
        //             ID = persistentID,
        //             IsSemiPersistent = false,
        //             Value = dataBool.Value
        //         });
        //         VogsBingoModPlugin.LogInfo($"Saving data: {persistentID}, {dataBool.Value}");
        //     }
        //     foreach (SaveDataInt dataInt in SaveData.ints)
        //     {
        //         string persistentSceneName = VogsBingoModPlugin.PersistentName;
        //         string persistentID = $"{saveSlotIndex}_{dataInt.Name}";
        //         SceneData.instance.PersistentInts.SetValue(new PersistentItemData<int>
        //         {
        //             SceneName = persistentSceneName,
        //             ID = persistentID,
        //             IsSemiPersistent = false,
        //             Value = dataInt.Value
        //         });
        //         VogsBingoModPlugin.LogInfo($"Saving data: {persistentID}, {dataInt.Value}");
        //     }
        //     foreach (SaveDataBitmask dataBitmask in SaveData.bitmasks)
        //     {
        //         string persistentSceneName = VogsBingoModPlugin.PersistentName;
        //         string persistentID = $"{saveSlotIndex}_{dataBitmask.Name}";
        //         SceneData.instance.PersistentInts.SetValue(new PersistentItemData<int>
        //         {
        //             SceneName = persistentSceneName,
        //             ID = persistentID,
        //             IsSemiPersistent = false,
        //             Value = (int)dataBitmask.bitmask
        //         });
        //         VogsBingoModPlugin.LogInfo($"Saving data: {persistentID}, {(int)dataBitmask.bitmask}");
        //     }
        //     VogsBingoModPlugin.LogInfo($"Finished saving to saveSlot {saveSlotIndex}.");
        // }

        // internal static void LoadData(int saveSlotIndex)
        // {
        //     VogsBingoModPlugin.LogInfo($"Loading automarker data from saveSlot {saveSlotIndex}.");
        //     foreach (SaveDataBool dataBool in SaveData.bools)
        //     {
        //         string persistentSceneName = VogsBingoModPlugin.PersistentName;
        //         string persistentID = $"{saveSlotIndex}_{dataBool.Name}";
        //         bool currentPersistentData = SceneData.instance.PersistentBools.GetValueOrDefault(persistentSceneName, persistentID);
        //         dataBool.Value = currentPersistentData;
        //         VogsBingoModPlugin.LogInfo($"Loading data {persistentID}, {currentPersistentData}, {dataBool.Value}");
        //     }
        //     foreach (SaveDataInt dataInt in SaveData.ints)
        //     {
        //         string persistentSceneName = VogsBingoModPlugin.PersistentName;
        //         string persistentID = $"{saveSlotIndex}_{dataInt.Name}";
        //         int currentPersistentData = SceneData.instance.PersistentInts.GetValueOrDefault(persistentSceneName, persistentID);
        //         dataInt.Value = currentPersistentData;
        //         VogsBingoModPlugin.LogInfo($"Loading data {persistentID}, {currentPersistentData}, {dataInt.Value}");
        //     }
        //     foreach (SaveDataBitmask dataBitmask in SaveData.bitmasks)
        //     {
        //         string persistentSceneName = VogsBingoModPlugin.PersistentName;
        //         string persistentID = $"{saveSlotIndex}_{dataBitmask.Name}";
        //         int currentPersistentData = SceneData.instance.PersistentInts.GetValueOrDefault(persistentSceneName, persistentID);
        //         dataBitmask.bitmask = (uint)currentPersistentData;
        //         VogsBingoModPlugin.LogInfo($"Loading data {persistentID}, {currentPersistentData}, {dataBitmask.bitmask}");
        //     }
        // }
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
