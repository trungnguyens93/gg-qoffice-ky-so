namespace eoffice_qn_kyso.Service.Services
{
    using System;
    using System.Net;
    using eoffice_qn_kyso.Service.Models.Dto;
    using eoffice_qn_kyso.Service.Models.Dto.Command;
    using eoffice_qn_kyso.Service.Models.Include;
    using Newtonsoft.Json;
    using RestSharp;

    public class HoSoCongViecService : BaseApiService
    {
        public HoSoCongViecService(string baseUrl, long chucDanhId, string token) : base(baseUrl, chucDanhId, token)
        {
        }

        public FileDuThaoDetailInclude GetThongTinFileDuThao(long duThaoId)
        {
            this._request.Resource = $"api/ho-so-cong-viec/du-thao/{duThaoId}";
            this._request.Method = Method.GET;

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var thongTinDuThao = JsonConvert.DeserializeObject<DuThaoDetailDto>(response.Content);

                return thongTinDuThao.FileDuThao;
            }

            return null;
        }
        
        public bool UpdateThongTinDuThao(long duThaoId, long hscvId, long fileId, string yKien)
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
            
            this._request.Resource = $"api/ho-so-cong-viec/du-thao/{duThaoId}/xu-ly";
            this._request.Method = Method.PUT;
            this._request.AddJsonBody(JsonConvert.SerializeObject(updateXuLyDuThao));

            IRestResponse response = this._restClient.Execute(this._request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }
    }
}
