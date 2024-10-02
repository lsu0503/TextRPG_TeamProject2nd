using System.Runtime.InteropServices;
using TextRPG_TeamProject2nd.Manager;

namespace TextRPG_TeamProject2nd
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            
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
