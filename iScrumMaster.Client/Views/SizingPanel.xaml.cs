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

namespace iScrumMaster.Views
{
    public partial class SizingPanel : UserControl
    {
        public SizingPanel()
        {
            InitializeComponent();
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Subscribe(OnMessageReceived);
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful);            
        }

        public void OnLoginSuccessful(bool status)
        {
            Visibility isVisible = GlobalHelper.sessionManager.CurrentUser.Role == Roles.ScrumMaster ? Visibility.Visible : Visibility.Collapsed;
            this.btnEndEstimation.Visibility = this.btnStartEstimation.Visibility = isVisible;
            this.btnUpdateStory.Visibility = isVisible;
            this.txtStory.IsEnabled = this.txtStoryID.IsEnabled = GlobalHelper.sessionManager.CurrentUser.Role == Infrastructure.Models.Roles.ScrumMaster;
            if (GlobalHelper.sessionManager.CurrentStory != null)
                this.UpdateStory(GlobalHelper.sessionManager.CurrentStory);
        }
        private void btnStartEstimation_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateControls(true);
            NotificationMessage notificationMessage = new NotificationMessage { Message = string.Empty, Action = ActionType.StartEstimation, Owner = GlobalHelper.sessionManager.CurrentUser };
            var sizeComparisonViewModel = ServiceLocator.Current.GetInstance<SizeComparisonViewModel>();
            sizeComparisonViewModel.Items.Clear();
            GlobalHelper.serviceClient.NotifyMembersAsync(notificationMessage);
        }

        private void btnEndEstimation_Click(object sender, RoutedEventArgs e)
        {
            UpdateControls(false);
            NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.EndEstimation, Owner = GlobalHelper.sessionManager.CurrentUser };            
            var sizeComparisonViewModel = ServiceLocator.Current.GetInstance<SizeComparisonViewModel>();            
            if( sizeComparisonViewModel.Items.Count > 0 )
            {
                var items = sizeComparisonViewModel.Items.OrderByDescending(item => item.UserCount).ToList();
                var sizeItem = items[0];
                UserStory story = new UserStory { ID = this.txtStoryID.Text, Size = sizeItem.Size };
                notificationMessage.Message = SerializationManager.ToString(story);
            }
            GlobalHelper.serviceClient.NotifyMembersAsync(notificationMessage);
            ///This message is published for the current user.
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Publish(notificationMessage);
          
        }

        private void UpdateControls(bool estimationStarted)
        {
            this.sizingPointsView.IsEnabled = estimationStarted;
            this.btnStartEstimation.IsEnabled = !estimationStarted;
            this.btnEndEstimation.IsEnabled = estimationStarted;
        }

        public void OnMessageReceived(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null )
            {
                switch (notificationMessage.Action)
                {

                    case ActionType.StartEstimation:
                    case ActionType.EndEstimation:
                        {
                            bool enableEstimation = notificationMessage.Action == ActionType.StartEstimation;
                            this.UpdateControls(enableEstimation);
                        }
                        break;
                    case ActionType.UpdateStory:
                        {
                            UserStory story = SerializationManager.FromString(notificationMessage.Message, typeof(UserStory)) as UserStory;
                            UpdateStory(story);
                        }
                        break;
                }                
            }
        }

        private void UpdateStory(UserStory story)
        {
            if (story != null)
            {
                this.txtStoryID.Text = story.ID;
                this.txtStory.Text = story.Description;
            }
        }

        private void btnUpdateStory_Click(object sender, RoutedEventArgs e)
        {
            UserStory story = new UserStory { ID = this.txtStoryID.Text, Description = this.txtStory.Text };
            NotificationMessage notificationMessage = new NotificationMessage { Owner = GlobalHelper.sessionManager.CurrentUser, Message = SerializationManager.ToString(story), Action = ActionType.UpdateStory };
            GlobalHelper.serviceClient.NotifyMembersAsync(notificationMessage);
        }
    }
}
