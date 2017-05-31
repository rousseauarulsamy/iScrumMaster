using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
namespace iScrumMaster.Infrastructure.Models
{
    public enum Roles : int
    {
        Administrator,
        ProductOwner,
        ScrumMaster,
        BusinessAnalyst,
        DEVMember,
        QAMember
    }
    public enum ActionType : int
    {
        StartEstimation,
        EndEstimation,
        SizeStory,
        AddRetroComments,
        AddDiscussion,
        UserJoined,
        UserLeft,
        UpdateStory,
        StartMeeting,
        EndMeeting
    }

    public enum MeetingType : byte
    {
        None,
        Planning,
        Retrospective
    }
}
