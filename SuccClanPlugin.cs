using System;
using System.Collections.Generic;
using System.Text;

using BepInEx;
using HarmonyLib;
using Trainworks.Interfaces;
using Trainworks.Managers;

using SuccClan.Cards;
using SuccClan.Cards.SpellCards;
using SuccClan.Cards.UnitCards;
using SuccClan.Relics;
using SuccClan.Effects;

namespace SuccClan
{
	[BepInPlugin(GUID, NAME, VERSION)]
	[BepInProcess("MonsterTrain.exe")]
	[BepInProcess("MtLinkHandler.exe")]
	[BepInDependency("tools.modding.trainworks")]
	public class SuccClanPlugin : BaseUnityPlugin, IInitializable
	{
		public const string GUID = "com.name.package.succclan-mod";
		public const string NAME = "Succubus Clan";
		public const string VERSION = "0.1.0";
		public const string ClanName = "SuccClan";

		public static ClassData clanRef;

		private void Awake()
		{
			var harmony = new Harmony(GUID);
			harmony.PatchAll();
		}

		public void Initialize()
		{
			var unitIDList = new List<string>
			{
				//ChaosCreation.IDName,
				//DemonPioneer.IDName,
				//IncubusButcher.IDName,
				//SuccbusTorturer.IDName,
				//Vrolikai.IDName,

				//EndlessShadow.IDName,
				//ShadowWarrior.IDName,
				//AbyssPrincess.IDName,
			};

			CustomLocalizationManager.ImportCSV("SuccClan.csv", ',');
			clanRef = Clan.Make();
			RegisterSubtypes();
			MakeStatuses();

			MakeCardPools();

			MakeCards();

			AccessUnitSynthesisMapping.FindUnitSynthesisMappingInstanceToStub();
			ShadowLady.Make();
			KnightMare.Make();
			Clan.RegisterBanner(unitIDList);

			MakeRelics();

			//ProviderManager.SaveManager.GetMetagameSave().SetLevelAndXP(clanRef.GetID(), 5, 99999);
			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, "OUTPUTHERE");
		}

		public static ClassData getClan()
		{
			return clanRef;
		}

		public static void RegisterSubtypes()
		{
			CustomCharacterManager.RegisterSubtype(ClanName + "_Subtype_Cubus");
			CustomCharacterManager.RegisterSubtype(ClanName + "_Subtype_Ghost");
		}

		static void MakeStatuses()
		{
			StatusEffectFrantic.Make();
			StatusEffectSoulEnchant.Make();
		}

		static void MakeCardPools()
		{
			ObsessingShard.Make();
			MyCardPools.Make();
		}

		static void MakeCards()
		{
			//Ascending Cards for Shadow Lady
			ProfaneAscending.Make();
			ProfaneAscendingPlus.Make();

			// Spell Cards: Basic
			Flogging.Make();
			Inception.Make();
			MyCardPools.MakeLater();

			// Spell Cards: Common
			//BloodCarnival.Make();
			//DreadShot.Make();
			//MindDomaination.Make();
			//PainAndPleasure.Make();

			//// Spell Cards: Uncommon
			//DangerousGame.Make();
			//DarkFury.Make();
			//DarkPact.Make();
			//ForTheQueen.Make();
			//MindBurning.Make();
			//PiercingShriek.Make();
			//PowerSiphon.Make();
			//ShadowEmbrace.Make();

			//// Spell Cards: Rare
			//CubusSpike.Make();
			//DepressionWhisper.Make();
			//IllusionTwins.Make();
			//InsanityReach.Make();
			//ParadoxTome.Make();
			//PlagueBoost.Make();
			//VitalityExtraction.Make();

			// Debug
			//NotHornBreak.Make();
			//GiveEveryoneArmor.Make();

			// Subunit Cards
			//ArroganceGhost.Make();
			//EnvyGhost.Make();
			//GluttonyGhost.Make();
			//GreedGhost.Make();
			//LustGhost.Make();
			//SlothGhost.Make();
			//WrathGhost.Make();

			// Unit cards: Uncommon
			//ChaosCreation.Make();
			//DemonPioneer.Make();
			//IncubusButcher.Make();
			//SuccbusTorturer.Make();
			//Vrolikai.Make();

			// Unit cards: Rare
			//AbyssPrincess.Make();
			//ShadowWarrior.Make();
			EndlessShadow.Make2();
		}

		static void MakeRelics()
		{
			DesireCrystal.Make();

			AbyssCrown.Make();
			DemonBlood.Make();
			FlareRibbon.Make();
			FleshRing.Make();
			MutantElixirs.Make();
			NetherBlossom.Make();
			ObsessingAromatherapy.Make();
			PoisonSerum.Make();
			ProfaneCrossbow.Make();
			ShadowCloak.Make();
		}
	}
}
