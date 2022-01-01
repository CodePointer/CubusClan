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
	class FlareRibbon
	{
		public static string IDName = "Relic_FlareRibbon";

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
						RelicEffectClassType = typeof(RelicEffectAddStatusEffectForPyreDamage),
						ParamCharacterSubtype = "SubtypesData_Pyre",
						ParamSourceTeam = Team.Type.Monsters,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = VanillaStatusEffectIDs.Armor,
								count = 4,
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
