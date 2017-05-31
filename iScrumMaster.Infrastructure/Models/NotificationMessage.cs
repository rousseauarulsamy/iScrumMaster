using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
namespace iScrumMaster.Infrastructure.Models
{
    [DataContract]
    public class NotificationMessage
    {
        [DataMember]
        public ActionType Action { get; set; }

        [DataMember]
        public User Owner { get; set; }

        [DataMember]
        public Project Project { get; set; }

        [DataMember]
        public Sprint CurrentSprint { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
