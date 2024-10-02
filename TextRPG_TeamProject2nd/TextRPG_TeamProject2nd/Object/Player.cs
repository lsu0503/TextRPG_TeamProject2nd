using System;
using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{

    internal class Player
    {
        public void Init()
        {
            
        }
        //-----------------
        /// <summary>
        /// 사용할 스킬(전투중 액션 번호), 몬스터 객체를 입력받아 해당 몬스터 객체에게 스킬효과를 가합니다.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mob"></param>
        public int UseSkill(int index, Monster mob = null)
        {
            isAttack += mob.Damaged;
            SKILLTYPE type = skillList[index].type;
            
            if (playerInfo != null && (type == SKILLTYPE.ATTACK))
            {
                isAttack?.Invoke(playerInfo.attack * skillList[index].power); //attack
                return playerInfo.attack * skillList[index].power;
            }
            else if (playerInfo != null && type == SKILLTYPE.HEAL)
            {
                playerInfo.hp = Math.Min(playerInfo.maxHp, playerInfo.hp + skillList[index].power);
                return skillList[index].power;
            }

            return 0;
        }
        public int UseItem()
        {
            if (item == null || ConsumeCount <= 0) return 0;
            playerInfo.hp = Math.Min(playerInfo.hp + item.amount, playerInfo.maxHp);
            ConsumeCount--;
            return item.amount;
        }
        public void Damaged(int damage)
        {
            if (playerInfo != null)
                playerInfo.hp = Math.Max(0, playerInfo.hp - (damage / playerInfo.defence));
        }

        public void GetExp(int val)
        {
            if ((playerInfo.exp + val) > playerInfo.maxHp)
            {
                playerInfo.exp = 0;
                IncreaseLevel();
            }
        }
        public void IncreaseLevel()
        {
            if (playerInfo == null)
                return;

            playerInfo.maxExp = baseMaxExp + playerInfo.level * 25;
            //TODO : 플레이어가 레벨업 하면 상승하는 스탯
            
        }

        public void PushInven(Item _item)
        {
            if (inven == null) return;
            inven.Add(_item);
        }
        public int PopInven(int _index)
        {
            if (inven == null) return 0;
            int value = inven[_index].value;
            inven.RemoveAt(_index);
            return value;

        }

        public void EQItem(int _index)
        {
            Item item = inven[_index];
            if (item == null) return;
           
            switch (item.type)
            {
                case ITEMTYPE.WEAPON:
                    if (weapon != null)
                    {
                        playerInfo.attack -= weapon.attack;
                        playerInfo.defence -= weapon.defence;
                        inven.Add(weapon);
                    }
                    weapon = item;
                    break;
                case ITEMTYPE.ARMOR:
                    if (armor != null)
                    {
                        playerInfo.attack -= armor.attack;
                        playerInfo.defence -= armor.defence;
                        inven.Add(armor);
                    }
                    armor = item;
                    break;
            }

            inven.RemoveAt(_index);

            if (playerInfo != null)
            {
                playerInfo.attack += item.attack;
                playerInfo.defence += item.defence;
            }

            if(item.skill.Count != 0 || skillList != null)
            {
                skillList.Clear();
                foreach (int id in item.skill)
                    skillList.Add(ObjectManager.Instance().GetSkill(id));
            }
            
               
        }
        public void DQItem(int index)
        {
            if(inven == null || inven[index] == null) return;
            Item item = inven[index];

            if (inven[index].type == ITEMTYPE.WEAPON)
            {
                inven.Add(weapon);
                weapon = null;
            }
            else if(inven[index].type == ITEMTYPE.ARMOR)
            {
                inven.Add(armor);
                armor = null;
            }

            if(skillList != null)
                skillList.Clear();

            if(playerInfo != null)
            {
                playerInfo.attack   -= item.attack;
                playerInfo.defence  -= item.defence;
            }
         
        }//더미

        public void BackVill()
        {
            ConsumeCount = 3;
            playerInfo.hp = playerInfo.maxHp;
        }

        public List<Skill> GetPlayerSkillList() {return skillList ?? new List<Skill>(); }
        public List<Item> GetPlayerInvenList() { return inven ?? new List<Item>(); }
        public PlayerInfo GetInfo()
        {
            if(playerInfo != null)
                return playerInfo;

            return null;
        }
        //-----------------
        public void SetPlayer(int _raceId, string name)
        {
            Race race = ObjectManager.Instance().GetRace(_raceId);
            if (race == null)
                return;

            playerInfo.level = 1;
            playerInfo.maxExp = baseMaxExp;
            playerInfo.hp = race.hp;
            playerInfo.maxHp = race.hp;
            playerInfo.attack = race.attack;
            playerInfo.defence = race.defence;
            playerInfo.actionPoint = race.actionPoint;
            playerInfo.race = race.name;
            playerInfo.name = name;
            playerInfo.money = 1000;
        }
        public void Save()
        {
            if (playerInfo == null) return;
            
            if(weapon != null)
                playerInfo.weaponId = weapon.id;

            if(armor != null)
                playerInfo.armorId = armor.id;

            FileManager.Instance().SavePlayer();
        }
        public void Load()
        {
            if (!FileManager.Instance().LoadPlayer())
                Console.WriteLine("저장 데이터가 없습니다.");

            if (playerInfo == null)
                return;

            weapon = ObjectManager.Instance().GetItem(playerInfo.weaponId);
            armor = ObjectManager.Instance().GetItem(playerInfo.armorId);

            if(weapon.skill.Count >= 0)
            {
                foreach (int val in weapon.skill)
                    skillList.Add(ObjectManager.Instance().GetSkill(val));
            }
            
        }

        
        //-----------------

        //-----------------

        PlayerInfo? playerInfo = new PlayerInfo();
        List<Skill>? skillList = new List<Skill>();
        Item? weapon = null;
        Item? armor = null;
        Item? item = null;

        int baseMaxExp = 100;
        int ConsumeCount = 3;

        //==
        public event UseSkillCallback? isAttack;
        private List<Item>? inven = new List<Item>();
    }
}
