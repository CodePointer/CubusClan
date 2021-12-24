using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.Effects;

namespace SuccClan.Cards.UnitCards
{
	class AbyssPrincess  // TODO
	{
		public static readonly string IDName = "Unit_AbyssPrincess";
		public static readonly string IDChar = "Unit_AbyssPrincessCharacter";

		public static void Make()
		{
			var charData = BuildUnit();
			BuildUpgrade(charData);

			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 2,
				Rarity = CollectableRarity.Rare,

				TriggerBuilders = new List<CardTriggerEffectDataBuilder>
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
								paramInt = 2,
							},
						},
					},
				},
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
				AttackDamage = 5,
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
								paramInt = 1,
							},
						},
					},
				},

			}.Build();
		}
	}
}
