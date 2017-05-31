using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure;
using iScrumMaster.Infrastructure.Models;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using iScrumMaster.Infrastructure.Events;

namespace iScrumMaster.ViewModels
{
    [Export(typeof(ManageUsersViewModel))]
    public class ManageUsersViewModel : ViewModelBase
    {
        private Users _users = new Users();
        private ICommand _refresh = null;
        private ICommand _save = null;
        [ImportingConstructor]
        public ManageUsersViewModel()
        {
            this._refresh = new DelegateCommand<object>(OnRefresh, param => { return true; });
            this._save = new DelegateCommand<object>(OnSave, param => { return true; });
            GlobalHelper.serviceClient.LoadUsersForProjectCompleted += serviceClient_LoadUsersForProjectCompleted;
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful);            
        }

        public void OnLoginSuccessful(bool status)
        {
            GlobalHelper.serviceClient.LoadUsersForProjectAsync(GlobalHelper.sessionManager.CurrentProject);
        }

        public void serviceClient_LoadUsersForProjectCompleted(object sender, LoadUsersForProjectCompletedEventArgs e)
        {
            if (e.Result != null && e.Error == null)
            {
                this._users.Clear();
                foreach (var user in e.Result)
                {
                    this._users.Add(user);
                }
            }
        }

        public ICommand SaveCommand 
        {
            get
            {
                return this._save;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return this._refresh;
            }
        }
        public Users AllUsers
        {
            get
            {
                return this._users;
            }
            set
            {
                this._users = value;
                this.NotifyPropertyChanged("AllUsers");
            }
        }

        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _positions;
        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> Roles
        {
            get
            {
                if (_positions == null)
                {
                    _positions = Telerik.Windows.Data.EnumDataSource.FromType<Roles>();
                }

                return _positions;
            }
        }

        public void OnSave(object data)
        {
            foreach (User user in this._users)
            {
                user.ProjectID = GlobalHelper.sessionManager.CurrentUser.ProjectID;
                ///Encoding only passwords less than 64 lenght.This is a quick fix. Need to find a better way in future
                ///SHA password length is 64 in length
                if(user.Password.Length < 64)
                    user.Password = GlobalHelper.GetSHA256EncodedString(user.Password);
            }
            GlobalHelper.serviceClient.SaveUsersAsync(this._users.ToList());
        }

        private void OnRefresh(object data)
        {
        }
    }
}
