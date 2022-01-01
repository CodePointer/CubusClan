using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;
using SuccClan.Cards;
using SuccClan.RelicEffects;
using SuccClan.Effects;

namespace SuccClan.Relics
{
	class MutantElixirs  
	{
		public static string IDName = "Relic_MutantElixirs";

		public static void Make()
		{
			var relic = new CollectableRelicDataBuilder
			{
				IconPath = "Relic/" + IDName + ".png",
				RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
				
				EffectBuilders = new List<RelicEffectDataBuilder>
				{
					new RelicEffectDataBuilder
					{
						RelicEffectClassType = typeof(RelicEffectAddUpgradeOnSpawn),
						ParamSourceTeam = Team.Type.Monsters,
						ParamString = "OnlyFromCard",
						ParamCharacterSubtype = "SubtypesData_None",

						EffectConditionBuilders = new List<RelicEffectConditionBuilder>
						{
							new RelicEffectConditionBuilder
							{
								paramTrackedValue = CardStatistics.TrackedValueType.SubtypeInDeck,
								paramCardType = CardStatistics.CardTypeTarget.Any,
								paramTrackTriggerCount = true,
								paramEntryDuration = CardStatistics.EntryDuration.ThisTurn,
								paramComparator = RelicEffectCondition.Comparator.Equal | RelicEffectCondition.Comparator.GreaterThan,
								paramInt = 1,
								allowMultipleTriggersPerDuration = false,
							},
						},

						ParamCardUpgradeData = new CardUpgradeDataBuilder
						{
							UpgradeTitleKey = IDName + "_Upgrade_Name",
							UpgradeDescriptionKey = IDName + "_Upgrade_Desc",
							BonusSize = 1,
							StatusEffectUpgrades = new List<StatusEffectStackData>
							{
								new StatusEffectStackData
								{
									statusId = VanillaStatusEffectIDs.Multistrike,
									count = 1,
								},
							},
						}.Build(),
					},
				},
			};

			Utils.AddRelic(relic, IDName);

			var r = relic.BuildAndRegister();
			r.GetNameEnglish();
		}
	}
}
