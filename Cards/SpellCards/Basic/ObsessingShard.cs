using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.SpellCards
{
	class ObsessingShard
	{
		public static string IDName = "Blight_ObsessingShard";
		//public static CardPool cardPool;
		//public static CardPool vengefulCardPool;

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 1,
				Rarity = CollectableRarity.Common,
				TargetsRoom = false,
				Targetless = true,

				CardType = CardType.Blight,

				TriggerBuilders = new List<CardTriggerEffectDataBuilder>
				{
					new CardTriggerEffectDataBuilder
					{
						Trigger = CardTriggerType.OnUnplayed,
						DescriptionKey = IDName + "_OnReserve_Desc",
						CardEffectBuilders = new List<CardEffectDataBuilder>
						{
							new CardEffectDataBuilder
							{
								EffectStateName = VanillaCardEffectTypes.CardEffectDamage.AssemblyQualifiedName,
								ParamInt = 1,
								TargetMode = TargetMode.Pyre
							},
						},
					}
				},

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateName = VanillaCardTraitTypes.CardTraitExhaustState.AssemblyQualifiedName,
					},
				}
			};

			Utils.AddSpellWithoutPool(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();

			// Make card pool
			//cardPool = new CardPoolBuilder
			//{
			//	CardPoolID = IDName + "_CardPool",
			//	CardIDs = new List<string>
			//	{
			//		IDName,
			//	},
			//}.BuildAndRegister();

			//vengefulCardPool = new CardPoolBuilder
			//{
			//	CardPoolID = IDName + "_CardPoolVengeful",
			//	CardIDs = new List<string>
			//	{
			//		VanillaCardIDs.VengefulShard,
			//	},
			//}.BuildAndRegister();
		}
	}
}
