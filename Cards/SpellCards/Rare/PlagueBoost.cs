using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.CardEffects;

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
						TraitStateName = MyCardTraitNames.Frozen,
					},
					new CardTraitDataBuilder
					{
						TraitStateName = MyCardTraitNames.Consume,
					},
					new CardTraitDataBuilder
					{
						TraitStateName = typeof(CardTraitBlightAddEnergy).AssemblyQualifiedName,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
