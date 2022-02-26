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
						TraitStateName = MyCardTraitNames.Attuned,
						ParamInt = 5,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectDamage.AssemblyQualifiedName,
						ParamInt = 30,
						TargetMode = TargetMode.FrontInRoom,
						TargetTeamType = Team.Type.Heroes,
					},
					new CardEffectDataBuilder
					{
						EffectStateName = typeof(CardEffectSpread).AssemblyQualifiedName,
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
