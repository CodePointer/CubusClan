using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;

namespace SuccClan.Cards.SpellCards
{
	class BloodCarnival
	{
		public static string IDName = "Spell_BloodCarnival";

		public static void Make()
		{
			CardDataBuilder railyard = new CardDataBuilder
			{
				Cost = 0,
				Rarity = CollectableRarity.Common,
				TargetsRoom = true,
				Targetless = true,

				EffectBuilders = new List<CardEffectDataBuilder>
				{
					new CardEffectDataBuilder
					{
						EffectStateType = VanillaCardEffectTypes.CardEffectBuffDamage,
						ParamInt = 8,
						TargetMode = TargetMode.Room,
						TargetTeamType = Team.Type.Heroes | Team.Type.Monsters,
					},
				},
			};

			Utils.AddSpell(railyard, IDName);
			Utils.AddImg(railyard, IDName + ".png");

			railyard.BuildAndRegister();
		}
	}
}
