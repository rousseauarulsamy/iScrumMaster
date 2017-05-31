using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace iScrumMaster.Views
{
    public partial class CustomMessageBox : RadWindow
    {
        #region Member Variables
        private string result = string.Empty;
        readonly AutoResetEvent waitHandle;
        #endregion
        public CustomMessageBox()
        {
            InitializeComponent();
            waitHandle = new AutoResetEvent(false);
        }

        public static Task<string> Show()
        {
            var customDialog = new CustomMessageBox();
            customDialog.ShowDialog();
            customDialog.Closed += (s, e) => customDialog.waitHandle.Set();
            return Task<string>.Factory.StartNew(() => { customDialog.waitHandle.WaitOne(); return customDialog.result; });
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            result = "Hai";
            Close();
        }

        public static async void ShowCustomDialog()
        {
            //var result = await CustomMessageBox.Show();
        }
    }
}
