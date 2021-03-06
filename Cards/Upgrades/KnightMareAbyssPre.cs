using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;
using SuccClan.CardEffects;

namespace SuccClan.Cards.Upgrades
{
	class KnightMareAbyssPre
	{
		public static string IDName = "Upgrade_KnightMareAbyssPre";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 5,
				BonusHP = 10,

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
								EffectStateName = typeof(CardEffectDrawAddEnergyPsionicBlust).AssemblyQualifiedName,
								ParamInt = 1,  // Soul cost
								AdditionalParamInt = 1,  // Card & Ember
							},
							new CardEffectDataBuilder
							{
								EffectStateName = VanillaCardEffectTypes.CardEffectBuffDamage.AssemblyQualifiedName,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 2
							},
							new CardEffectDataBuilder
							{
								EffectStateName = VanillaCardEffectTypes.CardEffectBuffMaxHealth.AssemblyQualifiedName,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 2
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
