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
	class KnightMare
	{
		public static string IDName = "Champion_KnightMare";
		public static string IDChar = "Champion_KnightMareChar";
		
		public static void Make()
		{
			var railyard = new ChampionCardDataBuilder
			{
				Cost = 0,
				Champion = BuildUnit(),
				ChampionIconPath = "ClanAssets/Icon_ClassSelect_KnightMare.png",
				ChampionSelectedCue = "",
				StarterCardData = CustomCardManager.GetCardDataByID(Inception.IDName),
				UpgradeTree = new CardUpgradeTreeDataBuilder
				{
					UpgradeTrees = new List<List<CardUpgradeDataBuilder>>
					{
						new List<CardUpgradeDataBuilder>
						{
							KnightMareBlustBasic.Builder(),
							KnightMareBlustPre.Builder(),
							KnightMareBlustPro.Builder(),
						},
						new List<CardUpgradeDataBuilder>
						{
							KnightMareSiphonBasic.Builder(),
							KnightMareSiphonPre.Builder(),
							KnightMareBlustPro.Builder()
						},
						new List<CardUpgradeDataBuilder>
						{
							KnightMareTraitorBasic.Builder(),
							KnightMareTraitorPre.Builder(),
							KnightMareTraitorPro.Builder()
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

			railyard.BuildAndRegister(1);
		}

		public static CharacterDataBuilder BuildUnit()
		{
			var charBuilder = new CharacterDataBuilder
			{
				CharacterID = IDChar,
				NameKey = IDName + "_Name",

				Size = 2,
				Health = 10,
				AttackDamage = 10,
			};

			Utils.AddUnitImg(charBuilder, IDName + ".png");
			return charBuilder;
		}
	}
}
