using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;


namespace iScrumMaster.Infrastructure.Models
{
    [XmlRoot("Porjects")]
    public class Projects : ObservableCollection<Project>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public Sprints Sprints { get; set; }
        public Users TeamMembers { get; set; }
    }
}
