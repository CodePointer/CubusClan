using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class CubusSpike
	{
		public static string IDName = "Spell_CubusSpike";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				CostType = CardData.CostType.ConsumeRemainingEnergy,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,

				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
								count = 0,
							},
						},
					},
				},

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitScalingAddStatusEffect.AssemblyQualifiedName,
						ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
						ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
						ParamInt = 2,
						ParamUseScalingParams = true,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = (StatusEffectFrantic.IDName).ToLower(),
								count = 0,
							},
						},
						ParamTeamType = Team.Type.Heroes,
					},
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
