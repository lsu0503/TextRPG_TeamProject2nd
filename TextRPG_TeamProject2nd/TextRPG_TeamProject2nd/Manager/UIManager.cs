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
                wordLengthByte = Encoding.Default.GetByteCount(tempStringList[i]);
                Console.SetCursorPosition(windowWidth - (wordLengthByte + 1), i);
                Console.Write(tempStringList[i]);
            }
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


