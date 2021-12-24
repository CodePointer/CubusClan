using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardTraitRewardGold : CardTraitState
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x00024864 File Offset: 0x00022A64
		public override IEnumerator OnCardDiscarded(CardManager.DiscardCardParams discardCardParams, CardManager cardManager, RelicManager relicManager, CombatManager combatManager, RoomManager roomManager, SaveManager saveManager)
		{
			if (!discardCardParams.wasPlayed)
			{
				yield break;
			}
			int additionalGold = this.GetAdditionalGold();
			if (additionalGold > 0)
			{
				cardManager.GetPlayerManager().AdjustGold(additionalGold, true);
			}
			yield break;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00024884 File Offset: 0x00022A84
		private int GetAdditionalGold()
		{
			return base.GetParamInt();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000248A6 File Offset: 0x00022AA6
		public override int GetModifiedParamInt(SaveManager saveManager)
		{
			if (saveManager != null)
			{
				return saveManager.GetAdjustedGoldAmount(base.GetParamInt(), true);
			}
			return base.GetParamInt();
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001C727 File Offset: 0x0001A927
		public override bool HasMultiWordDesc()
		{
			return true;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000248C8 File Offset: 0x00022AC8
		public override string GetCurrentEffectText(CardStatistics cardStatistics, SaveManager saveManager, RelicManager relicManager)
		{
			return string.Empty;
		}
	}
}
