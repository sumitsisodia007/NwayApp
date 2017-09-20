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
        public string User_name { get; set; }
        public string User_id { get; set; }
        public string Company_name { get; set; }
        public string Company_Id { get; set; }
        public string Device_id { get; set; }
        public string Tag_type { get; set; }
        public string Party_id { get; set; }
        public string Party_Name { get; set; }
        public string Min_Receipt_Amount { get; set; }
        public string Notification_Day_Count { get; set; }
        public string Page_Title { get; set; }
        public bool Is_Notification{ get; set; }
        public ObservableCollection<Site_id_Mdl> _site_Id { get; set; }

        public NavigationMdl PrepareAPIData()
        {
            NavigationMdl nav = new NavigationMdl();
            ObservableCollection<Site_id_Mdl> lst = new ObservableCollection<Site_id_Mdl>();
            ResponseModel res = StaticMethods.GetLocalSavedData();
            try
            {
                foreach (var item in StaticMethods._new_res._permissions)
                {
                    if (StaticMethods.Set_Company_Name == item.Company_name)
                    {
                        foreach (var item2 in item._company_site)
                        {

                            lst.Add(new Site_id_Mdl { Site_id = item2.Site_id, SiteName = item2.Site_name });
                        }
                        nav.Company_Id = item.Company_id.ToString();
                    }
                }
                nav.User_name = res.UserName;
                nav.Password = res.Password;
                nav.Device_id = res.Device_Id;
                nav.User_id = res.User_Id;
                nav.Tag_type = EnumMaster.TAGTYPENOTIFICATIONS;
                nav._site_Id = lst;
                nav.Party_id = "1";
            }
            catch (Exception ex)
            {
            }
            return nav;
        }


    }
    public class Site_id_Mdl
    {
        public int Site_id { get; set; }
        public string SiteName { get; set; }
    }
}
