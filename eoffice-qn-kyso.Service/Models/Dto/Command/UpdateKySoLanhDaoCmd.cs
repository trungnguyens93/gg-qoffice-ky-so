using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Dto.Command
{
    public class UpdateKySoLanhDaoCmd
    {
        [JsonProperty("file_van_ban_id")]
        public long FileVanBanId { get; set; }
    }
}
