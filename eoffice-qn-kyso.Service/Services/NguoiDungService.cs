using eoffice_qn_kyso.Service.Models.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace eoffice_qn_kyso.Service.Services
{
    public class NguoiDungService
    {
        public ThongTinNguoiDungDto GetThongTinNguoiDung(string urlBaseHeThongApi, string token)
        {
            var restClient = new RestClient(urlBaseHeThongApi);
            var request = new RestRequest($"api/me", Method.GET);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var thongTinNguoiDung = JsonConvert.DeserializeObject<ThongTinNguoiDungDto>(response.Content);

                return thongTinNguoiDung;
            }

            return null;
        }
    }
}
