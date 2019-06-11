namespace eoffice_qn_kyso
{
    using eoffice_qn_kyso.CommonServer;
    using eoffice_qn_kyso.Service.Constants;
    using eoffice_qn_kyso.Service.Helpers;
    using eoffice_qn_kyso.Service.Services;
    using System;
    using System.Configuration;
    using System.Windows.Forms;

    public partial class KySoBackgroundForm : Form
    {
        private KySoService _Service;
        private HoSoCongViecService _HoSoCongViecService;
        private NguoiDungService _NguoiDungService;

        private string _TrangThaiKySo;
        private string _GiaiDoanChuKy;
        private string _NoiDung;
        private string _DuThaoId;
        private string _ChucDanhId;
        private string _Id;
        private string _YKien;
        private string _Token;
        
        public KySoBackgroundForm()
        {
            InitializeComponent();
        }

        public KySoBackgroundForm(string trangThaiKySo, string giaiDoanKySo, string noiDung, string duThaoId, string chucDanhId, string id, string yKien, string token)
        {
            InitializeComponent();

            this._Service = new KySoService();
            this._HoSoCongViecService = new HoSoCongViecService();
            this._NguoiDungService = new NguoiDungService();

            this._TrangThaiKySo = trangThaiKySo;
            this._GiaiDoanChuKy = giaiDoanKySo;
            this._NoiDung = noiDung;
            this._DuThaoId = duThaoId;
            this._ChucDanhId = chucDanhId;
            this._Id = id;
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

                //var startInput = FileHelper.GetFileNameFromUrl(this._UrlFileKySo);
                var startInput = string.Empty;
                var finalInput = string.Empty;
                var output = string.Empty;
                var pathToProcess = ftpClientConnector.FtpClientRootFolder + ftpClientConnector.FtpClientFolder;
                var pathToImageFile = string.Empty;
                var urlDownloadFileDuThao = string.Empty;
                var tenFileAnhChuKy = string.Empty;
                
                // Create folder for downloading and uploading file
                FolderHelper.CreateTempFolder(pathToProcess);

                // Lay thong tin file du thao
                if (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC))
                {
                    var fileDuThaoHscvInfo = _HoSoCongViecService.GetThongTinFileDuThao(ftpClientConnector.UrlBaseHoSoCongViecApi, this._ChucDanhId, this._DuThaoId, this._Token);
                    if (fileDuThaoHscvInfo == null)
                    {
                        return false;
                    }
                    startInput = fileDuThaoHscvInfo.TenFile;
                    urlDownloadFileDuThao = fileDuThaoHscvInfo.Url;
                }
                
                // Lay thong tin nguoi dung
                var thongTinNguoiDung = _NguoiDungService.GetThongTinNguoiDung(ftpClientConnector.UrlBaseHeThongApi, this._Token);
                if (thongTinNguoiDung == null)
                {
                    return false;
                }

                if (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC) || (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.VAN_BAN_DI) && this._GiaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO)))
                {
                    tenFileAnhChuKy = "chu_ky_khong_dau.png";
                    pathToImageFile = pathToProcess + "\\image\\" + tenFileAnhChuKy;

                    var convertedImageResult = FileHelper.ConvertDataUrlToImage(pathToImageFile, thongTinNguoiDung.DsChucDanh[0].ChuKyKhongDau);

                    if (!convertedImageResult)
                    {
                        return false;
                    }
                } else
                {
                    tenFileAnhChuKy = "chu_ky_co_dau.png";
                    pathToImageFile = pathToProcess + "\\image\\" + tenFileAnhChuKy;

                    var convertedImageResult = FileHelper.ConvertDataUrlToImage(pathToImageFile, thongTinNguoiDung.DsChucDanh[0].ChuKyCoDau);

                    if (!convertedImageResult)
                    {
                        return false;
                    }
                }

                // Download file can ky so tu tren ftpserver
                var downloadFileDuThaoStatus = FileHelper.DownloadFile(ftpClientConnector.UrlBaseFileApi + urlDownloadFileDuThao, pathToProcess, "input", startInput);
                //var downloadFileKySoStatus = ftpClientConnector.DownloadFile(startInput);
                if (!downloadFileDuThaoStatus)
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
                var result = _Service.Sign(pathToProcess, startInput, this._TrangThaiKySo, this._GiaiDoanChuKy, this._NoiDung, tenFileAnhChuKy);
                if (!result)
                {
                    return false;
                }

                // Upload file vua duoc thuc hien ky so
                var fileId = FileHelper.UpdateFile(ftpClientConnector.UrlBaseFileApi, pathToProcess, output, this._DuThaoId, this._ChucDanhId, this._Id, this._YKien, this._Token);
                if (fileId == 0)
                {
                    return false;
                }

                // Cap nhap thong tin du thao
                if (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC))
                {
                    var updateThongTinHscvStatus = _HoSoCongViecService.UpdateThongTinDuThao(ftpClientConnector.UrlBaseHoSoCongViecApi, this._DuThaoId, this._ChucDanhId, this._Id, fileId, this._YKien, this._Token);
                    if (!updateThongTinHscvStatus)
                    {
                        return false;
                    }
                }

                // Delete all the temp files and temp folders
                //FolderHelper.DeleteTempFolder(ftpClientConnector.FtpClientRootFolder, ftpClientConnector.FtpClientFolder);

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
