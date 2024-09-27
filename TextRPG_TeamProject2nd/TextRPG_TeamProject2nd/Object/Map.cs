using TextRPG_TeamProject2nd.Utils;

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

        public int id                    { get; set; }
        public string name               { get; set; }
        public string desc               { get; set; }
        public int level                 { get; set; }
        public List<Monster> monsterList { get; set; } = new List<Monster>();
        public List<Monster> bossList    { get; set; } = new List<Monster>();
        public List<int> floorBonfire    { get; set; } = new List<int>();
        public int floorMax              { get; set; }
    }
}
