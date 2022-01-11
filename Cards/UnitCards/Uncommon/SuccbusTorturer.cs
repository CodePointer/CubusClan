using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.UnitCards
{
	class SuccbusTorturer
	{
		public static readonly string IDName = "Unit_SuccbusTorturer";
		public static readonly string IDChar = "Unit_SuccbusTorturerCharacter";

		public static void Make()
		{
			var charData = BuildUnit();
			BuildUpgrade(charData);

			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Uncommon,
			};

			Utils.AddUnit(railyard, IDName, charData);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}

		public static CharacterData BuildUnit()
		{
			var charBuilder = new CharacterDataBuilder
			{
				CharacterID = IDChar,
				NameKey = IDName + "_Name",
				SubtypeKeys = new List<string> { "SuccClan_Subtype_Cubus" },

				Size = 2,
				Health = 10,
				AttackDamage = 15,

				TriggerBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnAttacking,
						DescriptionKey = IDName + "_OnAttack_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.LastAttackedCharacter,
								TargetTeamType = Team.Type.Heroes,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										count = 1,
										statusId = StatusEffectMeleeWeaknessState.StatusId,
									}
								}
							}
						}
					}
				}
			};

			Utils.AddUnitImg(charBuilder, IDName + ".png");
			return charBuilder.BuildAndRegister();
		}

		public static void BuildUpgrade(CharacterData charData)
		{
			new CardUpgradeDataBuilder()
			{
				UpgradeTitleKey = IDName + "_Upgrade_Name",
				UpgradeDescriptionKey = IDName + "_Upgrade_Desc",
				SourceSynthesisUnit = charData,

				BonusHP = -10,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnAttacking,
						DescriptionKey = IDName + "_OnAttack_Upgrade_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.LastAttackedCharacter,
								TargetTeamType = Team.Type.Heroes,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										count = 1,
										statusId = StatusEffectMeleeWeaknessState.StatusId,
									}
								}
							}
						}
					}
				},
			}.Build();
		}
	}
}
