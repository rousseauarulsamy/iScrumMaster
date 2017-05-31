using FakeItEasy;
using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    public class ManageUsersViewModelTests
    {
        private ScrumServiceClient serviceClient = null;
        private IEventAggregator eventAggregator = null;
        private ManageUsersViewModel viewMode = null;
        public ManageUsersViewModelTests()
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            this.serviceClient = new ScrumServiceClient();
            this.eventAggregator = A.Fake<IEventAggregator>();            
        }
        [TestMethod]    
        public void LoadUsersFromProjectTests()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            User user = new User { UserID = "A", FirstName = "A", Role = Roles.ScrumMaster };
            List<User> users = new List<User>();
            users.Add(user);
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new ManageUsersViewModel();
            LoadUsersForProjectCompletedEventArgs e = new LoadUsersForProjectCompletedEventArgs(new object[] { users }, null, false, null);
            this.viewMode.serviceClient_LoadUsersForProjectCompleted(null, e);
            var command = this.viewMode.SaveCommand;
            command = this.viewMode.RefreshCommand;
            var allUsers = this.viewMode.AllUsers;
            var roles = this.viewMode.Roles;
            this.viewMode.OnLoginSuccessful(true);
            Assert.AreEqual(this.viewMode.AllUsers[0].UserID, "A");
        }
    }
}
