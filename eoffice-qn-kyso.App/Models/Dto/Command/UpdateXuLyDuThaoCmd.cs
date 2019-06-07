using eoffice_qn_kyso.App.Models.Include;
using Newtonsoft.Json;

namespace eoffice_qn_kyso.App.Models.Dto.Command
{
    public class UpdateXuLyDuThaoCmd
    {
        [JsonProperty("ho_so_cong_viec_id")]
        public long HoSoCongViecId { get; set; }

        [JsonProperty("xu_ly")]
        public XyLyInclude XuLy;
    }
}
