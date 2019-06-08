namespace eoffice_qn_kyso.App
{
    using eoffice_qn_kyso.Service.Helpers;
    using System;
    using System.Configuration;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            var registryName = ConfigurationManager.AppSettings.Get("registryName");
            var folderRoot = ConfigurationManager.AppSettings.Get("ftpClientRootFolder");

            var hasRegistry = RegistryHelper.CheckRegistryKey(registryName);

            if (!hasRegistry)
            {
                RegistryHelper.CreateRegistry(registryName, folderRoot);
                return;
            }

            if (args.Length == 0)
            {
                return;
            }

            //string temp = "qOffice-ky-so:urlFileKySo=FileCongVan.pdf;loaiChuKy=CHU_KY_KHONG_DAU;maDacBiet=#ChuKyCoDau;noidung=88;urlFileAnhChuKy=icon.png;duThaoId=123;chucDanhId=10;hscvId=123;yKien=Nguyễn Thành trung;token=eyJ4NXQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJraWQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJ0cnVuZ250QGNhcmJvbi5zdXBlciIsImF1ZCI6ImNOeEt5ZUp3YWZ1X1E1UHAzZlpqNlpKVFRlY2EiLCJuYmYiOjE1NTk4NzI0MjAsImF6cCI6ImNOeEt5ZUp3YWZ1X1E1UHAzZlpqNlpKVFRlY2EiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIiwiaXNzIjoiaHR0cHM6XC9cL3Nzby5sZ3NwLmdyZWVuZ2xvYmFsLnZuOjQ0M1wvb2F1dGgyXC90b2tlbiIsImdyb3VwcyI6WyJBcHBsaWNhdGlvblwvc3NvX2lzIiwiQXBwbGljYXRpb25cL3Nzb19hbSIsIkludGVybmFsXC9jcmVhdG9yIiwiSW50ZXJuYWxcL2V2ZXJ5b25lIiwiYWRtaW4iLCJBcHBsaWNhdGlvblwvYWRtaW5fdHJ1Y2xpZW50aG9uZ19QUk9EVUNUSU9OIiwiQXBwbGljYXRpb25cL3Nzb19laSIsIkFwcGxpY2F0aW9uXC92aWV0LXRlc3QiLCJBcHBsaWNhdGlvblwvZW9mZmljZS1sb2NhbCIsIkFwcGxpY2F0aW9uXC9BUElfUFVCTElTSEVSIiwiQXBwbGljYXRpb25cL0FQSV9TVE9SRSIsIkFwcGxpY2F0aW9uXC9lb2ZmaWNlLXRlc3QxIl0sImV4cCI6MTU2MDIzMjQyMCwiaWF0IjoxNTU5ODcyNDIwLCJqdGkiOiI2Zjg1YjQzYi0xZmFjLTQyNjUtODIwNy02ZmEzZDg0NTM3YTcifQ.kqLhq_0KG27TUIAnmNCi6h4Pho5A4fZOPwFrgm_GXhqKUNh9SAVSlFSk86cn9GCCAVwWsp-UrTGS5m8x1um7BXStIUJiD_qrUTzX4u01Cz1us83vuvljcAIMNeCwSmlOX5hEoZTRRSPKGVHAJYipAD-_ozY0O-6IdtJKR2FNgvIhrcXTK-1-sa2a64gYDt9yjHXZxFHvEwlecgi3t5VxIZ3b576Sf3ankps6fzvyi8z7Q9Wv1nEb54dkjukMpV7z43wsjKbr8jb1PfSzl5qaDvZLV1_ku_H_yEkPwar3gLWab47heQ_0DKbuRkPpgMZfOle1cEqQzmkxKHCdvDbXMg";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] arrStr = StringHelper.ReadDataInput(args[0]).ToArray();
            //string[] arrStr = StringHelper.ReadDataInput(temp).ToArray();
            Application.Run(new KySoBackgroundForm(arrStr[0], arrStr[1], arrStr[2], arrStr[3], arrStr[4], arrStr[5], arrStr[6], arrStr[7], arrStr[8], arrStr[9]));
        }
    }
}
