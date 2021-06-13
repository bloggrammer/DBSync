using DBSync.WPFClient.ViewModels;
using System.Windows.Controls;

namespace DBSync.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for ServerCRUDView.xaml
    /// </summary>
    public partial class ServerCRUDView : UserControl
    {
        public ServerCRUDView(ServerDBViewModel vm)
        {
            InitializeComponent();
            server.LoadingRow += Server_LoadingRow;
            DataContext = vm;
        }

        private void Server_LoadingRow(object sender, DataGridRowEventArgs e) => e.Row.Header = $"  {(e.Row.GetIndex() + 1)}";
    }
}
