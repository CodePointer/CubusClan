using System;
using System.Collections;
using ShinyShoe.Logging;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

//using SuccClan.Cards;
namespace SuccClan.RelicEffects
{
	//RelicEffectAddUpgradeOnSpawn
	public sealed class RelicEffectAddUpgradeOnSpawn : RelicEffectBase
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0000C623 File Offset: 0x0000A823
		public override bool CanShowNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00058298 File Offset: 0x00056498
		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.targetTeam = relicEffectData.GetParamSourceTeam();
			this.characterSubtype = relicEffectData.GetParamCharacterSubtype();
			this.excludeCharacterSubtypes = relicEffectData.GetParamExcludeCharacterSubtypes();
			this.restrictToRoom = relicEffectData.GetParamBool();
			this.restrictedRoomIndex = relicEffectData.GetParamInt();
			this.allowFromCard = (relicEffectData.GetParamString() != "NoCard");
			this.onlyAllowedFromCard = (relicEffectData.GetParamString() == "OnlyFromCard");

			this.cardUpgradeData = relicEffectData.GetParamCardUpgradeData();
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00058322 File Offset: 0x00056522
		public override IEnumerator OnCharacterAdded(CharacterState character, CardState fromCard, RelicManager relicManager, SaveManager saveManager, PlayerManager playerManager, RoomManager roomManager, CombatManager combatManager, CardManager cardManager)
		{
			bool overrideImmunity = this.characterSubtype.IsPyre;
			bool flag = fromCard != null && fromCard.GetSpawnCharacterData() == character.GetSourceCharacterData();
			if (this.cardUpgradeData == null)
			{
				yield break;
			}
			if (character.HasStatusEffect("cardless"))
			{
				flag = false;
			}
			if (flag && !this.allowFromCard)
			{
				yield break;
			}
			if (!flag && this.onlyAllowedFromCard)
			{
				yield break;
			}
			if (character.GetTeamType() != this.targetTeam || (!overrideImmunity && character.HasStatusEffect("immune")))
			{
				yield break;
			}
			if (character.HasStatusEffect("untouchable"))
			{
				yield break;
			}
			Utils.BepLog(new List<string> { "3" });
			foreach (SubtypeData subtypeData in this.excludeCharacterSubtypes)
			{
				if (character.GetHasSubtype(subtypeData))
				{
					yield break;
				}
			}
			if (this.restrictToRoom && this.restrictedRoomIndex != character.GetSpawnPoint(false).GetRoomOwner().GetRoomIndex())
			{
				yield break;
			}

			if (character.GetCharacterManager().DoesCharacterPassSubtypeCheck(character, this.characterSubtype))
			{
				yield return base.TimingYieldIfNonCovenant(RelicEffectBase.TimingContext.PreFire, saveManager);
				CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams
				{
					overrideImmunity = overrideImmunity,
					sourceRelicState = this._srcRelicState
				};

				CardUpgradeState cardUpgradeState = new CardUpgradeState();
				cardUpgradeState.Setup(this.cardUpgradeData);
				yield return character.ApplyCardUpgrade(cardUpgradeState, false);
				
				base.NotifyRelicTriggered(relicManager, character);
				yield return base.TimingYieldIfNonCovenant(RelicEffectBase.TimingContext.PostFire, saveManager);
			}
			yield break;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0005834E File Offset: 0x0005654E
		public bool ImpactsPyre()
		{
			return this.characterSubtype.IsPyre && this.targetTeam == Team.Type.Monsters;
		}

		// Token: 0x04000BAC RID: 2988
		private Team.Type targetTeam;

		// Token: 0x04000BAE RID: 2990
		private SubtypeData characterSubtype;

		// Token: 0x04000BAF RID: 2991
		private SubtypeData[] excludeCharacterSubtypes;

		// Token: 0x04000BB0 RID: 2992
		private bool restrictToRoom;

		// Token: 0x04000BB1 RID: 2993
		private int restrictedRoomIndex;

		// Token: 0x04000BB2 RID: 2994
		private bool allowFromCard;

		// Token: 0x04000BB3 RID: 2995
		private bool onlyAllowedFromCard;

		private CardUpgradeData cardUpgradeData;
	}
}
