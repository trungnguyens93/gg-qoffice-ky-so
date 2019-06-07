namespace eoffice_qn_kyso.Service.Helpers
{
    using Spire.Doc;
    using System;

    public class FileHelper
    {
        public static string ConvertWordToPdf(string path, string fileName)
        {
            Document document = new Document();
            try
            {
                var result = string.Empty;

                string[] arrStr = fileName.Split('.');
                result = String.Concat(arrStr[0], ".", "pdf");

                document.LoadFromFile(path + "\\" + "input" + "\\" + fileName);
                document.SaveToFile(path + "\\" + "input" + "\\" + result, FileFormat.PDF);

                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}