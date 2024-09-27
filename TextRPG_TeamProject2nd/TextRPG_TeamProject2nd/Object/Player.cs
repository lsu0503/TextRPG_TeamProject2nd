using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TextRPG_TeamProject2nd.Manager;

namespace TextRPG_TeamProject2nd.Object
{
    struct Race
    {
        public Race(int _id, string _name, string _desc, int _hp, int _attack, int _defence, int _actionPoint)
        {
            id = _id;
            name = _name;
            desc = _desc;

            hp = _hp;

            attack = _attack;
            defence = _defence;
            actionPoint = _actionPoint;
        }

        public int id;
        public string name;
        public string desc;

        public int hp;

        public int attack;
        public int defence;
        public int actionPoint;
    }

    internal class Player
    {
        // 생성자
        public Player(string _name, Race _race)
        {
            name = _name;
            race = _race;

            level = 0;
            expCur = 0;

            weapon = null;
            armor = null;

            InisializeCharacter();
            UpdateEquipment();
        }

        
        public void InisializeCharacter()
        {
            hpMax = race.hp;
            hpCur = hpMax;
            isDead = false;

            apCur = 0;
            bpCur = 0;
            bpMin = 0;

            statRank = new int[] { 0, 0, 0, 0, 0 };

            UpdateEquipment();
        }

        public int TakeDamage(int _damage)
        {
            float tempDamageFloat = _damage / defence;

            int tempDamageInt = (int)tempDamageFloat;

            if (tempDamageFloat % 1.0f > 0.5f) // 반올림
                tempDamageInt = (int)tempDamageFloat++;

            hpCur -= tempDamageInt;

            if (hpCur <= 0)
            {
                hpCur = 0;
                isDead = true;
            }

            return tempDamageInt;
        }

        public int TakeRecover(int _amount)
        {
            int healAmount = (int)MathF.Min(_amount, hpMax - hpCur);

            hpCur += healAmount;

            return healAmount;
        }

        public void OnNextTurn()
        {
            if (apCur > 0)
                bpCur = apCur + bpMin;

            apCur = actionPoint + (int)MathF.Min(apCur, 0);
        }

        public float CalculateRank(int _type)
        {
            return MathF.Max(1.0f, 1.0f + (0.25f * statRank[_type])) / MathF.Max(1.0f, 1.0f - (0.25f * statRank[_type])); ;
        }

        
        public void UpdateEquipment()
        {
            attack = race.attack + weapon.damage;
            defence = race.defence + weapon.defence;
            actionPoint = race.actionPoint + weapon.actionPoint;

            skillList = ObjectManager.Instance().Get
        }

        public void RewardExp(int _amount)
        {
            expCur += _amount;

            if (expCur >= expMax)
            {
                expCur -= expMax;
                level++;
                expMax = (int)(100 * MathF.Pow(2.5f, level) - 50 * MathF.Pow(level, 2)); // 임시 지정.

                InisializeCharacter();
            }
        }

        public void EquipEquipment(Item _item)
        {
            switch (_item.type)
            {
                case ITEMTYPE.WEAPON:
                    weapon = _item;
                    break;

                case ITEMTYPE.ARMOR:
                    armor = _item;
                    break;
            }
            
            UpdateEquipment();
        }

        string name;

        Race race;

        int hpCur;
        int hpMax;
        bool isDead;

        int attack;
        int defence;
        int actionPoint;

        int apCur;
        int bpCur;
        int bpMin;

        int level;
        int expCur;
        int expMax;

        int[] statRank = new int[5];

        List<Skill> skillList = new List<Skill>();

        Item weapon;
        Item armor;
    }
}
