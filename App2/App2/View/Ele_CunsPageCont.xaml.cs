using App2.ShowModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Model;
using App2.NativeMathods;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Ele_CunsPageCont : ContentPage
    {
        public List<ShowMpebMdl> MpebMdls{ get; set; }
        public List<ShowOtherMdl> OtherMdls { get; set; }
        public double _Width = 0;
        public Ele_CunsPageCont ()
		{
			InitializeComponent ();
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                 _Width = calcScreenWidth / 4 - 20;
            }
            TodayCollationList();
        }
        public void TodayCollationList()
        {
            var electricitydata = StaticMethods.ElectricityResp;
            MpebMdls = new List<ShowMpebMdl>();
            OtherMdls= new List<ShowOtherMdl>();
            try
            {
                foreach (var mpebMdl in electricitydata.ListElectricityGroupMdl.ListElectricityMpebMdl)
                {
                    MpebMdls.Add(new ShowMpebMdl
                    {
                        TxtWidth = _Width,
                        Particular = mpebMdl.MeterType ,
                        ClosingReading = mpebMdl.Closing.ToString(),
                        OpeningReading = mpebMdl.Closing.ToString(),
                        Consumption = mpebMdl.Consumption.ToString()
                    });
                }
                foreach (var otherMdl in electricitydata.ListElectricityGroupMdl.ListElectricityOtherMdl)
                {
                    OtherMdls.Add(new ShowOtherMdl
                    {
                        TxtWidth = _Width,
                        Particular = otherMdl.MeterType,
                        ClosingReading = otherMdl.Closing.ToString(),
                        OpeningReading = otherMdl.Closing.ToString(),
                        Consumption = otherMdl.Consumption.ToString()
                    });
                }
                listView.ItemsSource = MpebMdls;
                List1.ItemsSource = OtherMdls;
            }
            catch (Exception ex)
            {


            }
        }
    }
    public class ShowMpebMdl
    {
        public double TxtWidth { get; set; }
        public string Particular { get; set; }
        public string OpeningReading { get; set; }
        public string ClosingReading { get; set; }
        public string Consumption { get; set; }
    }

    public class ShowOtherMdl
    {
        public double TxtWidth { get; set; }
        public string Particular { get; set; }
        public string OpeningReading { get; set; }
        public string ClosingReading { get; set; }
        public string Consumption { get; set; }

    }
}