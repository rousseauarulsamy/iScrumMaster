using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace iScrumMaster.Infrastructure.Models
{
      [ServiceContract]
    public interface IScrumSerivceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void NotifyAllMembers(NotificationMessage notificationMessage);
    }
}
