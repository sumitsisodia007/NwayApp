using App2.Model;
using App2.NativeMathods;
using App2.View;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetail { get; set; }
        public static double ScreenHeight;
        public static double ScreenWidth;
        public async static Task NavigationDetailPage(Page page)
        {
            App.MasterDetail.IsPresented = false;
            await App.MasterDetail.Detail.Navigation.PushAsync(page);
        }
        public App()
        {
            InitializeComponent();
            var data = StaticMethods.GetLocalSavedData();
            if (data.Error == "false")
            {
                MainPage = new MasterMainPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
        public App(NavigationMdl mdl)
        {
            InitializeComponent();
            MainPage = new MasterMainPage(mdl);
           // MainPage =new NavigationPage(new PayableChart(mdl));
        }
            

        protected override void OnStart()
        {
        } 

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            //ResponseModel rs = StaticMethods.GetLocalSavedData();
            //StaticMethods.NotificationCount = rs.NotCount;
        }
    }
}
