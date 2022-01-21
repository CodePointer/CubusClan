using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectMultiplyStatusCategory : CardEffectBase
	{
		// ParamInt: StatusEffectData.DisplayCategory
		// ParamMultiplier: Scale for multiply

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001C727 File Offset: 0x0001A927
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			return true;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002322A File Offset: 0x0002142A
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			StatusEffectData.DisplayCategory displayCategory = (StatusEffectData.DisplayCategory)cardEffectState.GetParamInt();
			float scale = cardEffectState.GetParamMultiplier();

			List<CharacterState.StatusEffectStack> listStatus = new List<CharacterState.StatusEffectStack>();
			for (int i = 0; i < cardEffectParams.targets.Count; i++)
			{
				CharacterState characterState = cardEffectParams.targets[i];
				characterState.GetStatusEffects(out listStatus, false);
				for (int j = 0; j < listStatus.Count; j++)
				{
					if (listStatus[j].State.GetDisplayCategory() == displayCategory)
					{
						int currentCount = listStatus[j].Count;
						int targetCount = (int)Math.Max(0.0f, scale * currentCount);
						
						if (targetCount < currentCount)
						{
							characterState.RemoveStatusEffect(listStatus[j].State.GetStatusId(), false, currentCount - targetCount, true, cardEffectParams.sourceRelic, null);
						}
						else if (targetCount > currentCount)
						{
							characterState.AddStatusEffect(listStatus[j].State.GetStatusId(), targetCount - currentCount);
						}
						
						characterState.GetCharacterUI().ShowEffectVFX(characterState, cardEffectState.GetAppliedVFX());
					}
				}
				listStatus.Clear();
			}
			yield break;
		}
	}
}
