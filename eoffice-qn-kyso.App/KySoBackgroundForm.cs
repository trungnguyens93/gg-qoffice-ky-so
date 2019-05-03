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

        private string _FileName;
        private string _MaDacBiet;
        private string _NoiDung;
        private int _SoChuKyNhayDaCo;
        private string _RegistryName;
        
        public KySoBackgroundForm()
        {
            InitializeComponent();
        }

        public KySoBackgroundForm(string fileName, string maDacBiet, string noiDung, int soChuKyNhayDaCo)
        {
            InitializeComponent();

            this._Service = new KySoService();
            this._FileName = fileName;
            this._MaDacBiet = maDacBiet;
            this._NoiDung = noiDung;
            this._SoChuKyNhayDaCo = soChuKyNhayDaCo;
            this._RegistryName = ConfigurationManager.AppSettings.Get("registryName");
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
                var input = this._FileName;
                var output = StringHelper.ConvertInputToOutput(input);

                FTPClientConnector ftpClientConnector = new FTPClientConnector();

                // Create folder for downloading and uploading file
                ftpClientConnector.CreateTempFolder();

                // Download file can ky so tu tren ftpserver
                var downloadStatus = ftpClientConnector.DownloadFile(input);
                if (!downloadStatus)
                {
                    return false;
                }

                // Ky so vao file vua lay ve
                var result = _Service.SignB(ftpClientConnector.FtpClientRootFolder + ftpClientConnector.FtpClientFolder, this._FileName, this._MaDacBiet, this._NoiDung, this._SoChuKyNhayDaCo);
                if (!result)
                {
                    return false;
                }
                
                // Upload file vua duoc thuc hien ky so
                var uploadStatus = ftpClientConnector.UploadFile(output);
                if (!uploadStatus)
                {
                    return false;
                }

                // Delete all the temp files and temp folders
                ftpClientConnector.DeleteTempFolder();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
