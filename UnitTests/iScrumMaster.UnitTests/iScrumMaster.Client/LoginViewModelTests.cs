using FakeItEasy;
using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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


namespace iScrumMaster.UnitTests.iScrumMaster.Client
{
    [TestClass]
    public class LoginViewModelTests
    {
        private ScrumServiceClient serviceClient = null;
        private IEventAggregator eventAggregator = null;
        private LoginViewModel loginViewModel = null;
        public LoginViewModelTests()
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            this.serviceClient = new ScrumServiceClient();
            this.eventAggregator = A.Fake<IEventAggregator>();            
        }
        [TestMethod]
        public void LoginCompleted_Test_NullValue()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            this.loginViewModel = new LoginViewModel(this.serviceClient, this.eventAggregator); 
            this.loginViewModel._serviceClient_LoginCompleted(null, null);
            Assert.AreEqual(loginViewModel.ErrorMessage, "User name or password is not correct.");
        }

        [TestMethod]
        public void ValidateUserTest()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            this.loginViewModel = new LoginViewModel(this.serviceClient, this.eventAggregator); 
            this.loginViewModel.UserName = "Rousseau";
            this.loginViewModel.Passcode = "Setup2000";
            this.loginViewModel.ValidateUser();
            var credentials = this.serviceClient.ChannelFactory.Endpoint.Behaviors.Find<ClientCredentials>();
            Assert.AreEqual(this.loginViewModel.UserName, credentials.UserName.UserName);
            Assert.AreEqual(this.loginViewModel.Passcode, credentials.UserName.Password);
        }
    }
}
