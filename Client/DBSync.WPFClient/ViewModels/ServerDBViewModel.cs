using DBSync.Usecases.Base;
using DBSync.WPFClient.Notifications;
using MVVM.LTE.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DBSync.WPFClient.ViewModels
{
    public class ServerDBViewModel:ViewModelBase
    {
        public ServerDBViewModel(ClientDBViewModel client)
        {
            _client = new HttpClient();
            CreateOrUpdateUserCommand = new SimpleCommand(CreateOrUpdateUserAction);
            DeleteUserCommand = new SimpleCommand(DeleteUserAsync);
            EditUserCommand = new SimpleCommand(EditUserAction);
            BDSyncCommand = new SimpleCommand(UpdateClientDB);
            client.ServerVM = this;
            client.SyncServerDBEvent += Client_SyncServerDBEvent;
            Init();
            GetUsersAsync();
        }

        private void Init()
        {
            _client.BaseAddress = new Uri("https://localhost:44374/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }
        private void CreateOrUpdateUserAction()
        {
            if (_actionType == ActionType.Update)
                UpdateUserAsync();
            else
                CreateUserAsync();
            ClearInput();
        }
        private async void CreateUserAsync()
        {
            var arg = new UserArg(Name, Age);
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/user/create", arg);
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadAsAsync<UserDto>();
            Users.Add(user);
            ClearInput();
        }

        private async void UpdateUserAsync()
        {
            var arg = new UserArg(Name, Age) { Id = SelectedUser.Id };
            HttpResponseMessage response = await _client.PutAsJsonAsync("api/user/update", arg);
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadAsAsync<UserDto>();
            SelectedUser.Name = user.Name;
            SelectedUser.Age = user.Age;
            Users.Refresh();
        }

        private async void DeleteUserAsync()
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/user/{SelectedUser.Id}");
            ServerResponse= response.StatusCode.ToString();
        }
        private async void GetUsersAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("api/user/all");
            if (response.IsSuccessStatusCode)
            {
                 var users= await response.Content.ReadAsAsync<IEnumerable<UserDto>>();
                users.ToList().ForEach(x => Users.Add(x));
               // var users = await response.Content.ReadAsStringAsync();
               //var deserilizedUsers = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(users);
               // deserilizedUsers.ToList().ForEach(x => Users.Add(x));
            }
        }
        private void Client_SyncServerDBEvent(object sender)
        {
            GetUsersAsync();
        }
        public void UpdateClientDB() => SyncClientDBEvent?.Invoke(this);     

        public event SyncClientDB SyncClientDBEvent;

        private readonly HttpClient _client;
    }
}
