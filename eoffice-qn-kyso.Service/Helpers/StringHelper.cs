namespace eoffice_qn_kyso.Service.Helpers
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// String helper
    /// </summary>
    public class StringHelper
    { 
        /// <summary>
        /// Convert input name to output name
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>Output name</returns>
        public static String ConvertInputToOutput(string fileName)
        {
            var result = string.Empty;

            if (fileName.IndexOf("signed") < 0)
            {
                string subResult = fileName.Substring(0, fileName.Length - 4);
                result = String.Concat(subResult, ".signed.", "pdf");
            }
            else
            {
                result = fileName;
            }

            return result;
        }

        /// <summary>
        /// Convert input name to output name
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="number">Number variable</param>
        /// <returns>Output name</returns>
        public static String ConvertInputToOutput(string fileName, int number)
        {
            var result = string.Empty;

            string subResult = fileName.Substring(0, fileName.Length - 4);
            result = String.Concat(subResult, $".signed{number}.", "pdf");

            return result;
        }

        /// <summary>
        /// Convert file name from Word name to Pdf name
        /// </summary>
        /// <param name="fileName">File name of Word</param>
        /// <returns>File name of Pdf</returns>
        public static String ConvertFileNameFromWordToPdf(string fileName)
        {
            var result = string.Empty;
            
            if (fileName.IndexOf(".docx") > 0)
            {
                result = fileName.Replace(".docx", ".pdf");
            }

            if (fileName.IndexOf(".doc") > 0)
            {
                result = fileName.Replace(".doc", ".pdf");
            }

            return result;
        }

        /// <summary>
        /// Get list of data from string
        /// </summary>
        /// <param name="arg">list of data as string</param>
        /// <returns>List of data</returns>
        public static List<string> ReadDataInput(string arg)
        {
            List<string> result = new List<string>(); ;

            var strSubTitle = arg.Substring(14);

            var arrStr = strSubTitle.Split(';');
            
            for (int i=0; i<arrStr.Length; i++)
            {
                var index = arrStr[i].IndexOf('=');

                if (index >= 0)
                {
                    result.Add(arrStr[i].Substring(index + 1));
                }
                else
                {
                    result.Add(null);
                }
            }

            return result;
        }
    }
}
