using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using System.Collections.ObjectModel;
using iScrumMaster.Infrastructure;
using System.Linq;
using iScrumMaster.Client.Infrastructure;
namespace iScrumMaster.ViewModels
{
    [Export(typeof(SizeComparisonViewModel))]
    public class SizeComparisonViewModel : ViewModelBase
    {
        private ObservableCollection<SizeItem> _items = new ObservableCollection<SizeItem>();
        [ImportingConstructor]
        public SizeComparisonViewModel()
        {
            GlobalHelper.eventAggregator.GetEvent<NotifyMessageEvent>().Subscribe(OnMessageReceived);
            GlobalHelper.eventAggregator.GetEvent<LoginSuccessfulEvent>().Subscribe(OnLoginSuccessful);            
        }
        public void OnLoginSuccessful(bool status)
        {
            if (GlobalHelper.sessionManager.Items != null && GlobalHelper.sessionManager.Items.Count > 0)
            {
                this.Items = GlobalHelper.sessionManager.Items;
            }
        }
        public void OnMessageReceived(NotificationMessage notificationMessage)
        {
            if (notificationMessage != null && (notificationMessage.Action == ActionType.SizeStory || notificationMessage.Action == ActionType.StartEstimation))
            {
                switch (notificationMessage.Action)
                {
                    case ActionType.SizeStory:
                        {
                            double sizeValue = 0;
                            double.TryParse(notificationMessage.Message, out sizeValue);
                            this.UpdateSizeItem(sizeValue);
                        }
                        break;

                    case ActionType.StartEstimation:
                        {
                            this.Items.Clear();
                        }
                        break;
                }
                
            }
        }

        private void UpdateSizeItem(double sizeValue)
        {
            var sizeItem = this._items.FirstOrDefault(item => item.Size == sizeValue);
            if (sizeItem == null)
            {
                this._items.Add(new SizeItem { Size = sizeValue, UserCount = 1 });
            }
            else
                sizeItem.UserCount += 1;
            this._items.OrderBy(item => item.Size);
        }
        #region Properties
        public ObservableCollection<SizeItem> Items
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
