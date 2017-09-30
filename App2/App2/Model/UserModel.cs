using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class UserModel
    {
        public UserModel()
        { }
        public string TagType { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MinReceiptAmt { get; set; }
        public string NotificationDayCount { get; set; }
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceToken { get; set; }
        public string NotCount { get; set; }
        public string NotCountDate { get; set; }
        public string CompanyIndex { get; set; }
        public string CompanyName { get; set; }
      
        public async Task<CompanyTbl>  SaveLocalCompanyData(LoginResponseMdl lgnResponseMdl)
        {
            CompanyTbl tbl = new CompanyTbl();
            foreach (var item in lgnResponseMdl._permissions)
            {
                foreach (var itemSite in item.Sites)
                {
                    tbl.CompanyName = item.CompanyName;
                    tbl.SiteName = itemSite.Site_name;
                    tbl.SiteId = itemSite.Site_id.ToString();
                    tbl.SiteShortName = itemSite.Site_short_name;
                    tbl.IsSiteSelected = true;
                    tbl.ImageSrc = "on_btn.png";
                    await App.CmpDatabase.SaveItemAsync(tbl);
                }
            }
            return tbl;
        }
    }
}
