using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class PowerSiphon
	{
		public static string IDName = "Spell_PowerSiphon";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 2,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitStrongerMagicPower,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectDamage,
						ParamInt = 30,
						TargetMode = TargetMode.FrontInRoom,
						TargetTeamType = Team.Type.Heroes,
					},
					new CardEffectDataBuilder
					{
						EffectStateType = typeof(CardEffectSpread),
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
