using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class TrangThaiVanBanDiInclude
    {
        [JsonProperty("ma")]
        public string Ma { get; set; }

        [JsonProperty("mo_ta")]
        public string MoTa { get; set; }
    }
}
