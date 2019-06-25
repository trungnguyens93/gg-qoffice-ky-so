using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class LoaiVanBanInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("ten")]
        public string Ten { get; set; }
    }
}
