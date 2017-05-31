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
using System.Collections.ObjectModel;

namespace iScrumMaster.UnitTests.iScrumMaster.Client
{
    [TestClass]
    public class StorySizeComparisonViewModelTests
    {
        private ScrumServiceClient serviceClient = null;
        private IEventAggregator eventAggregator = null;
        private StorySizeComparisonViewModel viewMode = null;
        public StorySizeComparisonViewModelTests()
        {
        }
        [TestInitialize]
        public void Initialize()
        {
            this.serviceClient = new ScrumServiceClient();
            this.eventAggregator = A.Fake<IEventAggregator>();            
        }
        [TestMethod]
        public void OnLoginSuccessfulTests()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            ObservableCollection<StorySizeItem> sizeItems = new ObservableCollection<StorySizeItem> { new StorySizeItem { Size = 10, ID = "US1234" } };
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            GlobalHelper.sessionManager.StoryItems = sizeItems;
            this.viewMode = new StorySizeComparisonViewModel();
            this.viewMode.OnLoginSuccessful(true);
            var items = this.viewMode.Items;
            Assert.AreEqual(this.viewMode.Items.Count, 1);
        }

        [TestMethod]
        [Tag("New")]
        public void OnMessageReceivedTest_1()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new StorySizeComparisonViewModel();
            UserStory story = new UserStory { ID = "US1234", Size = 10 };
            string xml = SerializationManager.ToString(story);            
            NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.EndEstimation, Message = xml };
            this.viewMode.OnMessageReceived(notificationMessage);
            var items = this.viewMode.Items;
            Assert.AreEqual(this.viewMode.Items[0].Size, 10);
        }

        [TestMethod]
        [Tag("New")]
        public void OnMessageReceivedTest_2()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new StorySizeComparisonViewModel();
            UserStory story = new UserStory { ID = "US1234", Size = 10 };
            var item = new StorySizeItem { Size = 5, ID = "US1234" };
            this.viewMode.Items.Add(item);
            string xml = SerializationManager.ToString(story);
            NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.EndEstimation, Message = xml };
            this.viewMode.OnMessageReceived(notificationMessage);
            Assert.AreEqual(this.viewMode.Items[0].Size, 10);
        }
    }
}
