using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace App2.Model
{
   public class CompanyTbl
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string SiteName { get; set; }
        public string SiteId { get; set; }
        public string SiteShortName{ get; set; }
        public string ImageSrc{ get; set; }
        public bool IsSiteSelected { get; set; }
    }

   
}
