using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{
    class Race : IClone<Race>
    {
        public Race Clone()
        {
            Race race = new Race();
            race.id = id;
            race.hp = hp;
            race.attack = attack;
            race.defence = defence;
            race.actionPoint = actionPoint;
            race.name = name;
            race.desc = desc;
            return race;
        }

        public int id           { get; set; }
        public int hp           { get; set; }
        public int attack       { get; set; }
        public int defence      { get; set; }
        public int actionPoint  { get; set; }
        public string? name     { get; set; }
        public string? desc     { get; set; }
    }
}
