namespace eoffice_qn_kyso.Service.Models.Dto.Command
{
    using eoffice_qn_kyso.Service.Models.Include;
    using Newtonsoft.Json;

    public class UpdateXuLyDuThaoCmd
    {
        [JsonProperty("ho_so_cong_viec_id")]
        public long HoSoCongViecId { get; set; }

        [JsonProperty("xu_ly")]
        public XyLyInclude XuLy;
    }
}
