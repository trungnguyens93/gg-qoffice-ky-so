using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class VanBanDiInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("so_ky_hieu")]
        public string SoKyHieu { get; set; }
    }
}
