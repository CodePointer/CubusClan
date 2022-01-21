using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class DarkFury
	{
		public static string IDName = "Spell_DarkFury";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateName = typeof(CardEffectMultiplyDamage).AssemblyQualifiedName,
						ParamInt = 2,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
					},
					new CardEffectDataBuilder
					{
						EffectStateName = typeof(CardEffectMultiplyMaxHealth).AssemblyQualifiedName,
						ParamInt = -2,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
