namespace eoffice_qn_kyso.App
{
    using eoffice_qn_kyso.Service.Constants;
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
            var registryName = Constant.RegistryName.KY_SO;
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var hasRegistry = RegistryHelper.CheckRegistryKey(registryName);

            if (!hasRegistry)
            {
                RegistryHelper.CreateRegistry(registryName, baseDirectory);
                return;
            }

            if (args.Length == 0)
            {
                return;
            }

            //string temp = "qoffice-ky-so:trangThaiKySo=VAN_BAN_DI;giaiDoanKySo=KY_SO_BAN_HANH;noiDung=MzYsU0RGU0RGXzM0LFF14bqjbmcgTmFtLG5nw6B5IDMgdGjDoW5nIDcgbsSDbSAyMDE5;duThaoId=;chucDanhId=9;id=348;yKien=;token=eyJ4NXQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJraWQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJidXV0aCIsImF1ZCI6ImNOeEt5ZUp3YWZ1X1E1UHAzZlpqNlpKVFRlY2EiLCJuYmYiOjE1NjIxMjc5NzIsImF6cCI6ImNOeEt5ZUp3YWZ1X1E1UHAzZlpqNlpKVFRlY2EiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIiwiaXNzIjoiaHR0cHM6XC9cL3Nzby5sZ3NwLmdyZWVuZ2xvYmFsLnZuOjQ0M1wvb2F1dGgyXC90b2tlbiIsImdyb3VwcyI6WyJJbnRlcm5hbFwvZXZlcnlvbmUiLCJhZG1pbiIsIkFwcGxpY2F0aW9uXC9hZG1pbl90cnVjbGllbnRob25nX1BST0RVQ1RJT04iLCJBcHBsaWNhdGlvblwvdmlldC10ZXN0IiwiQXBwbGljYXRpb25cL0FQSV9QVUJMSVNIRVIiLCJBcHBsaWNhdGlvblwvQVBJX1NUT1JFIiwiQXBwbGljYXRpb25cL3Nzb19pcyIsIkFwcGxpY2F0aW9uXC9zc29fYW0iLCJJbnRlcm5hbFwvY3JlYXRvciIsIkFwcGxpY2F0aW9uXC9lb2ZmaWNlLWRldiIsIkFwcGxpY2F0aW9uXC9zc29fZWkiLCJBcHBsaWNhdGlvblwvZW9mZmljZS1sb2NhbCIsIkFwcGxpY2F0aW9uXC9lb2ZmaWNlLXRlc3QyIiwiQXBwbGljYXRpb25cL2VvZmZpY2UtdGVzdDEiXSwiZXhwIjoxNTYyNDg3OTcyLCJpYXQiOjE1NjIxMjc5NzIsImp0aSI6ImVhMzI3MDU0LWJjYmYtNGY4My05ZWZkLTgyYmU3MzY5NmQxMiJ9.OjjYtU6hsxeIXrbM7E74jYHQcLqEfgzSGs8T_dfvYtXvBXea8LBlU461rRqScZ1XKJuY5I2igw6E1QE1LbCUmaLC6fXAW3ysAPEOtRAblXiRr8tFhUO6PgEHAU79G0r9bPRINWk8nwS4vlVPsGezEI3bfkljtGtdt8TAutHZXpp9yQZpOXEn6fbsdLJVjOx9WOue0U96s-37PJcj2T74z9oRAZZRb_UvWC57RaeR7cpv2m5uULd3fO31aYlgm4fwyG7TP0q1FGzoBpwxN2_c5rCG4BhJ6aKNi-e62os_iUCYor7nZGA3LJTXc93Es3is2CW0xv0tD15AdUBvBZoAWg";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] arrStr = StringHelper.ReadDataInput(args[0]).ToArray();
            //string[] arrStr = StringHelper.ReadDataInput(temp).ToArray();
            if (arrStr.Length != 8)
            {
                return;
            }

            Application.Run(new KySoBackgroundForm(arrStr[0], arrStr[1], arrStr[2], arrStr[3], arrStr[4], arrStr[5], arrStr[6], arrStr[7]));
        }
    }
}
