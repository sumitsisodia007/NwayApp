using App2.APIService;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
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
	    private PayableNotificationMdl _payable;
	    private NavigationMdl navmdl;
	    private NavigationMdl obj_nav = null;
	    private PartysearchMdl lstLoca = null;
	    private bool isListSelected = false;
        public static double ScreenWidth = 120;
	    private API api = new API();

		public PayableChart ()
		{
			InitializeComponent ();
		}

        public PayableChart(NavigationMdl nmdl)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            MainMethods(nmdl);
            
        }

        private async void MainMethods(NavigationMdl nmdl)
        {
            if ((nmdl.TagType == "payable_outstanding"))
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
            if (!CrossConnectivity.Current.IsConnected)
            {

                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {

                lblChart.Text = nmdl.PartyName + " " + EnumMaster.LblChartTitle;
                var rs = StaticMethods.GetLocalSavedData();
                nmdl.UserId = rs.UserId;
                _payable = await api.PayableTable(nmdl);

                ShowTotalPayble();
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                if (!(Application.Current.MainPage.Width > 0) || !(Application.Current.MainPage.Height > 0)) return;
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                LblMn.WidthRequest = LblSu.WidthRequest = LblTu.WidthRequest = LblWe.WidthRequest = _Width = calcScreenWidth / 4 - 10;
            }
            catch (Exception ex)
            {
            }

        }

        public void ShowTotalPayble()
        {
            try
            {
            _showpayabletotalpayblelist = new List<ShowPayableTotalPayble>();
            foreach (var item in _payable.ListPayablemdl)
            {
                _showpayabletotalpayblelist.Add(this.Title == "Payable Chart"
                    ? new ShowPayableTotalPayble()
                    {
                        TxtWidth = _Width,
                        ShowSiteName = item.Site_name,
                        ShowBalance = item.Balance,
                        ShowTotalCr = item.Total_cr,
                        ShowTotalDr = item.Total_dr
                    }
                    : new ShowPayableTotalPayble()
                    {
                        TxtWidth = _Width,
                        ShowSiteName = item.Perticular,
                        ShowBalance = item.Balance,
                        ShowTotalCr = item.Receive,
                        ShowTotalDr = item.Total_Due
                    });
            }
                listView.ItemsSource = _showpayabletotalpayblelist;
            }
            catch (Exception ex)
            {

            }
        }

        private async void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            obj_nav = new NavigationMdl();
            api = new API();
            try
            {
               
                    isListSelected = false;
                    if (e.NewTextValue != string.Empty)
                    {

                    //navmdl = new NavigationMdl();
                    //UserModel rs = StaticMethods.GetLocalSavedData();
                    //navmdl.UserId = rs.UserId;
                    //navmdl.DeviceId = StaticMethods.GetDeviceidentifier();
                    //if (navmdl.DeviceId == "unknown")
                    //{
                    //    navmdl.DeviceId = "123456";
                    //}
                    //navmdl.CompanyName = Helper.EnumMaster.C21Malhar;
                    //
                    //navmdl.TagType = "partylist";
                    
                    NavigationMdl nav =await obj_nav.PrepareApiData();
                    nav.PartyName = e.NewTextValue;

                    lstLoca = new PartysearchMdl();
                        ObservableCollection<PartysearchlistMdl> lst = null;
                        lst = new ObservableCollection<PartysearchlistMdl>();
                      
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
                    }
                    else
                    {

                        lstLoca = await api.GetParty(nav);
                    }
                        foreach (var item in lstLoca.Party_List)
                        {
                            lst.Add(new PartysearchlistMdl { Party_Id = item.Party_Id, Party_Name = item.Party_Name });
                        }
                        AutoList.ItemsSource = lst;
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            if (lst.Count > 0)
                            {
                                AutoList.IsVisible = true;
                                if (AutoList.IsVisible == true)
                                {
                                    imgLogo.IsVisible = false;
                                }
                            //AutoList.ItemsSource = _lst.Select(c => { c.TxtWidth = ScreenWidth; return c; }).ToList();
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
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
                await Task.Delay(100);
            }
        }

	    private async void txtLocation_Focus(object sender, EventArgs args)
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

                //navmdl.PartyId = obj.Party_Id;
                //navmdl.DeviceId = "32132";
                //navmdl.CompanyName = EnumMaster.C21Malhar;
                //navmdl.PartyName = obj.PartyName;

                //_payable = api.PayableTable(toady_notification);
                obj_nav = new NavigationMdl();
                NavigationMdl nav =await obj_nav.PrepareApiData();
                nav.PartyId = obj.Party_Id;
                nav.PartyName = obj.Party_Name;
                nav.TagType = this.Title == "Receivable Chart" ? EnumMaster.TagtypereceivableOutstanding : EnumMaster.TagtypepayableOutstanding;
                //UserModel rs = StaticMethods.GetLocalSavedData();
                //navmdl.UserId = rs.UserId;
                _payable =await api.PayableTable(nav);
             
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