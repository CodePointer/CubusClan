using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.SpellCards
{
	class GiveEveryoneArmor
	{
		public static string IDName = "Spell_GiveEveryoneArmor";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Uncommon,
				TargetsRoom = true,
				Targetless = true,

				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Monsters | Team.Type.Heroes,
						ParamStatusEffects = new StatusEffectStackData[]
						{
							new StatusEffectStackData
							{
								statusId = VanillaStatusEffectIDs.Armor,
								count = 3
							}
						}
					}
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
