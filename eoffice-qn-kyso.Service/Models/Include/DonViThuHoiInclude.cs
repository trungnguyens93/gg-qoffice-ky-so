using Newtonsoft.Json;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class DonViThuHoiInclude
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("chon_don_vi")]
        public bool ChonDonVi { get; set; }

        [JsonProperty("so_thu_tu")]
        public int SoThuTu { get; set; }

        [JsonProperty("ten")]
        public string Ten { get; set; }
    }
}
