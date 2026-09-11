/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;

namespace VogsBingoMod.Automarking
{
    public class SaveData
    {
        internal static SaveData instance => VogsBingoModPlugin.instance.SaveData;
        public SaveDataBool BlastedSilkeater = new([GoalID.BlastedStepsSilkeaterCraftmetal], "BlastedSilkeater");
        public SaveDataBool SinnersKey = new([GoalID.BothFreeSimpleKeys], "SinnersKey");
        public SaveDataBool KarakKey = new([GoalID.BothFreeSimpleKeys], "KarakKey");
        public SaveDataBool ShellwoodMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "ShellwoodMaskShard");
        public SaveDataBool VaultsMaskShard = new([GoalID.ShellwoodVaultsMaskShards], "VaultsMaskShard");
        public SaveDataBool HasJournal = new([
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
        public SaveDataBool CogworkClapperKilled = new([GoalID.CogworkClapperJournalEntry], "CogworkClapperKilled");
        public SaveDataBool SquirrmKilled = new([GoalID.SquirrmJournalEntry], "SquirrmKilled");
        public SaveDataBool SkullwingKilled = new([GoalID.SkullwingJournalEntry], "SkullwingKilled");
        public SaveDataBool CraggliteKilled = new([GoalID.CraggliteJournalEntry], "CraggliteKilled");
        public SaveDataBool GromlingKilled = new([GoalID.GromlingJournalEntry], "GromlingKilled");
        public SaveDataBool MiteMotherKilled = new([GoalID.MiteMotherJournalEntry], "MiteMotherKilled");
        public SaveDataBool MassiveMossgrubKilled = new([GoalID.MassiveMossgrubJournalEntry], "MassiveMossgrubKilled");
        public SaveDataBool DeepDiverKilled = new([GoalID.DeepDiverJournalEntry], "DeepDiverKilled");
        public SaveDataBool UnravelledKilled = new([GoalID.UnravelledJournalEntry], "UnravelledKilled");
        public SaveDataBool ShadowChargerKilled = new([GoalID.ShadowChargerJournalEntry], "ShadowChargerKilled");
        public SaveDataBool ImobaKilled = new([GoalID.ImobaJournalEntry], "ImobaKilled");
        public SaveDataBool HuntressSpokenTo = new([GoalID.TalktoStyxHuntress], "HuntressSpokenTo");
        public SaveDataBool StyxSpokenTo = new([GoalID.TalktoStyxHuntress], "StyxSpokenTo");
        public SaveDataBool ForgeDaughterSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "ForgeDaughterSpokenTo");
        public SaveDataBool TwelfthArchitectSpokenTo = new([GoalID.TalktoForgeDaughterandTwelfthArchitect], "TwelfthArchitectSpokenTo");
        public SaveDataBool GreymoorStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "GreymoorStyxWallBroken");
        public SaveDataBool SinnersStyxWallBroken = new([GoalID.BreakBothofStyxsOneways], "SinnersStyxWallBroken");
        public SaveDataBool JubilanaKeyBought = new([GoalID.BothVendorSimpleKeys], "JubilanaKeyBought");
        public SaveDataBool PebbKeyBought = new([GoalID.BothVendorSimpleKeys], "PebbKeyBought");
        public SaveDataBool FleaCrawLake = new([GoalID.GreymoorFleasTwoKratt], "FleaCrawLake");
        public SaveDataBool FleaKratt = new([GoalID.GreymoorFleasTwoKratt], "FleaKratt");
        public SaveDataBool FleaGreymoorTower = new([GoalID.GreymoorFleasTwoKratt], "FleaGreymoorTower");
        public SaveDataBool FleaFarFieldsCage = new([GoalID.FarFieldsFleasTwo], "FleaFarFieldsCage");
        public SaveDataBool FleaPilgrimsRest = new([GoalID.FarFieldsFleasTwo], "FleaPilgrimsRest");
        public SaveDataBool FleaShellwood = new([GoalID.ShellwoodBellhartFleasTwo], "FleaShellwood");
        public SaveDataBool FleaBellvein = new([GoalID.ShellwoodBellhartFleasTwo], "FleaBellvein");
        public SaveDataBool FleaSwiftStep = new([GoalID.DeepDocksFleasThree], "FleaSwiftStep");
        public SaveDataBool FleaDeepDocksBellway = new([GoalID.DeepDocksFleasThree], "FleaDeepDocksBellway");
        public SaveDataBool FleaDeeperDocks = new([GoalID.DeepDocksFleasThree], "FleaDeeperDocks");
        public SaveDataBool FleaWormways = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaWormways");
        public SaveDataBool FleaBlastedSteps = new([GoalID.WormwaysBlastedStepsFleasTwo], "FleaBlastedSteps");
        public SaveDataBool FleaUnderworksCauldron = new([GoalID.UnderworksFleasTwo], "FleaUnderworksCauldron");
        public SaveDataBool FleaUnderworksWispThicket = new([GoalID.UnderworksFleasTwo], "FleaUnderworksWispThicket");
        public SaveDataBool FleaBilewaterThieves = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaBilewaterThieves");
        public SaveDataBool FleaHuntersMarch = new([GoalID.LowerBilewaterHuntersMarchFleasTwo], "FleaHuntersMarch");
        public SaveDataBool FleaSinnersRoad = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaSinnersRoad");
        public SaveDataBool FleaVaults = new([GoalID.SinnersRoadVaultsFleasTwo], "FleaVaults");
        public SaveDataBool DeepDocksSpoolFragNearSpa = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeepDocksSpoolFragNearSpa");
        public SaveDataBool DeeperDocksSpoolFrag = new([GoalID.DeepDocksSpoolFragmentsTwo], "DeeperDocksSpoolFrag");
        public SaveDataBool UnderworksArenaSpoolFrag = new([GoalID.UnderworksSpoolFragmentsTwo], "UnderworksArenaSpoolFrag");
        public SaveDataBool UnderworksLibrary_11bSpoolFrag = new([GoalID.UnderworksSpoolFragmentsTwo], "UnderworksLibrary_11bSpoolFrag");
        public SaveDataBool GillySpokenTo = new([GoalID.TalktoGrishkinandGilly], "GillySpokenTo");
        public SaveDataBool GrishkinSpokenTo = new([GoalID.TalktoGrishkinandGilly], "GrishkinSpokenTo");
        public SaveDataInt Crests = new([new(){markValue = 3, goalToMark = GoalID.ThreeNonHunterCrests}], "Crests");
        public SaveDataInt ExtraMasks = new([new(){markValue = 1, goalToMark = GoalID.OneExtraMask}, new(){markValue = 2, goalToMark = GoalID.TwoExtraMasks}], "ExtraMasks");
        public SaveDataInt SpoolFragments = new([new(){markValue = 2, goalToMark = GoalID.OneSpoolUpgrade}, new(){markValue = 4, goalToMark = GoalID.TwoSpoolUpgrades}, new(){markValue = 6, goalToMark = GoalID.ThreeSpoolUpgrades}], "SpoolFragments");
        public SaveDataInt SilkHearts = new([new(){markValue = 2, goalToMark = GoalID.TwoSilkHearts}], "SilkHearts");
        public SaveDataInt WishesCompleted = new([new(){markValue = 5, goalToMark = GoalID.CompleteFiveWishes}, new(){markValue = 7, goalToMark = GoalID.CompleteSevenWishes}], "WishesCompleted");
        public SaveDataInt HuntWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoHuntWishes}, new(){markValue = 4, goalToMark = GoalID.FourHuntWishes}], "HuntWishes");
        public SaveDataInt GatherWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoGatherWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeGatherWishes}], "GatherWishes");
        public SaveDataInt WayfarerWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoWayfarerWishes}, new(){markValue = 4, goalToMark = GoalID.FourWayfarerWishes}], "WayfarerWishes");
        public SaveDataInt DonationWishes = new([new(){markValue = 2, goalToMark = GoalID.TwoDonationWishes}, new(){markValue = 3, goalToMark = GoalID.ThreeDonationWishes}], "DonationWishes");
        public SaveDataInt Silkeaters = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeSilkeaters}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveSilkeaters}], "Silkeaters");    
        public SaveDataInt Craftmetal = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeCraftmetal}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveCraftmetal}], "Craftmetal");
        public SaveDataInt RuneHarps = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoRuneHarps}], "RuneHarps");
        public SaveDataInt BoneScrolls = new([new(){markValue = 3, goalToMark = GoalID.ObtainThreeBoneScrolls}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBoneScrolls}], "BoneScrolls");
        public SaveDataInt WeaverEffigies = new([new(){markValue = 2, goalToMark = GoalID.TwoWeaverEffigies}], "WeaverEffigies");
        public SaveDataInt PsalmCylinders = new([new(){markValue = 3, goalToMark = GoalID.ThreePsalmCylinders}], "PsalmCylinders");
        public SaveDataInt BeastShards = new([new(){markValue = 2, goalToMark = GoalID.ObtainTwoBeastShards}, new(){markValue = 3, goalToMark = GoalID.ObtainThreeBeastShards}, new(){markValue = 4, goalToMark = GoalID.ObtainFourBeastShards}, new(){markValue = 5, goalToMark = GoalID.ObtainFiveBeastShards}], "BeastShards");
        public SaveDataInt CogheartPieces = new([new(){markValue = 1, goalToMark = GoalID.OneCogheartPiece}, new(){markValue = 2, goalToMark = GoalID.TwoCogheartPieces}], "CogheartPieces");
        public SaveDataInt MapCount = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMaps}, new(){markValue = 8, goalToMark = GoalID.ObtainEightMaps}], "MapCount");
        public SaveDataInt CraftKitToolPouchCount = new([new(){markValue = 3, goalToMark = GoalID.ThreeToolPouchUpgradesCraftingKits}], "CraftKitToolPouchCount");
        public SaveDataInt FleasSaved = new([new(){markValue = 8, goalToMark = GoalID.FreeEightFleas},new(){markValue = 10, goalToMark = GoalID.FreeTenFleas},new(){markValue = 12, goalToMark = GoalID.FreeTwelveFleas},new(){markValue = 14, goalToMark = GoalID.FreeFourteenFleas}], "FleasSaved");
        public SaveDataInt CitadelFleas = new([new(){markValue = 3, goalToMark = GoalID.ThreeCitadelFleas}], "CitadelFleas");
        public SaveDataInt MemoryLockets = new([new(){markValue = 5, goalToMark = GoalID.ObtainFiveMemoryLockets}], "MemoryLockets");
        public SaveDataInt NonPurchasedShardBundles = new([], "NonPurchasedShardBundles");
        public SaveDataInt NonPurchasedBrokenShardBundles = new([new(){markValue = 4, goalToMark = GoalID.BreakFourShardBundlesnopurchasing}, new(){markValue = 6, goalToMark = GoalID.BreakSixShardBundlesnopurchasing}], "NonPurchasedBrokenShardBundles");
        public SaveDataInt CurrentPurchasedRosaryNecklaces = new([], "CurrentPurchasedRosaryNecklaces");
        public SaveDataInt NonPurchasedRosaryNecklaces = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeRosaryNecklacesnopurchasing}], "NonPurchasedRosaryNecklaces");
        public SaveDataInt VoidMassesKilled = new([new(){markValue = 3, goalToMark = GoalID.ThreeVoidMasses},new(){markValue = 6, goalToMark = GoalID.SixVoidMasses}], "VoidMassesKilled");
        public SaveDataInt DuoMossMothers = new([new(){markValue = 2, goalToMark = GoalID.MossMotherDuo}], "DuoMossMothers");
        public SaveDataInt VerdaniaFountainOrbs = new([new(){markValue = 5, goalToMark = GoalID.VerdaniaFountainOrbs}], "VerdaniaFountainOrbs");
        public SaveDataInt MementosObtained = new([new(){markValue = 2, goalToMark = GoalID.TwoMementos}], "MementosObtained");
        public SaveDataInt WoodWaspNestsBroken = new([new(){markValue = 3, goalToMark = GoalID.BreakThreeWoodWaspNests}], "WoodWaspNestsBroken");
        public SaveDataBitmask ShakraLocations = new([new(){markValue = 5, goalToMark = GoalID.TalktoShakraatFiveLocations}], "ShakraLocations");
        public SaveDataBitmask RelicTypesObtained = new([new(){markValue = 4, goalToMark = GoalID.ObtainFourDifferentTypesofRelic}], "RelicTypesObtained");
        public SaveDataBitmask RelicTypesCurrentlyHeld = new([new(){markValue = 3, goalToMark = GoalID.HaveThreeDifferentTypesofRelic}], "RelicTypesCurrentlyHeld");
        public AutomarkRosaryStringHandler automarkRosaryStringHandler = new AutomarkRosaryStringHandler();
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

    public struct AutomarkIntValue
    {
        public int markValue;
        public GoalID goalToMark;
    }
}
