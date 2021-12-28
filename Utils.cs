using System;
using System.Collections.Generic;
using System.Text;

using Trainworks;
using Trainworks.Builders;
using Trainworks.Utilities;
using Trainworks.Constants;

namespace SuccClan
{
	class Utils
	{
		public static string rootPath = "";
		public static string ucardPath = "CardAssets/UnitCardArt/";
		public static string scardPath = "CardAssets/SpellCardArt/";
		public static string unitPath = "CardAssets/UnitPortrait/";
		public static string relicPath = "Relic/";

		public static void AddSpell(CardDataBuilder r, string IDName)
		{
			r.CardID = IDName;
			r.NameKey = IDName + "_Name";
			r.OverrideDescriptionKey = IDName + "_Desc";
			r.LinkedClass = SuccClanPlugin.getClan();

			r.ClanID = Clan.IDName;
			r.CardPoolIDs = new List<string> { "Succubus", VanillaCardPoolIDs.MegaPool };

			r.AssetPath = rootPath + scardPath;

			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", new string[] 
			{ 
				"SpellCardAdded", r.NameKey.Localize(), r.Rarity.ToString(), r.Cost.ToString(), 
				r.OverrideDescriptionKey.Localize() 
			}));
		}

		public static void AddSpellWithoutPool(CardDataBuilder r, string IDName)
		{
			r.CardID = IDName;
			r.NameKey = IDName + "_Name";
			r.OverrideDescriptionKey = IDName + "_Desc";
			r.LinkedClass = SuccClanPlugin.getClan();

			r.ClanID = Clan.IDName;
			//r.CardPoolIDs = new List<string> { "Succubus", VanillaCardPoolIDs.MegaPool };  // TODO

			r.AssetPath = rootPath + scardPath;

			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", new string[]
			{
				"OtherCardAdded", r.NameKey.Localize(), r.Rarity.ToString(), r.Cost.ToString(),
				r.OverrideDescriptionKey.Localize()
			}));
		}

		public static void AddUnit(CardDataBuilder r, string IDName, CharacterData character)
		{
			r.CardID = IDName;
			r.NameKey = IDName + "_Name";
			r.OverrideDescriptionKey = IDName + "_Desc";
			r.LinkedClass = SuccClanPlugin.getClan();

			r.ClanID = Clan.IDName;
			r.CardPoolIDs = new List<string> { "Succubus", VanillaCardPoolIDs.UnitsAllBanner };
			r.CardType = CardType.Monster;
			r.TargetsRoom = true;

			r.AssetPath = rootPath + ucardPath;
			r.EffectBuilders.Add(
				new CardEffectDataBuilder
				{
					EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
					TargetMode = TargetMode.DropTargetCharacter,
					ParamCharacterData = character,
				});

			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", new string[]
			{
				"UnitCardAdded", r.NameKey.Localize(), r.Rarity.ToString(), r.Cost.ToString(),
				character.GetSize().ToString(), character.GetHealth().ToString(),
				character.GetAttackDamage().ToString(), character.GetLocalizedSubtype(),
				r.OverrideDescriptionKey.Localize()
			}));
		}

		public static void AddRelic(CollectableRelicDataBuilder r, string ID)
		{
			r.CollectableRelicID = ID;
			r.NameKey = ID + "_Name";
			r.DescriptionKey = ID + "_Desc";
			r.RelicActivatedKey = ID + "_Active";
			r.RelicLoreTooltipKeys = new List<string> { ID + "_Lore" };
			r.ClanID = Clan.IDName;
			r.Rarity = CollectableRarity.Uncommon;
			r.IsBossGivenRelic = false;
		}

		public static void AddImg(CardDataBuilder r, string imgName)
		{
			r.AssetPath = r.AssetPath + imgName;
		}

		public static void AddUnitImg(CharacterDataBuilder r, string imgName)
		{
			r.AssetPath = rootPath + unitPath + imgName;
		}

		public static void BepLog(List<string> outputStrs)
		{
			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", outputStrs));
		}
	}
}
