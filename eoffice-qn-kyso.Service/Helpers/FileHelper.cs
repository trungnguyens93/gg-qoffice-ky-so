namespace eoffice_qn_kyso.Service.Helpers
{
    using eoffice_qn_kyso.Service.Models.Dto;
    using eoffice_qn_kyso.Service.Models.Dto.Command;
    using eoffice_qn_kyso.Service.Models.Include;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestSharp;
    using Spire.Doc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    public class FileHelper
    {
        public static string GetFileNameFromUrl(string url)
        {
            var result = string.Empty;

            string[] arrStr = url.Split('/');
            result = arrStr[arrStr.Length - 1];

            return result;
        }

        public static string ConvertWordToPdf(string path, string fileName)
        {
            Document document = new Document();
            try
            {
                var result = string.Empty;

                string[] arrStr = fileName.Split('.');
                result = String.Concat(arrStr[0], ".", "pdf");

                document.LoadFromFile(path + "\\" + "input" + "\\" + fileName);
                document.SaveToFile(path + "\\" + "input" + "\\" + result, FileFormat.PDF);

                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Upload file va cap nhap thong tin du thao
        public static bool UpdateFile(string urlBaseFileApi, string urlBaseHoSoCongViecApi, string folder, string fileName, string duThaoId, string chucDanhId, string hscvId, string yKien, string token)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(folder + "\\" + "output" + "\\" + fileName);

                var restClient = new RestClient(urlBaseFileApi);
                var request = new RestSharp.RestRequest("api/file/upload", Method.POST);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddFile("multipart/form-data", bytes, fileName);

                IRestResponse response = restClient.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.Content))
                {
                    JObject obj = JObject.Parse(response.Content);

                    var uploadResult = (JArray)obj.SelectToken("result");

                    var fileUploadInfos = JsonConvert.DeserializeObject<List<FileUploadInfoDto>>(uploadResult.ToString());

                    // Create Object for 
                    var fileDuThao = new FileDuThaoInclude
                    {
                        FileId = fileUploadInfos[0].Id
                    };

                    var xuLy = new XyLyInclude
                    {
                        FileDuThao = fileDuThao,
                        LaKySo = true,
                        Ma = "DONG_Y",
                        YKien = yKien
                    };

                    var updateXuLyDuThao = new UpdateXuLyDuThaoCmd
                    {
                        HoSoCongViecId = Convert.ToInt64(hscvId),
                        XuLy = xuLy
                    };

                    // Call api for updating du thao
                    var updateThongTinDuThaoResult = UpdateThongTinDuThao(urlBaseHoSoCongViecApi, duThaoId, chucDanhId, updateXuLyDuThao, token);

                    if (updateThongTinDuThaoResult)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool UpdateThongTinDuThao(string urlBaseHoSoCongViecApi, string duThaoId, string chucDanhId, UpdateXuLyDuThaoCmd command, string token)
        {
            var restClient = new RestClient("https://dev-api-qn.eoffice.greenglobal.vn/ho-so-cong-viec");
            var request = new RestSharp.RestRequest($"api/ho-so-cong-viec/du-thao/{duThaoId}/xu-ly", Method.PUT);
            request.AddHeader("X-API-VERSION", "1");
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("chuc-danh-id", chucDanhId);
            request.AddJsonBody(JsonConvert.SerializeObject(command));

            IRestResponse response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public static bool DownloadFile(string url, string folder, string subFolder)
        {
            try
            {
                string fileName = GetFileNameFromUrl(url);

                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, folder + "\\" + subFolder + "\\" + fileName);

                return true;    
            }
            catch
            {
                return false;
            }
        }
    }
}