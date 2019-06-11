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

            //if (args.Length == 0)
            //{
            //    return;
            //}

            string temp = "qOffice-ky-so:trangThaiKySo=HO_SO_CONG_VIEC;giaiDoanKySo=KY_SO_LANH_DAO;noidung=88,trungnt,trungnt,trungnt;duThaoId=175;chucDanhId=10;id=184;yKien=Nguyễn Thành trung;token=eyJ4NXQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJraWQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJhbnZ0IiwiYXVkIjoiY054S3llSndhZnVfUTVQcDNmWmo2WkpUVGVjYSIsIm5iZiI6MTU2MDIzNDA5NSwiYXpwIjoiY054S3llSndhZnVfUTVQcDNmWmo2WkpUVGVjYSIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUiLCJpc3MiOiJodHRwczpcL1wvc3NvLmxnc3AuZ3JlZW5nbG9iYWwudm46NDQzXC9vYXV0aDJcL3Rva2VuIiwiZ3JvdXBzIjpbIkFwcGxpY2F0aW9uXC9zc29faXMiLCJBcHBsaWNhdGlvblwvc3NvX2FtIiwiSW50ZXJuYWxcL2NyZWF0b3IiLCJJbnRlcm5hbFwvZXZlcnlvbmUiLCJhZG1pbiIsIkFwcGxpY2F0aW9uXC9hZG1pbl90cnVjbGllbnRob25nX1BST0RVQ1RJT04iLCJBcHBsaWNhdGlvblwvc3NvX2VpIiwiQXBwbGljYXRpb25cL3ZpZXQtdGVzdCIsIkFwcGxpY2F0aW9uXC9lb2ZmaWNlLWxvY2FsIiwiQXBwbGljYXRpb25cL0FQSV9QVUJMSVNIRVIiLCJBcHBsaWNhdGlvblwvQVBJX1NUT1JFIiwiQXBwbGljYXRpb25cL2VvZmZpY2UtdGVzdDEiXSwiZXhwIjoxNTYwNTk0MDk1LCJpYXQiOjE1NjAyMzQwOTUsImp0aSI6ImNhZmFhNDEyLTM3YWUtNDUyYi05YTI2LTc5NGI5YmI3Yjk2YiJ9.keI1c17CsonRpMuYSbM9OhruBtZMflIairGE8psNSEnJEnMd_QpJtmzlJz7t2g6Rt88g4zsJFOLHnClVgWJOKO8ZzBkM4aYWr5wEu9Vr14fMnN2XH0RciJRNzUoy0jaBedAvhpqeKHE3OpGVv1snvmOATRdkMV6PurBPuwuLRojnXG8hf44tmBjrR9DJnX_DBm7wFOkbjvpEiGsFMX7TlXdM5LaAq5MkpxKYva-Jig0R5Ow01QV3eiy77-0-PNq1fjbB7rQjtwWqJsSATlu1W2LEQEK61xJXx2zUNTNRiuHlj4NvtqEV-fz-ATHIO62s4DvyiQcGLZePXLnmv_BVJg";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string[] arrStr = StringHelper.ReadDataInput(args[0]).ToArray();
            string[] arrStr = StringHelper.ReadDataInput(temp).ToArray();
            if (arrStr.Length != 8)
            {
                return;
            }

            Application.Run(new KySoBackgroundForm(arrStr[0], arrStr[1], arrStr[2], arrStr[3], arrStr[4], arrStr[5], arrStr[6], arrStr[7]));
        }
    }
}
