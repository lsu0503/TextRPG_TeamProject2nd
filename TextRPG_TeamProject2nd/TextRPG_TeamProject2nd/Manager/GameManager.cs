using TeamProjectBin;
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
            storeList = new List<Item>();
            InitStore();
            player = new Player();
            UIManager = UIManager.Instance();
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
                    SceneInven();
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
            int raceid = 0;
            string name = string.Empty;
            while(!isComplete)
            {
                Console.Clear();
                UIManager.DisplayTitle("캐릭터 생성");
                UIManager.DisplayCreateChracter();
                UIManager.DisplayCreateCharacterName();
                UIManager.PositionCursorToInput();
                name = InputLine();
                
                Console.Clear();
                UIManager.DisplayTitle("캐릭터 생성");
                UIManager.DisplayCreateChracter();
                UIManager.DisplayCreateCharacterRace(0);
                UIManager.PositionCursorToInput();
                raceid = InputKey() - 1;
                Race temp = ObjectManager.Instance().GetRace(raceid);

                Console.Clear();
                UIManager.DisplayTitle("캐릭터 생성");
                UIManager.DisplayCreateChracter();
                Console.WriteLine();
                Console.WriteLine($"이름:{name} 종족:{temp.name}");
                Console.WriteLine("[0]:다시 작성 [1]:확정");
                UIManager.PositionCursorToInput();
                if (InputKey() == 1) isComplete = true;
            }

            
            player.SetPlayer(raceid, name);
            player.Save();
            ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneMain()
        {
            UIManager.DisplayTitle("Poetry of Travelers");
            UIManager.DisplayStartMenu();
            UIManager.PositionCursorToInput();
            int input = InputKey();
            if (input == 0) { Environment.Exit(0); }
            if (input == 1) { ChangeScene(SCENESTATE.NONE); return;}
            if (input == 2) { player.Load(); ChangeScene(SCENESTATE.VILLAGE); }
           
        }
        private void SceneVIllage()
        {
            Console.Clear();
            UIManager.DisplayTitle("애니멀리아 가도");
            UIManager.DisplayVillageMenu();
            UIManager.DisplayPlayerInfoVillage(player);
            UIManager.PositionCursorToInput();
            int input = InputKey();
            if (input == 1) ChangeScene(SCENESTATE.STORE);
            else if (input == 2) ChangeScene(SCENESTATE.INVEN);
            else if (input == 3) ChangeScene(SCENESTATE.QUEST);
            else if (input == 4) ChangeScene(SCENESTATE.FILED);
            else if (input == 0) { player.Save(); Environment.Exit(0); }
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
            List<int> MobNums = map.mapInfo.mobId;
            List<int> BossNums = map.mapInfo.bossId;
            int maxFloor = map.mapInfo.floor;
            int index = 0;
            List<Monster> currentMobs = new List<Monster>();
            
            for(int i = 0; i < maxFloor; i++)
            {
                if (i == maxFloor - 2)
                   currentMobs.Add(ObjectManager.Instance().GetMonster(BossNums[random.Next(0, BossNums.Count)]));               
               currentMobs.Add(ObjectManager.Instance().GetMonster(MobNums[random.Next(0, MobNums.Count)]));
            } //몬스터 세팅
            Monster mob = currentMobs[index];
            mob.Init();//몬스터는 꼭 초기화가 되어야 사용가능
         
            while(true)
            {
                isTurn = true;
                while (true) //플레이어
                {
                    int PlayerInput = InputKey();
                    //player.UseSkill()
                }

                isTurn = false;
                while (true) //몬스터
                {

                }
            }
        }
        private void SceneStore()
        {
            Console.Clear();
            UIManager.DisplayTitle("래비츠 인더스트리");
            UIManager.DisplayShopEntrance();
            UIManager.DisplayPlayerInfoVillage(player);
            UIManager.PositionCursorToInput();
            int signal = InputKey();
            if (signal == 0) { ChangeScene(SCENESTATE.VILLAGE); return; }
            
            while(true)
            {
                Console.Clear();
                int input = -1;
                if(signal == 1) //구매
                {
                    UIManager.DisplayTitle("래비츠 인더스트리");
                    UIManager.DisplayShopBuyList();
                    UIManager.DisplayPlayerInfoVillage(player);
                    UIManager.PositionCursorToInput();

                    int index = 1;

                    input = InputKey();
                    if (input == 0) break;
                    if (input > storeList.Count || input < 0) continue;

                    Item item = storeList[input - 1];
                    if (item == null) continue;
                    if (player.GetInfo().money - item.value < 0) continue;

                    player.GetInfo().money -= item.value;
                    player.PushInven(item);
                    
                    UIManager.DisplayShopBuyText(storeList[input - 1]);
                    Console.ReadKey();
                }
                else if(signal == 2) //판매
                {
                    UIManager.DisplayTitle("래비츠 인더스트리");
                    UIManager.DisplayShopSellScreen(player);
                    UIManager.DisplayPlayerInfoVillage(player);
                    UIManager.PositionCursorToInput();

                    input = InputKey();
                    if (input == 0) break;
                    if (input > storeList.Count || input < 0) continue;

                    UIManager.DisplayShopSellText(player.GetPlayerInvenList()[input - 1]);
                    player.GetInfo().money += player.PopInven(input - 1);
                    Console.ReadKey();
                }

            }
            
        }
        private void SceneInven()
        {
            Console.Clear();
            UIManager.DisplayTitle("랫츠 보이드 오프너");
            UIManager.DisplayInventory(player);

            List<Item> temp = player.GetPlayerInvenList();
            for (int i = 0; i < temp.Count; i++)
                Console.WriteLine($"[{i + 1}] " + temp[i].name);

            int input = InputKey();
            if (input <= 0) { ChangeScene(SCENESTATE.VILLAGE); return; }

            player.EQItem(input - 1);
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
            int maxId = 2000;
            List<Item> items = ObjectManager.Instance().GetItemList();
            foreach(Item item in items)
            {
                if (item.id == maxId || storeList == null) break;
                storeList.Add(item);
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
        public bool GetTurn() { return isTurn; }
        
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
        UIManager UIManager;
        private Player? player = null;
        private SCENESTATE sceneState = SCENESTATE.MAIN;
        private List<Item>? storeList = null;
        bool isTurn = false;

    }
}
