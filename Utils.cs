using System;
using System.Collections.Generic;
using System.Text;

using Trainworks;
using Trainworks.Builders;
using Trainworks.Utilities;
using Trainworks.Constants;

namespace SuccClan
{
	public static class MyCardTraitNames
	{
		public static string Consume = "CardTraitExhaustState";
		public static string Piercing = "CardTraitIgnoreArmor";
		public static string ScalingAddStatusEffect = "CardTraitScalingAddStatusEffect";
		public static string Frozen = "CardTraitFreeze";
		public static string Pyrebound = "CardTraitLimitedRange";
		public static string Attuned = "CardTraitStrongerMagicPower";
	}

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
			r.CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool };

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

		public static void AddUnit(CardDataBuilder r, string IDName, CharacterData character, bool subunit=false)
		{
			r.CardID = IDName;
			r.NameKey = IDName + "_Name";
			r.OverrideDescriptionKey = IDName + "_Desc";
			r.LinkedClass = SuccClanPlugin.getClan();

			r.ClanID = Clan.IDName;
			if (subunit)
			{
				r.CardPoolIDs = new List<string> { VanillaCardPoolIDs.MegaPool };
			}
			else
			{
				r.CardPoolIDs = new List<string> { VanillaCardPoolIDs.UnitsAllBanner };
			}
			r.CardType = CardType.Monster;
			r.TargetsRoom = true;

			r.AssetPath = rootPath + ucardPath;
			r.EffectBuilders.Add(
				new CardEffectDataBuilder
				{
					EffectStateName = "CardEffectSpawnMonster",
					//EffectStateType = VanillaCardEffectTypes.CardEffectSpawnMonster,
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

		public static void BepLog(string outputStr)
		{
			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, outputStr);
		}
	}
}
