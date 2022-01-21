using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class ShadowEmbrace
	{
		public static string IDName = "Spell_ShadowEmbrace";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddBattleCard.AssemblyQualifiedName,
						ParamInt = (int)CardPile.DiscardPile,
						AdditionalParamInt = 2,
						ParamCardPool = MyCardPools.VengefulShardPool,
						ParamCardUpgradeData = MyCardPools.exhaustUpgradeData,
					},
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectDraw.AssemblyQualifiedName,
						ParamInt = 2,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
