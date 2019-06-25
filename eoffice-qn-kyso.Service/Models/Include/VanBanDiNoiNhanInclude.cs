using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class VanBanDiNoiNhanInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("don_vi_nhan")]
        public DonViInclude DonViNhan { get; set; }

        [JsonProperty("nguoi_xu_ly")]
        public NguoiDungInclude NguoiXuLy { get; set; }

        [JsonProperty("y_kien")]
        public string YKien { get; set; }

        [JsonProperty("han_tra_loi")]
        public string HanTraLoi { get; set; }

        [JsonProperty("trang_thai_xu_ly")]
        public TrangThaiVanBanDiInclude TrangThaiXuLy { get; set; }

        [JsonProperty("thoi_gian_xu_ly")]
        public string ThoiGianXuLy { get; set; }

        [JsonProperty("la_lien_thong")]
        public bool LaLienThong { get; set; }

        [JsonProperty("loai_doi_tuong")]
        public string LoaiDoiTuong { get; set; }

        [JsonProperty("ten_noi_nhan")]
        public string TenNoiNhan { get; set; }
    }
}
