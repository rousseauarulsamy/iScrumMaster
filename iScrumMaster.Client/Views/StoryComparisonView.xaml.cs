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
    public partial class StoryComparisonView : UserControl
    {
        private StorySizeComparisonViewModel _storySizeComparisonViewModel = null;
        public StoryComparisonView()
        {
            InitializeComponent();
            this._storySizeComparisonViewModel = ServiceLocator.Current.GetInstance<StorySizeComparisonViewModel>();
            this.DataContext = this._storySizeComparisonViewModel;
        }
    }
}
