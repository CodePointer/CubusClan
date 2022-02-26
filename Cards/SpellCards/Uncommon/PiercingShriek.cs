using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class PiercingShriek
	{
		public static string IDName = "Spell_PiercingShriek";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						//TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName,
						TraitStateName = MyCardTraitNames.Consume,
					},
					new CardTraitDataBuilder
					{
						TraitStateName = MyCardTraitNames.Piercing,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectDamage.AssemblyQualifiedName,
						ParamInt = 13,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
