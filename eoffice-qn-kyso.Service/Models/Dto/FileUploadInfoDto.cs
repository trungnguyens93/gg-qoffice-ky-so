using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Dto
{
    public class FileUploadInfoDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("file_id")]
        public long FileId { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}