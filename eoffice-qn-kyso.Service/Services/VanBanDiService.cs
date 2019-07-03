using eoffice_qn_kyso.Service.Models.Dto;
using eoffice_qn_kyso.Service.Models.Dto.Command;
using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace eoffice_qn_kyso.Service.Services
{
    public class VanBanDiService : BaseApiService
    {
        public VanBanDiService(string baseUrl, long chucDanhId, string token) : base(baseUrl, chucDanhId, token)
        {
        }

        public VanBanDiDetailDto GetThongTinVanBanDi(long vanBanDiId)
        {
            this._request.Resource = $"api/van-ban-di/{vanBanDiId}";
            this._request.Method = Method.GET;

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var thongTinVanBanDi = JsonConvert.DeserializeObject<VanBanDiDetailDto>(response.Content);

                return thongTinVanBanDi;
            }

            return null;
        }

        public bool KySoBanHanh(long vanBanDiId, long fileId, bool daKySo)
        {
            var command = new UpdateKySoVaoSoVanBanDiCmd();
            command.FileId = fileId;
            command.DaKySo = daKySo;
            
            this._request.Resource = $"api/van-ban-di/{vanBanDiId}/ky-so-ban-hanh";
            this._request.Method = Method.PUT;
            this._request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool KySoLanhDaoVanBanDi(long vanBanDiId, long fileId)
        {
            var command = new UpdateKySoLanhDaoCmd();
            command.FileVanBanId = fileId;

            this._request.Resource = $"api/van-ban-di/{vanBanDiId}/ky-so";
            this._request.Method = Method.PUT;
            this._request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public bool KySoBanHanhVanBanDiCoDuThao(long vanBanDiId, long fileId)
        {
            var command = new UpdateBanHanhVanBanDiCmd();
            command.FileVanBanId = fileId;

            this._request.Resource = $"api/van-ban-di/{vanBanDiId}/ban-hanh";
            this._request.Method = Method.PUT;
            this._request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
