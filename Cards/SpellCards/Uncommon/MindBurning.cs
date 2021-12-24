using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;
using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class MindBurning
	{
		public static string IDName = "Spell_MindBurning";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = false,

				CardType = CardType.Spell,
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Heroes,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
								count = 1,
							},
							new StatusEffectStackData
							{
								statusId = VanillaStatusEffectIDs.Multistrike,
								count = 1,
							}
						}
					},
					//new CardEffectDataBuilder
					//{
					//	EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
					//	ParamInt = (int)CardPile.DeckPileRandom,
					//	AdditionalParamInt = 1,
					//	ParamCardPool = MyCardPools.ObsessingShardPool,
					//},
					new CardEffectDataBuilder
					{
						EffectStateType = typeof(CardEffectSpread),
						ParamInt = 1,
						ShouldTest = false,
					}
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
