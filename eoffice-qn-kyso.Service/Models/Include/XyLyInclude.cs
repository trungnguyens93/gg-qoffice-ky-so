
namespace eoffice_qn_kyso.Service.Models.Include
{
    using Newtonsoft.Json;

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
