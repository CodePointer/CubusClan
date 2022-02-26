using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class InsanityReach
	{
		public static string IDName = "Spell_InsanityReach";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 2,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = MyCardTraitNames.Pyrebound,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectBuffDamage.AssemblyQualifiedName,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
						ParamInt = 10,
					},
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
								count = 3,
							}
						},
					},
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectBump.AssemblyQualifiedName,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
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
