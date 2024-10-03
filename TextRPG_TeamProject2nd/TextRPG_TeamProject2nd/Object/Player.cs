using System;
using System.IO;
using System.Text;
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
        public int UseSkill(int index, out bool isCrit, Monster mob = null)
        {
            isAttack = mob.Damaged;
            SKILLTYPE type = skillList[index].type;
            actionPoint -= skillList[index].cost;
            isCrit = false;


            if (playerInfo != null && (type == SKILLTYPE.ATTACK))
            {
                if (new Random().NextDouble() < 0.9f)
                {
                    int damage = (int)((playerInfo.attack * skillList[index].power) * MathF.Pow(1.2f, playerInfo.level) / mob.GetInfo().defence);
                    if (new Random().NextDouble() < 0.15f)
                    {
                        isCrit = true;
                        damage = (int)(damage * 1.6f);
                    }
                    isAttack?.Invoke(damage); //attack
                    return damage;
                }
                else
                    return -1;
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

        public void OnNextTurn()
        {
            if (actionPoint >= 0)
                actionPoint = playerInfo.actionPoint;
            else
                actionPoint += playerInfo.actionPoint;
        }

        public bool GetExp(int val)
        {
            bool result = false;

            playerInfo.exp += val;

            while (playerInfo.exp >= playerInfo.maxExp)
            {
                playerInfo.exp -= playerInfo.maxExp;
                IncreaseLevel();
                result = true;
            }

            return result;
        }
        public void IncreaseLevel()
        {
            if (playerInfo == null)
                return;

            playerInfo.level++;
            playerInfo.maxExp += playerInfo.level * 25;

            playerInfo.maxHp = (int)(playerInfo.maxHp * 1.2f);
            
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
            if (playerInfo != null)
            {
                playerInfo.hp = playerInfo.maxHp;
                actionPoint = playerInfo.actionPoint;
            }
        }

        public int GetActionPoint() { return actionPoint; }
        public int GetConsumeCount() { return ConsumeCount; }
        public Skill GetSkill(int index) { return skillList[index]; }
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
            playerInfo.questId = 0;
            playerInfo.questProgress = 0;
            quest = ObjectManager.Instance().GetQuest(playerInfo.questId);
        }
        public void Save()
        {
            if (playerInfo == null) return;
            
            if(weapon != null)
                playerInfo.weaponId = weapon.id;

            if(armor != null)
                playerInfo.armorId = armor.id;

            if(item != null)
                playerInfo.consumeId = item.id;

            if (quest != null)
                playerInfo.questProgress = quest.questProgressAmount;

            FileManager.Instance().SavePlayer();
            SaveInven();
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
            item = ObjectManager.Instance().GetItem(playerInfo.consumeId);

            skillList.Clear();
            if(weapon != null)
                foreach(int id in weapon.skill)
            {
                skillList.Add(ObjectManager.Instance().GetSkill(id));
            }
            
            quest = ObjectManager.Instance().GetQuest(playerInfo.questId);
            quest.questProgressAmount = playerInfo.questProgress;
            LoadInven();
        }

        public Quest GetCurrentQuest()
        {
            return quest;
        }

        public void SetQuest(Quest _quest)
        {
            quest = _quest;
            playerInfo.questId = quest.questId;
            playerInfo.questProgress = quest.questProgressAmount;
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
        }//이동

        void LoadInven()
        {
            StreamReader reader = new StreamReader(path + "\\INVEN.spam");
            string outData = reader.ReadLine();
            string[] inData = outData.Split(",");

            inven.Clear();
            foreach(string data in inData)
            {
                if (data == "") break;
                Item item = ObjectManager.Instance().GetItem(int.Parse(data));
                inven.Add(item);
            }
            reader.Close();
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
        int actionPoint = 0;
        //==
        public event UseSkillCallback? isAttack;
        private List<Item>? inven = new List<Item>();
        private Quest quest = new Quest();

    }
}
