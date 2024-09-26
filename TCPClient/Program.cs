using System;
using System.IO;
using System.Net.Sockets;

namespace EchoClientExpanded
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client");

            bool keepSending = true;

            //Moved the socket, stream, reader and writer before the readline
            //so it can reuse the same objects until the connection is closed

            TcpClient socket = new TcpClient("10.200.130.41", 7);

            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            while (keepSending)
            {
                string message = Console.ReadLine();

                //writes the message the user typed in the console to the server and appends a line break (cr lf)
                //notice the Line part of WriteLine
                writer.WriteLine(message);
                //Makes sure that the message isn't buffered somewhere, and actually send to the client
                //Always remember to flush after you
                writer.Flush();

                //Here it reads all data send until a line break (cr lf) is received
                //notice the Line part of the ReadLine
                string response = reader.ReadLine();

                //Writes the response it got from the server to the console
                //it should be the same as the line send, it the server is a simple Echo Server
                Console.WriteLine(response);

                //if the user types close (case in-sensitive) the clients sends the message to the server
                //and the sets the bool to false, so the loop stops
                if (message == "Stop")
                {
                    keepSending = false;
                }
            }

            //closes the connection, as it can only send one message before the server closes the connection
            socket.Close();
        }
    }
}
