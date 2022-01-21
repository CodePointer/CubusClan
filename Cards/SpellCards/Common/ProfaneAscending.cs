using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class ProfaneAscending
	{
		public static string IDName = "Spell_ProfaneAscending";
		public static CardPool cardPool;

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Common,
				TargetsRoom = true,
				Targetless = false,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName,
					},
				},

				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectBump.AssemblyQualifiedName,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Heroes,
						ParamInt = -100,
					},
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName,
						TargetMode = TargetMode.LastTargetedCharacters,
						TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
								count = 3
							},
						},
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();

			cardPool = new CardPoolBuilder
			{
				CardPoolID = IDName + "_CardPool",
				CardIDs = new List<string>
				{
					IDName,
				},
			}.BuildAndRegister();
		}
	}
}
