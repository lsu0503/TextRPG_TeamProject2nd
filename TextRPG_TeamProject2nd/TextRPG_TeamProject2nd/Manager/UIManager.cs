using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace TeamProjectBin
{
    internal class UIManager
    {

 
        static void Main(string[] args) // 메인 함수
        {
            Console.SetWindowSize(161, 41);

            int left = 1;
            int top = 1;
            Console.SetCursorPosition(0, 0);

            StartLogo();
            StartMenu();
            Information();
            Inventory();
        }

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


