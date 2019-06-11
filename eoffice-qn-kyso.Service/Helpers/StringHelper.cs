namespace eoffice_qn_kyso.Service.Helpers
{
    using System;
    using System.Collections.Generic;

    public class StringHelper
    { 
        public static String ConvertInputToOutput(string fileName)
        {
            var result = string.Empty;

            if (fileName.IndexOf("signed") < 0)
            {
                string[] arrStr = fileName.Split('.');
                result = String.Concat(arrStr[0], ".signed.", "pdf");
            }
            else
            {
                result = fileName;
            }

            return result;
        }

        public static String ConvertInputToOutput(string fileName, int number)
        {
            var result = string.Empty;

            if (fileName.IndexOf("signed") < 0)
            {
                string[] arrStr = fileName.Split('.');
                result = String.Concat(arrStr[0], $".signed{number}.", "pdf");
            }
            else
            {
                result = fileName;
            }

            return result;
        }

        public static String ConvertFileNameFromWordToPdf(string fileName)
        {
            var result = string.Empty;

            string[] arrStr = fileName.Split('.');
            result = String.Concat(arrStr[0], ".", "pdf");

            return result;
        }

        public static List<string> ReadDataInput(string arg)
        {
            List<string> result = new List<string>(); ;

            var strSubTitle = arg.Substring(14);

            var arrStr = strSubTitle.Split(';');
            
            for (int i=0; i<arrStr.Length; i++)
            {
                if (arrStr[i].IndexOf('=') >= 0)
                {
                    var arrStr1 = arrStr[i].Split('=');
                    result.Add(arrStr1[1]);
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
