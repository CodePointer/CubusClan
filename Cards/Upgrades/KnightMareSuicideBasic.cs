using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;
using SuccClan.CardEffects;

namespace SuccClan.Cards.Upgrades
{
	class KnightMareSuicideBasic
	{
		public static string IDName = "Upgrade_KnightMareSuicideBasic";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 0,
				BonusHP = 10,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = StatusEffectSoulBlust.IDName,
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
								ParamInt = 1,
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
										count = 1,
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
										count = 2,
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
