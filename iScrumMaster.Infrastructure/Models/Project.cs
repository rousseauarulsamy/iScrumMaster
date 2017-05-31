using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


namespace iScrumMaster.Infrastructure.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public Sprints Sprints { get; set; }
        public Users TeamMembers { get; set; }
    }
}
