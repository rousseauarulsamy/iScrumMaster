﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;

namespace iScrumMaster.Infrastructure.Models
{
    [XmlRoot("Sprints")]
    public class Sprints : ObservableCollection<Sprint>
    {

    }
}
