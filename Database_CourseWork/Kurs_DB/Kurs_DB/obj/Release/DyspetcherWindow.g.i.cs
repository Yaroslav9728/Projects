﻿#pragma checksum "..\..\DyspetcherWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "443479A26CA766E4F1ECF40F169443A1AEEC9E44"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Kurs_DB;
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


namespace Kurs_DB {
    
    
    /// <summary>
    /// DyspetcherWindow
    /// </summary>
    public partial class DyspetcherWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\DyspetcherWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\DyspetcherWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu menu;
        
        #line default
        #line hidden
        
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
            System.Uri resourceLocater = new System.Uri("/Kurs_DB;component/dyspetcherwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DyspetcherWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.menu = ((System.Windows.Controls.Menu)(target));
            return;
            case 3:
            
            #line 17 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectAllSchedule);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 18 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DepartureDate);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 19 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SuccessTrip);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 22 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem2_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 23 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem3_OnClick);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 24 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem4_OnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 25 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem5_OnClick);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 26 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem6_OnClick);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 27 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem7_OnClick);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 28 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem8_OnClick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 33 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Insert_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 34 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Insert2_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 39 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Update_Click1);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 40 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Update_Click2);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 41 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Update_Click3);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 42 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Update_Click4);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 51 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteAllTrips);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 52 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTrips);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 53 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTrips2);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 54 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTrips3);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 55 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteTrips4);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 58 "..\..\DyspetcherWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_OnClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

