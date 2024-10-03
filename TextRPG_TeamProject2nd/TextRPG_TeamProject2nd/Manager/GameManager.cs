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
            player.BackVill();
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
            UIManager.DisplayTitle("원정 고양이 협회");
            UIManager.DisplayDungeonList();
            UIManager.DisplayPlayerInfoVillage(player);
            UIManager.PositionCursorToInput();    
            int input = InputKey();
            if(input == 0) 
            {
                ChangeScene(SCENESTATE.VILLAGE);
                return;
            }

            Random random = new Random();
            Map map = ObjectManager.Instance().GetMap(input - 1);
            List<int> MobNums = map.mapInfo.mobId;
            List<int> BossNums = map.mapInfo.bossId;
            int maxFloor = map.mapInfo.floor;
            int mobCount = 0;
            List<Monster> currentMobs = new List<Monster>();
            
            for(int i = 0; i < maxFloor; i++)
            {
                if (i == maxFloor - 1)
                {
                    currentMobs.Add(ObjectManager.Instance().GetMonster(BossNums[random.Next(0, BossNums.Count)]));
                    break;
                }
                currentMobs.Add(ObjectManager.Instance().GetMonster(MobNums[random.Next(0, MobNums.Count)]));
            } //몬스터 세팅
            Monster mob = currentMobs[mobCount];
            mob.Init();//몬스터는 꼭 초기화가 되어야 사용가능
         
            while(true)//던전내부
            {
                Console.Clear();
                /*UIManager.DisplayLog();
                UIManager.DisplayDungeonInfo(map, mobCount);
                UIManager.DisplayEnemyInfo(mob);
                UIManager.DisplayPlayerInfoDungeon(player);*/
                Console.WriteLine($"현재 남은 몬스터 : {maxFloor - mobCount}");
                Console.WriteLine($"[{mob.GetInfo().name}] HP:{mob.GetInfo().hp}/{mob.GetInfo().maxHp}");
                Console.WriteLine($"[{player.GetInfo().name}] HP:{player.GetInfo().hp}/{player.GetInfo().maxHp}");

                isTurn = true;
                while (true) //플레이어
                {
                    //UIManager.PositionCursorToInput();
                    Console.WriteLine("0턴넘기기 1스킬 2아이템");
                    int PlayerInput = InputKey();
                    if (PlayerInput == 0) break;

                    if (PlayerInput == 1)
                    {
                        int index = 0;
                        List<Skill> skills = player.GetPlayerSkillList();
                        foreach (Skill skill in skills)
                        {
                            Console.WriteLine($"[{index}].{skill.name}");
                            index++;
                        }

                        //UIManager.PositionCursorToInput();
                        int SkillInput = InputKey();
                        if (skills[SkillInput].type == SKILLTYPE.ATTACK && !(player.GetInfo().actionPoint < 0))
                        {
                            int val = player.UseSkill(SkillInput, mob);
                            Console.WriteLine($"{mob.GetInfo().name}에게 {val}만큼의 데미지를 주었다!");
                        }
                        else if (skills[SkillInput].type == SKILLTYPE.HEAL && !(player.GetInfo().actionPoint < 0))
                        {
                            int val = player.UseSkill(SkillInput, mob);
                            Console.WriteLine($"플레이어는 {val}만큼 회복했다.");
                        }
                    }

                    if (PlayerInput == 2)
                    {
                        Console.WriteLine("당신은 " + player.UseItem() + "만큼 회복했습니다.");
                        continue;
                    }

                    Console.ReadLine();
                }

                isTurn = false;
                if(true) //몬스터
                {
                    int val = mob.UseSkill(player);
                    Console.WriteLine($"몬스터가 당신에게 {val}만큼의 피해를 주었다!");
                    Console.ReadLine();
                }

                if(mob.GetInfo().hp <= 0)
                {
                    mobCount++;
                    if(mobCount >= maxFloor)
                    {
                        Console.WriteLine("모든 적 처리");
                        Console.ReadLine();
                        ChangeScene(SCENESTATE.VILLAGE);
                        return;
                    }

                    {
                        Console.WriteLine("몬스터가 사망했습니다.");
                        Item item = mob.Drops();
                        if(item.id != -1)
                        {
                            Console.WriteLine($"{item.name}을 얻었다!");
                            player.PushInven(item);
                        }
                        int moneyDrop = 20 + (mob.GetInfo().level * 3);
                        int exp = 20 + (mob.GetInfo().level * 2);
                        player.GetInfo().money += moneyDrop;
                        Console.WriteLine(moneyDrop + "만큼의 골드를 얻었다!");
                        Console.WriteLine(exp + "만큼의 경험치를 얻었다!");
                        Console.ReadLine();

                        mob = currentMobs[mobCount];
                        mob.Init();
                    }
                }

                if(player.GetInfo().hp <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("사망..");
                    ChangeScene(SCENESTATE.MAIN);
                    currentMobs.Clear();
                    Console.ReadLine();
                    Console.Clear();
                    return;
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
            UIManager.DisplayPlayerInfoVillage(player);
            UIManager.PositionCursorToInput();
           
            int input = InputKey();
            if (input <= 0) { ChangeScene(SCENESTATE.VILLAGE); return; }

            UIManager.DisplayEquipText(player.GetPlayerInvenList()[input - 1]);
            player.EQItem(input - 1);
            Console.ReadKey();

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
