using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;
using SuccClan.CardEffects;

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
				BonusDamage = 30,
				BonusHP = 10,

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
						Trigger = CharacterTriggerData.Trigger.OnAttacking,
						DescriptionKey = IDName + "_OnAttack_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = typeof(CardEffectAddStatusEffectToOtherHeros).AssemblyQualifiedName,
								TargetMode = TargetMode.LastAttackedCharacter,
								TargetTeamType = Team.Type.Heroes,
								ParamInt = 30,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										statusId = StatusEffectFrantic.IDName,
										count = 2,
									}
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
