# SuccClan

This is a custom clan for Monster Train. 
I 'm pretty true that there will be tons of bugs and balance problems, so please inform me if you have any suggestion~

## TODO
- Loading mod may take a long time.
- Balance problem.
- Add some cards to the Concealed Caverns:
	- Chaos Portal: Gain a random Rare Card from your choice of primary or allied clan.
	- Glowing Brands: Choice of three different clans: Get a Rare Clan draft pick.
	- Historian's Records: Choice of three random clans: Take the tome.
- Descriptions for my clan.

## Known bugs


## Update Log

### Ver 0.1.4
- Fix the bug of ObsessingAromatherapy.
- Fix the bug of Consume stone.

- Buff the ShadowLady.

### Ver 0.1.3
- Fix the translation problem for Champion update.
- Removed the icon of custom status in tooltip.

- The Blood Thursty of Shadow Lady has been buffed.

### Ver 0.1.2
- Improve the translation csv file.
- Fixed the tooltip for Cubus Spike.
- Forget to remove some debug code... Fixed.

- Use a custom status 'Psionic' instead of 'Soul' for KnightMare.

### Ver 0.1.1
- Fixed some translation problems.
- Fixed the Surgestone did not work on the Power Siphon.
- Fixed the Mind Domaination have 'no valid target' problem.
- Subtype names now is shown under the unit cards.

- Dark Fury: +Consume.
- Incubus Butcher: +2[health] -> +1[health]

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
- CardTraitScalingAddStatusEffect & CardEffectAddStatusEffect for X card will take the lower case statusId for matching. So highly recommended to use lower for status id. 

## Contact me
pointer_0@outlook.com

bilibili: 点子Pointer

Or just submit issues in this project!
