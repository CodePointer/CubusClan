using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;
using SuccClan.CardEffects;

namespace SuccClan.Cards.UnitCards
{
	class ShadowWarrior
	{
		public static readonly string IDName = "Unit_ShadowWarrior";
		public static readonly string IDChar = "Unit_ShadowWarriorCharacter";

		public static void Make()
		{
			var charData = BuildUnit();
			BuildUpgrade(charData);

			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Rare,
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
				Health = 1,
				AttackDamage = 5,

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
								EffectStateType = typeof(CardEffectUpgradePermanent),
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusDamage = 2,
								}.Build(),
							},
						},
					},
				},

				//StartingStatusEffects = new StatusEffectStackData[]
				//{
				//	new StatusEffectStackData
				//	{
				//		statusId = VanillaStatusEffectIDs.Endless,
				//	},
				//},
			};

			Utils.AddUnitImg(charBuilder, IDName + ".png");
			return charBuilder.BuildAndRegister();
		}

		public static void BuildUpgrade(CharacterData charData)
		{
			new CardUpgradeDataBuilder
			{
				UpgradeTitleKey = IDName + "_Upgrade_Name",
				UpgradeDescriptionKey = IDName + "_Upgrade_Desc",
				SourceSynthesisUnit = charData,

				CardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder>
				{
					new CardTriggerEffectDataBuilder
					{
						Trigger = Trigger_OnFanatic.OnFanaticTrigger.GetEnum(),
						DescriptionKey = IDName + "_OnFanatic_Desc",
						CardTriggerEffects = new List<CardTriggerData>
						{
							new CardTriggerData
							{
								persistenceMode = PersistenceMode.SingleRun,
								cardTriggerEffect = "CardTriggerEffectBuffCharacterDamage",
								buffEffectType = "None",
								paramInt = 2,
							},
						},
					},
				},

			}.Build();
		}
	}
}
