using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_TeamProject2nd.Object
{
    class Race
    {
        public string? Name     { get; set; }
        public string? Desc     { get; set; }
        public int level        { get; set; }
        public int hp           { get; set; }
        public int maxHp        { get; set; }
        public int attack       { get; set; }
        public int defence      { get; set; }
        public int actionPoint  { get; set; }
    }
}
