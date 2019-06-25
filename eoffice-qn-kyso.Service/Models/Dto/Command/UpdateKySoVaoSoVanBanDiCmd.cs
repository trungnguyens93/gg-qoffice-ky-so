using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Dto.Command
{
    public class UpdateKySoVaoSoVanBanDiCmd
    {
        [JsonProperty("file_id")]
        public long FileId { get; set; }

        [JsonProperty("da_ky_so")]
        public bool DaKySo { get; set; }
    }
}
