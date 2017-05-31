using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using Microsoft.Practices.Prism.Events;
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
using System.Collections.ObjectModel;
using iScrumMaster.Infrastructure;
using System.Linq;

namespace iScrumMaster.ViewModels
{
    [Export(typeof(StorySizeComparisonViewModel))]
    public class StorySizeComparisonViewModel : ViewModelBase
    {
        #region Member Variables
        private ObservableCollection<StorySizeItem> _items = new ObservableCollection<StorySizeItem>();
        #endregion

        [ImportingConstructor]
        public StorySizeComparisonViewModel()
        {
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Subscribe(OnMessageReceived);
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful);            
        }

        public void OnLoginSuccessful(bool status)
        {
            if (GlobalHelper.sessionManager.StoryItems != null && GlobalHelper.sessionManager.StoryItems.Count > 0)
            {
                this.Items = GlobalHelper.sessionManager.StoryItems;
            }
        }

        public void OnMessageReceived(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null && !string.IsNullOrWhiteSpace(notificationMessage.Message) && notificationMessage.Action == ActionType.EndEstimation)
            {
                UserStory userStory = SerializationManager.FromString(notificationMessage.Message, typeof(UserStory)) as UserStory;
                if (userStory != null)
                {
                    var storyItem = this._items.FirstOrDefault(item => item.ID == userStory.ID);
                    if (storyItem != null)
                        storyItem.Size = userStory.Size;
                    else
                    {
                        storyItem = new StorySizeItem { Size = userStory.Size, ID = userStory.ID };
                        this._items.Add(storyItem);
                    }
                }
            }
        }
        
        #region Properties
        public ObservableCollection<StorySizeItem> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
                this.NotifyPropertyChanged("Items");
            }
        }
        #endregion

    }
}
