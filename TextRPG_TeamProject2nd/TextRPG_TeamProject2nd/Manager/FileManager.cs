using TextRPG_TeamProject2nd.Object;

namespace TextRPG_TeamProject2nd.Manager
{
    internal class FileManager
    {
        void Make()
        {0,10,0,10,"몽둥이",{},0
            Item item = new Item();
            item.damage = 0;//불러온거;
            //
            item.id = 0;
            items.Add(item);

        }

        public List<Item> GetitemList()
        {
            return items;
        }

        List<Item> items = new List<Item>();
    }
}
