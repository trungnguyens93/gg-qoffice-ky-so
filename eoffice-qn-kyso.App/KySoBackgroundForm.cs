namespace eoffice_qn_kyso
{
    using eoffice_qn_kyso.CommonServer;
    using eoffice_qn_kyso.Service.Helpers;
    using eoffice_qn_kyso.Service.Services;
    using System;
    using System.Configuration;
    using System.Windows.Forms;

    public partial class KySoBackgroundForm : Form
    {
        private KySoService _Service;
        
        private string _UrlFileKySo;
        private bool _LaKySoDonVi;
        private string _MaDacBiet;
        private string _NoiDung;
        private string _UrlChuKyKhongDau;
        private string _UrlChuKyCoDau;
        private string _DuThaoId;
        private string _ChucDanhId;
        private string _HscvId;
        private string _YKien;
        private string _Token;
        
        public KySoBackgroundForm()
        {
            InitializeComponent();
        }

        public KySoBackgroundForm(string urlFileKySo, bool laKySoDonVi, string maDacBiet, string noiDung, string urlChuKyKhongDau, string urlChuKyCoDau, string duThaoId, string chucDanhId, string hscvId, string yKien, string token)
        {
            InitializeComponent();

            this._Service = new KySoService();
            this._UrlFileKySo = urlFileKySo;
            this._LaKySoDonVi = laKySoDonVi;
            this._MaDacBiet = maDacBiet;
            this._NoiDung = noiDung;
            this._UrlChuKyKhongDau = urlChuKyKhongDau;
            this._UrlChuKyCoDau = urlChuKyCoDau;
            this._DuThaoId = duThaoId;
            this._ChucDanhId = chucDanhId;
            this._HscvId = hscvId;
            this._YKien = yKien;
            this._Token = token;
        }

        private void KySoBackgroundForm_Load(object sender, EventArgs e)
        {
            try
            {
                var result = KySo();
                
                if (result)
                {
                    this.niApp.ShowBalloonTip(1000, "Ky so", "Ky so thanh cong", ToolTipIcon.Info);
                }
                else
                {
                    this.niApp.ShowBalloonTip(1000, "Ky so", "Ky so that bai", ToolTipIcon.Info);
                }
            }
            catch
            {
                this.niApp.ShowBalloonTip(1000, "Ky so", "Gap loi khi ky so", ToolTipIcon.Info);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool KySo()
        {
            try
            {
                FTPClientConnector ftpClientConnector = new FTPClientConnector();
                FileHelper fileHelper = new FileHelper();

                var startInput = FileHelper.GetFileNameFromUrl(this._UrlFileKySo);
                var finalInput = string.Empty;
                var output = string.Empty;
                var pathToProcess = ftpClientConnector.FtpClientRootFolder + ftpClientConnector.FtpClientFolder;
                
                // Create folder for downloading and uploading file
                FolderHelper.CreateTempFolder(pathToProcess);

                // Download file can ky so tu tren ftpserver
                var downloadFileKySoStatus = FileHelper.DownloadFile(this._UrlFileKySo, pathToProcess, "input");
                if (!downloadFileKySoStatus)
                {
                    return false;
                }

                // Convert file from word to pdf
                //finalInput = FileHelper.ConvertWordToPdf(pathToInputFile, startInput);
                //if (string.IsNullOrEmpty(finalInput))
                //{
                //    return false;
                //}
                
                // Lay file output tu finalInput
                //output = StringHelper.ConvertInputToOutput(finalInput);
                output = StringHelper.ConvertInputToOutput(startInput);

                // Download file chu ky khong dau va chu ky co dau
                if (string.IsNullOrEmpty(this._UrlChuKyKhongDau))
                {
                    var downloadFileChuKyKhongDauStatus = FileHelper.DownloadFile(this._UrlChuKyKhongDau, pathToProcess, "image");
                    if (!downloadFileChuKyKhongDauStatus)
                    {
                        return false;
                    }
                }

                if (string.IsNullOrEmpty(this._UrlChuKyCoDau))
                {
                    var downloadFileChuKyCoDau = FileHelper.DownloadFile(this._UrlChuKyCoDau, pathToProcess, "image");
                    if (!downloadFileChuKyCoDau)
                    {
                        return false;
                    }
                }

                // Lay ten file chu ky co dau va chu ky khong dau
                var tenFileChuKyKhongDau = string.IsNullOrEmpty(this._UrlChuKyKhongDau) ? string.Empty : FileHelper.GetFileNameFromUrl(this._UrlChuKyKhongDau);
                var tenFileChuKyCoDau = string.IsNullOrEmpty(this._UrlChuKyCoDau) ? string.Empty : FileHelper.GetFileNameFromUrl(this._UrlChuKyCoDau);

                // Ky so vao file vua lay ve
                var result = _Service.Sign(pathToProcess, startInput, this._LaKySoDonVi, this._MaDacBiet, this._NoiDung, tenFileChuKyKhongDau, tenFileChuKyCoDau);
                if (!result)
                {
                    return false;
                }

                // Upload file vua duoc thuc hien ky so
                var uploadStatus = FileHelper.UpdateFile(pathToProcess, output, this._DuThaoId, this._ChucDanhId, this._HscvId, this._YKien, this._Token);
                if (!uploadStatus)
                {
                    return false;
                }

                // Delete all the temp files and temp folders
                FolderHelper.DeleteTempFolder(ftpClientConnector.FtpClientRootFolder, ftpClientConnector.FtpClientFolder);

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
