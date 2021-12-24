using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.UnitCards
{
	class Vrolikai
	{
		public static readonly string IDName = "Unit_Vrolikai";
		public static readonly string IDChar = "Unit_VrolikaiCharacter";

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
				Health = 6,
				AttackDamage = 6,

				StartingStatusEffects = new StatusEffectStackData[]
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Multistrike,
						count = 3,
					},
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.HealImmunity,
					},
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

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Multistrike,
						count = 1,
					},
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.HealImmunity,
					},
				},

			}.Build();
		}
	}
}
