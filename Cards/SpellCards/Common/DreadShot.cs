using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Effects;

namespace SuccClan.Cards.SpellCards
{
	class DreadShot
	{
		public static string IDName = "Spell_DreadShot";

		public static void Make()
		{
			var railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Common,
				TargetsRoom = true,
				Targetless = false,

				CardType = CardType.Spell,
				
				EffectBuilders = new List<CardEffectDataBuilder>
				{ 
					new CardEffectDataBuilder
					{
						EffectStateName = VanillaCardEffectTypes.CardEffectDamage.AssemblyQualifiedName,
						ParamInt = 2,
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Heroes | Team.Type.Monsters
					},
				},

				TriggerBuilders = new List<CardTriggerEffectDataBuilder>
				{ 
					new CardTriggerEffectDataBuilder
					{
						Trigger = CardTriggerType.OnKill,
						DescriptionKey = IDName + "_OnKill_Desc",
						CardEffects = new List<CardEffectData>
						{
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
										count = 1
									},
								},
							}.Build(),
						},
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");
			railyard.BuildAndRegister();
		}
	}
}
