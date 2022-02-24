using System;
using System.Collections;
using System.Collections.Generic;
using ShinyShoe.Logging;
using System.Text;

using UnityEngine;
using Trainworks.Constants;

using SuccClan.Effects;

namespace SuccClan.CardEffects
{
	// CardEffectAddStatusEffectWithSoul
	public class CardEffectAddStatusEffectPsionicBlust : CardEffectBase, ICardEffectStatuslessTooltip
	{
		public static StatusEffectStackData GetStatusEffectStack(CardEffectData cardEffectData, CardEffectState cardEffectState, CharacterState selfTarget, bool isTest = false)
		{
			StatusEffectStackData[] paramStatusEffects = cardEffectData.GetParamStatusEffects();

			StatusEffectStackData statusEffectStackData = paramStatusEffects[0];
			if (cardEffectState != null && cardEffectState.GetUseStatusEffectStackMultiplier() && selfTarget != null)
			{
				statusEffectStackData = statusEffectStackData.Copy();
				int statusEffectStacks = selfTarget.GetStatusEffectStacks(cardEffectState.GetStatusEffectStackMultiplier());
				statusEffectStackData.count *= statusEffectStacks;
			}
			return statusEffectStackData;
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			// Check if psionic stack is larger than cost
			int psionicCost = cardEffectState.GetParamInt();
			int psionicStackNum = cardEffectParams.selfTarget.GetStatusEffectStacks(StatusEffectPsionic.IDName);
			if (psionicStackNum < psionicCost)
			{
				return false;
			}

			StatusEffectStackData statusEffectStack = CardEffectAddStatusEffect.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, cardEffectParams.selfTarget, true);
			if (statusEffectStack == null)
			{
				return false;
			}
			if (cardEffectState.GetTargetMode() != TargetMode.DropTargetCharacter)
			{
				return true;
			}
			if (cardEffectParams.targets.Count <= 0)
			{
				return false;
			}
			if (cardEffectParams.statusEffectManager.GetStatusEffectDataById(statusEffectStack.statusId).IsStackable())
			{
				return true;
			}
			foreach (CharacterState characterState in cardEffectParams.targets)
			{
				if (!characterState.HasStatusEffect(statusEffectStack.statusId) && base.IsTargetValid(cardEffectState, characterState, true))
				{
					return true;
				}
			}
			return false;
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			int psionicCost = cardEffectState.GetParamInt();
			if (psionicCost > 0)
			{
				cardEffectParams.selfTarget.RemoveStatusEffect(StatusEffectPsionic.IDName, false, psionicCost, true, cardEffectParams.sourceRelic, null);
			}

			StatusEffectStackData statusEffectStack = CardEffectAddStatusEffect.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, cardEffectParams.selfTarget, false);
			if (statusEffectStack == null)
			{
				//Utils.BepLog(new List<string>
				//{
				//	"statusEffectStack is null."
				//});
				yield break;
			}

			//Utils.BepLog(new List<string>
			//{
			//	"statusEffectStack:",
			//	statusEffectStack.statusId.ToString(),
			//	statusEffectStack.count.ToString(),
			//});

			CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams
			{
				sourceRelicState = cardEffectParams.sourceRelic,
				sourceCardState = cardEffectParams.playedCard,
				cardManager = cardEffectParams.cardManager,
				sourceIsHero = (cardEffectState.GetSourceTeamType() == Team.Type.Heroes)
			};
			for (int i = cardEffectParams.targets.Count - 1; i >= 0; i--)
			{
				CharacterState characterState = cardEffectParams.targets[i];
				int count = statusEffectStack.count;
				characterState.AddStatusEffect(statusEffectStack.statusId, count, addStatusEffectParams);
			}
			yield break;
		}

		public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
		{
			CardEffectAddStatusEffect.GetTooltipsStatusList(cardEffectState.GetSourceCardEffectData(), ref outStatusIdList);
		}

		public static void GetTooltipsStatusList(CardEffectData cardEffectData, ref List<string> outStatusIdList)
		{
			foreach (StatusEffectStackData statusEffectStackData in cardEffectData.GetParamStatusEffects())
			{
				outStatusIdList.Add(statusEffectStackData.statusId);
			}
		}

		public string GetTooltipBaseKey(CardEffectState cardEffectState)
		{
			return "Hint_PsionicBlust";
		}
	}
}
