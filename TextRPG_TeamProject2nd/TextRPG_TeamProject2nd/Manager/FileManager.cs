using TextRPG_TeamProject2nd.Object;

namespace TextRPG_TeamProject2nd.Manager
{
    internal class FileManager
    {
        public static List<Item> LoadWeaponsFromCSV(string filePath = "weapons.csv")
        {
            List<Item> items = new List<Item>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine) // 첫 번째 라인(헤더) 읽지 않도록
                    {
                        isFirstLine = false;
                        continue;
                    }

                    string[] values = line.Split(',');

                    if (values.Length == 9)
                    {
                        Item item = new Item
                        {
                            id = int.Parse(values[0]),
                            damage = int.Parse(values[1]),
                            defence = int.Parse(values[2]),
                            actionPoint = int.Parse(values[3]),
                            value = int.Parse(values[4]),
                            amount = int.Parse(values[5]),
                            desc = values[6],
                            name = values[7],
                            skill = new List<int> { },
                            type = ITEMTYPE.WEAPON

                        };
                        items.Add(item);
                    }
                }
            }
            return items;
        }
                
    }
}
