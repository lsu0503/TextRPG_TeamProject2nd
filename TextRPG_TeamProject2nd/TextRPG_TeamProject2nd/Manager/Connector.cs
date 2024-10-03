

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TextRPG_TeamProject2nd.Manager
{
    internal class Connector
    {
        static public Connector Instance()
        {
            if(instance == null)
                instance = new Connector();
            return instance;
        }
        public bool HostStart(string _ip)//기다리는중
        {
            isHost = true;
            IPAddress ipAdress = IPAddress.Parse(_ip);
            server = new TcpListener(ipAdress, port);
            server.Start();
            client = server.AcceptTcpClient();
            Buffer = client.GetStream();
            return true;
        }
        public bool JoinStart(string _ip)
        {
            client = new TcpClient(_ip, port);
            Buffer = client.GetStream();
            return true;
        }

        public void Send(string _name, int _Itemid, string _message)//연결중
        {
            StringBuilder SendingStr = new StringBuilder();
            SendingStr.Append(_name + ",");
            SendingStr.Append(_Itemid + ",");
            SendingStr.Append(_message + ",");
            Buffer.Write(Encoding.UTF8.GetBytes(SendingStr.ToString()));       
        }
        public string[] Recv()
        {
            byte[] buffer = new byte[1024];
            int size = Buffer.Read(buffer, 0, buffer.Length);
            string recv = Encoding.UTF8.GetString(buffer, 0, size);
            return packet = recv.Split(",");
        }
        public void Close()
        {
            if (server != null)
                server.Stop();

            if(client != null)
                client.Close();

            if(Buffer != null)
                Buffer.Close();

            isHost = false;
        }

        public bool isHost = false;
        int port = 7474;
        string[] packet = new string[3];
        
        TcpListener? server = null;
        TcpClient? client = null;
        NetworkStream? Buffer = null;
        static Connector? instance = null;
    }
}
