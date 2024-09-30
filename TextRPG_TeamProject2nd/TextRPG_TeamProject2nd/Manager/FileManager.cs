using System.Text;
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

                    Item item = new Item
                    {
                        id = int.Parse(values[0]),
                        attack = int.Parse(values[1]),
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
            return items;
        }

        public static List<Skill> LoadSkillsFromCSV(string filePath = "skills.csv")
        {
            List<Skill> skills = new List<Skill>();

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

                    Skill skill = new Skill()
                    {
                        id = int.Parse(values[0]),
                        type = SKILLTYPE.HEAL,
                        name = values[2],
                        desc = values[3],
                        power = int.Parse(values[4]),
                    };
                    skills.Add(skill);
                }
            }
            return skills;
        }

        public static void PrintItems(List<Item> items)
        {
            foreach (Item item in items)
            {
                Console.Write($"{item.id}");
                Console.Write($"\t{item.attack}");
                Console.Write($"\t{item.defence}");
                Console.Write($"\t{item.actionPoint}");
                Console.Write($"\t{item.value}");
                Console.Write($"\t{item.amount}");
                Console.Write($"\t{item.desc}");
                Console.Write($"\t{item.name}");
                Console.WriteLine($"\t{item.skill}");
            }
        }

        public static void PrintSkills(List<Skill> skills)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(Console.OutputEncoding.EncodingName);
            foreach (Skill skill in skills)
            {
                Console.Write($"{skill.id}");
                Console.Write($"\t{skill.name}");
                Console.Write($"\t{skill.desc}");
                Console.WriteLine($"\t{skill.power}");
            }
        }
    }
}
