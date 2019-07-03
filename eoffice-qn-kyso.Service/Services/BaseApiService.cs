using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.Service.Services
{
    public class BaseApiService
    {
        protected RestClient _restClient;
        protected RestRequest _request;

        public BaseApiService(string baseUrl, long chucDanhId, string token)
        {
            this._restClient = new RestClient(baseUrl);

            this.InitRestRequest(chucDanhId, token);
        }

        private void InitRestRequest(long chucDanhId, string token)
        {
            this._request = new RestRequest();
            this._request.AddHeader("Authorization", "Bearer " + token);

            if (chucDanhId > 0)
            {
                this._request.AddHeader("X-API-VERSION", "1");
                this._request.AddHeader("Content-Type", "application/json");
                this._request.AddHeader("chuc-danh-id", chucDanhId.ToString());
            }
        }
    }
}
