using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.CardEffects;

namespace SuccClan.Cards.Upgrades
{
	class KnightMareEndlessPro
	{
		public static string IDName = "Upgrade_KnightMareEndlessPro";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 20,
				BonusHP = 10,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{ 
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnDeath,
						DescriptionKey = IDName + "_OnDeath_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = typeof(CardEffectSpawnSelfSoulEnchant).AssemblyQualifiedName,
								ParamInt = 4,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									StatusEffectUpgrades = new List<StatusEffectStackData>
									{
										new StatusEffectStackData
										{
											statusId = VanillaStatusEffectIDs.Multistrike,
											count = 4,
										},
									},
								}.Build(),
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
