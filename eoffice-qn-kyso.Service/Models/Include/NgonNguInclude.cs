using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class NgonNguInclude
    {
        [JsonProperty("ma")]
        public string Ma { get; set; }

        [JsonProperty("ten")]
        public string Ten { get; set; }
    }
}
