using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Events;
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
    public partial class RetrospectiveView : UserControl
    {
        public RetrospectiveView()
        {
            InitializeComponent();
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Subscribe(OnMessageReceived);
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful);              
        }

        public void OnLoginSuccessful(bool status)
        {
            if (GlobalHelper.sessionManager.SessionType == MeetingType.Retrospective)
            {
                this.AddToDiscussion(GlobalHelper.sessionManager.CurrentDiscussion);
            }
        }

        private string newMessageFormat = "{0}{1} {2} wrote :{3}";       

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Format(newMessageFormat, Environment.NewLine, GlobalHelper.sessionManager.CurrentUser.FirstName, DateTime.Now.ToString(), this.txtDiscussion.Text);
            this.AddToDiscussion(message);
            NotificationMessage notificationMessage = new NotificationMessage { Message = message, Action = ActionType.AddRetroComments, Owner= GlobalHelper.sessionManager.CurrentUser };
            this.txtDiscussion.Text = string.Empty;
            GlobalHelper.serviceClient.NotifyMembersAsync(notificationMessage);
        }

        private void AddToDiscussion(string incomingMessage)
        {
            this.txtblkDiscussion.Text += incomingMessage;
        }

        public void OnMessageReceived(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null && notificationMessage.Action == ActionType.AddRetroComments)
            {
                this.AddToDiscussion(notificationMessage.Message);
            }
        }
    }
}
