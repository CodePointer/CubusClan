using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectDebuffDamage : CardEffectBase
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x000214B4 File Offset: 0x0001F6B4
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			this.debuffAmount = cardEffectState.GetIntInRange();
			using (List<CharacterState>.Enumerator enumerator = cardEffectParams.targets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CharacterState characterState = enumerator.Current;
					if (this.TestEffectOnTarget(cardEffectState, cardEffectParams, characterState))  // No trait
					{
						//if (cardEffectState.GetParentCardState() != null)
						//{
						//	CardTraitState.ApplyingDamageParameters damageParams = new CardTraitState.ApplyingDamageParameters
						//	{
						//		damage = this.debuffAmount,
						//		damageType = Damage.Type.Default,
						//		combatManager = cardEffectParams.combatManager
						//	};
						//	foreach (CardTraitState cardTraitState in cardEffectState.GetParentCardState().GetTraitStates())
						//	{
						//		damageParams.damage = this.debuffAmount;
						//		this.debuffAmount = cardTraitState.OnApplyingBuffDamageToUnit(cardEffectParams.cardManager, damageParams);
						//	}
						//}
						characterState.DebuffDamage(this.debuffAmount, null, false);
						base.NotifyHealthEffectTriggered(cardEffectParams.saveManager, cardEffectParams.popupNotificationManager, this.GetActivatedDescription(cardEffectState), characterState.GetCharacterUI());
						if (!cardEffectParams.saveManager.PreviewMode && characterState.IsPyreHeart() && cardEffectState.GetTargetMode() == TargetMode.Pyre)
						{
							cardEffectParams.saveManager.pyreAttackChangedSignal.Dispatch(cardEffectParams.saveManager.GetDisplayedPyreAttack(), cardEffectParams.saveManager.GetDisplayedPyreNumAttacks());
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000214D1 File Offset: 0x0001F6D1
		public override string GetActivatedDescription(CardEffectState cardEffectState)
		{
			return CardEffectBuffDamage.GetNotificationText(-this.debuffAmount);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000214E0 File Offset: 0x0001F6E0
		public static string GetNotificationText(int amount)
		{
			if (CardEffectDebuffDamage.ActivatedKey.HasTranslation())
			{
				string key = (amount >= 0) ? "TextFormat_Add" : "TextFormat_Default";
				return string.Format(CardEffectDebuffDamage.ActivatedKey.Localize(null), string.Format(key.Localize(null), amount));
			}
			return null;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00021530 File Offset: 0x0001F730
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

		// Token: 0x060006FD RID: 1789 RVA: 0x00021590 File Offset: 0x0001F790
		public override bool TestEffectOnTarget(CardEffectState cardEffectState, CardEffectParams cardEffectParams, CharacterState target)
		{
			if (target.IsPyreHeart())
			{
				return !cardEffectParams.saveManager.PreviewMode || cardEffectParams.GetSelectedRoom().GetIsPyreRoom();
			}
			return target.IsAlive && target.GetCanAttack();
		}

		// Token: 0x04000438 RID: 1080
		private static readonly string ActivatedKey = "CardEffectBuffDamage_Activated";

		// Token: 0x04000439 RID: 1081
		private const string PositiveEffectKey = "TextFormat_Add";

		// Token: 0x0400043A RID: 1082
		private const string NegativeEffectKey = "TextFormat_Default";

		// Token: 0x0400043B RID: 1083
		private int debuffAmount;
	}
}
