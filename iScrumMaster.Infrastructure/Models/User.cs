using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


namespace iScrumMaster.Infrastructure.Models
{
    public class User : NotificationObject
    {
        #region Member Variables
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        #endregion

        public string FirstName 
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
                this.NotifyPropertyChanged("FirstName");
                this.NotifyPropertyChanged("Name");
            }
        }
        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
                this.NotifyPropertyChanged("LastName");
                this.NotifyPropertyChanged("Name");
            }
        }
        public string UserID { get; set; }
        public string Password { get; set; }
        public Project CurrentProject
        {
            get;
            set;
        }

        public int ProjectID { get; set; }

        public Roles Role { get; set; }

        public string Name 
        { 
            get
            {
                return string.Format("{0}, {1}", LastName, FirstName);                
            }
        }
    }
}
