namespace eoffice_qn_kyso.Service.Services
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using VGCACrypto.PDF;
    using iTextSharp.text.pdf;
    using eoffice_qn_kyso.Service.Helpers;
    using System.Drawing;

    public class KySoService
    {
        private const string DEFAULT_FONT_NAME = "Times New Roman";
        private const Single DEFAULT_FONT_SIZE = 14.0f;
        private const string DEFAULT_FORMAT_DATE_TIME = "yyMMdd_hhMMss";
        private const string DEFAULT_PDF_FILE = ".png";

        public bool Sign(string folderName, string tenFileKySo, string loaiChuKy, string maDacBiet, string noiDung, string tenFileChuKy)
        {
            VGCACrypto.Authorization.SetKey("TTBNNVFUQkJNakV0UWtJd05DMDBPVFJHTFRnMU1qZ3RNVVk0UkRsQk1EZzVPRFkyZkRZek56QTRNVFU1TXpVNU5EUTVPREU1Tnc9PXxkalZVQUNUbFdQbGZOS3RYOElsLzN1Vi84d1lYVldONk12eXA2Mjg4eXA5TmZhWVFXOHBlbVlQUTlIUGZNdk9oSXNCdWJwc0duMEZSMVM2cDhsbDEwSjI3bjcwajhLM1hzcmdKa1FYdmpHVDJSaDB6SkRteHF1Q24wNVVzQzJnSW14OG1tSGRoQjFaYjNGTHpNNFl4VEdlVXphd2FhNmZGZUp0WlFHc28ydENQZGpOQndxYVdEUmhDdGEvSGZhM3dCclJpbGZqMjkrTjRMNFg3dzVNYys3TzhNSkJTM3pDM3NqbnRzRzNUR0REUlkyaWp1bU1ucGsxOGl6MWZ4TEc3N0JlVW5Jc3FYeXN1cU9VY2JnT1J3RUE4cEVnKytaZjB5bEkyTlRuMGtSMkMwUStQU1I2OFoveXcxeGhhMzkwL3JzQm4rWVlhekUzQm5HMFpHdjlEZmc9PQ==");

            string input = string.Concat(folderName, "\\input\\", tenFileKySo);
            string output = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo));
            int numberOfPages = -1;
            int pageWidth = -1;
            int pageHeight = -1;
            int trangCanKy = -1;
            
            // Select a certificate to signature
            X509Certificate2Collection keyStore = new X509Certificate2Collection();
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);
            keyStore.AddRange(store.Certificates);
            store.Close();

            //Chung thu so nguoi ky
            X509Certificate2 cert = null;

            try
            {
                cert = X509Certificate2UI.SelectFromCollection(keyStore, "Chứng thư số ký", "Chọn chứng thư số ký", X509SelectionFlag.SingleSelection)[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (cert == null)
            {
                return false;
            }

            // Lay so trang cuar pdf
            PdfReader pdfReader = new PdfReader(input);
            numberOfPages = pdfReader.NumberOfPages;
            pageWidth = (int) pdfReader.GetPageSize(1).Width;
            pageHeight = (int)pdfReader.GetPageSize(1).Height;
            pdfReader.Dispose();

            if (loaiChuKy.Equals(Constants.Constant.LoaiChuKy.CHU_KY_KHONG_DAU) && maDacBiet.Equals(Constants.Constant.MaDacBiet.CHU_KY_CO_DAU))
            {
                trangCanKy = numberOfPages;
            }
            else
            {
                trangCanKy = 1;
            }

            iTextSharp.text.Rectangle rect = ImageHelper.GetRectPosstionFromPdf(input, maDacBiet, trangCanKy);

            if (rect == null || (rect != null && rect.Width <= 0 && rect.Height <= 0))
            {
                return false;
            }
            
            //[3] Ky so
            PdfSigner pdf = new PdfSigner(input, output, cert);
            pdf.Location = "Quảng Nam";
            pdf.TsaUrl = "http://ca.gov.vn/tsa";

            var leftPoint = -1;
            var bottomPoint = -1;
            var width = -1;
            var height = -1;
            
            try
            {
                if (loaiChuKy.Equals(Constants.Constant.LoaiChuKy.CHU_KY_DON_VI))
                {
                    width = 100;
                    height = 100;
                    leftPoint = pageWidth - 100;
                    bottomPoint = pageHeight - 100;

                    this.KySoDonVi(pdf, leftPoint, bottomPoint, width, height);
                }
                else if (loaiChuKy.Equals(Constants.Constant.LoaiChuKy.CHU_KY_KHONG_DAU))
                {
                    string pathForImageFile = string.Empty;

                    // Lay duong dan file anh va trang can ky
                    if (maDacBiet.Equals(Constants.Constant.MaDacBiet.CHU_KY_CO_DAU))
                    {
                        pathForImageFile = folderName + "\\image\\" + tenFileChuKy;
                        
                        width = 120;
                        height = 80;
                        leftPoint = (int)rect.Left - 12;
                        bottomPoint = (int)rect.Bottom - 40;
                    }
                    else
                    {
                        Image image = ImageHelper.DrawText(noiDung, new System.Drawing.Font(DEFAULT_FONT_NAME, DEFAULT_FONT_SIZE), Color.Black, Color.White, maDacBiet.Length * 11);
                        pathForImageFile = folderName + "\\image\\" + "sign_" + DateTime.UtcNow.ToString(DEFAULT_FORMAT_DATE_TIME) + DEFAULT_PDF_FILE;

                        using (var bmp = (Bitmap)image)
                        {
                            bmp.Save(pathForImageFile, System.Drawing.Imaging.ImageFormat.Png);
                            bmp.Dispose();
                        }

                        image.Dispose();

                        width = maDacBiet.Length * 6;
                        height = 16;
                        leftPoint = (int)rect.Left;
                        bottomPoint = (int)rect.Bottom;
                    }


                    this.KySoKhongDau(pdf, pathForImageFile, trangCanKy, leftPoint, bottomPoint, width, height);
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Ky so don vi
        /// </summary>
        /// <param name="pdf"></param>
        /// <param name="leftPoint"></param>
        /// <param name="bottomPoint"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void KySoDonVi(PdfSigner pdf, int leftPoint, int bottomPoint, int width, int height)
        {
            try
            {
                pdf.SignatureAppearance = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
                pdf.ShowDate = true;
                pdf.ShowEmail = true;
                pdf.ShowJob = true;
                pdf.ShowLabel = true;
                pdf.ShowOrg = true;

                pdf.Sign(1, leftPoint, bottomPoint, width, height);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ky so khong dau
        /// </summary>
        /// <param name="pdf"></param>
        /// <param name="pathOfImageFile"></param>
        /// <param name="trangKy"></param>
        /// <param name="leftPoint"></param>
        /// <param name="bottomPoint"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void KySoKhongDau(PdfSigner pdf, string pathForImageFile, int trangKy, int leftPoint, int bottomPoint, int width, int height)
        {
            try
            {
                Image image = Image.FromFile(pathForImageFile);

                pdf.SignatureAppearance = PdfSignatureAppearance.RenderingMode.GRAPHIC;
                pdf.SignatureImage = image;
                pdf.Sign(trangKy, leftPoint, bottomPoint, width, height);
                pdf.SignatureImage.Dispose();
                
                image.Dispose();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Ky so co dau
        /// </summary>
        /// <param name="pdf"></param>
        /// <param name="pathOfImageFile"></param>
        /// <param name="trangKy"></param>
        /// <param name="leftPoint"></param>
        /// <param name="bottomPoint"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void KySoCoDau(PdfSigner pdf, string pathOfImageFile, int trangKy, int leftPoint, int bottomPoint, int width, int height)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        public bool CheckMaDacBiet(string folder, string fileName)
        {
            //string input = string.Concat(folder, "\\", fileName);
            //var loaiChuKys = Constants.Constant.CreateStaticResource();

            //foreach (var item in loaiChuKys)
            //{
            //    iTextSharp.text.Rectangle rect = ImageHelper.GetRectPosstionFromPdf(input, item.MaDacBiet);

            //    if (rect == null || (rect != null && rect.Width <= 0 && rect.Height <= 0))
            //    {
            //        return false;
            //    }
            //}
 
            return true;
        }
    }
}
