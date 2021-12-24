using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectDiscardBlightAndDraw : CardEffectBase
	{
		private CardState sourceCardState;

		public override bool CanPlayAfterBossDead
		{
			get
			{
				return false;
			}
		}

		public override bool CanApplyInPreviewMode
		{
			get
			{
				return false;
			}
		}

		public override bool IsTriggerStackable
		{
			get
			{
				return true;
			}
		}

		//private bool CanDrawToMaxStartingHandCards(CardEffectParams cardEffectParams, out int numCardsToDraw)
		//{
		//	int num = cardEffectParams.cardManager.GetNumCardsInHand() - 1;
		//	int maxHandSize = cardEffectParams.cardManager.GetMaxHandSize();
		//	int startingHandSize = cardEffectParams.cardManager.GetStartingHandSize();
		//	numCardsToDraw = Mathf.Max(maxHandSize - num, 0);
		//	return numCardsToDraw > 0;
		//}

		public override void Setup(CardEffectState cardEffectState)
		{
			base.Setup(cardEffectState);
			this.sourceCardState = cardEffectState.GetParentCardState();
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int numBlights = 0;
			List<CardState> handCards = cardEffectParams.cardManager.GetHand(true);
			if (cardEffectParams.playedCard != null)
			{
				handCards.Remove(cardEffectParams.playedCard);
			}
			foreach (CardState cardToDiscard in handCards)
			{
				if (cardToDiscard.GetCardType() == CardType.Blight || cardToDiscard.GetCardType() == CardType.Junk)
				{
					numBlights++;
				}
			}

			//int num;
			return true;
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int numDiscard = 0;
			List<CardState> handCards = cardEffectParams.cardManager.GetHand(true);
			if (cardEffectParams.playedCard != null)
			{
				handCards.Remove(cardEffectParams.playedCard);
			}

			float effectDelay = 0.0f;
			CardManager.DiscardCardParams discardCardParams = new CardManager.DiscardCardParams();
			var removedCards = new List<CardState> { };
			foreach (CardState cardToDiscard in handCards)
			{
				yield return CoreUtil.WaitForSecondsOrBreak(effectDelay);
				if (cardToDiscard.GetCardType() == CardType.Blight 
					|| cardToDiscard.GetCardType() == CardType.Junk)
				{
					discardCardParams.discardCard = cardToDiscard;
					discardCardParams.triggeredByCard = true;
					discardCardParams.triggeredCard = this.sourceCardState;
					discardCardParams.wasPlayed = false;
					yield return cardEffectParams.cardManager.DiscardCard(discardCardParams, false);
					numDiscard++;

					effectDelay += cardEffectParams.allGameData.GetBalanceData().GetAnimationTimingData().handDiscardAnimationDelay;
					removedCards.Add(cardToDiscard);
				}
			}
			foreach (CardState cardDiscarded in removedCards)
			{
				handCards.Remove(cardDiscarded);
			}

			cardEffectParams.cardManager.DrawCards(numDiscard, cardEffectParams.playedCard, CardType.Invalid);
			yield break;
		}
	}
}
