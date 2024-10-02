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
        public void DisplayPlayerInfoVillage(Player _player)
        {
            PlayerInfo playerInfo = _player.GetInfo();
            List<string> tempStringList = new List<string>();
            int windowWidth = Console.WindowWidth;
            int wordLengthByte;
            int xPos = 0;

            tempStringList.Add(String.Format(playerInfo.name + $"     Lv.{playerInfo.level}" + $"[{playerInfo.exp / (float)playerInfo.maxExp: 2N, 5}]"));
            tempStringList.Add(String.Format($"HP: {playerInfo.hp,4} / {playerInfo.maxHp,-4}"));
            tempStringList.Add(String.Format($"공격력: {playerInfo.attack,-3}"));
            tempStringList.Add(String.Format($"방어력: {playerInfo.defence,-3}"));
            tempStringList.Add(String.Format($"행동력: {playerInfo.actionPoint,-3}"));
            tempStringList.Add("");

            if(playerInfo.weaponId != null)
                tempStringList.Add($"무  기: {ObjectManager.Instance().GetItem(playerInfo.weaponId).name, 40}");
            else
                tempStringList.Add($"{"무  기: ----------------", 40}");

            if(playerInfo.armorId != null)
                tempStringList.Add($"방어구: {ObjectManager.Instance().GetItem(playerInfo.armorId).name, 40}");
            else
                tempStringList.Add($"{"방어구: ----------------",40}");

            for (int i = 0; i < tempStringList.Count; i++)
            {
                // UTF-8일 경우 한글은 한 자에 3Byte로 구성된다. 이를 참고하여 수식을 구성했다.
                // [p.s. 본 프로젝트와는 관계 없지만, euc-kr일 경우에는 한글은 한 자에 2Byte로 구성된다.]
                wordLengthByte = Encoding.Default.GetByteCount(tempStringList[i]);
                Console.SetCursorPosition(windowWidth - (((wordLengthByte - tempStringList[i].Length) / 2) + tempStringList[i].Length + 1), i);
                Console.Write(tempStringList[i]);
            }
        }

        // 커서 위치 최하단(입력 창)으로 옮기는 함수.
        public void PositionCursorToInput()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
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
            Console.WriteLine("[꽃사슴 접수원]: 접수 서류 부터 작성하시고, 저-기 창구로 제출하세요.");
            Console.WriteLine("\n... 일단은 첫발을 내딪었다!");
        }

        // 이름 입력 화면
        public void DisplayCreateCharacterName()
        {
            Console.WriteLine("\n이름을 입력 해 주십시오.");
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

            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 5 < raceList.Count)
                Console.WriteLine($"         다음 [+]");
            else
                Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        // 마을 화면 관련 UI 함수
        public void DisplayVillageMenu()
        {
            Console.WriteLine("[1] 상  점: 래비츠 인더스트리");
            Console.WriteLine("[2] 보관함: 랫츠 보이드 오프너");
            Console.WriteLine("[3] 퀘스트: 도기독스 알선소");
            Console.WriteLine("[4] 던  전: 원정 고양이 협회");
            Console.WriteLine("[0] 나가기: 세어가는 양 여관");
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
        public void DisplayShopBuyList(int _page)
        {
            List<Item> storeList = GameManager.Instance().GetStoreList();

            for (int i = 0 * _page; i < 9; i++)
            {
                Console.WriteLine("[하얀 토끼 점장]: 지금 팔고 있는 물건은 이러하다오.");
                Console.WriteLine("                  재고를 걱정할 필요는 없소. 노동이란 그 끝이 없음이니.\n");

                if ((_page * 9) + i >= storeList.Count)
                    break;

                Console.Write($"[{i + 1}] {storeList[(_page * 9) + i].name} <{storeList[(_page * 9) + i].value}#>");
                Console.CursorLeft = 35;
                Console.WriteLine($"| {storeList[(_page * 9) + i].desc}");
            }


            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 9 < storeList.Count)
                Console.WriteLine($"         다음 [+]");
            else
                Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        // 구매 완료 시 표시.
        public void DisplayShoBuyText(Item _itemBought)
        {
            Console.WriteLine($"{_itemBought.name}을 {_itemBought.value}에 구매하셨습니다.");
        }

        // 상점 - 판매하기
        public void DisplayShopSellScreen(Player _player, int _page)
        {
            List<Item> invenList = _player.GetPlayerInvenList();

            Console.WriteLine("[하얀 토끼 점장]: 호오. 노동의 결과는 항상 그 가치를 인정받아야 함이라오.");
            Console.WriteLine("                  보여주시오. 원가 까지는 아니더라도 값을 좀 쳐 주겠소.\n");

            for (int i = 0 * _page; i < 9; i++)
            {
                if ((_page * 9) + i >= invenList.Count)
                    break;

                Console.Write($"[{i + 1}] {invenList[(_page * 9) + i].name} <{invenList[(_page * 9) + i].value}#>");
                Console.CursorLeft = 35;
                Console.WriteLine($"| {invenList[(_page * 9) + i].desc}");
            }

            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 9 < invenList.Count)
                Console.WriteLine($"         다음 [+]");
            else
                Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        // 판매 완료 시 표시.
        public void DisplayShopSellText(Item _itemBought)
        {
            Console.WriteLine($"{_itemBought.name}을 {(int)(_itemBought.value * 0.85f)}에 판매하셨습니다.");
        }

        // 보관함
        public void DisplayInventory(Player _player, int _page)
        {
            List<Item> invenList = _player.GetPlayerInvenList();

            Console.WriteLine("[단안경 신사 쥐돌이]: 귀하의 보관함을 열어드리지요. 저희가 모아놓은 귀하의 물건들입니다.");
            Console.WriteLine("                      아. 돈은 됬고, 다음에도 잘, 부탁드리도록 하겠습니다.");
            Console.WriteLine("                      뒤따라 가면서 얻는 부산물이 꽤 많거든요. 후후훗.\n");

            for (int i = 0 * _page; i < 9; i++)
            {
                if ((_page * 9) + i >= invenList.Count)
                    break;

                Console.Write($"[{i + 1}] {invenList[(_page * 9) + i].name}");
                Console.CursorLeft = 25;
                Console.WriteLine($"| {invenList[(_page * 9) + i].desc}");
            }

            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 9 < invenList.Count)
                Console.WriteLine($"         다음 [+]");
            else
                Console.WriteLine();

            Console.WriteLine("[0] 나가기");
        }

        // 퀘스트[일시 보류 - Quest 관련 항목이 아직 없음.]


        // 던전 진입 UI
        public void DisplayDungeonList(int _page)
        {
            List<Map> mapList = ObjectManager.Instance().GetMapList();

            Console.WriteLine("[치즈 고양이 소년]: 어서와랴옹! 고양이들이 보증하는 던전 원정 협회댜옹!");
            Console.WriteLine("                    지금 가지고 있는 정보는 여기 있댜옹. 어디를 들어갈거냐옹?");
            Console.WriteLine("                    아, 들어가면 살아나오는 건 본인 책임이댜옹. 신중하게 결정하랴옹.\n");

            // 던전 목록 출력.
            for (int i = 0 * _page; i < 9; i++)
            {
                if ((_page * 9) + i >= mapList.Count)
                    break;

                Console.Write($"[{i + 1}] {mapList[(_page * 9) + i].mapInfo.name} [Lv.{mapList[(_page * 9) + i].mapInfo.levelLimit} / 전투: {mapList[(_page * 9) + i].mapInfo.floor}]");
                Console.CursorLeft = 40;
                Console.WriteLine($"| {mapList[(_page * 9) + i].mapInfo.desc}");
            }

            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 9 < mapList.Count)
                Console.WriteLine($"         다음 [+]");
            else
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
        // 플레이어 정보 출력 함수
        public void DisplayPlayerInfoDungeon(Player _player)
        {
            PlayerInfo playerInfo = _player.GetInfo();
            int logFrameBottom = Console.WindowHeight - 3;
            int logFrameRight = Console.WindowWidth - 1;
            List<string> tempStringList = new List<string>();

            tempStringList.Add(String.Format(playerInfo.name + $"     Lv.{playerInfo.level}" + $"[{playerInfo.exp / (float)playerInfo.maxExp: 2N, 5}]"));
            tempStringList.Add(String.Format($"HP {playerInfo.hp,4} / {playerInfo.maxHp,-4}   AP {playerInfo.actionPoint, 2}")); // AP가 플레이어에 없어, 임시로 PlayerInfo의 값을 넣음.
            tempStringList.Add(String.Format($"공격력: {playerInfo.attack,-3}"));
            tempStringList.Add(String.Format($"방어력: {playerInfo.defence,-3}"));
            tempStringList.Add(String.Format($"행동력: {playerInfo.actionPoint,-3}"));
            tempStringList.Add(String.Format(""));

            Item playerWeapon = ObjectManager.Instance().GetItem(playerInfo.weaponId);
            for (int i = 0; i < playerWeapon.skill.Count; i++)
            {
                Skill targetSkill = ObjectManager.Instance().GetSkill(playerWeapon.skill[i]);
                tempStringList.Add(String.Format($"{$"[{i + 1}]{targetSkill.name}[AP {targetSkill.cost}]", -40}"));
            }
            // 스킬 관련 요소가 미완이라서 일단 무기 스킬 만 넣음.

            for(int i = tempStringList.Count-1; i>= 0; i--)
            {
                // UTF-8일 경우 한글은 한 자에 3Byte로 구성된다. 이를 참고하여 수식을 구성했다.
                // [p.s. 본 프로젝트와는 관계 없지만, euc-kr일 경우에는 한글은 한 자에 2Byte로 구성된다.]
                int wordLengthByte = Encoding.Default.GetByteCount(tempStringList[i]);
                Console.SetCursorPosition(logFrameRight - (((wordLengthByte - tempStringList[i].Length) / 2) + tempStringList[i].Length + 1), logFrameBottom - i);
            }

            tempStringList.Add(String.Format("[0] 턴 넘기기"));
        }

        // 던전 정보 출력 함수
        public void DisplayDungeonInfo(Map _map, int _floor)
        {
            int frameLeft = Console.WindowWidth - 40;
            Console.SetCursorPosition(frameLeft, 0);
            Console.Write($"{_map.mapInfo.name}");
            
            Console.SetCursorPosition(frameLeft, 1);
            Console.Write($"[{_floor} / {_map.mapInfo.floor}]");
        }

        // 적 정보 출력 함수
        public void DisplayEnemyInfo(Monster _monster)
        {
            int frameTop = 5;
            int frameLeft = Console.WindowWidth - 40;

            Console.SetCursorPosition(frameLeft, frameTop);
            Console.Write($"{_monster.GetInfo().name} [{_monster.GetInfo().level}]");

            Console.SetCursorPosition(frameLeft, frameTop + 1);
            Console.Write($"HP {_monster.GetInfo().hp,  4} / {_monster.GetInfo().maxHp, -4}");
        }

        // 현재 턴 정보 출력 함수
        public void DisplayCurrntTurn(bool _isPlayerTurn)
        {
            int playerInfoTop = 8;
            int monsterInfoBottom = 6;
            int frameHeight = Console.WindowHeight - 3;
            int turnInfoMiddle = ((frameHeight - playerInfoTop - monsterInfoBottom) / 2) + monsterInfoBottom;

            int frameRight = Console.WindowWidth - 1;

            Console.SetCursorPosition(frameRight - 26, turnInfoMiddle);
            Console.Write("Current Turn");

            if (_isPlayerTurn)
            {
                Console.SetCursorPosition(frameRight - 28, turnInfoMiddle + 1);
                Console.Write("↓ Now Your Turn");
            }

            else
            {
                Console.SetCursorPosition(frameRight - 30, turnInfoMiddle - 1);
                Console.Write("↑ Now Enemy's Turn");

                Console.SetCursorPosition(frameRight - 34, turnInfoMiddle + 1);
                Console.Write("Press Any Button to Progress");
            }
        }

        // 로그 출력 함수
        public void DisplayLog()
        {
            int logFrameHeight = Console.WindowHeight - 3;

            for(int i = Math.Max(0, logList.Count - logFrameHeight); i < logList.Count; i++)
                Console.WriteLine(logList[i]);
        }

        // 로그 생성 함수
        public void CreateLogAppearance(Monster _monster)
        {
            logList.Add($"[{_monster.GetInfo().name}]이 출현하였다!");
            logList.Add("");
        }

        public void CreateLogBattle(Player _player, Monster _monster, Skill _skill, int _power, int _skillType)
        {
            if (GameManager.Instance().GetTurn())
            {
                logList.Add($"[{_player.GetType().Name}]은(는) [{_skill.name}]을(를) 사용했다!");
                if(_skillType == 0)
                    logList.Add($"[{_monster.GetType().Name}]은(는) {_power}의 피해를 받았다!");
                else if( _skillType == 1)
                    logList.Add($"[{_player.GetType().Name}]은(는) {_skill.name}만큼 회복했다!");
            }

            else
            {
                logList.Add($"[{_monster.GetType().Name}]은(는) [{_skill.name}]을(를) 사용했다!");
                if (_skillType == 0)
                    logList.Add($"[{_player.GetType().Name}]은(는) {_power}의 피해를 받았다!");
                else if (_skillType == 1)
                    logList.Add($"[{_monster.GetType().Name}]은(는) {_skill.name}만큼 회복했다!");
            }

            logList.Add("");
        }

        // 승리 로그 생성 함수
        public void CreateLogVictory(Monster _monster, int _itemId, int reward)
        {
            logList.Add($"[{_monster.GetType().Name}]과의 전투에서 승리하였다!");
            
            if (_itemId >= 0)
                logList.Add($"[{ObjectManager.Instance().GetItem(_itemId)}]을(를) 습득하였다.");
            
            logList.Add($"돈과 경험치를 {reward}만큼 습득하였다!");

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
        //-------------------------------------------------
        private List<string> logList = new List<string>();

        static UIManager? instance;
    }
}