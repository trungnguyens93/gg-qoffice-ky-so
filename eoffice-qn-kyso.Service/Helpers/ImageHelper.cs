﻿namespace eoffice_qn_kyso.Service.Helpers
{
    using System;
    using System.Drawing;
    using System.IO;
    using iTextSharp.text.pdf;
    using iTextSharp.text.pdf.parser;
    using eoffice_qn_kyso.Service.TextExtractions;

    /// <summary>
    /// Image helper
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// Get position of text from PDF file 
        /// </summary>
        /// <param name="input">PDF file</param>
        /// <param name="text">Text</param>
        /// <param name="page">Page</param>
        /// <returns>Position of text</returns>
        public static iTextSharp.text.Rectangle GetRectPositionFromPdf(string input, string text, int page)
        {
            if (File.Exists(input))
            {
                PdfReader reader = new PdfReader(input);

                //LocationTextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                LocationTextExtractionStrategyEx strategy = new LocationTextExtractionStrategyEx(text);
                string currentText = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                reader.Dispose();

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

        /// <summary>
        /// Create a picture
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="font">Font</param>
        /// <param name="textColor">Text color</param>
        /// <param name="backColor">Background color</param>
        /// <param name="backgroundWidth">Background Width</param>
        /// <returns></returns>
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
