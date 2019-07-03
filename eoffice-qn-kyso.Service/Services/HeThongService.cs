using eoffice_qn_kyso.Service.Models.Dto;
using eoffice_qn_kyso.Service.Models.Include;
using Newtonsoft.Json;
using RestSharp;

namespace eoffice_qn_kyso.Service.Services
{
    public class HeThongService : BaseApiService
    {
        public HeThongService(string baseUrl, long chucDanhId, string token) : base(baseUrl, chucDanhId, token)
        {
        }

        public ChucDanhInclude GetThongTinChucDanh(long chucDanhId)
        {
            this._request.Resource = $"api/client/chuc-danh/{chucDanhId}";
            this._request.Method = Method.GET;

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var thongTinNguoiDung = JsonConvert.DeserializeObject<ChucDanhInclude>(response.Content);

                return thongTinNguoiDung;
            }

            return null;
        }
    }
}
