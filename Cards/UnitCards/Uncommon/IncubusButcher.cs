using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.UnitCards
{
	class IncubusButcher
	{
		public static readonly string IDName = "Unit_IncubusButcher";
		public static readonly string IDChar = "Unit_IncubusButcherCharacter";

		public static void Make()
		{
			var charData = BuildUnit();
			BuildUpgrade(charData);

			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 2,
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
				Health = 20,
				AttackDamage = 5,

				StartingStatusEffects = new StatusEffectStackData[]
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Sweep,
					},
				},

				TriggerBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnAnyUnitDeathOnFloor,
						DescriptionKey = IDName + "_OnHarvest_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 3,
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffMaxHealth,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 2,
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

				CostReduction = -2,

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Sweep,
					},
				},

			}.Build();
		}
	}
}
