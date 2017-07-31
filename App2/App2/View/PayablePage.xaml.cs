using App2.APIService;
using App2.Model;
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
	public partial class PayablePage : ContentPage
    {
        public List<ShowPayableTodayDetail> _payableshowlist { get; set; }
        public List<ShowPayableTotalPayble> _showpayabletotalpayblelist { get; set; }
        public double _Width = 0;
        public static int flag = 0;

        public List<PayableNotificationMdl> _payablenotificationdata { get; set; }
        PayableNotificationMdl _payable;
        API api = new API();
        public PayablePage ()
		{
			InitializeComponent ();
             _payable = api.PayableTable();
            flag = 1;
            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;

                LblMn.WidthRequest =LblSu.WidthRequest =LblTu.WidthRequest =LblWe.WidthRequest = _Width = calcScreenWidth / 4 - 10;
            }
            ShowPaybleToday();
            ShowTotalPayble();
        }
        public void ShowPaybleToday()
        {
            _payableshowlist = new List<ShowPayableTodayDetail>();
            foreach (var item in _payable.ListPayableNotification)
            {
                foreach (var item2 in item.ListPayablemdl)
                {
                    foreach (var item3 in item2.Notification)
                    {
                        _payableshowlist.Add(new ShowPayableTodayDetail() { txtWidth = _Width, Show_Pay_Party = item3.Party_name, Show_Pay_Outstanding = item3.Party_outstanding, Show_Amount_Received = item3.Amount_received, Show_Cur_Outstanding = item3.Current_outstanding });
                    }
                }
            }
            list_today_payble.ItemsSource = _payableshowlist;
        }

        public void ShowTotalPayble()
        {
            _showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            foreach (var item in _payable.ListPayablemdl)
            {
                _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name= item.Site_name, Show_Balance= item.Balance, Show_Total_cr= item.Total_cr, Show_Total_dr= item.Total_dr});
             
            }
            list_totalpayble.ItemsSource = _showpayabletotalpayblelist;
        }

        private void Row_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PayableChart());
        }
    }
    public class ShowPayableTodayDetail
    {
        public string Show_Pay_Party { get; set; }
        public string Show_Pay_Outstanding { get; set; }
        public string Show_Amount_Received { get; set; }
        public string Show_Cur_Outstanding { get; set; }
        public double txtWidth { get; set; }
    }
    public class ShowPayableTotalPayble
    {
        public string Show_Site_name { get; set; }
        public string Show_Total_dr { get; set; }
        public string Show_Total_cr { get; set; }
        public string Show_Balance { get; set; }
        public double txtWidth { get; set; }
    }

}