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
	// Refer to the RelicEffectAddTempUpgrade
	public class RelicEffectAddStatusEffectOnEnterChampionRoom : RelicEffectBase, ICharacterActionRelicEffect,
		IRelicEffect, IStatusEffectRelicEffect
	{
		private StatusEffectStackData[] statusEffects;
		private Team.Type targetTeamType;
		private bool firstWaveOnly;

		public override bool CanShowNotifications
		{
			get
			{
				return false;
			}
		}

		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this.statusEffects = relicEffectData.GetParamStatusEffects();
			this.targetTeamType = relicEffectData.GetParamSourceTeam();
			this.firstWaveOnly = relicEffectData.GetParamBool();
		}

		public bool TestCharacterTriggerEffect(CharacterTriggerRelicEffectParams relicEffectParams)
		{
			if (relicEffectParams.characterState.IsDestroyed)
			{
				return false;
			}

			bool flag1 = relicEffectParams.trigger == CharacterTriggerData.Trigger.PostAscension || relicEffectParams.trigger == CharacterTriggerData.Trigger.AfterSpawnEnchant;
			bool flag2 = this.targetTeamType == Team.Type.None || (this.targetTeamType & relicEffectParams.characterState.GetTeamType()) > Team.Type.None;
			bool flag3 = relicEffectParams.characterState.HasStatusEffect("untouchable");
			bool flag4 = !this.firstWaveOnly || relicEffectParams.combatManager.GetCombatPhase() == CombatManager.Phase.Start;
			bool flag = flag1 && flag2 && !flag3 && flag4;

			bool flagHasChampion = false;
			CharacterState targetCharacter = relicEffectParams.characterState;
			RoomState roomOwner = targetCharacter.GetSpawnPoint(false).GetRoomOwner();
			var charList = new List<CharacterState>();
			targetCharacter.GetCombatManager().GetAllCharactersInRoom(charList, roomOwner);
			foreach (var character in charList)
			{
				if (character.IsChampion())
				{
					flagHasChampion = true;
					break;
				}
			}

			//Utils.BepLog(new List<string>
			//{
			//	"TestCharacterTriggerEffect",
			//	flag1.ToString(), flag2.ToString(), flag3.ToString(), flag4.ToString(),
			//	flagHasChampion.ToString(),
			//});

			return flag && flagHasChampion;
		}

		public IEnumerator ApplyCharacterTriggerEffect(CharacterTriggerRelicEffectParams relicEffectParams)
		{
			Utils.BepLog(new List<string> { "ApplyCharacterTriggerEffect" });
			for (int i = 0; i < this.statusEffects.Length; i++)
			{
				int numStacks = this.statusEffects[i].count;
				var addStatusEffectParams = new CharacterState.AddStatusEffectParams
				{
					sourceRelicState = this._srcRelicState
				};
				CharacterState characterState = relicEffectParams.characterState;
				if (characterState.GetCharacterManager().DoesCharacterPassSubtypeCheck(characterState, null))
				{
					characterState.AddStatusEffect(this.statusEffects[i].statusId, numStacks, addStatusEffectParams);
				}
			}
			yield break;
		}

		public StatusEffectStackData[] GetStatusEffects()
		{
			return this.statusEffects;
		}
	}
}
