using System;
using System.IO;
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
            isAttack = mob.Damaged;
            SKILLTYPE type = skillList[index].type;
            
            if (playerInfo != null && (type == SKILLTYPE.ATTACK))
            {
                isAttack?.Invoke((playerInfo.attack * skillList[index].power) / mob.GetInfo().defence); //attack
                return (playerInfo.attack * skillList[index].power) / mob.GetInfo().defence;
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
                playerInfo.hp = Math.Max(0, playerInfo.hp - damage);
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
            Item Selectitem = inven[_index];
            if (Selectitem == null) return;
           
            switch (Selectitem.type)
            {
                case ITEMTYPE.WEAPON:
                    if (weapon != null)
                    {
                        playerInfo.attack -= weapon.attack;
                        playerInfo.defence -= weapon.defence;
                        inven.Add(weapon);
                    }
                    weapon = Selectitem;
                    break;
                case ITEMTYPE.ARMOR:
                    if (armor != null)
                    {
                        playerInfo.attack -= armor.attack;
                        playerInfo.defence -= armor.defence;
                        inven.Add(armor);
                    }
                    armor = Selectitem;
                    break;
                case ITEMTYPE.CONSUMABLE:
                    if(item != null)
                    {
                        inven.Add(item);
                    }
                    item = Selectitem;
                    break;
            }

            inven.RemoveAt(_index);
            if (Selectitem.type == ITEMTYPE.CONSUMABLE) return;

            if (playerInfo != null)
            {
                playerInfo.attack += Selectitem.attack;
                playerInfo.defence += Selectitem.defence;
            }

            if((Selectitem.skill.Count != 0 || skillList != null) && Selectitem.type == ITEMTYPE.WEAPON)
            {
                skillList.Clear();
                foreach (int id in Selectitem.skill)
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
            if(playerInfo != null)
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
            //quest.questId = ;
            //quest.questProgressAmount = ;
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
            {
                Console.WriteLine("저장 데이터가 없습니다.");
                return;
            }

            if (playerInfo == null)
                return;

            weapon = ObjectManager.Instance().GetItem(playerInfo.weaponId);
            armor = ObjectManager.Instance().GetItem(playerInfo.armorId);

            foreach(int id in weapon.skill)
            {
                skillList.Add(ObjectManager.Instance().GetSkill(id));
            }
            
            //quest = ObjectManager.Instance().GetQuest(playerInfo.questId);
            //quest.questProgressAmount = playerInfo.questProgress;
        }

        public Quest GetCurrentQuest()
        {
            return quest;
        }

        public Item[] GetPlayerEq()
        {
            Item[] ret = new Item[3];
            ret[(int)ITEMTYPE.WEAPON] = weapon ?? new Item() { name = "없음" };
            ret[(int)ITEMTYPE.ARMOR] = armor ?? new Item() { name = "없음" };
            ret[(int)ITEMTYPE.CONSUMABLE] = item ?? new Item() { name = "없음" };

            return ret;
             
        }

        void SaveInven()
        {
            StreamWriter streamWriter = new StreamWriter(path + "\\INVEN.spam");

            string outData = string.Empty;
            foreach (Item item in inven)
            {
                outData += item.id + ",";
            }

            streamWriter.WriteLine(outData);
            streamWriter.Close();
        }

        void LoadInven()
        {
            StreamReader reader = new StreamReader(path + "\\INVEN.spam");
            string outData = reader.ReadToEnd();
            string[] inData = outData.Split(",");

            foreach(string data in inData)
            {
                Item item = ObjectManager.Instance().GetItem(int.Parse(data));
                inven.Add(item);
            }
        }

        //-----------------

        //-----------------
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Team25");
        PlayerInfo? playerInfo = new PlayerInfo();
        List<Skill>? skillList = new List<Skill>();
        Item? weapon = null; //0
        Item? armor = null; //1
        Item? item = null;//2
        Quest? currentQuestId = null;
        Quest? currentQuestProgressAmount = null;

        int baseMaxExp = 100;
        int ConsumeCount = 3;

        //==
        public event UseSkillCallback? isAttack;
        private List<Item>? inven = new List<Item>();
        private Quest quest = new Quest();

    }
}
