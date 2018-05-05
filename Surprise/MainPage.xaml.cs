using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Surprise
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TheNaviGate_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var i = args.SelectedItem as NavigationViewItem;
            switch (i.Tag)
            {

                case "WelcomePage":
                    TheNaviGate.Header = "Welcome to Surprise!";
                    MyFrame.Navigate(typeof(Welcome_Page));
                    break;

                case "AddPage":
                    TheNaviGate.Header = "Show me some ideas";
                    MyFrame.Navigate(typeof(Add_Page));
                    break;

                case "EditPage":
                    TheNaviGate.Header = "Let me see..";
                    MyFrame.Navigate(typeof(Manage_Page));
                    break;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TheNaviGate.Header = "Welcome to Surprise";
            MyFrame.Navigate(typeof(Welcome_Page));
        }

    }
}
