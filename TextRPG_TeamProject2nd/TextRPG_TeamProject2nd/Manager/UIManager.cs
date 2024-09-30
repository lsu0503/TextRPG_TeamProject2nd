using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Object;
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
        /*
        static void DisplayPlayerInfoRight(Player _player)
        {
            List<string> tempStringList = new List<string>();
            int windowWidth = Console.WindowWidth;
            int wordLengthByte;
            int xPos = 0;

            tempStringList.Add(String.Format(_player.name + $"     Lv.{_player.level}" + $"[{_player.expCur / (float)_player.expMax: 2N, 5}]"));
            tempStringList.Add(String.Format($"HP: {_player.hpCur, 4} / {_player.hpMax, -4}"));
            tempStringList.Add(String.Format($"공격력: {_player.attack, -3}"));
            tempStringList.Add(String.Format($"방어력: {_player.defence, -3}"));
            tempStringList.Add(String.Format($"행동력: {_player.ActionPoint, -3}"));
            tempStringList.Add("");
            tempStringList.Add($"무  기: {ObjectManager.Instance().GetItem(_player.weapon).name}");
            tempStringList.Add($"방어구: {ObjectManager.Instance().GetItem(_player.armor).name}");

            for (int i = 0; i < tempStringList.Count; i++)
            {
                // UTF-8일 경우 한글은 한 자에 3Byte로 구성된다. 이를 참고하여 수식을 구성했다.
                // euc-kr일 경우에는 한글은 한 자에 2Byte로 구성된다.
                wordLengthByte = Encoding.Default.GetByteCount(tempStringList[i]);
                Console.SetCursorPosition(windowWidth - (((wordLengthByte - tempStringList[i].Length) / 2) + tempStringList[i].Length + 1), i);
                Console.Write(tempStringList[i]);
            }
        }
        */

        // 타이틀 화면 관련 UI 함수
        public void DisplayStartMenu()
        {
            Console.WriteLine("[1] 새로 시작");
            Console.WriteLine("[2] 불러오기");
            Console.WriteLine("[0] 게임 종료");
        }

        // 세이브 슬롯 확인 함수.
        public void DisplaySaveSlot()
        {
            // 세이브 시스템 제작 이후 작성하자.
        }

        // 마을 화면 관련 UI 함수
        public void DisplayVillageMenu()
        {
            Console.WriteLine("[1] 상  점: 래비츠 인더스트리");
            Console.WriteLine("[2] 보관함: 랫츠 보이드 오프너");
            Console.WriteLine("[3] 퀘스트: 도기독스 알선소");
            Console.WriteLine("[4] 던  전: 원정 고양이 협회");
            Console.WriteLine("[0] 나가기: 양 한마리 여관");
        }

        // 상점 관련 UI 함수.
        public void DisplayShopEntrance()
        {
            Console.WriteLine("[1] 구매하기");
            Console.WriteLine("[2] 판매하기");
            Console.WriteLine("[0] 나 가 기");
        }

        // 상점 - 구매하기
        public void DisplayShopBuyList(int _page)
        {
            for (int i = 0 * _page; i < 9; i++)
            {
                if ((_page * 9) + i >= GameManager.Instance().storeList.Count)
                    break;

                Console.Write($"[{i + 1}] {GameManager.Instance().storeList[(_page * 9) + i].name} <{GameManager.Instance().storeList[(_page * 9) + i].value}#>");
                Console.CursorLeft = 50;
                Console.WriteLine($" | {GameManager.Instance().storeList[(_page * 9) + i].desc}");
            }


            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 9 < GameManager.Instance().storeList.Count)
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
        public void DisplayShopSellScreen(int _page)
        {
            for (int i = 0 * _page; i < 9; i++)
            {
                if ((_page * 9) + i >= GameManager.Instance().storageList.Count)
                    break;

                Console.WriteLine($"[{i + 1}] {GameManager.Instance().storageList[(_page * 9) + i].name}"
                                + $"<{(int)(GameManager.Instance().storageList[(_page * 9) + i].value * 0.85f)}#>"
                                + $" | {GameManager.Instance().storageList[(_page * 9) + i].desc}");
            }

            if (_page > 0)
                Console.Write($"[-] 이전         ");
            else
                Console.Write($"                 ");

            Console.Write($"[ {_page} ]");

            if ((_page + 1) * 9 < GameManager.Instance().storeList.Count)
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



        public void PositionCursorToInput()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

        static void StartMenu()
        {

        }

        static void Inventory()
        {

        }
    }
}