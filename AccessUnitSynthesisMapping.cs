using System;
using System.Collections.Generic;
using System.Text;

using HarmonyLib;

using Trainworks;
using Trainworks.Builders;
using Trainworks.Utilities;
using Trainworks.Constants;
using Trainworks.Managers;

namespace SuccClan
{
	public class AccessUnitSynthesisMapping
	{
		public static void FindUnitSynthesisMappingInstanceToStub()
		{
			// Gets a reference to AllGameData with Trainworks
			AllGameData testData = ProviderManager.SaveManager.GetAllGameData();
			Utils.BepLog(new List<string> { 
				"testData",
				testData.ToString(),
			});

			// Use AllGameData to get access to BalanceData
			BalanceData balanceData = testData.GetBalanceData();
			Utils.BepLog(new List<string> {
				"balanceData",
				balanceData.ToString(),
			});

			// Use BalanceData to get access to the current instance of the UnitSynthesisMapping
			UnitSynthesisMapping mappingInstance = balanceData.SynthesisMapping;
			if (mappingInstance == null)
			{
				Utils.BepLog(new List<string> { "Failed to find a mapping instance" });
			}
			else
			{
				Utils.BepLog(new List<string> { "Find mapping instance: ", mappingInstance.GetID() });
			}

			// Calls CollectMappingData method
			RecallingCollectMappingData.CollectMappingDataStub(mappingInstance);
			Trainworks.Trainworks.Log(BepInEx.Logging.LogLevel.All, "OUTPUTHERE");
		}

		[HarmonyPatch(typeof(UnitSynthesisMapping), "CollectMappingData", new Type[] { })]
		class RecallingCollectMappingData
		{
			[HarmonyReversePatch]
			public static void CollectMappingDataStub(object instance)
			{
				// It's a stub so it has no initial content
				Utils.BepLog(new List<string> { "Stub is called." });
			}
		}
	}
}
