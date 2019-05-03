namespace eoffice_qn_kyso.CommonServer
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;

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

        public void CreateTempFolder()
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(FtpClientRootFolder + FtpClientFolder);
            directoryInfo.CreateSubdirectory("input");
            directoryInfo.CreateSubdirectory("output");
            directoryInfo.CreateSubdirectory("image");
        }

        public void DeleteTempFolder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(this.FtpClientRootFolder);

            foreach (var folder in directoryInfo.GetDirectories())
            {
                if (folder.Name.Equals(this.FtpClientFolder))
                {
                    foreach (var folder2 in folder.GetDirectories())
                    {
                        foreach (var file in folder2.GetFiles())
                        {
                            file.Delete();
                        }

                        folder2.Delete();
                    }

                    folder.Delete();
                }
            }
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
                throw;
            }
        }
    }
}
