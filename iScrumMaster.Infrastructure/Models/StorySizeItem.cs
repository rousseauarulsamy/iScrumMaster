using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iScrumMaster.Infrastructure.Models
{
    public class StorySizeItem : NotificationObject
    {

        public string ID { get; set; }
        public double Size 
        {
            get; 
            set;             
        }
    }
}
