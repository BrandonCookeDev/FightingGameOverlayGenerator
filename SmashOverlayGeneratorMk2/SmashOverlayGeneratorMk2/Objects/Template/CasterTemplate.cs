using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SmashOverlayGeneratorMk2.Objects.Points;

namespace SmashOverlayGeneratorMk2.Objects
{
    class CasterTemplate : Template
    {
        private CasterPoint caster1Point;
        private CasterPoint caster2Point;
        private TwitterPoint caster1TwitterPoint;
        private TwitterPoint caster2TwitterPoint;

        //
        private Font caster1Font;
        private Font caster2Font;
        private Font twitterFont;

        public CasterTemplate(string filePath) : base(filePath) { }

        public CasterTemplate(string filePath,
                              CasterPoint caster1, CasterPoint caster2)
            : base(filePath)
        {
            if (caster1 == null)
                throw new Exception("Caster Template: Caster 1 must have a point on template");
            if (caster2 == null)
                throw new Exception("Caster Template: Caster 2 must have a point on template");
           
            this.caster1Point = caster1;
            this.caster2Point = caster2;
        }

        public CasterTemplate(string filePath, 
                              bool caster1Checked, bool caster2Checked,
                              CasterPoint caster1, CasterPoint caster2,
                              TwitterPoint caster1Twitter,
                              TwitterPoint caster2Twitter)
            : base(filePath)
        {
            if (caster1Checked && caster1 == null)
                throw new Exception("Caster Template: Caster 1 must have a point on template");
            if (caster2Checked && caster2 == null)
                throw new Exception("Caster Template: Caster 2 must have a point on template");
          
            this.caster1Point = caster1;
            this.caster2Point = caster2;
            this.caster1TwitterPoint = caster1Twitter;
            this.caster2TwitterPoint = caster2Twitter;
        }

        public CasterPoint Caster1Point
        {
            get { return this.caster1Point; }
            set { this.caster1Point = value; }
        }

        public CasterPoint Caster2Point
        {
            get { return this.caster2Point; }
            set { this.caster2Point = value; }
        }

        public TwitterPoint Caster1TwitterPoint
        {
            get { return this.caster1TwitterPoint; }
            set { this.caster1TwitterPoint = value; }
        }

        public TwitterPoint Caster2TwitterPoint
        {
            get { return this.caster2TwitterPoint; }
            set { this.caster2TwitterPoint = value; }
        }

        public Font Caster1Font
        {
            get { return this.caster1Font; }
            set { this.caster1Font = value; }
        }

        public Font Caster2Font
        {
            get { return this.caster2Font; }
            set { this.caster2Font = value; }
        }
        public Font TwitterFont
        {
            get { return this.twitterFont; }
            set { this.twitterFont = value; }
        }

        public void setFontSizes(string caster1, string caster2)
        {
            int[] fontSize = determineFontSize(caster1, caster2);
            
            caster1Font = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            caster2Font = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            twitterFont = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold);

        }

        public int[] determineFontSize(string caster1, string caster2)
        {
            int[] fontSizeArr = new int[2];
            int MAX = 20, MEDIUM = 17, SMALL = 15, SMALL_PLUS = 13;

            if (caster1.Length >= 23) fontSizeArr[0] = SMALL_PLUS;
            else if (caster1.Length >= 20) fontSizeArr[0] = SMALL;
            else if (caster1.Length >= 17) fontSizeArr[0] = MEDIUM;
            else fontSizeArr[0] = MAX;

            if (caster2.Length >= 23) fontSizeArr[1] = SMALL_PLUS;
            else if (caster2.Length >= 20) fontSizeArr[1] = SMALL;
            else if (caster2.Length >= 17) fontSizeArr[1] = MEDIUM;
            else fontSizeArr[1] = MAX;

            return fontSizeArr;        
        }

        public override Bitmap drawTextOnImage(SmashOverlayGenerator form)
        {
            setFontSizes(form.Caster1, form.Caster2);

            //CENTER NAME TEXT
            StringFormat nameFormat = new StringFormat();
            StringFormat twitterFormat = new StringFormat();

            nameFormat.LineAlignment = StringAlignment.Near;
            nameFormat.Alignment = StringAlignment.Near;
            twitterFormat.Alignment = StringAlignment.Near;

            Bitmap image = base.getImage();
            Graphics g = Graphics.FromImage(image);

            g.DrawString(form.Caster1, Caster1Font, Brushes.White, Caster1Point.getPoint(), nameFormat);
            g.DrawString(form.Caster2, Caster2Font, Brushes.White, Caster2Point.getPoint(), nameFormat);
            g.DrawString(form.Caster1Twitter, TwitterFont, Brushes.White, Caster1TwitterPoint.getPoint(), twitterFormat);
            g.DrawString(form.Caster2Twitter, TwitterFont, Brushes.White, Caster2TwitterPoint.getPoint(), twitterFormat);

            return image;
        }

        public override void saveImage(Bitmap image)
        {
            String fileName = base.FilePath;
            fileName = fileName.Substring(fileName.LastIndexOf(".Caster.") + 8);
            fileName = fileName.Substring(0, fileName.Length - 4) + "_official.png";
            image.Save(fileName);
        }
    }
}
