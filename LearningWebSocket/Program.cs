using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Web.UI;

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

                Byte[] bytes = new Byte[client.Available]; // Receves the message

                stream.Read(bytes, 0, bytes.Length); // Read the whole message in bytes

                String data = Encoding.UTF8.GetString(bytes); // Translate bytes to string

                if(new Regex("^GET").IsMatch(data)) {
                    const string newLine = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker

                    Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + newLine
                        + "Connection: Upgrade" + newLine
                        + "Upgrade: websocket" + newLine
                        + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                            System.Security.Cryptography.SHA1.Create().ComputeHash(
                                Encoding.UTF8.GetBytes(
                                    new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                                )
                            )
                        ) + newLine + newLine
                    );

                    stream.Write(response, 0, response.Length);
                }
                else {

                }
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