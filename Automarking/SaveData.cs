/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;

namespace VogsBingoMod.Automarking
{
    internal static class SaveData
    {
        internal static SaveDataBool BlastedSilkeater = new([GoalID.BlastedStepsSilkeaterCraftmetal], "BlastedSilkeater");
        internal static SaveDataBool SinnersKey = new([GoalID.BothFreeSimpleKeys], "SinnersKey");
        internal static SaveDataBool KarakKey = new([GoalID.BothFreeSimpleKeys], "KarakKey");
        internal static SaveDataBool ShellwoodMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "ShellwoodMaskShard");
        internal static SaveDataBool VaultsMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "VaultsMaskShard");
        internal static SaveDataBool HasJournal = new([
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
        internal static SaveDataBool CogworkClapperKilled = new([GoalID.CogworkClapperJournalEntry], "CogworkClapperKilled");
        internal static SaveDataBool SquirrmKilled = new([GoalID.SquirrmJournalEntry], "SquirrmKilled");
        internal static SaveDataBool SkullwingKilled = new([GoalID.SkullwingJournalEntry], "SkullwingKilled");
        internal static SaveDataBool CraggliteKilled = new([GoalID.CraggliteJournalEntry], "CraggliteKilled");
        internal static SaveDataBool GromlingKilled = new([GoalID.GromlingJournalEntry], "GromlingKilled");
        internal static SaveDataBool MiteMotherKilled = new([GoalID.MiteMotherJournalEntry], "MiteMotherKilled");
        internal static SaveDataBool MassiveMossgrubKilled = new([GoalID.MassiveMossgrubJournalEntry], "MassiveMossgrubKilled");
        internal static SaveDataBool DeepDiverKilled = new([GoalID.DeepDiverJournalEntry], "DeepDiverKilled");
        internal static SaveDataBool UnravelledKilled = new([GoalID.UnravelledJournalEntry], "UnravelledKilled");
        internal static SaveDataBool ShadowChargerKilled = new([GoalID.ShadowChargerJournalEntry], "ShadowChargerKilled");
        internal static SaveDataBool ImobaKilled = new([GoalID.ImobaJournalEntry], "ImobaKilled");
        internal static SaveDataBool HuntressSpokenTo = new([GoalID.TalktoStyxHuntress], "HuntressSpokenTo");
        internal static SaveDataBool StyxSpokenTo = new([GoalID.TalktoStyxHuntress], "StyxSpokenTo");
        internal static SaveDataBool ForgeDaughterSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "ForgeDaughterSpokenTo");
        internal static SaveDataBool TwelfthArchitectSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "TwelfthArchitectSpokenTo");
        internal static SaveDataBool GreymoorStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "GreymoorStyxWallBroken");
        internal static SaveDataBool SinnersStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "SinnersStyxWallBroken");
        internal static SaveDataBool JubilanaKeyBought = new([GoalID.BothVendorSimpleKeys], "JubilanaKeyBought");
        internal static SaveDataBool PebbKeyBought = new([GoalID.BothVendorSimpleKeys], "PebbKeyBought");
        internal static SaveDataBool FleaCrawLake = new([GoalID.GreymoorFleasTwoKratt], "FleaCrawLake");
        internal static SaveDataBool FleaKratt = new([GoalID.GreymoorFleasTwoKratt], "FleaKratt");
        internal static SaveDataBool FleaGreymoorTower = new([GoalID.GreymoorFleasTwoKratt], "FleaGreymoorTower");
        internal static SaveDataBool FleaFarFieldsCage = new([GoalID.FarFieldsFleasTwo], "FleaFarFieldsCage");
        internal static SaveDataBool FleaPilgrimsRest = new([GoalID.FarFieldsFleasTwo], "FleaPilgrimsRest");
        internal static SaveDataBool FleaShellwood = new([GoalID.ShellwoodBellhartFleasTwo], "FleaShellwood");
        internal static SaveDataBool FleaBellvein = new([GoalID.ShellwoodBellhartFleasTwo], "FleaBellvein");
        internal static SaveDataBool FleaSwiftStep = new([GoalID.DeepDocksFleasThree], "FleaSwiftStep");
        internal static SaveDataBool FleaDeepDocksBellway = new([GoalID.DeepDocksFleasThree], "FleaDeepDocksBellway");
        internal static SaveDataBool FleaDeeperDocks = new([GoalID.DeepDocksFleasThree], "FleaDeeperDocks");
        internal static SaveDataBool FleaWormways = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaWormways");
        internal static SaveDataBool FleaBlastedSteps = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaBlastedSteps");
        internal static SaveDataBool FleaUnderworksCauldron = new([GoalID.UnderworksFleasTwo], "FleaUnderworksCauldron");
        internal static SaveDataBool FleaUnderworksWispThicket = new([GoalID.UnderworksFleasTwo], "FleaUnderworksWispThicket");
        internal static SaveDataBool FleaBilewaterThieves = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaBilewaterThieves");
        internal static SaveDataBool FleaHuntersMarch = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaHuntersMarch");
        internal static SaveDataBool FleaSinnersRoad = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaSinnersRoad");
        internal static SaveDataBool FleaVaults = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaVaults");
        internal static SaveDataBool DeepDocksSpoolFragNearSpa = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeepDocksSpoolFragNearSpa");
        internal static SaveDataBool DeeperDocksSpoolFrag = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeeperDocksSpoolFrag");
        internal static SaveDataBool UnderworksArenaSpoolFrag = new([GoalID.UnderworksSpoolFragmentsTwo], "UnderworksArenaSpoolFrag");
        internal static SaveDataBool UnderworksLibrary_11bSpoolFrag = new([GoalID.UnderworksSpoolFragmentsTwo], "UnderworksLibrary_11bSpoolFrag");
        internal static SaveDataBool GillySpokenTo = new([GoalID.TalktoGrishkinandGilly], "GillySpokenTo");
        internal static SaveDataBool GrishkinSpokenTo = new([GoalID.TalktoGrishkinandGilly], "GrishkinSpokenTo");
        internal static SaveDataInt Crests = new([new(){markValue = 3, goalToMark = GoalID.ThreeNonHunterCrests}], "Crests");
        internal static SaveDataInt ExtraMasks = new([new(){markValue = 1, goalToMark = GoalID.OneExtraMask}, new(){markValue = 2, goalToMark = GoalID.TwoExtraMasks}], "ExtraMasks");
        internal static SaveDataInt SpoolFragments = new([new(){markValue = 2, goalToMark = GoalID.OneSpoolUpgrade}, new(){markValue = 4, goalToMark = GoalID.TwoSpoolUpgrades}, new(){markValue = 6, goalToMark = GoalID.ThreeSpoolUpgrades}], "SpoolFragments");
        internal static SaveDataInt SilkHearts = new([new(){markValue = 2, goalToMark = GoalID.TwoSilkHearts}], "SilkHearts");
        internal static SaveDataInt WishesCompleted = new([new(){markValue = 5, goalToMark = GoalID.CompleteFiveWishes}, new(){markValue = 7, goalToMark = GoalID.CompleteSevenWishes}], "WishesCompleted");
        internal static SaveDataInt HuntWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoHuntWishes}, new(){markValue = 4, goalToMark = GoalID.FourHuntWishes}], "HuntWishes");
        internal static SaveDataInt GatherWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoGatherWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeGatherWishes}], "GatherWishes");
        internal static SaveDataInt WayfarerWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoWayfarerWishes}, new(){markValue = 4, goalToMark = GoalID.FourWayfarerWishes}], "WayfarerWishes");
        internal static SaveDataInt DonationWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoDonationWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeDonationWishes}], "DonationWishes");
        internal static SaveDataInt Silkeaters = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeSilkeaters}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveSilkeaters}], "Silkeaters");    
        internal static SaveDataInt Craftmetal = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeCraftmetal}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveCraftmetal}], "Craftmetal");
        internal static SaveDataInt RuneHarps = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoRuneHarps}], "RuneHarps");
        internal static SaveDataInt BoneScrolls = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeBoneScrolls}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBoneScrolls}], "BoneScrolls");
        internal static SaveDataInt WeaverEffigies = new([new(){markValue = 2, goalToMark = GoalID.TwoWeaverEffigies}], "WeaverEffigies");
        internal static SaveDataInt PsalmCylinders = new([new(){markValue = 3, goalToMark = GoalID.ThreePsalmCylinders}], "PsalmCylinders");
        internal static SaveDataInt BeastShards = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoBeastShards}, new(){markValue = 3, goalToMark = GoalID.ObtainThreeBeastShards}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBeastShards}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveBeastShards}], "BeastShards");
        internal static SaveDataInt CogheartPieces = new([new(){markValue = 1, goalToMark = GoalID.OneCogheartPiece}, new(){markValue = 2, goalToMark = GoalID.TwoCogheartPieces}], "CogheartPieces");
        internal static SaveDataInt MapCount = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMaps}, new(){markValue = 8, goalToMark = GoalID.ObtainEightMaps}], "MapCount");
        internal static SaveDataInt CraftKitToolPouchCount = new([new(){markValue = 3, goalToMark = GoalID.ThreeToolPouchUpgradesCraftingKits}], "CraftKitToolPouchCount");
        internal static SaveDataInt FleasSaved = new([new(){markValue = 8, goalToMark = GoalID.FreeEightFleas},new(){markValue = 10, goalToMark = GoalID.FreeTenFleas},new(){markValue = 12, goalToMark = GoalID.FreeTwelveFleas},new(){markValue = 14, goalToMark = GoalID.FreeFourteenFleas}], "FleasSaved");
        internal static SaveDataInt CitadelFleas = new([new(){markValue = 3, goalToMark = GoalID.ThreeCitadelFleas}], "CitadelFleas");
        internal static SaveDataInt MemoryLockets = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMemoryLockets}], "MemoryLockets");
        internal static SaveDataInt NonPurchasedShardBundles = new([], "NonPurchasedShardBundles");
        internal static SaveDataInt NonPurchasedBrokenShardBundles = new([new(){markValue = 4, goalToMark = GoalID.BreakFourShardBundlesnopurchasing}, new(){markValue = 6, goalToMark = GoalID.BreakSixShardBundlesnopurchasing}], "NonPurchasedBrokenShardBundles");
        internal static SaveDataInt CurrentPurchasedRosaryNecklaces = new([], "CurrentPurchasedRosaryNecklaces");
        internal static SaveDataInt NonPurchasedRosaryNecklaces = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeRosaryNecklacesnopurchasing}], "NonPurchasedRosaryNecklaces");
        internal static SaveDataInt VoidMassesKilled = new([new(){markValue = 3, goalToMark = GoalID.ThreeVoidMasses},new(){markValue = 6, goalToMark = GoalID.SixVoidMasses}], "VoidMassesKilled");
        internal static SaveDataInt DuoMossMothers = new([new(){markValue = 2, goalToMark = GoalID.MossMotherDuo}], "DuoMossMothers");
        internal static SaveDataInt VerdaniaFountainOrbs = new([new(){markValue = 5, goalToMark = GoalID.VerdaniaFountainOrbs}], "VerdaniaFountainOrbs");
        internal static SaveDataInt MementosObtained = new([new(){markValue = 2, goalToMark = GoalID.TwoMementos}], "MementosObtained");
        internal static SaveDataInt WoodWaspNestsBroken = new([new(){markValue = 3, goalToMark = GoalID.BreakThreeWoodWaspNests}], "WoodWaspNestsBroken");
        internal static SaveDataBitmask ShakraLocations = new([new(){markValue = 5, goalToMark = GoalID.TalktoShakraatFiveLocations}], "ShakraLocations");
        internal static SaveDataBitmask RelicTypesObtained = new([new(){markValue = 4, goalToMark = GoalID.ObtainFourDifferentTypesofRelic}], "RelicTypesObtained");
        internal static SaveDataBitmask RelicTypesCurrentlyHeld = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeDifferentTypesofRelic}], "RelicTypesCurrentlyHeld");
        internal static AutomarkRosaryStringHandler automarkRosaryStringHandler = new AutomarkRosaryStringHandler();
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

    internal struct AutomarkIntValue
    {
        internal int markValue;
        internal GoalID goalToMark;
    }
}
