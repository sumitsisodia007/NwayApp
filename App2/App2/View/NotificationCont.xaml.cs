using App2.APIService;
using App2.Model;
using App2.NativeMathods;
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
    public partial class NotificationCont : ContentPage
    {
        PartysearchMdl lstLoca = null;
       
        API api = null;
        bool isListSelected = false;
        public static double ScreenWidth=120;

        public NotificationCont()
        {
            InitializeComponent();
        }
        public NotificationCont(string name)
        {
            InitializeComponent();
            this.Title = name;
            
        }

        async void Location_focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Task.Delay(100);
            if (Device.OS == TargetPlatform.Android)
            {
              //  lblGender.IsVisible = stackGender.IsVisible = stackDate.IsVisible = false;
            }
            //statckGender.IsVisible = stackDate.IsVisible = false;
      
          //  SetPlaceHolderColor(sender);
          //  txtLocation.Placeholder = string.Empty;
            
        }
        void Location_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (!isListSelected)
            {
                txtauto.TextChanged -= Location_TextChanged;
                txtauto.Text = string.Empty;
                txtauto.TextChanged += Location_TextChanged;
            }
            imgclearlocation.IsVisible = false;
            //Location_listView.ItemsSource = null;

            if (Device.OS == TargetPlatform.Android)
            {
             //   lblGender.IsVisible = stackGender.IsVisible = stackDate.IsVisible = true;
            }
            //statckGender.IsVisible = stackDate.IsVisible = true;
            Location_listview.IsVisible = false;

            //PM 18-2-2017
            txtauto.Placeholder = "Select Party";
           
           // SetPlaceHolderColor(sender);
        
        }
        NavigationMdl navmdl = null;
        async void Location_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
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
                        _lst.Add(new PartysearchlistMdl { Party_Id = item.Party_Id, Party_Name = item.Party_Name });
                    }
                    Location_listview.ItemsSource = _lst;
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        if (_lst.Count > 0)
                        {
                            Location_listview.IsVisible = true;
                            //Location_listview.ItemsSource = _lst.Select(c => { c.txtWidth = ScreenWidth; return c; }).ToList();
                            Location_listview.HeightRequest = 40 * 5;
                        }
                        else
                        {
                            Location_listview.ItemsSource = null;
                            Location_listview.IsVisible = false;
                        }
                     //   await scrollbar.ScrollToAsync(lblGender, ScrollToPosition.Start, true);
                    });
                    imgclearlocation.IsVisible = true;
                }
                else
                {
                    Location_listview.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
               // StaticMethods.ShowToast(ex.Message);
            }
        }

        async void txtLocation_Focus(object sender, EventArgs args)
        {
            await Task.Delay(2000);
            txtauto.Unfocus();
            txtauto.Focus();
        }

        void btnClearLocation(object sender, System.EventArgs e)
        {
            txtauto.Text = string.Empty;
            imgclearlocation.IsVisible = false;
        }
        void Location_ItemTabbed(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            PartysearchlistMdl obj = null;
            try
            {
                isListSelected = true;
                Location_listview.IsVisible = false;
                txtauto.TextChanged -= Location_TextChanged;
                obj = (PartysearchlistMdl)e.Item;
                txtauto.Text = obj.Party_Name;
                //objRegistrationModel.Location = obj.name;
                //objRegistrationModel.Latitude = obj.latitude;
                //objRegistrationModel.Longitude = obj.longitude;
                //objRegistrationModel.StreetAddress = obj.name;
                //objRegistrationModel.ProfileType = 1;
                //PM 18-2-2017
                imgclearlocation.IsVisible = false;
                txtauto.Unfocus();
                
                //End PM 18-2-2017
            }
            catch (Exception ex)
            {
                //StaticMethods.ShowToast(ex.Message);
            }
            finally
            {
                obj = null;
                txtauto.TextChanged += Location_TextChanged;
            }
        }

    }
}