using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.App.Models.Include
{
    public class XyLyInclude
    {
        [JsonProperty("file_du_thao")]
        public FileDuThaoInclude FileDuThao { get; set; }

        [JsonProperty("la_ky_so")]
        public bool LaKySo { get; set; }

        [JsonProperty("ma")]
        public string Ma;

        [JsonProperty("y_kien")]
        public string YKien;
    }
}
