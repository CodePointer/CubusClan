using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

//using SuccClan.Cards;
namespace SuccClan.RelicEffects
{
	//RelicEffectAddBattleCardToPile
	public sealed class RelicEffectAddBattleCardToPile : RelicEffectAddCardBase, IStartOfPlayerTurnAfterDrawRelicEffect, ITurnTimingRelicEffect, IRelicEffect
	{
		private CardData cardToAdd;
		private CardUpgradeData cardUpgradeData;
		private CardPile cardPile;
		//private int cardNum;

		// Token: 0x060015CE RID: 5582 RVA: 0x000574B4 File Offset: 0x000556B4
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.cardUpgradeData = relicEffectData.GetParamCardUpgradeData();
			this.cardPile = (CardPile)relicEffectData.GetParamInt();
			//this.cardNum = (int)relicEffectData.GetParamFloat();
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000574CB File Offset: 0x000556CB
		public bool TestEffect(RelicEffectParams relicEffectParams)
		{
			this.cardToAdd = null;
			if (this.cardPool != null)
			{
				this.cardToAdd = this.cardPool.GetRandomChoice(RngId.Battle);
			}
			return true;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000574F5 File Offset: 0x000556F5
		public IEnumerator ApplyEffect(RelicEffectParams relicEffectParams)
		{
			if (this.cardToAdd)
			{
				CardManager cardManager = relicEffectParams.cardManager;
				bool pileUpdated = false;
				cardManager.cardPilesChangedSignal.AddOnce(delegate (CardManager.CardPileInformation _)
				{
					pileUpdated = true;
				});
				CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new CardManager.AddCardUpgradingInfo();
				if (this.cardUpgradeData != null)
				{
					addCardUpgradingInfo.upgradeDatas.Add(this.cardUpgradeData);
				}
				addCardUpgradingInfo.tempCardUpgrade = true;
				addCardUpgradingInfo.upgradingCardSource = null;
				if (cardManager.AddCard(this.cardToAdd, this.cardPile, 1, 1, true, false, addCardUpgradingInfo) == null)
				{
					pileUpdated = true;
				}
				yield return new WaitUntil(() => pileUpdated);
			}
			yield break;
		}

		// Token: 0x04000B87 RID: 2951
		

		// Token: 0x04000B88 RID: 2952
		
	}
}
