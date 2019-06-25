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

        [JsonProperty("loai_nghiep_vu_van_ban")]
        public LoaiNghiepVuVanBanInclude LoaiNghiepVuVanBan { get; set; }

        [JsonProperty("loai_van_ban")]
        public LoaiVanBanInclude LoaiVanBan { get; set; }

        [JsonProperty("nguoi_tao")]
        public NguoiDungInclude NguoiTao { get; set; }

        [JsonProperty("ngay_tao")]
        public string NgayTao { get; set; }

        [JsonProperty("ngon_ngu")]
        public NgonNguInclude NgonNgu { get; set; }

        [JsonProperty("han_tra_loi_van_ban")]
        public string HanTraLoiVanBan { get; set; }

        [JsonProperty("ghi_chu")]
        public string GhiChu { get; set; }

        [JsonProperty("do_khan")]
        public DoKhanInclude DoKhan { get; set; }

        [JsonProperty("do_mat")]
        public DoMatInclude DoMat { get; set; }

        [JsonProperty("ds_file_lien_quan")]
        public List<FileLienQuanInclude> DsFileLienQuan { get; set; }

        [JsonProperty("file_du_thao")]
        public FileDuThaoDetailInclude FileDuThao { get; set; }

        [JsonProperty("du_thao_thu_hoi")]
        public DuThaoThuHoiInclude DuThaoThuHoi { get; set; }
        
        [JsonProperty("ds_luon_du_thao")]
        public List<LuonDuThaoInclude> DsLuonDuThao { get; set; }
    }
}
