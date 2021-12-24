using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectSpread : CardEffectBase, ICardEffectStatuslessTooltip
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

		public override void Setup(CardEffectState cardEffectState)
		{
			base.Setup(cardEffectState);
			this.sourceCardState = cardEffectState.GetParentCardState();
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int numCardsInHand = cardEffectParams.cardManager.GetNumCardsInHand();
			return numCardsInHand != 0 && (numCardsInHand != 1 || cardEffectParams.cardManager.GetHand(false)[0] != cardEffectParams.playedCard);
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)  // TODO
		{
			List<CardState> handCards = cardEffectParams.cardManager.GetHand(true);
			if (cardEffectParams.playedCard != null)
			{
				handCards.Remove(cardEffectParams.playedCard);
			}

			// Get all blights
			List<CardState> handBlights = new List<CardState> { };
			foreach (CardState card in handCards)
			{
				if (card.GetCardType() == CardType.Blight || card.GetCardType() == CardType.Junk)
				{
					handBlights.Add(card);
				}
			}

			// Select the discarded cards
			//int intInRange = cardEffectState.GetIntInRange();
			int intInRange = 1;
			int num = Mathf.Max(0, handBlights.Count - intInRange);
			for (int i = 0; i < num; i++)
			{
				int index = RandomManager.Range(0, handBlights.Count, RngId.Battle);
				handBlights.RemoveAt(index);
			}

			float effectDelay = cardEffectParams.allGameData.GetBalanceData().GetAnimationTimingData().cardEffectDiscardAnimationDelay; ;
			CardManager.DiscardCardParams discardCardParams = new CardManager.DiscardCardParams();
			foreach (CardState cardToDiscard in handBlights)
			{
				discardCardParams.effectDelay = effectDelay;
				discardCardParams.discardCard = cardToDiscard;
				discardCardParams.triggeredByCard = true;
				discardCardParams.triggeredCard = this.sourceCardState;
				discardCardParams.wasPlayed = false;
				yield return cardEffectParams.cardManager.DiscardCard(discardCardParams, false);
				effectDelay += cardEffectParams.allGameData.GetBalanceData().GetAnimationTimingData().cardEffectDiscardAnimationDelay;
			}

			// Draw one
			if (handBlights.Count > 0)
			{
				cardEffectParams.cardManager.DrawCards(handBlights.Count, cardEffectParams.playedCard, CardType.Invalid);
			}

			yield break;
		}

		public string GetTooltipBaseKey(CardEffectState cardEffectState)
		{
			return "CardEffectSpread";
		}

		public override string GetDescriptionAsTrait(CardEffectState cardEffectState)
		{
			return "CardEffectSpread_AsTrait".Localize(null);
		}
	}
}
