using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using HarmonyLib;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;

using SuccClan.Cards.UnitCards;

namespace SuccClan
{
	class Clan
	{
		public static string IDName = "Succubus";

		public static ClassData Make()
		{
			ClassDataBuilder clan = new ClassDataBuilder
			{
				ClassID = IDName,
				DraftIconPath = "ClanAssets/Icon_CardBack_SuccClan.png",

				IconAssetPaths = new List<string>
				{
					"ClanAssets/ClanLogo_92_stroke1.png",
					"ClanAssets/ClanLogo_92_stroke2.png",
					"ClanAssets/ClanLogo_92_stroke1.png",
					"ClanAssets/ClanLogo_silhouette.png",
				},

				CardFrameUnitPath = "ClanAssets/unit-cardframe-succubus.png",
				CardFrameSpellPath = "ClanAssets/spell-cardframe-succubus.png",

				UiColor = new Color(1.0f, 0.525f, 0.7f, 1.0f),
				UiColorDark = new Color(0.55f, 0.1f, 0.5f, 1.0f),
			};

			return clan.BuildAndRegister();
		}

		public static void RegisterBanner(List<string> cardIDList)
		{
			CardPool cardPool = UnityEngine.ScriptableObject.CreateInstance<CardPool>();
			var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(cardPool);

			// Add card here
			//var cardIDList = new List<string> { 
			//	ChaosCreation.IDName,
			//	DemonPioneer.IDName,
			//	IncubusButcher.IDName,
			//	SuccbusTorturer.IDName,
			//	//Vrolikai.IDName,
			//	//EndlessShadow.IDName,

			//	//ShadowWarrior.IDName,
			//	//AbyssPrincess.IDName,
			//};
			foreach (string cardID in cardIDList)
			{
				var cardData = CustomCardManager.GetCardDataByID(cardID);
				cardDataList.Add(cardData);
				Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All,
						"Unit added to Banner: " + cardData.GetName());
			}

			//foreach (var card in CustomCardManager.CustomCardData)
			//{
			//	if (card.Value.GetLinkedClassID() == SuccClanPlugin.getClan().GetID()
			//		&& card.Value.GetSpawnCharacterData() != null
			//		&& !card.Value.GetSpawnCharacterData().IsChampion())
			//	{
			//		cardDataList.Add(card.Value);
			//		Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All,
			//			"Unit added to Banner: " + card.Value.GetName());
			//	}
			//}

			new RewardNodeDataBuilder()
			{
				RewardNodeID = IDName + "_Banner",
				MapNodePoolIDs = new List<string>
				{
					VanillaMapNodePoolIDs.RandomChosenMainClassUnit,
					VanillaMapNodePoolIDs.RandomChosenSubClassUnit
				},
				TooltipTitleKey = "RewardNodeData_" + IDName + "_UnitBanner_TooltipTitleKey",
				TooltipBodyKey = "RewardNodeData_" + IDName + "_UnitBanner_TooltipBodyKey",
				RequiredClass = SuccClanPlugin.getClan(),
				ControllerSelectedOutline = "ClanAssets/selection_outlines.png",
				FrozenSpritePath = "ClanAssets/POI_Map_Clan_Succubus_Frozen.png",
				EnabledSpritePath = "ClanAssets/POI_Map_Clan_Succubus_Enabled.png",
				EnabledVisitedSpritePath = "ClanAssets/POI_Map_Clan_Succubus_Enabled.png",
				DisabledSpritePath = "ClanAssets/POI_Map_Clan_Succubus_Disabled.png",
				DisabledVisitedSpritePath = "ClanAssets/POI_Map_Clan_Succubus_VisitedDisabled.png",
				GlowSpritePath = "ClanAssets/MSK_Map_Clan_Succubus_01.png",
				MapIconPath = "ClanAssets/POI_Map_Clan_Succubus_Enabled.png",
				MinimapIconPath = "ClanAssets/Icon_MiniMap_ClanBanner.png",
				SkipCheckInBattleMode = true,
				OverrideTooltipTitleBody = false,
				NodeSelectedSfxCue = "Node_Banner",
				RewardBuilders = new List<IRewardDataBuilder>
				{
					new DraftRewardDataBuilder()
					{
						DraftRewardID = IDName + "_BannerReward",
						_RewardSpritePath = "ClanAssets/POI_Map_Clan_Succubus_Enabled.png",
						_RewardTitleKey = IDName + "Reward_Title",
						_RewardDescriptionKey = IDName + "Reward_Desc",
						Costs = new int[] { 100 },
						_IsServiceMerchantReward = false,
						DraftPool = cardPool,
						ClassType = (RunState.ClassType)7,
						DraftOptionsCount = 2,
						RarityFloorOverride = CollectableRarity.Uncommon
					}
				}
			}.BuildAndRegister();
		}
	}
}
