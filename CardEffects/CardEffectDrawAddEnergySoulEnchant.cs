using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using Trainworks.Constants;

namespace SuccClan.CardEffects
{
	// CardEffectDrawAddEnergySoulEnchant
	public sealed class CardEffectDrawAddEnergySoulEnchant : CardEffectBase, ICardEffectStatuslessTooltip
	{
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

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			// Check if soul stack is larger than cost
			int soulCost = cardEffectState.GetParamInt();
			int soulStackNum = cardEffectParams.selfTarget.GetStatusEffectStacks(VanillaStatusEffectIDs.Soul);
			//Utils.BepLog(new List<string>
			//{
			//	"soulCost, soulStackNum",
			//	soulCost.ToString(),
			//	soulStackNum.ToString(),
			//});
			return soulStackNum >= soulCost;
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int soulCost = cardEffectState.GetParamInt();
			if (soulCost > 0)
			{
				//Utils.BepLog(new List<string>
				//{
				//	"soulCost:",
				//	soulCost.ToString(),
				//});
				cardEffectParams.selfTarget.RemoveStatusEffect(VanillaStatusEffectIDs.Soul, false, soulCost, true, cardEffectParams.sourceRelic, null);
			}

			// Draw Cards
			int numCardsToDraw = cardEffectState.GetAdditionalParamInt();
			if (numCardsToDraw > 0)
			{
				cardEffectParams.cardManager.DrawCards(numCardsToDraw, cardEffectParams.playedCard, CardType.Invalid);
			}

			// Gain Energy
			int emberToGain = cardEffectState.GetAdditionalParamInt();
			if (emberToGain > 0)
			{
				var triggeredCharacter = cardEffectParams.selfTarget ?? cardEffectParams.cardTriggeredCharacter;
				this.ShowCharacterNotification(emberToGain, triggeredCharacter, cardEffectParams.popupNotificationManager);
				cardEffectParams.playerManager.AddEnergy(emberToGain);
			}

			yield break;
		}

		public void ShowCharacterNotification(int magnitude, CharacterState triggeredCharacter, PopupNotificationManager popupNotificationManager)
		{
			if (triggeredCharacter == null)
			{
				return;
			}
			PopupNotificationUI.NotificationData notificationData = new PopupNotificationUI.NotificationData
			{
				text = string.Format("CardEffectGainEnergy_Activated".Localize(null), magnitude),
				colorType = ColorDisplayData.ColorType.Default,
				source = PopupNotificationUI.Source.General,
			};
			popupNotificationManager.ShowNotification(notificationData, triggeredCharacter.GetCharacterUI());
		}

		public string GetTooltipBaseKey(CardEffectState cardEffectState)
		{
			return "Hint_SoulEnchant";
		}
	}
}
