using System;
using System.Collections;
using ShinyShoe.Logging;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

//using SuccClan.Cards;
namespace SuccClan.RelicEffects
{
	//RelicEffectAddUpgradeOnSpawn
	public sealed class RelicEffectDamageEnemyOnChampionFloor : RelicEffectBase, IRelicEffect, 
		IStartOfPlayerTurnAfterDrawRelicEffect, ITurnTimingRelicEffect
	{
		private List<CharacterState> roomCharacters = new List<CharacterState>();
		private int damageAmount;

		private void CollectTargets(RelicEffectParams relicEffectParams)
		{
			this.roomCharacters.Clear();

			for (int roomIndex = 0; roomIndex < relicEffectParams.roomManager.GetNumRooms(); roomIndex++)
			{
				RoomState roomState = relicEffectParams.roomManager.GetRoom(roomIndex);
				var charList = new List<CharacterState>();
				relicEffectParams.combatManager.GetAllCharactersInRoom(charList, roomState);
				
				bool flagHasChampion = false;
				CharacterState firstEnemy = null;
				foreach (var character in charList)
				{
					if (character.IsChampion())
					{
						flagHasChampion = true;
					}
					if (character.GetTeamType() == Team.Type.Heroes && firstEnemy == null)
					{
						firstEnemy = character;
					}
					//Utils.BepLog(new List<string>
					//{
					//	"firstEnemy & character:",
					//	firstEnemy == null ? "null" : firstEnemy.ToString(),
					//	character == null ? "null" : character.ToString(),
					//});
				}

				if (flagHasChampion && firstEnemy != null)
				{
					//Utils.BepLog(new List<string>
					//{
					//	"flagHasChampion. Characters:",
					//	firstEnemy == null ? "null" : firstEnemy.ToString(),
					//});
					this.roomCharacters.Add(firstEnemy);
				}
			}
		}

		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.damageAmount = relicEffectData.GetParamInt();
		}

		public bool TestEffect(RelicEffectParams relicEffectParams)
		{
			this.CollectTargets(relicEffectParams);
			//Utils.BepLog(new List<string> { "roomCharacters-Count", this.roomCharacters.Count.ToString() });
			return this.roomCharacters.Count > 0;
		}

		public IEnumerator ApplyEffect(RelicEffectParams relicEffectParams)
		{
			SaveManager saveManager = relicEffectParams.saveManager;
			RelicManager relicManager = relicEffectParams.relicManager;
			RoomManager roomManager = relicEffectParams.roomManager;
			CombatManager combatManager = relicEffectParams.combatManager;

			this.CollectTargets(relicEffectParams);

			//Utils.BepLog(new List<string> { "Apply-roomCharacters", this.roomCharacters.Count.ToString() });

			foreach (CharacterState target in this.roomCharacters)
			{
				//Utils.BepLog(new List<string>
				//{
				//	"target character:",
				//	target == null ? "null" : target.ToString(),
				//});

				yield return roomManager.GetRoomUI().SetSelectedRoom(target.GetCurrentRoomIndex(), false);
				yield return base.TimingYieldIfNonCovenant(RelicEffectBase.TimingContext.PreFire, saveManager);
				base.NotifyRelicTriggered(relicEffectParams.relicManager, target);
				yield return combatManager.ApplyDamageToTarget(this.damageAmount, target,
					new CombatManager.ApplyDamageToTargetParameters
					{
						relicState = this._srcRelicState,
						vfxAtLoc = this._srcRelicEffectData.GetAppliedVfx(),
						showDamageVfx = true
					});
				yield return base.TimingYieldIfNonCovenant(RelicEffectBase.TimingContext.PostFire, saveManager);
			}

			yield break;
		}

		//public override string GetActivatedDescription()
		//{
		//	return "Activate Test";
		//}
	}
}
