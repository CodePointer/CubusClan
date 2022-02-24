using System;
using System.Collections.Generic;
using System.Text;

using ShinyShoe;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Effects
{
	class StatusEffectPsionic : StatusEffectState
	{
		public const string IDName = "status_psionic";

		public override bool TestTrigger(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
		{
			return false;
		}

		protected override System.Collections.IEnumerator OnTriggered(InputTriggerParams inputTriggerParams, OutputTriggerParams outputTriggerParams)
		{
			yield break;
		}

		public override int GetEffectMagnitude(int stacks = 1)
		{
			return 0;
		}

		public static void Make()
		{
			new StatusEffectDataBuilder
			{
				StatusEffectStateName = typeof(StatusEffectPsionic).AssemblyQualifiedName,
				StatusId = IDName,
				DisplayCategory = StatusEffectData.DisplayCategory.Persistent,
				TriggerStage = StatusEffectData.TriggerStage.None,
				IsStackable = true,
				RemoveStackAtEndOfTurn = false,
				IconPath = "Status/" + IDName + ".png"
			}.Build();
		}

	}
}
