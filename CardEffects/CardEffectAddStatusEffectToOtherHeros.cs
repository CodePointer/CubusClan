using System;
using System.Collections;
using System.Collections.Generic;
using ShinyShoe.Logging;
using System.Text;

using UnityEngine;

namespace SuccClan.CardEffects
{
	public class CardEffectAddStatusEffectToOtherHeros : CardEffectBase
	{
		private List<CharacterState> targetList = new List<CharacterState>();

		public static StatusEffectStackData GetStatusEffectStack(CardEffectData cardEffectData, CardEffectState cardEffectState, CharacterState selfTarget, bool isTest = false)
		{
			StatusEffectStackData[] paramStatusEffects = cardEffectData.GetParamStatusEffects();

			if (paramStatusEffects == null || paramStatusEffects.Length == 0)
			{
				Log.Error(LogGroups.Gameplay, "cardEffectData.GetParamStatusEffects() yielded no results.");
				return null;
			}

			StatusEffectStackData statusEffectStackData = paramStatusEffects[0];
			if (cardEffectState != null && cardEffectState.GetUseStatusEffectStackMultiplier() && selfTarget != null)
			{
				statusEffectStackData = statusEffectStackData.Copy();
				int statusEffectStacks = selfTarget.GetStatusEffectStacks(cardEffectState.GetStatusEffectStackMultiplier());
				statusEffectStackData.count *= statusEffectStacks;
			}

			return statusEffectStackData;
		}

		private void GetTargetList(CardEffectParams cardEffectParams)
		{
			targetList.Clear();

			RoomState roomState = cardEffectParams.selfTarget.GetSpawnPoint().GetRoomOwner();
			var charactersInRoom = new List<CharacterState>();
			cardEffectParams.combatManager.GetAllCharactersInRoom(charactersInRoom, roomState);
			foreach (var character in charactersInRoom)
			{
				if (character.GetTeamType() == Team.Type.Heroes && !cardEffectParams.targets.Contains(character))
				{
					targetList.Add(character);
				}
			}
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams)
		{
			StatusEffectStackData statusEffectStack = CardEffectAddStatusEffect.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, cardEffectParams.selfTarget, true);
			if (statusEffectStack == null)
			{
				return false;
			}
			if (cardEffectParams.statusEffectManager.GetStatusEffectDataById(statusEffectStack.statusId).IsStackable())
			{
				return true;
			}

			// Get All other character in this room
			this.GetTargetList(cardEffectParams);
			foreach (CharacterState characterState in this.targetList)
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
			StatusEffectStackData statusEffectStack = CardEffectAddStatusEffect.GetStatusEffectStack(cardEffectState.GetSourceCardEffectData(), cardEffectState, cardEffectParams.selfTarget, false);
			if (statusEffectStack == null)
			{
				yield break;
			}

			CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams
			{
				sourceRelicState = cardEffectParams.sourceRelic,
				sourceCardState = cardEffectParams.playedCard,
				cardManager = cardEffectParams.cardManager,
				sourceIsHero = (cardEffectState.GetSourceTeamType() == Team.Type.Heroes)
			};

			this.GetTargetList(cardEffectParams);
			foreach (var target in this.targetList)
			{
				int count = statusEffectStack.count;
				target.AddStatusEffect(statusEffectStack.statusId, count, addStatusEffectParams);
				int buffAmount = cardEffectState.GetParamInt();
				if (buffAmount > 0)
				{
					target.BuffDamage(buffAmount, null, false);
				}
			}

			yield break;
		}

		public override void GetTooltipsStatusList(CardEffectState cardEffectState, ref List<string> outStatusIdList)
		{
			CardEffectAddStatusEffectToOtherHeros.GetTooltipsStatusList(cardEffectState.GetSourceCardEffectData(), ref outStatusIdList);
		}

		public static void GetTooltipsStatusList(CardEffectData cardEffectData, ref List<string> outStatusIdList)
		{
			foreach (StatusEffectStackData statusEffectStackData in cardEffectData.GetParamStatusEffects())
			{
				outStatusIdList.Add(statusEffectStackData.statusId);
			}
		}

		public override string GetActivatedDescription(CardEffectState cardEffectState)
		{
			string activatedKey = "CardEffectBuffDamage_Activated";
			int buffAmount = cardEffectState.GetParamInt();

			if (activatedKey.HasTranslation())
			{
				string key = "TextFormat_Add";
				return string.Format(activatedKey.Localize(null), string.Format(key.Localize(null), buffAmount));
			}
			return null;
		}
	}
}
