using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using System;
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

namespace iScrumMaster.ViewModels
{
    [Export(typeof(MemberDisplayViewModel))]
    public class MemberDisplayViewModel: ViewModelBase
    {
        private Users _scrumMasters = new Users();
        private Users _productOwners = new Users();
        private Users _teamMembers = new Users();
        [ImportingConstructor]
        public MemberDisplayViewModel()
        {
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Subscribe(OnMessageReceived);
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful);            
        }

        public void OnLoginSuccessful(bool status)
        {
            this.AddUserToCorrectList(GlobalHelper.sessionManager.CurrentUser);
            this.AddUsersToList();
        }

        public void AddUsersToList()
        {
            Users users = GlobalHelper.sessionManager.Members;
            if( users != null )
            {
                foreach (User user in users)
                {
                    if( user.UserID != GlobalHelper.sessionManager.CurrentUser.UserID)
                        this.AddUserToCorrectList(user);
                }
            }
        }

        public Users ScrumMasters
        {
            get
            {
                return this._scrumMasters;
            }
            set
            {
                this._scrumMasters = value;
                this.NotifyPropertyChanged("ScrumMasters");
            }
        }

        public Users ProductOwners
        {
            get
            {
                return this._productOwners;
            }
            set
            {
                this._productOwners = value;
                this.NotifyPropertyChanged("ProductOwners");
            }
        }

        public Users TeamMembers
        {
            get
            {
                return this._teamMembers;
            }
            set
            {
                this._teamMembers = value;
                this.NotifyPropertyChanged("TeamMembers");
            }
        }

        public void OnMessageReceived(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null && notificationMessage.Action == ActionType.UserJoined && notificationMessage.Owner != null)
            {
                this.AddUserToCorrectList(notificationMessage.Owner);
            }
        }

        private void AddUserToCorrectList(User user)
        {
            switch (user.Role)
            {
                case Roles.ScrumMaster:
                    {
                        this._scrumMasters.Add(user);
                    }
                    break;
                case Roles.ProductOwner:
                case Roles.BusinessAnalyst:
                    {
                        this._productOwners.Add(user);
                    }
                    break;
                default:
                    {
                        this._teamMembers.Add(user);
                    }
                    break;
            }
        }
    }
}
