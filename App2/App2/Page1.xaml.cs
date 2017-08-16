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
            string temp = "Formandmanner.AAA";
            if (temp.Length > 4)
            {
                string tmp = temp.Substring(0,4);

                DisplayAlert("String", "Real Value is : " + temp + " and change value " + tmp, "OK");
            }
        }
     
    }
}