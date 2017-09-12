
using App2.APIService;
using App2.ExpandableListView;
using App2.Helper;
using App2.Model;
using App2.NativeMathods;
using App2.PopUpPages;
using App2.ShowModels;
using App2.View;
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
        API api = null;
       
        public List<NotificationShow> _receivablList { get; set; }
        public double _Width = 0;

        public ObservableCollection<NotificationListMdl> _NotificationListMdl;

        public MainPage()
        {
            InitializeComponent();

                NotificationMehods();
                UpdateListContent();
          
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                if (Application.Current.MainPage.Width > 0 && Application.Current.MainPage.Height > 0)
                {
                    var calcScreenWidth = Application.Current.MainPage.Width;
                    var calcScreenHieght = Application.Current.MainPage.Height;
                    _Width = calcScreenWidth / 2;
                }
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

        private void NotificationMehods()
        {
            
            api = new API();
            NavigationMdl nav = new NavigationMdl();
            nav.Device_id = StaticMethods.getDeviceidentifier();
            if (nav.Device_id == "unknown")
            {
                nav.Device_id = "123456";
            }
            nav.Company_name = EnumMaster.C21_MALHAR;
            nav.Tag_type = EnumMaster.TAGTYPENOTIFICATIONS;
            ResponseModel rs = StaticMethods.GetLocalSavedData();
            nav.User_id = rs.User_Id;
            _notificationModel = api.PostNotification(nav);

            ObservableCollection<NotificationGroup> _not = new ObservableCollection<NotificationGroup>();
            foreach (var item in _notificationModel.ListNotificationDate)
            {
                NotificationGroup group = new NotificationGroup(item.Date + " (" + item.NotCount + ")", "o");
                foreach (var item2 in item.ListTags)
                {
                    //foreach (var item3 in item2.Notification)
                    //{
                    //    item3.Site_short_name
                    //}
                    if (item2.Tag == "invoice_cancelletion")
                    {
                        item2.Tag = "Invoice_cancelletion";
                    }
                    else if (item2.Tag == "receipt")
                    {
                        item2.Tag = "Receipt";
                    }
                    else if (item2.Tag == "paid") { item2.Tag = "Paid"; }
                    else if (item2.Tag == "booking_entry") { item2.Tag = "Booking_entry"; }
                    else if (item2.Tag == "booking_end") { item2.Tag = "Booking_end"; }
                    else if (item2.Tag == "invoice_event") { item2.Tag = "Invoice_event"; }
                
                    group.Add(new NotificationDetails() { Name =  item2.Tag, TagNotCount = ":" + item2.NotCount, Tag_Amount = item2.Total_Amount, Tag_date = item.Date });
                }
                _not.Add(group);
            }
            _allGroups = _not;
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
            NotificationDetails food = (NotificationDetails)e.Item;
            overlay.IsVisible = true;
            lblName.Text = food.Name;
            TodayCollationList(food);
            await this.ScaleTo(0.95, 50, Easing.CubicOut);
            await this.ScaleTo(1, 50, Easing.CubicIn);
        }

        void OnOKButtonClicked(object sender, EventArgs args)
        {
            overlay.IsVisible = false;
            _receivablList.Clear();
        }

        public void TodayCollationList(NotificationDetails notdetail)
        {
            try
            {
            _receivablList = new List<NotificationShow>();
            var notification = _notificationModel.ListNotificationDate.Where(o => o.Date == notdetail.Tag_date).ToList();

            foreach (var item in notification)
            {
                foreach (var item2 in item.ListTags)
                {
                    foreach (var item3 in item2.Notification)
                    {
                        if (lblName.Text == "Paid" && item2.Tag == "Paid" && item.Date == notdetail.Tag_date&& item3.Party_name != null)
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
                            _receivablList.Add(new NotificationShow
                            {
                                show_amount_received = item3.Amount_received,
                                txtWidth = _Width,
                                show_sohrtName=item3.Site_short_name+":",

                                show_party_name_customer_name = tmp ,
                                show_party_id_invoice_id = item3.Party_id
                            });

                        }
                        else if (item2.Tag == "Receipt" && lblName.Text == "Receipt" && item.Date == notdetail.Tag_date && item3.Party_name != null)
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
                            _receivablList.Add(new NotificationShow
                            {
                                show_amount_received = item3.Amount_received,
                                txtWidth = _Width,
                                show_sohrtName = item3.Site_short_name + ":",
                                show_party_name_customer_name = tmp ,
                                show_party_id_invoice_id = item3.Party_id
                            });
                        }
                        else if (item2.Tag == "Invoice_cancelletion" && lblName.Text == "Invoice_cancelletion" && item.Date == notdetail.Tag_date && item3.Customer_name!=null)
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
                            _receivablList.Add(new NotificationShow
                            {
                                txtWidth = _Width,
                                show_party_name_customer_name = tmp ,
                                show_amount_received = item3.Invoice_code,
                                show_sohrtName = item3.Site_short_name + ":",
                                show_party_id_invoice_id = item3.Customer_id,
                            });
                        }
                        else if (item2.Tag == "Booking_end" && lblName.Text == "Booking_end" && item.Date == notdetail.Tag_date)
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
                            _receivablList.Add(new NotificationShow
                            {
                                txtWidth = _Width,
                                show_party_name_customer_name = tmp,
                                show_sohrtName = item3.Site_short_name + ":",
                                show_amount_received = item3.Invoice_code,
                                show_party_id_invoice_id = item3.Customer_id,
                            });
                        }
                        else if (item2.Tag == "Booking_end" && lblName.Text == "Booking_end" && item.Date == notdetail.Tag_date)
                        {
                            string tmp;
                            if (Convert.ToInt32(item3.Party_name.Length) >= 20)
                            {
                                tmp = item3.Party_name.Substring(0, 20);
                            }
                            else
                            {
                                tmp = item3.Party_name;
                            }
                            _receivablList.Add(new NotificationShow
                            {
                                txtWidth = _Width,
                                show_party_name_customer_name = tmp + "..",
                                show_sohrtName = item3.Site_short_name + ":",
                                show_amount_received = item3.Invoice_code,
                                show_party_id_invoice_id = item3.Customer_id,
                            });
                        }
                    }
                }
            }
            MainlistView.ItemsSource = _receivablList;
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
           // navmdl.Party_id = obj.show_party_id_invoice_id;
            navmdl.Device_id = StaticMethods.getDeviceidentifier();
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            if (navmdl.Device_id == "unknown")
            {
                navmdl.Device_id = "123456";
            }
            navmdl.Company_name = EnumMaster.C21_MALHAR;
            navmdl.Party_Name = obj.show_party_name_customer_name;
            if (lblName.Text == "Receipt")
            {
                navmdl.Page_Title = "Receivable";
                navmdl.Tag_type = EnumMaster.TAGTYPERECEIVABLE_OUTSTANDING;
                await Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Paid")
            {
                navmdl.Page_Title = "Payable";
                navmdl.Tag_type = EnumMaster.TAGTYPEPAYABLE_OUTSTANDING;
                await Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Invoice_cancelletion")
            {
                navmdl.Page_Title = "Invoice_cancelletion";
                //navmdl.Tag_type = EnumMaster.TAGTYPEINVOICE_CANCELLETION;
                //Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Booking_end")
            {
                navmdl.Page_Title = "Invoice_cancelletion";
                //navmdl.Tag_type = EnumMaster.TAGTYPEINVOICE_CANCELLETION;
                //Navigation.PushAsync(new PayablePage(navmdl));
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
            else if (lblName.Text == "Booking_entry")
            {
                navmdl.Page_Title = "Invoice_cancelletion";
                //navmdl.Tag_type = EnumMaster.TAGTYPEINVOICE_CANCELLETION;
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
