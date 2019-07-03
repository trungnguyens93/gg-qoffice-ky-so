using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace eoffice_qn_kyso.Service.Models.Dto
{
    public class VanBanDiDetailDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("van_ban_id")]
        public long VanBanId { get; set; }

        [JsonProperty("da_ky_so_lanh_dao")]
        public bool DaKySoLanhDao { get; set; }

        [JsonProperty("nguoi_ky")]
        public NguoiDungInclude NguoiKy { get; set; }

        [JsonProperty("ngay_ky")]
        public string NgayKy { get; set; }

        [JsonProperty("ds_file_dinh_kem")]
        public List<FileDinhKemInclude> DsFileDinhKem { get; set; }
    }
}
