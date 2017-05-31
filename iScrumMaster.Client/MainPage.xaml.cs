using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace iScrumMaster
{

    /// <summary>
    /// Added comment for testing.
    /// </summary>
    [Export(typeof(MainPage))]
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Subscribe(OnMessageReceived);
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful); 
            
        }

        public void OnLoginSuccessful(bool status)
        {
            this.tabManageUsers.Visibility = this.tabSettings.Visibility = System.Windows.Visibility.Collapsed;
            if (GlobalHelper.sessionManager.CurrentUser.Role != Roles.ScrumMaster)
                this.UpdateLayout(GlobalHelper.sessionManager.SessionType);
            else
            {
                this.tabManageUsers.Visibility = this.tabSettings.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public void OnMessageReceived(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null && (notificationMessage.Action == ActionType.StartMeeting || notificationMessage.Action == ActionType.EndMeeting))
            {
                int type = 0;
                int.TryParse(notificationMessage.Message, out type);
                MeetingType meetingType = (MeetingType)type;
                if (notificationMessage.Action == ActionType.StartMeeting)
                    UpdateLayout(meetingType);
                else
                    NoMeetingMode();
            }
        }

        private void UpdateLayout(MeetingType meetingType)
        {           
            switch (meetingType)
            {
                case MeetingType.None:
                    {
                        NoMeetingMode();
                    }
                    break;
                case MeetingType.Planning:
                    {
                        this.gridTabManager.Visibility = this.tabPlanning.Visibility = Visibility.Visible;                        
                        this.txtNoMeeting.Visibility = this.tabRetro.Visibility = Visibility.Collapsed;
                        this.tabPlanning.IsSelected = true;
                    }
                    break;
                case MeetingType.Retrospective:
                    {
                        this.gridTabManager.Visibility = this.tabRetro.Visibility = System.Windows.Visibility.Visible;
                        this.txtNoMeeting.Visibility  = this.tabPlanning.Visibility = Visibility.Collapsed;
                        this.tabRetro.IsSelected = true;
                    }
                    break;
            }
        }

        private void NoMeetingMode()
        {
            this.gridTabManager.Visibility = Visibility.Collapsed;
            this.txtNoMeeting.Visibility = Visibility.Visible;
        }

        public void UpdateUserSettings()
        {
            this.headerView.UserName = GlobalHelper.sessionManager.CurrentUser.FirstName;
        }
    }
}
