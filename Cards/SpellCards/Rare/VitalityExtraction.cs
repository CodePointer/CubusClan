using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.CardEffects;

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
				Targetless = true,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName,
					},
					new CardTraitDataBuilder
					{
						TraitStateName = typeof(CardTraitFranticAddStatus).AssemblyQualifiedName,
					},
				},

				EffectBuilders = new List<CardEffectDataBuilder> 
				{
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Monsters,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = VanillaStatusEffectIDs.Regen,
								count = 0,
							}
						},
					},
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Monsters,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = VanillaStatusEffectIDs.Rage,
								count = 0,
							}
						},
					}
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
