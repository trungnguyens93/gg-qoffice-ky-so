using Newtonsoft.Json;
using System.Collections.Generic;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class ThongTinDuThaoInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("trich_yeu")]
        public string TrichYeu { get; set; }

        [JsonProperty("file_du_thao")]
        public FileDinhKemInclude FileDuThao { get; set; }

        [JsonProperty("nguoi_tao")]
        public string NguoiTao { get; set; }

        [JsonProperty("ngay_tao")]
        public string NgayTao { get; set; }

        [JsonProperty("nguoi_ky")]
        public string NguoiKy { get; set; }

        [JsonProperty("ngay_ky")]
        public string NgayKy { get; set; }

        [JsonProperty("loai_van_ban")]
        public string LoaiVanBan { get; set; }

        [JsonProperty("loai_nghiep_vu_van_ban")]
        public string LoaiNghiepVuVanBan { get; set; }

        [JsonProperty("do_khan")]
        public string DoKhan { get; set; }

        [JsonProperty("do_mat")]
        public string DoMat { get; set; }

        [JsonProperty("ngon_ngu")]
        public string NgonNgu { get; set; }

        [JsonProperty("han_tra_loi_van_ban")]
        public string HanTraLoiVanBan { get; set; }

        [JsonProperty("ds_file_lien_quan")]
        public List<FileDinhKemInclude> DsFileLienQuan { get; set; }

        [JsonProperty("du_thao_thu_hoi")]
        public DuThaoThuHoiInclude DuThaoThuHoi { get; set; }

        [JsonProperty("ghi_chu")]
        public string GhiChu { get; set; }
    }
}
