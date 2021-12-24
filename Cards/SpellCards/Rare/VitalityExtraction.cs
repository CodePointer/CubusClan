using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.SpellCards
{
	class VitalityExtraction
	{
		public static string IDName = "Spell_VitalityExtraction";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 2,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = false,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
					},
				},

				EffectBuilders = new List<CardEffectDataBuilder>  // TODO
				{
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
						ParamInt = 8,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
