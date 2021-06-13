using DBSync.WPFClient.ViewModels;
using System.Windows.Controls;

namespace DBSync.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for ClientCRUDView.xaml
    /// </summary>
    public partial class ClientCRUDView : UserControl
    {
        public ClientCRUDView(ClientDBViewModel vm)
        {
            InitializeComponent();
            client.LoadingRow += Client_LoadingRow;
            DataContext = vm;
        }
        private void Client_LoadingRow(object sender, DataGridRowEventArgs e) => e.Row.Header = $"  {(e.Row.GetIndex() + 1)}";

        
    }
}
