using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectMultiplyMaxHealth : CardEffectBase
	{
		public static readonly string ActivatedKey = "CardEffectAdjustMaxHealth_Activated";

		private float GetScaleNum(CardEffectState cardEffectState)
		{
			int paramInt = cardEffectState.GetIntInRange();
			if (paramInt < 0)
			{
				return -(1.0f / (float)paramInt);
			}
			else if (paramInt > 0)
			{
				return (float)paramInt;
			}
			else
			{
				return 1.0f;
			}
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			using (List<CharacterState>.Enumerator enumerator = cardEffectParams.targets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CharacterState characterState = enumerator.Current;
					base.NotifyHealthEffectTriggered(cardEffectParams.saveManager, cardEffectParams.popupNotificationManager, this.GetActivatedDescription(cardEffectState), characterState.GetCharacterUI());

					float multiply = GetScaleNum(cardEffectState);
					int currentValue = characterState.GetMaxHP();
					int buffAmount = (int)(multiply * currentValue) - currentValue;

					//Utils.BepLog(new List<string> {
					//	"MultiplyMaxHealth",
					//	characterState.ToString(),
					//	multiply.ToString(),
					//	currentValue.ToString(),
					//	buffAmount.ToString()
					//});

					if (buffAmount < 0)
					{
						if (currentValue == 1)
						{
							buffAmount = 0;
						}
						yield return characterState.DebuffMaxHP(-buffAmount, 0);
					}
					else
					{
						yield return characterState.BuffMaxHP(buffAmount, false, null);
					}
					characterState.UpdateCharacterStateUI();
				}
				yield break;
			}
		}

		public override string GetActivatedDescription(CardEffectState cardEffectState)
		{
			return null;
		}
	}
}
