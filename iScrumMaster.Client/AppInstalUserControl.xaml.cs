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

namespace iScrumMaster.Client
{
    public partial class AppInstalUserControl : UserControl
    {
        public AppInstalUserControl()
        {
            InitializeComponent();
            Application.Current.InstallStateChanged += new EventHandler(Current_InstallStateChanged);
            if (Application.Current.InstallState == InstallState.Installed)
            {
                instaled.Visibility = System.Windows.Visibility.Collapsed;
                alreadyinstalledText.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Application.Current.InstallState == InstallState.NotInstalled)
            {
                alreadyinstalledText.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        void Current_InstallStateChanged(object sender, EventArgs e)
        {
            if (Application.Current.InstallState == InstallState.Installed)
            {
                instaled.Visibility = System.Windows.Visibility.Collapsed;
                alreadyinstalledText.Visibility = System.Windows.Visibility.Visible;
            }
            else if (Application.Current.InstallState == InstallState.InstallFailed)
            {
                MessageBox.Show("Installation Failed, Please restart installation");
            }

            else if (Application.Current.InstallState == InstallState.NotInstalled)
            {
                alreadyinstalledText.Visibility = System.Windows.Visibility.Collapsed;
                instaled.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Install();

        }

    }
}
