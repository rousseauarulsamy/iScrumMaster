using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iScrumMaster.Infrastructure.Models
{
    public class SizeItem : NotificationObject
    {
        private int _userCount = 0;

        public int UserCount 
        {
            get
            {
                return this._userCount;
            }
            set
            {
                this._userCount = value;
                this.NotifyPropertyChanged("UserCount");
            }
        }
        public double Size 
        {
            get; 
            set; 
            
        }
    }
}
