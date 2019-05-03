namespace eoffice_qn_kyso.Service.Helpers
{
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;
    using eoffice_qn_kyso.Service.TextExtractions;
    using System;
    using System.Drawing;
    using System.IO;

    public class ImageHelper
    {
        public static iTextSharp.text.Rectangle GetRectPosstionFromPdf(string input, string text)
        {
            if (File.Exists(input))
            {
                PdfReader reader = new PdfReader(input);

                //LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                LocationTextExtractionStrategyEx strategy = new LocationTextExtractionStrategyEx(text);
                string currentText = PdfTextExtractor.GetTextFromPage(reader, 1, strategy);

                if (strategy.myPoints != null && strategy.myPoints.Count > 0)
                {
                    return strategy.myPoints[0].Rect;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public static Image DrawText(String text, Font font, Color textColor, Color backColor, int backgroundWidth)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap(backgroundWidth, 24);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}
