using App2.ExpandableListView;
using App2.Model;
using App2.NativeMathods;
using App2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var data = StaticMethods.GetLocalSavedData();
            if (data.Error == "False")
            {
                MainPage = new MasterMenuPage();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

        }
        public App(NavigationMdl mdl)
        {
            InitializeComponent();
            MainPage =new NavigationPage(new PayableChart(mdl));
        }
            

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
