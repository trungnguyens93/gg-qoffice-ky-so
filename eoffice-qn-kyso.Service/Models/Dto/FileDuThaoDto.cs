namespace eoffice_qn_kyso.Service.Models.Dto
{
    using Newtonsoft.Json;

    public class FileDuThaoDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("kieu_file")]
        public string KieuFile { get; set; }

        [JsonProperty("ten_file")]
        public string TenFile { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
