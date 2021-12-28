using System;
using System.Collections.Generic;
using System.Text;

using HarmonyLib;
using Trainworks;
using Trainworks.Enums.MTTriggers;
using Trainworks.Managers;
using Trainworks.Constants;

namespace SuccClan.Effects
{
	public static class Trigger_OnFanatic
	{
		public const string IDName = "Trigger_OnFanatic";
		public static CharacterTrigger OnFanaticCharTrigger = new CharacterTrigger(IDName + "_Char");
		public static CardTrigger OnFanaticTrigger = new CardTrigger(IDName);

		static Trigger_OnFanatic()
		{
			Utils.BepLog(new List<string> { "AssociateTriggers. " });
			CustomTriggerManager.AssociateTriggers(OnFanaticTrigger, OnFanaticCharTrigger);
		}
	}

	[HarmonyPatch(typeof(CardManager), "OnCardPlayed")]
	class QueueOnFanaticUpdatePlay
	{
		static void Prefix(CardManager __instance, CardState playCard, int selectedRoom, 
			RoomState roomState, SpawnPoint dropLocation, CharacterState characterSummoned, 
			List<CharacterState> targets, bool discardCard)
		{
			if (playCard.GetCardType() == CardType.Blight 
				|| playCard.GetCardType() == CardType.Junk)
			{
				ProviderManager.TryGetProvider<RoomManager>(out RoomManager roomManager);
				roomManager.GetSelectedRoom();
				int roomIndex = roomManager.GetSelectedRoom();
				if (roomIndex != -1)
				{
					List<CharacterState> charList = new List<CharacterState>();
					ProviderManager.CombatManager.GetMonsterManager().AddCharactersInRoomToList(charList, roomManager.GetSelectedRoom());
					ProviderManager.CombatManager.GetHeroManager().AddCharactersInRoomToList(charList, roomManager.GetSelectedRoom());
					foreach (var unit in charList)
					{
						CustomTriggerManager.QueueTrigger(Trigger_OnFanatic.OnFanaticCharTrigger, unit);
					}
				}
			}
		}
	}

	[HarmonyPatch(typeof(CardManager), "DiscardCard")]
	class QueueOnFanaticUpdateDiscard
	{
		static void Prefix(CardManager.DiscardCardParams discardCardParams, bool fromNaturalPlay)
		{ 
			//Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", new string[]
			//{
		 //  		"DiscardCard", 
			//	"\ndiscardCard: ", discardCardParams.discardCard.ToString(),
			//	"\nwasPlayed", discardCardParams.wasPlayed.ToString(),
			//	"\nhandDiscarded", discardCardParams.handDiscarded.ToString(),
			//	"\ntriggeredByCard", discardCardParams.triggeredByCard.ToString(),
			//}));
			if (!discardCardParams.handDiscarded && !discardCardParams.wasPlayed)
			{
				CardState discardCard = discardCardParams.discardCard;
				if (discardCard.HasTrait(VanillaCardTraitTypes.CardTraitTreasure))
				{
					if (discardCard.GetCardType() == CardType.Blight
						|| discardCard.GetCardType() == CardType.Junk)
					{
						//Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", new string[]
						//{
						//	"DiscardCard", discardCardParams.discardCard.ToString(),
						//}));
						ProviderManager.TryGetProvider<RoomManager>(out RoomManager roomManager);
						roomManager.GetSelectedRoom();
						int roomIndex = roomManager.GetSelectedRoom();
						//Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, string.Join("\t", new string[]
						//{
						//	"RoomIndex", roomIndex.ToString(),
						//}));
						if (roomIndex != -1)
						{
							List<CharacterState> charList = new List<CharacterState>();
							ProviderManager.CombatManager.GetMonsterManager().AddCharactersInRoomToList(charList, roomManager.GetSelectedRoom());
							ProviderManager.CombatManager.GetHeroManager().AddCharactersInRoomToList(charList, roomManager.GetSelectedRoom());
							foreach (var unit in charList)
							{
								CustomTriggerManager.QueueTrigger(Trigger_OnFanatic.OnFanaticCharTrigger, unit);
							}
						}
					}
				}
			}
		}
	}
}
