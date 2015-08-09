using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using SmashOverlayGeneratorMk2.General;
using SmashOverlayGeneratorMk2.Objects.Points;

namespace SmashOverlayGeneratorMk2.Objects
{
    class MatchupPictureTemplate : Template
    {
        #region GLOBAL VARIABLES
        private string gametype;
        private string tournamentName;
        private string tournamentImage;
        private string competitor1;
        private string competitor2;
        private string competitor3;
        private string competitor4;
        private string character1;
        private string character2;
        private string character3;
        private string character4;
        private string character1Path;
        private string character2Path;
        private string tournamentRound;
        private Font competitor1Font;
        private Font competitor2Font;
        private Font competitor3Font;
        private Font competitor4Font;
        private Font tournamentFont;
        private Font tournamentRoundFont;
        private CompetitorPoint competitor1Point;
        private CompetitorPoint competitor2Point;
        private CompetitorPoint competitor3Point;
        private CompetitorPoint competitor4Point;
        private CharacterPoint character1Point;
        private CharacterPoint character2Point;
        private CharacterPoint character3Point;
        private CharacterPoint character4Point;
        private TournamentPoint tournamentPoint;
        private TournamentPoint tournamentRoundPoint;
        #endregion GLOBAL VARIABLES

        #region GETTERS AND SETTERS
        public string TournamentRound
        {
            get { return this.tournamentRound; }
            set { this.tournamentRound = value; }
        }

        public string Gametype
        {
            get{ return this.gametype; }
            set{ this.gametype = value; }
        }
        public string TournamentName
        {
            get { return this.tournamentName; }
            set { this.tournamentName = value; }
        }
        public string TournamentImage
        {
            get { return this.tournamentImage; }
            set { this.tournamentImage = value; }
        }
        public string Competitor1
        {
            get { return this.competitor1; }
            set { this.competitor1 = value; }
        }
        public string Competitor2
        {
            get { return this.competitor2; }
            set { this.competitor2 = value; }
        }
        public string Competitor3
        {
            get { return this.competitor3; }
            set { this.competitor3 = value; }
        }
        public string Competitor4
        {
            get { return this.competitor4; }
            set { this.competitor4 = value; }
        }
        public string Characer1
        {
            get { return this.character1; }
            set { this.character1 = value; }
        }
        public string Characer2
        {
            get { return this.character2; }
            set { this.character2 = value; }
        }
        public string Characer3
        {
            get { return this.character3; }
            set { this.character3 = value; }
        }
        public string Characer4
        {
            get { return this.character4; }
            set { this.character4 = value; }
        }
        public string Character1Path
        {
            get { return this.character1Path; }
            set { this.character1Path = value; }
        }
        public string Character2Path
        {
            get { return this.character2Path; }
            set { this.character2Path = value; }
        }
        public Font Competitor1Font
        {
            get { return this.competitor1Font; }
            set { this.competitor1Font = value; }
        }
        public Font Competitor2Font
        {
            get { return this.competitor2Font; }
            set { this.competitor2Font = value; }
        }
        public Font Competitor3Font
        {
            get { return this.competitor3Font; }
            set { this.competitor3Font = value; }
        }
        public Font Competitor4Font
        {
            get { return this.competitor4Font; }
            set { this.competitor4Font = value; }
        }
        public Font TournamentFont
        {
            get { return this.tournamentFont; }
            set { this.tournamentFont = value; }
        }
        public Font TournamentRoundFont
        {
            get { return this.tournamentRoundFont; }
            set { this.tournamentRoundFont = value; }
        }
        public CompetitorPoint Competitor1Point
        {
            get { return this.competitor1Point; }
            set { this.competitor1Point = value; }
        }
        public CompetitorPoint Competitor2Point
        {
            get { return this.competitor2Point; }
            set { this.competitor2Point = value; }
        }
        public CompetitorPoint Competitor3Point
        {
            get { return this.competitor3Point; }
            set { this.competitor3Point = value; }
        }
        public CompetitorPoint Competitor4Point
        {
            get { return this.competitor4Point; }
            set { this.competitor4Point = value; }
        }
        public CharacterPoint Character1Point
        {
            get { return this.character1Point; }
            set { this.character1Point = value; }
        }
        public CharacterPoint Character2Point
        {
            get { return this.character2Point; }
            set { this.character2Point = value; }
        }
        public CharacterPoint Character3Point
        {
            get { return this.character3Point; }
            set { this.character3Point = value; }
        }
        public CharacterPoint Character4Point
        {
            get { return this.character4Point; }
            set { this.character4Point = value; }
        }
        public TournamentPoint TournamentPoint
        {
            get { return this.tournamentPoint; }
            set { this.tournamentPoint = value; }
        }
        public TournamentPoint TournamentRoundPoint
        {
            get { return this.tournamentRoundPoint; }
            set { this.tournamentRoundPoint = value; }
        }
        #endregion GETTERS AND SETTERS

