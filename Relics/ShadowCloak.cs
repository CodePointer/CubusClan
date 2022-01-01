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
	class ShadowCloak
	{
		public static string IDName = "Relic_ShadowCloak";

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
						RelicEffectClassType = typeof(RelicEffectModifyTriggerCount),
						ParamSourceTeam = Team.Type.Monsters,
						ParamInt = 1,
						ParamTrigger = Trigger_OnFanatic.OnFanaticCharTrigger.GetEnum(),
					},
				},
			};

			Utils.AddRelic(relic, IDName);

			var r = relic.BuildAndRegister();
			r.GetNameEnglish();
		}
	}
}
