using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Model
{
   public class PartysearchMdl
    {
        public string TagType { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        [JsonProperty("list")]
        public ObservableCollection<PartysearchlistMdl> Party_List{ get; set; }

    }
    public class PartysearchlistMdl
    {
        public string Party_Name { get; set; }
        public string Party_Id { get; set; }
        public double txtWidth { get; set; }
    }
    }
