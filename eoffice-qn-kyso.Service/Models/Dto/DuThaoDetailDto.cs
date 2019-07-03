using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace eoffice_qn_kyso.Service.Models.Dto
{
    public class DuThaoDetailDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("trich_yeu")]
        public string TrichYeu { get; set; }

        [JsonProperty("nguoi_tao")]
        public NguoiDungInclude NguoiTao { get; set; }

        [JsonProperty("ngay_tao")]
        public string NgayTao { get; set; }

        [JsonProperty("file_du_thao")]
        public FileDuThaoDetailInclude FileDuThao { get; set; }
    }
}
