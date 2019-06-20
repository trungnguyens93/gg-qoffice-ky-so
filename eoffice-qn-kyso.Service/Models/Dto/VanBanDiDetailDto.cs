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

        [JsonProperty("so_van_ban")]
        public SoVanBanInclude SoVanBan { get; set; }

        [JsonProperty("la_van_ban_quy_pham_phap_luat")]
        public bool LaVanBanQuyPhamPhapLuat { get; set; }

        [JsonProperty("loai_van_ban")]
        public LoaiVanBanInclude LoaiVanBan { get; set; }

        [JsonProperty("ngay_ban_hanh")]
        public string NgayBanHanh { get; set; }

        [JsonProperty("so_ky_hieu")]
        public string SoKyHieu { get; set; }

        [JsonProperty("so_di")]
        public int SoDi { get; set; }

        [JsonProperty("ky_hieu_chen")]
        public string KyHieuChen { get; set; }

        [JsonProperty("ky_hieu_van_ban")]
        public KyHieuVanBanDto KyHieuVanBan { get; set; }

        [JsonProperty("dia_diem_ban_hanh")]
        public string DiaDiemBanHanh { get; set; }

        [JsonProperty("ngay_hieu_luc")]
        public string NgayHieuLuc { get; set; }

        [JsonProperty("trich_yeu")]
        public string TrichYeu { get; set; }

        [JsonProperty("loai_nghiep_vu_van_ban")]
        public LoaiNghiepVuVanBanInclude LoaiNghiepVuVanBan { get; set; }

        [JsonProperty("nguoi_ky")]
        public NguoiDungInclude NguoiKy { get; set; }

        [JsonProperty("ngay_ky")]
        public string NgayKy { get; set; }

        [JsonProperty("han_tra_loi")]
        public HanTraLoiInclude HanTraLoi { get; set; }

        [JsonProperty("ngon_ngu")]
        public NgonNguInclude NgonNgu { get; set; }

        [JsonProperty("do_khan")]
        public DoKhanInclude DoKhan { get; set; }

        [JsonProperty("do_mat")]
        public DoMatInclude DoMat { get; set; }

        [JsonProperty("kem_van_ban_giay")]
        public bool KemVanBanGiay { get; set; }

        [JsonProperty("ghi_chu")]
        public string GhiChu { get; set; }

        [JsonProperty("ds_file_dinh_kem")]
        public List<FileDinhKemInclude> DsFileDinhKem { get; set; }

        [JsonProperty("ds_co_quan_trong_tinh")]
        public List<VanBanDiNoiNhanInclude> DsCoQuanTrongTinh;

        [JsonProperty("ds_co_quan_ngoai_tinh")]
        public List<VanBanDiNoiNhanInclude> DsCoQuanNgoaiTinh { get; set; }

        [JsonProperty("ds_nguoi_nhan")]
        public List<VanBanDiNoiNhanInclude> DsNguoiNhan { get; set; }

        [JsonProperty("ds_co_quan_khac")]
        public List<VanBanDiNoiNhanInclude> DsCoQuanKhac { get; set; }

        [JsonProperty("la_noi_dung_xu_ly_rieng")]
        public bool? LaNoiDungXuLyRieng { get; set; }

        [JsonProperty("trang_thai_van_ban")]
        public TrangThaiVanBanDiInclude TrangThaiVanBan { get; set; }

        [JsonProperty("thong_tin_ho_so")]
        public ThongTinHoSoInclude ThongTinHoSo { get; set; }

        [JsonProperty("thong_tin_du_thao")]
        public ThongTinDuThaoInclude ThongTinDuThao { get; set; }

        [JsonProperty("la_thu_hoi_don_vi")]
        public bool? LaThuHoiDonVi { get; set; }
    }
}
