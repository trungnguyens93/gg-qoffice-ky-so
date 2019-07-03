namespace eoffice_qn_kyso.Service.Helpers
{
    using System.IO;

    /// <summary>
    /// Folder helper
    /// </summary>
    public class FolderHelper
    {
        /// <summary>
        /// Create folder for storing templte files
        /// </summary>
        public static void CreateProcessFolder(string path)
        {
            DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
            directoryInfo.CreateSubdirectory("input");
            directoryInfo.CreateSubdirectory("output");
            directoryInfo.CreateSubdirectory("image");
        }

        /// <summary>
        /// Delete all files and folders in that folder has been created
        /// </summary>
        public static void DeleteTempFolder(string rootFolder, string subFolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(rootFolder);

            foreach (var folder in directoryInfo.GetDirectories())
            {
                if (folder.Name.Equals(subFolder))
                {
                    foreach (var folder2 in folder.GetDirectories())
                    {
                        foreach (var file in folder2.GetFiles())
                        {
                            file.Delete();
                        }

                        folder2.Delete();
                    }

                    folder.Delete();
                }
            }
        }
    }
}