        #region CONSTRUCTORS
        /* CONSTRUCTORS */
        public MatchupPictureTemplate(){}


        public MatchupPictureTemplate(string filePath, string gametype, 
                                      TournamentPoint tournamentPoint, TournamentPoint tournamentRoundPoint,
                                      string character1Path, CharacterPoint character1Point,
                                      CompetitorPoint competitor1Point, 
                                      string character2Path, CharacterPoint character2Point,
                                      CompetitorPoint competitor2Point)
            : base(filePath)
        {
            this.tournamentPoint = tournamentPoint;
            this.tournamentRoundPoint = tournamentRoundPoint;
            this.character1Path = character1Path;
            this.character2Path = character2Path;
            this.competitor1Point = competitor1Point;
            this.competitor2Point = competitor2Point;
            this.character1Point = character1Point;
            this.character2Point = character2Point;
        }
        #endregion CONSTRUCTORS

        #region ABSTRACTED METHODS
        public override System.Drawing.Bitmap drawTextOnImage(SmashOverlayGenerator form)
        {
            //IF FONT IS NOT INSTALLED WE INSTALL IT

            fontsInstalled(form, "EARTHQUAKE");
            fontsInstalled(form, "FLAPHEAD");
            fontsInstalled(form, "ZEKTON RG");

            setFontSizes(form.ResourceType, form.MatchupCompetitor1, form.MatchupCompetitor2);
            if (!GenFcns.isNullOrEmpty(Gametype) && Gametype.Equals("doubles"))
                setFontSizes(form.ResourceType, Competitor3, Competitor4);

            TournamentName = form.TournamentName;
            TournamentRound = form.TournamentRound;
            Competitor1 = form.MatchupCompetitor1;
            Competitor2 = form.MatchupCompetitor2;

            StringFormat nameFormat = new StringFormat();
            StringFormat tournamentFormat = new StringFormat();
            Bitmap image = base.getImage();            
            Graphics g = Graphics.FromImage(image);

            nameFormat.LineAlignment = StringAlignment.Center;
            nameFormat.Alignment = StringAlignment.Center;
            tournamentFormat.Alignment = StringAlignment.Center;

            //DRAW CHARACTERS
            drawCharactersOnImage(g, Character1Path, Character1Point, Character2Path, Character2Point);


            //DRAW BANNER
            Pen p =new Pen(Color.LightCyan, (float)50.0);
            SolidBrush b = new SolidBrush(Color.MediumPurple);
            SolidBrush bBorder = new SolidBrush(Color.White);
            Rectangle r = new Rectangle(new Point(0, 800), new Size(1920, 200));
            Rectangle rBorder = new Rectangle(new Point(0, 780), new Size(1920, 240));
            g.FillRectangle(bBorder, rBorder);
            g.FillRectangle(b, r);

            //DRAW COMPETITOR INFORMATION
            g.DrawString(form.MatchupCompetitor1, Competitor1Font, Brushes.White, Competitor1Point.getPoint(), nameFormat);
            g.DrawString(form.MatchupCompetitor2, Competitor2Font, Brushes.White, Competitor2Point.getPoint(), nameFormat);
            //g.DrawString(form.TournamentName, TournamentFont, Brushes.White, TournamentPoint.getPoint(), tournamentFormat);
            
            
            //DRAW TOURNAMENT STUFFS
            string tournamentLogoPath = ListBoxFcns.getResourcePath(form.ProductName, "tournament", "S@CsLogo.png");
            Image logoImage = base.getImage(tournamentLogoPath);
            g.DrawImage(logoImage, TournamentPoint.getPoint());
            g.DrawString(form.TournamentRound, TournamentRoundFont, Brushes.White, TournamentRoundPoint.getPoint(), tournamentFormat);

            
            Image img = (Image)image;
            Bitmap newImage = new Bitmap(img, new Size(640, 400));

            return newImage;
        }

