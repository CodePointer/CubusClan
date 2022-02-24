using System;
using System.Collections.Generic;
using System.Text;

using ShinyShoe;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Effects
{
	class StatusEffectFrantic : StatusEffectState
	{
		public const string IDName = "status_frantic";  // You'd better use lower for status id. The CardTraitScalingAddStatusEffect will use lower case id for matching.

		public override bool TestTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
		{
			return inputTriggerParams.associatedCharacter != null 
				&& inputTriggerParams.associatedCharacter.IsAlive 
				&& inputTriggerParams.associatedCharacter.GetStatusEffectStacks(base.GetStatusId()) > 0;
		}

		protected override System.Collections.IEnumerator OnTriggered(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
		{
			CoreSignals.DamageAppliedPlaySound.Dispatch(Damage.Type.DirectAttack);
			CombatManager combatManager = inputTriggerParams.combatManager;
			CharacterState thisCharacter = inputTriggerParams.associatedCharacter;
			RoomState roomOwner = inputTriggerParams.associatedCharacter.GetSpawnPoint(false).GetRoomOwner();

			// Multistrike
			int multiStrikeStack = thisCharacter.GetStatusEffectStacks(VanillaStatusEffectIDs.Multistrike);
			int currentHP = thisCharacter.GetHP();
			for (int i = 0; i <= multiStrikeStack; i++)
			{
				if (thisCharacter.IsAlive)
				{
					List<CharacterState> targetList = new List<CharacterState>();
					combatManager.GetAllCharactersInRoom(targetList, roomOwner);
					CharacterState target = thisCharacter;
					foreach (CharacterState unit in targetList)
					{
						if (unit.GetTeamType() == thisCharacter.GetTeamType())
						{
							target = unit;
							break;
						}
					}

					int damageAmount = this.GetDamageAmount(thisCharacter);

					var parameters = default(CombatManager.ApplyDamageToTargetParameters);
					parameters.damageType = Damage.Type.DirectAttack;
					StatusEffectData sourceStatusEffectData = base.GetSourceStatusEffectData();
					parameters.vfxAtLoc = ((sourceStatusEffectData != null) ? sourceStatusEffectData.GetOnAffectedVFX() : null);
					parameters.showDamageVfx = true;
					parameters.relicState = inputTriggerParams.suppressingRelic;

					yield return combatManager.ApplyDamageToTarget(damageAmount, target, parameters);

					if (target == thisCharacter)
					{
						currentHP -= damageAmount;
					}
				}
				else
				{
					break;
				}
			}

			outputTriggerParams.canAttackOrHeal = false;
			yield break;
		}

		public override int GetEffectMagnitude(int stacks = 1)
		{
			CharacterState associatedCharacter = base.GetAssociatedCharacter();
			if (associatedCharacter != null && associatedCharacter.GetSpawnPoint(false) != null 
				&& associatedCharacter.GetSpawnPoint(false).GetRoomOwner() != null)
			{
				return this.GetDamageAmount(associatedCharacter);
			}
			return 0;
		}

		private int GetDamageAmount(CharacterState characterState)
		{
			int characterDamage = characterState.GetAttackDamage();
			return characterDamage;
		}

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectFrantic).AssemblyQualifiedName,
				StatusId = IDName,
				DisplayCategory = StatusEffectData.DisplayCategory.Negative,
				TriggerStage = StatusEffectData.TriggerStage.OnCombatTurnSpark,
				IsStackable = true,
				RemoveStackAtEndOfTurn = true,
				IconPath = "Status/" + IDName + ".png"
			}.Build();
		}

	}
}
