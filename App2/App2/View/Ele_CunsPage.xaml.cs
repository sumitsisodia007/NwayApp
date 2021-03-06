﻿using App2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ele_CunsPage : ContentPage
    {
        public List<ShowElectricityMdl> _receivablList { get; set; }
        public double _Width = 0;
        public Ele_CunsPage ()
		{
			InitializeComponent ();
            //this.Title = "ATHLETE";
            //NavigationPage.SetHasBackButton(this, true);
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;

                LblMn.WidthRequest =
                LblSu.WidthRequest =
                LblTu.WidthRequest =
                LblWe.WidthRequest = _Width = calcScreenWidth / 4 - 20;
            }
            TodayCollationList();
        }
        public void TodayCollationList()
        {

            _receivablList = new List<ShowElectricityMdl>();
            try
            {
                _receivablList.Add(new ShowElectricityMdl { TxtWidth = _Width, Particular = "MPEB", OpeningReading = "30037680.00", ClosingReading = "30144520.00", Consumption = "106,840.00" });
                _receivablList.Add(new ShowElectricityMdl { TxtWidth = _Width, Particular = "Brands Reading", OpeningReading = "3296251.00", ClosingReading = "3355989.00", Consumption = "59,738.00" });
                _receivablList.Add(new ShowElectricityMdl { TxtWidth = _Width, Particular = "Common Area Reading", OpeningReading = "749926.00", ClosingReading = "770183.00", Consumption = "20,257.00" });
                _receivablList.Add(new ShowElectricityMdl { TxtWidth = _Width, Particular = "TFM Consumption Reading", OpeningReading = "749926.00", ClosingReading = "770183.00", Consumption = "20,257.00" });
                listView.ItemsSource = _receivablList;
            }
            catch (Exception)
            {


            }
        }
       

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Ele_CunsPageCont());
        }
    }

    public class ShowElectricityMdl
    {
        public double TxtWidth { get; set; }
        public string Particular { get; set; }
        public string OpeningReading { get; set; }
        public string ClosingReading { get; set; }
        public string Consumption { get; set; }

    }
}