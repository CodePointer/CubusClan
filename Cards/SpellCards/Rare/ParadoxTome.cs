using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class ParadoxTome
	{
		public static string IDName = "Spell_ParadoxTome";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 3,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = false,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Monsters,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = VanillaStatusEffectIDs.Sweep,
							}
						},
					},
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
						ParamInt = (int)CardPile.HandPile,  // 0: Draw  1: Discard  3: Hand
						AdditionalParamInt = 3,  // Number of added card
						ParamCardPool = MyCardPools.ObsessingShardPool,
						ShouldTest = false,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
