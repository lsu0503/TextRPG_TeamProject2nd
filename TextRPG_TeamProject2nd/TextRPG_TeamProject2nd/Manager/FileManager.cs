using System.Text;
using TextRPG_TeamProject2nd.Object;
using TextRPG_TeamProject2nd.Utils;

namespace TextRPG_TeamProject2nd.Manager
{
    enum FILETYPE
    {
        NONE,
        ITEM,
        MAP,
        MONSTER,
        RACE,
        SKILL,        
    }
    internal class FileManager
    {
        static public FileManager Instance()
        {
            if(instance == null)
                instance = new FileManager();

            return instance;
        }
        public void Init()
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            GetFileToList("Item.csv", FILETYPE.ITEM);
            GetFileToList("Map.csv", FILETYPE.MAP);
            GetFileToList("Monster.csv", FILETYPE.MONSTER);
            GetFileToList("Race.csv", FILETYPE.RACE);
            GetFileToList("Skill.csv", FILETYPE.SKILL);        
        }

        public void SavePlayer()
        {
            StreamWriter streamWriter = new StreamWriter(path + "\\SAVE.spam");
            Player player = GameManager.Instance().GetCurrentPlayer();
            if (player == null)
                return;

            PlayerInfo info = player.GetInfo();
            string outData = string.Empty;
            {
                outData += info.name + ",";
                outData += info.weaponId + ",";
                outData += info.armorId + ",";
                outData += info.level + ",";
                outData += info.hp + ",";
                outData += info.maxHp + ",";
                outData += info.attack + ",";
                outData += info.defence + ",";
                outData += info.actionPoint + ",";
            }
          
            streamWriter.WriteLine(outData);
            streamWriter.Close();
        }
        public bool LoadPlayer()
        {
            string fullPath = path + "\\SAVE.spam";
            if(!File.Exists(fullPath))
            {
                return false;
            }

            StreamReader streamReader = new StreamReader(fullPath);
            string str = streamReader.ReadToEnd();
            string[] inData = str.Split(",");
            
            Player player = GameManager.Instance().GetCurrentPlayer();
            player.GetInfo().name = inData[0];
            player.GetInfo().weaponId = int.Parse(inData[1]);
            player.GetInfo().armorId = int.Parse(inData[2]);
            player.GetInfo().level = int.Parse(inData[3]);
            player.GetInfo().hp = int.Parse(inData[4]);
            player.GetInfo().maxHp = int.Parse(inData[5]);
            player.GetInfo().attack = int.Parse(inData[6]);
            player.GetInfo().defence = int.Parse(inData[7]);
            player.GetInfo().actionPoint = int.Parse(inData[8]);
            return true;      
        }
        void GetFileToList(string _fileName, FILETYPE _type)
        {
            StreamReader reader = new StreamReader(Path.Combine(path, _fileName));
            bool isFirst = true;
            type = _type;
            string line = string.Empty;
            while((line = reader.ReadLine()) != null)
            {
                if (isFirst)
                {
                    isFirst = false; 
                    continue;
                }
                    //--
                    string[] temp = line.Split(',');
                switch (type)
                {            
                    case FILETYPE.ITEM:
                        {
                            Item item    = new Item();
                            item.id      = int.Parse(temp[0]);
                            item.attack  = int.Parse(temp[1]);
                            item.defence = int.Parse(temp[2]);
                            item.value   = int.Parse(temp[3]);
                            item.amount  = int.Parse(temp[4]);
                            item.desc    = temp[5];
                            item.name    = temp[6];
                            item.skill   = StringToIndex(temp[7]);
                            item.type    = (ITEMTYPE)int.Parse(temp[8]);
                            //TODO
                            items.Add(item);
                        }
                        break;
                    case FILETYPE.MAP:
                        {
                            Map map = new Map();
                            map.mapInfo.id = int.Parse(temp[0]);
                            map.mapInfo.levelLimit = int.Parse(temp[1]);
                            map.mapInfo.floor = int.Parse(temp[2]);
                            map.mapInfo.mobId = StringToIndex(temp[3]);
                            map.mapInfo.mobId = StringToIndex(temp[4]);
                            map.mapInfo.name = temp[5];
                            map.mapInfo.desc = temp[6];
                            //TODO
                            maps.Add(map);
                        }
                        break;
                    case FILETYPE.MONSTER:
                        {
                            Monster monster = new Monster();
                            monster.GetInfo().id = int.Parse(temp[0]);
                            monster.GetInfo().name = temp[1];
                            monster.GetInfo().skillList = StringToIndex(temp[2]);
                            monster.GetInfo().dropList = StringToIndex(temp[3]);
                            monster.GetInfo().level = int.Parse(temp[4]);
                            monster.GetInfo().hp = int.Parse(temp[5]);
                            monster.GetInfo().maxHp = int.Parse(temp[6]);
                            monster.GetInfo().attack = int.Parse(temp[7]);
                            monster.GetInfo().defence = int.Parse(temp[8]);
                            monsters.Add(monster);
                        }
                        break;
                    case FILETYPE.RACE:
                        {
                            Race race = new Race();
                            race.id = int.Parse(temp[0]);
                            race.hp = int.Parse(temp[1]);
                            race.attack = int.Parse(temp[2]);
                            race.defence = int.Parse(temp[3]);
                            race.actionPoint = int.Parse(temp[4]);
                            race.name = temp[5];
                            race.desc = temp[6];
                            //TODO
                            races.Add(race);
                        }
                        break;
                    case FILETYPE.SKILL:
                        {
                            Skill skill = new Skill();
                            skill.id = int.Parse(temp[0]);
                            skill.type = (SKILLTYPE)int.Parse(temp[1]);
                            skill.name = temp[2];
                            skill.desc = temp[3];
                            skill.power = int.Parse(temp[4]);
                            skill.cost = int.Parse(temp[5]);
                            Skills.Add(skill);
                        }
                        break;
                }
            }

            reader.Close();
        }//CORE //파일매니저 핵심 기능 함수입니다.
        List<int> StringToIndex(string str)
        {
            string[] strings = str.Split('|');
            List<int> ret = new List<int>();
            foreach (string s in strings)
                ret.Add(int.Parse(s));

            return ret;

        }

        public List<Item>      GetItemFile()    {return items;}
        public List<Map>       GetMapFile()     {return maps;}
        public List<Monster>   GetMonsterFile() {return monsters;}
        public List<Race>      GetRaceFile()    {return races;}
        public List<Skill>     GetSkillFile()   {return Skills;}

        string path = Path.Combine(Directory.GetCurrentDirectory(), "Team25"); //기본 경로
        List<Item>      items       = new List<Item>();
        List<Map>       maps        = new List<Map>();
        List<Monster>   monsters    = new List<Monster>();
        List<Race>      races       = new List<Race>();
        List<Skill>     Skills      = new List<Skill>();

        FILETYPE type = FILETYPE.NONE;
        static FileManager? instance;

        //-----------------------------------------------------------------------------// 나영님//
        /*public static List<Item> LoadWeaponsFromCSV(string filePath = "weapons.csv")
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
                        //actionPoint = int.Parse(values[3]),
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
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(Console.OutputEncoding.EncodingName);

            foreach (Item item in items)
            {
                Console.Write($"{item.id}");
                Console.Write($"\t{item.attack}");
                Console.Write($"\t{item.defence}");
                //Console.Write($"\t{item.actionPoint}");
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
        }*/
    }
}
