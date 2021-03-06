using System;
using System.Collections.Generic;
using System.Text;

using HarmonyLib;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.CardEffects;

namespace SuccClan.Cards.UnitCards
{
	class EndlessShadow
	{
		public static readonly string IDName = "Unit_EndlessShadow";
		public static readonly string IDChar = "Unit_EndlessShadowCharacter";

		public static void Make()
		{
			var charData = BuildUnit();

			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Rare,
			};

			Utils.AddUnit(railyard, IDName, charData);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();

			BuildUpgrade(charData);
		}

		public static CharacterData BuildUnit()
		{
			var charBuilder = new CharacterDataBuilder
			{
				CharacterID = IDChar,
				NameKey = IDName + "_Name",
				SubtypeKeys = new List<string> { "SuccClan_Subtype_Cubus" },

				Size = 2,
				Health = 15,
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
								EffectStateName = typeof(CardEffectSpawnSelf).AssemblyQualifiedName,
								ParamInt = 1,
								ParamBool = true,
								TargetMode = TargetMode.Self,
								TargetTeamType = Team.Type.Monsters,
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
				//UpgradeTitle = "123",
				UpgradeTitleKey = IDName + "_Upgrade_Name",
				//UpgradeDescription = "456",
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
