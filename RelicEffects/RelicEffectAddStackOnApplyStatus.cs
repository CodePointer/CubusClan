using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

//using SuccClan.Cards;
namespace SuccClan.RelicEffects
{
	public sealed class RelicEffectAddStackOnApplyStatus : RelicEffectBase, IOnStatusEffectAddedRelicEffect, IRelicEffect, IStatusEffectRelicEffect
	{
		private Team.Type team;

		private string statusId;

		private int additionalStacks;

		private bool addOnlyIfNonZeroStart;

		public override bool CanApplyInPreviewMode
		{
			get
			{
				return true;
			}
		}

		public override bool CanShowNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00058448 File Offset: 0x00056648
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.team = relicEffectData.GetParamSourceTeam();
			this.addOnlyIfNonZeroStart = relicEffectData.GetParamBool();
			var paramStatusEffects = relicEffectData.GetParamStatusEffects();
			if (paramStatusEffects.Length != 0)
			{
				this.statusId = paramStatusEffects[0].statusId;
				this.additionalStacks = paramStatusEffects[0].count;
			}
		}

		public void OnStatusEffectAddedApplyMultiplier(OnStatusEffectAddedRelicEffectParams relicEffectParams)
		{
		}

		public void OnStatusEffectAddedApplyAdder(OnStatusEffectAddedRelicEffectParams relicEffectParams)
		{
			if (relicEffectParams.fromEffectType == typeof(CardEffectTransferAllStatusEffects) || relicEffectParams.fromEffectType == typeof(CardEffectMultiplyStatusEffect))
			{
				return;
			}
			base.NotifyRelicTriggered(relicEffectParams.relicManager, relicEffectParams.characterState);
		}

		public void OnStatusEffectRemoved(OnStatusEffectAddedRelicEffectParams relicEffectParams)
		{
		}

		public int GetModifiedStatusEffectStacksFromMultiplier(StatusEffectStackData statusEffectStackData, CharacterState onCharacter)
		{
			return statusEffectStackData.count;
		}

		public int GetStatusEffectStacksToAdd(StatusEffectStackData statusEffectStackData, CharacterState onCharacter)
		{
			if (statusEffectStackData.statusId.ToLower() != this.statusId.ToLower() || (onCharacter != null && this.team != Team.Type.None && onCharacter.GetTeamType() != this.team))
			{
				return 0;
			}
			if (this.addOnlyIfNonZeroStart && statusEffectStackData.count <= 0)
			{
				return 0;
			}
			return this.additionalStacks;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00058556 File Offset: 0x00056756
		public StatusEffectStackData[] GetStatusEffects()
		{
			return new StatusEffectStackData[]
			{
				new StatusEffectStackData
				{
					statusId = this.statusId,
					count = this.additionalStacks,
				},
			};
		}

		
	}
}
