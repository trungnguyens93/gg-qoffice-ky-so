using eoffice_qn_kyso.Service.Models.Dto;
using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace eoffice_qn_kyso.Service.Services
{
    public class VanBanDiService
    {
        public FileDuThaoDetailInclude GetThongTinFileDuThao(string urlBaseVanBanDiApi, string chucDanhId, string duThaoId, string token)
        {
            var restClient = new RestClient(urlBaseVanBanDiApi);
            var request = new RestRequest($"api/van-ban-di/du-thao/{duThaoId}", Method.GET);
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
    }
}
