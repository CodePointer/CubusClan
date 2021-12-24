using System;
using System.Collections.Generic;
using System.Text;

using HarmonyLib;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.UnitCards
{
	class EndlessShadow
	{
		public static readonly string IDName = "Unit_EndlessShadow";
		public static readonly string IDChar = "Unit_EndlessShadowCharacter";

		public static void Make()
		{
			var charData = BuildUnit();

			CardEffectData effectData = charData.GetTriggers()[0].GetEffects()[0];
			Traverse.Create(effectData).Field("paramCharacterData").SetValue(charData); // TODO: 可能还是需要写一个自己的Effect。

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
				Health = 5,
				AttackDamage = 10,

				TriggerBuilders = new List<CharacterTriggerDataBuilder>
				{
					new CharacterTriggerDataBuilder
					{
						Trigger = CharacterTriggerData.Trigger.OnDeath,
						DescriptionKey = IDName + "_OnDeath_Desc",
						EffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
								ParamInt = 1,
								ParamCardUpgradeData = new CardUpgradeDataBuilder
								{
									BonusHP = -5,
								}.Build(),
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

				StatusEffectUpgrades = new List<StatusEffectStackData>
				{
					new StatusEffectStackData
					{
						statusId = VanillaStatusEffectIDs.Endless,
					},
				},

			}.Build();
		}
	}
}
