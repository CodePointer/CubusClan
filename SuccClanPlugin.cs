using System;
using System.Collections.Generic;
using System.Text;

using BepInEx;
using HarmonyLib;
using Trainworks.Interfaces;
using Trainworks.Managers;
using Trainworks.Constants;

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
		public const string VERSION = "0.1.2";
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
				ChaosCreation.IDName,
				DemonPioneer.IDName,
				IncubusButcher.IDName,
				SuccbusTorturer.IDName,
				Vrolikai.IDName,
				Oolioddroo.IDName,

				EndlessShadow.IDName,
				ShadowWarrior.IDName,
				AbyssPrincess.IDName,
			};

			CustomLocalizationManager.ImportCSV("SuccClan.csv", ',');
			clanRef = Clan.Make();
			RegisterSubtypes();
			MakeStatuses();

			MakeCardPools();

			MakeCards();

			Trainworks.Patches.AccessUnitSynthesisMapping.FindUnitSynthesisMappingInstanceToStub();
			ShadowLady.Make();
			KnightMare.Make();
			Clan.RegisterBanner(unitIDList);

			MakeRelics();

			// Fix Magic TODO: Is this necessary?
			AddToMagicPowerUpgradeList(VanillaCardEffectTypes.CardEffectDamage.AssemblyQualifiedName);
			AddToDoubleStackEnhancerList(VanillaCardEffectTypes.CardEffectAddStatusEffect.AssemblyQualifiedName);

			//ProviderManager.SaveManager.GetMetagameSave().SetLevelAndXP(clanRef.GetID(), 5, 99999);
			//Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, "OUTPUTHERE: " + typeof(CardEffectDamage).AssemblyQualifiedName);
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
			StatusEffectPsionic.Make();
			//StatusEffectSoulBlust.Make();
		}

		static void MakeCardPools()
		{
			//ObsessingShard.Make();
			MyCardPools.Make();
		}

		static void MakeCards()
		{
			//Ascending Cards for Shadow Lady
			
			//ProfaneAscendingPlus.Make();

			// Spell Cards: Basic
			Flogging.Make();
			//AddToMagicPowerUpgradeList(Flogging.IDName);
			Inception.Make();
			//AddToDoubleStackEnhancerList(Inception.IDName);
			//MyCardPools.MakeLater();

			//Spell Cards: Common
			BloodCarnival.Make();
			DreadShot.Make();
			//AddToMagicPowerUpgradeList(DreadShot.IDName);
			//AddToDoubleStackEnhancerList(DreadShot.IDName);
			MindDomaination.Make();
			//AddToDoubleStackEnhancerList(MindDomaination.IDName);
			PainAndPleasure.Make();
			ProfaneAscending.Make();
			//AddToDoubleStackEnhancerList(ProfaneAscending.IDName);

			// Spell Cards: Uncommon
			DangerousGame.Make();
			//AddToDoubleStackEnhancerList(DangerousGame.IDName);
			DarkFury.Make();
			DarkPact.Make();
			ForTheQueen.Make();
			MindBurning.Make();
			//AddToDoubleStackEnhancerList(MindBurning.IDName);
			PiercingShriek.Make();
			//AddToMagicPowerUpgradeList(PiercingShriek.IDName);
			PowerSiphon.Make();
			//AddToMagicPowerUpgradeList(PowerSiphon.IDName);
			ShadowEmbrace.Make();

			// Spell Cards: Rare
			CubusSpike.Make();
			//AddToDoubleStackEnhancerList(CubusSpike.IDName);
			DepressionWhisper.Make();
			IllusionTwins.Make();
			InsanityReach.Make();
			//AddToDoubleStackEnhancerList(InsanityReach.IDName);
			ParadoxTome.Make();
			PlagueBoost.Make();
			VitalityExtraction.Make();

			//// Debug
			//NotHornBreak.Make();
			//GiveEveryoneArmor.Make();

			// Subunit Cards
			ArroganceGhost.Make();
			EnvyGhost.Make();
			GluttonyGhost.Make();
			GreedGhost.Make();
			LustGhost.Make();
			SlothGhost.Make();
			WrathGhost.Make();

			// Unit cards: Uncommon
			ChaosCreation.Make();
			DemonPioneer.Make();
			IncubusButcher.Make();
			SuccbusTorturer.Make();
			Vrolikai.Make();
			Oolioddroo.Make();

			// Unit cards: Rare
			AbyssPrincess.Make();
			ShadowWarrior.Make();
			EndlessShadow.Make();
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

		static void AddToMagicPowerUpgradeList(string cardID)
		{
			// (11593 SpellMagicPowerBigExtraCost -> +20/Consume)
			var enhancerData = ProviderManager.SaveManager.GetAllGameData().FindEnhancerData("07de18ca-a585-4200-b139-63c5d4661140");
			var filter = enhancerData.GetEffects()[0].GetParamCardUpgradeData().GetFilters()[0];
			var list = Traverse.Create(filter).Field("requiredCardEffects").GetValue<List<string>>();
			list.Add(cardID);

			// (11592 SpellMagicPower -> +10)
			enhancerData = ProviderManager.SaveManager.GetAllGameData().FindEnhancerData("015f4d9d-3a87-4053-8e30-45a80fdf78ee");
			filter = enhancerData.GetEffects()[0].GetParamCardUpgradeData().GetFilters()[0];
			list = Traverse.Create(filter).Field("requiredCardEffects").GetValue<List<string>>();
			list.Add(cardID);
		}

		static void AddToDoubleStackEnhancerList(string cardID)
		{
			// (11920 SpellUpgradeTraitAddJuice -> Doublestack)
			var enhancerData = ProviderManager.SaveManager.GetAllGameData().FindEnhancerData("72f61ae8-7e0f-4066-a3fb-a1273f3aa273");
			var filter = enhancerData.GetEffects()[0].GetParamCardUpgradeData().GetFilters()[0];
			var list = Traverse.Create(filter).Field("requiredCardEffects").GetValue<List<string>>();
			list.Add(cardID);
		}
	}
}
