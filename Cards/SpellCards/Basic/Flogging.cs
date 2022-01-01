using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class Flogging
	{
		public static string IDName = "Spell_Flogging";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Starter,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
						ParamInt = 2,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes
					},
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
						ParamInt = (int)CardPile.DeckPileRandom,
						AdditionalParamInt = 1,  // Number of added card
						ParamCardPool = MyCardPools.ObsessingShardPool,
						ShouldTest = false,
					},
					//new CardEffectDataBuilder
					//{
					//	EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
					//	TargetMode = TargetMode.Room,
					//	TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
					//	ParamStatusEffects = new StatusEffectStackData[]
					//	{
					//		new StatusEffectStackData
					//		{
					//			statusId = StatusEffectFrantic.IDName,
					//			count = 2
					//		},
					//	}
					//},
					//new CardEffectDataBuilder
					//{
					//	EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
					//	TargetMode = TargetMode.Room,
					//	TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
					//	ParamStatusEffects = new StatusEffectStackData[]
					//	{
					//		new StatusEffectStackData
					//		{
					//			statusId = VanillaStatusEffectIDs.Multistrike,
					//			count = 2
					//		},
					//	}
					//},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
