using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;
using SuccClan.CardEffects;

namespace SuccClan.Cards.Upgrades
{
	class KnightMareSuicidePre
	{
		public static string IDName = "Upgrade_KnightMareTraitorPre";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 0,
				BonusHP = 30,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = StatusEffectSoulEnchant.IDName,
						count = 1,
					},
				},

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.CardSpellPlayed,
						DescriptionKey = IDName + "_OnIncant_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffMaxHealth,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 2,
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Spikes,
										count = 2,
									},
								},
							},
							new CardEffectDataBuilder
							{
								EffectStateType = typeof(CardEffectAddStatusEffectWithSoul),
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = VanillaStatusEffectIDs.Regen,
										count = 3,
									},
								},
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
