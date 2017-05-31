using iScrumMaster.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
namespace iScrumMaster.Service
{
    public class ScrumService : IScrumService
    {
        private static Dictionary<User, IScrumSerivceCallBack> clientList = new Dictionary<User, IScrumSerivceCallBack>();
        private static Users allUsers = null;
        private static Dictionary<int, SessionManager> projectSessionMap = new Dictionary<int, SessionManager>();
        private static string dataFolderPath = string.Format("{0}Data\\",AppDomain.CurrentDomain.BaseDirectory);
        static ScrumService()
        {
            string usersFilePath = string.Format("{0}{1}", dataFolderPath, "Users.xml");
            allUsers = SerializationManager.FromString(File.ReadAllText(usersFilePath), typeof(Users)) as Users;
        }

        public SessionManager Login(string userName, string passcode)
        {
            SessionManager sessionManager = null;
            User user = null;
            if (allUsers != null && allUsers.Count > 0)
            {
                user = allUsers.FirstOrDefault(item => item.UserID == userName && item.Password == passcode);
                IScrumSerivceCallBack client = OperationContext.Current.GetCallbackChannel<IScrumSerivceCallBack>();
                if (user != null)
                {
                    if (!clientList.Keys.Any(item => item.UserID == user.UserID))
                    {
                        clientList.Add(user, client);
                    }
                    else
                        clientList[user] = client;
                    NotificationMessage notificationMessage = new NotificationMessage { Action = ActionType.UserJoined, Owner= user };                    
                    sessionManager = this.GetCurrentSession(user);
                    this.NotifyMembers(notificationMessage);
                }
            }
            return sessionManager;
        }

        private SessionManager GetCurrentSession(User user)
        {
            SessionManager sessionManager = null;
            if (projectSessionMap.Keys.Contains(user.ProjectID))
            {
                sessionManager = projectSessionMap[user.ProjectID];
            }
            else
            {
                sessionManager = new SessionManager { SessionType = MeetingType.None };
                projectSessionMap.Add(user.ProjectID, sessionManager);
            }
            sessionManager.CurrentUser = user;
            sessionManager.CurrentProject = user.CurrentProject;
            sessionManager.Members.Add(user);
            return sessionManager;
        }

        public bool Logout(User user)
        {
            bool status = true;
            CleanUpSession(user);
            return status;
        }

        private void CleanUpSession(User user)
        {
            lock (clientList)
            {
                var client = clientList.FirstOrDefault(item => item.Key.UserID == user.UserID);
                if (client.Key != null)
                    clientList.Remove(client.Key);
            }
            SessionManager sessionManager = this.GetCurrentSession(user);
            var member = sessionManager.Members.FirstOrDefault(item => item.UserID == user.UserID);
            sessionManager.Members.Remove(member);
            if (sessionManager.Members.Count == 0)
            {
                projectSessionMap.Remove(user.ProjectID);
            }
        }


