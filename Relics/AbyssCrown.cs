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
	class AbyssCrown
	{
		public static string IDName = "Relic_AbyssCrown";

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
						RelicEffectClassType = typeof(RelicEffectAddStackOnApplyStatus),
						ParamBool = true,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
								count = 1,
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
