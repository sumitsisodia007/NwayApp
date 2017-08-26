using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.ShowModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        NavigationMdl navmdl;
        PartysearchMdl lstLoca = null;
        bool isListSelected = false;
        public static double ScreenWidth = 120;
        API api = new API();
		public PayableChart ()
		{
			InitializeComponent ();
		}
        public PayableChart(NavigationMdl nmdl)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            Device.BeginInvokeOnMainThread(async () => { 
            if ((nmdl.Tag_type == "payable_outstanding"))
            {
                this.Title = "Payable Chart";
                this.BackgroundColor = Color.FromHex("#ED7D31");
                LblSu.BackgroundColor = LblMn.BackgroundColor = LblTu.BackgroundColor = LblWe.BackgroundColor = Color.FromHex("#ED7D31");
            }
            else
            {
                this.Title = "Receivable Chart";
                this.BackgroundColor = Color.FromHex("#A3C1E5");
                LblSu.BackgroundColor = LblMn.BackgroundColor = LblTu.BackgroundColor = LblWe.BackgroundColor = Color.FromHex("#A3C1E5");
            }
            lblChart.Text = nmdl.Party_Name + " " + EnumMaster.LblChartTitle;
        
                _payable =await  api.PayableTable(nmdl);
   
            //_payable = api.PayableTable(nmdl);
            try
            {
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;
                    LblMn.WidthRequest = LblSu.WidthRequest = LblTu.WidthRequest = LblWe.WidthRequest = _Width = calcScreenWidth / 4 - 10;
                }
            }
            catch (Exception ex)
            {
            }
            
            ShowTotalPayble();
            });
        }
        public void ShowTotalPayble()
        {
            try
            {
            _showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            foreach (var item in _payable.ListPayablemdl)
            {
                if (this.Title == "Payable Chart")
                {
                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Site_name, Show_Balance = item.Balance, Show_Total_cr = item.Total_cr, Show_Total_dr = item.Total_dr });
                }
                else
                {
                    _showpayabletotalpayblelist.Add(new ShowPayableTotalPayble() { txtWidth = _Width, Show_Site_name = item.Perticular, Show_Balance = item.Balance, Show_Total_cr = item.Receive, Show_Total_dr = item.Total_Due });
                }
                
            }
                listView.ItemsSource = _showpayabletotalpayblelist;
            }
            catch (Exception ex)
            {

            }
        }

        private async void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                isListSelected = false;
                if (e.NewTextValue != string.Empty)
                {
                    navmdl = new NavigationMdl();
                    navmdl.User_id = "1";
                    navmdl.Device_id = "123456";//StaticMethods.getDeviceidentifier();
                    navmdl.Company_name = Helper.EnumMaster.C21_MALHAR;
                    navmdl.Party_Name = e.NewTextValue;
                    navmdl.Tag_type = "partylist";
                    lstLoca = new PartysearchMdl();
                    ObservableCollection<PartysearchlistMdl> _lst = null;
                    _lst = new ObservableCollection<PartysearchlistMdl>();
                    api = new API();
                    lstLoca = await api.GetParty(navmdl);

                    foreach (var item in lstLoca.Party_List)
                    {
                        _lst.Add(new PartysearchlistMdl { Party_Id = item.Party_Id, Party_Name = item.Party_Name});
                    }
                    AutoList.ItemsSource = _lst;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        if (_lst.Count > 0)
                        {
                            AutoList.IsVisible = true;
                            if (AutoList.IsVisible == true)
                            {
                                imgLogo.IsVisible = false;
                            }
                            //AutoList.ItemsSource = _lst.Select(c => { c.txtWidth = ScreenWidth; return c; }).ToList();
                            AutoList.HeightRequest = 40 * 5;
                        }
                        else
                        {
                            AutoList.ItemsSource = null;
                            AutoList.IsVisible = false;
                        }
                        await scrollbar.ScrollToAsync(txtAuto, ScrollToPosition.Start, true);
                    });

                }
                else
                {
                    AutoList.IsVisible = false;
                    imgLogo.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                // StaticMethods.ShowToast(ex.Message);
            }
        }

        private async void txtAuto_Focused(object sender, FocusEventArgs e)
        {
            await Task.Delay(100);

        }

        async void txtLocation_Focus(object sender, EventArgs args)
        {
            await Task.Delay(2000);
            txtAuto.Unfocus();
            txtAuto.Focus();
        }

        private void txtAuto_Unfocused(object sender, FocusEventArgs e)
        {
            if (!isListSelected)
            {
                txtAuto.TextChanged -= txtAuto_TextChanged;
                txtAuto.Text = string.Empty;
                txtAuto.TextChanged += txtAuto_TextChanged;
            }

            AutoList.IsVisible = false;
            imgLogo.IsVisible = true;
            //PM 18-2-2017
            txtAuto.Placeholder = "Select Party";

        }

        private async void AutoList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            PartysearchlistMdl obj = null;
            navmdl = new NavigationMdl();
            try
            {
                isListSelected = true;
                AutoList.IsVisible = false;
                txtAuto.TextChanged -= txtAuto_TextChanged;
                txtAuto.Unfocus();
                obj = (PartysearchlistMdl)e.Item;

                navmdl.Party_id = obj.Party_Id;
                navmdl.Device_id = "32132";
                navmdl.Company_name = EnumMaster.C21_MALHAR;
                navmdl.Party_Name = obj.Party_Name;

                //_payable = api.PayableTable(toady_notification);

                if (this.Title == "Receivable Chart")
                {
                    navmdl.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
                }
                else
                {
                    navmdl.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
                }
              
                    _payable =await api.PayableTable(navmdl);
             
                //_payable = api.PayableTable(navmdl);
                ShowTotalPayble();
                lblChart.Text = obj.Party_Name + " " + EnumMaster.LblChartTitle;
            }
            catch (Exception ex)
            {
                //StaticMethods.ShowToast(ex.Message);
            }
            finally
            {
                obj = null;
                txtAuto.TextChanged += txtAuto_TextChanged;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}