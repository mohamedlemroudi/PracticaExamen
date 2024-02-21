using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JocVaixells
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cell cell;
        StartServer server = new StartServer();

        public MainWindow()
        {
            InitializeComponent();

            cell = mainTable.celdaTocat();
            this.mainTable.SetContentValor(cell.fila, cell.columna, "Tocat", true);

            // Suscribir el manejador de eventos MessageReceived
            server.MessageReceived += Server_MessageReceived;
            server.DisplayVisible += Change_DisplayVisible;
        }

        private void Change_DisplayVisible(object? sender, string e)
        {
            Dispatcher.Invoke(() =>
            {
                var colorChangeStoryboard = (Storyboard)FindResource("ColorChangeStoryboard");
                if (e == "Tocat")
                {
                    // Obtén la animación desde los recursos del control
                    colorChangeStoryboard = (Storyboard)FindResource("ColorChangeStoryboard2");
                }

                // Inicia la animación
                colorChangeStoryboard.Begin(displayVisible);
            });
        }

        private void Server_MessageReceived(object sender, string e)
        {
            Dispatcher.Invoke(() =>
            {
                txtConsole.AppendText(e + Environment.NewLine);
            });
        }

        private void btnStartServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnServer.Content.ToString() == "Start Server")
                {
                    btnServer.Content = "Stop Server";
                    server.port = Int32.Parse(txtPort.Text);
                    server.IP = txtIP.Text;
                    Task.Run(() =>
                    {
                        server.Pos(cell);
                        server.Start();
                    });
                }
                else
                {
                    btnServer.Content = "Start Server";
                    Task.Run(() =>
                    {
                        server.StopServer();
                    });
                }
            }
            catch 
            {
                MessageBox.Show("Falta algun camp");
            }
        }

        private void mainTable_SelectItemClick(object sender, Events.SelectUIElementEventArgs e)
        {
            MessageBox.Show("Main table click at position(" + e.Row + "," + e.Col + ")");
        }

        private void mainTable_SelectItemRightClick(object sender, Events.SelectUIElementEventArgs e)
        {
            this.mainTable.SetContentValor(cell.fila, cell.columna, "", false);
            cell.fila = e.Row;
            cell.columna = e.Col;
            this.mainTable.SetContentValor(cell.fila, cell.columna, "Tocat", true);
            Task.Run(() =>
            {
                server.Pos(cell);
            });
        }

        private void txtRows_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(txtRows.Text, out int rows))
            {
                mainTable.Rows = rows;
            }
        }

        private void txtCols_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (int.TryParse(txtCols.Text, out int cols))
            {
                mainTable.Cols = cols;
            }
        }
    }
}