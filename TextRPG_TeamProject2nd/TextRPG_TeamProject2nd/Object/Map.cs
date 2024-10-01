using TextRPG_TeamProject2nd.Utils;
using TextRPG_TeamProject2nd.Manager;

namespace TextRPG_TeamProject2nd.Object
{
    internal class Map: IClone<Map>
    {
        public Map Clone()
        {
            Map ret = new Map();
            ret.mapInfo = mapInfo;
            return ret;
        }
        
        public MapInfo mapInfo = new MapInfo();
    }
}
