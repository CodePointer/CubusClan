# CubusClan
 
## TODO List

### Implementation

- 1 Uncommon Spell: ����һ��Ghost�����ƣ��Ե�������˺���
- 1 Uncommon Unit�����ӡ������е�����ɵ�ͬ�ڹ��������˺���
- ������Ghostת��Ϊ��λ�����޸İ棨Ǳ�У���+1Ѫ�����ڿ��ܵ����⣺bossʱ�Ƿ�ᵼ�����⣿�з���λ�����Ƿ��ɱǰ�ţ����Բο�disciple�����bossʯ��
- All Upgrade for Second Champion
- Relic: Profane Crossbow
- Essence of Subunits
- Change all the StateType into StateName ���һ�롣
- KnightMare��Soul Blust˵���ƺ�����Щ���⡣��Ҫ�޸ġ�
- Add some cards into the Caverns

### Localization

- Enssence of Subunits
- The Enssence name? Where?

### Bugs

- Tooltip of ForTheQueen
- ������������ĵ���ʱ�޷���������Ч����

### Game Balance Adjustment

- ShadowLady_Profanity, �ո�Ǳ���е�̫ǿ�ˡ�
- ���ȼӷ��е�̫ǿ�ˡ���Ҫ����������

## Tips for Developer

- Use StateName instead of StateType.
- If you want to buff unit permanently, you need to write your own custom CardEffect. Do not use the CardTrigger, there will be some bugs.
- The trigger for some types of card played: ������discard��ʱ����Ҫ��������play card��ʱ��Ҳ��Ҫ�������׼��������޷�����playcard���trigger�ģ���Ҫ��discard�����Լ�д��
- UpgradeTree for champions, search 'UpgradeTree' by Assets.
- CardEffectAddStatusEffect, only one status can be applied.