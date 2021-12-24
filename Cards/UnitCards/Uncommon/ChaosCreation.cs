using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;

namespace SuccClan.Cards.UnitCards
{
	class ChaosCreation
	{
		public static readonly string IDName = "Unit_ChaosCreation";
		public static readonly string IDChar = "Unit_ChaosCreationCharacter";

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
				Health = 40,
				AttackDamage = 0,

				TriggerBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnHit,
						DescriptionKey = IDName + "_OnHit_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Heroes,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										count = 1,
										statusId = StatusEffectFrantic.IDName,
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

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnHit,
						DescriptionKey = IDName + "_OnHit_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
								TargetMode = TargetMode.Room,
								TargetTeamType = Team.Type.Heroes,
								ParamStatusEffects = new StatusEffectStackData[]
								{
									new StatusEffectStackData
									{
										count = 1,
										statusId = StatusEffectFrantic.IDName,
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
