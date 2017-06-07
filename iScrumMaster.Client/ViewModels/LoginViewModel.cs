using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure;
using iScrumMaster.Infrastructure.Events;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.ServiceModel.Description;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace iScrumMaster.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : ViewModelBase
    {
        #region Member Variables
        private ICommand _cancelCommand = null;
        private ScrumServiceClient _serviceClient = null;
        private string _userName = string.Empty;
        private string _passcode = string.Empty;
        private IEventAggregator _eventAggregator = null;
        private string _errorString = string.Empty;
        #endregion

        [ImportingConstructor]
        public LoginViewModel(ScrumServiceClient serviceClient, IEventAggregator eventAggregator)
        {
            this._cancelCommand = new DelegateCommand<object>(OnCancel, param => { return true; });
            this._serviceClient = serviceClient;
            this._serviceClient.LoginCompleted += _serviceClient_LoginCompleted;
            this._serviceClient.InitializeEvents();
            this._eventAggregator = eventAggregator;
        }       

        public void _serviceClient_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            if (e != null && e.Result != null && e.Error == null)
            {
                LogManager.LogMessage(Models.LogTypes.Info, "Successfull Authentication");
                GlobalHelper.sessionManager = e.Result;
                GlobalHelper.sessionManager.CurrentProject = new Project { ID = GlobalHelper.sessionManager.CurrentUser.ProjectID };
                var mainPage = ServiceLocator.Current.GetInstance<MainPage>();
                mainPage.UpdateUserSettings();
                this._eventAggregator.GetEvent<LoginSuccessfulEvent>().Publish(true);
                var emptyUserControl = GlobalHelper.AppContent as EmptyUserControl;
                if (emptyUserControl != null)
                    emptyUserControl.Content = mainPage;
                this.ErrorMessage = string.Empty;
            }
            else
            {
                this.ErrorMessage = "User name or password is not correct.";
                LogManager.LogMessage(Models.LogTypes.Error, "Wrong user name or password");
            }
            this.Passcode = string.Empty;
        }

        #region Properties
       public ICommand CancenCommand
        {
            get
            {
                return this._cancelCommand;
            }
        }

        public string UserName 
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
                this.NotifyPropertyChanged("UserName");
            }
        }

        public string Passcode
        {
            get
            {
                return this._passcode;
            }
            set
            {
                this._passcode = value;
                this.NotifyPropertyChanged("Passcode");
            }
        }
        #endregion

        #region Private Methods
        private void OnLogin(object data)
        {
            ValidateUser();
        }

        public void ValidateUser()
        {
            if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Passcode))
            {
                LogManager.LogMessage(Models.LogTypes.Info, "Validated User Credentials");
                var credentials = this._serviceClient.ChannelFactory.Endpoint.Behaviors.Find<ClientCredentials>();
                credentials.UserName.UserName = this.UserName;
                credentials.UserName.Password = this.Passcode;
                string encodedPasscode = GlobalHelper.GetSHA256EncodedString(this.Passcode);
                this._serviceClient.LoginAsync(this.UserName, encodedPasscode);
            }
        }

        private void OnCancel(object data)
        {
            Application.Current.MainWindow.Close();
        }

        public string ErrorMessage 
        {
            get
            {
                return this._errorString;
            }
            set
            {
                this._errorString = value;
                this.NotifyPropertyChanged("ErrorMessage");
            }
        }
        #endregion
    }
}
