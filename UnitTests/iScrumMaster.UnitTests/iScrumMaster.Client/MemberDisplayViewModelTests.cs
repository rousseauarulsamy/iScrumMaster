using FakeItEasy;
using iScrumMaster.Client.Infrastructure;
using iScrumMaster.Infrastructure.Models;
using iScrumMaster.ViewModels;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Silverlight.Testing;
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
    public class MemberDisplayViewModelTests
    {
        private ScrumServiceClient serviceClient = null;
        private IEventAggregator eventAggregator = null;
        private MemberDisplayViewModel viewMode = null;
        public MemberDisplayViewModelTests()
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            this.serviceClient = new ScrumServiceClient();
            this.eventAggregator = A.Fake<IEventAggregator>();            
        }
        [TestMethod]    
        public void AddUsersToList()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            GlobalHelper.sessionManager.Members.Add(new User { UserID = "A", FirstName = "A", Role = Roles.ScrumMaster });
            GlobalHelper.sessionManager.Members.Add(new User { UserID = "B", FirstName = "B", Role = Roles.DEVMember });
            GlobalHelper.sessionManager.Members.Add(new User { UserID = "C", FirstName = "C", Role = Roles.ProductOwner });
            GlobalHelper.sessionManager.CurrentUser.UserID = "X";
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new MemberDisplayViewModel();
            this.viewMode.AddUsersToList();
            Assert.AreEqual(this.viewMode.ScrumMasters[0].UserID, "A");
            Assert.AreEqual(this.viewMode.TeamMembers[0].UserID, "B");
            Assert.AreEqual(this.viewMode.ProductOwners[0].UserID, "C");           
        }

        [TestMethod]
        public void OnLoginSuccessfulTests()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            GlobalHelper.sessionManager.Members.Add(new User { UserID = "A", FirstName = "A", Role = Roles.ScrumMaster });
            GlobalHelper.sessionManager.Members.Add(new User { UserID = "B", FirstName = "B", Role = Roles.DEVMember });
            GlobalHelper.sessionManager.Members.Add(new User { UserID = "C", FirstName = "C", Role = Roles.ProductOwner });
            GlobalHelper.sessionManager.CurrentUser.UserID = "X";
            GlobalHelper.sessionManager.CurrentUser.Role = Roles.ScrumMaster;
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new MemberDisplayViewModel();
            this.viewMode.OnLoginSuccessful(true);
            Assert.AreEqual(this.viewMode.ScrumMasters[0].UserID, "X");
            Assert.AreEqual(this.viewMode.ScrumMasters[1].UserID, "A");
            Assert.AreEqual(this.viewMode.TeamMembers[0].UserID, "B");
            Assert.AreEqual(this.viewMode.ProductOwners[0].UserID, "C");     
        }

        [TestMethod]
        public void OnMessageReceivedTests()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();            
            GlobalHelper.sessionManager.CurrentUser.UserID = "X";
            GlobalHelper.sessionManager.CurrentUser.Role = Roles.ScrumMaster;
            NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.UserJoined, Owner = GlobalHelper.sessionManager.CurrentUser };
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new MemberDisplayViewModel();
            this.viewMode.OnMessageReceived(notificationMessage);
            Assert.AreEqual(this.viewMode.ScrumMasters[0].UserID, "X");
        }

        [TestMethod]
        public void MembersTests()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            Users scrumMasters = new Users { new User { UserID = "A", FirstName = "A" } };
            Users productOwners = new Users { new User { UserID = "B", FirstName = "B" } };
            Users teamMembers = new Users { new User { UserID = "C", FirstName = "C" } };
            GlobalHelper.sessionManager.CurrentUser.UserID = "X";
            GlobalHelper.sessionManager.CurrentUser.Role = Roles.ScrumMaster;
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new MemberDisplayViewModel();
            this.viewMode.ScrumMasters = scrumMasters;
            this.viewMode.ProductOwners = productOwners;
            this.viewMode.TeamMembers = teamMembers;
            Assert.AreEqual(this.viewMode.ScrumMasters[0].UserID, "A");
            Assert.AreEqual(this.viewMode.ProductOwners[0].UserID, "B");
            Assert.AreEqual(this.viewMode.TeamMembers[0].UserID, "C");
            
        }
    }
}
