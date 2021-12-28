using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;
using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class DepressionWhisper
	{
		public static string IDName = "Spell_DepressionWhisper";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = true,

				CardType = CardType.Spell,
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateType = typeof(CardEffectMultiplyStatusCategory),
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
						ParamInt = (int)StatusEffectData.DisplayCategory.Positive,
						ParamMultiplier = 0.0f,
					},
					new CardEffectDataBuilder
					{
						EffectStateType = typeof(CardEffectMultiplyStatusCategory),
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes,
						ParamInt = (int)StatusEffectData.DisplayCategory.Negative,
						ParamMultiplier = 2.0f,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
