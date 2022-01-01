using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.Upgrades
{
	class ShadowLadyBloodThurstyPro
	{
		public static string IDName = "Upgrade_ShadowLadyBloodThurstyPro";

		public static CardUpgradeDataBuilder Builder()
		{
			var railyard = new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Name",
				UseUpgradeHighlightTextTags = true,
				BonusDamage = 0,
				BonusHP = 60,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{ 
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnTurnBegin,
						DescriptionKey = IDName + "_OnAction_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
								ParamInt = 20
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
