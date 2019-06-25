using eoffice_qn_kyso.Service.Models.Dto;
using eoffice_qn_kyso.Service.Models.Dto.Command;
using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace eoffice_qn_kyso.Service.Services
{
    public class VanBanDiService
    {
        public VanBanDiDetailDto GetThongTinVanBanDi(string urlBaseVanBanDiApi, string vanBanDiId, string chucDanhId, string token)
        {
            var restClient = new RestClient(urlBaseVanBanDiApi);
            var request = new RestRequest($"api/van-ban-di/{vanBanDiId}", Method.GET);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var thongTinVanBanDi = JsonConvert.DeserializeObject<VanBanDiDetailDto>(response.Content);

                return thongTinVanBanDi;
            }

            return null;
        }

        public bool KySoBanHanh(string urlBaseVanBanDiApi, string vanBanDiId, string chucDanhId, long fileId, bool daKySo, string token)
        {
            var command = new UpdateKySoVaoSoVanBanDiCmd();
            command.FileId = fileId;
            command.DaKySo = daKySo;

            var restClient = new RestClient(urlBaseVanBanDiApi);
            var request = new RestRequest($"api/van-ban-di/{vanBanDiId}/ky-so-ban-hanh", Method.PUT);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);
            request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool KySoLanhDaoVanBanDi(string urlBaseVanBanDiApi, string vanBanDiId, string chucDanhId, long fileId, string token)
        {
            var command = new UpdateKySoLanhDaoCmd();
            command.FileVanBanId = fileId;

            var restClient = new RestClient(urlBaseVanBanDiApi);
            var request = new RestRequest($"api/van-ban-di/{vanBanDiId}/ky-so", Method.PUT);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);
            request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool KySoBanHanhVanBanDiCoDuThao(string urlBaseVanBanDiApi, string vanBanDiId, string chucDanhId, long fileId, string token)
        {
            var command = new UpdateBanHanhVanBanDiCmd();
            command.FileVanBanId = fileId;

            var restClient = new RestClient(urlBaseVanBanDiApi);
            var request = new RestRequest($"api/van-ban-di/{vanBanDiId}/ban-hanh", Method.PUT);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);
            request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
