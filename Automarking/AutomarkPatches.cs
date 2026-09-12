/// author: AtlasOfSouls
/// © 2026 AtlasOfSouls
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using HarmonyLib;
using UnityEngine.SceneManagement;

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
                case "Fine Pins":
                    Automarker.MarkIfAvailable(GoalID.FinePins);
                    break;
                default:
                    break;
            }
            switch (__instance.QuestType.name.ToLower())
            {
                case "hunt": case "grand hunt":
                    SaveData.HuntWishes.Value++;
                    SaveData.WishesCompleted.Value++;
                    break;
                case "gather":
                SaveData.GatherWishes.Value++;
                    SaveData.WishesCompleted.Value++;
                    break;
                case "wayfarer":
                    SaveData.WayfarerWishes.Value++;
                    SaveData.WishesCompleted.Value++;
                    break;
                case "donate":
                    SaveData.DonationWishes.Value++;
                    SaveData.WishesCompleted.Value++;
                    break;
                case "delivery": case "learn": case "sprint": case "witness": case "steel":
                    SaveData.WishesCompleted.Value++;
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
                case GoalHelper.SpriteNameCraftmetal:
                    SaveData.Craftmetal.Value++;
                    switch (GetSceneName())
                    {
                        case "Coral_32":
                            Automarker.CheckIfGoalCompleted(GoalID.BlastedStepsSilkeaterCraftmetal, "craftmetal");
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
                            SaveData.SinnersKey.Value = true;
                            break;
                        case "Bellshrine_Coral":
                            SaveData.KarakKey.Value = true;
                            break;
                    }
                    break;
                case GoalHelper.SpriteNameRuneHarp:
                    SaveData.RuneHarps.Value++;
                    SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.RuneHarp);
                    SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.RuneHarp);
                    break;
                case GoalHelper.SpriteNameBoneScroll:
                    SaveData.BoneScrolls.Value++;
                    SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.BoneScroll);
                    SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.BoneScroll);
                    break;
                case GoalHelper.SpriteNameWeaverEffigy:
                    SaveData.WeaverEffigies.Value++;
                    SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.WeaverEffigy);
                    SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.WeaverEffigy);
                    break;
                case GoalHelper.SpriteNameChoralCommandment:
                    SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                    SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                    break;
                case GoalHelper.SpriteNameCogheartPiece0: case GoalHelper.SpriteNameCogheartPiece1: case GoalHelper.SpriteNameCogheartPiece2: 
                    SaveData.CogheartPieces.Value++;
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
                    SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.ArcaneEgg);
                    SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.ArcaneEgg);
                    break;
                case GoalHelper.SpriteNameSurfaceMemento:
                    Automarker.MarkIfAvailable(GoalID.SurfaceMemento);
                    SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameGreyMemento:
                    Automarker.MarkIfAvailable(GoalID.GreyMemento);
                    SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameToolPouch:
                    if (PlayerData.instance.pinGalleriesCompleted >= 1)
                        Automarker.MarkIfAvailable(GoalID.WinaLoddieMinigame);
                    SaveData.CraftKitToolPouchCount.Value++;
                    Automarker.MarkIfAvailable(GoalID.OneToolPouchUpgrade);
                    break;
                case GoalHelper.SpriteNameCraftingKit:
                    SaveData.CraftKitToolPouchCount.Value++;
                    Automarker.MarkIfAvailable(GoalID.OneCraftingKit);
                    break;
                case GoalHelper.SpriteNameMemoryLocket:
                    SaveData.MemoryLockets.Value++;
                    break;
                case GoalHelper.SpriteNameShardBundle:
                    SaveData.NonPurchasedShardBundles.Value++;
                    break;
                case GoalHelper.SpriteNameRosaryNecklace: case GoalHelper.SpriteNameHeavyRosaryNecklace: case GoalHelper.SpriteNamePaleRosaryNecklace:
                    SaveData.NonPurchasedRosaryNecklaces.Value++;
                    break;
                case GoalHelper.SpriteNameHerosMemento:
                    SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameCrawMemento:
                    SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameHuntersMemento:
                    SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameSprintmasterMemento:
                    SaveData.MementosObtained.Value++;
                    break;
                case GoalHelper.SpriteNameSethMemento:
                    SaveData.MementosObtained.Value++;
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
                    if (SaveData.CurrentPurchasedRosaryNecklaces > 0)
                    {
                        SaveData.CurrentPurchasedRosaryNecklaces.Value--;
                    } else
                    {
                        SaveData.NonPurchasedRosaryNecklaces.Value--;
                    }
                    break;
                case GoalHelper.ObjectNameHeavyNecklace: case GoalHelper.ObjectNamePaleNecklace:
                    SaveData.NonPurchasedRosaryNecklaces.Value--;
                    break;
                case GoalHelper.ObjectNameShardBundle:
                    if (SaveData.NonPurchasedShardBundles > SaveData.NonPurchasedBrokenShardBundles)
                    {
                        SaveData.NonPurchasedBrokenShardBundles.Value++;
                    }
                    break;
                case GoalHelper.ObjectNameRosaryString:
                    SaveData.automarkRosaryStringHandler.BreakString();
                    break;
                case GoalHelper.ObjectNameFrayedString:
                    SaveData.automarkRosaryStringHandler.BreakFrayedString();
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
                    SaveData.HasJournal.Value = true;
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
                    SaveData.CogworkClapperKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameSkullwing:
                    SaveData.SkullwingKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameCragglite:
                    SaveData.CraggliteKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameGromling:
                    SaveData.GromlingKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameMiteMother:
                    SaveData.MiteMotherKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameMassiveMossgrub:
                    SaveData.MassiveMossgrubKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameDeepDiver:
                    SaveData.DeepDiverKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameUnravelled:
                    SaveData.UnravelledKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameShadowCharger:
                    SaveData.ShadowChargerKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameImoba:
                    SaveData.ImobaKilled.Value = true;
                    break;
                case GoalHelper.EnemyNameSquirrm:
                    SaveData.SquirrmKilled.Value = true;
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
                        SaveData.DuoMossMothers.Value++;
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
                    Automarker.UpdateFleas(bigFleaBeaten: true);
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
                    SaveData.VoidMassesKilled.Value++;
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
                case GoalHelper.EnemyNameShardillard:
                    Automarker.MarkIfAvailable(GoalID.KillOneShardillard);
                    break;
                case GoalHelper.EnemyNameGarmondZaza:
                    Automarker.MarkIfAvailable(GoalID.GarmondandZaza);
                    break;
                case GoalHelper.EnemyNamePilgrimPouncer when IsScene("Bone_09"):
                    Automarker.MarkIfAvailable(GoalID.KillPebb);
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
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.BoneBottom);
                            break;
                        case "Bone_04":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Marrow);
                            break;
                        case "Bone_East_01":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.DeepDocks);
                            break;
                        case "Ant_04_mid": case "Ant_20":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.HuntersMarch);
                            break;
                        case "Bone_East_21":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.FarFields);
                            break;
                        case "Greymoor_02":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.GreymoorBell);
                            break;
                        case "Greymoor_08" when PlayerData.instance.mapperLocationAct3 == 3:
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.GreymoorAct3);
                            break;
                        case "Belltown":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Bellhart);
                            break;
                        case "Shellwood_16":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.ShellwoodNormal);
                            break;
                        case "Shellwood_01" when PlayerData.instance.mapperLocationAct3 == 1:
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.ShellwoodAct3);
                            break;
                        case "Crawl_01":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Wormways);
                            break;
                        case "Coral_12":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.BlastedSteps);
                            break;
                        case "Dust_10":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.SinnersRoad);
                            break;
                        case "Peak_02":
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.MountFay);
                            break;
                        case "Coral_40":
                            Automarker.MarkIfAvailable(GoalID.TalktoShakrainSandsofKarak);
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.SandsOfKarak);
                            break;
                        case "Shadow_23":
                            Automarker.MarkIfAvailable(GoalID.TalktoShakrainBilewater);
                            SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.Bilewater);
                            break;
                        default:
                            break;
                    }
                    break;
                case GoalHelper.NPCNameShakraDuel:
                    SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.GreymoorDuel);
                    break;
                case GoalHelper.NPCNameShakraTrailsEnd:
                    SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.TrailsEnd);
                    break;
                case GoalHelper.NPCNameShakraAid: case GoalHelper.NPCNameShakraHHAWin:
                    SaveData.ShakraLocations.AddFlag((uint)ShakraLocationFlags.HighHallsArena);
                    break;
                case GoalHelper.NPCNameHuntress:
                    SaveData.HuntressSpokenTo.Value = true;
                    Automarker.MarkIfAvailable(GoalID.TalktoHuntress);
                    break;
                case GoalHelper.NPCNameStyx:
                    SaveData.StyxSpokenTo.Value = true;
                    break;
                case GoalHelper.NPCNameForgeDaughter when IsScene("Room_Forge"):
                    SaveData.ForgeDaughterSpokenTo.Value = true;
                    break;
                case GoalHelper.NPCNameTwelfthArchitect:
                    SaveData.TwelfthArchitectSpokenTo.Value = true;
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
                case GoalHelper.NPCNameOldPenitent:
                    Automarker.MarkIfAvailable(GoalID.TalktoOldPenitent);
                    break;
                case GoalHelper.NPCNamePiousIsamor when PlayerData.instance.libraryStatueWoken:
                    Automarker.MarkIfAvailable(GoalID.ListentoPiousIsamor);
                    break;
                case GoalHelper.NPCNameGilly:
                    SaveData.GillySpokenTo.Value = true;
                    break;
                case GoalHelper.NPCNameGrishkinGreymoor: case GoalHelper.NPCNameGrishkinPot:
                    SaveData.GrishkinSpokenTo.Value = true;
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(NPCControlBase),nameof(NPCControlBase.OnStartingDialogue))]
        static void BasicNPCDialogueStartPatch(NPCControlBase __instance)
        {
            switch (__instance.name)
            {
                case GoalHelper.NPCNameGrishkinMarrow: case GoalHelper.NPCNameGrishkinFleatopia:
                    SaveData.GrishkinSpokenTo.Value = true;
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
                    SaveData.GreymoorStyxWallBroken.Value = true;
                    break;
                case "One Way Wall (2)":
                    SaveData.SinnersStyxWallBroken.Value = true;
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
                if (__instance.Item.name == GoalHelper.ShopItemNameGrindleSpoolFrag)
                {
                    SaveData.PsalmCylinders.Value++;
                }
            }
            switch (__instance.name)
            {
                case "Bonebottom Faith Token":
                    SaveData.PebbKeyBought.Value = true;
                    break;
                case "City Merchant Simple Key":
                    SaveData.JubilanaKeyBought.Value = true;
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
                        SaveData.RelicTypesObtained.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                    SaveData.RelicTypesCurrentlyHeld.AddFlag((uint)RelicTypeFlags.ChoralCommandment);
                        break;
                    case "Rosary_Set_Medium":
                        SaveData.CurrentPurchasedRosaryNecklaces.Value++;
                        break;
                    case "Rosary_Set_Small":
                        SaveData.automarkRosaryStringHandler.AddPurchasedString();
                        break;
                    case "Crest Socket Unlocker":
                        SaveData.MemoryLockets.Value++;
                        break;
                    case "Tool Metal":
                        SaveData.Craftmetal.Value++;
                        break;
                    case GoalHelper.ObjectNameSilkSpool:
                        SaveData.SpoolFragments.Value++;
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
                    SaveData.RelicTypesCurrentlyHeld.ResetFlags();
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
                            SaveData.ShellwoodMaskShard.Value = true;
                            break;
                        case "Library_05":
                            SaveData.VaultsMaskShard.Value = true;
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
                    SaveData.SpoolFragments.Value++;
                    switch (GetSceneName())
                    {
                        case "Bone_East_13":
                            SaveData.DeepDocksSpoolFragNearSpa.Value = true;
                            break;
                        case "Dock_03c":
                            SaveData.DeeperDocksSpoolFrag.Value = true;
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
                        case "Under_10":
                            SaveData.UnderworksArenaSpoolFrag.Value = true;
                            break;
                        case "Library_11b":
                            SaveData.UnderworksLibrary_11bSpoolFrag.Value = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case GoalHelper.ObjectNameWoodWaspNest: case GoalHelper.ObjectNameWoodWaspNestOne:
                    SaveData.WoodWaspNestsBroken.Value++;
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
                SaveData.SilkHearts.Value++;
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
        // static void SaveDataPatch(int saveSlot)
        // {
        //     VogsBingoModPlugin.LogInfo("Saving Automarker Data...   Don't turn off the power.");
        //     SaveData.SaveData(saveSlot);
        // }

        // [HarmonyPostfix]
        // [HarmonyPatch(typeof(GameManager),nameof(GameManager.SetState))]
        // static void GameStatePatch(GameState newState, GameManager __instance)
        // {
        //     if (newState == GameState.LOADING && AutomarkPatches.wasInMainMenu)
        //     {
        //         AutomarkPatches.wasInMainMenu = false;
        //         SaveData.LoadData(__instance.profileID);
        //     }
        //     if (newState == GameState.MAIN_MENU)
        //     {
        //         if (!AutomarkPatches.wasInMainMenu)
        //         {
        //             SaveData.SetDefaultData();
        //             AutomarkPatches.wasInMainMenu = true;
        //         }
        //     }
        // }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerData),nameof(PlayerData.AddToMaxHealth))]
        static void AddToMaxHealthPatch()
        {
            SaveData.ExtraMasks.Value = PlayerData.instance.CurrentMaxHealth - 5;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CollectableItemPickup),nameof(CollectableItemPickup.DoPickupAction))]
        static void ItemPickupPatch(CollectableItemPickup __instance, bool __result)
        {
            if (!__result)
            {
                return;
            }
            switch (__instance.Item.name)
            {
                case GoalHelper.ItemNamePollenHeart:
                    Automarker.MarkIfAvailable(GoalID.PollenHeart);
                    if (PlayerData.instance.CollectedHeartClover && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartHunter)
                    {
                        SaveData.MementosObtained.Value++;
                    }
                    break;
                case GoalHelper.ItemNameConjoinedHeart:
                    Automarker.MarkIfAvailable(GoalID.ConjoinedHeart);
                    if (PlayerData.instance.CollectedHeartHunter && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartFlower)
                    {
                        SaveData.MementosObtained.Value++;
                    }
                    break;
                case GoalHelper.ItemNameHuntersHeart:
                    Automarker.MarkIfAvailable(GoalID.HuntersHeart);
                    if (PlayerData.instance.CollectedHeartClover && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartFlower)
                    {
                        SaveData.MementosObtained.Value++;
                    }
                    break;
                case GoalHelper.ItemNameEncrustedHeart when PlayerData.instance.CollectedHeartClover && PlayerData.instance.CollectedHeartCoral && PlayerData.instance.CollectedHeartFlower:
                    SaveData.MementosObtained.Value++;
                    break;
                case "Rosary_Set_Frayed":
                    SaveData.automarkRosaryStringHandler.AddFrayedString();
                    break;
                case "Rosary_Set_Small":
                    SaveData.automarkRosaryStringHandler.AddNonPurchasedString();
                    break;
                case GoalHelper.ItemNameHereticKey:
                    Automarker.MarkIfAvailable(GoalID.KeyofHeretic);
                    break;
                case GoalHelper.ItemNamePsalmCylinderLibraryRoof: case GoalHelper.ItemNamePsalmCylinderCardinius: case GoalHelper.ItemNamePsalmCylinderHighHalls: case GoalHelper.ItemNamePsalmCylinderUnderworks:
                    SaveData.PsalmCylinders.Value++;
                    break;
                case GoalHelper.ItemNameMemoryLocket when IsScene("Crawl_09"):
                    Automarker.MarkIfAvailable(GoalID.WormwaysMemoryLocket);
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
                switch(item.name){
                    case "Magnetite":
                        Automarker.MarkIfAvailable(GoalID.InspectMagnetiteinBrightvein);
                        break;
                    case GoalHelper.ItemNameFlintstone:
                        Automarker.MarkIfAvailable(GoalID.InspectFlintstoneinDeepDocks);
                        break;
                    default:
                        break;
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
                SaveData.VerdaniaFountainOrbs.Value++;
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
                VogsBingoModPlugin.LogInfo($"Crest unlocked: {__instance.name}");
                SaveData.Crests.Value++;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ToolItemManager),nameof(ToolItemManager.ReportToolUnlocked), [typeof(ToolItemType), typeof(bool)])]
        static void SilkSkillUnlockPatch(ToolItemType type)
        {
            if (type == ToolItemType.Skill){
                Automarker.UpdateSilkSkills();
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ToolItem),nameof(ToolItem.Unlock))]
        static void ToolUnlockPatch(ToolItem __instance)
        {
            if (__instance.IsUnlocked)
            {
                return;
            }
            VogsBingoModPlugin.LogInfo($"Unlocked a tool: \"{__instance.name}\"");
            
            switch (__instance.name)
            {
                case "Straight Pin": case "Tri Pin": case "Harpoon":
                    Automarker.CheckIfGoalCompleted(GoalID.StraightThreefoldandLongPin, __instance.name);
                    break;
                case "Sting Shard":
                    Automarker.CheckIfGoalCompleted(GoalID.PollipPouchStingShard, __instance.name);
                    break;
                case "Tack":
                    Automarker.MarkIfAvailable(GoalID.Tacks);
                    break;
                case "Lightning Rod": case "Pimpilo":
                    Automarker.CheckIfGoalCompleted(GoalID.PimpilloVoltvessels, __instance.name);
                    break;
                case "Conch Drill":
                    Automarker.MarkIfAvailable(GoalID.Conchcutter);
                    break;
                case "WebShot Forge":
                    Automarker.MarkIfAvailable(GoalID.RepairSilkshot);
                    break;
                case "WebShot Architect":
                    Automarker.MarkIfAvailable(GoalID.RepairSilkshot);
                    break;
                case "WebShot Weaver":
                    Automarker.MarkIfAvailable(GoalID.RepairSilkshot);
                    break;
                case "Screw Attack":
                    Automarker.MarkIfAvailable(GoalID.DelversDrill);
                    break;
                case "Cogwork Flier":
                    Automarker.MarkIfAvailable(GoalID.Cogfly);
                    break;
                case "Rosary Cannon":
                    Automarker.MarkIfAvailable(GoalID.RosaryCannon);
                    break;
                case "Flintstone":
                    Automarker.MarkIfAvailable(GoalID.Flintslate);
                    break;
                case "Silk Snare":
                    Automarker.MarkIfAvailable(GoalID.SnareSetter);
                    break;
                case "Lifeblood Syringe":
                    Automarker.MarkIfAvailable(GoalID.PlasmiumPhial);
                    break;
                case "Mosscreep Tool 2":
                    Automarker.MarkIfAvailable(GoalID.UpgradeDruidsEye);
                    break;
                case "Lava Charm": case "Curve Claws":
                    Automarker.CheckIfGoalCompleted(GoalID.MagmaBellCurveclaw, __instance.name);
                    break;
                case "Bell Bind":
                    Automarker.CheckIfGoalCompleted(GoalID.WardingBellClawMirror, __instance.name);
                    Automarker.CheckIfGoalCompleted(GoalID.WardingBellSawtoothCirclet, __instance.name);
                    break;
                case "Poison Pouch":
                    Automarker.CheckIfGoalCompleted(GoalID.PollipPouchStingShard, __instance.name);
                    Automarker.MarkIfAvailable(GoalID.PollipPouch);
                    break;
                case "Fractured Mask": case "Barbed Wire":
                    Automarker.CheckIfGoalCompleted(GoalID.BarbedBraceletFracturedMask, __instance.name);
                    break;
                case "Multibind":
                    Automarker.MarkIfAvailable(GoalID.Multibinder);
                    break;
                case "White Ring": case "Quickbind":
                    Automarker.CheckIfGoalCompleted(GoalID.WeavelightInjectorBand, __instance.name);
                    break;
                case "Brolly Spike":
                    Automarker.CheckIfGoalCompleted(GoalID.WardingBellSawtoothCirclet, __instance.name);
                    break;
                case "Dazzle Bind":
                    Automarker.CheckIfGoalCompleted(GoalID.WardingBellClawMirror, __instance.name);
                    break;
                case "Revenge Crystal":
                    Automarker.MarkIfAvailable(GoalID.MemoryCrystal);
                    break;
                case "Quick Sling":
                    Automarker.MarkIfAvailable(GoalID.QuickSling);
                    break;
                case "Maggot Charm":
                    Automarker.MarkIfAvailable(GoalID.WreathofPurity);
                    break;
                case "Pinstress Tool":
                    Automarker.MarkIfAvailable(GoalID.PinBadge);
                    break;
                case "Compass": case "Bone Necklace":
                    Automarker.CheckIfGoalCompleted(GoalID.CompassPendantBrooch, __instance.name);
                    break;
                case "Rosary Magnet":
                    Automarker.CheckIfGoalCompleted(GoalID.CompassPendantBrooch, __instance.name);
                    Automarker.CheckIfGoalCompleted(GoalID.MagnetiteDiceMagnetiteBrooch, __instance.name);
                    break;
                case "Weighted Anklet": case "Wallcling":
                    Automarker.CheckIfGoalCompleted(GoalID.WeightedBeltAscendantsGrip, __instance.name);
                    break;
                case "Dead Mans Purse":
                    Automarker.CheckIfGoalCompleted(GoalID.DeadBugsPurseaSilkeater, __instance.name);
                    break;
                case "Magnetite Dice":
                    Automarker.CheckIfGoalCompleted(GoalID.MagnetiteDiceMagnetiteBrooch, __instance.name);
                    break;
                case "Scuttlebrace": case "Sprintmaster":
                    Automarker.CheckIfGoalCompleted(GoalID.ScuttlebraceSilkspeed, __instance.name);
                    break;
                default:
                    break;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ToolItem),nameof(ToolItem.Unlock))]
        static void ToolCountPatch()
        {
            int redToolCount = 0;
            int blueToolCount = 0;
            int yellowToolCount = 0;
            foreach (ToolItem tool in ToolItemManager.GetUnlockedTools())
            {
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
                    Automarker.MarkIfAvailable(GoalID.FiveRedTools);
                    break;
                case >= 3:
                    Automarker.MarkIfAvailable(GoalID.ThreeRedTools);
                    break;
                default:
                    break;
            }
            if (blueToolCount >= 3)
            {
                Automarker.MarkIfAvailable(GoalID.ThreeBlueTools);
            }
            switch (yellowToolCount)
            {
                case >= 5:
                    Automarker.MarkIfAvailable(GoalID.FiveYellowTools);
                    break;
                case >= 3:
                    Automarker.MarkIfAvailable(GoalID.ThreeYellowTools);
                    break;
                default:
                    break;
            }
            if (redToolCount >= 2 && blueToolCount >= 2 && yellowToolCount >= 2)
            {
                Automarker.MarkIfAvailable(GoalID.Twoofeachtooltype);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CollectableItemManager),nameof(CollectableItemManager.AddItem))]
        static void ItemAddPatch(CollectableItem item, int amount)
        {
            VogsBingoModPlugin.LogInfo($"Item added: \"{item.name}\", amount added: {amount}");
            switch (item.name)
            {
                case "Great Shard":
                    SaveData.BeastShards.Value += amount;
                    break;
                case "Silk Grub":
                    SaveData.Silkeaters.Value += amount;
                    Automarker.CheckIfGoalCompleted(GoalID.DeadBugsPurseaSilkeater);
                    if (IsScene("Coral_37"))
                        SaveData.BlastedSilkeater.Value = true;
                    break;
                default:
                    break;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CollectableItemCollect),nameof(CollectableItemCollect.DoAction))]
        static void RosaryStringDispenserPatch(CollectableItem item)
        {
            switch (item.name)
            {
                case "Rosary_Set_Small":
                    SaveData.automarkRosaryStringHandler.AddPurchasedString();
                    break;
                default:
                    break;
            }
        }
        

#pragma warning disable HARMONIZE004
        static bool IsScene(string sceneName) {return SceneManager.GetActiveScene().name.Equals(sceneName);}
        static string GetSceneName() {return SceneManager.GetActiveScene().name;}
#pragma warning restore HARMONIZE004
    }
}
