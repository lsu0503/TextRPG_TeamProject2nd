using TextRPG_TeamProject2nd.Utils;
using TextRPG_TeamProject2nd.Manager;

namespace TextRPG_TeamProject2nd.Object
{
    internal class Map: IObject, IClone<Map>
    {
        public Map Clone()
        {
            Map ret = new Map();

            ret.id           = id;
            ret.name         = name;
            ret.desc         = desc;
            ret.level        = level;
            ret.monsterList  = monsterList.ToList();
            ret.bossList     = bossList.ToList();
            ret.floorBonfire = floorBonfire.ToList();
            ret.floorMax     = floorMax;

            return ret;
        }

        public Monster[] HauntMonster(int _amount)
        {
            Random rand = new Random();
            Monster[] monstersHaounted = new Monster[_amount];

            for(int i = 0; i < _amount; i++)
            {
                monstersHaounted[i] = ObjectManager.Instance().GetMonster(rand.Next(0, monsterList.Count));
            }

            return monstersHaounted;
        }

        public Monster[] HauntBoss(int _minionAmount)
        {
            Random rand = new Random();
            Monster[] monstersHaounted = new Monster[_minionAmount + 1];

            monstersHaounted[0] = ObjectManager.Instance().GetMonster(bossList[rand.Next(0, bossList.Count)]);

            for (int i = 1; i <= _minionAmount; i++)
            {
                monstersHaounted[i] = ObjectManager.Instance().GetMonster(monsterList[rand.Next(0, monsterList.Count)]);
            }

            return monstersHaounted;
        }

        public bool CheckBonfire(int floor)
        {
            if (floorBonfire.Exists(index => index == floor))
                return true;

            else
                return false;
        }

        public int id                    { get; set; }
        public string name               { get; set; }
        public string desc               { get; set; }
        public int level                 { get; set; }
        public List<int> monsterList { get; set; } = new List<int>();
        public List<int> bossList    { get; set; } = new List<int>();
        public List<int> floorBonfire    { get; set; } = new List<int>();
        public int floorMax              { get; set; }
    }
}
