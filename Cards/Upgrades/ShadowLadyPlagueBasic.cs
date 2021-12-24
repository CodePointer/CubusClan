using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Cards.SpellCards;
using SuccClan.Effects;

namespace SuccClan.Cards.Upgrades
{
	class ShadowLadyPlagueBasic
	{
		public static string IDName = "Upgrade_ShadowLadyPlagueBasic";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 0,
				BonusHP = 20,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
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
								ParamInt = 3,  // 0: Draw  1: Discard  3: Hand
								AdditionalParamInt = 3,
								ParamCardPool = MyCardPools.VengefulShardPool,
							}
						}
					},
					new CharacterTriggerDataBuilder
					{
						Trigger = Trigger_OnFanatic.OnFanaticCharTrigger.GetEnum(),
						DescriptionKey = IDName + "_OnFanatic_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Self,
								ParamInt = 3
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.Self,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Regen,
										count = 1,
									},
								},
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectGainEnergy,
								ParamInt = 1
							},
						},
					},
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
