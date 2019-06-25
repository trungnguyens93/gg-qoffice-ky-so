using Newtonsoft.Json;
using System.Collections.Generic;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class ChucDanhInclude
    {
        [JsonProperty("chu_ky_co_dau")]
        public string ChuKyCoDau { get; set; }

        [JsonProperty("chu_ky_khong_dau")]
        public string ChuKyKhongDau { get; set; }

        [JsonProperty("chu_ky_nhay")]
        public string ChuKyNhay { get; set; }

        [JsonProperty("chuc_danh_chinh")]
        public bool ChucDanhChinh { get; set; }

        [JsonProperty("chuc_danh_id")]
        public long ChucVuId { get; set; }

        [JsonProperty("don_vi_id")]
        public long DonViId { get; set; }

        [JsonProperty("ds_quyen")]
        public List<string> DsQuyen { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("ma_chuc_danh")]
        public string MaChucVu { get; set; }

        [JsonProperty("nguoi_dung_id")]
        public long NguoiDungId { get; set; }

        [JsonProperty("nguoi_nhan_van_ban")]
        public bool NguoiNhanVanBan { get; set; }

        [JsonProperty("nhan_vien_ban_cho_don_vi")]
        public bool NhanVanBanChoDonVi { get; set; }

        [JsonProperty("nhan_van_ban_cho_phong_ban")]
        public bool NhanVanBanChoPhongBan { get; set; }

        [JsonProperty("phong_ban_id")]
        public long PhongBanId { get; set; }

        [JsonProperty("so_thu_tu")]
        public int SoThuTu { get; set; }

        [JsonProperty("su_dung")]
        public bool SuDung { get; set; }

        [JsonProperty("ten_chuc_vu")]
        public string TenChucVu { get; set; }

        [JsonProperty("ten_don_vi")]
        public string TenDonVi { get; set; }

        [JsonProperty("ten_nguoi_dung")]
        public string TenNguoiDung { get; set; }

        [JsonProperty("ten_phong_ban")]
        public string TenPhongBan { get; set; }

        [JsonProperty("ten_vai_tro")]
        public string TenVaiTro { get; set; }

        [JsonProperty("vai_tro_id")]
        public long VaiTroId { get; set; }
    }
}
