using iScrumMaster.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace iScrumMaster.Service
{
    [ServiceContract(Namespace = "Silverlight", CallbackContract = typeof(IScrumSerivceCallBack))]
    public interface IScrumService
    {
        [OperationContract]
        SessionManager Login(string userName, string passcode);

        [OperationContract]
        bool Logout(User user);

        [OperationContract]
        void NotifyMembers(NotificationMessage notificationMessage);

        [OperationContract]
        Users LoadUsersForProject(Project project);

        [OperationContract]
        Sprints LoadSprintsFromProject(Project project);

        [OperationContract]
        bool SaveSprints(Sprints sprints);

        [OperationContract]
        Projects LoadProjects();

        [OperationContract]
        bool SaveProjects(Projects projects);
        
        
        [OperationContract]
        Users GetCurrentlyAvailableUsers(User user);

        [OperationContract]
        bool SaveUsers(Users users);
    }   
}
