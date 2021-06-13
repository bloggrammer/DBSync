using DBSync.Models;
using DBSync.Usecases;
using DBSync.Usecases.Base;
using DBSync.WPFClient.Notifications;
using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;
using MVVM.LTE.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DBSync.WPFClient.ViewModels
{
	public class ClientDBViewModel: ViewModelBase
    {
		public ClientDBViewModel(IUnitOfWork unitOfWork, string connectionString)
		{
			_unitOfWork = unitOfWork;
			CreateOrUpdateUserCommand = new SimpleCommand(CreateOrUpdateUserAction);
			EditUserCommand = new SimpleCommand(EditUserAction);
			DeleteUserCommand = new SimpleCommand(DelateUserAction);
			BDSyncCommand = new SimpleCommand(UpdateServerDB);
			_connectionString = connectionString;
			FetchUsers();
		}

		private void Server_SyncClientDBEvent(object sender)
		{
			if (SynchronizeAsync().Result)
				FetchUsers();
		}

		private void FetchUsers()
		{
			var handler = new FetchAllUsersHandler(_unitOfWork);
			var users = handler.Handle();
			users.ToList().ForEach(x => Users.Add(x));
		}

		private void CreateOrUpdateUserAction()
		{
			if (_actionType == ActionType.Update)
				UpdateUserAction();
			else
				CreateUserAction();
			ClearInput();
		}
		private void CreateUserAction()
		{
			var handler = new CreateUserHandler(_unitOfWork);
			var user = handler.Handle(new UserArg(Name,Age));
			Users.Add(user);
			ClearInput();
		}

		private void UpdateUserAction()
		{
			var handler = new UpdateUserHandler(_unitOfWork);
			var user = handler.Handle(new UserArg(Name, Age) { Id=SelectedUser.Id});
			SelectedUser.Name = user.Name;
			SelectedUser.Age = user.Age;
			Users.Refresh();
		}

		private void DelateUserAction()
		{
			var handler = new DeleteUserHandler(_unitOfWork);
			bool isUserDeleted= handler.Handle(SelectedUser.Id);
			if(isUserDeleted)
				Users.Remove(SelectedUser);
		}

		private async Task<bool> SynchronizeAsync()
		{
			var serverOrchestrator = new WebClientOrchestrator("https://localhost:44374/api/sync");

			var clientProvider = new SqliteSyncProvider(_connectionString);

			var agent = new SyncAgent(clientProvider, serverOrchestrator);

			try
			{
				var progress = new SynchronousProgress<ProgressArgs>(args => SyncMessage += $"{args.PogressPercentageString}:\t{args.Message}");
			
				var result = await agent.SynchronizeAsync(Dotmim.Sync.Enumerations.SyncType.Normal,progress);

				SyncMessage += result;
				return true;

			}
			catch (Exception ex)
			{
				SyncMessage = ex.Message;
				return false;
			}
		}

		public async void UpdateServerDB()
		{
			await SynchronizeAsync();
			//SyncServerDBEvent?.Invoke(this);	
			FetchUsers();
		}

		private readonly IUnitOfWork _unitOfWork;
		private readonly string _connectionString;

		public event SyncServerDB SyncServerDBEvent;

		private ServerDBViewModel _serverMV;

		public ServerDBViewModel ServerVM
		{
			get { return _serverMV; }
			set { _serverMV = value;
				ServerVM.SyncClientDBEvent += Server_SyncClientDBEvent;
			}
		}

	}
}
