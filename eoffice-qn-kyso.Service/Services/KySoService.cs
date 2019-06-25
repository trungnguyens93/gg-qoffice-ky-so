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

        public bool Sign(string folderName, string tenFileKySo, string trangThaiKySo, string giaiDoanKySo, string noiDung, string tenFileChuKy, bool daKySoLanhDao)
        {
            VGCACrypto.Authorization.SetKey("TTBNNVFUQkJNakV0UWtJd05DMDBPVFJHTFRnMU1qZ3RNVVk0UkRsQk1EZzVPRFkyZkRZek56QTRNVFU1TXpVNU5EUTVPREU1Tnc9PXxkalZVQUNUbFdQbGZOS3RYOElsLzN1Vi84d1lYVldONk12eXA2Mjg4eXA5TmZhWVFXOHBlbVlQUTlIUGZNdk9oSXNCdWJwc0duMEZSMVM2cDhsbDEwSjI3bjcwajhLM1hzcmdKa1FYdmpHVDJSaDB6SkRteHF1Q24wNVVzQzJnSW14OG1tSGRoQjFaYjNGTHpNNFl4VEdlVXphd2FhNmZGZUp0WlFHc28ydENQZGpOQndxYVdEUmhDdGEvSGZhM3dCclJpbGZqMjkrTjRMNFg3dzVNYys3TzhNSkJTM3pDM3NqbnRzRzNUR0REUlkyaWp1bU1ucGsxOGl6MWZ4TEc3N0JlVW5Jc3FYeXN1cU9VY2JnT1J3RUE4cEVnKytaZjB5bEkyTlRuMGtSMkMwUStQU1I2OFoveXcxeGhhMzkwL3JzQm4rWVlhekUzQm5HMFpHdjlEZmc9PQ==");

            string input = string.Concat(folderName, "\\input\\", tenFileKySo);
            string output = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo));
            int numberOfPages = -1;
            int pageWidth = -1;
            int pageHeight = -1;
            string pathForImageFile = string.Empty;

            //Chung thu so nguoi ky
            X509Certificate2 cert = null;

            if (!(trangThaiKySo.Equals(Constants.Constant.TrangThaiKySo.VAN_BAN_DI) && giaiDoanKySo.Equals(Constants.Constant.GiaiDoanKySo.BAN_HANH)))
            {
                // Select a certificate to signature
                X509Certificate2Collection keyStore = new X509Certificate2Collection();
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                keyStore.AddRange(store.Certificates);
                store.Close();
                
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
            }

            // Lay so trang cuar pdf
            PdfReader pdfReader = new PdfReader(input);
            numberOfPages = pdfReader.NumberOfPages;
            pageWidth = (int) pdfReader.GetPageSize(1).Width;
            pageHeight = (int)pdfReader.GetPageSize(1).Height;
            pdfReader.Dispose();
           
            var leftPoint = -1;
            var bottomPoint = -1;
            var width = -1;
            var height = -1;
            var content = string.Empty;
            
            try
            {
                //if (loaiChuKy.Equals(Constants.Constant.LoaiChuKy.CHU_KY_DON_VI))
                if (trangThaiKySo.Equals(Constants.Constant.TrangThaiKySo.HO_SO_CONG_VIEC) || (trangThaiKySo.Equals(Constants.Constant.TrangThaiKySo.VAN_BAN_DI) && giaiDoanKySo.Equals(Constants.Constant.GiaiDoanKySo.KY_SO_LANH_DAO)))
                {
                    pathForImageFile = folderName + "\\image\\" + tenFileChuKy;

                    iTextSharp.text.Rectangle rect = ImageHelper.GetRectPosstionFromPdf(input, Constants.Constant.MaDacBiet.CHU_KY_CO_DAU, numberOfPages);

                    if (rect == null || (rect != null && rect.Width <= 0 && rect.Height <= 0))
                    {
                        return false;
                    }

                    width = 150;
                    height = 100;
                    leftPoint = (int)rect.Left;
                    bottomPoint = (int)rect.Bottom - 50;

                    this.KySoKhongDau(input, output, cert, pathForImageFile, numberOfPages, leftPoint, bottomPoint, width, height);
                }
                else if (trangThaiKySo.Equals(Constants.Constant.TrangThaiKySo.VAN_BAN_DI) && giaiDoanKySo.Equals(Constants.Constant.GiaiDoanKySo.KY_SO_BAN_HANH))
                {
                    //string pathForImageFile = string.Empty;

                    string tempInput = input;
                    string tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, 0));

                    var dsMaDacBiet = Constants.Constant.CreateStaticResource();
                    byte[] dataNoiDung = System.Convert.FromBase64String(noiDung);
                    string[] arrStr = System.Text.Encoding.UTF8.GetString(dataNoiDung).Split(',');

                    for (int i = 0; i < dsMaDacBiet.Count; i++)
                    {
                        iTextSharp.text.Rectangle tempRect = ImageHelper.GetRectPosstionFromPdf(input, dsMaDacBiet[i], 1);

                        if (tempRect == null || (tempRect != null && tempRect.Width <= 0 && tempRect.Height <= 0))
                        {
                            return false;
                        }

                        // Setting vi tri can ky
                        //width = dsMaDacBiet[i].Length * 6;
                        width = 200;
                        height = 16;
                        leftPoint = (int)tempRect.Left;
                        bottomPoint = (int)tempRect.Bottom;

                        if (i == 0)
                        {
                            //width = 180;
                            content = string.Concat(arrStr[0], "/", arrStr[1]);
                        }
                        else
                        {
                            //width = width * 3;
                            content = string.Concat(arrStr[2], ", ", arrStr[3]);
                        }

                        // Tao file anh
                        Image image = ImageHelper.DrawText(content, new System.Drawing.Font(DEFAULT_FONT_NAME, DEFAULT_FONT_SIZE), Color.Black, Color.White, width * 2);
                        pathForImageFile = folderName + "\\image\\" + $"sign_{i}_" + DateTime.UtcNow.ToString(DEFAULT_FORMAT_DATE_TIME) + DEFAULT_PDF_FILE;

                        using (var bmp = (Bitmap)image)
                        {
                            bmp.Save(pathForImageFile, System.Drawing.Imaging.ImageFormat.Png);
                            bmp.Dispose();
                        }

                        image.Dispose();
                        
                        this.KySoKhongDau(tempInput, tempOutput, cert, pathForImageFile, 1, leftPoint, bottomPoint, width, height);

                        // Thay dou ten file input va output
                        tempInput = tempOutput;
                        tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, i + 1));
                    }

                    // Ky so chu ky co dau cua don vi
                    iTextSharp.text.Rectangle kySoCoDauRect = ImageHelper.GetRectPosstionFromPdf(input, Constants.Constant.MaDacBiet.CHU_KY_CO_DAU, numberOfPages);

                    if (kySoCoDauRect == null || (kySoCoDauRect != null && kySoCoDauRect.Width <= 0 && kySoCoDauRect.Height <= 0))
                    {
                        return false;
                    }
                    
                    // Xoa chu ky co dau neu 
                    if (!daKySoLanhDao)
                    {
                        Image image = ImageHelper.DrawText("", new System.Drawing.Font(DEFAULT_FONT_NAME, DEFAULT_FONT_SIZE), Color.Black, Color.White, Constants.Constant.MaDacBiet.CHU_KY_CO_DAU.Length * 11);
                        pathForImageFile = folderName + "\\image\\" + "sign_4_" + DateTime.UtcNow.ToString(DEFAULT_FORMAT_DATE_TIME) + DEFAULT_PDF_FILE;

                        using (var bmp = (Bitmap)image)
                        {
                            bmp.Save(pathForImageFile, System.Drawing.Imaging.ImageFormat.Png);
                            bmp.Dispose();
                        }

                        image.Dispose();

                        width = Constants.Constant.MaDacBiet.CHU_KY_CO_DAU.Length * 7;
                        height = 16;
                        leftPoint = (int)kySoCoDauRect.Left;
                        bottomPoint = (int)kySoCoDauRect.Bottom;
                        
                        this.BanHanh(tempInput, tempOutput, pathForImageFile, numberOfPages, leftPoint, bottomPoint, width, height);

                        tempInput = tempOutput;
                        tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, 5));
                    }

                    // Chen con dau don vi
                    pathForImageFile = folderName + "\\image\\" + tenFileChuKy;
                    width = 100;
                    height = 100;
                    leftPoint = (int)kySoCoDauRect.Left;
                    bottomPoint = (int)kySoCoDauRect.Bottom - 50;

                    this.KySoKhongDau(tempInput, tempOutput, cert, pathForImageFile, numberOfPages, leftPoint - 50, bottomPoint, width, height);

                    tempInput = tempOutput;

                    // Ky so don vi
                    width = 100;
                    height = 100;
                    leftPoint = pageWidth - 100;
                    bottomPoint = pageHeight - 100;

                    this.KySoDonVi(tempInput, output, cert, leftPoint, bottomPoint, width, height);
                }
                else
                {
                    string tempInput = input;
                    string tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, 0));

                    var dsMaDacBiet = Constants.Constant.CreateStaticResource();
                    byte[] dataNoiDung = System.Convert.FromBase64String(noiDung);
                    string[] arrStr = System.Text.Encoding.UTF8.GetString(dataNoiDung).Split(',');

                    for (int i = 0; i < dsMaDacBiet.Count; i++)
                    {
                        iTextSharp.text.Rectangle tempRect = ImageHelper.GetRectPosstionFromPdf(input, dsMaDacBiet[i], 1);

                        if (tempRect == null || (tempRect != null && tempRect.Width <= 0 && tempRect.Height <= 0))
                        {
                            return false;
                        }

                        // Setting vi tri can ky
                        //width = dsMaDacBiet[i].Length * 6;
                        width = 200;
                        height = 16;
                        leftPoint = (int)tempRect.Left;
                        bottomPoint = (int)tempRect.Bottom;

                        if (i == 0)
                        {
                            //width = width * 6;
                            content = string.Concat(arrStr[0], "/", arrStr[1]);
                        }
                        else
                        {
                            //width = width * 3;
                            content = string.Concat(arrStr[2], ", ", arrStr[3]);
                        }

                        // Tao file anh
                        Image image = ImageHelper.DrawText(content, new System.Drawing.Font(DEFAULT_FONT_NAME, DEFAULT_FONT_SIZE), Color.Black, Color.White, width * 2);
                        pathForImageFile = folderName + "\\image\\" + $"sign_{i}_" + DateTime.UtcNow.ToString(DEFAULT_FORMAT_DATE_TIME) + DEFAULT_PDF_FILE;

                        using (var bmp = (Bitmap)image)
                        {
                            bmp.Save(pathForImageFile, System.Drawing.Imaging.ImageFormat.Png);
                            bmp.Dispose();
                        }

                        image.Dispose();
                        
                        this.BanHanh(tempInput, tempOutput, pathForImageFile, 1, leftPoint, bottomPoint, width, height);

                        // Thay dou ten file input va output
                        tempInput = tempOutput;

                        if (daKySoLanhDao)
                        {
                            if (i < 2)
                            {
                                tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, i + 1));
                            }
                            else
                            {
                                tempOutput = output;
                            }
                        }
                        else
                        {
                            tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, i + 1));
                        }
                    }

                    iTextSharp.text.Rectangle chuKyCoDauRect = ImageHelper.GetRectPosstionFromPdf(input, Constants.Constant.MaDacBiet.CHU_KY_CO_DAU, numberOfPages);

                    if (chuKyCoDauRect == null || (chuKyCoDauRect != null && chuKyCoDauRect.Width <= 0 && chuKyCoDauRect.Height <= 0))
                    {
                        return false;
                    }

                    // Xoa ma chu ky co dau
                    if (!daKySoLanhDao)
                    {
                        // Tao file anh
                        Image image = ImageHelper.DrawText("", new System.Drawing.Font(DEFAULT_FONT_NAME, DEFAULT_FONT_SIZE), Color.Black, Color.White, Constants.Constant.MaDacBiet.CHU_KY_CO_DAU.Length * 11);
                        pathForImageFile = folderName + "\\image\\" + "sign_4_" + DateTime.UtcNow.ToString(DEFAULT_FORMAT_DATE_TIME) + DEFAULT_PDF_FILE;

                        using (var bmp = (Bitmap)image)
                        {
                            bmp.Save(pathForImageFile, System.Drawing.Imaging.ImageFormat.Png);
                            bmp.Dispose();
                        }

                        image.Dispose();

                        // Setting vi tri can ky
                        width = Constants.Constant.MaDacBiet.CHU_KY_CO_DAU.Length * 7;
                        height = 16;
                        leftPoint = (int)chuKyCoDauRect.Left;
                        bottomPoint = (int)chuKyCoDauRect.Bottom;

                        this.BanHanh(tempInput, tempOutput, pathForImageFile, numberOfPages, leftPoint, bottomPoint, width, height);

                        tempInput = tempOutput;
                        tempOutput = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(tenFileKySo, 4));
                    }

                    pathForImageFile = folderName + "\\image\\" + tenFileChuKy;
                    width = 100;
                    height = 100;
                    leftPoint = (int)chuKyCoDauRect.Left;
                    bottomPoint = (int)chuKyCoDauRect.Bottom - 50;

                    this.BanHanh(tempInput, output, pathForImageFile, numberOfPages, leftPoint - 50, bottomPoint, width, height);

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
        private void KySoDonVi(string input, string output, X509Certificate2 cert, int leftPoint, int bottomPoint, int width, int height)
        {
            try
            {
                //[3] Ky so
                PdfSigner pdf = new PdfSigner(input, output, cert);
                pdf.Location = "Quảng Nam";
                pdf.TsaUrl = "http://ca.gov.vn/tsa";

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
        private void KySoKhongDau(string input, string output, X509Certificate2 cert, string pathForImageFile, int trangKy, int leftPoint, int bottomPoint, int width, int height)
        {
            try
            {
                Image image = Image.FromFile(pathForImageFile);

                PdfSigner pdf = new PdfSigner(input, output, cert);
                pdf.Location = "Quảng Nam";
                pdf.TsaUrl = "http://ca.gov.vn/tsa";

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

        private void BanHanh(string input, string output, string pathForImageFile, int trangKy, int leftPoint, int bottomPoint, int width, int height)
        {
            try
            {
                PdfReader pdfReader = new PdfReader(input);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new System.IO.FileStream(output, System.IO.FileMode.Create));

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(pathForImageFile);
                image.ScaleToFit(width, height);
                image.SetAbsolutePosition(leftPoint, bottomPoint);

                PdfContentByte over = pdfStamper.GetOverContent(trangKy);
                over.AddImage(image);

                pdfStamper.Close();
                pdfReader.Dispose();
            }
            catch
            {
                throw;
            }
        }
    }
}
