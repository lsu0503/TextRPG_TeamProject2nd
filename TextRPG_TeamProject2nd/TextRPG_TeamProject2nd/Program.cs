using TextRPG_TeamProject2nd.Manager;

namespace TextRPG_TeamProject2nd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Init()
            //FileManger.Instance().Init();
            GameManager.Instance().Init();
            ObjectManager.Instance().Init();

            //Update()
            while (true)
            {
                GameManager.Instance().Update();
            }

            //Render() OR Etc..

        }
    }
}
