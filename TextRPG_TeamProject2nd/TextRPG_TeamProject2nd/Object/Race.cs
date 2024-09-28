using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamProject2nd.Object
{
    class Race
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
}
