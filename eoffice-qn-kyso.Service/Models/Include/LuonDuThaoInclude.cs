using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.Service.Models.Include
{
    public class LuonDuThaoInclude
    {
        [JsonProperty("nguoi_chuyen")]
        public NguoiDungInclude NguoiChuyen { get; set; }

        [JsonProperty("ngay_chuyen")]
        public string NgayChuyen { get; set; }

        [JsonProperty("nguoi_xu_ly")]
        public NguoiDungInclude NguoiXuLy { get; set; }

        [JsonProperty("ngay_xu_ly")]
        public string NgayXuLy { get; set; }

        [JsonProperty("thao_tac")]
        public ThaoTacInclude ThaoTac { get; set; }

        [JsonProperty("y_kien")]
        public string YKien { get; set; }
    }
}
