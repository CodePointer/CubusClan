using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Cards.SpellCards;

namespace SuccClan.Cards.Upgrades
{
	class ShadowLadyProfanityBasic
	{
		public static string IDName = "Upgrade_ShadowLadyProfanityBasic";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 10,
				BonusHP = 20,

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
								ParamInt = 3,
								AdditionalParamInt = 1,
								ParamCardPool = ProfaneAscending.cardPool,
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
										statusId = VanillaStatusEffectIDs.Lifesteal,
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
