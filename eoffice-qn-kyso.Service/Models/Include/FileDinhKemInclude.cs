using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class FileDinhKemInclude
    {
        [JsonProperty("file_id")]
        public long FileId { get; set; }

        [JsonProperty("kieu_file")]
        public string KieuFile { get; set; }

        [JsonProperty("ten_file")]
        public string TenFile { get; set; }

        [JsonProperty("la_van_ban_chinh")]
        public bool LaVanBanChinh { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
