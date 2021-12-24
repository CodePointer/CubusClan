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
						EffectStateType = VanillaCardEffectTypes.CardEffectAddBattleCard,
						ParamInt = (int)CardPile.DiscardPile,
						AdditionalParamInt = 2,
						ParamCardPool = MyCardPools.ObsessingShardPool,
					},
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectDraw,
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
