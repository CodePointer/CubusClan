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
	class PoisonSerum
	{
		public static string IDName = "Relic_PoisonSerum";

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
						RelicEffectClassType = typeof(RelicEffectChangeStartingHandSize),
						ParamInt = 1,
					},
					new RelicEffectDataBuilder
					{
						RelicEffectClassType = typeof(RelicEffectAddBattleCardToPile),
						ParamInt = (int)CardPile.DeckPileRandom,
						ParamCardPool = MyCardPools.ObsessingShardPool,
					},
				},
			};

			Utils.AddRelic(relic, IDName);

			var r = relic.BuildAndRegister();
			r.GetNameEnglish();
		}
	}
}
