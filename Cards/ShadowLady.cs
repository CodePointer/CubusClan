using System;
using System.Collections.Generic;
using System.Text;

using Trainworks;
using Trainworks.Builders;
using Trainworks.Managers;
using Trainworks.Constants;

using SuccClan.Cards.Upgrades;
using SuccClan.Cards.SpellCards;


namespace SuccClan.Cards
{
	class ShadowLady
	{
		public static string IDName = "Champion_ShadowLady";
		public static string IDChar = "Champion_ShadowLadyChar";
		
		public static void Make()
		{
			var railyard = new ChampionCardDataBuilder
			{
				Cost = 0,
				Champion = BuildUnit(),
				ChampionIconPath = "ClanAssets/Icon_ClassSelect_ShadowLady.png",
				ChampionSelectedCue = "",
				StarterCardData = CustomCardManager.GetCardDataByID(Flogging.IDName),
				UpgradeTree = new CardUpgradeTreeDataBuilder
				{
					UpgradeTrees = new List<List<CardUpgradeDataBuilder>>
					{
						new List<CardUpgradeDataBuilder>
						{
							ShadowLadyBloodThurstyBasic.Builder(),
							ShadowLadyBloodThurstyPre.Builder(),
							ShadowLadyBloodThurstyPro.Builder(),
						},
						new List<CardUpgradeDataBuilder>
						{
							ShadowLadyPlagueBasic.Builder(),
							ShadowLadyPlaguePre.Builder(),
							ShadowLadyPlaguePro.Builder(),
						},
						new List<CardUpgradeDataBuilder>
						{
							ShadowLadyProfanityBasic.Builder(),
							ShadowLadyProfanityPre.Builder(),
							ShadowLadyProfanityPro.Builder(),
						},
					},
				},

				CardID = IDName,
				NameKey = IDName + "_Name",
				OverrideDescriptionKey = IDName + "_Desc",
				LinkedClass = SuccClanPlugin.getClan(),
				ClanID = Clan.IDName,

				CardPoolIDs = new List<string> { "Succubus", VanillaCardPoolIDs.UnitsAllBanner },
				CardType = CardType.Monster,
				TargetsRoom = true,

				AssetPath = Utils.rootPath + Utils.ucardPath,
			};

			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}

		public static CharacterDataBuilder BuildUnit()
		{
			var charBuilder = new CharacterDataBuilder
			{
				CharacterID = IDChar,
				NameKey = IDName + "_Name",

				Size = 3,
				Health = 30,
				AttackDamage = 10,
			};

			Utils.AddUnitImg(charBuilder, IDName + ".png");
			return charBuilder;
		}
	}
}
