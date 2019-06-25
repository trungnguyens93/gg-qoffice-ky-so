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

            //string temp = "qoffice-ky-so:trangThaiKySo=VAN_BAN_DI;giaiDoanKySo=KY_SO_BAN_HANH;noiDung=MjUsMjUvU+G7nyBnaWFvIHRow7RuZyB24bqtbiB04bqjaSxRdeG6o25nIE5hbSxuZ8OgeSAyNCB0aMOhbmcgNiBuxINtIDIwMTk=;duThaoId=;chucDanhId=9;id=298;yKien=;token=eyJ4NXQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJraWQiOiJNekF6TjJNeVlUY3paakF3TnpWak9HSTRNbVE1TUROaU5HTTRabVZrTmpJM1lUaGpaREkxTWciLCJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJidXV0aCIsImF1ZCI6ImNOeEt5ZUp3YWZ1X1E1UHAzZlpqNlpKVFRlY2EiLCJuYmYiOjE1NjEzNjg5MzksImF6cCI6ImNOeEt5ZUp3YWZ1X1E1UHAzZlpqNlpKVFRlY2EiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIiwiaXNzIjoiaHR0cHM6XC9cL3Nzby5sZ3NwLmdyZWVuZ2xvYmFsLnZuOjQ0M1wvb2F1dGgyXC90b2tlbiIsImdyb3VwcyI6WyJJbnRlcm5hbFwvZXZlcnlvbmUiLCJhZG1pbiIsIkFwcGxpY2F0aW9uXC9hZG1pbl90cnVjbGllbnRob25nX1BST0RVQ1RJT04iLCJBcHBsaWNhdGlvblwvdmlldC10ZXN0IiwiQXBwbGljYXRpb25cL0FQSV9QVUJMSVNIRVIiLCJBcHBsaWNhdGlvblwvQVBJX1NUT1JFIiwiQXBwbGljYXRpb25cL3Nzb19pcyIsIkFwcGxpY2F0aW9uXC9zc29fYW0iLCJJbnRlcm5hbFwvY3JlYXRvciIsIkFwcGxpY2F0aW9uXC9lb2ZmaWNlLWRldiIsIkFwcGxpY2F0aW9uXC9zc29fZWkiLCJBcHBsaWNhdGlvblwvZW9mZmljZS1sb2NhbCIsIkFwcGxpY2F0aW9uXC9lb2ZmaWNlLXRlc3QyIiwiQXBwbGljYXRpb25cL2VvZmZpY2UtdGVzdDEiXSwiZXhwIjoxNTYxNzI4OTM5LCJpYXQiOjE1NjEzNjg5MzksImp0aSI6IjdkZWNjZDg3LTMzZGEtNDcwMi04NTJkLWQwYzdkYzVjOTU5YiJ9.TmG3Q88OThHp_AhTbhUlgU3EDDt35UZazn2MMFrt6inQzl8zuHkE_U1n4V4loGAPJ-4VpLz1xcinaCr6H5Ig-q9eymVCxav3IQODJN1HcOlNfmpiUzXCucD5EQjwEYLsJESW4DA_rxRoK496VDg1_uCzobAyv3cHD9tXqT4JUdokjFSYkD71NZt4ZTub-LMOCxhrP6PE6_bBbQzPt56GS8gTj_Oz60oJ2sN_GPoWx2uP2aHqwiqIbJxjXvgknZNxT2nJ9FLbpUyvr-JU69I6F5jYv2n_JpIaumajngDDc_7el73_ers0QyOboAwmGAldkNqRYeChvR_4gpBg2B5W5w";

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
