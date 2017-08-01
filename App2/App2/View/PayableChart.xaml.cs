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
	public partial class PayableChart : ContentPage
	{
        public List<ShowPayableTotalPayble> _showpayabletotalpayblelist { get; set; }
        public double _Width = 0;
        PayableNotificationMdl _payable;
        API api = new API();
		public PayableChart ()
		{
			InitializeComponent ();
		}
        public PayableChart(NavigationMdl toady_notification)
        {
            InitializeComponent();
            _payable = api.PayableTable(toady_notification);

            if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
            {
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                LblMn.WidthRequest = LblSu.WidthRequest = LblTu.WidthRequest = LblWe.WidthRequest = _Width = calcScreenWidth / 4 - 10;
            }
            ShowTotalPayble();
        }
        public void ShowTotalPayble()
        {
            _showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            foreach (var item in _payable.ListPayablemdl)
            {
                _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Site_name, Show_Balance = item.Balance, Show_Total_cr = item.Total_cr, Show_Total_dr = item.Total_dr });
            }
            listView.ItemsSource = _showpayabletotalpayblelist;
        }
    }
}