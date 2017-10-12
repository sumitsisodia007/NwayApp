using App2.Model;
using App2.NativeMathods;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App2.APIService;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App2.PopUpPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeftMenu : PopupPage
    {
        private List<CompanySite> menuList { get; set; }
       // private List<TempSiteIdMdl> tempchlst = null;//not now used Date 29 Sep SSS
        private string ComIndex { get; set; }
        private List<ShowCompanyNameMdl> Companyname { get; set; }
        LoginResponseMdl _data;
        private int Counter = 0;
        private static string LocalCompanyName { get; set; }
        public LeftMenu()
        {
            InitializeComponent();
            PrepareLayout();
            DrawalMenu();
        }

        public LeftMenu(LoginResponseMdl data)
        {
            InitializeComponent();
            _data = data;
            LocalCompanyName = lblupdate.Text = StaticMethods.SetCompanyName;
            Task.Delay(100);
            PickerData(data);
            //DrawalMenu();
            PrepareLayout();
        }

        private void PickerData(LoginResponseMdl res)
        {
            Companyname = new List<ShowCompanyNameMdl>();
            foreach (var item in res._permissions)
            {
                Companyname.Add(new ShowCompanyNameMdl { CompanyName = item.CompanyName });
            }
            MainPickr.ItemsSource = Companyname;
        }

        private void MainPickr_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex == -1) return;
            try
            {

                LocalCompanyName = (string)picker.Items[selectedIndex];
                
                ComIndex= selectedIndex.ToString();

                if (ComIndex == picker.SelectedItem) return;
                if (Counter ==0)
                {
                    Task.Delay(100);
                    DrawalMenu();
                }
                Counter--;
            }
            catch (Exception ex)
            {
            }
        }

        private void PrepareLayout()
        {
            if (!(Application.Current.MainPage.Width > 0) || !(Application.Current.MainPage.Height > 0)) return;
            var calcScreenWidth = Application.Current.MainPage.Width;
            var calcScreenHieght = Application.Current.MainPage.Height;
            stkMessage.HeightRequest = calcScreenHieght;
            stkMessage.WidthRequest = calcScreenWidth - 100;
            //UserModel res = StaticMethods.GetLocalSavedData();
            if (LocalCompanyName == "M.P. ENTERTAINMENT & DEVELOPERS PVT. LTD.")
            {
                MainPickr.SelectedIndex = 1;
            }
            else
            {
                MainPickr.SelectedIndex = 0;
            }
            //MainPickr.SelectedIndex = LocalCompanyName == "M.P. ENTERTAINMENT & DEVELOPERS PVT. LTD." ? 1 : 0;
        }

        public async void DrawalMenu()
        {
           await Task.Delay(200);
            Counter ++;
            menuList = new List<CompanySite>();
            var cmpTblTbl = await App.CmpDatabase.GetItemsAsync();
            if (cmpTblTbl == null)
            {
                foreach (var item in _data._permissions)
                {
                    if (LocalCompanyName != item.CompanyName) continue;
                    foreach (var item2 in item.Sites)
                    {
                        string imgesource = null;
                        imgesource = item2.Chk_id == true ? "on_btn.png" : "off_btn.png";
                        menuList.Add(new CompanySite
                        {
                            Site_id = item2.Site_id,
                            Site_name = item2.Site_name,
                            Site_short_name = item2.Site_short_name,
                            Chk_id = item2.Chk_id,
                            ImgName = imgesource
                        });
                    }
                }
            }
            else
            {
                foreach (var companyites in cmpTblTbl)
                {
                    if (companyites.CompanyName == LocalCompanyName)
                        menuList.Add(new CompanySite
                        {
                            Site_id = Convert.ToInt32(companyites.SiteId),
                            Site_name = companyites.SiteName,
                            Site_short_name = companyites.SiteShortName,
                            Chk_id = companyites.IsSiteSelected,
                            ImgName = companyites.ImageSrc
                        });
                }
            }
            NavigationList.ItemsSource = menuList.AsEnumerable().Distinct();
            lblupdate.Text = LocalCompanyName;
        }

        private void NavigationList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //  HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(2000);
            await PopupNavigation.RemovePageAsync(this);
        }

        private NavigationMdl _objNav = null;
        readonly API _api = new API();

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var loadingPage = new LoaderPage();
            await PopupNavigation.PushAsync(loadingPage);
            StaticMethods.NewRes = _data;
            var umdl = StaticMethods.GetLocalSavedData();
            //foreach (var item in _data._permissions)
            //{
            //    if (lblupdate.Text == item.CompanyName)
            //    {
            //      umdl.CompanyName=  StaticMethods.SetCompanyName = lblupdate.Text;
            //    }
            //}
           StaticMethods.SetCompanyName= umdl.CompanyName = LocalCompanyName;
            umdl.CompanyIndex = ComIndex;
            StaticMethods.SaveLocalData(umdl);

            //_objNav = new NavigationMdl();
            //var nav = _objNav.PrepareApiData();
            //var cashdetails = _api.CashFlowDetails(nav);
            //if (cashdetails.Error == "false")
            //{
            //    StaticMethods.BankRes = cashdetails;
            //}
             await Task.WhenAll(
             //Navigation.PushModalAsync(new App2.View.MasterMainPage(_data, tempchlst)),
             Navigation.PushModalAsync(new App2.View.MasterMainPage(_data)),
             PopupNavigation.RemovePageAsync(this)
            );
            await PopupNavigation.RemovePageAsync(loadingPage);
        }

        protected override bool OnBackgroundClicked()
        {
            return base.OnBackgroundClicked();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MainPickr.Focus();
            });
        }

        private async void SwitchButton_OnTapped(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    Xamarin.Forms.Image img = (Xamarin.Forms.Image)sender;
                    var selectdata = (CompanySite)img.BindingContext;
                    Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)img.Source;
                    var cmpTblTbl = await App.CmpDatabase.GetItemsAsync();

                    //  if (movementListTbl.Count == 0)
                    // string strFileName = objFileImageSource.File;
                    foreach (var item in _data._permissions)
                    {
                        foreach (var item2 in item.Sites)
                        {
                            if (item2.Site_name != selectdata.Site_name) continue;
                            foreach (var items in cmpTblTbl.Where(ml => ml.SiteName == selectdata.Site_name))
                            {
                                if (objFileImageSource == "off_btn.png")
                                {
                                    //await img.FadeTo(0, 100);
                                    img.Source = "on_btn.png";
                                    //await img.FadeTo(1, 100);
                                    items.IsSiteSelected = item2.Chk_id = true;
                                    items.ImageSrc = item2.ImgName = "on_btn.png";
                                    //foreach (var itemcmp in cmpTblTbl.Where(ml => ml.Selected == true))
                                    //{
                                    //    await App.CmpDatabase.SaveItemAsync(itemcmp);
                                    //}
                                }
                                else
                                {
                                    //await img.FadeTo(0, 100);
                                    img.Source = "off_btn.png";
                                    //await img.FadeTo(1, 100);
                                    items.IsSiteSelected = item2.Chk_id = false;
                                    items.ImageSrc = item2.ImgName = "off_btn.png";
                                    //Below Code is for checkbox animation
                                    //{
                                    //await Imgcheck.ScaleTo(1.5, 100);
                                    //Imgcheck.Source = "unchecked.png";
                                    //await Imgcheck.ScaleTo(1, 100);
                                    //}
                                }
                                await App.CmpDatabase.UpdateItemAsync(items);
                            }
                        }
                        
                    }
                });
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
        }
    }
}