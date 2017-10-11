using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App2.Model
{
   public class ElectricityMdl
    {
        [JsonProperty("tagtype")]
        public string Tagtype { get; set; }
        [JsonProperty("error")]
        public bool Error { get; set; }
        [JsonProperty("list")]
        public ElectricityGroupMdl ListElectricityGroupMdl { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

    }

    public class ElectricityGroupMdl
    {
        [JsonProperty("mpeb")]
        public ObservableCollection<ElectricityMpebMdl> ListElectricityMpebMdl { get; set; }

        [JsonProperty("mpeb_total_consumption")]
        public int MpebTotalConsumption { get; set; }

        [JsonProperty("other")]
        public ObservableCollection<ElectricityOtherMdl> ListElectricityOtherMdl { get; set; }

        [JsonProperty("other_total_consumption")]
        public int OtherTotalConsumption { get; set; }
    }

    public class ElectricityMpebMdl
    {
        [JsonProperty("meter_type")]
        public string MeterType { get; set; }
        [JsonProperty("opening")]
        public int Opening { get; set; }
        [JsonProperty("closing")]
        public int Closing { get; set; }
        [JsonProperty("consumption")]
        public int Consumption { get; set; }
    }

    public class ElectricityOtherMdl
    {
        [JsonProperty("meter_type")]
        public string MeterType { get; set; }
        [JsonProperty("opening")]
        public int Opening { get; set; }
        [JsonProperty("closing")]
        public int Closing { get; set; }
        [JsonProperty("consumption")]
        public int Consumption { get; set; }
    }
}
