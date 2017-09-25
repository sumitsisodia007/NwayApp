
using App2.APIService;
using App2.ExpandableListView;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using App2.View;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<NotificationGroup> _allGroups;
        private ObservableCollection<NotificationGroup> _expandedGroups;
        NotificationListMdl _notificationModel;
      //  API _api = null;
       
        public List<NotificationShow> Shows { get; set; }
        private double _width = 0;

       // public ObservableCollection<NotificationListMdl> _NotificationListMdl;

        public MainPage()
        {
            InitializeComponent();
           // NotificationMehods( );
        }
        public MainPage(NotificationListMdl notification)
        {
            InitializeComponent();
            _notificationModel = notification;
            NotificationMehods(notification);
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                if (!(Application.Current.MainPage.Width > 0) || !(Application.Current.MainPage.Height > 0)) return;
                var calcScreenWidth = Application.Current.MainPage.Width;
                var calcScreenHieght = Application.Current.MainPage.Height;
                _width = calcScreenWidth / 2;
            });
            
        }

        private void HeaderTapped(object sender, EventArgs args)
        {
            try
            {
                int selectedIndex = _expandedGroups.IndexOf(
                ((NotificationGroup)((Button)sender).CommandParameter));
                _allGroups[selectedIndex].Expanded = !_allGroups[selectedIndex].Expanded;
               
                UpdateListContent();
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", ex.Message, "OK");
            }
        }

        private async void NotificationMehods(NotificationListMdl notification)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushPopupAsync(new LoginSuccessPopupPage("E", "No Internet Connection"));
            }
            else
            {
               // _api = new API();
                NavigationMdl nav = new NavigationMdl
                {
                    DeviceId = StaticMethods.GetDeviceidentifier()
                };
                if (nav.DeviceId == "unknown")
                {
                    nav.DeviceId = "123456";
                }
                nav.CompanyName = EnumMaster.C21Malhar;
                nav.TagType = EnumMaster.Tagtypenotifications;
                ResponseModel rs = StaticMethods.GetLocalSavedData();
                nav.UserId = rs.UserId;

          //  _notificationModel = api.PostNotification(nav); 

            ObservableCollection<NotificationGroup> notGroup = new ObservableCollection<NotificationGroup>();
            foreach (var item in notification.ListNotificationDate)
            {
                NotificationGroup group = new NotificationGroup(item.Date + " (" + item.NotCount + ")", "o");
                foreach (var item2 in item.ListTags)
                {
                        //foreach (var item3 in item2.Notification)
                        //{
                        //    item3.SiteShortName
                        //}
                        switch (item2.Tag)
                        {
                            case "invoice_cancelletion":
                                item2.Tag = "Invoice_cancelletion";
                                break;
                            case "receipt":
                                item2.Tag = "Receipt";
                                break;
                            case "paid":
                                item2.Tag = "Paid";
                                break;
                            case "booking_entry":
                                item2.Tag = "Booking_entry";
                                break;
                            case "booking_end":
                                item2.Tag = "Booking_end";
                                break;
                            case "invoice_event":
                                item2.Tag = "Invoice_event";
                                break;
                        }

                        group.Add(new NotificationDetails() { Name =  item2.Tag, TagNotCount = ":" + item2.NotCount, TagAmount = item2.Total_Amount, TagDate = item.Date });
                    }
                    notGroup.Add(group);
                }
                _allGroups = notGroup;
                UpdateListContent();
            }
        }

        private void UpdateListContent()
        {
            try
            {
                _expandedGroups = new ObservableCollection<NotificationGroup>();
                foreach (NotificationGroup notgroup in _allGroups)
                {
                   
                    //Create new FoodGroups so we do not alter original list
                    NotificationGroup newGroup = new NotificationGroup(notgroup.Title, "o", notgroup.Expanded);
                    //Add the count of food items for Lits Header Titles to use
                    if (notgroup.Expanded)
                    {
                        foreach (NotificationDetails n in notgroup)
                        {
                            newGroup.Add(n);
                        }
                    }
                    _expandedGroups.Add(newGroup);
                }
                GroupedView.ItemsSource = _expandedGroups;
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", ex.Message, "OK");
            }

        }

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            NotificationDetails nItems = (NotificationDetails)e.Item;
            overlay.IsVisible = true;
            lblName.Text = nItems.Name;
            TodayCollationList(nItems);
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.ScaleTo(1, 50, Easing.CubicIn);
        }

        void OnOkButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
            Shows.Clear();
        }

        public void TodayCollationList(NotificationDetails notdetail)
        {
            try
            {
            Shows = new List<NotificationShow>();
            var notification = _notificationModel.ListNotificationDate.Where(o => o.Date == notdetail.TagDate).ToList();

            foreach (var item in notification)
            {
                foreach (var item2 in item.ListTags)
                {
                    foreach (var item3 in item2.Notification)
                    {
                        if (lblName.Text == "Paid" && item2.Tag == "Paid" && item.Date == notdetail.TagDate&& item3.Party_name != null)
                        {

                            string tmp;
                            if (Convert.ToInt32(item3.Party_name.Length) >= 20)
                            {
                                tmp = item3.Party_name.Substring(0, 20);
                                tmp = tmp + "...";
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            Shows.Add(new NotificationShow
                            {
                                ShowAmountReceived = item3.Amount_received,
                                TxtWidth = _width,
                                ShowSohrtName=item3.Site_short_name+":",

                                ShowPartyNameCustomerName = tmp ,
                                ShowPartyIdInvoiceId = item3.Party_id
                            });

                        }
                        else if (item2.Tag == "Receipt" && lblName.Text == "Receipt" && item.Date == notdetail.TagDate && item3.Party_name != null)
                        {
                            string tmp;
                            if (Convert.ToInt32(item3.Party_name.Length) >= 20)
                            {
                                tmp = item3.Party_name.Substring(0, 20);
                                tmp = tmp + "...";
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            Shows.Add(new NotificationShow
                            {
                                ShowAmountReceived = item3.Amount_received,
                                TxtWidth = _width,
                                ShowSohrtName = item3.Site_short_name + ":",
                                ShowPartyNameCustomerName = tmp ,
                                ShowPartyIdInvoiceId = item3.Party_id
                            });
                        }
                        else if (item2.Tag == "Invoice_cancelletion" && lblName.Text == "Invoice_cancelletion" && item.Date == notdetail.TagDate && item3.Customer_name!=null)
                        {
                            string tmp;
                            if (Convert.ToInt32(item3.Customer_name.Length) >= 20)
                            {
                                tmp = item3.Customer_name.Substring(0, 20);
                                    tmp = tmp + "...";
                                }
                            else
                            {
                                tmp = item3.Customer_name;
                            }
                            Shows.Add(new NotificationShow
                            {
                                TxtWidth = _width,
                                ShowPartyNameCustomerName = tmp ,
                                ShowAmountReceived = item3.Invoice_code,
                                ShowSohrtName = item3.Site_short_name + ":",
                                ShowPartyIdInvoiceId = item3.Customer_id,
                            });
                        }
                        else if (item2.Tag == "Booking_end" && lblName.Text == "Booking_end" && item.Date == notdetail.TagDate)
                        {
                            string tmp;
                            if (item3.Party_name != null && Convert.ToInt32(item3.Party_name.Length) >= 20)
                            {
                                tmp = item3.Party_name.Substring(0, 20);
                                tmp = tmp + "...";
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            Shows.Add(new NotificationShow
                            {
                                TxtWidth = _width,
                                ShowPartyNameCustomerName = tmp,
                                ShowSohrtName = item3.Site_short_name + ":",
                                ShowAmountReceived = item3.Invoice_code,
                                ShowPartyIdInvoiceId = item3.Customer_id,
                            });
                        }
                        else if (item2.Tag == "Booking_end" && lblName.Text == "Booking_end" && item.Date == notdetail.TagDate)
                        {
                            string tmp;
                            if (item3.Party_name != null && Convert.ToInt32(item3.Party_name.Length) >= 20)
                            {
                                tmp = item3.Party_name.Substring(0, 20);
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            Shows.Add(new NotificationShow
                            {
                                TxtWidth = _width,
                                ShowPartyNameCustomerName = tmp + "..",
                                ShowSohrtName = item3.Site_short_name + ":",
                                ShowAmountReceived = item3.Invoice_code,
                                ShowPartyIdInvoiceId = item3.Customer_id,
                            });
                        }
                    }
                }
            }
            MainlistView.ItemsSource = Shows;
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }

        private async void MainlistView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            NotificationShow obj = (NotificationShow)e.Item;
            NavigationMdl navmdl = new NavigationMdl();
           // navmdl.PartyId = obj.ShowPartyIdInvoiceId;
            navmdl.DeviceId = StaticMethods.GetDeviceidentifier();
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            if (navmdl.DeviceId == "unknown")
            {
                navmdl.DeviceId = "123456";
            }
            navmdl.CompanyName = EnumMaster.C21Malhar;
            navmdl.PartyName = obj.ShowPartyNameCustomerName;
            if (lblName.Text == "Receipt")
            {
                navmdl.PageTitle = "Receivable";
                navmdl.TagType = EnumMaster.TagtypereceivableOutstanding;
                await Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Paid")
            {
                navmdl.PageTitle = "Payable";
                navmdl.TagType = EnumMaster.TagtypepayableOutstanding;
                await Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Invoice_cancelletion")
            {
                navmdl.PageTitle = "Invoice_cancelletion";
                //navmdl.TagType = EnumMaster.TagtypeinvoiceCancelletion;
                //Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Booking_end")
            {
                navmdl.PageTitle = "Invoice_cancelletion";
                //navmdl.TagType = EnumMaster.TagtypeinvoiceCancelletion;
                //Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Booking_entry")
            {
                navmdl.PageTitle = "Invoice_cancelletion";
                //navmdl.TagType = EnumMaster.TagtypeinvoiceCancelletion;
                //Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
        }

        //private void C21_Tapped(object sender, EventArgs e)
        //{
        //    if (!(MALHAR.Text == "C21"))
        //    {
        //        MALHAR.BackgroundColor = Color.White;
        //        MALHAR.TextColor = Color.FromHex("#4472C4");
        //        C21.BackgroundColor = Color.FromHex("#4472C4");
        //        C21.TextColor = Color.White;
        //    }
        //}

        //private void MALHAR_Tapped(object sender, EventArgs e)
        //{
        //    if (!(C21.Text == "MALHAR"))
        //    {
        //        MALHAR.BackgroundColor = Color.FromHex("#4472C4");
        //        MALHAR.TextColor = Color.White;
        //        C21.BackgroundColor = Color.White;
        //        C21.TextColor = Color.FromHex("#4472C4");

        //    }
        //}
    }
}
