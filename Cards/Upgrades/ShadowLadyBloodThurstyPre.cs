using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.Upgrades
{
	class ShadowLadyBloodThurstyPre
	{
		public static string IDName = "Upgrade_ShadowLadyBloodThurstyPre";

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
						Trigger = CharacterTriggerData.Trigger.OnTurnBegin,
						DescriptionKey = IDName + "_OnAction_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
								ParamInt = 15
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
