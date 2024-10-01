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
        // 상단 화면 타이틀을 그리는 함수.
        static void DisplayTitle(string _titleText)
        {
            Console.SetCursorPosition(0, 0);
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
        static void DisplayPlayerInfoRight(Player _player)
        {
            PlayerInfo playerinfo = _player.GetInfo();
            List<string> tempStringList = new List<string>();
            int windowWidth = Console.WindowWidth;
            int wordLengthByte;
            int xPos = 0;

            tempStringList.Add(String.Format(playerinfo.name + $"     Lv.{playerinfo.level}" + $"[{playerinfo.exp / (float)playerinfo.maxExp: 2N, 5}]"));
            tempStringList.Add(String.Format($"HP: {playerinfo.hp, 4} / {playerinfo.maxHp, -4}"));
            tempStringList.Add(String.Format($"공격력: {playerinfo.attack, -3}"));
            tempStringList.Add(String.Format($"방어력: {playerinfo.defence, -3}"));
            tempStringList.Add(String.Format($"행동력: {playerinfo.actionPoint, -3}"));
            tempStringList.Add("");
            tempStringList.Add($"무  기: {ObjectManager.Instance().GetItem(playerinfo.weaponId).name}");
            tempStringList.Add($"방어구: {ObjectManager.Instance().GetItem(playerinfo.armorId).name}");

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

        // 종족 선택 화면 [일시 보류: ObjectManager에 races를 받아오는 함수 필요.]


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
            Console.WriteLine("[치즈 고양이 소년]: 어서와랴옹! 고양이들이 보증하는 던전 원정 협회댜옹!");
            Console.WriteLine("                    지금 가지고 있는 정보는 여기 있댜옹. 어디를 들어갈거냐옹?");
            Console.WriteLine("                    아, 들어가면 살아나오는 건 본인 책임이댜옹. 신중하게 결정하랴옹.\n");

            // 던전 목록 출력. [일시 보류 - Object Manager에 Maps를 받아오는 함수 필요.]
        }

        //게임 종료 관련 UI(마을에서)
        public void DisplayExitGameVillage()
        {
            Console.WriteLine("[뜨개질 중인 양 할머니]: 양 세는 여관에 어서오거라. 오늘도 수고 많았다.");
            Console.WriteLine("                         마음 높고 푹 쉬려무나. 숙박비는 걱정 말고. 다른 곳에서 지원이 들어온단다.");
            Console.WriteLine("                         가끔 기한 없이 잠들기도 하지만, 모두들 마음에 쏙 들어한단다.");
            Console.WriteLine("                         뭐, 이전에 있었던 일이다만, 엄청 커다란 날치에 관한 이야기인데...");
            Console.WriteLine("<말이 끝날 기미가 안보인다. 빨리 용무를 마쳐야 할 거 같다.>\n");

            Console.WriteLine();
            Console.WriteLine("종료하시겠습니까? [Y - 예 / N - 아니오]");
        }
        
        // 던전 내부 관련 UI

    }
}