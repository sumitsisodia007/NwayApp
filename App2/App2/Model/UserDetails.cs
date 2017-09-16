using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
    class UserDetails
    {
        public string username { get; set; }
        public string password { get; set; }
        public string device_id { get; set; }
        public string company_id { get; set; }
        public string party_id { get; set; }
        public string tagtype { get; set; }
        public ObservableCollection<Site_id_lst> _site_id { get; set; }
    }
    public class Site_id_lst
    {
        public int Site_id { get; set; }

    }
}
