/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using System;
using System.Collections.Generic;

namespace SilksongBingoMod.Automarking
{
    internal class GoalHelper
    {
        internal const string SpriteNameCouriersSwag = "courier_items_destroyed_0003_Layer-1";
        internal const string SpriteNameCouriersRasher = "courier_items_destroyed_0000_Layer-4";
        internal const string SpriteNameQueensEgg = "courier_items_destroyed_0001_Layer-3";
        internal const string SpriteNameLiquidLacquer = "courier_items_destroyed_0002_Layer-2";
        internal const string SpriteNameBellhomeKey = "belltown_house_key";
        internal const string SpriteNameSilkeater = "Silk_Grub_idle0000";
        internal const string SpriteNameCraftmetal = "Hornet_Tool_Metal";
        internal const string SpriteNameApostateKey = "I_slab_key_brass";
        internal const string SpriteNameRedQuill = "I_quill_red";
        internal const string SpriteNameCrustnut = "_0007_quest_coral_nut";
        internal const string SpriteNameMossberryStew = "Mossberry_Stew";
        internal const string SpriteNameVintageNectar = "vintage_nectar_item";
        internal const string SpriteNameTwistedBud = "mandrake_icon0000";
        internal const string SpriteNameSimpleKey = "I_token_of_faith";
        internal const string SpriteNameRuneHarp = "Hornet_icon_0002_R_weaver_record";
        internal const string SpriteNameBoneScroll = "Hornet_icon_0002_R_bone_record";
        internal const string SpriteNameChoralCommandment = "Hornet_icon_0000_R_seal_chit";
        internal const string SpriteNameWeaverEffigy = "Hornet_icon_0005_R_saint_locket";
        internal const string SpriteNameBeastShard = "Icon_Beast_Shard";
        internal const string SpriteNameCogheartPiece0 = "cog_heart__0001_left";
        internal const string SpriteNameCogheartPiece1 = "cog_heart__0002_right";
        internal const string SpriteNameCogheartPiece2 = "cog_heart__0003_back";
        internal const string SpriteNameCradleMap = "Shop_map_icon__0012_cradle";
        internal const string SpriteNameWeavenestMap = "Shop_map_icon__weavehome";
        internal const string SpriteNameVerdaniaMap = "Shop_map_icon__0003_clover";
        internal const string SpriteNameArcaneEgg = "Hornet_icon_0004_R_ancient_egg";
        internal const string SpriteNameSurfaceMemento = "surface_memento";
        internal const string SpriteNameGreyMemento = "grey_warrior_memento";
        internal const string SpriteNameRosaryNecklace = "I_rosary_necklace_0000_3";
        internal const string SpriteNameHeavyRosaryNecklace = "I_rosary_necklace_0000_3_old";
        internal const string SpriteNamePaleRosaryNecklace = "I_rosary_necklace_0001_2";
        internal const string SpriteNameRosaryString = "I_rosary_necklace_0002_1";
        internal const string SpriteNameFrayedString = "I_rosary_necklace_0002_1_frayed";
        internal const string SpriteNameToolPouch = "Inv_tool_pouch_upgrade_02";
        internal const string SpriteNameCraftingKit = "icon_tool_kit_upgrade";
        internal const string SpriteNameMemoryLocket = "Charm_Notch";
        internal const string SpriteNameShardBundle = "Hornet_Tool_Metal_Pouch";
        internal const string SpriteNameHerosMemento = "garmond_memento";
        internal const string SpriteNameHuntersMemento = "hunter_nuu_memento_seal";
        internal const string SpriteNameCrawMemento = "craw_court_memento";
        internal const string SpriteNameSprintmasterMemento = "sprintmaster_memento";
        internal const string SpriteNameSethMemento = "seth_memento";
        internal const string EnemyNameCogworkClapper = "Song Automaton Ball";
        internal const string EnemyNameSquirrm = "Coral Judge Child";
        internal const string EnemyNameSkullwing = "Bone Goomba Bounce Fly";
        internal const string EnemyNameCragglite = "Small Crab";
        internal const string EnemyNameGromling = "Crypt Worm";
        internal const string EnemyNameMiteMother = "Gnat Giant";
        internal const string EnemyNameMassiveMossgrub = "MossBone Crawler Fat";
        internal const string EnemyNameDeepDiver = "Dock Charger";
        internal const string EnemyNameUnravelled = "Conductor Boss";
        internal const string EnemyNameShadowCharger = "Abyss Crawler Large";
        internal const string EnemyNameImoba = "Spike Lazy Flyer";
        internal const string EnemyNameCraggler = "Roof Crab";
        internal const string EnemyNameMoorwing = "Vampire Gnat";
        internal const string EnemyNameSavageBeastfly = "Bone Flyer Giant";
        internal const string EnemyNameGreatConchfly = "Coral Conch Driller Giant";
        internal const string EnemyNameMossMother = "Mossbone Mother";
        internal const string EnemyNameRhinogrund = "Rhino";
        internal const string EnemyNameFourthChorus = "Song Golem";
        internal const string EnemyNameCogworkDancers = "Clockwork Dancer";
        internal const string EnemyNameLastJudge = "Last Judge";
        internal const string EnemyNamePhantom = "Phantom";
        internal const string EnemyNameTrobbio = "Trobbio";
        internal const string EnemyNameChefLugoli = "Roachkeeper Chef";
        internal const string EnemyNameFatherOfTheFlame = "Wisp Pyre Effigy";
        internal const string EnemyNameForebrothers = "Dock Guard Thrower";
        internal const string EnemyNameCovetousPilgrim = "Rosary Pilgrim";
        internal const string EnemyNameSisterSplinter = "Splinter Queen";
        internal const string EnemyNameVoltvyrm = "Zap Core Enemy";
        internal const string EnemyNameSkullTyrant = "Skull King";
        internal const string EnemyNameBigFlea = "Giant Flea";
        internal const string EnemyNameSeth = "Seth";
        internal const string EnemyNamePaleStag = "Cloverstag White";
        internal const string EnemyNameTormentedTrobbio = "Tormented Trobbio";
        internal const string EnemyNameVoidMass = "Black Thread Core";
        internal const string NPCNameShakra = "Mapper NPC";
        internal const string NPCNameShakraSitting = "Mapper Sit NPC";
        internal const string NPCNameShakraDupe = "Mapper NPC (1)";
        internal const string NPCNameShakraRest = "Mapper Rest NPC";
        internal const string NPCNameShakraTrailsEnd = "Mapper Master NPC";
        internal const string NPCNameShakraDuel = "Mapper Spar NPC";
        internal const string NPCNameShakraAid = "Shakra Aid NPC";
        internal const string NPCNameShakraHHAWin = "Victory NPC";
        internal const string NPCNameHuntress = "Huntress";
        internal const string NPCNameStyx = "Grub Farmer NPC";
        internal const string NPCNameForgeDaughter = "Shop Prompt";
        internal const string NPCNameTwelfthArchitect = "Architect NPC";
        internal const string NPCNameLoam = "Understore Large Worker";
        internal const string NPCNameMaskMaker = "Peak Mask Maker";
        internal const string NPCNameFlickSurvivorsCamp = "Fixer Sitting Bone_10";
        internal const string NPCNameMrMushroom = "Mr Mushroom NPC";
        internal const string NPCNameShermaSpa = "Sherma Citadel Spa NPC";
        internal const string NPCNamePlinneySave = "Plinney Outside";
        internal const string ObjectNameVentrica = "tube_toll_machine";
        internal const string ObjectNameMaskShard = "Heart Piece";
        internal const string ObjectNameMaskShardDupe = "Heart Piece (1)";
        internal const string ObjectNameSilkSpool = "Silk Spool";
        internal const string ObjectNameSilkHeart = "Silk Heart";
        internal const string ObjectNameFrayedString = "Rosary_Set_Frayed";
        internal const string ObjectNameRosaryString = "Rosary_Set_Small";
        internal const string ObjectNameRosaryNecklace = "Rosary_Set_Medium";
        internal const string ObjectNameHeavyNecklace = "Rosary_Set_Large";
        internal const string ObjectNamePaleNecklace = "Rosary_Set_Huge_White";
        internal const string ObjectNameShardBundle = "Shard Pouch";
        internal const string ShopItemNameGrindleSpoolFrag = "Grindle Spool Piece";
        internal const string ItemNamePollenHeart = "Flower Heart";
        internal const string ItemNameConjoinedHeart = "Clover Heart";
        internal const string ItemNameHuntersHeart = "Hunter Heart";
        internal const string ItemNameEncrustedHeart = "";
        static string[] idToName = GetEmbeddedGoals();
        static Dictionary<string, int> nameToID = GenerateNameToIDs();
        internal static int NameToID(string goalName)
        {
            try{
                return nameToID[goalName];
            } catch (Exception)
            {
                SilksongBingoModPlugin.LogError($"Could not find a goal ID for goal: \"{goalName}\"");
                return 0;
            }
        }

        static Dictionary<string, int> GenerateNameToIDs()
        {
            Dictionary<string, int> nameToID = new Dictionary<string, int>();
            for (int i = 0; i < idToName.Length; i++)
            {
                nameToID.Add(idToName[i].ToLower(), i);
            }
            return nameToID;
        }

        static string[] GetEmbeddedGoals()
        {
            string json;
            try{
                json = Automarker.GetGoalsJson();
            } catch (Exception e)
            {
                SilksongBingoModPlugin.LogError(e);
                json = "";
            }
            return JsonHelper.GetGoalNamesFromList(json);
        }
    }
}
