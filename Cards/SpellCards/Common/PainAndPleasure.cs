using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class PainAndPleasure
	{
		public static string IDName = "Spell_PainAndPleasure";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Common,
				TargetsRoom = true,
				Targetless = true,

				//TraitBuilders = new List<CardTraitDataBuilder>
				//{
				//	new CardTraitDataBuilder
				//	{
				//		TraitStateType = VanillaCardTraitTypes.CardTraitRetain,
				//	},
				//	new CardTraitDataBuilder
				//	{
				//		TraitStateType = VanillaCardTraitTypes.CardTraitIntrinsicState,
				//	},
				//},

				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateName = typeof(CardEffectDiscardBlightAndDraw).AssemblyQualifiedName,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
