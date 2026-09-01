/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;
using System;
using GlobalEnums;

namespace VogsBingoMod.Automarking
{
    [HarmonyPatch]
    internal class AutomarkPatches
    {

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ToolItemManager),nameof(ToolItemManager.SetEquippedCrest))]
        private static void CrestUnlockPatch(string crestId)
        {
            switch (crestId)
            {
                case "Wanderer":
                    Automarker.MarkIfAvailable(GoalID.WanderersCrest);
                    break;
                case "Warrior":
                    Automarker.MarkIfAvailable(GoalID.BeastsCrest);
                    break;
                case "Hunter_v2":
                    Automarker.MarkIfAvailable(GoalID.EvolvedHuntersCrest);
                    break;
                case "Reaper":
                    Automarker.MarkIfAvailable(GoalID.ReapersCrest);
                    break;
                case "Witch":
                    Automarker.MarkIfAvailable(GoalID.WitchCrest);
                    break;
                case "Spell":
                    Automarker.MarkIfAvailable(GoalID.ShamanCrest);
                    break;
                default:
                    break;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(FullQuestBase),nameof(FullQuestBase.TryEndQuest))]
        private static void QuestCompletionPatch(bool __result, FullQuestBase __instance)
        {
            if (!__result || !__instance.IsCompleted)
            {
                return;
            }
            switch (__instance.name)
            {
                case "Crow Feathers":
                    Automarker.MarkIfAvailable(GoalID.CrawbugClearing);
                    break;
                case "Rock Rollers":
                    Automarker.MarkIfAvailable(GoalID.VolatileFlintbeetles);
                    break;
                case "Save City Merchant":
                    Automarker.MarkIfAvailable(GoalID.SaveJubilana);
                    break;
                case "A Pinsmiths Tools":
                    Automarker.MarkIfAvailable(GoalID.UpgradeNeedleTwice);
                    break;
                case "Wood Witch Curse":
                    Automarker.MarkIfAvailable(GoalID.GetCursed);
                    break;
                default:
                    break;
            }
            switch (__instance.QuestType.name.ToLower())
            {
                case "hunt": case "grand hunt":
                    VogsBingoModPlugin.instance.SaveData.HuntWishes.Value++;
                    VogsBingoModPlugin.instance.SaveData.WishesCompleted.Value++;
                    break;
                case "gather":
                VogsBingoModPlugin.instance.SaveData.GatherWishes.Value++;
                    VogsBingoModPlugin.instance.SaveData.WishesCompleted.Value++;
                    break;
                case "wayfarer":
                    VogsBingoModPlugin.instance.SaveData.WayfarerWishes.Value++;
                    VogsBingoModPlugin.instance.SaveData.WishesCompleted.Value++;
                    break;
                case "donate":
                    VogsBingoModPlugin.instance.SaveData.DonationWishes.Value++;
                    VogsBingoModPlugin.instance.SaveData.WishesCompleted.Value++;
                    break;
                case "delivery": case "learn": case "sprint": case "witness": case "steel":
                    VogsBingoModPlugin.instance.SaveData.WishesCompleted.Value++;
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CollectableUIMsg),nameof(CollectableUIMsg.Spawn),[typeof(ICollectableUIMsgItem),typeof(Color),typeof(CollectableUIMsg),typeof(bool)])]
        static void UIMsgPatch(ICollectableUIMsgItem item)
        {
            switch (item.GetUIMsgSprite().name)
            {
                case GoalHelper.SpriteNameBellhomeKey:
                    Automarker.MarkIfAvailable(GoalID.BellhomeKey);
                    break;
                case GoalHelper.SpriteNameSilkeater:
                    VogsBingoModPlugin.instance.SaveData.Silkeaters.Value++;
                    VogsBingoModPlugin.instance.SaveData.SilkeaterBool.Value = true;
                    if (IsScene("Coral_37"))
                        VogsBingoModPlugin.instance.SaveData.BlastedSilkeater.Value = true;
                    break;
                case GoalHelper.SpriteNameCraftmetal:
                    VogsBingoModPlugin.instance.SaveData.Craftmetal.Value++;
                    switch (GetSceneName())
                    {
                        case "Coral_32":
                            VogsBingoModPlugin.instance.SaveData.BlastedCraftmetal.Value = true;
                            break;
                        case "Aqueduct_05":
                            Automarker.MarkIfAvailable(GoalID.PaleLakeCraftmetal);
                            break;
                        default:
                            break;
                    }
                    break;
                case GoalHelper.SpriteNameApostateKey:
                    Automarker.MarkIfAvailable(GoalID.ApostateKey);
                    break;
                case GoalHelper.SpriteNameRedQuill:
                    Automarker.MarkIfAvailable(GoalID.RedQuill);
                    break;
                case GoalHelper.SpriteNameCrustnut:
                    Automarker.MarkIfAvailable(GoalID.Crustnut);
                    break;
                case GoalHelper.SpriteNameMossberryStew:
                    Automarker.MarkIfAvailable(GoalID.MossberrySoup);
                    break;
                case GoalHelper.SpriteNameVintageNectar:
                    Automarker.MarkIfAvailable(GoalID.VintageNectar);
                    break;
                case GoalHelper.SpriteNameTwistedBud:
                    Automarker.MarkIfAvailable(GoalID.TwistedBud);
                    break;
                case GoalHelper.SpriteNameSimpleKey:
                    switch (GetSceneName())
                    {
                        case "Dust_06":
                            VogsBingoModPlugin.instance.SaveData.SinnersKey.Value = true;
                            break;
                        case "Bellshrine_Coral":
                            VogsBingoModPlugin.instance.SaveData.KarakKey.Value = true;
                            break;
                    }
                    break;
                case GoalHelper.SpriteNameRuneHarp:
                    VogsBingoModPlugin.instance.SaveData.RuneHarps.Value++;
                    VogsBingoModPlugin.instance.SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.RuneHarp);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.RuneHarp);
                    break;
                case GoalHelper.SpriteNameBoneScroll:
                    VogsBingoModPlugin.instance.SaveData.BoneScrolls.Value++;
                    VogsBingoModPlugin.instance.SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.BoneScroll);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.BoneScroll);
                    break;
                case GoalHelper.SpriteNameWeaverEffigy:
                    VogsBingoModPlugin.instance.SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.WeaverEffigy);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.WeaverEffigy);
                    break;
                case GoalHelper.SpriteNameChoralCommandment:
                    VogsBingoModPlugin.instance.SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                    break;
                case GoalHelper.SpriteNameBeastShard:
                    VogsBingoModPlugin.instance.SaveData.BeastShards.Value++;
                    break;
                case GoalHelper.SpriteNameCogheartPiece0: case GoalHelper.SpriteNameCogheartPiece1: case GoalHelper.SpriteNameCogheartPiece2: 
                    VogsBingoModPlugin.instance.SaveData.CogheartPieces.Value++;
                    break;
                case GoalHelper.SpriteNameCradleMap:
                    Automarker.MarkIfAvailable(GoalID.CradleMap);
                    break;
                case GoalHelper.SpriteNameWeavenestMap when IsScene("Abyss_12"):
                    Automarker.MarkIfAvailable(GoalID.AbyssMap);
                    break;
                case GoalHelper.SpriteNameVerdaniaMap:
                    Automarker.MarkIfAvailable(GoalID.VerdaniaMap);
                    break;
                case GoalHelper.SpriteNameArcaneEgg:
                    Automarker.MarkIfAvailable(GoalID.OneArcaneEgg);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.ArcaneEgg);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.ArcaneEgg);
                    break;
                case GoalHelper.SpriteNameSurfaceMemento:
                    Automarker.MarkIfAvailable(GoalID.SurfaceMemento);
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameGreyMemento:
                    Automarker.MarkIfAvailable(GoalID.GreyMemento);
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameToolPouch:
                    if (PlayerData.instance.pinGalleriesCompleted >= 1)
                        Automarker.MarkIfAvailable(GoalID.WinaLoddieMinigame);
                    VogsBingoModPlugin.instance.SaveData.CraftKitToolPouchCount.Value++;
                    Automarker.MarkIfAvailable(GoalID.OneToolPouchUpgrade);
                    break;
                case GoalHelper.SpriteNameCraftingKit:
                    VogsBingoModPlugin.instance.SaveData.CraftKitToolPouchCount.Value++;
                    Automarker.MarkIfAvailable(GoalID.OneCraftingKit);
                    break;
                case GoalHelper.SpriteNameMemoryLocket:
                    VogsBingoModPlugin.instance.SaveData.MemoryLockets.Value++;
                    break;
                case GoalHelper.SpriteNameShardBundle:
                    VogsBingoModPlugin.instance.SaveData.NonPurchasedShardBundles.Value++;
                    break;
                case GoalHelper.SpriteNameRosaryNecklace: case GoalHelper.SpriteNameHeavyRosaryNecklace: case GoalHelper.SpriteNamePaleRosaryNecklace:
                    VogsBingoModPlugin.instance.SaveData.NonPurchasedRosaryNecklaces.Value++;
                    break;
                case GoalHelper.SpriteNameRosaryString:
                    if (IsScene("Greymoor_01") || IsScene("Shellwood_08c") || IsScene("Hang_06_bank"))
                    {
                        AutomarkRosaryStringHandler.AddPurchasedString();
                    } else
                    {
                        AutomarkRosaryStringHandler.AddNonPurchasedString();
                    }
                    break;
                case GoalHelper.SpriteNameFrayedString:
                    AutomarkRosaryStringHandler.AddFrayedString();
                    break;
                case GoalHelper.SpriteNameHerosMemento:
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameCrawMemento:
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameHuntersMemento:
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameSprintmasterMemento:
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameSethMemento:
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(InventoryItemCollectable),nameof(InventoryItemCollectable.PlayConsumeFinalShake))]
        static void ConsumeCollectablePatch(InventoryItemCollectable __instance)
        {
            switch(__instance.name)
            {
                case GoalHelper.ObjectNameRosaryNecklace:
                    if (VogsBingoModPlugin.instance.SaveData.CurrentPurchasedRosaryNecklaces > 0)
                    {
                        VogsBingoModPlugin.instance.SaveData.CurrentPurchasedRosaryNecklaces.Value--;
                    } else
                    {
                        VogsBingoModPlugin.instance.SaveData.NonPurchasedRosaryNecklaces.Value--;
                    }
                    break;
                case GoalHelper.ObjectNameHeavyNecklace: case GoalHelper.ObjectNamePaleNecklace:
                    VogsBingoModPlugin.instance.SaveData.NonPurchasedRosaryNecklaces.Value--;
                    break;
                case GoalHelper.ObjectNameShardBundle:
                    if (VogsBingoModPlugin.instance.SaveData.NonPurchasedShardBundles > VogsBingoModPlugin.instance.SaveData.NonPurchasedBrokenShardBundles)
                    {
                        VogsBingoModPlugin.instance.SaveData.NonPurchasedBrokenShardBundles.Value++;
                    }
                    break;
                case GoalHelper.ObjectNameRosaryString:
                    AutomarkRosaryStringHandler.BreakString();
                    break;
                case GoalHelper.ObjectNameFrayedString:
                    AutomarkRosaryStringHandler.BreakFrayedString();
                    break;
                default:
                    break;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerDataIntAdd),nameof(PlayerDataIntAdd.OnEnter))]
        static void MossberryPatch(PlayerDataIntAdd __instance)
        {
            if (PlayerData.instance.druidMossBerriesSold > 0 && IsScene("Mosstown_02c"))
            {
                Automarker.MarkIfAvailable(GoalID.SellaMossberry);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CreateUIMsgGetItem),nameof(CreateUIMsgGetItem.OnEnter))]
        static void UIItemMsgPatch(CreateUIMsgGetItem __instance)
        {
            switch (GetSceneName())
            {
                case "Room_Pinstress":
                    Automarker.MarkIfAvailable(GoalID.NeedleStrike);
                    break;
                case "Peak_08b":
                    Automarker.MarkIfAvailable(GoalID.FaydownCloak);
                    break;
                case "Halfway_01":
                    VogsBingoModPlugin.instance.SaveData.HasJournal.Value = true;
                    break;
                case "Bellway_Centipede_Arena":
                    Automarker.MarkIfAvailable(GoalID.BeastlingCall);
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ItemReceptacle),nameof(ItemReceptacle.AcceptedPrompt))]
        static void AcceptPromptPatch()
        {
            switch (GetSceneName())
            {
                case "Room_Forge":
                    Automarker.MarkIfAvailable(GoalID.UseaSimpleKeyinDeepDocks);
                    break;
                case "Dust_02":
                    Automarker.MarkIfAvailable(GoalID.FreeGreenPrince);
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EnemyJournalManager),nameof(EnemyJournalManager.RecordKill),[typeof(EnemyJournalRecord), typeof(bool),typeof(bool)])]
        static void KillRecordPatch(EnemyJournalRecord journalRecord)
        {
            switch (journalRecord.name)
            {
                case GoalHelper.EnemyNameCogworkClapper:
                    VogsBingoModPlugin.instance.SaveData.CogworkClapperKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameSkullwing:
                    VogsBingoModPlugin.instance.SaveData.SkullwingKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameCragglite:
                    VogsBingoModPlugin.instance.SaveData.CraggliteKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameGromling:
                    VogsBingoModPlugin.instance.SaveData.GromlingKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameMiteMother:
                    VogsBingoModPlugin.instance.SaveData.MiteMotherKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameMassiveMossgrub:
                    VogsBingoModPlugin.instance.SaveData.MassiveMossgrubKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameDeepDiver:
                    VogsBingoModPlugin.instance.SaveData.DeepDiverKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameUnravelled:
                    VogsBingoModPlugin.instance.SaveData.UnravelledKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameShadowCharger:
                    VogsBingoModPlugin.instance.SaveData.ShadowChargerKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameImoba:
                    VogsBingoModPlugin.instance.SaveData.ImobaKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameSquirrm:
                    VogsBingoModPlugin.instance.SaveData.SquirrmKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameGreatConchfly:
                    switch (GetSceneName())
                    {
                        case "Coral_27":
                            Automarker.MarkIfAvailable(GoalID.RagingConchflySandsofKarak);
                            break;
                        case "Coral_11":
                            Automarker.MarkIfAvailable(GoalID.GreatConchfliesBlastedSteps);
                            break;
                        default:
                            break;
                    }
                    break;
                case GoalHelper.EnemyNameMossMother:
                    if (IsScene("Weave_03"))
                    {
                        VogsBingoModPlugin.instance.SaveData.DuoMossMothers.Value++;
                    }
                    if (PlayerData.instance.act3_wokeUp)
                    {
                        Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    }
                    break;
                case GoalHelper.EnemyNameFourthChorus:
                    Automarker.MarkIfAvailable(GoalID.FourthChorus);
                    break;
                case GoalHelper.EnemyNameCraggler:
                    Automarker.MarkIfAvailable(GoalID.Craggler);
                    break;
                case GoalHelper.EnemyNameMoorwing:
                    Automarker.MarkIfAvailable(GoalID.Moorwing);
                    if (PlayerData.instance.act3_wokeUp)
                    {
                        Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    }
                    break;
                case GoalHelper.EnemyNameSavageBeastfly:
                    Automarker.MarkIfAvailable(GoalID.SavageBeastfly);
                    if (PlayerData.instance.act3_wokeUp)
                    {
                        Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    }
                    break;
                case GoalHelper.EnemyNameRhinogrund:
                    Automarker.MarkIfAvailable(GoalID.DefeataRhinogrund);
                    break;
                case GoalHelper.EnemyNameCogworkDancers:
                    Automarker.MarkIfAvailable(GoalID.CogworkDancers);
                    break;
                case GoalHelper.EnemyNameLastJudge:
                    Automarker.MarkIfAvailable(GoalID.LastJudge);
                    break;
                case GoalHelper.EnemyNamePhantom:
                    Automarker.MarkIfAvailable(GoalID.Phantom);
                    break;
                case GoalHelper.EnemyNameTrobbio:
                    Automarker.MarkIfAvailable(GoalID.Trobbio);
                    break;
                case GoalHelper.EnemyNameChefLugoli:
                    Automarker.MarkIfAvailable(GoalID.DisgracedChefLugoli);
                    if (PlayerData.instance.act3_wokeUp)
                    {
                        Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    }
                    break;
                case GoalHelper.EnemyNameFatherOfTheFlame:
                    Automarker.MarkIfAvailable(GoalID.FatherOfTheFlame);
                    break;
                case GoalHelper.EnemyNameForebrothers:
                    Automarker.MarkIfAvailable(GoalID.ForebrothersSignisGron);
                    break;
                case GoalHelper.EnemyNameCovetousPilgrim:
                    Automarker.MarkIfAvailable(GoalID.DefeataCovetousPilgrim);
                    break;
                case GoalHelper.EnemyNameSisterSplinter:
                    Automarker.MarkIfAvailable(GoalID.SisterSplinter);
                    break;
                case GoalHelper.EnemyNameVoltvyrm:
                    Automarker.MarkIfAvailable(GoalID.Voltvyrm);
                    break;
                case GoalHelper.EnemyNameSkullTyrant:
                    Automarker.MarkIfAvailable(GoalID.SkullTyrant);
                    if (PlayerData.instance.act3_wokeUp)
                    {
                        Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    }
                    break;
                case GoalHelper.EnemyNameBigFlea:
                    Automarker.MarkIfAvailable(GoalID.BeatBigFlea);
                    Automarker.UpdateFleas();
                    break;
                case GoalHelper.EnemyNameSeth:
                    Automarker.MarkIfAvailable(GoalID.FightSeth);
                    break;
                case GoalHelper.EnemyNamePaleStag:
                    Automarker.MarkIfAvailable(GoalID.PaleStag);
                    break;
                case GoalHelper.EnemyNameTormentedTrobbio:
                    Automarker.MarkIfAvailable(GoalID.TormentedTrobbio);
                    break;
                case GoalHelper.EnemyNameVoidMass:
                    VogsBingoModPlugin.instance.SaveData.VoidMassesKilled.Value++;
                    break;
                case "Garmond" when PlayerData.instance.act3_wokeUp:
                    Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    break;
                case "Abyss Mass":
                    Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    break;
                case "Lost Lace":
                    Automarker.MarkIfAvailable(GoalID.DefeataBlackthreadedBoss);
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CompleteJournalRecordV2),nameof(CompleteJournalRecordV2.OnEnter))]
        static void JournalFullCompletePatch()
        {
            if (IsScene("Hang_14"))
            {
                Automarker.MarkIfAvailable(GoalID.DisabletheClawmaidens);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PlayMakerNPC),nameof(PlayMakerNPC.OnStartingDialogue))]
        static void NPCDialogueStartPatch(PlayMakerNPC __instance)
        {
            switch (__instance.name)
            {
                case GoalHelper.NPCNameShakra: case GoalHelper.NPCNameShakraSitting: case GoalHelper.NPCNameShakraDupe: case GoalHelper.NPCNameShakraRest:
                    switch (GetSceneName())
                    {
                        case "Bonetown":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.BoneBottom);
                            break;
                        case "Bone_04":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Marrow);
                            break;
                        case "Bone_East_01":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.DeepDocks);
                            break;
                        case "Ant_04_mid": case "Ant_20":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.HuntersMarch);
                            break;
                        case "Bone_East_21":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.FarFields);
                            break;
                        case "Greymoor_02":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.GreymoorBell);
                            break;
                        case "Greymoor_08" when PlayerData.instance.mapperLocationAct3 == 3:
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.GreymoorAct3);
                            break;
                        case "Belltown":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Bellhart);
                            break;
                        case "Shellwood_16":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.ShellwoodNormal);
                            break;
                        case "Shellwood_01" when PlayerData.instance.mapperLocationAct3 == 1:
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.ShellwoodAct3);
                            break;
                        case "Crawl_01":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Wormways);
                            break;
                        case "Coral_12":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.BlastedSteps);
                            break;
                        case "Dust_10":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.SinnersRoad);
                            break;
                        case "Peak_02":
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.MountFay);
                            break;
                        case "Coral_40":
                            Automarker.MarkIfAvailable(GoalID.TalktoShakrainSandsofKarak);
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.SandsOfKarak);
                            break;
                        case "Shadow_23":
                            Automarker.MarkIfAvailable(GoalID.TalktoShakrainBilewater);
                            VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Bilewater);
                            break;
                        default:
                            break;
                    }
                    break;
                case GoalHelper.NPCNameShakraDuel:
                    VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.GreymoorDuel);
                    break;
                case GoalHelper.NPCNameShakraTrailsEnd:
                    VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.TrailsEnd);
                    break;
                case GoalHelper.NPCNameShakraAid: case GoalHelper.NPCNameShakraHHAWin:
                    VogsBingoModPlugin.instance.SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.HighHallsArena);
                    break;
                case GoalHelper.NPCNameHuntress:
                    VogsBingoModPlugin.instance.SaveData.HuntressSpokenTo.Value = true;
                    Automarker.MarkIfAvailable(GoalID.TalktoHuntress);
                    break;
                case GoalHelper.NPCNameStyx:
                    VogsBingoModPlugin.instance.SaveData.StyxSpokenTo.Value = true;
                    break;
                case GoalHelper.NPCNameForgeDaughter when IsScene("Room_Forge"):
                    VogsBingoModPlugin.instance.SaveData.ForgeDaughterSpokenTo.Value = true;
                    break;
                case GoalHelper.NPCNameTwelfthArchitect:
                    VogsBingoModPlugin.instance.SaveData.TwelfthArchitectSpokenTo.Value = true;
                    break;
                case GoalHelper.NPCNameLoam:
                    Automarker.MarkIfAvailable(GoalID.TalktoLoam);
                    break;
                case GoalHelper.NPCNameMaskMaker:
                    Automarker.MarkIfAvailable(GoalID.TalktoMaskMaker);
                    break;
                case GoalHelper.NPCNameFlickSurvivorsCamp:
                    Automarker.MarkIfAvailable(GoalID.TalktoFlickatSurvivorsCamp);
                    break;
                case GoalHelper.NPCNameMrMushroom:
                    Automarker.MarkIfAvailable(GoalID.TalktoMrMushroom);
                    break;
                case GoalHelper.NPCNameShermaSpa:
                    Automarker.MarkIfAvailable(GoalID.MeetShermaintheSpa);
                    break;
                case GoalHelper.NPCNamePlinneySave:
                    Automarker.MarkIfAvailable(GoalID.SavePinmasterPlinney);
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SetCollider),nameof(SetCollider.OnEnter))]
        static void SetColliderPatch(SetCollider __instance)
        {
            if (!IsScene("Dust_11"))
            {
                return;
            }
            switch (__instance.Fsm.GetOwnerDefaultTarget(__instance.gameObject).name)
            {
                case "One Way Wall (1)":
                    VogsBingoModPlugin.instance.SaveData.GreymoorStyxWallBroken.Value = true;
                    break;
                case "One Way Wall (2)":
                    VogsBingoModPlugin.instance.SaveData.SinnersStyxWallBroken.Value = true;
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ShopItem),nameof(ShopItem.SetPurchased))]
        static void ShopItemPurchasePatch(ShopItem __instance)
        {
            if (IsScene("Coral_42") && !__instance.Item.name.Equals("Rosary_Set_Small"))
            {
                Automarker.MarkIfAvailable(GoalID.BuyFromGrindleNoString);
            }
            switch (__instance.name)
            {
                case "Bonebottom Faith Token":
                    VogsBingoModPlugin.instance.SaveData.PebbKeyBought.Value = true;
                    break;
                case "City Merchant Simple Key":
                    VogsBingoModPlugin.instance.SaveData.JubilanaKeyBought.Value = true;
                    break;
                case "Belltown Spool Segment":
                    Automarker.MarkIfAvailable(GoalID.FreysSpoolFragment);
                    break;
                default:
                    break;
            }
            if (__instance.Item != null){
                switch (__instance.Item.name)
                {
                    case "Seal Chit City Merchant":
                        VogsBingoModPlugin.instance.SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                        break;
                    case "Rosary_Set_Medium":
                        VogsBingoModPlugin.instance.SaveData.CurrentPurchasedRosaryNecklaces.Value++;
                        break;
                    case "Rosary_Set_Small":
                        AutomarkRosaryStringHandler.AddPurchasedString();
                        break;
                    case "Crest Socket Unlocker":
                        VogsBingoModPlugin.instance.SaveData.MemoryLockets.Value++;
                        break;
                    case "Tool Metal":
                        VogsBingoModPlugin.instance.SaveData.Craftmetal.Value++;
                        break;
                    case GoalHelper.ObjectNameSilkSpool:
                        VogsBingoModPlugin.instance.SaveData.SpoolFragments.Value++;
                        break;
                    default:
                        break;
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(YesNoAction),nameof(YesNoAction.SendEvent))]
        static void YesNoActionPatch(YesNoAction __instance, bool isYes)
        {
            if (!isYes)
            {
                return;
            }
            switch (GetSceneName())
            {
                case "Under_08" when __instance.fsm.GameObjectName.Equals("NPC Control"):
                    Automarker.MarkIfAvailable(GoalID.PayToConfess);
                    break;
                case "Song_01b" when __instance.fsm.GameObjectName.Equals(GoalHelper.ObjectNameVentrica):
                    Automarker.MarkIfAvailable(GoalID.ChoralChambersVentrica);
                    break;
                case "Hang_06b" when __instance.fsm.GameObjectName.Equals(GoalHelper.ObjectNameVentrica):
                    Automarker.MarkIfAvailable(GoalID.HighHallsVentrica);
                    break;
                case "Arborium_Tube":
                    Automarker.MarkIfAvailable(GoalID.MemoriumVentrica);
                    break;
                case "Bellway_Shadow" when __instance.fsm.GameObjectName.Equals("Bellway Toll Machine"):
                    Automarker.MarkIfAvailable(GoalID.BilewaterBellway);
                    break;
                case "Cog_10":
                    Automarker.MarkIfAvailable(GoalID.ActivateSecondSentinel);
                    break;
                case "Belltown_Room_Relic":
                    VogsBingoModPlugin.instance.SaveData.RelicTypesCurrentlyHeld.ResetFlags();
                    break;
                default:
                    break;
            }
            if (__instance.fsm.GameObjectName.Equals("Caravan Lech"))
            {
                Automarker.MarkIfAvailable(GoalID.PayforaFleaSpa);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(InventoryToolCrestSlot),nameof(InventoryToolCrestSlot.PlayFinalShake))]
        static void CrestSlotUnlock(InventoryToolCrestSlot __instance)
        {
            if (__instance.DisplayName.Equals("Hunter"))
            {
                return;
            }
            foreach (InventoryToolCrestSlot slot in __instance.Crest.activeSlots)
            {
                if (slot.IsLocked == true)
                {
                    return;
                }
            }
            Automarker.MarkIfAvailable(GoalID.FullyUnlockaNonHuntersCrest);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Breakable),nameof(Breakable.Break))]
        static void BreakablePatch(Breakable __instance)
        {
            if (IsScene("Dust_10") && __instance.gameObject.name.Equals("main root"))
            {
                Automarker.MarkIfAvailable(GoalID.FixtheSinnersRoadBench);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SetBoolValue),nameof(SetBoolValue.OnEnter))]
        static void BoolValuePatch(SetBoolValue __instance)
        {
            switch (__instance.owner.name)
            {
                case GoalHelper.ObjectNameMaskShard: case GoalHelper.ObjectNameMaskShardDupe:
                    switch (GetSceneName())
                    {
                        case "Dock_08":
                            Automarker.MarkIfAvailable(GoalID.DeepDocksMaskShard);
                            break;
                        case "Bone_East_LavaChallenge":
                            Automarker.MarkIfAvailable(GoalID.FarfieldsEscapeMaskShard);
                            break;
                        case "Slab_17":
                            Automarker.MarkIfAvailable(GoalID.SlabMaskShard);
                            break;
                        case "Shadow_13":
                            Automarker.MarkIfAvailable(GoalID.BilewaterMaskShard);
                            break;
                        case "Shellwood_14":
                            VogsBingoModPlugin.instance.SaveData.ShellwoodMaskShard.Value = true;
                            break;
                        case "Library_05":
                            VogsBingoModPlugin.instance.SaveData.VaultsMaskShard.Value = true;
                            break;
                        case "Coral_19b":
                            Automarker.MarkIfAvailable(GoalID.BlastedStepsMaskShard);
                            break;
                        case "Song_09":
                            Automarker.MarkIfAvailable(GoalID.CogworkCoreMaskShard);
                            break;
                        case "Wisp_07":
                            Automarker.MarkIfAvailable(GoalID.WispThicketMaskShard);
                            break;
                        case "Peak_04c":
                            Automarker.MarkIfAvailable(GoalID.MtFayMaskShard);
                            break;
                        case "Peak_06":
                            Automarker.MarkIfAvailable(GoalID.BrightveinMaskShard);
                            break;
                        default:
                            break;
                    }
                    break;
                case GoalHelper.ObjectNameSilkSpool:
                    VogsBingoModPlugin.instance.SaveData.SpoolFragments.Value++;
                    switch (GetSceneName())
                    {
                        case "Bone_East_13":
                            VogsBingoModPlugin.instance.SaveData.DeepDocksSpoolFragNearSpa.Value = true;
                            break;
                        case "Dock_03c":
                            VogsBingoModPlugin.instance.SaveData.DeeperDocksSpoolFrag.Value = true;
                            break;
                        case "Greymoor_02":
                            Automarker.MarkIfAvailable(GoalID.GreymoorSpoolFragment);
                            break;
                        case "Song_19_entrance":
                            Automarker.MarkIfAvailable(GoalID.GrandGateSpoolFragment);
                            break;
                        case "Hang_03_top":
                            Automarker.MarkIfAvailable(GoalID.HighHallsSpoolFragment);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(EventRegister),nameof(EventRegister.SendEvent),[typeof(string), typeof(GameObject)])]
        static void SendEventPatch(string eventName)
        {
            if (eventName == "MEMORY ORB COLLECT")
            {
                VogsBingoModPlugin.instance.SaveData.SilkHearts.Value++;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DeliveryQuestItem),nameof(DeliveryQuestItem.BreakEffect))]
        static void CourierBreakPatch()
        {
            Automarker.MarkIfAvailable(GoalID.BreakaCourierItem);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SavedItemGetV2),nameof(SavedItemGetV2.OnEnter))]
        static void ItemGetPatch(SavedItemGetV2 __instance)
        {
            if (((SavedItem)__instance.Item.Value).name.Equals("FleasCollected Target"))
            {
                Automarker.UpdateFleas();
            }
        }

        // [HarmonyPrefix]
        // [HarmonyPatch(typeof(GameManager),nameof(GameManager.SaveGame),[typeof(int),typeof(Action<bool>),typeof(bool),typeof(AutoSaveName)])]
        // static void VogsBingoModPlugin.instance.SaveDataPatch(int saveSlot)
        // {
        //     VogsBingoModPlugin.LogInfo("Saving Automarker Data...   Don't turn off the power.");
        //     VogsBingoModPlugin.instance.SaveData.VogsBingoModPlugin.instance.SaveData(saveSlot);
        // }

        // [HarmonyPostfix]
        // [HarmonyPatch(typeof(GameManager),nameof(GameManager.SetState))]
        // static void GameStatePatch(GameState newState, GameManager __instance)
        // {
        //     if (newState == GameState.LOADING && AutomarkPatches.wasInMainMenu)
        //     {
        //         AutomarkPatches.wasInMainMenu = false;
        //         VogsBingoModPlugin.instance.SaveData.LoadData(__instance.profileID);
        //     }
        //     if (newState == GameState.MAIN_MENU)
        //     {
        //         if (!AutomarkPatches.wasInMainMenu)
        //         {
        //             VogsBingoModPlugin.instance.SaveData.SetDefaultData();
        //             AutomarkPatches.wasInMainMenu = true;
        //         }
        //     }
        // }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerData),nameof(PlayerData.AddToMaxHealth))]
        static void AddToMaxHealthPatch()
        {
            VogsBingoModPlugin.instance.SaveData.ExtraMasks.Value = PlayerData.instance.CurrentMaxHealth - 5;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CollectableItemPickup),nameof(CollectableItemPickup.EndInteraction))]
        static void ItemPickupPatch(CollectableItemPickup __instance, bool didPickup)
        {
            if (!didPickup)
            {
                return;
            }
            switch (__instance.Item.name)
            {
                case GoalHelper.ItemNamePollenHeart:
                    Automarker.MarkIfAvailable(GoalID.PollenHeart);
                    if (PlayerData.instance.CollectedHeartClover && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartHunter)
                    {
                        VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    }
                    break;
                case GoalHelper.ItemNameConjoinedHeart:
                    Automarker.MarkIfAvailable(GoalID.ConjoinedHeart);
                    if (PlayerData.instance.CollectedHeartHunter && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartFlower)
                    {
                        VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    }
                    break;
                case GoalHelper.ItemNameHuntersHeart:
                    Automarker.MarkIfAvailable(GoalID.HuntersHeart);
                    if (PlayerData.instance.CollectedHeartClover && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartFlower)
                    {
                        VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    }
                    break;
                case GoalHelper.ItemNameEncrustedHeart when PlayerData.instance.CollectedHeartClover && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartFlower:
                    VogsBingoModPlugin.instance.SaveData.MementosObtained.Value++;
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(BasicNPC),nameof(BasicNPC.OnStartDialogue))]
        static void BasicNPCPatch(BasicNPC __instance)
        {
            foreach (SavedItem item in __instance.giveOnFirstTalkItems)
            {
                if (item.name.Equals("Magnetite"))
                {
                    Automarker.MarkIfAvailable(GoalID.InspectMagnetiteinBrightvein);
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MemoryOrbGroup),nameof(MemoryOrbGroup.CollectedOrb))]
        static void MemoryOrbGroupPatch(MemoryOrbGroup __instance)
        {
            if (!__instance.IsAllCollected)
            {
                return;
            }
            if (IsScene("Clover_18"))
            {
                VogsBingoModPlugin.instance.SaveData.VerdaniaFountainOrbs.Value++;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BattleScene),nameof(BattleScene.DoEndBattle))]
        static void ArenaCompletePatch(BattleScene __instance)
        {
            if (__instance.completed && IsScene("Memory_Coral_Tower"))
            {
                Automarker.MarkIfAvailable(GoalID.ClearFirstCoralTowerArena);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(SprintRaceController),nameof(SprintRaceController.CheckCompletion))]
        static void RaceCompletePatch(SprintRaceController __instance, bool isHero)
        {
            if (isHero && __instance.heroLapsCompleted >= __instance.lapCount && !__instance.isCompleted)
            {
                Automarker.MarkIfAvailable(GoalID.WinARace);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Remasker),nameof(Remasker.Entered))]
        static void AreaEnterPatch(Remasker __instance)
        {
            if (IsScene("Dust_11") && __instance.name.Equals("Remasker New (2)"))
            {
                Automarker.MarkIfAvailable(GoalID.VisitStyxsMaskRoom);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ToolCrest),nameof(ToolCrest.Unlock))]
        static void ToolCrestUnlockPatch(ToolCrest __instance)
        {
            if (!__instance.IsUnlocked)
            {
                VogsBingoModPlugin.instance.SaveData.Crests.Value++;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ToolItemManager),nameof(ToolItemManager.ReportToolUnlocked), [typeof(ToolItemType), typeof(bool)])]
        static void ToolUnlockPatch(ToolItemType type)
        {
            if (type == ToolItemType.Skill)
            {
                Automarker.UpdateSilkSkills();
            } else
            {
                Automarker.UpdateTools(ToolItemManager.GetUnlockedTools());
            }
        }
        

#pragma warning disable HARMONIZE004
        static bool IsScene(string sceneName) {return SceneManager.GetActiveScene().name.Equals(sceneName);}
        static string GetSceneName() {return SceneManager.GetActiveScene().name;}
#pragma warning restore HARMONIZE004
    }
}
