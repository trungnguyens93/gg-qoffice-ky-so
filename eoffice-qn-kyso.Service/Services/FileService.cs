using eoffice_qn_kyso.Service.Models.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eoffice_qn_kyso.Service.Services
{
    public class FileService : BaseApiService
    {
        private string _baseUrlFileApi;

        public FileService(string baseUrl, string token) : base(baseUrl, -1, token)
        {
            this._baseUrlFileApi = baseUrl;
        }

        public long UploadFile(string folderName, string fileName, long duThaoId, long hscvId, string yKien)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(folderName + "\\output\\" + fileName);

                this._request.Resource = "api/file/upload";
                this._request.Method = Method.POST;
                this._request.AddFile("multipart/form-data", bytes, fileName);

                IRestResponse response = this._restClient.Execute(this._request);

                if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                {
                    JObject obj = JObject.Parse(response.Content);

                    var uploadResult = (JArray)obj.SelectToken("result");

                    var fileUploadInfos = JsonConvert.DeserializeObject<List<FileUploadInfoDto>>(uploadResult.ToString());

                    return fileUploadInfos[0].Id;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public bool DownloadFile(string resource, string folder, string subFolder, string fileName)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(this._baseUrlFileApi + resource, folder + "\\" + subFolder + "\\" + fileName);
                //this._request.Resource = resource;
                //this._request.Method = Method.GET;

                //var response = this._restClient.DownloadData(this._request);
                //File.WriteAllBytes(folder + subFolder + fileName, response);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
