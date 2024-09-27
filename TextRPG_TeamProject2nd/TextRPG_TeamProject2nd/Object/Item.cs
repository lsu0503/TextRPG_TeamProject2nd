using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Object
{
    enum ITEMTYPE
    {
        WEAPON,
        ARMOR,
        CONSUMABLE
    }


    internal class Item
    {

        void Attack() { }
        void Defence() { }
        void Drink() { }

        public int id           { get; set; }
        public int damage       { get; set; }
        public int defence      { get; set; }
        public int actionPoint  { get; set; }
        public string name      { get; set; }
        public string skill     { get; set; }
        public ITEMTYPE type    { get; set; }
    }
}
