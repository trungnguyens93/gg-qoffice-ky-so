namespace eoffice_qn_kyso.Service.Helpers
{
    using Microsoft.Win32;

    public class RegistryHelper
    {
        public static bool CheckRegistryKey(string registryName)
        {
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(registryName);
            
            return registryKey != null ? true : false;
        }

        public static void CreateRegistry(string registryName, string folderRoot)
        {
            RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(registryName);
            registryKey.SetValue("", "URL:" + registryName + " Protocol");
            registryKey.SetValue("URL Protocol", registryName);

            RegistryKey iconRegistryKey = registryKey.CreateSubKey("DefaultIcon");
            iconRegistryKey.SetValue("", folderRoot + "eoffice-qn-kyso.App.exe");

            RegistryKey shellRegistryKey = registryKey.CreateSubKey("shell");
            RegistryKey openRegistryKey = shellRegistryKey.CreateSubKey("open");
            RegistryKey commandRegistryKey = openRegistryKey.CreateSubKey("command");
            commandRegistryKey.SetValue("", folderRoot + "eoffice-qn-kyso.App.exe \"%1\"");

            iconRegistryKey.Close();
            commandRegistryKey.Close();
            openRegistryKey.Close();
            shellRegistryKey.Close();
            registryKey.Close();
        }
    }
}
