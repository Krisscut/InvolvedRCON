﻿#pragma checksum "..\..\RconInvolvedApp.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "68263C1A04CC8D6BD3D1BBAE24699975"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using Framework.UI;
using Framework.UI.Commands;
using Framework.UI.Controls;
using Framework.UI.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace RconInvolved {
    
    
    /// <summary>
    /// RconInvolvedApp
    /// </summary>
    public partial class RconInvolvedApp : Framework.UI.ElysiumApplication {
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            
            #line 6 "..\..\RconInvolvedApp.xaml"
            this.Exit += new System.Windows.ExitEventHandler(this.AppExiting);
            
            #line default
            #line hidden
            
            #line 5 "..\..\RconInvolvedApp.xaml"
            this.StartupUri = new System.Uri("Windows/SplashScreenWindow.xaml", System.UriKind.Relative);
            
            #line default
            #line hidden
            System.Uri resourceLocater = new System.Uri("/RconInvolved;component/rconinvolvedapp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RconInvolvedApp.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main() {
            RconInvolved.RconInvolvedApp app = new RconInvolved.RconInvolvedApp();
            app.InitializeComponent();
            app.Run();
        }
    }
}

