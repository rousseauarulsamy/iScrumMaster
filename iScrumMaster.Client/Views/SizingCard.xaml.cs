using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace iScrumMaster.Views
{
    public partial class SizingCard : UserControl, INotifyPropertyChanged
    {
        #region Member Variables
        private string _storySize = "1";

        #endregion

        public SizingCard()
        {
            InitializeComponent();
            this.StorySize = "1";
            this.DataContext = this;
           
        }

        private void InitializeGlobalEntities()
        {
        }

        #region Properties
        public string StorySize 
        {
            get
            {
                return this._storySize;
            }
            set
            {
                this._storySize = value;
                this.NotifyPropertyChanged("StorySize");
            }
        }
        #endregion

        #region Implement INotifyPropertyChanged Interface
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NotificationMessage notificationMessage = new NotificationMessage { Message = this._storySize, Action = ActionType.SizeStory, Owner = GlobalHelper.sessionManager.CurrentUser };
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Publish(notificationMessage);
            GlobalHelper.serviceClient.NotifyMembersAsync(notificationMessage);
        }
    }
}
