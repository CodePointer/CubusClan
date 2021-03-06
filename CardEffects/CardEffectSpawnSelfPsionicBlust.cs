using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using Trainworks.Constants;

using SuccClan.Effects;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectSpawnSelfPsionicBlust : CardEffectBase, ICardEffectStatuslessTooltip
	{
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			// Check if psionic stack is larger than cost
			int psionicCost = cardEffectState.GetParamInt();
			int psionicStackNum = cardEffectParams.selfTarget.GetStatusEffectStacks(StatusEffectPsionic.IDName);
			if (psionicStackNum < psionicCost)
			{
				return false;
			}

			// Check if monster can alive
			CardUpgradeData paramCardUpgradeData = cardEffectState.GetParamCardUpgradeData();
			if (paramCardUpgradeData != null)
			{
				int bounsMaxHP = paramCardUpgradeData.GetBonusHP();
				int currentMaxHP = cardEffectParams.selfTarget.GetMaxHP();
				//Utils.BepLog(new List<string> { "HP check: ", bounsMaxHP.ToString(), currentMaxHP.ToString() });
				if (currentMaxHP + bounsMaxHP <= 0)
				{
					return false;
				}
			}

			RoomState selectedRoom = cardEffectParams.GetSelectedRoom();
			return selectedRoom == null || selectedRoom.IsRoomEnabled();
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int psionicCost = cardEffectState.GetParamInt();
			if (psionicCost > 0)
			{
				cardEffectParams.selfTarget.RemoveStatusEffect(StatusEffectPsionic.IDName, false, psionicCost, true, cardEffectParams.sourceRelic, null);
			}

			SaveManager saveManager = cardEffectParams.saveManager;
			SpawnMode spawnMode = SpawnMode.SelectedSlot;
			RoomState room = cardEffectParams.GetSelectedRoom();
			CharacterState selfCharacter = cardEffectParams.selfTarget;
			CharacterData selfCharacterData = selfCharacter.GetSourceCharacterData();

			SpawnPoint fromMonsterSpawnPoint = selfCharacter.GetSpawnPoint(true);
			int spawnIndex = fromMonsterSpawnPoint.GetIndexInRoom();
			int num = room.ShiftSpawnPoints(Team.Type.Monsters, spawnIndex);
			spawnIndex = Mathf.Max(spawnIndex - num, 0);
			//Utils.BepLog(new List<string> 
			//{ 
			//	"spawnIndex: " + spawnIndex.ToString(),
			//	"fromMonsterSpawnPoint: " + fromMonsterSpawnPoint.ToString(),
			//});


			if (!saveManager.PreviewMode && num > 0)
			{
				yield return cardEffectParams.roomManager.GetRoomUI().CenterCharacters(room, fromMonsterSpawnPoint != null, false, true);
			}

			RelicManager relicManager = cardEffectParams.relicManager;

			int numSpawns = Mathf.Min(1, room.GetRemainingSpawnPointCount(Team.Type.Monsters));
			int i = spawnIndex;

			while (i < spawnIndex + numSpawns && i < saveManager.GetNumSpawnPointsPerFloor())
			{
				SpawnPoint spawnPoint = null;

				spawnPoint = room.GetMonsterPoint(i);

				CardState spawnerCard = cardEffectParams.playedCard;
				if (fromMonsterSpawnPoint != null && spawnerCard != null)
				{
					spawnerCard = CardManager.CopyCardState(spawnerCard, cardEffectParams.relicManager, saveManager, 
						cardEffectParams.allGameData, cardEffectParams.combatManager.GetCardManager().GetCardStatistics());
				}

				List<string> startingStatusEffects = new List<string> { "cardless" };

				CharacterState newMonster = null;
				yield return cardEffectParams.monsterManager.CreateMonsterState(selfCharacterData, spawnerCard, cardEffectParams.selectedRoom, delegate (CharacterState character)
				{
					newMonster = character;
					if (newMonster != null)
					{
						CharacterHelper.CopyCharacterStats(newMonster, selfCharacter);
						newMonster.SetAttackDamage(selfCharacter.GetAttackDamageWithoutStatusEffectBuffs());
						newMonster.SetHealth(newMonster.GetMaxHP(), newMonster.GetMaxHP());
					}
				}, spawnMode, spawnPoint, null, false, null, startingStatusEffects, false);

				CardUpgradeData paramCardUpgradeData = cardEffectState.GetParamCardUpgradeData();
				if (newMonster != null && paramCardUpgradeData != null)
				{
					CardUpgradeState upgradeState = new CardUpgradeState();
					upgradeState.Setup(paramCardUpgradeData, false);
					yield return newMonster.ApplyCardUpgrade(upgradeState, false);
					if (spawnerCard != null && !saveManager.PreviewMode)
					{
						spawnerCard.GetTemporaryCardStateModifiers().AddUpgrade(upgradeState, null);
						spawnerCard.UpdateCardBodyText(null);
					}
					upgradeState = null;
				}

				i += 1;
			}
			if (!saveManager.PreviewMode)
			{
				yield return cardEffectParams.roomManager.GetRoomUI().CenterCharacters(room, fromMonsterSpawnPoint != null, false, true);
			}
			else
			{
				room.ShiftSpawnPoints(Team.Type.Monsters, -1);
			}

			yield break;
		}

		public string GetTooltipBaseKey(CardEffectState cardEffectState)
		{
			return "Hint_PsionicBlust";
		}
	}
}
