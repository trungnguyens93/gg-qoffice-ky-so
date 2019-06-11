using Newtonsoft.Json;
using System.Collections.Generic;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class DuThaoThuHoiInclude
    {
        [JsonProperty("ds_don_vi_thu_hoi")]
        public List<DonViThuHoiInclude> DsDonViThuHoi { get; set; }

        [JsonProperty("ly_do")]
        public string LyDo { get; set; }

        [JsonProperty("van_ban_di")]
        public VanBanDiInclude VanBanDi { get; set; }
    }
}
