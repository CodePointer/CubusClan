using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.SpellCards
{
	class IllusionTwins
	{
		public static string IDName = "Spell_IllusionTwins";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
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
						EffectStateType = VanillaCardEffectTypes.CardEffectCopyUnits,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Monsters,
						TargetCharacterSubtype = "SubtypesData_None",
						ParamInt = 1
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
