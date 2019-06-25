using eoffice_qn_kyso.Service.Models.Dto;
using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using RestSharp;

namespace eoffice_qn_kyso.Service.Services
{
    public class NguoiDungService
    {
        public ChucDanhInclude GetThongTinNguoiDung(string urlBaseHeThongApi, string chucDanhId, string token)
        {
            var restClient = new RestClient(urlBaseHeThongApi);
            var request = new RestRequest($"api/client/chuc-danh/{chucDanhId}", Method.GET);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var thongTinNguoiDung = JsonConvert.DeserializeObject<ChucDanhInclude>(response.Content);

                return thongTinNguoiDung;
            }

            return null;
        }
    }
}
