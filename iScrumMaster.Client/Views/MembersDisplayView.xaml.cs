using iScrumMaster.Infrastructure.Models;
using iScrumMaster.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
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

namespace iScrumMaster.Views
{
    [Export(typeof(MembersDisplayView))]
    public partial class MembersDisplayView : UserControl
    {
        private MemberDisplayViewModel _memberDisplayViewModel = null;
        
        public MembersDisplayView()
        {
            InitializeComponent();
            this._memberDisplayViewModel = ServiceLocator.Current.GetInstance<MemberDisplayViewModel>();
            this.DataContext = this._memberDisplayViewModel;
        }
    }
}
