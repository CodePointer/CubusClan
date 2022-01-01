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
	class FleshRing // TODO: Add scourge.
	{
		public static string IDName = "Relic_FleshRing";

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
						RelicEffectClassType = typeof(RelicEffectCardDrawOnBlightPlay),
						ParamInt = 1,
						ParamCardType = CardType.Blight,
						EffectConditionBuilders = new List<RelicEffectConditionBuilder>
						{
							new RelicEffectConditionBuilder
							{
								paramTrackedValue = CardStatistics.TrackedValueType.AnyCardPlayed,
								paramCardType = CardStatistics.CardTypeTarget.Any,
								paramTrackTriggerCount = false,
								paramEntryDuration = CardStatistics.EntryDuration.ThisTurn,
								paramComparator = RelicEffectCondition.Comparator.Equal | RelicEffectCondition.Comparator.GreaterThan,
								allowMultipleTriggersPerDuration = false,
								paramInt = 1,
							},
						},
					},
				},
			};

			Utils.AddRelic(relic, IDName);

			var r = relic.BuildAndRegister();
			r.GetNameEnglish();
		}
	}
}
