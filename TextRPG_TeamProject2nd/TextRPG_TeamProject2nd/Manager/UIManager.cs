using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Object;
using TextRPG_TeamProject2nd.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace TeamProjectBin
{
    internal class UIManager
    {
        static public UIManager Instance()
        {
            if (instance == null)
                instance = new UIManager();

            return instance;
        }

        // 상단 화면 타이틀을 그리는 함수.
        public void DisplayTitle(string _titleText)
        {
            Console.WriteLine("===================================================================================================="); // 반각으로 100자.

            int titleByteLength = Encoding.Default.GetByteCount(_titleText); // 문자열의 바이트 크기 구하기.
            int fullwidthAmount = (titleByteLength - _titleText.Length) / 2; // (바이트 크기 - 문자열 길이) / [한글과 영어의 바이트 차이] = 전각 문자 개수.
            int titleTextLength = fullwidthAmount + _titleText.Length; // 전각 문자 갯수 만큼 문자열 길이에 가산.

            int xTurm = (100 - titleTextLength) / 2; // 중앙정렬. 100자에서 글자 수를 뺸 다음 2로 나누기.
            Console.CursorLeft = xTurm;
            Console.WriteLine(_titleText);

            Console.WriteLine("====================================================================================================");
        }

        // 우측 플레이어 정보를 그리는 함수.
        public int DisplayPlayerInfo(Player _player, int yPos)
        {
            PlayerInfo playerInfo = _player.GetInfo();
            List<string> tempStringList = new List<string>();
            int consoleWidth = Console.WindowWidth - 1;
            int wordLengthByte;
            int tempLength;
            int frameLeft = 0;

            tempStringList.Add(String.Format(playerInfo.name + $"     Lv.{playerInfo.level}" + $"[{(playerInfo.exp / (float)playerInfo.maxExp) * 100,5:N2}%]"));
            tempStringList.Add(String.Format($"HP: {playerInfo.hp,4} / {playerInfo.maxHp,-4}"));
            tempStringList.Add(String.Format($"AP: {_player.GetActionPoint()}"));
            tempStringList.Add(String.Format($""));
            tempStringList.Add(String.Format($"공격력: {playerInfo.attack,-3}"));
            tempStringList.Add(String.Format($"방어력: {playerInfo.defence,-3}"));
            tempStringList.Add(String.Format($"행동력: {playerInfo.actionPoint,-3}"));
            tempStringList.Add(String.Format($""));
            tempStringList.Add(String.Format($"소지금: {playerInfo.money}"));
            tempStringList.Add(String.Format($""));

            Item[] EquipArray = _player.GetPlayerEq();

            tempStringList.Add($"무  기: {EquipArray[0].name}");
            tempStringList.Add($"방어구: {EquipArray[1].name}");
            tempStringList.Add($"회복약: {EquipArray[2].name}");

            for (int i = 0; i < tempStringList.Count; i++)
            {
                // UTF-8일 경우 한글은 한 자에 3Byte로 구성된다. 이를 참고하여 수식을 구성했다.
                // [p.s. 본 프로젝트와는 관계 없지만, euc-kr일 경우에는 한글은 한 자에 2Byte로 구성된다.]
                wordLengthByte = Encoding.Default.GetByteCount(tempStringList[i]);
                tempLength = ((wordLengthByte - tempStringList[i].Length) / 2) + tempStringList[i].Length + 1;
                if (frameLeft < tempLength)
                    frameLeft = tempLength;
            }

            for (int i = 0; i < tempStringList.Count; i++)
            {
                Console.SetCursorPosition(consoleWidth - frameLeft, yPos + i);
                Console.Write(tempStringList[i]);
            }

            return frameLeft;
        }

        // 커서 위치 최하단(입력 창)으로 옮기는 함수.
        public void PositionCursorToInput()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
        }

        // 타이틀 화면 관련 UI 함수
        public void DisplayStartMenu()
        {
            Console.WriteLine("[1] 새로 시작");
            Console.WriteLine("[2] 불러오기");
            Console.WriteLine("[0] 게임 종료");
        }

        public void DisplayFailedLoad()
        {
            Console.WriteLine("세이브파일이 없습니다.");
        }

        // 캐릭터 생성 관련 UI
        public void DisplayCreateChracter()
        {
            Console.WriteLine("당신은 반짝거리는 태양 아래 서 있습니다.");
            Console.WriteLine("바로 앞에는 웅장한 모험가 협회가 기다리고 있구요.");
            Console.WriteLine("이윽고 모험자로의 첫 말을 내딪으면...!\n");
            Console.WriteLine("[꽃사슴 접수원]: 접수 서류 부터 작성하시고, 저-기 창구로 제출하세요. 다음분!");
            Console.WriteLine("\n... 일단은 첫발을 내딪었다!");
        }

        // 이름 입력 화면
        public void DisplayCreateCharacterName()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n이름을 입력 해 주십시오.");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 종족 선택 화면
        public void DisplayCreateCharacterRace(int _page)
        {
            List<Race> raceList = ObjectManager.Instance().GetRaceList();

            Console.WriteLine("\n종족을 기입 해 주십시오.\n");

            for (int i = 0 * _page; i < 5; i++)
            {
                if ((_page * 5) + i >= raceList.Count)
                    break;

                Console.Write($"[{i + 1}] {raceList[(_page * 9) + i].name}");
                Console.CursorLeft = 25;
                Console.WriteLine($"| {raceList[(_page * 9) + i].desc}");
                Console.CursorLeft = 25;
                Console.WriteLine($"| HP: {raceList[(_page * 9) + i].hp, -5}   공격력: {raceList[(_page * 9) + i].attack, -3}" +
                                  $"   방어력: {raceList[(_page * 9) + i].defence, -3}   행동력: {raceList[(_page * 9) + i].actionPoint}");
                Console.WriteLine();
            }
        }

        // 마을 화면 관련 UI 함수
        public void DisplayVillageMenu()
        {
            Console.WriteLine("[1] 상  점         : 래비츠 인더스트리");
            Console.WriteLine("[2] 보관함         : 랫츠 보이드 오프너");
            Console.WriteLine("[3] 퀘스트         : 도기독스 알선소");
            Console.WriteLine("[4] 던  전         : 원정 고양이 협회");
            Console.WriteLine("[0] 저장하고 나가기: 세어가는 양 여관");
        }

        // 상점 관련 UI 함수.
        public void DisplayShopEntrance()
        {
            Console.WriteLine("[하얀 토끼 점장]: 노동은 신성한 땀의 결정이지.");
            Console.WriteLine("                  무엇을 찾으시오.\n");

            Console.WriteLine("[1] 구매하기");
            Console.WriteLine("[2] 판매하기");
            Console.WriteLine("[0] 나 가 기");
        }

        // 상점 - 구매하기
        public void DisplayShopBuyList()
        {
            List<Item> storeList = GameManager.Instance().GetStoreList();

            Console.WriteLine("[하얀 토끼 점장]: 지금 팔고 있는 물건은 이러하다오.");
            Console.WriteLine("                  재고를 걱정할 필요는 없소. 노동이란 그 끝이 없음이니.\n");

            for (int i = 0; i < storeList.Count; i++)
            {
                Console.Write($"[{i + 1}] {storeList[i].name}");
                Console.CursorLeft = 23;
                Console.WriteLine($"<{storeList[i].value,4} Gold> | {storeList[i].desc}");
                switch (storeList[i].type)
                {
                    case ITEMTYPE.WEAPON:
                        Console.CursorLeft = 35;
                        Console.Write($"| 공격력: {storeList[i].attack, 3}  방어력: {storeList[i].defence, 3}");
                        for(int j = 0; j < storeList[i].skill.Count; j++)
                            Console.Write($"    스킬 {j}: {ObjectManager.Instance().GetSkill(storeList[i].skill[j]).name}");
                        Console.WriteLine();
                        break;
                    case ITEMTYPE.ARMOR:
                        Console.CursorLeft = 35;
                        Console.WriteLine($"| 공격력: {storeList[i].attack,3}  방어력: {storeList[i].defence,3} ");
                        break;
                    case ITEMTYPE.CONSUMABLE:
                        Console.CursorLeft = 35;
                        Console.WriteLine($"| 의약품   회복량: {storeList[i].amount}");
                        break;
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        // 구매 완료 시 표시.
        public void DisplayShopBuyText(Item _itemBought)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.WriteLine($"{_itemBought.name}을(를) {_itemBought.value} Gold에 구매하셨습니다.");
            Console.WriteLine($" --- 진행하시려면 아무 버튼이나 눌러주세요. ---");
        }

        // 상점 - 판매하기
        public void DisplayShopSellScreen(Player _player)
        {
            List<Item> invenList = _player.GetPlayerInvenList();

            Console.WriteLine("[하얀 토끼 점장]: 호오. 노동의 결과는 항상 그 가치를 인정받아야 함이라오.");
            Console.WriteLine("                  보여주시오. 원가 까지는 아니더라도 값을 좀 쳐 주겠소.\n");

            for (int i = 0; i < invenList.Count; i++)
            {
                Console.Write($"[{i + 1}] {invenList[i].name} <{invenList[i].value} Gold>");
                Console.CursorLeft = 35;
                Console.WriteLine($"| {invenList[i].desc}");

                switch (invenList[i].type)
                {
                    case ITEMTYPE.WEAPON:
                        Console.CursorLeft = 35;
                        Console.Write($"| 공격력: {invenList[i].attack,3}  방어력: {invenList[i].defence,3}");
                        for (int j = 0; j < invenList[i].skill.Count; j++)
                            Console.Write($"  스킬 {j}: {ObjectManager.Instance().GetSkill(invenList[i].skill[j]).name}");
                        Console.WriteLine();
                        break;
                    case ITEMTYPE.ARMOR:
                        Console.CursorLeft = 35;
                        Console.WriteLine($"| 공격력: {invenList[i].attack,3}  방어력: {invenList[i].defence,3} ");
                        break;
                    case ITEMTYPE.CONSUMABLE:
                        Console.CursorLeft = 35;
                        Console.WriteLine($"| 의약품   회복량: {invenList[i].amount}");
                        break;
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        // 판매 완료 시 표시.
        public void DisplayShopSellText(Item _itemBought)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.WriteLine($"{_itemBought.name}을(을) {_itemBought.value} Gold에 판매하셨습니다.");
            Console.WriteLine($" --- 진행하시려면 아무 버튼이나 눌러주세요. ---");
        }

        // 보관함
        public void DisplayInventory(Player _player)
        {
            List<Item> invenList = _player.GetPlayerInvenList();

            Console.WriteLine("[단안경 신사 쥐돌이]: 귀하의 보관함을 열어드리지요. 저희가 모아놓은 귀하의 물건들입니다.");
            Console.WriteLine("                      아. 돈은 됬고, 다음에도 잘, 부탁드리도록 하겠습니다.");
            Console.WriteLine("                      뒤따라 가면서 얻는 부산물이 꽤 많거든요. 후후훗.\n");

            for (int i = 0; i < invenList.Count; i++)
            {
                Console.Write($"[{i + 1}] {invenList[i].name}");
                Console.CursorLeft = 25;
                Console.WriteLine($"| {invenList[i].desc}");

                switch (invenList[i].type)
                {
                    case ITEMTYPE.WEAPON:
                        Console.CursorLeft = 25;
                        Console.Write($"| 공격력: {invenList[i].attack,3}  방어력: {invenList[i].defence,3}");
                        for (int j = 0; j < invenList[i].skill.Count; j++)
                            Console.Write($"    스킬 {j}: {ObjectManager.Instance().GetSkill(invenList[i].skill[j]).name}");
                        Console.WriteLine();
                        break;
                    case ITEMTYPE.ARMOR:
                        Console.CursorLeft = 25;
                        Console.WriteLine($"| 공격력: {invenList[i].attack,3}  방어력: {invenList[i].defence,3} ");
                        break;
                    case ITEMTYPE.CONSUMABLE:
                        Console.CursorLeft = 25;
                        Console.WriteLine($"| 의약품   회복량: {invenList[i].amount}");
                        break;
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        public void DisplayEquipText(Item _itemEquiped)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.WriteLine($"{_itemEquiped.name}을(를) 장착하셨습니다.");
            Console.WriteLine($" --- 진행하시려면 아무 버튼이나 눌러주세요. ---");
        }

        // 퀘스트 화면
        public void DisplayQuest(Player _player)
        {
            Quest quest = _player.GetCurrentQuest();

            Console.WriteLine("[멍뭉이 조사원]: 안녕하세요! 현재 귀하께서는 하단의 임무를 수행하고 계십니다!");
            Console.WriteLine("                 도기독스 보험단이랑 연계되는 임무라서 임무를 바꾸실 수는 없어요!");
            Console.WriteLine("                 조금 늦게 하셔도 좋으니까, 차분히 진행해 주세요!\n\n\n");

            // 퀘스트 내용 표시
            Console.WriteLine($"{quest.questName}");
            Console.WriteLine("------------------------------------------------------------------------------");

            string[] descStrings = quest.questDesc.Split("@");
            for(int i = 0; i < descStrings.Length; i++)
                Console.WriteLine($"{descStrings[i]}");
            Console.WriteLine();
            Console.WriteLine($"{ObjectManager.Instance().GetMonster(quest.questTargetId).GetInfo().name} {quest.questTargetAmount}마리 잡기. [{quest.questProgressAmount} / {quest.questTargetAmount}]");
            Console.Write($"보상: ");
            Item tempItem = ObjectManager.Instance().GetItem(quest.rewardItemId);
            if (tempItem != null)
                Console.Write($"{tempItem.name}, ");
            Console.WriteLine($"{quest.rewardGold} Gold, exp {quest.rewardGold}\n");

            if (quest.CheckProgress())
                Console.WriteLine("[1] 퀘스트 보고    ");

            Console.WriteLine("[0] 나가기");

        }

        // 퀘스트 보상 습득 확인
        public void DisplayQuestClear(Quest _quest)
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 5);
            Console.WriteLine($"{_quest.questName} 미션을 완수하셨습니다.");
            Item tempItem = ObjectManager.Instance().GetItem(_quest.rewardItemId);
            if (tempItem != null)
                Console.Write($"{tempItem.name}, ");
            Console.WriteLine($"{_quest.rewardGold} Gold, Exp {_quest.rewardGold}를 습득하였습니다.");
            Console.WriteLine($" --- 진행하시려면 아무 버튼이나 눌러주세요. ---");
        }

        // 던전 진입 UI
        public void DisplayDungeonList()
        {
            List<Map> mapList = ObjectManager.Instance().GetMapList();

            Console.WriteLine("[치즈 고양이 소년]: 어서와랴옹! 고양이들이 보증하는 던전 원정 협회댜옹!");
            Console.WriteLine("                    지금 가지고 있는 정보는 여기 있댜옹. 어디를 들어갈거냐옹?");
            Console.WriteLine("                    아, 들어가면 살아나오는 건 본인 책임이댜옹. 신중하게 결정하랴옹.\n");

            // 던전 목록 출력.
            for (int i = 0; i < mapList.Count; i++)
            {
                Console.Write($"[{i + 1}] {mapList[i].mapInfo.name}");
                Console.CursorLeft = 23;
                Console.WriteLine($"[Lv.{mapList[i].mapInfo.levelLimit, 2} / 전투: {mapList[i].mapInfo.floor, 2}] | {mapList[i].mapInfo.desc}");
            }

            Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        //게임 종료 관련 UI(마을에서)
        public void DisplayExitGameVillage()
        {
            Console.WriteLine("[뜨개질 중인 양 할머니]: 양 세는 여관에 어서오거라. 오늘도 수고 많았다.");
            Console.WriteLine("                         마음 높고 푹 쉬려무나. 숙박비는 걱정 말고. 다른 곳에서 지원이 들어온단다.");
            Console.WriteLine("                         가끔 기한 없이 잠들기도 하지만, 모두들 마음에 쏙 들어한단다.");
            Console.WriteLine("                         우리 침구에 쓰이는 건 우리 수면양족의 털이거든. 이게 주기적으로 안깎아내면 무거워서...");
            Console.WriteLine("");
            Console.WriteLine("<말이 끝날 기미가 안보인다. 빨리 용무를 마쳐야 할 거 같다.>\n");

            Console.WriteLine();
            Console.WriteLine("종료하시겠습니까? [Y - 예 / N - 아니오]");
        }
        
        /* 던전 내부 관련 UI */
        // 전투 행동 선택 라인 출력
        public void DisplayActionLine(Player _player)
        {
            Item[] playerEquiped = _player.GetPlayerEq();

            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.Write("[1] 스킬    ");
            Console.Write($"[2] {playerEquiped[2].name}");
            if (playerEquiped[2].name != "없음")
                Console.Write($"[{_player.GetConsumeCount()}]");
            Console.WriteLine("    [0] 턴 넘기기");
        }

        // 스킬 선택 라인 출력
        public void DisplaySkillLine(Player _player)
        {
            List<Skill> playerSkillList = _player.GetPlayerSkillList();

            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            for (int i = 0; i < playerSkillList.Count; i++)
            {
                Console.Write($"{$"[{i + 1}] {playerSkillList[i].name} [AP {playerSkillList[i].cost}]", -25}");
            }
            Console.WriteLine("[0] 취소");
        }

        public void DisplayInputReady()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine($" --- 진행하시려면 아무 버튼이나 눌러주세요. ---");
        }

        public void DisplayApShotage()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.WriteLine("AP가 모자랍니다.");
            Console.WriteLine($" --- 진행하시려면 아무 버튼이나 눌러주세요. ---");
        }

        // 던전 정보 출력 함수
        public void DisplayDungeonInfo(Map _map, int _floor, int frameLeft)
        {
            int windowWidth = Console.WindowWidth - 1;
            
            Console.SetCursorPosition(windowWidth - frameLeft, 0);
            Console.Write($"{_map.mapInfo.name}");
            
            Console.SetCursorPosition(windowWidth - frameLeft, 1);
            Console.Write($"[{_floor + 1} / {_map.mapInfo.floor}]");
        }

        // 적 정보 출력 함수
        public void DisplayEnemyInfo(Monster _monster, int frameLeft)
        {
            int frameTop = 3;
            int windowWidth = Console.WindowWidth - 1;

            Console.SetCursorPosition(windowWidth - frameLeft, frameTop);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{_monster.GetInfo().name}"); // 몬스터 이름 빨간색으로 표시
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" [Lv.{_monster.GetInfo().level, 2}]"); // 몬스터 레벨 일단 노란색으로.

            Console.SetCursorPosition(windowWidth - frameLeft, frameTop + 1);
            if (_monster.GetInfo().hp > 0)
                Console.ForegroundColor = ConsoleColor.White;
            else
                Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"HP {_monster.GetInfo().hp,4} / {_monster.GetInfo().maxHp,-4}"); // 몬스터 체력 [생존 시 흰색 / 사망 시 회색]

            Console.ForegroundColor = ConsoleColor.Gray; // 글자 색 리셋.
        }
        
        //// 현재 턴 정보 출력 함수
        //public void DisplayCurrntTurn(bool _isPlayerTurn)
        //{
        //    int playerInfoTop = 10;
        //    int monsterInfoBottom = 6;
        //    int frameHeight = Console.WindowHeight - 4;
        //    int turnInfoMiddle = ((frameHeight - playerInfoTop - monsterInfoBottom) / 2) + monsterInfoBottom;

        //    int frameRight = Console.WindowWidth - 1;

        //    Console.SetCursorPosition(frameRight - 36, turnInfoMiddle);
        //    Console.Write("Current Turn");

        //    if (_isPlayerTurn)
        //    {
        //        Console.SetCursorPosition(frameRight - 38, turnInfoMiddle + 1);
        //        Console.Write("↓ Now Your Turn");
        //    }

        //    else
        //    {
        //        Console.SetCursorPosition(frameRight - 40, turnInfoMiddle - 1);
        //        Console.Write("↑ Now Enemy's Turn");

        //        Console.SetCursorPosition(frameRight - 44, turnInfoMiddle + 1);
        //        Console.Write("Press Any Button to Progress");
        //    }
        //}
        
        // 로그 출력 함수
        public void DisplayLog()
        {
            int logFrameHeight = Console.WindowHeight - 4;

            for(int i = Math.Max(0, logList.Count - logFrameHeight); i < logList.Count; i++)
                Console.WriteLine(logList[i]);
        }

        // 로그 생성 함수
        public void CreateLogAppearance(Monster _monster)
        {
            logList.Add($"[{_monster.GetInfo().name}]이 출현하였다!");
            logList.Add("");
        }

        public void CreateLogAction(Player _player, Monster _monster, Skill _skill, int _power, int _skillType)
        {
            if (GameManager.Instance().GetTurn())
            {
                logList.Add($"[{_player.GetType().Name}]은(는) [{_skill.name}]을(를) 사용했다!");
                if(_skillType == 0)
                    logList.Add($"[{_monster.GetType().Name}]은(는) {_power}의 피해를 받았다!");
                else if( _skillType == 1)
                    logList.Add($"[{_player.GetType().Name}]은(는) {_power}만큼 회복했다!");
            }

            else
            {
                logList.Add($"[{_monster.GetType().Name}]은(는) [{_skill.name}]을(를) 사용했다!");
                if (_skillType == 0)
                    logList.Add($"[{_player.GetType().Name}]은(는) {_power}의 피해를 받았다!");
                else if (_skillType == 1)
                    logList.Add($"[{_monster.GetType().Name}]은(는) {_power}만큼 회복했다!");
            }

            logList.Add("");
        }

        public void CreateLogItem(Player _player, Monster _monster, int _power)
        {
            Item[] playerEquiped = _player.GetPlayerEq();

            logList.Add($"{_monster.GetType().Name}]은(는) [{playerEquiped[2].name}]을(를) 사용했다!");
            logList.Add($"[{_player.GetType().Name}]은(는) {_power}만큼 회복했다!");
            logList.Add("");
        }

        // 승리 로그 생성 함수
        public void CreateLogVictory(Monster _monster, Item _item, int _money, int _exp)
        {
            logList.Add($"[{_monster.GetType().Name}]과의 전투에서 승리하였다!");
            
            if (_item.id != -1)
                logList.Add($"[{_item.name}]을(를) 습득하였다.");
            
            logList.Add($"{_money} Gold와 경험치 {_exp}를 얻었다!");

            logList.Add("");
        }

        public void CreateLogLevelUp(Player _player)
        {
            logList.Add($"레벨업!");
            logList.Add($"[{_player.GetType().Name}]은(는) Lv.{_player.GetInfo().level}이 되었다.");
            logList.Add("");
        }

        public void CreateLogDefeat()
        {
            logList.Add($"패배하였다...");
            logList.Add("");
        }

        public void CreateLogComplete(Map _map)
        {
            logList.Add($"{_map.mapInfo.name}을 모두 재패하였다!");
            logList.Add($"마을로 돌아가서 승전보를 울리자!");
            logList.Add("");
        }

        public void ClearLogList()
        {
            logList.Clear();
        }
        //-------------------------------------------------
        private List<string> logList = new List<string>();

        static UIManager? instance;
    }
}