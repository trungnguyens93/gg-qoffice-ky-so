using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class SoVanBanInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("ten")]
        public string Ten { get; set; }
    }
}
