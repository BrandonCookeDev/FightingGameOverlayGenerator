using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using SmashOverlayGeneratorMk2.Objects.Points;
using SmashOverlayGeneratorMk2.General;

namespace SmashOverlayGeneratorMk2.Objects
{
    class CompetitorTemplate : Template
    {
        #region GLOBAL VARIABLES
        private CompetitorPoint competitor1Point;
        private CompetitorPoint competitor2Point;
        private CompetitorPoint competitor1WebcamPoint;
        private CompetitorPoint competitor2WebcamPoint;
        private ScorePoint score1Point;
        private ScorePoint score2Point;
        private TournamentPoint tournamentNameRoundPoint;
        
        //DETERMINE FONT SIZE
        private Font name1Font;
        private Font name2Font;
        private Font scoreFont;
        private Font tournmentFont;
        #endregion GLOBAL VARIABLES

        #region CONSTRUCTORS
        public CompetitorTemplate(string filePath) : base(filePath) { }

        public CompetitorTemplate(string filePath,
                                  CompetitorPoint competitor1, 
                                  CompetitorPoint competitor2,
                                  ScorePoint score1, ScorePoint score2,
                                  TournamentPoint tournamentNameRound)
            : base(filePath)
        {
            if(competitor1 == null)
               throw new Exception("Competitor Template: Competitor 1 must have a point on template");
            if(competitor2 == null)
                throw new Exception("Competitor Template: Competitor 2 must have a point on template");
            if (score1 == null)
                throw new Exception("Competitor Template: Score 1 must have a point on template");
            if (score2 == null)
                throw new Exception("Competitor Template: Score 2 must have a point on template");
            if (tournamentNameRound == null)
                throw new Exception("Competitor Template: Tournament Name must have a point on template");
          
            this.competitor1Point = competitor1;
            this.competitor2Point = competitor2;
            this.score1Point = score1;
            this.score2Point = score2;
            this.tournamentNameRoundPoint = tournamentNameRound;
        }

        public CompetitorTemplate(string filePath,
                                 CompetitorPoint competitor1,
                                 CompetitorPoint competitor2,
                                 CompetitorPoint competitor1Webcam,
                                 CompetitorPoint competitor2Webcam,
                                 ScorePoint score1, ScorePoint score2,
                                 TournamentPoint tournamentNameRound)
            : base(filePath)
        {
            if (competitor1 == null)
                throw new Exception("Competitor Template: Competitor 1 must have a point on template");
            if (competitor2 == null)
                throw new Exception("Competitor Template: Competitor 2 must have a point on template");
            if (competitor1Webcam == null)
                throw new Exception("Competitor Template: Competitor 1's Webcam must have a point on template");
            if (competitor2Webcam == null)
                throw new Exception("Competitor Template: Competitor 2's Webcam must have a point on template");
            if (score1 == null)
                throw new Exception("Competitor Template: Score 1 must have a point on template");
            if (score2 == null)
                throw new Exception("Competitor Template: Score 2 must have a point on template");
            if (tournamentNameRound == null)
                throw new Exception("Competitor Template: Tournament Name must have a point on template");
          
            this.competitor1Point = competitor1;
            this.competitor2Point = competitor2;
            this.competitor1WebcamPoint = competitor1Webcam;
            this.competitor2WebcamPoint = competitor2Webcam;
            this.score1Point = score1;
            this.score2Point = score2;
            this.tournamentNameRoundPoint = tournamentNameRound;
        }
        #endregion CONSTRUCTORS

        #region GETTERS AND SETTERS
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

        public CompetitorPoint Competitor1WebcamPoint
        {
            get { return this.competitor1WebcamPoint; }
            set { this.competitor1WebcamPoint = value; }
        }

        public CompetitorPoint Competitor2WebcamPoint
        {
            get { return this.competitor2WebcamPoint; }
            set { this.competitor2WebcamPoint = value; }
        }

        public ScorePoint Score1Point
        {
            get { return this.score1Point; }
            set { this.score1Point = value; }
        }

        public ScorePoint Score2Point
        {
            get { return this.score2Point; }
            set { this.score2Point = value; }
        }

        public TournamentPoint TournamentNameRoundPoint
        {
            get { return this.tournamentNameRoundPoint; }
            set { this.tournamentNameRoundPoint = value; }
        }

