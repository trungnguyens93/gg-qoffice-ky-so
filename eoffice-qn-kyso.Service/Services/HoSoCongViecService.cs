namespace eoffice_qn_kyso.Service.Services
{
    using eoffice_qn_kyso.Service.Models.Dto;
    using eoffice_qn_kyso.Service.Models.Dto.Command;
    using eoffice_qn_kyso.Service.Models.Include;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class HoSoCongViecService
    {
        public FileDuThaoDetailInclude GetThongTinFileDuThao(string urlBaseHoSoCongViecApi, string chucDanhId, string duThaoId, string token)
        {
            var restClient = new RestClient(urlBaseHoSoCongViecApi);
            var request = new RestRequest($"api/ho-so-cong-viec/du-thao/{duThaoId}", Method.GET);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var thongTinDuThao = JsonConvert.DeserializeObject<DuThaoDetailDto>(response.Content);

                return thongTinDuThao.FileDuThao;
            }

            return null;
        }
        
        public bool UpdateThongTinDuThao(string urlBaseHoSoCongViecApi, string duThaoId, string chucDanhId, string hscvId, long fileId, string yKien, string token)
        {
            byte[] dataYKien = System.Convert.FromBase64String(yKien);

            // Create Object for 
            var fileDuThao = new FileDuThaoInclude
            {
                FileId = fileId
            };

            var xuLy = new XyLyInclude
            {
                FileDuThao = fileDuThao,
                LaKySo = true,
                Ma = "DONG_Y",
                YKien = System.Text.Encoding.UTF8.GetString(dataYKien)
            };

            var updateXuLyDuThao = new UpdateXuLyDuThaoCmd
            {
                HoSoCongViecId = Convert.ToInt64(hscvId),
                XuLy = xuLy
            };

            var restClient = new RestClient(urlBaseHoSoCongViecApi);
            var request = new RestSharp.RestRequest($"api/ho-so-cong-viec/du-thao/{duThaoId}/xu-ly", Method.PUT);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);
            request.AddJsonBody(JsonConvert.SerializeObject(updateXuLyDuThao));

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
