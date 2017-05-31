using iScrumMaster.Infrastructure.Events;
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
    [Export(typeof(SizeComparisonView))]
    public partial class SizeComparisonView : UserControl
    {
        private IEventAggregator _eventAggregator = null;
        private SizeComparisonViewModel _sizingComparisonViewModel = null;
        public SizeComparisonView()
        {
            InitializeComponent();
            this._eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            this._sizingComparisonViewModel = ServiceLocator.Current.GetInstance<SizeComparisonViewModel>();
            this.DataContext = this._sizingComparisonViewModel;
        }

     
    }
}
