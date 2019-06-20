using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class ThongTinHoSoInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("tieu_de_ho_so")]
        public string TieuDeHoSo { get; set; }

        [JsonProperty("nguoi_lap_ho_so")]
        public string NguoiLapHoSo { get; set; }

        [JsonProperty("han_xu_ly")]
        public string HanXuLy { get; set; }
    }
}
