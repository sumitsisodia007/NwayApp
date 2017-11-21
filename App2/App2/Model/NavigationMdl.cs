using App2.Helper;
using App2.NativeMathods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.APIService;

namespace App2.Model
{
   public class NavigationMdl
    {
        public NavigationMdl()
        {
        }
        public string Password{ get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string DeviceId { get; set; }
        public string TagType { get; set; }
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public string MinReceiptAmount { get; set; }
        public string NotificationDayCount { get; set; }
        public string PageTitle { get; set; }
        public bool IsNotification{ get; set; }
        public string Tokan { get; set; }
        public int CancelDayCount{ get; set; }
        public int ExpireDayCount { get; set; }
        public ObservableCollection<SiteIdMdl> SiteIdMdls { get; set; }

        public async Task<NavigationMdl> PrepareApiData()
        {
            NavigationMdl nav = new NavigationMdl();
            ObservableCollection<SiteIdMdl> lst = new ObservableCollection<SiteIdMdl>();
            var cmpTblTbl = await App.CmpDatabase.GetItemsAsync();
            var res = StaticMethods.GetLocalSavedData();
            try
            {
                //foreach (var item in StaticMethods.NewRes._permissions)
                //{
                //    if (StaticMethods.SetCompanyName == null)
                //    {
                //        StaticMethods.SetCompanyName = res.CompanyName;
                //    }
                //    if (StaticMethods.SetCompanyName == item.CompanyName)
                //    {
                //        foreach (var item2 in item.Sites)
                //        {

                //            lst.Add(new SiteIdMdl { SiteId = item2.Site_id, SiteName = item2.Site_name ,ChkId = item2.Chk_id});
                //        }
                //        nav.CompanyId = item.CompanyId.ToString();
                //    }
                //}
                foreach (var item in cmpTblTbl)
                {
                    if (StaticMethods.SetCompanyName == null)
                    {
                        StaticMethods.SetCompanyName = res.CompanyName;
                    }
                    if (StaticMethods.SetCompanyName == item.CompanyName)
                    {
                      
                            lst.Add(new SiteIdMdl { SiteId =Convert.ToInt32(item.SiteId), SiteName = item.SiteName, ChkId = item.IsSiteSelected});
                  
                        nav.CompanyId = item.CompanyID.ToString();
                    }
                }
                nav.UserName = res.UserName;
                nav.Password = res.Password;
                nav.DeviceId = res.DeviceId;
                nav.UserId = res.UserId;
                nav.SiteIdMdls = lst;
                nav.PartyId = "1";
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return nav;
        }
        private NavigationMdl _objNav = null;
        readonly API _api = new API();
        public async void SetBedge()
        {
            try
            {
                UserModel rs = StaticMethods.GetLocalSavedData();
                _objNav = new NavigationMdl();
                NavigationMdl nav =await PrepareApiData();
               // var notificationModel = _api.PostNotification(nav);
                //var cashdetails = _api.CashFlowDetails(nav);
                //var electrCons = _api.ElectricityCuns(nav);
                //if (electrCons.Error== false)
                //{
                //    StaticMethods.ElectricityResp = electrCons;
                //}
                //if (cashdetails.Error == "false")
                //{
                //    StaticMethods.BankRes = cashdetails;
                //}
                nav.Tokan = StaticMethods.GetTokan();
                var home = _api.MainHomeMdl(nav);
              
                if (home.error == false)
                {
                      StaticMethods.StaticHome = home;
                }
               // await Task.Delay(700);
                foreach (var item in home.ListHomeDetails)
                {
                  //  rs.NotCount = item.notificationCount.ToString();
                }
                
                //var d2 = DateTime.Now.ToString("dd-MMM-yyyy");
                //string dateChk = null;
                //string notcount = "0";

                //foreach (var item in notificationModel.ListNotificationDate)
                //{
                //    dateChk = item.Date;
                //    notcount = item.NotCount;
                //    break;
                //}
                //rs.NotCount = d2.ToString() == dateChk ? notcount : "0";
                StaticMethods.SaveLocalData(rs);

            }
            catch (Exception e)
            {
            }
        }
    }

    public class SiteIdMdl
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteShortName { get; set; }
        public bool ChkId { get; set; }
        public string ImgName { get; set; }
    }
    //public class TempSiteIdMdl
    //{
    //    public int SiteId { get; set; }
    //    public string SiteName { get; set; }
    //    public string SiteShortName { get; set; }
    //    public bool ChkId { get; set; }
    //}

    public class ShowCompanyNameMdl
    {
        public string CompanyName { get; set; }
    }
}
