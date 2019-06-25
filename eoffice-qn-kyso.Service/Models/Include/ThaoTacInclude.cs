using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class ThaoTacInclude
    {
        [JsonProperty("ma")]
        public string Ma { get; set; }

        [JsonProperty("mo_ta")]
        public string MoTa { get; set; }
    }
}
