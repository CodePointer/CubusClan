using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

using SuccClan.Cards.SpellCards;

namespace SuccClan.Cards
{
	class MyCardPools
	{
		//public static CardPool ObsessingShardPool;
		public static CardPool VengefulShardPool;
		public static CardPool BlightAndJunkPool;
		public static CardUpgradeData exhaustUpgradeData;
		//public static CardPool StarterPool;

		public static void Make()
		{
			//ObsessingShardPool = new CardPoolBuilder
			//{
			//	CardPoolID = ObsessingShard.IDName + "_CardPool",
			//	CardIDs = new List<string>
			//	{
			//		ObsessingShard.IDName,
			//	},
			//}.BuildAndRegister();

			VengefulShardPool = new CardPoolBuilder
			{
				CardPoolID = VanillaCardIDs.VengefulShard + "_CardPool",
				CardIDs = new List<string>
				{
					VanillaCardIDs.VengefulShard
				},
			}.BuildAndRegister();

			exhaustUpgradeData = new CardUpgradeDataBuilder
			{
				TraitDataUpgradeBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName,
					},
				},
			}.Build();

			var BlightJunkIDs = new List<string>
			{ 
				VanillaCardIDs.SelfMutilation,
				VanillaCardIDs.SinnersBurden,
				VanillaCardIDs.TheUltimatePenance,
				VanillaCardIDs.WeightofContrition,
				VanillaCardIDs.CalcifiedEmber,
				VanillaCardIDs.DantesCandle,
				VanillaCardIDs.Deadweight,
				VanillaCardIDs.VengefulShard,
			};
			foreach (var card in CustomCardManager.CustomCardData)
			{
				if (card.Value.GetCardType() == CardType.Blight 
					|| card.Value.GetCardType() == CardType.Junk)
				{
					BlightJunkIDs.Add(card.Value.GetID());
				}
			}

			BlightAndJunkPool = new CardPoolBuilder
			{
				CardPoolID = "BlightAndJunk_CardPool",
				CardIDs = BlightJunkIDs,
			}.BuildAndRegister();
		}

		//public static void MakeLater()
		//{
		//	StarterPool = new CardPoolBuilder
		//	{
		//		CardPoolID = "Starter_CardPool",
		//		CardIDs = { Flogging.IDName, VanillaCardIDs.Torch },
		//	}.BuildAndRegister();
		//}
	}
}
