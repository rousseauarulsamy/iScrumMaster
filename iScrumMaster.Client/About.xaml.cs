using iScrumMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace iScrumMaster
{
    [Export(typeof(About))]
    public partial class About : RadWindow
    {
        #region Member Variables
        private LoginViewModel _loginViewModel = null;
        #endregion
        [ImportingConstructor]
        public About()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string Version 
        {
            get
            {
                string version = string.Empty;
                AssemblyFileVersionAttribute[] attributes = (AssemblyFileVersionAttribute[])Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    version = attributes[0].Version;
                return version;
            }
        }

    }
}
