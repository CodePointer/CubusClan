﻿using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;
using SuccClan.Cards;

namespace SuccClan.Cards.UnitCards
{
	class SlothGhost
	{
		public static readonly string IDName = "Subunit_SlothGhost";
		public static readonly string IDChar = "Subunit_SlothGhostCharacter";

		public static void Make()
		{
			var charData = BuildUnit();
			BuildUpgrade(charData);

			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Uncommon,
			};

			Utils.AddUnit(railyard, IDName, charData);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}


		public static CharacterData BuildUnit()
		{
			var charBuilder = new CharacterDataBuilder
			{
				CharacterID = IDChar,
				NameKey = IDName + "_Name",
				SubtypeKeys = new List<string> { "SuccClan_Subtype_Ghost" },

				Size = 1,
				Health = 10,
				AttackDamage = 0,
				PriorityDraw = false,
				
				TriggerBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnSpawn,
						DescriptionKey = IDName + "_OnSpawn_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
								ParamInt = (int)CardPile.HandPile,
								AdditionalParamInt = 1,
								ParamCardPool = MyCardPools.ObsessingShardPool
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
								ParamInt = (int)CardPile.DeckPileRandom,
								AdditionalParamInt = 1,
								ParamCardPool = MyCardPools.ObsessingShardPool
							},
						},
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = Trigger_OnFanatic.OnFanaticCharTrigger.GetEnum(),
						DescriptionKey = IDName + "_OnFanatic_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Heroes,
								ParamInt = 5,
							}
						},
					},
				}
			};

			Utils.AddUnitImg(charBuilder, IDName + ".png");
			return charBuilder.BuildAndRegister();
		}

		public static void BuildUpgrade(CharacterData charData)
		{
			new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Upgrade_Name",
				UpgradeDescriptionKey = IDName + "_Upgrade_Desc",
				SourceSynthesisUnit = charData,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = Trigger_OnFanatic.OnFanaticCharTrigger.GetEnum(),
						DescriptionKey = IDName + "_OnFanatic_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Heroes,
								ParamInt = 5,
							}
						},
					},
				},
			}.Build();
		}
	}
}
