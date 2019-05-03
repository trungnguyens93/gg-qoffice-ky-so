namespace eoffice_qn_kyso.Service.Constants
{
    using eoffice_qn_kyso.Service.Models;
    using System;
    using System.Collections.Generic;

    public class Constant
    {
        public static String FontText = "";

        public static List<LoaiChuKy> CreateStaticResource()
        {
            List<LoaiChuKy> loaiChuKys = new List<LoaiChuKy>();
            loaiChuKys.Add(new LoaiChuKy() { STT = 1, TenLoaiChiDao = "Số văn bản", MaDacBiet = "#So" });
            loaiChuKys.Add(new LoaiChuKy() { STT = 2, TenLoaiChiDao = "Ký hiệu văn bản", MaDacBiet = "#KyHieu" });
            loaiChuKys.Add(new LoaiChuKy() { STT = 3, TenLoaiChiDao = "Địa điểm ban hành", MaDacBiet = "#DiaDiemBanHanh" });
            loaiChuKys.Add(new LoaiChuKy() { STT = 4, TenLoaiChiDao = "Ngày ban hành", MaDacBiet = "#NgayBanHanh" });
            loaiChuKys.Add(new LoaiChuKy() { STT = 5, TenLoaiChiDao = "Chũ ký có dấu", MaDacBiet = "#ChuKyCoDau" });
            loaiChuKys.Add(new LoaiChuKy() { STT = 6, TenLoaiChiDao = "Chũ ký nháy", MaDacBiet = "#ChuKyNhay" });

            return loaiChuKys;
        }
    }
}
