using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.UnitCards
{
	class BlueEyesWhiteDragon
	{
		public static string IDName = "Unit_BlueEyes";
		public static string IDChar = "Unit_BlueEyesCharacter";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Uncommon,
			};

			Utils.AddUnit(railyard, IDName, BuildUnit());
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}

		public static CharacterData BuildUnit()
		{
			CharacterDataBuilder charBuilder = new CharacterDataBuilder
			{
				CharacterID = IDChar,
				NameKey = IDName + "_Name",
				SubtypeKeys = new List<string> { "SuccClan_Subtype_Cubus" },
				PriorityDraw = true,

				Size = 5,
				Health = 250,
				AttackDamage = 300,
			};

			charBuilder.AddStartingStatusEffect(VanillaStatusEffectIDs.Multistrike, 2);

			Utils.AddUnitImg(charBuilder, IDName + ".png");
			return charBuilder.BuildAndRegister();
		}
	}
}
