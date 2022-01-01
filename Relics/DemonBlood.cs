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
	class DemonBlood
	{
		public static string IDName = "Relic_DemonBlood";

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
						RelicEffectClassType = typeof(RelicEffectDamageOnCardPlayed),
						ParamSourceTeam = Team.Type.Heroes,
						ParamInt = 3,
						ParamTargetMode = TargetMode.RandomInRoom,
						ParamCardType = CardType.Blight,
					},
					new RelicEffectDataBuilder
					{
						RelicEffectClassType = typeof(RelicEffectDamageOnCardPlayed),
						ParamSourceTeam = Team.Type.Heroes,
						ParamInt = 3,
						ParamTargetMode = TargetMode.RandomInRoom,
						ParamCardType = CardType.Junk,
					},
				},
			};

			Utils.AddRelic(relic, IDName);

			var r = relic.BuildAndRegister();
			r.GetNameEnglish();
		}
	}
}
