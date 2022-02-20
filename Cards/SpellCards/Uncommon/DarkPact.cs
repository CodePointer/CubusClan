using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class DarkPact
	{
		public static string IDName = "Spell_DarkPact";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddBattleCard.AssemblyQualifiedName,
						ParamInt = (int)CardPile.DeckPileRandom,
						AdditionalParamInt = 2,
						ParamCardPool = MyCardPools.VengefulShardPool,
						ParamCardUpgradeData = MyCardPools.exhaustUpgradeData,
					},
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectGainEnergyEveryTurn.AssemblyQualifiedName,
						ParamInt = 1,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
