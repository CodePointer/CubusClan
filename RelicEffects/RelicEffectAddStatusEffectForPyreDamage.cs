using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using HarmonyLib;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

//using SuccClan.Cards;
namespace SuccClan.RelicEffects
{
	//RelicEffectAddStatusEffectForPyreDamage
	public sealed class RelicEffectAddStatusEffectForPyreDamage : RelicEffectBase, ITowerDamageTakenModifiedRelicEffect, IRelicEffect, IStatusEffectRelicEffect
	{
		private StatusEffectStackData[] addedStatusEffects;

		public override bool CanApplyInPreviewMode
		{
			get
			{
				return true;
			}
		}

		public MerchantData.Currency Currency { get; private set; }

		// Token: 0x060016E1 RID: 5857 RVA: 0x0005A1B3 File Offset: 0x000583B3
		public override void Initialize(RelicState relicState, RelicData srcRelicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, srcRelicData, relicEffectData);
			this.addedStatusEffects = relicEffectData.GetParamStatusEffects();
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0005A1DC File Offset: 0x000583DC
		public int ApplyModifiedDamage(int previousDamage, RelicEffectParams relicEffectParams)
		{
			if (previousDamage >= 0)
			{
				PyreRoomState pyreRoomState = relicEffectParams.roomManager.GetPyreRoom();
				CharacterState pyreHeart = pyreRoomState.GetPyreHeart();

				foreach (var statusEffect in addedStatusEffects)
				{
					//if (pyreHeart.IsImmune(statusEffect.statusId))
					//{
					//	List<string> statusEffectImmunities = Traverse.Create(pyreHeart).Field("statusEffectImmunities").GetValue<List<string>>();
					//	Utils.BepLog(statusEffectImmunities);
					//	//Traverse.Create(pyreHeart).Field("statusEffectImmunities");
					//}
					var addStatusEffectParams = new CharacterState.AddStatusEffectParams
					{
						overrideImmunity = true,
					};
					pyreHeart.AddStatusEffect(statusEffect.statusId, statusEffect.count, addStatusEffectParams);
				}

				base.NotifyRelicTriggered(relicEffectParams.relicManager, relicEffectParams.roomManager.GetPyreRoom());
			}
			
			return previousDamage;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0005A2B2 File Offset: 0x000584B2
		public override string GetActivatedDescription()
		{
			if (this.Currency == MerchantData.Currency.Crystals)
			{
				return string.Format(this._srcRelicState.GetRelicActivatedKey().Localize(null), this.lastDamageAmount);
			}
			return base.GetActivatedDescription();
		}

		public bool ImpactsPyre()
		{
			return true;
		}

		public StatusEffectStackData[] GetStatusEffects()
		{
			return this.addedStatusEffects;
		}

		// Token: 0x04000C0F RID: 3087
		private int lastDamageAmount = 0;
	}
}
