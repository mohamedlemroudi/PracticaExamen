using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            string serverIP = txtServerIP.Text;
            int serverPort = Int32.Parse(txtServerPort.Text);
            
            string fila = txtFila.Text;
            string columna = txtColumna.Text;
            byte[] message = Encoding.UTF8.GetBytes($"{fila},{columna},<EOF>");

            IPHostEntry host = Dns.GetHostEntry(serverIP);
            IPAddress ip = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, serverPort);

            try
            {
                Socket socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                if (socket.RemoteEndPoint != null)
                {
                    Console.WriteLine("Socket client connected to: " + socket.RemoteEndPoint.ToString());
                    int byteSend = socket.Send(message);

                    byte[] messageReceived = new byte[byteSend];
                    int byteReceived = socket.Receive(messageReceived);

                    txtHistorico.Text += $"\nMessage from server: {Encoding.UTF8.GetString(messageReceived, 0, byteReceived)}";

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}