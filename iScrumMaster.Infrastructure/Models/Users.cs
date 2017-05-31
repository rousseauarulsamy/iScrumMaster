using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace iScrumMaster.Infrastructure.Models
{
    [XmlRoot("Users")]
    public class Users : ObservableCollection<User>
    {

    }
}
