using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NicoDanmuClient
{
    class DanmuManager
    {
        //Singleton
        private static DanmuManager instance = new DanmuManager();
        private DanmuManager() { }
        public static DanmuManager getInstance()
        {
            if (instance == null)
            {
                instance = new DanmuManager();
            }
            return instance;
        }

        Form1 f;
        private string host;
        private const int port = 8877;
        Socket socket;
        System.Threading.Timer get_Data_Timer;
        System.Threading.Timer heart_Beat_Timer;
        byte[] heart_Beat_Byte = System.Text.Encoding.UTF8.GetBytes("ok\r");

        public void setWindow(Form1 f)
        {
            this.f = f;
        }

        public bool connect(string h)
        {
            Console.WriteLine("Ready to connect");
            this.host = h;
            //config
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            //create
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Connecting...");
            //connect
            try
            {
                socket.Connect(ipe);
                Console.WriteLine("connect success");
                get_Data_Timer = new System.Threading.Timer(getDanmu, null, 0, 2000);
                //heart_Beat_Timer = new System.Threading.Timer(heart_Beat, null, 0, 4000);
                return true;
            }
            catch
            {
                Console.WriteLine("connect fail");
                return false;
            }
        }

        private void getDanmu(object o)
        {
            byte[] recvBytes = new byte[1024];
            int lengthOfRecv;
            //Console.WriteLine("等待接收");
            try
            {
                //is_Socket_waiting = true;
                lengthOfRecv = socket.Receive(recvBytes, recvBytes.Length, 0);
                //is_Socket_waiting = false;
                f.loadDanmu(Encoding.UTF8.GetString(recvBytes, 0, lengthOfRecv));
                Console.WriteLine("get data: "+ Encoding.UTF8.GetString(recvBytes, 0, lengthOfRecv));        
            }
            catch
            {
                Console.WriteLine("lengthOfRecv = c.Receive(recvBytes, recvBytes.Length, 0);//从服务器端接受返回信息 ");
            }
        }

        public void closeSocket()
        {
            if(socket!=null)
            {
                socket.Send(Encoding.UTF8.GetBytes("close"));
                socket.Close();
                socket = null;
            }
        }

        private void heart_Beat(object o)
        {
            if(socket != null)
                socket.Send(heart_Beat_Byte);
        }
    }
}
