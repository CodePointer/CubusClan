using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectMySacrifice : CardEffectBase, ICardEffectStatuslessTooltip
	{
		public override bool CanRandomizeTargetMode
		{
			get
			{
				return false;
			}
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			return cardEffectParams.targets.Count > 0;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00023640 File Offset: 0x00021840
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			foreach (CharacterState characterState in cardEffectParams.targets)
			{
				//bool paramBool = cardEffectState.GetParamBool();
				yield return characterState.Sacrifice(cardEffectParams.playedCard, false, false);
			}
			yield break;
		}

		public string GetTooltipBaseKey(CardEffectState cardEffectState)
		{
			return "CardEffectSacrifice_NoSubtype";
		}

		public override string GetDescriptionAsTrait(CardEffectState cardEffectState)
		{
			return "CardEffectSacrifice_AsTrait".Localize(null);
		}
	}
}
