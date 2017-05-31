using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


namespace iScrumMaster.Infrastructure.Models
{
    public class UserStory : NotificationObject
    {
        private double _size = 0;
        public string ID { get; set; }
        public string ExternalID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Size 
        {
            get
            {
                return this._size;
            }
            set
            {
                this._size = value;
                this.NotifyPropertyChanged("Size");
            }
        }
    }
}
