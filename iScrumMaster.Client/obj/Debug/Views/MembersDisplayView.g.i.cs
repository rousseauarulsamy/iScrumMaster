﻿#pragma checksum "C:\Users\rarulsa\Source\Repos\optum-ecac-rarulsamy\iScrumMaster\iScrumMaster.Client\Views\MembersDisplayView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5BEEA140D9AB32DA5A25AEA414F5F214"
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


namespace iScrumMaster.Views {
    
    
    public partial class MembersDisplayView : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel scrumMasterPanel;
        
        internal System.Windows.Controls.TextBlock scrumMasterTitle;
        
        internal RadListBox scrumMasterList;
        
        internal System.Windows.Controls.StackPanel productOwnersPanel;
        
        internal System.Windows.Controls.TextBlock productOwnersTitle;
        
        internal RadListBox productOwnersList;
        
        internal System.Windows.Controls.StackPanel projectMembersPanel;
        
        internal System.Windows.Controls.TextBlock projectMembersTitle;
        
        internal RadListBox teamMembersList;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/iScrumMaster;component/Views/MembersDisplayView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.scrumMasterPanel = ((System.Windows.Controls.StackPanel)(this.FindName("scrumMasterPanel")));
            this.scrumMasterTitle = ((System.Windows.Controls.TextBlock)(this.FindName("scrumMasterTitle")));
            this.scrumMasterList = ((RadListBox)(this.FindName("scrumMasterList")));
            this.productOwnersPanel = ((System.Windows.Controls.StackPanel)(this.FindName("productOwnersPanel")));
            this.productOwnersTitle = ((System.Windows.Controls.TextBlock)(this.FindName("productOwnersTitle")));
            this.productOwnersList = ((RadListBox)(this.FindName("productOwnersList")));
            this.projectMembersPanel = ((System.Windows.Controls.StackPanel)(this.FindName("projectMembersPanel")));
            this.projectMembersTitle = ((System.Windows.Controls.TextBlock)(this.FindName("projectMembersTitle")));
            this.teamMembersList = ((RadListBox)(this.FindName("teamMembersList")));
        }
    }
}
