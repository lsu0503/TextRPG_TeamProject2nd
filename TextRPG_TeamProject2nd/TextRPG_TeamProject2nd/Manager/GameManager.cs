using TeamProjectBin;
using TextRPG_TeamProject2nd.Object;
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
        CONNECT,
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
                    player.Save();
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
                case SCENESTATE.CONNECT:
                    SceneConnect();
                    break;
                case SCENESTATE.END:
                    SceneEnd();
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
            Console.Clear();
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
            UIManager.DisplayPlayerInfo(player, 0);
            UIManager.PositionCursorToInput();
            int input = InputKey();
            if (input == 1) ChangeScene(SCENESTATE.STORE);
            else if (input == 2) ChangeScene(SCENESTATE.INVEN);
            else if (input == 3) ChangeScene(SCENESTATE.QUEST);
            else if (input == 4) ChangeScene(SCENESTATE.FILED);
            else if (input == 5) ChangeScene(SCENESTATE.CONNECT);
            else if (input == 0) ChangeScene(SCENESTATE.END);
        }
        private void SceneFiled()
        {
            Console.Clear();
            UIManager.DisplayTitle("원정 고양이 협회");
            UIManager.DisplayDungeonList();
            UIManager.DisplayPlayerInfo(player, 0);
            UIManager.PositionCursorToInput();    
            int input = InputKey();
            if(input == 0) 
            {
                ChangeScene(SCENESTATE.VILLAGE);
                return;
            }

            Random random = new Random();
            Map map = ObjectManager.Instance().GetMap(input - 1);
            if (map == null) return;
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

            isTurn = true;

            while(true)//던전내부
            {
                player.OnNextTurn();
                if (isTurn)
                {
                    while (true) //플레이어
                    {
                        
                        SceneFieldUIDisplay(map, mob, mobCount);
                        UIManager.DisplayActionLine(player);
                        int PlayerInput = InputKey();
                        if (PlayerInput == 0) break;

                        if (PlayerInput == 1)
                        {
                            List<Skill> skills = player.GetPlayerSkillList();
                            SceneFieldUIDisplay(map, mob, mobCount);
                            UIManager.DisplaySkillLine(player);
                            int inputAction = InputKey();

                            if (inputAction > 0)
                            {
                                if (player.GetActionPoint() > 0)
                                {
                                    int SkillInput = inputAction - 1;
                                    bool isCrit;
                                    int val = player.UseSkill(SkillInput, out isCrit, mob);

                                    if (skills[SkillInput].type == SKILLTYPE.ATTACK)
                                        UIManager.CreateLogAction(player, mob, player.GetSkill(SkillInput), val, 0, isCrit);

                                    else if (skills[SkillInput].type == SKILLTYPE.HEAL)
                                        UIManager.CreateLogAction(player, mob, player.GetSkill(SkillInput), val, 1, isCrit);
                                }

                                else
                                {
                                    UIManager.DisplayApShotage();
                                }
                            }   
                        }

                        if (PlayerInput == 2)
                        {
                            int val = player.UseItem();
                            UIManager.CreateLogItem(player, mob, val);
                            continue;
                        }

                        SceneFieldUIDisplay(map, mob, mobCount);
                        UIManager.DisplayInputReady();
                        Console.ReadKey();
                    }
                }
                
                if(!isTurn) //몬스터
                {
                    Skill tempSkill;
                    bool isCrit;
                    int val = mob.UseSkill(player, out tempSkill, out isCrit);
                    if (tempSkill.type == SKILLTYPE.ATTACK)
                        UIManager.CreateLogAction(player, mob, tempSkill, val, 0, isCrit);

                    else if (tempSkill.type == SKILLTYPE.HEAL)
                        UIManager.CreateLogAction(player, mob, tempSkill, val, 1, isCrit);

                    SceneFieldUIDisplay(map, mob, mobCount);
                    UIManager.DisplayInputReady();
                    Console.ReadKey();
                }

                isTurn = !isTurn;

                if (mob.GetInfo().hp <= 0)
                {
                    Item item = mob.Drops();
                    if (item.id != -1)
                        player.PushInven(item);

                    int moneyDrop = 35 + (mob.GetInfo().level * 4);
                    int exp = 20 + (mob.GetInfo().level * 2);
                    player.GetInfo().money += moneyDrop;
                    bool levelUp = player.GetExp(exp);

                    UIManager.CreateLogVictory(mob, item, moneyDrop, exp);

                    if (levelUp)
                        UIManager.CreateLogLevelUp(player);

                    player.GetCurrentQuest().ProgressQuest(mob);
                    SceneFieldUIDisplay(map, mob, mobCount);
                    UIManager.DisplayInputReady();
                    Console.ReadKey();

                    if (mobCount >= maxFloor - 1)
                    {
                        UIManager.CreateLogComplete(map);
                        SceneFieldUIDisplay(map, mob, mobCount);
                        UIManager.DisplayInputReady();
                        ChangeScene(SCENESTATE.VILLAGE);
                        UIManager.ClearLogList();
                        Console.ReadKey();
                        return;
                    }

                    else
                    {
                        UIManager.ClearLogList();
                        isTurn = true;
                        mobCount++;
                        mob = currentMobs[mobCount];
                        mob.Init();
                    }
                }

                if(player.GetInfo().hp <= 0)
                {
                    UIManager.CreateLogDefeat();
                    SceneFieldUIDisplay(map, mob, mobCount);
                    UIManager.DisplayInputReady();

                    ChangeScene(SCENESTATE.MAIN);
                    currentMobs.Clear();
                    UIManager.ClearLogList();
                    Console.ReadKey();
                    return;
                }
            }
        }

        private void SceneFieldUIDisplay(Map _map, Monster _mob, int _mobCount)
        {
            Console.Clear();
            UIManager.DisplayLog();
            int frameLeft = UIManager.DisplayPlayerInfo(player, 6);
            UIManager.DisplayDungeonInfo(_map, _mobCount, frameLeft);
            UIManager.DisplayEnemyInfo(_mob, frameLeft);
            UIManager.PositionCursorToInput();
        }

        private void SceneStore()
        {
            Console.Clear();
            UIManager.DisplayTitle("래비츠 인더스트리");
            UIManager.DisplayShopEntrance();
            UIManager.DisplayPlayerInfo(player, 0);
            UIManager.PositionCursorToInput();
            int signal = InputKey();
            if (signal == 0) { ChangeScene(SCENESTATE.VILLAGE); return; }
            
            while(true)
            {
                Console.Clear();
                int input = -1;
                if (signal == 1) //구매
                {
                    UIManager.DisplayTitle("래비츠 인더스트리");
                    UIManager.DisplayShopBuyList();
                    UIManager.DisplayPlayerInfo(player, 0);
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
                else if (signal == 2) //판매
                {
                    UIManager.DisplayTitle("래비츠 인더스트리");
                    UIManager.DisplayShopSellScreen(player);
                    UIManager.DisplayPlayerInfo(player, 0);
                    UIManager.PositionCursorToInput();

                    input = InputKey();
                    if (input == 0) break;
                    if (input > player.GetPlayerInvenList().Count || input < 0) continue;

                    UIManager.DisplayShopSellText(player.GetPlayerInvenList()[input - 1]);
                    player.GetInfo().money += player.PopInven(input - 1);
                    Console.ReadKey();
                }
                else if (signal >= 3 || signal < 0) { break; }

            }
            
        }
        private void SceneInven()
        {
            Console.Clear();
            UIManager.DisplayTitle("랫츠 보이드 오프너");
            UIManager.DisplayInventory(player);
            UIManager.DisplayPlayerInfo(player, 0);
            UIManager.PositionCursorToInput();
           
            int input = InputKey();
            if (input <= 0) { ChangeScene(SCENESTATE.VILLAGE); return; }

            if (input <= player.GetPlayerInvenList().Count)
            {
                UIManager.DisplayEquipText(player.GetPlayerInvenList()[input - 1]);
                player.EQItem(input - 1);
                Console.ReadKey();
            }
        }
        private void SceneOuest()
        {
            Console.Clear();
            UIManager.DisplayTitle("도기독스 알선소");
            UIManager.DisplayQuest(player);
            UIManager.DisplayPlayerInfo(player, 0);
            UIManager.PositionCursorToInput();

            int input = InputKey();
            Quest currentQuest = player.GetCurrentQuest();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
            else if(input == 1 && currentQuest.CheckProgress())
            {
                if (currentQuest.rewardItemId != 0)
                {
                    Item reward = ObjectManager.Instance().GetItem(currentQuest.rewardItemId);
                    player.PushInven(reward);
                }
                player.GetExp(currentQuest.rewardGold);
                player.GetInfo().money += currentQuest.rewardGold;
                Quest nextQuest = ObjectManager.Instance().GetQuest(currentQuest.questId + 1);
                
                if(nextQuest == null)
                    nextQuest = ObjectManager.Instance().GetQuest(currentQuest.questId);

                UIManager.DisplayQuestClear(currentQuest, player);
                Console.ReadKey();

                player.SetQuest(nextQuest);

            }
        }
        private void SceneEnd()
        {
            Console.Clear();
            UIManager.DisplayTitle("세어가는 양 여관");
            UIManager.DisplayExitGameVillage();
            UIManager.DisplayPlayerInfo(player, 0);
            UIManager.PositionCursorToInput();

            int input = InputKey();
            if (input == 0) { ChangeScene(SCENESTATE.VILLAGE); }
            if (input == 1) { player.Save(); Environment.Exit(0); }
        }
        private void SceneConnect()
        {
            Console.Clear();
            UIManager.DisplayTitle("애니멀 월드 대륙간 거래소 창구");
            Console.WriteLine("[슬라임 과장] : (O ㅅ O ) 개인 거래소를 개설 또는 참가를 하시면 다른 대륙의 친구분과 대화 또는 거래가 가능하십니다. ");
            Console.WriteLine();
            Console.WriteLine("[0]나 가 기");
            Console.WriteLine("[1]거래소 개설");
            Console.WriteLine("[2]거래소 참가");
            UIManager.PositionCursorToInput();
            int key = InputKey();
            if (key == 0) { ChangeScene(SCENESTATE.VILLAGE); return; }

            if (key == 1)
            {
                try
                {
                    Console.WriteLine("본인의 아이피를 입력하세요.");
                    string ip = InputLine();
                    Console.WriteLine("상대방을 기다립니다...");
                    Connector.Instance().HostStart(ip);
                    while (true)
                    {
                        Console.Clear();
                        int index = 1;
                        Console.WriteLine("보낼 아이템을 선택하세요. [아이템번호:보내기] [0:메세지만 보내기]");
                        foreach (Item it in player.GetPlayerInvenList())
                        {
                            Console.WriteLine($"{index} : {it.name}");
                            index++;
                        }
                        
                        index = InputKey();
                        Item? sendItem = null;
                        if (index != 0)
                        {
                            sendItem = player.GetPlayerInvenList()[index - 1];
                            player.PopInven(index - 1);
                        }             
                        Console.WriteLine("메세지를 입력하세요.");
                        string mess = InputLine();

                        if(sendItem != null)
                            Connector.Instance().Send(player.GetInfo().name, sendItem.id, mess);
                        else
                            Connector.Instance().Send(player.GetInfo().name, 0, mess);
                        //--------------------------------------

                        Console.WriteLine("상대방의 응답을 기다립니다..");
                        string[] packet = Connector.Instance().Recv();
                        Item? item = null;
                        if (int.Parse(packet[1]) != 0)
                        {
                            item = ObjectManager.Instance().GetItem(int.Parse(packet[1]));
                            Console.WriteLine($"[{packet[0]}]이(가) [{item.name}]을 보냈습니다.");
                            Console.WriteLine($"[{item.name}]이(가) 인벤토리에 추가 되었습니다.");
                            player.PushInven(item);
                        }
                        Console.WriteLine($"[{packet[0]}]:{packet[2]}");
                        
                        Console.WriteLine("[0]:마을로 가기 [1]:계속하기");
                        int input = InputKey();
                        if (input == 0)
                        {
                            ChangeScene(SCENESTATE.VILLAGE);
                            Connector.Instance().Close();
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("연결 종료.. [Enter:마을로 이동]");
                    Console.ReadLine();
                    ChangeScene(SCENESTATE.VILLAGE);
                    Connector.Instance().Close();
                    return;
                }
            } //거래소 개설
            if (key == 2)
            {
                try
                {
                    Console.WriteLine("거래소의 아이피를 입력하세요.");
                    string ip = InputLine();
                    Console.WriteLine("접속중...");
                    Connector.Instance().JoinStart(ip);
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("상대방의 응답을 기다립니다..");
                        string[] packet = Connector.Instance().Recv();
                        Item? item = null;
                        if (int.Parse(packet[1]) != 0)
                        {
                            item = ObjectManager.Instance().GetItem(int.Parse(packet[1]));
                            Console.WriteLine($"[{packet[0]}]이(가) [{item.name}]을 보냈습니다.");
                            Console.WriteLine($"[{item.name}]이(가) 인벤토리에 추가 되었습니다.");
                            player.PushInven(item);
                        }
                        Console.WriteLine($"[{packet[0]}]:{packet[2]}");

                        Console.WriteLine("[0]:마을로 가기 [1]:계속하기");
                        int input = InputKey();
                        if (input == 0)
                        {
                            ChangeScene(SCENESTATE.VILLAGE);
                            Connector.Instance().Close();
                            break;
                        }
                        //-------------------------

                        int index = 1;
                        Console.WriteLine("보낼 아이템을 선택하세요. [아이템번호:보내기] [0:메세지만 보내기]");
                        foreach (Item it in player.GetPlayerInvenList())
                        {
                            Console.WriteLine($"{index} : {it.name}");
                            index++;
                        }

                        index = InputKey();
                        Item? sendItem = null;
                        if (index != 0)
                        {
                            sendItem = player.GetPlayerInvenList()[index - 1];
                            player.PopInven(index - 1);
                        }
                        Console.WriteLine("메세지를 입력하세요.");
                        string mess = InputLine();

                        if (sendItem != null)
                            Connector.Instance().Send(player.GetInfo().name, sendItem.id, mess);
                        else
                            Connector.Instance().Send(player.GetInfo().name, 0, mess);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("연결 종료.. [Enter:마을로 이동]");
                    Console.ReadLine();
                    ChangeScene(SCENESTATE.VILLAGE);
                    Connector.Instance().Close();
                    return;
                }

            } //거래소 참가

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
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">>                                                              ");
                Console.SetCursorPosition(2, Console.CursorTop);
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
