using iScrumMaster.Infrastructure.Models;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Text;

namespace iScrumMaster.Client.Infrastructure
{
    public static class GlobalHelper
    {
        #region MemberVariables
        public static IEventAggregator eventAggregator = null;
        public static SessionManager sessionManager = null;
        public static ScrumServiceClient serviceClient = null;        
        #endregion
        static GlobalHelper()
        {
            eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            serviceClient = ServiceLocator.Current.GetInstance<ScrumServiceClient>();
        }

        public static string GetSHA256EncodedString(string input)
        {
            string hashString = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                SHA256Managed hashstring = new SHA256Managed();
                byte[] hash = hashstring.ComputeHash(bytes);                
                foreach (byte x in hash)
                {
                    hashString += String.Format("{0:x2}", x);
                }
            }
            return hashString;
        }

        public static UIElement AppContent
        {
            get
            {
                return Application.Current.IsRunningOutOfBrowser
                    ? Application.Current.MainWindow.Content
                    : Application.Current.RootVisual;
            }
        }
    }
}
