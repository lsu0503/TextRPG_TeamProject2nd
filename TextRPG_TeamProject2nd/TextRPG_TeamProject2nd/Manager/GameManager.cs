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
            player = new Player();
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
            //player = new Player();
            int raceid = 0;
            string name = string.Empty;
            while(!isComplete)
            {
                Console.Clear();
                Console.WriteLine("[캐릭터를 생성합니다]");
                name = InputLine();
                Console.Clear();
                Console.WriteLine("[종족을 선택하세요]");           
                Console.WriteLine("1.더미");
                Console.WriteLine("2.더미");
                Console.WriteLine("3.더미");
                Console.WriteLine("4.더미");
                raceid = (InputKey());
                Race temp = ObjectManager.Instance().GetRace(raceid);

                Console.WriteLine($"이름:{name} 종족:{temp.name}");
                Console.WriteLine("[0]:확정 [1]:다시 작성");
                if(InputKey() == 0) isComplete = true;
            }

            
            player.SetPlayer(raceid, name);
            player.Save();
            ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneMain()
        {
            Console.Clear();
            Console.WriteLine("----------게임제목-----------");
            Console.WriteLine("[0]:종료 [1]:새로 시작 [2]:불러오기");
            int input = InputKey();
            if (input == 0) { Environment.Exit(0); }
            if (input == 1) { ChangeScene(SCENESTATE.NONE); return;}
            if (input == 2) { player.Load(); ChangeScene(SCENESTATE.VILLAGE); }
           
        }
        private void SceneVIllage()
        {
            Console.Clear();
            Console.WriteLine("마을입니다.");
            Console.WriteLine("[0]:상점 [1]:퀘스트 [2]:보관함 [3]:던 전 [4]:저장 및 종료");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.STORE);
            else if (input == 1) ChangeScene(SCENESTATE.QUEST);
            else if (input == 2) ChangeScene(SCENESTATE.INVEN);
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

           

            while(true)
            {
                isTurn = true;
                while (true) //플레이어
                {

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

            Console.WriteLine("이름     : " + player.GetInfo().name);
            Console.WriteLine("종족     : " + player.GetInfo().race);
            Console.WriteLine("무기     : " + player.GetInfo().weaponId);
            Console.WriteLine("방어구   : " + player.GetInfo().armorId);
            Console.WriteLine("레벨     : " + player.GetInfo().level);
            Console.WriteLine("체력     : " + player.GetInfo().hp +"/"+ player.GetInfo().maxHp);
            Console.WriteLine("공격력   : " + player.GetInfo().attack);
            Console.WriteLine("방어력   : " + player.GetInfo().defence);
            Console.WriteLine("AP       : " + player.GetInfo().actionPoint);
            Console.WriteLine("경험치   : " + player.GetInfo().exp +"/"+ player.GetInfo().maxExp);
            Console.WriteLine("소지금   : " + player.GetInfo().money);

            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);

            List<Item> temp = player.GetPlayerInvenList();
            for (int i = 0; i < temp.Count; i++)
                Console.WriteLine($"[{i}] " + temp[i].name);
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
        private Player? player = null;
        private SCENESTATE sceneState = SCENESTATE.MAIN;
        private List<Item>? storeList = null;
        bool isTurn = false;

    }
}
