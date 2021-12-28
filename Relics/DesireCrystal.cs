using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;
using SuccClan.Cards;
using SuccClan.RelicEffects;

namespace SuccClan.Relics
{
	class DesireCrystal
	{
		public static string IDName = "Relic_DesireCrystal";
		public static string offeringUpgradeID = "BlightOfferingUpgrade";

		public static void Make()
		{
			// Please add your upgrade via this CardUpgradeData.
			// You can refer to Sting-relevent relics for upgrade writing.
			var offeringUpgrade = new CardUpgradeDataBuilder
			{

				UpgradeTitleKey = offeringUpgradeID + "_Title",
				UpgradeDescriptionKey = offeringUpgradeID + "_Desc",

				HideUpgradeIconOnCard = false,
				UseUpgradeHighlightTextTags = true,

				TraitDataUpgrades = new List<CardTraitData>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitTreasure,
					}.Build(),
				},

			}.Build();

			var relic = new CollectableRelicDataBuilder
			{
				IconPath = "Relic/" + IDName + ".png",
				RelicPoolIDs = new List<string> { VanillaRelicPoolIDs.MegaRelicPool },
				EffectBuilders = new List<RelicEffectDataBuilder>
				{
					new RelicEffectDataBuilder
					{
						RelicEffectClassType = typeof(RelicEffectBlightAddOfferingUpgrade),
						ParamSourceTeam = Team.Type.Monsters,
						ParamCardUpgradeData = offeringUpgrade,
					},
				}
			};

			Utils.AddRelic(relic, IDName);

			var r = relic.BuildAndRegister();
			r.GetNameEnglish();
		}
	}
}
