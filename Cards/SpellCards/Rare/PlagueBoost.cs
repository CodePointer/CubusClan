using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.SpellCards
{
	class PlagueBoost
	{
		public static string IDName = "Spell_PlagueBoost";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = true,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
					},
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
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
