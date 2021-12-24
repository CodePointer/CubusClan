using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Builders;
using Trainworks.Constants;


namespace SuccClan.Cards.SpellCards
{
	class NotHornBreak
	{
		public static string IDName = "Spell_NotHornBreak";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Common,
				TargetsRoom = true,
				Targetless = false,

				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
						ParamInt = 5,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
					},
				},

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitIgnoreArmor,
					},
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitFreeze,
					},
				}
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
