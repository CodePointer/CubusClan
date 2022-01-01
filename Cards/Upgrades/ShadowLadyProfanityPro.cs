using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Cards.SpellCards;

namespace SuccClan.Cards.Upgrades
{
	class ShadowLadyProfanityPro
	{
		public static string IDName = "Upgrade_ShadowLadyProfanityPro";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 45,
				BonusHP = 50,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Quick,
					},
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = IDName + "_OnResovle_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
								ParamInt = (int)CardPile.HandPile,
								AdditionalParamInt = 2,
								ParamCardPool = ProfaneAscendingPlus.cardPool,
							},
						},
					},

					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnAnyUnitDeathOnFloor,
						DescriptionKey = IDName + "_OnHarvest_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.Self,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Stealth,
										count = 1,
									},
								},
							},
						},
					}
				},
			};

			return railyard;
		}

		public static CardUpgradeData Make()
		{
			return Builder().Build();
		}
	}
}
