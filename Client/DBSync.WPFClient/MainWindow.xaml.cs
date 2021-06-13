using DBSync.DAL.Repositories;
using DBSync.WPFClient.ViewModels;
using DBSync.WPFClient.Views;
using System.Windows;
using static System.Environment;
using DBSync.DAL.Configurations;

namespace DBSync.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var connectionString = $"Data Source={CurrentDirectory}\\SyncedDB.db;";
           var sessionFactory = new SessionFactory(connectionString, DatabaseType.SQLite);
           var unitOfWork = new UnitOfWork(sessionFactory);

            var clientVM = new ClientDBViewModel(unitOfWork, connectionString);
            var serverVM = new ServerDBViewModel(clientVM);
            serverUI.Content= new ServerCRUDView(serverVM);           
            clientUI.Content= new ClientCRUDView(clientVM);
        }
    }
}
