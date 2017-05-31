using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


namespace iScrumMaster.Infrastructure.Models
{
    public class Sprint
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Project Project { get; set; }
        public UserStories Stories { get; set; }
    }
}
