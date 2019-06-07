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
        
        private string _Path;
        private string _FileName;
        private string _UrlChuKyKhongDau;
        private string _NoiDungSo;
        private string _NoiDungKyHieu;
        private string _NoiDungDiaDiemBanHanh;
        private string _NoiDungNgayBanHanh;
        private string _DuThaoId;
        private string _ChucDanhId;
        private string _HscvId;
        private string _YKien;
        private string _Token;
        
        public KySoBackgroundForm()
        {
            InitializeComponent();
        }

        public KySoBackgroundForm(string path, string fileName, string urlChuKyKhongDau, string noiDungSo, string noiDungKyHieu, string noiDungDiaDiemBanHanh, string noiDungNgayBanHanh, string duThaoId, string chucDanhId, string hscvId, string yKien, string token)
        {
            InitializeComponent();

            this._Service = new KySoService();
            this._Path = path;
            this._FileName = fileName;
            this._UrlChuKyKhongDau = urlChuKyKhongDau;
            this._NoiDungSo = noiDungSo;
            this._NoiDungKyHieu = noiDungKyHieu;
            this._NoiDungDiaDiemBanHanh = noiDungDiaDiemBanHanh;
            this._NoiDungNgayBanHanh = noiDungNgayBanHanh;
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

                var startInput = this._FileName;
                var finalInput = string.Empty;
                var output = string.Empty;
                var pathToInputFile = ftpClientConnector.FtpClientRootFolder + ftpClientConnector.FtpClientFolder;

                // Create folder for downloading and uploading file
                FolderHelper.CreateTempFolder(pathToInputFile);

                // Download file can ky so tu tren ftpserver
                var downloadStatus = ftpClientConnector.DownloadFile(startInput);
                if (!downloadStatus)
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

                // Ky so vao file vua lay ve
                var result = _Service.Sign(pathToInputFile, startInput, "#ChuKyCoDau", null);
                if (!result)
                {
                    return false;
                }

                // Upload file vua duoc thuc hien ky so
                var uploadStatus = ftpClientConnector.UpdateFileWithApi(output, this._DuThaoId, this._ChucDanhId, this._HscvId, this._YKien, this._Token);
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
