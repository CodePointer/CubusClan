using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class ForTheQueen
	{
		public static string IDName = "Spell_ForTheQueen";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = false,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{ 
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
					},
					new CardTraitDataBuilder
					{
						TraitStateType = typeof(CardTraitRewardGold),
						ParamInt = 50,
					},
				},

				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectSacrifice,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Monsters,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
