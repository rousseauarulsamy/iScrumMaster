using iScrumMaster.Infrastructure.Events;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
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

namespace iScrumMaster.Infrastructure.Models
{
    [Export(typeof(ScrumServiceClient))]
    public partial class ScrumServiceClient
    {
        IEventAggregator _eventAggregator = null;
        public void InitializeEvents()
        {
            this._eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();            
            this.NotifyAllMembersReceived += ScrumServiceClient_NotifyAllMembersReceived;
        }

        void ScrumServiceClient_NotifyAllMembersReceived(object sender, NotifyAllMembersReceivedEventArgs e)
        {
            if (e != null && e.Error == null && e.notificationMessage != null)
            {
                this._eventAggregator.GetEvent<NotifyMessageEvent>().Publish(e.notificationMessage);
            }
        }
    }
}
