using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace TeamProjectBin
{
    internal class UIManager
    {

 
       

        //public void getSelect() // 미작동
        //{
        //    while (true) 
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"UI 창을 보시려면 1~4까지의 번호중 하나를 입력해 주세요\n");
        //        Console.WriteLine();

        //        if (int.TryParse(Console.ReadLine(), out int selectInput))

        //        {
        //            switch (selectInput)
        //            {
        //                case 1:
        //                    StartLogo();
        //                    break;
        //                case 2:
        //                    StartMenu();
        //                    break;
        //                case 3:
        //                    Information();
        //                    break;
        //                case 4:
        //                    Inventory();
        //                    break;
        //            }
        //            if (selectInput >= 1 && selectInput <= 4) 
        //                break;
        //        }
        //    }
        //}
        //public void BackSelect()   
        //{
        //    while (true)
        //    {
        //        if (int.TryParse(Console.ReadLine(), out int userInput))
        //        {
        //            if (userInput == 0)
        //            {
        //                getSelect();
        //                break;
        //            }
        //            else
        //            {
        //                Console.WriteLine("잘못된 입력입니다.  1~4 사이의 숫자를 입력하세요.");
        //            }
        //        }
        //    }
        //}


        static void StartLogo() // 게임 스타트 화면 세로 40 가로 160
        {
            Console.WriteLine("I===============================================================================================================================================================I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                 게임 타이틀                                                                                   I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I===============================================================================================================================================================I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                     화면                                                                                      I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I                                                                                                                                                               I");
            Console.WriteLine("I===============================================================================================================================================================I");
            Console.ReadKey(); // 아무 키나 입력 받음


        }
           

            static void StartMenu()
            {

                Console.WriteLine("I===============================================================================================================================================================I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                     현재 화면 이름                                                             I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I================================================================================================================================I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I          캐릭터 정보         I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                           화면 테스트                                                          I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I===============================================================================================================================================================I");
                Console.ReadKey(); // 아무 키나 입력 받음

            }

            static void Information()
            {

                Console.WriteLine("I===============================================================================================================================================================I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                     현재 화면 이름                                                             I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I================================================================================================================================I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I          캐릭터 정보         I");
                Console.WriteLine("I               아이템 설명               I                                        아이템 설명                                   I                              I");
                Console.WriteLine("I                   or                    I                                             or                                       I                              I");
                Console.WriteLine("I               퀘스트 설명               I                                        퀘스트 설명                                   I                              I");
                Console.WriteLine("I                   or                    I                                             or                                       I                              I");
                Console.WriteLine("I                던전 설명                I                                         던전 설명                                    I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I===============================================================================================================================================================I");
                Console.ReadKey(); // 아무 키나 입력 받음

            }

            static void Inventory()
            {

                Console.WriteLine("I===============================================================================================================================================================I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                     현재 화면 이름                                                             I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I================================================================================================================================I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I                                                  아이템 카테고리 목록                                                          I                              I");
                Console.WriteLine("I                                                                                                                                I                              I");
                Console.WriteLine("I================================================================================================================================I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I          캐릭터 정보         I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I           보관함 아이템 목록            I                                        아이템 설명                                   I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I                                         I                                                                                      I                              I");
                Console.WriteLine("I===============================================================================================================================================================I");
                Console.ReadKey(); // 아무 키나 입력 받음

            }

    }
}


