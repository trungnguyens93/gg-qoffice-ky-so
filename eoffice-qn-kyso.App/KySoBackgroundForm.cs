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
        private VanBanDiService _VanBanDiService;
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
            this._VanBanDiService = new VanBanDiService();
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
                
                var startInput = string.Empty;
                var finalInput = string.Empty;
                var output = string.Empty;
                var pathToProcess = ftpClientConnector.FtpClientRootFolder + ftpClientConnector.FtpClientFolder;
                var pathToImageFile = string.Empty;
                var urlDownloadFileDuThao = string.Empty;
                var tenFileAnhChuKy = string.Empty;
                var chucDanhId = string.Empty;
                var tempChucDanhId = string.Empty;
                var imageData = string.Empty;
                bool daKySoLanhDao = true;
                
                // 1. Create folder for downloading and uploading file
                FolderHelper.CreateTempFolder(pathToProcess);

                // 2. Get infor from HoSoCongViec/VanBanDi Service
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
                else
                {
                    var vanBanDiInfo = _VanBanDiService.GetThongTinVanBanDi(ftpClientConnector.UrlBaseVanBanDiApi, this._Id, this._ChucDanhId, this._Token);
                    if (vanBanDiInfo == null)
                    {
                        return false;
                    }
                    startInput = vanBanDiInfo.DsFileDinhKem.Find(p => p.LaVanBanChinh == true).TenFile;
                    urlDownloadFileDuThao = vanBanDiInfo.DsFileDinhKem.Find(p => p.LaVanBanChinh == true).Url;
                    tempChucDanhId = vanBanDiInfo.NguoiKy.ChucDanhId.ToString();
                    daKySoLanhDao = vanBanDiInfo.DaKySoLanhDao;
                }

                // 3. Set value for chucDanhId variable
                if (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.VAN_BAN_DI) && this._GiaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO))
                {
                    chucDanhId = tempChucDanhId;
                }
                else
                {
                    chucDanhId = this._ChucDanhId;
                }

                // 4. Get infor of client base on chucDanhId variable
                var thongTinNguoiDung = _NguoiDungService.GetThongTinNguoiDung(ftpClientConnector.UrlBaseHeThongApi, chucDanhId, this._Token);
                if (thongTinNguoiDung == null)
                {
                    return false;
                }

                // 5. Set value for variables using create images file
                if (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC) || (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.VAN_BAN_DI) && this._GiaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO)))
                {
                    tenFileAnhChuKy = "chu_ky_khong_dau.png";
                    pathToImageFile = pathToProcess + "\\image\\" + tenFileAnhChuKy;
                    imageData = thongTinNguoiDung.ChuKyKhongDau;
                }
                else
                {
                    tenFileAnhChuKy = "chu_ky_co_dau.png";
                    pathToImageFile = pathToProcess + "\\image\\" + tenFileAnhChuKy;
                    imageData = thongTinNguoiDung.ChuKyCoDau;
                }

                // 6. Convert data from client to image file
                var convertedImageResult = FileHelper.ConvertDataUrlToImage(pathToImageFile, imageData);
                if (!convertedImageResult)
                {
                    return false;
                }

                // 7.  Download signature file
                var downloadFileDuThaoStatus = FileHelper.DownloadFile(ftpClientConnector.UrlBaseFileApi + urlDownloadFileDuThao, pathToProcess, "input", startInput);
                if (!downloadFileDuThaoStatus)
                {
                    return false;
                }

                // 8. Convert file from word to pdf and return it's name
                finalInput = FileHelper.ConvertWordToPdf(pathToProcess, startInput);
                if (string.IsNullOrEmpty(finalInput))
                {
                    return false;
                }

                // 9. Create name for output file
                output = StringHelper.ConvertInputToOutput(finalInput);

                // 10. Signature
                var result = _Service.Sign(pathToProcess, finalInput, this._TrangThaiKySo, this._GiaiDoanChuKy, this._NoiDung, tenFileAnhChuKy, daKySoLanhDao);
                if (!result)
                {
                    return false;
                }

                // 11. Update output file after signaturing digital
                var fileId = FileHelper.UpdateFile(ftpClientConnector.UrlBaseFileApi, pathToProcess, output, this._DuThaoId, this._ChucDanhId, this._Id, this._YKien, this._Token);
                if (fileId == 0)
                {
                    return false;
                }

                // 12. Update info base on calling service api
                if (this._TrangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC))
                {
                    var updateThongTinHscvStatus = _HoSoCongViecService.UpdateThongTinDuThao(ftpClientConnector.UrlBaseHoSoCongViecApi, this._DuThaoId, this._ChucDanhId, this._Id, fileId, this._YKien, this._Token);
                    if (!updateThongTinHscvStatus)
                    {
                        return false;
                    }
                }
                else
                {
                    if (this._GiaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO))
                    {
                        var updateKySoLanhDaoVanbanDiStatus = _VanBanDiService.KySoLanhDaoVanBanDi(ftpClientConnector.UrlBaseVanBanDiApi, this._Id, this._ChucDanhId, fileId, this._Token);
                        if (!updateKySoLanhDaoVanbanDiStatus)
                        {
                            return false;
                        }
                    }

                    if (this._GiaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_BAN_HANH))
                    {
                        if (string.IsNullOrEmpty(this._DuThaoId))
                        {
                            var updateKySoVaBanHanhVanBanDiStatus = _VanBanDiService.KySoBanHanh(ftpClientConnector.UrlBaseVanBanDiApi, this._Id, this._ChucDanhId, fileId, true, this._Token);
                            if (!updateKySoVaBanHanhVanBanDiStatus)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var updateKySoBanHanhVanBanhVanBanDiStatus = _VanBanDiService.KySoBanHanhVanBanDiCoDuThao(ftpClientConnector.UrlBaseVanBanDiApi, this._Id, this._ChucDanhId, fileId, this._Token);
                            if (!updateKySoBanHanhVanBanhVanBanDiStatus)
                            {
                                return false;
                            }
                        }
                    }

                    if (this._GiaiDoanChuKy.Equals(Constant.GiaiDoanKySo.BAN_HANH))
                    {
                        var updateBanHanhVanBanhVanBanDiStatus = _VanBanDiService.KySoBanHanhVanBanDiCoDuThao(ftpClientConnector.UrlBaseVanBanDiApi, this._Id, this._ChucDanhId, fileId, this._Token);
                        if (!updateBanHanhVanBanhVanBanDiStatus)
                        {
                            return false;
                        }
                    }
                }

                // 13. Delete all the temp files and temp folders
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
