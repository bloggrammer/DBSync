using DBSync.Usecases.Base;
using MVVM.LTE.Commands;
using MVVM.LTE.ViewModel;
using vm = MVVM.LTE.ViewModel;

namespace DBSync.WPFClient.ViewModels
{
	public class ViewModelBase : vm.ViewModelBase
    {
		
		protected void ClearInput()
		{
			Name = null;
			Age = null;
			BtnContent = "Create";
		}
		protected void EditUserAction()
		{
			Name = SelectedUser.Name;
			Age = SelectedUser.Age;
			BtnContent = "Update";
			_actionType = ActionType.Update;
		}
		private string _name;

		public string Name
		{
			get { return _name; }
			set { _name = value; RaisePropertyChanged(); }
		}

		private string _btnContent="Create";

		public string BtnContent
		{
			get { return _btnContent; }
			set { _btnContent = value; RaisePropertyChanged(); }
		}


		private int? _age;

		public int? Age
		{
			get { return _age; }
			set { _age = value; RaisePropertyChanged(); }
		}

		private string _serverResponse;

		public string ServerResponse
		{
			get { return _serverResponse; }
			set { _serverResponse = value; RaisePropertyChanged(); }
		}

		private UserDto _selectedUser;

		public UserDto SelectedUser
		{
			get { return _selectedUser; }
			set { _selectedUser = value; RaisePropertyChanged(); }
		}

		private string _syncMessage;

		public string SyncMessage
		{
			get { return _syncMessage; }
			set { _syncMessage = value; RaisePropertyChanged(); }
		}


		public SimpleCommand CreateOrUpdateUserCommand { get; set; }
		public SimpleCommand DeleteUserCommand { get; set; }
		public SimpleCommand EditUserCommand { get; set; }
		public SimpleCommand BDSyncCommand { get; set; }

		public CustomObservableCollection<UserDto> Users { get; set; } = new CustomObservableCollection<UserDto>();

		protected ActionType _actionType;
    }
	public enum ActionType { Create, Update }
}
