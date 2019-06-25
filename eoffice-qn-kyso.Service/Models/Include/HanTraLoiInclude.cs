using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class HanTraLoiInclude
    {
        [JsonProperty("ma")]
        public string Ma { get; set; }

        [JsonProperty("mo_ta")]
        public string MoTa { get; set; }

        [JsonProperty("thoi_gian")]
        public string ThoiGian { get; set; }
    }
}
