# SuccClan

This is a custom clan for Monster Train. 
I 'm pretty true that there will be tons of bugs and balance problems, so please inform me if you have any suggestion~

## TODO
- Loading mod may take a long time.
- Sprite name = "Status_Frantic" in tooltip
- Balance problem.
- Add some cards to the Concealed Caverns:
	- Chaos Portal: Gain a random Rare Card from your choice of primary or allied clan.
	- Glowing Brands: Choice of three different clans: Get a Rare Clan draft pick.
	- Historian's Records: Choice of three random clans: Take the tome.
- Descriptions for my clan.

## Update Log

### Ver 0.1.0
- Uploaded my project.


## Tips for Developer

- Use StateName instead of StateType.
- If you want to buff unit permanently, you need to write your own custom CardEffect. Do not use the CardTrigger, there will be some bugs. You can refer to my unit "ShadowWarrior".
- The trigger for some types of card played: You need to listen both discard() and playcard().
- Offering cannot trigger the playcard(). You need to add that in the discard().
- If you want to find the UpgradeTree for champions in Assets, you can use the keyword "UpgradeTree".
- In CardEffectAddStatusEffect, only one status can be applied. Other status will be ignored.
	- For example, you want to add two status from one card, you need to add two CardEffectAddStatusEffect with one status each.
- CardEffectSacrifies may have some bugs for Tip. So I write my own version.

## Contact me
pointer_0@outlook.com
bilibili: µã×ÓPointer
Or just submit issues in this project!
