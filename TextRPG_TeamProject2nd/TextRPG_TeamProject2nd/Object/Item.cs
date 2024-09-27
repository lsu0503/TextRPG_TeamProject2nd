using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{
    enum ITEMTYPE
    {
        WEAPON,
        ARMOR,
        CONSUMABLE
    }


    internal class Item: IObject, IClone<Item>
    {

        /// <summary>
        /// 프로토타입 기능입니다, 해당 객체를 복사하여 완전하게 동일한 새로운 객체를 생성합니다.
        /// </summary>
        /// <returns></returns>
        public Item Clone()
        {
            Item ret= new Item();
            ret.id          = id;
            ret.attack      = attack;
            ret.defence     = defence;
            ret.actionPoint = actionPoint;
            ret.name        = name;
            ret.skill       = skill;
            ret.type        = type;

            return ret;
        }

        public int id           { get; set; }
        public int attack       { get; set; }
        public int defence      { get; set; }
        public int actionPoint  { get; set; }
        public int value        { get; set; }
        public int amount       { get; set; }
        public string desc      { get; set; }
        public string name      { get; set; }
        public List<int> skill  { get; set; }
        public ITEMTYPE type    { get; set; }
    }
}
