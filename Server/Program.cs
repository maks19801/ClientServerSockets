using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, 1024);

            socket.Bind(endPoint);
            socket.Listen(10);
            Console.WriteLine("Server started...");
            try
            {
                while (true)
                {
                    Console.WriteLine("Wait for connection through the port {0}", endPoint);

                    Socket handler = socket.Accept();

                    string data = null;

                    byte[] bytes = new byte[1024];
                    int bytesReceived = handler.Receive(bytes);

                    data += Encoding.UTF8.GetString(bytes, 0, bytesReceived);
                    Console.WriteLine("Received from client: " + data);

                    Console.WriteLine("Input the message: ");
                    string message = Console.ReadLine();

                    handler.Send(Encoding.UTF8.GetBytes(message));
                    if(data == "Bye" || message == "Bye")
                    {
                        Console.WriteLine("Server has ended connection...");
                        break;
                    }

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
