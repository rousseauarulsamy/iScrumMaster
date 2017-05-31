using iScrumMaster.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace iScrumMaster
{
    [Export(typeof(LoginView))]
    public partial class LoginView : UserControl
    {
        #region Member Variables
        private LoginViewModel _loginViewModel = null;
        #endregion
        [ImportingConstructor]
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            this._loginViewModel = loginViewModel;
            this.DataContext = loginViewModel;
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            this._loginViewModel.UserName = this.txtUserName.Text;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            this._loginViewModel.Passcode = this.txtPassword.Password;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {            
            this._loginViewModel.ValidateUser();            
        }
    }
}
