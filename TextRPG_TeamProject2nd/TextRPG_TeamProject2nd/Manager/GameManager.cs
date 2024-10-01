using TextRPG_TeamProject2nd.Object;
using TextRPG_TeamProject2nd.Utils;
namespace TextRPG_TeamProject2nd.Manager
{
    enum SCENESTATE
    {
        NONE,
        MAIN,
        VILLAGE,
        FILED,
        STORE,
        INVEN,
        QUEST,
        END,
    }
    internal class GameManager
    {
        
        //---------------------------------------------------------------
        public static GameManager Instance()
        {
            if(instance == null)
                instance = new GameManager();

            return instance;
        }
        public void Init()
        {
            if(!FileManager.Instance().LoadPlayer())
            {
                ChangeScene(SCENESTATE.NONE);
            }

            player = new Player();
            storeList = new List<Item>();
            InitStore();
        }
        //---------------------------------------------------------------
        public void Update()
        {
            switch (sceneState)
            {
                case SCENESTATE.NONE:
                    SceneNone();
                    break;
                case SCENESTATE.MAIN:
                    SceneMain();
                    break;
                case SCENESTATE.VILLAGE:
                    SceneVIllage();
                    break;
                case SCENESTATE.FILED:
                    SceneFiled();
                    break;
                case SCENESTATE.STORE:
                    SceneStore();
                    break;
                case SCENESTATE.INVEN:
                    SceneStore();
                    break;
                case SCENESTATE.QUEST:
                    SceneOuest();
                    break;
                case SCENESTATE.END:
                    //SAVE()
                    //TODO
                    break;
            }
        }
        //---------------------------------------------------------------

        //맵
        private void SceneNone()
        {
            bool isComplete = false;
            Player player = new Player();
            int race = 0;
            while(!isComplete)
            {
                Console.WriteLine("[캐릭터를 생성합니다]");
                Console.Write("이름을 입력하세요 : ");
                string name = InputLine();
                Console.Clear();
                Console.WriteLine("[직업을 선택하세요]");
                //List<Race> races = FileManager.GetRace();
                Console.WriteLine("1.더미");//UI
                Console.WriteLine("2.더미");
                Console.WriteLine("3.더미");
                race = (InputKey() - 1);

                Console.WriteLine($"이름:{name} 직업:{race}");
                Console.WriteLine("[0]:확정 [1]:다시 작성");
                if(InputKey() == 0) isComplete = true;
            }

            player.SetPlayer(race);
            player.Save();
            ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneMain()
        {
            Console.Clear();
            Console.WriteLine("----------게임제목-----------");
            Console.WriteLine("[0]:계속하기 [1]:종료");
            int input = InputKey();
            if(input == 1) Environment.Exit(0);

            if (player == null) { Console.WriteLine("로드오류"); return; }
            player.Load();

            ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneVIllage()
        {
            Console.Clear();
            Console.WriteLine("마을입니다.");
            Console.WriteLine("[0]:상점 [1]:퀘스트 [2]:보관함 [3]:던 전 [4]:저장 및 종료");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.STORE);
            else if (input == 1) ChangeScene(SCENESTATE.QUEST);
            else if (input == 2) ChangeScene(SCENESTATE.STORE);
            else if (input == 3) ChangeScene(SCENESTATE.FILED);
            else if (input == 4) { player.Save(); Environment.Exit(0); }
        }
        private void SceneFiled()
        {
            Console.Clear();
            Console.WriteLine("던전을 고르세요!");
            Console.WriteLine("[0]:마을로이동 [번호]:해당 던전");
            int input = InputKey();
            int maxId = 0; // TODO
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
            for(int i = 0; i < maxId; i++)
            {
                Console.WriteLine($"{i}." + ObjectManager.Instance().GetMap(i).mapInfo.name);
            }

            Random random = new Random();
            Map map = ObjectManager.Instance().GetMap(input);
            List<int> ids = map.mapInfo.mobId;
            List<Monster> currentMobs = new List<Monster>(); 

            int seed    = random.Next(0, ids.Count);
            int count   = random.Next(3, 6);
            int floor   = 0;
            for (int i = 0; i < count; i++)
                currentMobs.Add(ObjectManager.Instance().GetMonster(ids[seed]));

            while(true)
            {
                
                break;
            }
        }
        private void SceneStore()
        {
            Console.Clear();
            Console.WriteLine("상점 공간입니다.");
            Console.WriteLine("[0]:마을로이동");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneInven()
        {
            Console.Clear();
            Console.WriteLine("아이템을 보관하고 장착하는 인벤토리 입니다.");
            Console.WriteLine("[0]:마을로이동");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneOuest()
        {
            Console.Clear();
            Console.WriteLine("이곳에 퀘스트UI와 정보가 등록됩니다.");
            Console.WriteLine("[0]:마을로이동");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
        }

        //유틸
        private void ChangeScene(SCENESTATE type)
        {
            sceneState = type;
        }
        private void InitStore()
        {
            for(int i = 1000; i < 1500; i++)
            {
               storeList.Add(ObjectManager.Instance().GetItem(i));
            }
        }
        private bool BuyItem(int index)
        {
            if (player == null || storeList == null) return false;

            int money = player.GetInfo().money;
            int price = storeList[index].value;
            if (money < price) return false;
            
            player.PushInven(storeList[index]);
            storeList.RemoveAt(index);
            return true;
        }
        private bool Sellitem(int index)
        {
            if (player == null || storeList == null) return false;

            Item item = player.GetPlayerInvenList()[index];
            if (item == null) return false;

            player.GetInfo().money += (item.value / 2);
            player.GetPlayerInvenList().RemoveAt(index);
            return true;
        }
     
        public Player GetCurrentPlayer() { return player; }
        public List<Item> GetStoreList() { return storeList; }
        
        //사용자 입력
        public int InputKey()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">>");
            Console.ResetColor();
            int input = -1;
            
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">>");
                Console.ResetColor();
            }
            
            return input;
        }
        private string InputLine()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">>");
            Console.ResetColor();
            string input = Console.ReadLine() ?? "";

            return input;
        }

        //---------------------------------------------------------------
        static GameManager? instance;
        private Player? player = null;
        private SCENESTATE sceneState = SCENESTATE.MAIN;
        private List<Item>? storeList = null;

    }
}
