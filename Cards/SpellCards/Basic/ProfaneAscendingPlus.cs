using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class ProfaneAscendingPlus
	{
		public static string IDName = "Spell_ProfaneAscendingPlus";
		public static CardPool cardPool;

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Starter,
				TargetsRoom = true,
				Targetless = false,

				CardType = CardType.Spell,

				TraitBuilders = new List<CardTraitDataBuilder>
				{ 
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitSelfPurge,
					},
				},
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectBump,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Heroes,
						ParamInt = -100,
					},
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
						TargetMode = TargetMode.LastTargetedCharacters,
						TargetTeamType = Team.Type.Heroes,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = StatusEffectFrantic.IDName,
								count = 2
							},
						},
					},
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
						TargetMode = TargetMode.LastTargetedCharacters,
						TargetTeamType = Team.Type.Heroes,
						ParamInt = 5,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();

			cardPool = new CardPoolBuilder
			{
				CardPoolID = IDName + "_CardPool",
				CardIDs = new List<string>
				{
					IDName,
				},
			}.BuildAndRegister();
		}
	}
}
