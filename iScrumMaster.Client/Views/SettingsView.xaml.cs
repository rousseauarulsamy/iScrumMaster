using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
namespace iScrumMaster.Views
{
    public partial class SettingsView : UserControl
    {
        bool meetingStarted = false;
        public SettingsView()
        {
            InitializeComponent();
        }       

        private void btnStartEndMeeting_Click(object sender, RoutedEventArgs e)
        {
            meetingStarted = !meetingStarted;
            this.btnStartEndMeeting.Content = meetingStarted ? "End Meeting" : "Start Meeting";
            ActionType actionType = meetingStarted ? ActionType.StartMeeting : ActionType.EndMeeting;
            GlobalHelper.serviceClient.NotifyMembersAsync(new NotificationMessage { Owner = GlobalHelper.sessionManager.CurrentUser, Message = (this.cmbxMeetingType.SelectedIndex + 1).ToString(), Action = actionType });
        }
    }
}
