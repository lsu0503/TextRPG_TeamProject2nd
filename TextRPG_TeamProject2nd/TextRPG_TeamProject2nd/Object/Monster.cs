﻿using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Utils;


namespace TextRPG_TeamProject2nd.Object
{
    internal class Monster: IClone<Monster>
    {

        public void Init()
        {
            Player player = GameManager.Instance().GetCurrentPlayer();
            isAttack += player.Damaged;

            foreach (int id in mobInfo.skillList)
                skillList.Add(ObjectManager.Instance().GetSkill(id));
        }

        public int UseSkill(Player player)
        {
            if (mobInfo == null || skillList == null)
                return 0;

            Random random = new Random();
            int range = mobInfo.skillList.Count;
            int index = random.Next(0, range);

            Skill skill = skillList[index];
            if(skill.type == SKILLTYPE.ATTACK)
            {
                int damage = (skillList[index].power * mobInfo.attack) / player.GetInfo().defence;
                isAttack?.Invoke(damage);
                return damage;
            }
            else
            {
                mobInfo.hp += skill.power;
                return skill.power;
            }
                
        }


        public void Damaged(int damage)
        {
            if (mobInfo == null)
                return;

            mobInfo.hp = Math.Max(0, mobInfo.hp - damage);
        }

        public Item Drops()
        {
            Random rand = new Random();

            int id = mobInfo.dropList[rand.Next(0, mobInfo.dropList.Count)];
            if (id == -1) return new Item { id = -1 };
            return ObjectManager.Instance().GetItem(id);

        }

        public Monster Clone()
        {
            Monster ret = new Monster();
            ret.mobInfo.name = mobInfo.name;
            ret.mobInfo.skillList = mobInfo.skillList;
            ret.mobInfo.dropList = mobInfo.dropList;

            ret.mobInfo.id = mobInfo.id;
            ret.mobInfo.level = mobInfo.level;
            ret.mobInfo.hp = mobInfo.hp;
            ret.mobInfo.maxHp = mobInfo.maxHp;
            ret.mobInfo.attack = mobInfo.attack;
            ret.mobInfo.defence = mobInfo.defence;
            
            return ret;
        }

        public MobInfo GetInfo()
        {
            return mobInfo;
        }
        //--------------------------------------
       

       
        //--------------------------------------
        private MobInfo? mobInfo = new MobInfo();
        private List<Skill>? skillList = new List<Skill>();
        public event UseSkillCallback? isAttack;
    }
}
