using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Utils;


namespace TextRPG_TeamProject2nd.Object
{
    internal class Monster: IObject, IClone<Monster>
    {
        public Monster Clone()
        {
            Monster ret = new Monster();

            ret.id              = id;
            ret.name            = name;
            ret.desc            = desc;
            ret.level           = level;
            ret.hpMax           = hpMax;
            ret.probabilityDrop = probabilityDrop;
            ret.listDropItem    = listDropItem.ToList();
            ret.bpMin           = bpMin;
            ret.rewardExp       = rewardExp;
            ret.rewardMoney     = rewardMoney;
            ret.skillPool       = skillPool;

            return ret;
        }

        public void InitializeMonster()
        {
            hpCur = hpMax;
            isDead = false;

            apCur = actionPoint;
            bpCur = bpMin;

            statRank = new int[] { 0, 0, 0, 0, 0 };

            skillList.Clear();
            Random rand = new Random();
            for(int i = 0; i < 4; i++)
            {
                while(true)
                {
                    Skill tempSkill = ObjectManager.Instance().GetSkill(skillPool[rand.Next(0, skillPool.Count)]);

                    if (skillList.Any(index => index.id == tempSkill.id))
                    {
                        skillList.Add(tempSkill);
                        break;
                    }
                }
            }
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

        public int DropItem()
        {
            Random rand = new Random();

            if (rand.NextDouble() <= probabilityDrop)
                return rand.Next(0, listDropItem.Count);

            else
                return -1;
        }

        public int id                 { get; set; }
        public string name            { get; set; }
        public string desc            { get; set; }
        public int level              { get; set; }
        public int hpCur              { get; set; }
        public int hpMax              { get; set; }
        public bool isDead            { get; set; }
        public int attack             { get; set; }
        public int defence            { get; set; }
        public int actionPoint        { get; set; }
        public int apCur              { get; set; }
        public int bpCur              { get; set; }
        public int bpMin              { get; set; }
        public int[] statRank         { get; set; } = new int[5];
        public List<Skill> skillList  { get; set; } = new List<Skill>();
        public float probabilityDrop  { get; set; }
        public List<int> listDropItem { get; set; } = new List<int>();
        public int rewardExp          { get; set; }
        public int rewardMoney        { get; set; }
        public List<int> skillPool    { get; set; } = new List<int>();
    }
}
