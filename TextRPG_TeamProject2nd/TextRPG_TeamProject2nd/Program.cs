using System.Runtime.InteropServices;
using TextRPG_TeamProject2nd.Manager;

namespace TextRPG_TeamProject2nd
{
    internal class Program
    {
       
        static void FirstStart()
        {
            Console.WriteLine("게임의 원할한 진행을 위해 전체화면으로 바꿔 주시고 진행을 부탁드립니다. 계속 : [Enter]..");
            Console.ReadLine();
            Console.Clear();
        }
        static void Main(string[] args)
        {

            FirstStart();
            //Init()
            FileManager.Instance().Init();
            ObjectManager.Instance().Init();
            GameManager.Instance().Init();

            //Update()
            while (true)
            {
                GameManager.Instance().Update();
            }

            //Render() OR Etc..

        }

    }
}
