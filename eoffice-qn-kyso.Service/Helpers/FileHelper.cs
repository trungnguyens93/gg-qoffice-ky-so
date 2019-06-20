using eoffice_qn_kyso.Service.Models.Dto;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace eoffice_qn_kyso.Service.Helpers
{

    public class FileHelper
    {
        public static string GetFileNameFromUrl(string url)
        {
            var result = string.Empty;

            string[] arrStr = url.Split('/');
            result = arrStr[arrStr.Length - 1];

            return result;
        }

        public static bool ConvertDataUrlToImage(string pathToImageFile, string data)
        {
            try
            {
                var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                var binData = Convert.FromBase64String(base64Data);

                using (var stream = new MemoryStream(binData))
                {
                    var bmp = new Bitmap(stream);
                    bmp.Save(pathToImageFile, ImageFormat.Png);
                    bmp.Dispose();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ConvertWordToPdf(string path, string fileName)
        {
            try
            {
                if (fileName.IndexOf(".pdf") >= 0)
                {
                    return fileName;
                }

                var result = string.Empty;

                string[] arrStr = fileName.Split('.');
                result = String.Concat(arrStr[0], ".", "pdf");

                // Convert word to PDF
                // Create a new Microsoft Word application object
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                word.Visible = false;
                word.ScreenUpdating = false;

                // C# doesn't have optional arguments so we'll need a dummy value
                object oMissing = System.Reflection.Missing.Value;


                FileInfo wordFile = new FileInfo(path + "\\input\\" + fileName);

                // Cast as Object for word Open method
                Object filename = (Object)wordFile.FullName;

                // Use the dummy value as a placeholder for optional arguments
                Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(ref filename, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                doc.Activate();

                object fileFormat = WdSaveFormat.wdFormatPDF;
                object outputFileName;
                if (wordFile.FullName.IndexOf(".docx") >= 0)
                {
                    outputFileName = wordFile.FullName.Replace(".docx", ".pdf");
                }
                else
                {
                    outputFileName = wordFile.FullName.Replace(".doc", ".pdf");
                }
                
                // Save document into PDF Format
                doc.SaveAs(ref outputFileName,
                    ref fileFormat, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                // Close the Word document, but leave the Word application open.
                // doc has to be cast to type _Document so that it will find the
                // correct Close method.                
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
                ((_Document)doc).Close(ref saveChanges, ref oMissing, ref oMissing);
                doc = null;

                // word has to be cast to type _Application so that it will find
                // the correct Quit method.
                ((_Application)word).Quit(ref oMissing, ref oMissing, ref oMissing);
                word = null;

                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Upload file va cap nhap thong tin du thao
        public static long UpdateFile(string urlBaseFileApi, string folder, string fileName, string duThaoId, string chucDanhId, string hscvId, string yKien, string token)
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

                    return fileUploadInfos[0].Id;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        //public static bool DownloadFile(string url, string folder, string subFolder)
        //{
        //    try
        //    {
        //        string fileName = GetFileNameFromUrl(url);

        //        WebClient webClient = new WebClient();
        //        webClient.DownloadFile(url, folder + "\\" + subFolder + "\\" + fileName);

        //        return true;    
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public static bool DownloadFile(string url, string folder, string subFolder, string fileName)
        {
            try
            {
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