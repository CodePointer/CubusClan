using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using Trainworks.Constants;

using SuccClan.Effects;

namespace SuccClan.CardEffects
{
	// CardEffectDrawAddEnergySoulEnchant
	public sealed class CardEffectDrawAddEnergyPsionicBlust : CardEffectBase, ICardEffectStatuslessTooltip
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
			// Check if psionic stack is larger than cost
			int psionicCost = cardEffectState.GetParamInt();
			int psionicStackNum = cardEffectParams.selfTarget.GetStatusEffectStacks(StatusEffectPsionic.IDName);
			//Utils.BepLog(new List<string>
			//{
			//	"soulCost, soulStackNum",
			//	soulCost.ToString(),
			//	soulStackNum.ToString(),
			//});
			return psionicStackNum >= psionicCost;
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int psionicCost = cardEffectState.GetParamInt();
			if (psionicCost > 0)
			{
				//Utils.BepLog(new List<string>
				//{
				//	"soulCost:",
				//	soulCost.ToString(),
				//});
				cardEffectParams.selfTarget.RemoveStatusEffect(StatusEffectPsionic.IDName, false, psionicCost, true, cardEffectParams.sourceRelic, null);
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
			return "Hint_PsionicBlust";
		}
	}
}
