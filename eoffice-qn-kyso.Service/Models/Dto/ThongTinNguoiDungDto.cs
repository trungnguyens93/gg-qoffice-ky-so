using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace eoffice_qn_kyso.Service.Models.Dto
{
    public class ThongTinNguoiDungDto
    {
        [JsonProperty("ds_chuc_danh")]
        public List<ChucDanhInclude> DsChucDanh { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("hinh_dai_dien")]
        public string HinhDaiDien { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("ngay_sinh")]
        public string NgaySinh { get; set; }

        [JsonProperty("so_dien_thoai")]
        public string SoDienThoai { get; set; }

        [JsonProperty("so_dien_thoai_khac")]
        public string SoDienThoaiKhac { get; set; }

        [JsonProperty("su_dung")]
        public bool SuDung { get; set; }

        [JsonProperty("tai_khoan")]
        public string TaiKhoan { get; set; }

        [JsonProperty("ten_nguoi_dung")]
        public string TenNguoiDung { get; set; }

        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}
