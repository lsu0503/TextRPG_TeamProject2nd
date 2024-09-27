using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{
    enum SKILLTYPE
    {
        ATTACK,
        ASSIST,
        HEAL
    }

    internal class Skill: IObject, IClone<Skill>
    {
        public Skill Clone()
        {
            Skill ret = new Skill();

            ret.id               = id;
            ret.type             = type;
            ret.name             = name;
            ret.desc             = desc;

            ret.power            = power;
            ret.RankChangeTarget = RankChangeTarget.ToArray();
            ret.RankChangeSelf   = RankChangeSelf.ToArray();

            return ret;
        }


        public int id                 { get; set; }
        public SKILLTYPE type         { get; set; }
        public string name            { get; set; }
        public string desc            { get; set; }
        
        public int power              { get; set; }
        public int[] RankChangeTarget { get; set; } = new int[5];
        public int[] RankChangeSelf   { get; set; } = new int[5];
    }
}