        public override void saveImage(System.Drawing.Bitmap image)
        {
            string targetPath = base.ParentPath + "\\Matchup";

            String fileName = base.FilePath;
            fileName = fileName.Substring(fileName.LastIndexOf(".Matchup.") + 9);
            fileName = fileName.Substring(0, fileName.Length - 4) + "_official_" + TournamentName + "_" + TournamentRound + "_" + Competitor1 + "vs" + Competitor2+".png";
            image.Save(fileName);

            if (!Directory.Exists(base.ParentPath))
            {
                Directory.CreateDirectory(base.ParentPath);
            }
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            image.Save(targetPath + "\\" + fileName);
        }
        #endregion ABSTRACTED METHODS

        public void drawCharactersOnImage(Graphics g, string character1Path, CharacterPoint character1Point, 
                                                      string character2Path, CharacterPoint character2Point)
        {
            g.DrawImage(base.getImage(character1Path), character1Point.getPoint());

            Image reverseChar2 = (Image)base.getImage(character2Path);
            reverseChar2.RotateFlip(RotateFlipType.Rotate180FlipY);
            g.DrawImage(reverseChar2, character2Point.getPoint());
        }

        public void setFontSizes(string resourceType,
                                 string _competitor1, string _competitor2,
                                 string _competitor3, string _competitor4)
        {
            int[] fontSize = GenFcns.determineFontSize(_competitor1, _competitor2);
            if (resourceType.Equals("file"))
            {
                Competitor1Font = new Font(FontFamily.GenericSansSerif, /*fontSize[0]*/ 42, FontStyle.Bold);
                Competitor2Font = new Font(FontFamily.GenericSansSerif, fontSize[1], FontStyle.Bold);
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
            }
            else
            {
                Competitor1Font = new Font(FontFamily.GenericSansSerif, fontSize[0] + 8, FontStyle.Bold);
                Competitor2Font = new Font(FontFamily.GenericSansSerif, fontSize[1] + 8, FontStyle.Bold);
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
            }

            fontSize = GenFcns.determineFontSize(_competitor3, _competitor4);
            if (resourceType.Equals("file"))
            {
                Competitor3Font = new Font(FontFamily.GenericSansSerif, fontSize[0], FontStyle.Bold);
                Competitor4Font = new Font(FontFamily.GenericSansSerif, fontSize[1], FontStyle.Bold);
            }
            else
            {
                Competitor3Font = new Font(FontFamily.GenericSansSerif, fontSize[0] + 8, FontStyle.Bold);
                Competitor4Font = new Font(FontFamily.GenericSansSerif, fontSize[1] + 8, FontStyle.Bold);
            }
        }

        public void setFontSizes(string resourceType,
                                 string _competitor1, string _competitor2)
        {
            int[] fontSize = GenFcns.determineFontSize(_competitor1, _competitor2);
            Font f = new Font("EARTHQUAKE", 62, FontStyle.Bold);
            if (!GenFcns.isNullOrEmpty(resourceType) && resourceType.Equals("file"))
            {
                Competitor1Font = new Font(FontFamily.GenericSansSerif, 42, FontStyle.Bold);
                Competitor2Font = new Font(FontFamily.GenericSansSerif, 42, FontStyle.Bold);
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
                TournamentRoundFont = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
            }
            else
            {
                Competitor1Font = new Font("EARTHQUAKE", 120, FontStyle.Bold);
                Competitor2Font = new Font("EARTHQUAKE", 120, FontStyle.Bold);                
                TournamentFont = new Font(FontFamily.GenericSansSerif, 70, FontStyle.Bold);
                TournamentRoundFont = new Font("FLAPHEAD", 115, FontStyle.Bold);
            }
        }

        public void fontsInstalled(SmashOverlayGenerator form, string fontName)
        {
            try
            {
                float fontSize = 12;

                using (Font fontTester = new Font(
                        fontName,
                        fontSize,
                        FontStyle.Regular,
                        GraphicsUnit.Pixel))
                {
                    if (String.Equals(fontTester.Name, fontName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Font exists
                    }
                    else
                    {
                        GenFcns.installFonts(form, fontName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
