﻿#pragma checksum "C:\Users\rarulsa\Source\Repos\optum-ecac-rarulsamy\iScrumMaster\iScrumMaster.Client\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5AD2291A856190F047E7FBF9A396E0AA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using iScrumMaster.Views;


namespace iScrumMaster {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal iScrumMaster.Views.HeaderView headerView;
        
        internal iScrumMaster.Views.MembersDisplayView memberDispalyView;
        
        internal System.Windows.Controls.Grid gridTabManager;
        
        internal Telerik.Windows.Controls.RadTabControl radTabManager;
        
        internal Telerik.Windows.Controls.RadTabItem tabPlanning;
        
        internal iScrumMaster.Views.SizingView sizingView;
        
        internal Telerik.Windows.Controls.RadTabItem tabRetro;
        
        internal iScrumMaster.Views.RetrospectiveView retroView;
        
        internal Telerik.Windows.Controls.RadTabItem tabManageUsers;
        
        internal iScrumMaster.Views.ManageUsersView managerUsers;
        
        internal Telerik.Windows.Controls.RadTabItem tabSettings;
        
        internal iScrumMaster.Views.SettingsView settignsView;
        
        internal System.Windows.Controls.TextBlock txtNoMeeting;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/iScrumMaster;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.headerView = ((iScrumMaster.Views.HeaderView)(this.FindName("headerView")));
            this.memberDispalyView = ((iScrumMaster.Views.MembersDisplayView)(this.FindName("memberDispalyView")));
            this.gridTabManager = ((System.Windows.Controls.Grid)(this.FindName("gridTabManager")));
            this.radTabManager = ((Telerik.Windows.Controls.RadTabControl)(this.FindName("radTabManager")));
            this.tabPlanning = ((Telerik.Windows.Controls.RadTabItem)(this.FindName("tabPlanning")));
            this.sizingView = ((iScrumMaster.Views.SizingView)(this.FindName("sizingView")));
            this.tabRetro = ((Telerik.Windows.Controls.RadTabItem)(this.FindName("tabRetro")));
            this.retroView = ((iScrumMaster.Views.RetrospectiveView)(this.FindName("retroView")));
            this.tabManageUsers = ((Telerik.Windows.Controls.RadTabItem)(this.FindName("tabManageUsers")));
            this.managerUsers = ((iScrumMaster.Views.ManageUsersView)(this.FindName("managerUsers")));
            this.tabSettings = ((Telerik.Windows.Controls.RadTabItem)(this.FindName("tabSettings")));
            this.settignsView = ((iScrumMaster.Views.SettingsView)(this.FindName("settignsView")));
            this.txtNoMeeting = ((System.Windows.Controls.TextBlock)(this.FindName("txtNoMeeting")));
        }
    }
}

