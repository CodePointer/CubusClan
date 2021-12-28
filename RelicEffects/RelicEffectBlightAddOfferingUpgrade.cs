using System;
using System.Collections.Generic;
using System.Text;

using Trainworks.Builders;
using Trainworks.Constants;
using Trainworks.Managers;

//using SuccClan.Cards;
namespace SuccClan.RelicEffects
{
	// Refer to the RelicEffectAddTempUpgrade
	public class RelicEffectBlightAddOfferingUpgrade : RelicEffectBase, ICardModifierRelicEffect,
		IRelicEffect
	{
		private Team.Type _sourceTeam;
		private CardUpgradeData _cardUpgradeData;

		public override void Initialize(RelicState relicState, RelicData relicData, RelicEffectData relicEffectData)
		{
			base.Initialize(relicState, relicData, relicEffectData);
			this._sourceTeam = relicEffectData.GetParamSourceTeam();
			this._cardUpgradeData = relicEffectData.GetParamCardUpgradeData();
		}

		public bool ApplyCardStateModifiers(CardState cardState, SaveManager saveManager, 
			CardManager cardManager, RelicManager relicManager)
		{
			bool result = false;
			if (this._sourceTeam == Team.Type.Monsters)
			{
				// ** My custom: Your filters here. **
				if (cardState.GetCardType() != CardType.Blight && cardState.GetCardType() != CardType.Junk)
				{
					return false;
				}
				// ** End my custom. **

				CardStateModifiers temporaryCardStateModifiers = cardState.GetTemporaryCardStateModifiers();
				CardUpgradeState cardUpgradeState = new CardUpgradeState();
				cardUpgradeState.Setup(this._cardUpgradeData, false);
				temporaryCardStateModifiers.AddUpgrade(cardUpgradeState, null);
				cardState.UpdateCardBodyText(null);
				if (cardManager != null)
				{
					cardManager.RefreshCardInHand(cardState, true);
				}
				return true;
			}
			return result;
		}

		public bool GetTooltip(out string title, out string body)
		{
			title = string.Empty;
			body = string.Empty;
			return false;
		}
	}
}
