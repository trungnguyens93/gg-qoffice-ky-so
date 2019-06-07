namespace eoffice_qn_kyso.CommonServer
{
    using eoffice_qn_kyso.App.Models.Dto;
    using eoffice_qn_kyso.App.Models.Dto.Command;
    using eoffice_qn_kyso.App.Models.Include;
    using eoffice_qn_kyso.App.ViewModels;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class FTPClientConnector
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FtpServerFolder { get; set; }
        public string FtpClientFolder { get; set; }
        public string FtpClientRootFolder { get; set; }

        public FTPClientConnector()
        {
            this.Host = ConfigurationManager.AppSettings.Get("ftpHost");
            this.Username = ConfigurationManager.AppSettings.Get("ftpUsername");
            this.Password = ConfigurationManager.AppSettings.Get("ftpPassword"); ;
            this.FtpServerFolder = ConfigurationManager.AppSettings.Get("ftpServerFolder");
            this.FtpClientRootFolder = ConfigurationManager.AppSettings.Get("ftpClientRootFolder");
            this.FtpClientFolder = ConfigurationManager.AppSettings.Get("ftpClientFolder");
        }

        public bool UploadFile(string fileName)
        {
            try
            {
                string absoluteFileName = Path.GetFileName(fileName);

                FtpWebRequest request = WebRequest.Create(new Uri(string.Format(@"ftp://{0}/{1}/{2}", this.Host, this.FtpServerFolder, absoluteFileName))) as FtpWebRequest;
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = true;
                request.Credentials = new NetworkCredential(this.Username, this.Password);

                using (FileStream fs = File.OpenRead(this.FtpClientRootFolder + this.FtpClientFolder + "\\" + "output" + "\\" + fileName))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateFileWithApi(string fileName, string duThaoId, string chucDanhId, string hscvId, string yKien, string token)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(this.FtpClientRootFolder + this.FtpClientFolder + "\\" + "output" + "\\" + fileName);

                var restClient = new RestClient("https://dev-file-qn.eoffice.greenglobal.vn");
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
                    var updateThongTinDuThaoResult = this.UpdateThongTinDuThao(duThaoId, chucDanhId, updateXuLyDuThao, token);

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

        public bool UpdateThongTinDuThao(string duThaoId, string chucDanhId, UpdateXuLyDuThaoCmd command, string token)
        {
            // Thieu chucDanhId

            var restClient = new RestClient("https://dev-api-qn.eoffice.greenglobal.vn/ho-so-cong-viec/");
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

        public bool DownloadFile(string fileName)
        {
            try
            {
                string ftpfullpath = string.Format(@"ftp://{0}/{1}/{2}", this.Host, this.FtpServerFolder, fileName);

                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(this.Username, this.Password);
                    byte[] fileData = request.DownloadData(ftpfullpath);

                    using (FileStream file = File.Create(this.FtpClientRootFolder + this.FtpClientFolder + "\\" + "input" + "\\" + fileName))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DownloadFileWithApi(string url)
        {
            try
            {
                var client = new RestClient(url);


                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
