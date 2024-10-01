using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamProject2nd.Utils
{
    class PlayerInfo
    {
        public string? name    { get; set; }
        public int weaponId    { get; set; }
        public int armorId     { get; set; }
        public int level       { get; set; }
        public int hp          { get; set; }
        public int maxHp       { get; set; }
        public int attack      { get; set; }
        public int defence     { get; set; }
        public int actionPoint { get; set; }
        public int exp         { get; set; }
        public int maxExp      { get; set; }
        public int money       { get; set; }

    }

    class MobInfo
    {
        public string? name         { get; set; }
        public List<int>? skillList  { get; set; }
        public List<int>? dropList   { get; set; }

        public int id       { get; set; }
        public int level    { get; set; }
        public int hp       { get; set; }
        public int maxHp    { get; set; }
        public int attack   { get; set; }
        public int defence  { get; set; }
    }

    class MapInfo
    {
        public int id               { get; set; }
        public int levelLimit       { get; set; }
        public int floor            { get; set; }
        public List<int>? mobId     { get; set; }
        public List<int>? bossId    { get; set; }
        public string? name         { get; set; }
        public string? desc         { get; set; }
    }



}
