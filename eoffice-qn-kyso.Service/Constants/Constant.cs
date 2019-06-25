namespace eoffice_qn_kyso.Service.Constants
{
    using eoffice_qn_kyso.Service.Models;
    using System;
    using System.Collections.Generic;

    public class Constant
    {
        public static String FontText = "";

        public static class TrangThaiKySo
        {
            public static readonly string HO_SO_CONG_VIEC = "HO_SO_CONG_VIEC";
            public static readonly string VAN_BAN_DI = "VAN_BAN_DI";
        }

        public static class GiaiDoanKySo
        {
            public static readonly string KY_SO_LANH_DAO = "KY_SO_LANH_DAO";
            public static readonly string KY_SO_BAN_HANH = "KY_SO_BAN_HANH";
            public static readonly string BAN_HANH = "BAN_HANH";
        }

        public static class LoaiChuKy
        {
            public static readonly string CHU_KY_DON_VI = "CHU_KY_DON_VI";
            public static readonly string CHU_KY_CO_DAU = "CHU_KY_CO_DAU";
            public static readonly string CHU_KY_KHONG_DAU = "CHU_KY_KHONG_DAU";
        }

        public static class MaDacBiet
        {
            public static readonly string SO = "#SoVB";
            public static readonly string KY_HIEU_VAN_BAN = "#KyHieu";
            public static readonly string DIA_DIEM = "#DiaDiemBanHanh";
            public static readonly string NGAY_BAN_HANH = "#NgayBanHanh";
            public static readonly string CHU_KY_CO_DAU = "#ChuKyCoDau";
        }

        public static List<string> CreateStaticResource()
        {
            List<string> dsMaDacBiet = new List<string>();
            dsMaDacBiet.Add(MaDacBiet.SO);
            dsMaDacBiet.Add(MaDacBiet.DIA_DIEM);

            return dsMaDacBiet;
        }
    }
}
