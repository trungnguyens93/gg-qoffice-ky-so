using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class NguoiDungInclude
    {
        [JsonProperty("chuc_danh_id")]
        public long ChucDanhId { get; set; }

        [JsonProperty("ten")]
        public string Ten { get; set; }

        [JsonProperty("chuc_vu")]
        public string ChucVu { get; set; }
    }
}
