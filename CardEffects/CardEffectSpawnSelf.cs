using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectSpawnSelf : CardEffectBase // TODO: Need to revise.
	{
		private int numCopiesOfEachUnit;
		private bool flagHeal;

		public override void Setup(CardEffectState cardEffectState)
		{
			base.Setup(cardEffectState);
			this.numCopiesOfEachUnit = cardEffectState.GetParamInt();
			this.flagHeal = cardEffectState.GetParamBool();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00023774 File Offset: 0x00021974
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			RoomState selectedRoom = cardEffectParams.GetSelectedRoom();
			return selectedRoom == null || selectedRoom.IsRoomEnabled();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000237DB File Offset: 0x000219DB
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{

			SaveManager saveManager = cardEffectParams.saveManager;
			SpawnMode spawnMode = SpawnMode.SelectedSlot;
			RoomState room = cardEffectParams.GetSelectedRoom();
			CharacterState selfCharacter = cardEffectParams.selfTarget;
			CharacterData selfCharacterData = selfCharacter.GetSourceCharacterData();

			// Check if monster can alive
			CardUpgradeData paramCardUpgradeData = cardEffectState.GetParamCardUpgradeData();
			if (paramCardUpgradeData != null)
			{
				int bounsMaxHP = paramCardUpgradeData.GetBonusHP();
				int currentMaxHP = selfCharacter.GetMaxHP();
				//Utils.BepLog(new List<string> { "HP check: ", bounsMaxHP.ToString(), currentMaxHP.ToString() });
				if (currentMaxHP + bounsMaxHP <= 0)
				{
					//Utils.BepLog(new List<string> { "HP check failed." });
					yield break;
				}
			}

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

			int numSpawns = Mathf.Min(this.numCopiesOfEachUnit, room.GetRemainingSpawnPointCount(Team.Type.Monsters));
			int i = spawnIndex;

			while (i < spawnIndex + numSpawns && i < saveManager.GetNumSpawnPointsPerFloor())
			{
				SpawnPoint spawnPoint = null;

				spawnPoint = room.GetMonsterPoint(i);

				CardState spawnerCard = cardEffectParams.playedCard;
				if (fromMonsterSpawnPoint != null && spawnerCard != null)
				{
					spawnerCard = CardManager.CopyCardState(spawnerCard, cardEffectParams.relicManager, saveManager, cardEffectParams.allGameData, cardEffectParams.combatManager.GetCardManager().GetCardStatistics());
				}

				List<string> startingStatusEffects = new List<string> { "cardless" };

				CharacterState newMonster = null;
				yield return cardEffectParams.monsterManager.CreateMonsterState(selfCharacterData, spawnerCard, cardEffectParams.selectedRoom, delegate (CharacterState character)
				{
					newMonster = character;
				}, spawnMode, spawnPoint, null, false, null, startingStatusEffects, false);

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

		// Token: 0x060007F5 RID: 2037 RVA: 0x000237F8 File Offset: 0x000219F8
		public override string GetCardText(CardEffectState cardEffectState, RelicManager relicManager = null)
		{
			string result = "";
			//cardEffectState.GetParamCharacterData().GetCharacterCardText(out result, cardEffectState.GetParentCardState(), relicManager, cardEffectState.SaveManager);
			return result;
		}
	}
}
