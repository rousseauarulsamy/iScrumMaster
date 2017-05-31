using iScrumMaster.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iScrumMaster.Service
{
    public class Notification
    {
        public IScrumSerivceCallBack Client { get; set; }
        public NotificationMessage Message { get; set; }
    }
}