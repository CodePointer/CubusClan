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

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
					},
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitScalingAddStatusEffect,
						ParamTrackedValue = CardStatistics.TrackedValueType.PlayedCost,
						ParamEntryDuration = CardStatistics.EntryDuration.ThisBattle,
						ParamInt = 2,
						ParamUseScalingParams = true,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
							},
						},
						ParamTeamType = Team.Type.Heroes,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
							},
						}
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
