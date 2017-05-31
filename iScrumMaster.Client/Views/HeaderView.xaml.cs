using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using Microsoft.Practices.Prism.Events;
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
    public partial class HeaderView : UserControl
    {
        #region Member Variables
        private IEventAggregator _eventAggregator = null;
        private ScrumServiceClient _serviceClient = null;
        # endregion

        public HeaderView()
        {
            InitializeComponent();
            this._eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            this._serviceClient = ServiceLocator.Current.GetInstance<ScrumServiceClient>();
        }

        private void hyperlinkLogOut_Click(object sender, RoutedEventArgs e)
        {
            this._eventAggregator.GetEvent<LogoutSuccessfulEvent>().Publish(true);
            var loginView = ServiceLocator.Current.GetInstance<LoginView>();
            var emptyUserControl = GlobalHelper.AppContent as EmptyUserControl;
            if (emptyUserControl != null)
                emptyUserControl.Content = loginView;
        }

        public string UserName 
        {
            get
            {
                return this.txtUserName.Text;
            }
            set
            {
                this.txtUserName.Text = value;
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = ServiceLocator.Current.GetInstance<About>();
            aboutWindow.ShowDialog();
        }
    }
}