        public Font Name1Font
        {
            get { return this.name1Font; }
            set { this.name1Font = value; }
        }

        public Font Name2Font
        {
            get { return this.name2Font; }
            set { this.name2Font = value; } 
        }

        public Font ScoreFont
        {
            get { return this.scoreFont; }
            set { this.scoreFont = value; }
        }

        public Font TournamentFont
        {
            get { return this.tournmentFont; }
            set { this.tournmentFont = value; }
        }
        #endregion GETTERS AND SETTERS
        
        #region ABSTRACTED METHODS
        public override Bitmap drawTextOnImage(SmashOverlayGenerator form) { return null; }

        public Bitmap drawTextOnImage(SmashOverlayGenerator form, bool swap)
        {
            setFontSizes(form.ResourceType, form.Competitor1, form.Competitor2);

            //CENTER NAME TEXT
            StringFormat nameFormat = new StringFormat();
            StringFormat scoreFormat = new StringFormat();

            nameFormat.LineAlignment = StringAlignment.Center;
            nameFormat.Alignment = StringAlignment.Center;
            scoreFormat.Alignment = StringAlignment.Center;

            Bitmap image = base.getImage();
            Graphics g = Graphics.FromImage(image);

            if (!swap)
            {
                g.DrawString(form.Competitor1, Name1Font, Brushes.White, Competitor1Point.getPoint(), nameFormat);
                g.DrawString(form.Competitor2, Name2Font, Brushes.White, Competitor2Point.getPoint(), nameFormat);
                g.DrawString(form.Score1, ScoreFont, Brushes.White, Score1Point.getPoint(), scoreFormat);
                g.DrawString(form.Score2, ScoreFont, Brushes.White, Score2Point.getPoint(), scoreFormat);
            }
            else
            {
                g.DrawString(form.Competitor1, Name1Font, Brushes.White, Competitor2Point.getPoint(), nameFormat);
                g.DrawString(form.Competitor2, Name2Font, Brushes.White, Competitor1Point.getPoint(), nameFormat);
                g.DrawString(form.Score1, ScoreFont, Brushes.White, Score2Point.getPoint(), scoreFormat);
                g.DrawString(form.Score2, ScoreFont, Brushes.White, Score1Point.getPoint(), scoreFormat);
            }      
            g.DrawString(form.Competitor1, Name1Font, Brushes.White, Competitor1WebcamPoint.getPoint(), nameFormat);
            g.DrawString(form.Competitor2, Name2Font, Brushes.White, Competitor2WebcamPoint.getPoint(), nameFormat);            
            g.DrawString(form.TournamentName + " | " + form.TournamentRound, 
                TournamentFont, Brushes.White, TournamentNameRoundPoint.getPoint(), nameFormat);

            return image;
        }

        public override void saveImage(Bitmap image)
        {
            string targetPath = base.ParentPath + "\\Competitor";

            String fileName = base.FilePath;
            fileName = fileName.Substring(fileName.LastIndexOf(".Templates.") + 11);
            fileName = fileName.Substring(0, fileName.Length - 4) + "_official.png";
            
            if(!Directory.Exists(base.ParentPath)){
                Directory.CreateDirectory(base.ParentPath);
            } 
            if(!Directory.Exists(targetPath)){
                Directory.CreateDirectory(targetPath);
            }

            image.Save(targetPath + "\\" + fileName);
        }
        #endregion ABSTRACTED METHODS

        public void setFontSizes(string resourceType,
                                 string competitor1, string competitor2)
        {
            int[] fontSize = GenFcns.determineFontSize(competitor1, competitor2);
            if (resourceType.Equals("file"))
            {
                Name1Font = new Font(FontFamily.GenericSansSerif, fontSize[0], FontStyle.Bold);
                Name2Font = new Font(FontFamily.GenericSansSerif, fontSize[1], FontStyle.Bold);
                ScoreFont = new Font(FontFamily.GenericSansSerif, 45, FontStyle.Bold);
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
            }
            else
            {
                Name1Font = new Font(FontFamily.GenericSansSerif, fontSize[0] + 8, FontStyle.Bold);
                Name2Font = new Font(FontFamily.GenericSansSerif, fontSize[1] + 8, FontStyle.Bold);
                ScoreFont = new Font(FontFamily.GenericSansSerif, 35, FontStyle.Bold);
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
            }
        }
    }
}
