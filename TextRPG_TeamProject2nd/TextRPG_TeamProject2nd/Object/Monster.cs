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
