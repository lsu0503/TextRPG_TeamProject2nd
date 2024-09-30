using TextRPG_TeamProject2nd.Object;
using TextRPG_TeamProject2nd.Utils;
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
        END,
    }
    enum RACETYPE
    {
        KNIGHT,
        ARCHUR,
        MAGE,
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
            //TODO
            //파일 매니저에서 플레이어 정보를 로드해서 적재 해야합니다.
            //player = FileManger.Inst().Load();
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
                    SceneVIllage();
                    break;
                case SCENESTATE.FILED:
                    SceneFiled();
                    break;
                case SCENESTATE.STORE:
                    SceneStore();
                    break;
                case SCENESTATE.INVEN:
                    SceneStore();
                    break;
                case SCENESTATE.QUEST:
                    SceneOuest();
                    break;
                case SCENESTATE.END:
                    //SAVE()
                    //TODO
                    break;
            }
        }
        //---------------------------------------------------------------

        //맵
        private void SceneNone()
        {
            bool isComplete = false;
            while(!isComplete)
            {
                Console.WriteLine("이름을 입력하세요.");
                string name = InputLine();
                Console.Clear();
                Console.WriteLine("직업을 선택하세요.");
                //List<Race> races = FileManager.GetRace();
                Console.WriteLine("1.기사");
                Console.WriteLine("2.궁수");
                Console.WriteLine("3.마법사");
                RACETYPE type = (RACETYPE)(InputKey() - 1);
                Console.Clear();

                switch (type)
                {
                    case RACETYPE.KNIGHT:
                        //TODO
                        break;
                    case RACETYPE.ARCHUR:
                        //TODO
                        break;
                    case RACETYPE.MAGE:
                        //TODO
                        break;
                }

                Console.WriteLine("당신의 이름은 {0}, 직업은 {1}입니다.", name, type);
                Console.WriteLine("[0]:확정 [1]:초기화");
                isComplete = InputKey() == 0 ? true : false;
                Console.Clear();
            }

            ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneMain()
        {
            Console.Clear();
            Console.WriteLine("더미 메인화면 입니다.");
            Console.WriteLine("[0]:시작 [1]:종료");
            int input = InputKey();
            if(input == 0)
                ChangeScene(SCENESTATE.VILLAGE);
            else Environment.Exit(0);
        }
        private void SceneVIllage()
        {
            Console.Clear();
            Console.WriteLine("마을입니다.");
            Console.WriteLine("[0]:상점 [1]:퀘스트 [2]:보관함 [3]:던 전 [4]:저장 및 종료");
            int input = InputKey();
            if (input == 0)         ChangeScene(SCENESTATE.STORE);
            else if (input == 1)    ChangeScene(SCENESTATE.QUEST);
            else if (input == 2)    ChangeScene(SCENESTATE.STORE);
            else if (input == 3)    ChangeScene(SCENESTATE.FILED);
            else if (input == 4)    Environment.Exit(0); //SAVE
        }
        private void SceneFiled()
        {
            Console.Clear();
            Console.WriteLine("던전을 고르세요!");
            Console.WriteLine("[0]:마을로이동 [1]:더미던전");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);

            while(true)
            {
                
                break;
            }
        }
        private void SceneStore()
        {
            Console.Clear();
            Console.WriteLine("상점 공간입니다.");
            Console.WriteLine("[0]:마을로이동");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneInven()
        {
            Console.Clear();
            Console.WriteLine("아이템을 보관하고 장착하는 인벤토리 입니다.");
            Console.WriteLine("[0]:마을로이동");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
        }
        private void SceneOuest()
        {
            Console.Clear();
            Console.WriteLine("이곳에 퀘스트UI와 정보가 등록됩니다.");
            Console.WriteLine("[0]:마을로이동");
            int input = InputKey();
            if (input == 0) ChangeScene(SCENESTATE.VILLAGE);
        }

        //유틸
        private void ChangeScene(SCENESTATE type)
        {
            sceneState = type;
        }
        public Player GetCurrentPlayer() { return player; }
        
        //사용자 입력
        public int InputKey()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(">>");
            Console.ResetColor();
            int input = -1;
            
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">>");
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
        private Player? player = null;
        private SCENESTATE sceneState = SCENESTATE.MAIN;

    }
}
