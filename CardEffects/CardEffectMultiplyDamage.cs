using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectMultiplyDamage : CardEffectBase
	{
		private float GetScaleNum(CardEffectState cardEffectState)
		{
			int paramInt = cardEffectState.GetIntInRange();
			if (paramInt < 0)
			{
				return (float)(1 / paramInt);
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
					if (this.TestEffectOnTarget(cardEffectState, cardEffectParams, characterState))
					{
						float multiply = GetScaleNum(cardEffectState);
						int currentValue = characterState.GetAttackDamageWithoutStatusEffectBuffs();
						int buffAmount = (int)(multiply * currentValue) - currentValue;

						//Utils.BepLog(new List<string> {
						//	"MultiplyDamage",
						//	characterState.ToString(),
						//	multiply.ToString(),
						//	currentValue.ToString(),
						//	buffAmount.ToString()
						//});

						if (cardEffectState.GetParentCardState() != null)
						{
							CardTraitState.ApplyingDamageParameters damageParams = new CardTraitState.ApplyingDamageParameters
							{
								damage = buffAmount,
								damageType = Damage.Type.Default,
								combatManager = cardEffectParams.combatManager
							};
							foreach (CardTraitState cardTraitState in cardEffectState.GetParentCardState().GetTraitStates())
							{
								damageParams.damage = buffAmount;
								buffAmount = cardTraitState.OnApplyingBuffDamageToUnit(cardEffectParams.cardManager, damageParams);
							}
						}
						characterState.BuffDamage(buffAmount, null, false);
						base.NotifyHealthEffectTriggered(cardEffectParams.saveManager, cardEffectParams.popupNotificationManager, this.GetActivatedDescription(cardEffectState), characterState.GetCharacterUI());
						if (!cardEffectParams.saveManager.PreviewMode && characterState.IsPyreHeart() && cardEffectState.GetTargetMode() == TargetMode.Pyre)
						{
							cardEffectParams.saveManager.pyreAttackChangedSignal.Dispatch(cardEffectParams.saveManager.GetDisplayedPyreAttack(), cardEffectParams.saveManager.GetDisplayedPyreNumAttacks());
						}
					}
				}
				yield break;
			}
		}

		public override string GetActivatedDescription(CardEffectState cardEffectState)
		{
			return null;
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			foreach (CharacterState target in cardEffectParams.targets)
			{
				if (this.TestEffectOnTarget(cardEffectState, cardEffectParams, target))
				{
					return true;
				}
			}
			return false;
		}

		public override bool TestEffectOnTarget(CardEffectState cardEffectState, CardEffectParams cardEffectParams, CharacterState target)
		{
			if (target.IsPyreHeart())
			{
				return !cardEffectParams.saveManager.PreviewMode || cardEffectParams.GetSelectedRoom().GetIsPyreRoom();
			}
			return target.IsAlive && target.GetCanAttack();
		}
	}
}
