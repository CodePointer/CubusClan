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
	// RelicEffectAddNewStatusEffectOnApplyStatus
	// Refer to the RelicEffectAddTempUpgrade
	public sealed class RelicEffectAddNewStatusEffectOnApplyStatus : RelicEffectBase, IOnStatusEffectAddedRelicEffect, IRelicEffect, IStatusEffectRelicEffect
	{
		private Team.Type team;

		private string triggerStatusId;

		private StatusEffectStackData[] addedStatusEffects;

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
			this.triggerStatusId = relicEffectData.GetParamString().ToLower();
			this.addedStatusEffects = relicEffectData.GetParamStatusEffects();
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public void OnStatusEffectAddedApplyMultiplier(OnStatusEffectAddedRelicEffectParams relicEffectParams)
		{
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000584AC File Offset: 0x000566AC
		public void OnStatusEffectAddedApplyAdder(OnStatusEffectAddedRelicEffectParams relicEffectParams)
		{
			//if (relicEffectParams.fromEffectType == typeof(CardEffectTransferAllStatusEffects) || relicEffectParams.fromEffectType == typeof(CardEffectMultiplyStatusEffect))
			//{
			//	return;
			//}
			//Utils.BepLog(new List<string> 
			//{ 
			//	"StatusApplied: " + relicEffectParams.statusId,
			//	this.team.ToString(), relicEffectParams.characterState.GetTeamType().ToString(),
			//});
			if (relicEffectParams.statusId != this.triggerStatusId)
			{
				return;
			}
			if (this.team != Team.Type.None && relicEffectParams.characterState.GetTeamType() != this.team)
			{
				return;
			}

			int addedStacks = relicEffectParams.stacksAdded;
			var character = relicEffectParams.characterState;
			foreach (var addedStatusEffect in this.addedStatusEffects)
			{
				character.AddStatusEffect(addedStatusEffect.statusId, addedStacks);
				addedStatusEffect.count = addedStacks;
			}

			//Utils.BepLog(new List<string>
			//{
			//	"StatusFinished.",
			//	addedStacks.ToString(),
			//	character.ToString(),
			//});

			base.NotifyRelicTriggered(relicEffectParams.relicManager, relicEffectParams.characterState);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public void OnStatusEffectRemoved(OnStatusEffectAddedRelicEffectParams relicEffectParams)
		{
		}

		public int GetStatusEffectStacksToAdd(StatusEffectStackData statusEffectStackData, CharacterState onCharacter)
		{
			return 0;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00058556 File Offset: 0x00056756
		public StatusEffectStackData[] GetStatusEffects()
		{
			return this.addedStatusEffects;
		}

		public int GetModifiedStatusEffectStacksFromMultiplier(StatusEffectStackData statusEffectStackData, CharacterState onCharacter)
		{
			return statusEffectStackData.count;
		}
	}
}
