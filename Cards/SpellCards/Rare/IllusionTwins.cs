using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

using SuccClan.CardEffects;

namespace SuccClan.Cards.SpellCards
{
	class IllusionTwins
	{
		public static string IDName = "Spell_IllusionTwins";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 2,
				Rarity = CollectableRarity.Rare,
				TargetsRoom = true,
				Targetless = false,

				TraitBuilders = new List<CardTraitDataBuilder>
				{
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitExhaustState,
					},
					new CardTraitDataBuilder
					{
						TraitStateType = VanillaCardTraitTypes.CardTraitIntrinsicState,
					},
				},

				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateType = typeof(CardEffectCopyUnitWithUpgrade),
						TargetMode = TargetMode.DropTargetCharacter,
						TargetTeamType = Team.Type.Monsters,
						ParamInt = 1,
						AdditionalParamInt = (int)SpawnMode.BackSlot,
						ParamBool = false,  // Restore the health
						ParamCardUpgradeData = new CardUpgradeDataBuilder
						{
							StatusEffectUpgrades = new List<StatusEffectStackData>
							{
								new StatusEffectStackData
								{
									statusId = VanillaStatusEffectIDs.Fragile,
								},
							},
						}.Build(),
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
