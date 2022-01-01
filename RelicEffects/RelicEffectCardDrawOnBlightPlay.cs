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
	//RelicEffectCardDrawOnBlightPlay
	public sealed class RelicEffectCardDrawOnBlightPlay : RelicEffectBase, IRelicEffect, IOnDiscardRelicEffect
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00059816 File Offset: 0x00057A16
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.numberCardsToDraw = relicEffectData.GetParamInt();
		}

		//// Token: 0x0600169F RID: 5791 RVA: 0x00059845 File Offset: 0x00057A45
		//public bool TestCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
		//{
		//	Utils.BepLog(new List<string> { "OnTestCardPlayed: ", relicEffectParams.cardState.ToString() });
		//	return (relicEffectParams.cardState.GetCardType() == CardType.Blight || relicEffectParams.cardState.GetCardType() == CardType.Junk);
		//}

		//// Token: 0x060016A0 RID: 5792 RVA: 0x0005987C File Offset: 0x00057A7C
		//public IEnumerator OnCardPlayed(CardPlayedRelicEffectParams relicEffectParams)
		//{
		//	Utils.BepLog(new List<string> { "OnCardPlayed: ", relicEffectParams.cardState.ToString() });
		//	base.NotifyRoomRelicTriggered(relicEffectParams);
		//	relicEffectParams.cardManager.DrawCards(this.numberCardsToDraw, null, CardType.Invalid);
		//	yield break;
		//}

		public bool TestOnCardDiscarded(CardDiscardedRelicEffectParams relicEffectParams)
		{
			//Utils.BepLog(new List<string> { "TestOnCardDiscarded" });
			if (relicEffectParams.discardCardParams.handDiscarded)
			{
				return false;
			}
			CardState cardState = relicEffectParams.discardCardParams.discardCard;
			if (!cardState.HasTrait(VanillaCardTraitTypes.CardTraitTreasure))
			{
				return false;
			}
			return cardState.GetCardType() == CardType.Blight || cardState.GetCardType() == CardType.Junk;
		}

		public IEnumerator OnCardDiscarded(CardDiscardedRelicEffectParams relicEffectParams)
		{
			//Utils.BepLog(new List<string> { "OnCardDiscarded: ", relicEffectParams.discardCardParams.discardCard.ToString() });
			base.NotifyRoomRelicTriggered(relicEffectParams);
			relicEffectParams.cardManager.DrawCards(this.numberCardsToDraw, null, CardType.Invalid);
			yield break;
		}

		// Token: 0x04000BF6 RID: 3062
		private int numberCardsToDraw;
	}
}
