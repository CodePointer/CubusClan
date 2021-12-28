using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectCopyUnitWithUpgrade : CardEffectBase
	{
		// Token: 0x04000442 RID: 1090
		private int numCopiesOfEachUnit;
		private bool flagHeal;
		private SpawnMode spawnMode;

		// Token: 0x0600071B RID: 1819 RVA: 0x00021A18 File Offset: 0x0001FC18
		public override void Setup(CardEffectState cardEffectState)
		{
			base.Setup(cardEffectState);
			this.numCopiesOfEachUnit = cardEffectState.GetParamInt();
			this.flagHeal = cardEffectState.GetParamBool();
			this.spawnMode = (SpawnMode)cardEffectState.GetAdditionalParamInt();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00021094 File Offset: 0x0001F294
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			//Utils.BepLog(new List<string> { "TestEffectEnter.",
			//	cardEffectParams.playedCard != null ? cardEffectParams.playedCard.ToString() : "NULL",
			//	cardEffectParams.targets.Count.ToString(),
			//	cardEffectParams.fromTrigger.ToString(),
			//	cardEffectParams.triggerType != null ? cardEffectParams.triggerType.ToString() : "NULL",
			//	cardEffectParams.overrideTargetCharacter != null ? cardEffectParams.overrideTargetCharacter.ToString() : "NULL",
			//	cardEffectParams.cardTriggeredCharacter != null ? cardEffectParams.cardTriggeredCharacter.ToString() : "NULL",
			//	cardEffectParams.selfTarget != null ? cardEffectParams.selfTarget.ToString() : "NULL"
			//});
			return cardEffectParams.targets.Count > 0;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00021A2D File Offset: 0x0001FC2D
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			Utils.BepLog(new List<string> { "ApplyEffectEnter." });
			int numSpawns = 0;
			//var targetList = cardEffectParams.targets;
			//if (cardEffectState.GetParamStr() == "Self")
			//{
			//	targetList = new List<CharacterState> { cardEffectParams.selfTarget };
			//}

			using (List<CharacterState>.Enumerator enumerator = cardEffectParams.targets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					var copyUnitSrc = enumerator.Current;
					CardUpgradeData paramCardUpgradeData = cardEffectState.GetParamCardUpgradeData();
					
					// Check if monster can alive
					if (paramCardUpgradeData != null)
					{
						int bounsMaxHP = paramCardUpgradeData.GetBonusHP();
						int currentMaxHP = copyUnitSrc.GetMaxHP();
						if (currentMaxHP - bounsMaxHP <= 0)
						{
							Utils.BepLog(new List<string> { "HP check failed." });
							continue;
						}
					}

					SpawnPoint spawnPoint = copyUnitSrc.GetSpawnPoint(false);
					
					Utils.BepLog(new List<string> { "ApplyEffect-enumerator.", copyUnitSrc.ToString(), copyUnitSrc.GetCharacterManager().ToString() });
					//var subtypeData = copyUnitSrc.GetSubtypes();
					//foreach(SubtypeData subtype in subtypeData)
					//{
					//	Utils.BepLog(new List<string> { copyUnitSrc.ToString(), subtype.Key });
					//}

					if (spawnPoint != null)
					{
						Utils.BepLog(new List<string> { "ApplyEffect-spawnPoint.", spawnPoint.ToString() });
						int num = 0;
						for (int i = 0; i < this.numCopiesOfEachUnit; i = num + 1)
						{
							CharacterState newMonster = null;

							yield return cardEffectParams.monsterManager.CreateMonsterState(copyUnitSrc.GetSourceCharacterData(), copyUnitSrc.GetSpawnerCard(), 
								cardEffectParams.selectedRoom, delegate (CharacterState character)
								{
									newMonster = character;
									if (newMonster != null)
									{
										CharacterHelper.CopyCharacterStats(newMonster, copyUnitSrc);
										//newMonster.AddStatusEffect("cardless", 1);
									}
								}, this.spawnMode, spawnPoint, null, false, null, null, false);

							if (newMonster != null)
							{
								if (flagHeal)
								{
									newMonster.SetHealth(newMonster.GetMaxHP(), newMonster.GetMaxHP());
								}
								if (paramCardUpgradeData != null)
								{
									CardUpgradeState upgradeState = new CardUpgradeState();
									upgradeState.Setup(paramCardUpgradeData, false);
									yield return newMonster.ApplyCardUpgrade(upgradeState, false);
								}

								Utils.BepLog(new List<string> { "ApplyEffect-newMonster.", newMonster.ToString() });
								num = numSpawns;
								numSpawns = num + 1;
							}
							num = i;
						}
						spawnPoint = null;
					}
				}
			}
			if (numSpawns > 1 && !cardEffectParams.saveManager.PreviewMode)
			{
				yield return cardEffectParams.roomManager.GetRoomUI().CenterCharacters(cardEffectParams.GetSelectedRoom(), false, false, false);
			}
			yield break;
		}
	}
}
