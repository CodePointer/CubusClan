using System;
using System.Collections.Generic;
using System.Text;

using HarmonyLib;
using Trainworks;
using Trainworks.Constants;
using Trainworks.Managers;
using SuccClan.Cards.SpellCards;
using SuccClan.Cards.UnitCards;

using SuccClan.Relics;

namespace SuccClan.Cards
{
	[HarmonyPatch(typeof(SaveManager), "SetupRun")]
	class AddToStartingDeck
	{
		static void Postfix(ref SaveManager __instance)
		{
			var testCardID = new List<string>
			{
				DarkFury.IDName,
				PowerSiphon.IDName,
				MindDomaination.IDName,
				//EndlessShadow.IDName,
				//IllusionTwins.IDName,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//GreedGhost.IDName,
				//GreedGhost.IDName,
				//GreedGhost.IDName,
				//GreedGhost.IDName,
			};

			foreach (var cardID in testCardID)
			{
				__instance.AddCardToDeck(CustomCardManager.GetCardDataByID(cardID));
			}

			if (__instance.GetMainClass() == SuccClanPlugin.clanRef
				|| __instance.GetSubClass() == SuccClanPlugin.clanRef)
			{
				//var starterCrystal = CustomCollectableRelicManager.GetRelicDataByID(DesireCrystal.IDName);
				//__instance.AddRelic(starterCrystal);

				//__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(ProfaneCrossbow.IDName));
				//__instance.AddRelic(CustomCollectableRelicManager.GetRelicDataByID(MutantElixirs.IDName));
			}
		}
	}

	[HarmonyPatch(typeof(CompendiumSectionCards), "IsCardUnlockedAndDiscovered")]
	class RevealAllCards
	{
		static void Postfix(ref bool __result)
		{
			__result = true;
		}
	}

	[HarmonyPatch(typeof(CompendiumRelicUI), "SetLocked")]
	class RevealAllRelics
	{
		static bool Prefix(CompendiumRelicUI __instance)
		{
			return false;
		}
	}

	[HarmonyPatch(typeof(UpgradeTreeUI), "RefreshDiscovered")]
	class RevealAllChamps
	{
		static void Prefix(UpgradeTreeUI __instance, MetagameSaveData metagameSave)
		{
			foreach (ClassData classData in ProviderManager.SaveManager.GetBalanceData().GetClassDatas())
			{
				for (int i = 0; i < 2; i++)
				{
					foreach (CardUpgradeTreeData.UpgradeTree upgradeTree in classData.GetUpgradeTree(i).GetUpgradeTrees())
					{
						foreach (CardUpgradeData cardUpgrade in upgradeTree.GetCardUpgrades())
						{
							metagameSave.MarkChampionUpgradeDiscovered(cardUpgrade);
						}
					}
				}
			}
		}
	}
}
