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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] arrStr = StringHelper.ReadDataInput(args[0]).ToArray();
            Application.Run(new KySoBackgroundForm(arrStr[0], arrStr[1], arrStr[2], Convert.ToInt32(arrStr[3])));
        }
    }
}
