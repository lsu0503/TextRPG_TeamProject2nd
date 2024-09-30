using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{
    enum SKILLTYPE
    {
        ATTACK,
        ASSIST,
        HEAL
    }

    internal class Skill: IClone<Skill>
    {
        public Skill Clone()
        {
            Skill ret = new Skill();

            ret.id               = id;
            ret.type             = type;
            ret.name             = name;
            ret.desc             = desc;
            ret.power            = power;
            return ret;
        }

        public int id                 { get; set; }
        public SKILLTYPE type         { get; set; }
        public string name            { get; set; }
        public string desc            { get; set; } 
        public int power              { get; set; }
        public int cost               { get; set; }
       
    }
}
