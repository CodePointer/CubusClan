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
using SuccClan.Effects;

namespace SuccClan.Cards
{
	[HarmonyPatch(typeof(TooltipUI), "FormatTitleWithIcon")]
	public static class IconRemovalService
	{
		private static void Prefix(ref string title, ref string icon)
		{
			var effects = new List<string>
			{
				StatusEffectFrantic.IDName,
				StatusEffectPsionic.IDName,
			};

			foreach (var effect_name in effects)
			{
				string text = StatusEffectManager.GetLocalizedName(effect_name, 1, false, true, false);
				if (title == text)
				{
					icon = "";
				}
			}
		}
	}

	//[HarmonyPatch(typeof(RerollMerchantRewardData), "CanBeGranted")]
	//public static class InfiniteReroll
	//{
	//	private static void Postfix(ref bool __result)
	//	{
	//		__result = true;
	//	}
	//}

	[HarmonyPatch(typeof(SaveManager), "SetupRun")]
	class AddToStartingDeck
	{
		static void Postfix(ref SaveManager __instance)
		{
			//__instance.AdjustGold(5000);

			var testCardID = new List<string>
			{
				//PiercingShriek.IDName,
				//Oolioddroo.IDName,
				//BloodCarnival.IDName,
				//PowerSiphon.IDName,
				//MindDomaination.IDName,
				//CubusSpike.IDName,
				//EndlessShadow.IDName,
				//IllusionTwins.IDName,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
				//VanillaCardIDs.VengefulShard,
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

	//[HarmonyPatch(typeof(CompendiumSectionCards), "IsCardUnlockedAndDiscovered")]
	//class RevealAllCards
	//{
	//	static void Postfix(ref bool __result)
	//	{
	//		__result = true;
	//	}
	//}

	//[HarmonyPatch(typeof(CompendiumRelicUI), "SetLocked")]
	//class RevealAllRelics
	//{
	//	static bool Prefix(CompendiumRelicUI __instance)
	//	{
	//		return false;
	//	}
	//}

	//[HarmonyPatch(typeof(UpgradeTreeUI), "RefreshDiscovered")]
	//class RevealAllChamps
	//{
	//	static void Prefix(UpgradeTreeUI __instance, MetagameSaveData metagameSave)
	//	{
	//		foreach (ClassData classData in ProviderManager.SaveManager.GetBalanceData().GetClassDatas())
	//		{
	//			for (int i = 0; i < 2; i++)
	//			{
	//				foreach (CardUpgradeTreeData.UpgradeTree upgradeTree in classData.GetUpgradeTree(i).GetUpgradeTrees())
	//				{
	//					foreach (CardUpgradeData cardUpgrade in upgradeTree.GetCardUpgrades())
	//					{
	//						metagameSave.MarkChampionUpgradeDiscovered(cardUpgrade);
	//					}
	//				}
	//			}
	//		}
	//	}
	//}
}
