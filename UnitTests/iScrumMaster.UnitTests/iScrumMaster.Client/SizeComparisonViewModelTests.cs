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
    public class SizeComparisonViewModelTests
    {
        private ScrumServiceClient serviceClient = null;
        private IEventAggregator eventAggregator = null;
        private SizeComparisonViewModel viewMode = null;
        public SizeComparisonViewModelTests()
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
            ObservableCollection<SizeItem> sizeItems = new ObservableCollection<SizeItem> { new SizeItem { Size = 1, UserCount = 10 } };
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            GlobalHelper.sessionManager.Items = sizeItems;
            this.viewMode = new SizeComparisonViewModel();
            this.viewMode.OnLoginSuccessful(true);
            var items = this.viewMode.Items;
            Assert.AreEqual(this.viewMode.Items.Count, 1);
        }

        [TestMethod]
        public void OnMessageReceivedTest_1()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new SizeComparisonViewModel();
            NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.SizeStory, Message = "10" };
            this.viewMode.OnMessageReceived(notificationMessage);
            var items = this.viewMode.Items;
            Assert.AreEqual(this.viewMode.Items[0].Size, 10);
        }

        [TestMethod]
        public void OnMessageReceivedTest_2()
        {
            ServiceLocator.SetLocatorProvider(() => A.Fake<IServiceLocator>());
            GlobalHelper.sessionManager = new SessionManager();
            GlobalHelper.serviceClient = this.serviceClient;
            GlobalHelper.eventAggregator = this.eventAggregator;
            this.viewMode = new SizeComparisonViewModel();
            NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.StartEstimation, Message = "10" };
            this.viewMode.OnMessageReceived(notificationMessage);
            var items = this.viewMode.Items;
            Assert.AreEqual(this.viewMode.Items.Count, 0);
        }
    }
}
