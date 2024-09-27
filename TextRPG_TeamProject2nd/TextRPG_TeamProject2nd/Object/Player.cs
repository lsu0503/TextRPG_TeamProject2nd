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

            InitializeCharacter();
            UpdateEquipment();
        }

        
        public void InitializeCharacter()
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
            attack = race.attack;
            defence = race.defence;
            actionPoint = race.actionPoint;

            if (weapon != null)
            {
                attack += weapon.attack;
                defence += weapon.defence;
                actionPoint += weapon.actionPoint;
            }

            if (armor != null)
            {
                attack += armor.attack;
                defence += armor.defence;
                actionPoint += armor.actionPoint;
            }

            skillList.Clear();
            for (int i = 0; i < skillList.Count; i++)
                skillList.Add(ObjectManager.Instance().GetSkill(weapon.skill[i]));
        }

        public void RewardExp(int _amount)
        {
            expCur += _amount;

            if (expCur >= expMax)
            {
                expCur -= expMax;
                level++;
                expMax = (int)(100 * MathF.Pow(2.5f, level) - 50 * MathF.Pow(level, 2)); // 임시 지정.

                InitializeCharacter();
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

        string name            { get; set; }
        Race race              { get; set; }
        int hpCur              { get; set; }
        int hpMax              { get; set; }
        bool isDead            { get; set; }
        int attack             { get; set; }
        int defence            { get; set; }
        int actionPoint        { get; set; }
        int apCur              { get; set; }
        int bpCur              { get; set; }
        int bpMin              { get; set; }
        int level              { get; set; }
        int expCur             { get; set; }
        int expMax             { get; set; }
        int[] statRank         { get; set; } = new int[5];
        List<Skill> skillList  { get; set; } = new List<Skill>();
        Item weapon            { get; set; }
        Item armor             { get; set; }
    }
}
