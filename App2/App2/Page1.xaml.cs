using App2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        bool isListSelected = false;
        public Page1()
        {
            InitializeComponent();
            //yourButton.BorderRadius = Device.OnPlatform<int>(iOS: 0, Android: 1, WinPhone: 0)
        }

        private async void YourButton_Clicked(object sender, EventArgs e)
        {
            // YourButton.BorderRadius = Device.OnPlatform<int>(iOS: 0, Android: 1, WinPhone: 0);
            DemoResponse res = await SignUp();
        }
        public readonly string BaseURL = @"http://54.162.72.241/pets/";
        public async Task<DemoResponse> SignUp()
        {
            DemoResponse rs = new DemoResponse();
            StringContent content;
            try
            {
                var RestURL = BaseURL + string.Format("register.php?mem=mi@2l.com&mpa=123456&mph=1236547899&mcq=&mca=&mpc=ABC23D&mmi=&wts=WAGGINTALES");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RestURL);

                HttpResponseMessage response = await client.GetAsync(RestURL);
                if (response.IsSuccessStatusCode)
                {
                    //var dataObjects = response.Content.ReadAsStringAsync().Result;
                    //JObject jObj = JObject.Parse(dataObjects);

                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    JObject jObj = JObject.Parse(dataObjects);
                    var wtMeta = jObj["wtSuccess"]["wtMeta"];
                    var wtData = jObj["wtSuccess"]["wtData"];

                    string Status = wtMeta["wtStatus"].ToString();
                    string wt_Email = wtData["wtUserEmail"].ToString();
                    string wt_Password = wtData["wtUserPassword"].ToString();
                    string wt_UserAct_Code =wtData["wtUserActCode"].ToString();
                    string wt_Challenge_Q = wtData["wtUserChallengeQ"].ToString();
                    string wt_Challenge_A = wtData["wtUserChallengeA"].ToString();
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.AndroidSnackBar(e.Message);
            }
            finally
            {
                //content = null;
            }
            return rs;
        }
    }
}