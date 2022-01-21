# CubusClan
 
## TODO List

### Implementation

- 1 Uncommon Spell: 牺牲一个Ghost。抽牌，对敌人造成伤害。
- 1 Uncommon Unit：蛾子。对所有敌人造成等同于攻击力的伤害。
- 将所有Ghost转变为相位锁定修改版（潜行），+1血。存在可能的问题：boss时是否会导致问题？敌方单位亡语是否会杀前排？可以参考disciple里面的boss石。
- All Upgrade for Second Champion
- Relic: Profane Crossbow
- Essence of Subunits
- Change all the StateType into StateName 完成一半。
- KnightMare的Soul Blust说明似乎还有些问题。需要修改。
- Add some cards into the Caverns

### Localization

- Enssence of Subunits
- The Enssence name? Where?

### Bugs

- Tooltip of ForTheQueen
- 主动打出非消耗的牌时无法触发狂热效果。

### Game Balance Adjustment

- ShadowLady_Profanity, 收割潜行有点太强了。
- 狂热加费有点太强了。需要考虑削弱。

## Tips for Developer

- Use StateName instead of StateType.
- If you want to buff unit permanently, you need to write your own custom CardEffect. Do not use the CardTrigger, there will be some bugs.
- The trigger for some types of card played: 不光在discard的时候需要监听，在play card的时候也需要。另外献祭词条是无法触发playcard这个trigger的，需要在discard里面自己写。
- UpgradeTree for champions, search 'UpgradeTree' by Assets.
- CardEffectAddStatusEffect, only one status can be applied.