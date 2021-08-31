using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace ConsoleServer
{
    public class ClientObject
    {
        public Socket client;
        public ClientObject(Socket tcpClient)
        {
            client = tcpClient;
        }

        public void Process()
        {

            try
            {


                byte[] data = new byte[64]; // буфер для получаемых данных
                while (true)
                {
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = client.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (client.Available > 0);

                    string message = builder.ToString();
                    Console.WriteLine(message);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
    class Program
    {
        const int port = 8888;

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                Console.WriteLine("Waiting for connection...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    ClientObject clientObject = new ClientObject(handler);

                    // создаем новый поток для обслуживания нового клиента
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