        public void NotifyMembers(NotificationMessage notificationMessage)
        {
            IScrumSerivceCallBack currentClient = OperationContext.Current.GetCallbackChannel<IScrumSerivceCallBack>();
            var clientsToNotify = clientList.Where(item => item.Key.ProjectID == notificationMessage.Owner.ProjectID);
            this.UpdateSession(notificationMessage);
            foreach (var client in clientsToNotify)
            {
                ///Notify only client other than current client.
                if (currentClient != client.Value)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.NotifyAllMembers));
                    thread.Start(new Notification { Client = client.Value, Message = notificationMessage });
                }
            }            
        }

        public void NotifyAllMembers(object notification)
        {
            Notification notificationMessage = notification as Notification;
            if (notificationMessage != null)
            {
                try
                {
                    notificationMessage.Client.NotifyAllMembers(notificationMessage.Message);
                }
                catch (Exception ex)
                {
                    ///TODO: Log the error.
                   var clientItem = clientList.FirstOrDefault(item => item.Value == notificationMessage.Client);
                   this.CleanUpSession(clientItem.Key);                  
                }
            }
        }

        private void UpdateSession(NotificationMessage notificationMessage)
        {
            SessionManager sessionManager = this.GetCurrentSession(notificationMessage.Owner);
            switch (notificationMessage.Action)
            {
                case ActionType.SizeStory:
                    {
                        double sizeValue = 0;
                        double.TryParse(notificationMessage.Message, out sizeValue);
                        var sizeItem = sessionManager.Items.FirstOrDefault(item => item.Size == sizeValue);
                        if (sizeItem == null)
                        {
                            sessionManager.Items.Add(new SizeItem { Size = sizeValue, UserCount = 1 });
                        }
                        else
                            sizeItem.UserCount += 1;
                        sessionManager.Items.OrderBy(item => item.Size);
                    }
                    break;
                case ActionType.AddDiscussion:
                case ActionType.AddRetroComments:
                    {
                        sessionManager.CurrentDiscussion += notificationMessage.Message;
                    }
                    break;

                case ActionType.UpdateStory:
                    {
                        sessionManager.CurrentStory = SerializationManager.FromString(notificationMessage.Message,typeof(UserStory)) as UserStory;
                        sessionManager.CurrentDiscussion = string.Empty;
                        sessionManager.Items.Clear();
                    }
                    break;
                case ActionType.StartEstimation:
                    {
                        sessionManager.Items.Clear();
                    }
                    break;
                case ActionType.EndEstimation:
                    {
                        if( sessionManager.CurrentStory != null)
                        {
                            var storyItem = sessionManager.StoryItems.FirstOrDefault(item => item.ID == sessionManager.CurrentStory.ID);
                            if (storyItem == null)
                            {
                                storyItem = new StorySizeItem();
                            }                           
                            double size = 0;
                            double.TryParse(notificationMessage.Message, out size);
                            storyItem.Size = size;
                        }
                    }
                    break;

                case ActionType.StartMeeting:
                    {
                        int meetingType = 0;
                        int.TryParse(notificationMessage.Message ,out meetingType);
                        sessionManager.SessionType = (MeetingType)meetingType;
                    }
                    break;
                case ActionType.EndMeeting:
                    {
                        ///Clean up the session.
                        ///
                        projectSessionMap.Remove(notificationMessage.Owner.ProjectID);
                    }
                    break;
            }
        }

        public Users LoadUsersForProject(Project project)
        {
            Users users = new Users();
            if (null != project)
            {
                var projectUsers = allUsers.Where(user => user.ProjectID == project.ID);
                foreach (var user in projectUsers)
                {
                    users.Add(user);
                }
            }
            return users;
        }
        
        public Users GetCurrentlyAvailableUsers(User currentUser)
        {
            Users users = new Users();
            ///Select except the current user.
            var usersFromProject = clientList.Where(item => item.Key.ProjectID == currentUser.ProjectID && item.Key.UserID != currentUser.UserID);
            foreach (var item in usersFromProject)
            {              
                users.Add(item.Key);
            }
            return users;
        }
        public bool SaveUsers(Users users)
        {
            bool status = true;
            string usersFilePath = string.Format("{0}{1}", dataFolderPath, "Users.xml");
            allUsers = SerializationManager.FromString(File.ReadAllText(usersFilePath), typeof(Users)) as Users;
            if (allUsers != null)
            {
                foreach(var user in users)
                {
                    var existUser = allUsers.FirstOrDefault(item => item.UserID == user.UserID);
                    if (existUser != null)
                        allUsers.Remove(existUser);
                    allUsers.Add(user);
                }
                try
                {
                    File.WriteAllText(usersFilePath, SerializationManager.ToString(allUsers),Encoding.UTF8);
                }
                catch (Exception ex)
                {                    
                    throw;
                }
            }
            return status;
        }


        public Sprints LoadSprintsFromProject(Project project)
        {
            throw new NotImplementedException();
        }

        public bool SaveSprints(Sprints sprints)
        {
            throw new NotImplementedException();
        }

        public Projects LoadProjects()
        {
            throw new NotImplementedException();
        }

        public bool SaveProjects(Projects projects)
        {
            throw new NotImplementedException();
        } 
    }
}
