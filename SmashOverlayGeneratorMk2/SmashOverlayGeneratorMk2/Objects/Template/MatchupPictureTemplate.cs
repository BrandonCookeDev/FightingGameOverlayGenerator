using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
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
                                      CompetitorPoint competitor1Point, CharacterPoint character1Point,
                                      CompetitorPoint competitor2Point, CharacterPoint character2Point)
            : base(filePath)
        {
            this.tournamentPoint = tournamentPoint;
            this.tournamentRoundPoint = tournamentRoundPoint;
            this.competitor1Point = competitor1Point;
            this.competitor2Point = competitor2Point;
            this.character1Point = character1Point;
            this.character2Point = character2Point;
        }
                                      
        /*
        public MatchupPictureTemplate(string filePath, string gametype, string tournamentName,
                                      string competitor1, string character1, 
                                      string competitor2, string character2) : base(filePath)
        {
            this.gametype = "singles";
            
            if(tournamentName == null || tournamentName.Equals(""))
                throw new Exception("Matchup Picture: Tournament Name cannot be empty");
            if(competitor1 == null || competitor1.Equals(""))
                throw new Exception("Matchup Picture: Competitor 1 Name cannot be null");
            if(competitor2 == null || competitor2.Equals(""))
                throw new Exception("Matchup Picture: Competitor 2 Name cannot be null");
            if(character1 == null || character1.Equals(""))
                throw new Exception("Matchup Picture: Character 1 Name cannot be null");
            if(character2 == null || character2.Equals(""))
                throw new Exception("Matchup Picture: Character 2 Name cannot be null");

            this.tournamentName = tournamentName;
            this.competitor1 = competitor1;
            this.competitor2 = competitor2;
            this.character1 = character1;
            this.character2 = character2;
        }

        public MatchupPictureTemplate(string filePath, string gametype, string tournamentName,
                                      string competitor1, string character1,
                                      string competitor2, string character2,
                                      string competitor3, string character3, 
                                      string competitor4, string character4) : base(filePath)
        {
            this.gametype = "doubles";
            
            if(tournamentName == null || tournamentName.Equals(""))
                throw new Exception("Matchup Picture: Tournament Name cannot be empty");
            if(competitor1 == null || competitor1.Equals(""))
                throw new Exception("Matchup Picture: Competitor 1 Name cannot be null");
            if(competitor2 == null || competitor2.Equals(""))
                throw new Exception("Matchup Picture: Competitor 2 Name cannot be null");
            if(character1 == null || character1.Equals(""))
                throw new Exception("Matchup Picture: Character 1 Name cannot be null");
            if(character2 == null || character2.Equals(""))
                throw new Exception("Matchup Picture: Character 2 Name cannot be null");
            if(competitor3 == null || competitor3.Equals(""))
                throw new Exception("Matchup Picture: Competitor 3 Name cannot be null");
            if(competitor4 == null || competitor4.Equals(""))
                throw new Exception("Matchup Picture: Competitor 4 Name cannot be null");
            if(character3 == null || character3.Equals(""))
                throw new Exception("Matchup Picture: Character 3 Name cannot be null");
            if(character4 == null || character4.Equals(""))
                throw new Exception("Matchup Picture: Character 4 Name cannot be null");
            
            this.tournamentName = tournamentName;
            this.competitor1 = competitor1;
            this.competitor2 = competitor2;
            this.competitor3 = competitor3;
            this.competitor4 = competitor4;
            this.character1 = character1;
            this.character2 = character2;
            this.character3 = character3;
            this.character4 = character4;
        }
         * */
        #endregion CONSTRUCTORS

        #region ABSTRACTED METHODS
        public override System.Drawing.Bitmap drawTextOnImage(SmashOverlayGenerator form)
        {
            setFontSizes(form.ResourceType, Competitor1, Competitor2);
            if (Gametype.Equals("doubles"))
                setFontSizes(form.ResourceType, Competitor3, Competitor4);

            StringFormat nameFormat = new StringFormat();
            StringFormat tournamentFormat = new StringFormat();

            Bitmap image = base.getImage();
            Graphics g = Graphics.FromImage(image);

            g.DrawString(form.MatchupCompetitor1, Competitor1Font, Brushes.White, Competitor1Point.getPoint(), nameFormat);
            g.DrawString(form.MatchupCompetitor2, Competitor2Font, Brushes.White, Competitor2Point.getPoint(), nameFormat);
            g.DrawString(form.TournamentName, TournamentFont, Brushes.White, TournamentPoint.getPoint(), tournamentFormat);
            g.DrawString(form.TournamentRound, TournamentRoundFont, Brushes.White, TournamentRoundPoint.getPoint(), tournamentFormat);

            return null;
            //throw new NotImplementedException();
        }

        public override void saveImage(System.Drawing.Bitmap image)
        {
            throw new NotImplementedException();
        }
        #endregion ABSTRACTED METHODS

        public void drawCharactersOnImage(Assembly myAssembly, string character1, string character2)
        {
            
        }

        public void setFontSizes(string resourceType,
                                 string competitor1, string competitor2)
        {
            int[] fontSize = GenFcns.determineFontSize(Competitor1, Competitor2);
            if (resourceType.Equals("file"))
            {
                Competitor1Font = new Font(FontFamily.GenericSansSerif, fontSize[0], FontStyle.Bold);
                Competitor2Font = new Font(FontFamily.GenericSansSerif, fontSize[1], FontStyle.Bold);
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
            }
            else
            {
                Competitor1Font = new Font(FontFamily.GenericSansSerif, fontSize[0] + 8, FontStyle.Bold);
                Competitor2Font = new Font(FontFamily.GenericSansSerif, fontSize[1] + 8, FontStyle.Bold);                
                TournamentFont = new Font(FontFamily.GenericSansSerif, 27, FontStyle.Bold);
            }

            if (Gametype.Equals("doubles"))
            {
                fontSize = GenFcns.determineFontSize(Competitor3, Competitor4);
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
        }
    }
}
