using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamProject2nd.Utils
{
    class PlayerInfo
    {
        public string? name        { get; set; }
        public string? weaponId    { get; set; }
        public string? armorId     { get; set; }
       
        public int level       { get; set; }
        public int hp          { get; set; }
        public int maxHp       { get; set; }
        public int attack      { get; set; }
        public int defence     { get; set; }
        public int actionPoint { get; set; }
    }

    class MobInfo
    {
        public string? name         { get; set; }
        public string[]? skillList  { get; set; }
        public string[]? dropList   { get; set; }

        public int level    { get; set; }
        public int hp       { get; set; }
        public int maxHp    { get; set; }
        public int attack   { get; set; }
        public int defence  { get; set; }
    }

    class MapInfo
    {
        public string? name     { get; set; }
        public string? desc     { get; set; }
        public string[]? mobId  { get; set; }     
        public int levelLimit   { get; set; }
    }



}
