using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using TextRPG_TeamProject2nd.Manager;
using TextRPG_TeamProject2nd.Object;

namespace TeamProjectBin
{
    internal class UIManager
    {
        static void DisplayTitle(string _titleText)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("===================================================================================================="); // 반각으로 100자.

            int xTurm = (100 - _titleText.Length) / 2; // 중앙정렬. 100자에서 글자 수를 뺸 다음 2로 나누기.
            Console.CursorLeft = xTurm;
            Console.WriteLine(_titleText);

            Console.WriteLine("====================================================================================================");
        }

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

        public void DisplayStartMenu()
        {
            Console.WriteLine("[1] 새로 시작");
            Console.WriteLine("[2] 불러오기");
            Console.WriteLine("[0] 게임 종료");
        }

        public void DisplaySaveSlot()
        {
            // 세이브 시스템 제작 이후 작성하자.
        }

        public void DisplayVillageMenu()
        {
            Console.WriteLine("[1] 상  점: 래비츠 인더스트리");
            Console.WriteLine("[2] 보관함: 랫츠 보이드 오프너");
            Console.WriteLine("[3] 퀘스트: 도기독스 알선소");
            Console.WriteLine("[4] 던  전: 원정 고양이 협회");
            Console.WriteLine("[0] 나가기: 양 한마리 여관");
        }

        public void DisplayShopEntrance()
        {
            Console.WriteLine("[1] 구매하기");
            Console.WriteLine("[2] 판매하기");
            Console.WriteLine("[0] 나 가 기");
        }

        public void DisplayShopMerchandise(int _index)
        {
            
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


