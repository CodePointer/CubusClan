using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Cards.SpellCards;
using SuccClan.Effects;

namespace SuccClan.Cards.Upgrades
{
	class ShadowLadyPlaguePro
	{
		public static string IDName = "Upgrade_ShadowLadyPlaguePro";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 25,
				BonusHP = 40,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PreCombat,
						DescriptionKey = IDName + "_PreCombat_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = VanillaCardEffectTypes.CardEffectAddBattleCard.AssemblyQualifiedName,
								ParamInt = (int)CardPile.HandPile,  // 0: Draw  1: Discard  3: Hand
								AdditionalParamInt = 2,
								ParamCardPool = MyCardPools.VengefulShardPool,
								ParamCardUpgradeData = MyCardPools.exhaustUpgradeData,
							},
							new CardEffectDataBuilder
							{
								EffectStateName = VanillaCardEffectTypes.CardEffectGainEnergy.AssemblyQualifiedName,
								ParamInt = 1,
							},
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
								ParamInt = 5,
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.Self,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.DamageShield,
										count = 2,
									},
								},
							},
							//new CardEffectDataBuilder
							//{
							//	EffectStateType = VanillaCardEffectTypes.CardEffectGainEnergy,
							//	ParamInt = 1
							//},
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
