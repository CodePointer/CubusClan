using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public sealed class CardEffectUpgradePermanent : CardEffectBase
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x000214B4 File Offset: 0x0001F6B4
		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			if (!TestEffect(cardEffectState, cardEffectParams))
			{
				yield break;
			}

			CardUpgradeState cardUpgradeState = new CardUpgradeState();
			cardUpgradeState.Setup(cardEffectState.GetParamCardUpgradeData());

			CharacterState targetSelf = cardEffectParams.selfTarget;

			CardState spawnCard = targetSelf.GetSpawnerCard();
			if (spawnCard != null)
			{
				spawnCard.Upgrade(cardUpgradeState, cardEffectParams.saveManager, false);
			}
			yield return targetSelf.ApplyCardUpgrade(cardUpgradeState, false);

			yield break;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00021530 File Offset: 0x0001F730
		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			return cardEffectState.GetParamCardUpgradeData() != null;
		}
	}
}
