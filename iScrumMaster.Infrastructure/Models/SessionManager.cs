using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
namespace iScrumMaster.Infrastructure.Models
{
    public class SessionManager
    {
        #region MemberVariables
        private Users _projectMembers = new Users();
        private Project _currentProject = null;
        private User _currentUser = null;
        private ObservableCollection<SizeItem> _items = new ObservableCollection<SizeItem>();
        private ObservableCollection<StorySizeItem> _stroyItems = new ObservableCollection<StorySizeItem>();
        #endregion

        #region Properties
        public Users Members 
        {
            get
            {
                return this._projectMembers;
            }
            set
            {
                this._projectMembers = value;
            }
        }

        public Project CurrentProject 
        {
            get
            {
                return this._currentProject;
            }
            set
            {
                this._currentProject = value;
            }
        }

        public User CurrentUser         
        {
            get
            {
                if (null == this._currentUser)
                    this._currentUser = new User();
                return this._currentUser;
            }
            set
            {
                this._currentUser = value;
            }
        }

        public MeetingType SessionType { get; set; }

        public UserStory CurrentStory { get; set; }

        public string CurrentDiscussion { get; set; }

        public ObservableCollection<SizeItem> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }

        public ObservableCollection<StorySizeItem> StoryItems
        {
            get
            {
                if (this._stroyItems == null)
                    this._stroyItems = new ObservableCollection<StorySizeItem>();
                return this._stroyItems;
            }
            set
            {
                this._stroyItems = value;
            }
        }

        #endregion

    }
}
