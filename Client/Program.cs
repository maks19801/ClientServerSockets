using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            string data = null;
            Console.WriteLine("Please, input server ip address...");
            string ipAddress = Console.ReadLine();
            IPAddress ip = IPAddress.Parse(ipAddress);
            Console.WriteLine("Please, input port number");
            int port = Int32.Parse(Console.ReadLine());
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            try
            {
                while (true)
                {
                    Socket client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.IP);
                    client.Connect(endPoint);
                    Console.WriteLine("Input the message: ");
                    string message = Console.ReadLine();

                    client.Send(Encoding.UTF8.GetBytes(message));

                    int bytesReceived = client.Receive(bytes);
                    data = Encoding.UTF8.GetString(bytes, 0, bytesReceived);
                    Console.WriteLine("Received from server: " + data);
                    if (data == "Bye" || message == "Bye")
                    {
                        client.Send(Encoding.UTF8.GetBytes(message));
                        Console.WriteLine("Client has ended connection...");
                        break;
                    }

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
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
