﻿using App2.APIService;
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
        PayableNotificationMdl _payable;
        NavigationMdl navmdl;
        NavigationMdl obj_nav = null;
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
            MainMethods(nmdl);
            
        }

        private async void MainMethods(NavigationMdl nmdl)
        {
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
            if (!CrossConnectivity.Current.IsConnected)
            {

                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {

                lblChart.Text = nmdl.Party_Name + " " + EnumMaster.LblChartTitle;
                ResponseModel rs = StaticMethods.GetLocalSavedData();
                nmdl.User_id = rs.User_Id;
                _payable = await api.PayableTable(nmdl);

                ShowTotalPayble();
            }
        }

        protected override void OnAppearing()
        {
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
            obj_nav = new NavigationMdl();
            api = new API();
            try
            {
               
                    isListSelected = false;
                    if (e.NewTextValue != string.Empty)
                    {

                    //navmdl = new NavigationMdl();
                    //ResponseModel rs = StaticMethods.GetLocalSavedData();
                    //navmdl.User_id = rs.User_Id;
                    //navmdl.Device_id = StaticMethods.getDeviceidentifier();
                    //if (navmdl.Device_id == "unknown")
                    //{
                    //    navmdl.Device_id = "123456";
                    //}
                    //navmdl.Company_name = Helper.EnumMaster.C21_MALHAR;
                    //
                    //navmdl.Tag_type = "partylist";
                    
                    NavigationMdl nav = obj_nav.PrepareAPIData();
                    nav.Party_Name = e.NewTextValue;

                    lstLoca = new PartysearchMdl();
                        ObservableCollection<PartysearchlistMdl> _lst = null;
                        _lst = new ObservableCollection<PartysearchlistMdl>();
                      
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
                            _lst.Add(new PartysearchlistMdl { Party_Id = item.Party_Id, Party_Name = item.Party_Name });
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
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
                await Task.Delay(100);
            }
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

                //navmdl.Party_id = obj.Party_Id;
                //navmdl.Device_id = "32132";
                //navmdl.Company_name = EnumMaster.C21_MALHAR;
                //navmdl.Party_Name = obj.Party_Name;

                //_payable = api.PayableTable(toady_notification);
                obj_nav = new NavigationMdl();
                NavigationMdl nav = obj_nav.PrepareAPIData();
                nav.Party_id = obj.Party_Id;
                nav.Party_Name = obj.Party_Name;
                if (this.Title == "Receivable Chart")
                {
                    nav.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
                }
                else
                {
                    nav.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
                }
                //ResponseModel rs = StaticMethods.GetLocalSavedData();
                //navmdl.User_id = rs.User_Id;
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