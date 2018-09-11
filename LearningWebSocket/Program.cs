using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace LearningWebSocket {
    public class Program {

        public static void Main() {
            // Defining data
            string ipAdress = "127.0.0.1";
            int portNumber = 80;

            TcpListener server = new TcpListener(IPAddress.Parse(ipAdress), portNumber);
            server.Start();
            Console.WriteLine($"Server started on {ipAdress}{Environment.NewLine}Waiting for connection...");

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();

            while(true) { // Endless loop to keep receiving changes in stream
                while(!stream.DataAvailable);

                Byte[] bytes = new Byte[client.Available];

                stream.Read(bytes, 0, bytes.Length); // Read the whole message
            }

            //Socket serverSocket = new Socket(AddressFamily.Unspecified, SocketType.Stream, ProtocolType.IP); // hmm, what
        }

        public void Write(byte[] buffer, int offset, int size) {

        }

        public void Read(byte[] buffer, int offset, int size) {

        }
    }
}
// Following a tutorial: https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server 