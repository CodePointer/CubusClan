using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;

namespace SuccClan.Cards.UnitCards
{
	class DemonPioneer
	{
		public static readonly string IDName = "Unit_DemonPioneer";
		public static readonly string IDChar = "Unit_DemonPioneerCharacter";

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

				Size = 3,
				Health = 30,
				AttackDamage = 20,

				TriggerBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = Trigger_OnFanatic.OnFanaticCharTrigger.GetEnum(),
						DescriptionKey = IDName + "_OnFanatic_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 2,
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffMaxHealth,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 5,
							},
						},
					},
					//new CharacterTriggerDataBuilder
					//{
					//	Trigger = CharacterTriggerData.Trigger.OnAnyUnitDeathOnFloor,
					//	DescriptionKey = IDName + "_OnHarvest_Desc",
					//	EffectBuilders = new List<CardEffectDataBuilder>
					//	{
					//		new CardEffectDataBuilder
					//		{
					//			EffectStateType = VanillaCardEffectTypes.CardEffectAddStatusEffect,
					//			TargetMode = TargetMode.Self,
					//			TargetTeamType = Team.Type.Monsters,
					//			ParamStatusEffects = new StatusEffectStackData[]
					//			{
					//				new StatusEffectStackData
					//				{
					//					statusId = VanillaStatusEffectIDs.Lifesteal,
					//					count = 1,
					//				},
					//			},
					//		},
					//	},
					//},
				},
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

				BonusSize = 1,

				TriggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = Trigger_OnFanatic.OnFanaticCharTrigger.GetEnum(),
						DescriptionKey = IDName + "_OnFanatic_Upgrade_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 2,
							},
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectBuffMaxHealth,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
								ParamInt = 5,
							},
						},
					},
				},

			}.Build();
		}
	}
}
