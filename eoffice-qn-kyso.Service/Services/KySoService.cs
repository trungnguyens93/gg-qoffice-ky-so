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
        public bool SignB(string folderName, string fileName, string maDacBiet, string noiDung, int soChuKyNhayDaCo)
        {
            VGCACrypto.Authorization.SetKey("TTBNNVFUQkJNakV0UWtJd05DMDBPVFJHTFRnMU1qZ3RNVVk0UkRsQk1EZzVPRFkyZkRZek56QTRNVFU1TXpVNU5EUTVPREU1Tnc9PXxkalZVQUNUbFdQbGZOS3RYOElsLzN1Vi84d1lYVldONk12eXA2Mjg4eXA5TmZhWVFXOHBlbVlQUTlIUGZNdk9oSXNCdWJwc0duMEZSMVM2cDhsbDEwSjI3bjcwajhLM1hzcmdKa1FYdmpHVDJSaDB6SkRteHF1Q24wNVVzQzJnSW14OG1tSGRoQjFaYjNGTHpNNFl4VEdlVXphd2FhNmZGZUp0WlFHc28ydENQZGpOQndxYVdEUmhDdGEvSGZhM3dCclJpbGZqMjkrTjRMNFg3dzVNYys3TzhNSkJTM3pDM3NqbnRzRzNUR0REUlkyaWp1bU1ucGsxOGl6MWZ4TEc3N0JlVW5Jc3FYeXN1cU9VY2JnT1J3RUE4cEVnKytaZjB5bEkyTlRuMGtSMkMwUStQU1I2OFoveXcxeGhhMzkwL3JzQm4rWVlhekUzQm5HMFpHdjlEZmc9PQ==");

            string input = string.Concat(folderName, "\\input\\", fileName);
            string output = string.Concat(folderName, "\\output\\", StringHelper.ConvertInputToOutput(fileName));

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

            iTextSharp.text.Rectangle rect = ImageHelper.GetRectPosstionFromPdf(input, maDacBiet);

            if (rect == null || (rect != null && rect.Width <= 0 && rect.Height <= 0))
            {
                return false;
            }

            //[3] Ky so
            PdfSigner pdf = new PdfSigner(input, output, cert);
            pdf.Location = "Hà Nội";
            pdf.TsaUrl = "http://ca.gov.vn/tsa";

            //Hiển thị chữ ký trên tài liệu dạng thông tin miêu tả
            var loaiChuKys = Constants.Constant.CreateStaticResource();
            var stt = loaiChuKys.Find(p => p.MaDacBiet.Equals(maDacBiet)).STT;

            var leftPoint = -1;
            var bottomPoint = -1;
            var width = -1;
            var height = -1;

            Image image = ImageHelper.DrawText(noiDung, new System.Drawing.Font("Times New Roman", 14.0f), Color.Black, Color.White, maDacBiet.Length * 11);
            var imageName = folderName + "\\image" + "\\" + "sign_" + DateTime.UtcNow.ToString("yyMMdd_hhMMss") + ".png";

            using (var bmp = (Bitmap)image)
            {
                bmp.Save(imageName, System.Drawing.Imaging.ImageFormat.Png);
                bmp.Dispose();
            }

            if (string.IsNullOrEmpty(noiDung))
            {
                pdf.SignatureAppearance = PdfSignatureAppearance.RenderingMode.DESCRIPTION;
                pdf.ShowDate = false;
                pdf.ShowEmail = false;
                pdf.ShowJob = false;
                pdf.ShowLabel = false;
                pdf.ShowOrg = false;

                width = 60;
                height = 60;
                leftPoint = (int)rect.Left;
                bottomPoint = (int)rect.Bottom - 60;
            }
            else
            {

                pdf.SignatureAppearance = PdfSignatureAppearance.RenderingMode.GRAPHIC;
                pdf.SignatureImage = System.Drawing.Image.FromFile(imageName);

                width = maDacBiet.Length * 6;
                height = 16;

                if (!maDacBiet.Equals("#ChuKyNhay"))
                {
                    leftPoint = (int)rect.Left;
                    bottomPoint = (int)rect.Bottom;
                }
                else
                {
                    if (soChuKyNhayDaCo >= 3)
                    {
                        leftPoint = (int)rect.Left + ((soChuKyNhayDaCo % 3) * width);
                        bottomPoint = (int)rect.Bottom - (Convert.ToInt32(soChuKyNhayDaCo / 3) * height);
                    }
                    else if (soChuKyNhayDaCo > 0)
                    {
                        leftPoint = (int)rect.Left + (soChuKyNhayDaCo * width);
                        bottomPoint = (int)rect.Bottom;
                    }
                    else
                    {
                        leftPoint = (int)rect.Left;
                        bottomPoint = (int)rect.Bottom;
                    }
                }
            }

            try
            {
                pdf.Sign(1, leftPoint, bottomPoint, width, height); //iPage: trang; llx: toa do X, lly: Toa do y; iWidth: rong; iHeight: cao
                pdf.SignatureImage.Dispose();
                
                image.Dispose();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckMaDacBiet(string folder, string fileName)
        {
            string input = string.Concat(folder, "\\", fileName);
            var loaiChuKys = Constants.Constant.CreateStaticResource();

            foreach (var item in loaiChuKys)
            {
                iTextSharp.text.Rectangle rect = ImageHelper.GetRectPosstionFromPdf(input, item.MaDacBiet);

                if (rect == null || (rect != null && rect.Width <= 0 && rect.Height <= 0))
                {
                    return false;
                }
            }
 
            return true;
        }
    }
}
