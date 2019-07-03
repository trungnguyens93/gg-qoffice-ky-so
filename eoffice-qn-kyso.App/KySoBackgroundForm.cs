namespace eoffice_qn_kyso
{
    using eoffice_qn_kyso.Service.Constants;
    using eoffice_qn_kyso.Service.Helpers;
    using eoffice_qn_kyso.Service.Services;
    using NLog;
    using System;
    using System.Configuration;
    using System.Windows.Forms;

    public partial class KySoBackgroundForm : Form
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly string PROCESS_FOLDER_NAME = "SignedFolder";
        
        // Services
        private KySoService _service;
        private HoSoCongViecService _hoSoCongViecService;
        private VanBanDiService _vanBanDiService;
        private HeThongService _heThongService;
        private FileService _fileService;

        /// Properties
        private string _pathToProcessFolder;

        private string _pathToRootProcessFolder;

        /// <summary>
        /// Trang thai ky so
        /// </summary>
        private string _trangThaiKySo;

        /// <summary>
        /// Giai doan ky so
        /// </summary>
        private string _giaiDoanChuKy;

        /// <summary>
        /// Noi dung chua thong tin cua 4 ma dac biet
        /// </summary>
        private string _noiDung;

        /// <summary>
        /// Du thao Id
        /// </summary>
        private long _duThaoId;

        /// <summary>
        /// Chuc danh Id
        /// </summary>
        private long _chucDanhId;

        /// <summary>
        /// Ho so cong viec Id | Van ban di Id
        /// </summary>
        private long _id;

        /// <summary>
        /// Y kien nguoi lanh dao
        /// </summary>
        private string _yKien;

        /// <summary>
        /// Token nguoi dung hien tai
        /// </summary>
        private string _token;
        
        public KySoBackgroundForm()
        {
            InitializeComponent();
        }

        public KySoBackgroundForm(string trangThaiKySo, string giaiDoanKySo, string noiDung, string duThaoId, string chucDanhId, string id, string yKien, string token)
        {
            InitializeComponent();

            this.InitServices(Convert.ToInt64(chucDanhId), token);
            this.GetPathToProcessFolder();

            this._trangThaiKySo = trangThaiKySo;
            this._giaiDoanChuKy = giaiDoanKySo;
            this._noiDung = noiDung;
            this._duThaoId = string.IsNullOrEmpty(duThaoId) ? -1 : Convert.ToInt64(duThaoId);
            this._chucDanhId = Convert.ToInt64(chucDanhId);
            this._id = Convert.ToInt64(id);
            this._yKien = yKien;
            this._token = token;
        }

        private void InitServices(long chucDanhId, string token)
        {
            // Get value from app.config
            var baseUrlHoSoCongViecApi = ConfigurationManager.AppSettings.Get(Constant.BaseUrl.HO_SO_CONG_VIEC_API);
            var baseUrlVanBanDiApi = ConfigurationManager.AppSettings.Get(Constant.BaseUrl.VAN_BAN_DI_API);
            var baseUrlHeThongApi = ConfigurationManager.AppSettings.Get(Constant.BaseUrl.HE_THONG_API);
            var baseUrlFileApi = ConfigurationManager.AppSettings.Get(Constant.BaseUrl.FILE_API);

            this._service = new KySoService();
            this._hoSoCongViecService = new HoSoCongViecService(baseUrlHoSoCongViecApi, chucDanhId, token);
            this._vanBanDiService = new VanBanDiService(baseUrlVanBanDiApi, chucDanhId, token);
            this._heThongService = new HeThongService(baseUrlHeThongApi, chucDanhId, token);
            this._fileService = new FileService(baseUrlFileApi, token);
        }

        private void GetPathToProcessFolder()
        {
            var rootFolder = AppDomain.CurrentDomain.BaseDirectory;

            this._pathToRootProcessFolder = rootFolder;
            this._pathToProcessFolder = rootFolder + this.PROCESS_FOLDER_NAME;
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
                var startInput = string.Empty;
                var finalInput = string.Empty;
                var output = string.Empty;
                var pathToImageFile = string.Empty;
                var urlFileDuThao = string.Empty;
                var tenFileAnhChuKy = string.Empty;
                var imageData = string.Empty;
                bool daKySoLanhDao = true;

                _logger.Info("===================================================================================");

                // 1. Create process folder
                FolderHelper.CreateProcessFolder(this._pathToProcessFolder);
                _logger.Info(Constant.TrackingStatus.TAO_THU_MUC_CHUNG);

                // 2. Get infor from HoSoCongViec|VanBanDi Service
                if (this._trangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC))
                {
                    var fileDuThaoHscvInfo = _hoSoCongViecService.GetThongTinFileDuThao(this._duThaoId);
                    if (fileDuThaoHscvInfo == null)
                    {
                        return false;
                    }
                    startInput = fileDuThaoHscvInfo.TenFile;
                    urlFileDuThao = fileDuThaoHscvInfo.Url;

                    _logger.Info(Constant.TrackingStatus.LAY_THONG_TIN_HO_SO_CONG_VIEC);
                }
                else
                {
                    var vanBanDiInfo = _vanBanDiService.GetThongTinVanBanDi(this._id);
                    if (vanBanDiInfo == null)
                    {
                        return false;
                    }
                    startInput = vanBanDiInfo.DsFileDinhKem.Find(p => p.LaVanBanChinh == true).TenFile;
                    urlFileDuThao = vanBanDiInfo.DsFileDinhKem.Find(p => p.LaVanBanChinh == true).Url;
                    daKySoLanhDao = vanBanDiInfo.DaKySoLanhDao;

                    if (this._giaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO))
                    {
                        this._chucDanhId = vanBanDiInfo.NguoiKy.ChucDanhId;
                    }

                    _logger.Info(Constant.TrackingStatus.LAY_THONG_TIN_VAN_BAN_DI);
                }

                // 3.  Download signature file
                var downloadFileDuThaoStatus = _fileService.DownloadFile(urlFileDuThao, this._pathToProcessFolder, Constant.TypeFolder.INPUT, startInput);
                if (!downloadFileDuThaoStatus)
                {
                    return false;
                }

                _logger.Info(Constant.TrackingStatus.DOWNLOAD_FILE_DU_THAO);

                // 4. Get infor of client base on chucDanhId variable
                var thongTinChucDanh = _heThongService.GetThongTinChucDanh(this._chucDanhId);
                if (thongTinChucDanh == null)
                {
                    return false;
                }

                _logger.Info(Constant.TrackingStatus.LAY_THONG_TIN_CHUC_DANH);

                // 5. Set value for variables using create images file
                if (this._trangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC) || (this._trangThaiKySo.Equals(Constant.TrangThaiKySo.VAN_BAN_DI) && this._giaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO)))
                {
                    tenFileAnhChuKy = "chu_ky_khong_dau.png";
                    pathToImageFile = this._pathToProcessFolder + Constant.TypeFolder.IMAGE + tenFileAnhChuKy;
                    imageData = thongTinChucDanh.ChuKyKhongDau;
                }
                else
                {
                    tenFileAnhChuKy = "chu_ky_co_dau.png";
                    pathToImageFile = this._pathToProcessFolder + Constant.TypeFolder.IMAGE + tenFileAnhChuKy;
                    imageData = thongTinChucDanh.ChuKyCoDau;
                }

                // 6. Convert data from client to image file
                var convertedImageResult = FileHelper.ConvertDataUrlToImage(pathToImageFile, imageData);
                if (!convertedImageResult)
                {
                    return false;
                }

                _logger.Info(Constant.TrackingStatus.TAO_ANH_KY_SO);
                
                // 8. Convert file from word to pdf and return it's name
                finalInput = FileHelper.ConvertWordToPdf(this._pathToProcessFolder, startInput);
                if (string.IsNullOrEmpty(finalInput))
                {
                    return false;
                }

                _logger.Info(Constant.TrackingStatus.CONVERT_WORD_TO_PDF);

                // 9. Create name for output file
                output = StringHelper.ConvertInputToOutput(finalInput);

                // 10. Signature
                var result = _service.Sign(this._pathToProcessFolder, finalInput, this._trangThaiKySo, this._giaiDoanChuKy, this._noiDung, tenFileAnhChuKy, daKySoLanhDao);
                if (!result)
                {
                    return false;
                }

                _logger.Info(Constant.TrackingStatus.KY_SO);

                // 11. Update output file after signaturing digital
                var fileId = _fileService.UploadFile(this._pathToProcessFolder, output, this._duThaoId, this._id, this._yKien);
                if (fileId == 0)
                {
                    return false;
                }

                _logger.Info(Constant.TrackingStatus.UPLOAD_FILE_DU_THAO);

                // 12. Update info base on calling service api
                if (this._trangThaiKySo.Equals(Constant.TrangThaiKySo.HO_SO_CONG_VIEC))
                {
                    var updateThongTinHscvStatus = _hoSoCongViecService.UpdateThongTinDuThao(this._duThaoId, this._id, fileId, this._yKien);
                    if (!updateThongTinHscvStatus)
                    {
                        return false;
                    }
                }
                else
                {
                    if (this._giaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_LANH_DAO))
                    {
                        var updateKySoLanhDaoVanbanDiStatus = _vanBanDiService.KySoLanhDaoVanBanDi(this._id, fileId);
                        if (!updateKySoLanhDaoVanbanDiStatus)
                        {
                            return false;
                        }
                    }

                    if (this._giaiDoanChuKy.Equals(Constant.GiaiDoanKySo.KY_SO_BAN_HANH))
                    {
                        if (this._duThaoId <= 0)
                        {
                            var updateKySoVaBanHanhVanBanDiStatus = _vanBanDiService.KySoBanHanh(this._id, fileId, true);
                            if (!updateKySoVaBanHanhVanBanDiStatus)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var updateKySoBanHanhVanBanhVanBanDiStatus = _vanBanDiService.KySoBanHanhVanBanDiCoDuThao(this._id, fileId);
                            if (!updateKySoBanHanhVanBanhVanBanDiStatus)
                            {
                                return false;
                            }
                        }
                    }

                    if (this._giaiDoanChuKy.Equals(Constant.GiaiDoanKySo.BAN_HANH))
                    {
                        var updateBanHanhVanBanhVanBanDiStatus = _vanBanDiService.KySoBanHanhVanBanDiCoDuThao(this._id, fileId);
                        if (!updateBanHanhVanBanhVanBanDiStatus)
                        {
                            return false;
                        }
                    }
                }

                _logger.Info(Constant.TrackingStatus.CAP_NHAP_THONG_TIN);

                // 13. Delete process folder
                FolderHelper.DeleteTempFolder(this._pathToRootProcessFolder, this.PROCESS_FOLDER_NAME);
                _logger.Info(Constant.TrackingStatus.XOA_FOLDER_XU_LU);

                _logger.Info("===================================================================================\n");

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());

                throw;
            }
        }
    }
}
