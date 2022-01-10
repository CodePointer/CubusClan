using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

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
				BonusDamage = 0,
				BonusHP = 10,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{ 
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.PostCombat,
						DescriptionKey = IDName + "_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
								ParamInt = 5
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffMaxHealth,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
								ParamInt = 5
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
