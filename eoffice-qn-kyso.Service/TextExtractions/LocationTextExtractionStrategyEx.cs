namespace eoffice_qn_kyso.Service.TextExtractions
{
    using iTextSharp.text.pdf.parser;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LocationTextExtractionStrategyEx : LocationTextExtractionStrategy
    {
        //Hold each coordinate
        public List<RectAndText> myPoints = new List<RectAndText>();

        public String CurrentVerb = String.Empty;

        public float llx;
        public float lly;
        public float rux;
        public float ruy;

        public float Right;

        //The string that we're searching for
        public String TextToSearchFor { get; set; }

        //How to compare strings
        public System.Globalization.CompareOptions CompareOptions { get; set; }

        public LocationTextExtractionStrategyEx(String textToSearchFor, System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.None)
        {
            this.TextToSearchFor = textToSearchFor;
            this.CompareOptions = compareOptions;
        }

        //Automatically called for each chunk of text in the PDF
        public override void RenderText(TextRenderInfo renderInfo)
        {
            base.RenderText(renderInfo);
            
            var t1 = renderInfo.GetText().IndexOf('#');

            if (t1 >= 0)
            {
                if (string.IsNullOrEmpty(this.CurrentVerb))
                {
                    var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(renderInfo.GetText(), "#", this.CompareOptions);

                    var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take("#".Length).ToList();

                    //Grab the first and last character
                    var firstChar = chars.First();
                    var bottomLeft = firstChar.GetDescentLine().GetStartPoint();
                    this.llx = bottomLeft[Vector.I1];
                    this.lly = bottomLeft[Vector.I2] - 2;

                    this.CurrentVerb += renderInfo.GetText().Substring(t1);
                }
                else
                {
                    var str = string.Concat(this.CurrentVerb, renderInfo.GetText());

                    if (str.IndexOf(this.TextToSearchFor) >= 0)
                    {
                        var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(renderInfo.GetText(), this.TextToSearchFor, this.CompareOptions);
                        var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take(this.TextToSearchFor.Length).ToList();
                        var lastChar = chars.Last();
                        var topRight = lastChar.GetDescentLine().GetEndPoint();
                        this.rux = this.llx + (this.TextToSearchFor.Length * 5);
                        this.ruy = this.lly + 16;

                        //Create a rectangle from it
                        var rect = new iTextSharp.text.Rectangle(this.llx, this.lly, this.rux, this.ruy);

                        //Add this to our main collection
                        this.myPoints.Add(new RectAndText(rect, this.TextToSearchFor));
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(this.CurrentVerb))
                {
                    this.CurrentVerb += renderInfo.GetText();
                }

                //if (this.TextToSearchFor.IndexOf(this.CurrentVerb) >= 0)
                if (this.CurrentVerb.IndexOf(this.TextToSearchFor) >= 0)
                {
                    //if (this.TextToSearchFor.Equals(this.CurrentVerb))
                    //{
                        var startPosition = System.Globalization.CultureInfo.CurrentCulture.CompareInfo.IndexOf(renderInfo.GetText(), this.TextToSearchFor, this.CompareOptions);
                        var chars = renderInfo.GetCharacterRenderInfos().Skip(startPosition).Take(this.TextToSearchFor.Length).ToList();
                        var lastChar = chars.Last();
                        var topRight = lastChar.GetDescentLine().GetEndPoint();
                        this.rux = this.llx + (this.TextToSearchFor.Length * 5);
                        this.ruy = this.lly + 16;

                        //Create a rectangle from it
                        var rect = new iTextSharp.text.Rectangle(this.llx, this.lly, this.rux, this.ruy);

                        //Add this to our main collection
                        this.myPoints.Add(new RectAndText(rect, this.TextToSearchFor));
                    //}
                }
                else
                {
                    if (this.TextToSearchFor.IndexOf(this.CurrentVerb) < 0) {
                        this.CurrentVerb = string.Empty;
                        this.llx = -1;
                        this.lly = -1;
                        this.rux = -1;
                        this.ruy = -1;
                    }
                }
            }
        }
    }
}
