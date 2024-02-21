using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace JocVaixells
{
    public class StartServer
    {
        private Socket listener;
        private CancellationTokenSource cancellationTokenSource;

        public event EventHandler<string> MessageReceived;
        public event EventHandler<string> DisplayVisible;

        public Cell cell;
        public string IP;
        public int port;

        public void Pos(Cell cell)
        {
            this.cell = cell;
        }

        public void Start()
        {
            IPHostEntry host = Dns.GetHostEntry(IP);
            IPAddress ip = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            Thread.Sleep(10000);
            try
            {
                Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(endPoint);
                listener.Listen();
                while (true)
                {
                    Console.WriteLine("Waiting for connection");
                    Socket clientSocket = listener.Accept();

                    byte[] bytes = new byte[4096];
                    string data = "";
                    while (true)
                    {
                        int numBytes = clientSocket.Receive(bytes);
                        data += Encoding.UTF8.GetString(bytes, 0, numBytes);
                        if (data.IndexOf("<EOF>") > -1)
                        {
                            OnMessageReceived(data);
                            break;
                        }
                    }

                    Console.WriteLine("Text received: {0}", data);

                    string[] dades = data.Split(",");
                    byte[] message;

                    if (dades[0] == cell.fila.ToString() &&
                        dades[1] == cell.columna.ToString())
                    {
                        message = Encoding.UTF8.GetBytes("Tocat");
                        BackgroudEfect("Tocat");
                    }
                    else
                    {
                        message = Encoding.UTF8.GetBytes("Aigua");
                        BackgroudEfect("Aigua");
                    }
                    
                    clientSocket.Send(message);
                    data = "";

                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void StopServer()
        {
            cancellationTokenSource?.Cancel();

            listener?.Shutdown(SocketShutdown.Both);
            listener?.Close();
        }

        private void OnMessageReceived(string message)
        {
            MessageReceived?.Invoke(this, message);
        }

        private void BackgroudEfect(string message)
        {
            DisplayVisible?.Invoke(this, message);
        }
    }
}
