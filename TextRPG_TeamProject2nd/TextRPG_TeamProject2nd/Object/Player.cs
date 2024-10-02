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
        public void UseSkill(int index, Monster mob = null)
        {
            isAttack += mob.Damaged;
            SKILLTYPE type = skillList[index].type;
            
            if (playerInfo != null && (type == SKILLTYPE.ATTACK))
            {
                isAttack?.Invoke(playerInfo.attack * skillList[index].power); //attack
            }
            else if (playerInfo != null && type == SKILLTYPE.HEAL)
            {
                playerInfo.hp = Math.Min(playerInfo.maxHp, playerInfo.hp + skillList[index].power);
            }
        }
        public void Damaged(int damage)
        {
            if (playerInfo != null)
                playerInfo.hp = Math.Max(0, playerInfo.hp - damage);
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

        public void EQItem(Item _item)
        {
            if (inven == null || _item == null) return;

            switch (_item.type)
            {
                case ITEMTYPE.WEAPON:
                    if (weapon != null) inven.Add(weapon);
                    weapon = _item;
                    break;
                case ITEMTYPE.ARMOR:
                    if (armor != null) inven.Add(armor);
                    armor = _item;
                    break;
            }

            if (playerInfo != null)
            {
                playerInfo.attack += _item.attack;
                playerInfo.defence += _item.defence;
            }

            if(_item.skill.Count != 0 || skillList != null)
            {
                skillList.Clear();
                foreach (int id in _item.skill)
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
         
        }

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
            quest = ObjectManager.Instance().GetQuest(playerInfo.questId);

            quest.questProgressAmount = playerInfo.questProgress;
        }

        public Quest GetCurrentQuest()
        {
            return quest;
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
        Item? weapon = null;
        Item? armor = null;
        Quest? currentQuestId = null;
        Quest? currentQuestProgressAmount = null;

        int baseMaxExp = 100;

        //==
        public event UseSkillCallback? isAttack;
        private List<Item>? inven = new List<Item>();
        private Quest quest = new Quest();

    }
}
