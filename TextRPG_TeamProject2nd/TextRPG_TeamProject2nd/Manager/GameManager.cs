using TextRPG_TeamProject2nd.Object;
namespace TextRPG_TeamProject2nd.Manager
{
    enum SceneState
    {
        NONE,
        MAIN,
        VILLAGE,
        FILED,
        STORE,
        INVEN,
        QUEST,
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
                case SceneState.NONE:
                    SceneNone();
                    break;
                case SceneState.MAIN:
                    SceneMain();
                    break;
                case SceneState.VILLAGE:
                    SceneVIllage();
                    break;
                case SceneState.FILED:
                    SceneFiled();
                    break;
                case SceneState.STORE:
                    SceneStore();
                    break;
                case SceneState.INVEN:
                    SceneStore();
                    break;
                case SceneState.QUEST:
                    SceneOuest();
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

            ChangeScene(SceneState.VILLAGE);
        }
        private void SceneMain()
        {

        }
        private void SceneVIllage()
        {
            Console.WriteLine("마을입니다.");
        }
        private void SceneFiled()
        {

        }
        private void SceneStore()
        {

        }
        private void SceneInven()
        {

        }
        private void SceneOuest()
        {

        }

        //유틸
        private void ChangeScene(SceneState type)
        {
            sceneState = type;
        }

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
        private SceneState sceneState = SceneState.NONE;

    }
}
