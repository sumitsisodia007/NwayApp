using App2.Helper;
using App2.NativeMathods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ObservableCollection<SiteIdMdl> SiteIdMdls { get; set; }

        public NavigationMdl PrepareApiData()
        {
            NavigationMdl nav = new NavigationMdl();
            ObservableCollection<SiteIdMdl> lst = new ObservableCollection<SiteIdMdl>();
            UserModel res = StaticMethods.GetLocalSavedData();
            try
            {
                foreach (var item in StaticMethods.NewRes._permissions)
                {
                    if (StaticMethods.SetCompanyName == null)
                    {
                        StaticMethods.SetCompanyName = "CENTURY 21 TOWN PLANNERS PVT. LTD.";
                    }
                    if (StaticMethods.SetCompanyName == item.CompanyName)
                    {
                        foreach (var item2 in item.Sites)
                        {

                            lst.Add(new SiteIdMdl { SiteId = item2.Site_id, SiteName = item2.Site_name ,ChkId = item2.Chk_id});
                        }
                        nav.CompanyId = item.CompanyId.ToString();
                    }
                }
                nav.UserName = res.UserName;
                nav.Password = res.Password;
                nav.DeviceId = res.DeviceId;
                nav.UserId = res.UserId;
                nav.TagType = EnumMaster.Tagtypenotifications;
                nav.SiteIdMdls = lst;
                nav.PartyId = "1";
            }
            catch (Exception ex)
            {
                StaticMethods.ShowToast(ex.Message);
            }
            return nav;
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
    public class TempSiteIdMdl
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteShortName { get; set; }
        public bool ChkId { get; set; }
    }

    public class ShowCompanyNameMdl
    {
        public string CompanyName { get; set; }
    }
}
