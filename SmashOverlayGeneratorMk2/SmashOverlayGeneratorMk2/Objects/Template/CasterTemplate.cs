using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using SmashOverlayGeneratorMk2.Objects.Points;
using SmashOverlayGeneratorMk2.General;

namespace SmashOverlayGeneratorMk2.Objects
{
    class CasterTemplate : Template
    {
        #region GLOBAL VARIABLES
        private CasterPoint caster1Point;
        private CasterPoint caster2Point;
        private TwitterPoint caster1TwitterPoint;
        private TwitterPoint caster2TwitterPoint;

        //
        private Font caster1Font;
        private Font caster2Font;
        private Font twitterFont;
        #endregion GLOBAL VARIABLES

        #region CONSTRUCTORS
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
        #endregion CONSTRUCTORS

        #region GETTERS AND SETTERS
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
        #endregion GETTERS AND SETTERS

        #region ABSTRACTED METHODS
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
            string targetPath = base.ParentPath + "\\Caster";

            String fileName = base.FilePath;
            fileName = fileName.Substring(fileName.LastIndexOf(".Caster.") + 8);
            fileName = fileName.Substring(0, fileName.Length - 4) + "_official.png";
            image.Save(fileName);
            
            if(!Directory.Exists(base.ParentPath)){
                Directory.CreateDirectory(base.ParentPath);
            } 
            if(!Directory.Exists(targetPath)){
                Directory.CreateDirectory(targetPath);
            }

            image.Save(targetPath + "\\" + fileName);
        }
        #endregion ABSTRACTED METHODS

        public void setFontSizes(string caster1, string caster2)
        {
            int[] fontSize = GenFcns.determineFontSize(caster1, caster2);

            caster1Font = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            caster2Font = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            twitterFont = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold);

        }

    }
}
