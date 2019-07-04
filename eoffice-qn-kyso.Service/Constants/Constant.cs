namespace eoffice_qn_kyso.Service.Constants
{
    using System.Collections.Generic;

    public class Constant
    {
        public static class RegistryName
        {
            public static readonly string KY_SO = "qOffice-ky-so";
        }

        public static class FolderName
        {
            public static readonly string PROCESS_FOLDER = "SignedFolder";
        }

        public static class BaseUrl
        {
            public static readonly string HO_SO_CONG_VIEC_API = "baseUrlHoSoCongViecApi";
            public static readonly string VAN_BAN_DI_API = "baseUrlVanBanDiApi";
            public static readonly string HE_THONG_API = "baseUrlHeThongApi";
            public static readonly string FILE_API = "baseUrlFileApi";
        }

        public static class TypeFolder
        {
            public static readonly string INPUT = "\\input\\";
            public static readonly string OUTPUT = "\\output\\";
            public static readonly string IMAGE = "\\image\\";
        }

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
