namespace eoffice_qn_kyso.Service.Constants
{
    using System.Collections.Generic;

    public class Constant
    {
        public static class BaseUrl
        {
            public static readonly string HO_SO_CONG_VIEC_API = "baseUrlHoSoCongViecApi";
            public static readonly string VAN_BAN_DI_API = "baseUrlVanBanDiApi";
            public static readonly string HE_THONG_API = "baseUrlHeThongApi";
            public static readonly string FILE_API = "baseUrlFileApi";
        }

        public static class TrackingStatus
        {
            public static readonly string TAO_THU_MUC_CHUNG = "Tao thu muc dung chung";
            public static readonly string LAY_THONG_TIN_HO_SO_CONG_VIEC = "Lay thogn tin ho so cong viec";
            public static readonly string LAY_THONG_TIN_VAN_BAN_DI = "Lay thong tin van ban di";
            public static readonly string DOWNLOAD_FILE_DU_THAO = "Download file du thao";
            public static readonly string LAY_THONG_TIN_CHUC_DANH = "Lay thong tin chuc danh";
            public static readonly string TAO_ANH_KY_SO = "Tao anh ky so";
            public static readonly string CONVERT_WORD_TO_PDF = "Convert word to pdf";
            public static readonly string KY_SO = "Ky so";
            public static readonly string UPLOAD_FILE_DU_THAO = "Upload file du thao";
            public static readonly string CAP_NHAP_THONG_TIN = "Cap nhap thong tin";
            public static readonly string XOA_FOLDER_XU_LU = "Xoa folder xu ly";
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
