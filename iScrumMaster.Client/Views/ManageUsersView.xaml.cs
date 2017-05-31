using iScrumMaster.Client.Infrastructure;
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
    public partial class ManageUsersView : UserControl
    {
        private ManageUsersViewModel _viewModel = null;
        public ManageUsersView()
        {
            InitializeComponent();
            this._viewModel = ServiceLocator.Current.GetInstance<ManageUsersViewModel>();
            this.DataContext = this._viewModel;
        }
    }
}
