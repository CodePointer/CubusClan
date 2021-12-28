using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using SuccClan.Effects;

namespace SuccClan.CardEffects
{
	public sealed class CardTraitFranticAddStatus : CardTraitState
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x00024166 File Offset: 0x00022366
		public override int OnStatusEffectApplied(CharacterState affectedCharacter, CardState thisCard, CardManager cardManager, RelicManager relicManager, string statusId, int sourceStacks = 0)
		{
			return this.GetAdditionalStacks(affectedCharacter);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00024184 File Offset: 0x00022384
		private int GetAdditionalStacks(CharacterState affectedCharacter)
		{
			RoomState roomOwner = affectedCharacter.GetSpawnPoint(false).GetRoomOwner();
			int totalFranticStack = 0;
			var charList = new List<CharacterState>();
			affectedCharacter.GetCombatManager().GetAllCharactersInRoom(charList, roomOwner);
			foreach(CharacterState character in charList)
			{
				var listStatus = new List<CharacterState.StatusEffectStack>();
				int stacks = character.GetStatusEffectStacks(StatusEffectFrantic.IDName);
				totalFranticStack += stacks;
			}

			return totalFranticStack;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001C727 File Offset: 0x0001A927
		public override bool HasMultiWordDesc()
		{
			return true;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000241B0 File Offset: 0x000223B0
		public override string GetCurrentEffectText(CardStatistics cardStatistics, SaveManager saveManager, RelicManager relicManager)
		{
			//if (cardStatistics != null && cardStatistics.GetStatValueShouldDisplayOnCardNow(base.StatValueData))
			//{
			//	return string.Format("CardTraitBlightAddEnergy_CurrentScaling_CardText".Localize(null), this.GetAdditionalEnergy(cardStatistics, true));
			//}
			return string.Empty;
		}
	}
}
