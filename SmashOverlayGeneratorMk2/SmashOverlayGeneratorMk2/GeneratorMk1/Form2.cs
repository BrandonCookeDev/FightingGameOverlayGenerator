using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace SmashOverlayProgram
{
    public partial class SmashOverlayGenerator : Form
    {
        /* GLOBAL VARIABLES */
        private string templateFile;
        private string newTemplateFile;
        private string competitor1;
        private string competitor2;
        private string score1;
        private string score2;

        /* GETTERS AND SETTERS */
        public string TemplateFile
        {
            get{ return this.templateFile; }
            set{ this.templateFile = value; }
        }

        public string NewTemplateFile
        {
            get { return this.newTemplateFile; }
            set { this.newTemplateFile = value; }
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

        public string Score1
        {
            get { return this.score1; }
            set { this.score1 = value; }
        }

        public string Score2
        {
            get { return this.score2; }
            set { this.score2 = value; }
        }

        /* CONSTRUCTOR */
        public SmashOverlayGenerator()
        {
            InitializeComponent();
        }

        /* METHODS */
        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                //DETERMINE WHETHER SINGLES OR DOUBLES
                string gameType = singlesOrDoubles();
                //GET THE COMBATANTS' NAMES (SET TO THE GLOBAL VARIABLES)
                getCombatants(gameType);
                //VERIFY TOURNAMENT AND ROUND ARE FILLED IN
                verifyTourneyDataFilled();
                //VERIFY THE SCORES ARE IN CORRECT FORMAT
                checkScoreFormat();
                
                //ALTER THE IMAGE WITH NEW INFORMATION
                paintText(TemplateFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void paintText(string filePath)
        {
            //DEFINE THE IMAGE TO BE ALTERED
            Image template = Image.FromFile(@filePath);
            //DEFINE THE BITS MAPPING OF THE IMAGE
            Bitmap bMap = new Bitmap(template);
            //DEFINE THE GENERIC GRAPHICS OBJECT FOR THE IMAGE
            Graphics g = Graphics.FromImage(bMap);
            //DEFINE THE FONT TO USE
            Font nameFont = new Font(FontFamily.GenericSansSerif, 17, FontStyle.Bold);
            Font scoreFont = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            Font tourneyFont = new Font(FontFamily.GenericSansSerif, 23, FontStyle.Bold);

            //CENTER NAME TEXT
            StringFormat nameFormat = new StringFormat();
            nameFormat.LineAlignment = StringAlignment.Center;
            nameFormat.Alignment = StringAlignment.Center;

            /* DEFINE LOCATIONS ON TEMPLATE */
            //PLAYER 1: 416, 1050 or 501, 1050 and 1521, 351 or 1686, 351
            //Point p1 = new Point(416, 1050);
            Point p1 = new Point(501,1060);
            //Point p1Cam = new Point(1521, 351);
            Point p1Cam = new Point(1686, 351);
            
            
            //PLAYER 2: 1075, 1050 or 976, 1050 and 1521, 717 or 1686, 717
            //Point p2 = new Point(1075, 1050);
            Point p2 = new Point(976, 1060);
            //Point p2Cam = new Point(1521, 717);
            Point p2Cam = new Point(1686, 717);
            
            //PLAYER 1 SCORE: 702, 1050
            Point p1Score = new Point(665, 1030);
            
            //PLAYER 2 SCORE: 775, 1050
            Point p2Score = new Point(775, 1030);
            
            //TOURNAMENT NAME AND ROUND: 579, 20 or 900, 20
            //Point tourneyData = new Point(579, 20);
            Point pTourneyData = new Point(900, 25);
            
            /* DRAW THE TEXT */
            //PLAYER 1
            g.DrawString(Competitor1, nameFont, Brushes.White, p1, nameFormat);
            g.DrawString(Competitor1, nameFont, Brushes.White, p1Cam, nameFormat);
            //PLAYER 2
            g.DrawString(Competitor2, nameFont, Brushes.White, p2, nameFormat);
            g.DrawString(Competitor2, nameFont, Brushes.White, p2Cam, nameFormat);
            //PLAYER 1 SCORE
            g.DrawString(Score1, scoreFont, Brushes.White, p1Score);
            //PLAYER 2 SCORE
            g.DrawString(Score2, scoreFont, Brushes.White, p2Score);
            //TOURNAMENT NAME AND ROUND
            g.DrawString(tourneyNameTextbox.Text + " | " + tourneyRoundTextbox.Text,
                            tourneyFont, Brushes.White, pTourneyData, nameFormat);
            
            //RELOAD THE NEW TEMPLATE
            String fileName =  NewTemplateFile.Substring(0, NewTemplateFile.Length-4) + "_official.png";
            bMap.Save(fileName);
            template.Dispose();
        }

        /**
         * Will return either "singles" or "doubles" 
         */
        private string singlesOrDoubles()
        {
            if (singlesP1Textbox.Text.Equals("") &&
                singlesP2Textbox.Text.Equals("") &&
                p1ScoreTextbox.Text.Equals("") &&
                p2ScoreTextbox.Text.Equals("") &&
                !doublesT1P1Textbox.Text.Equals("") &&
                !doublesT1P2Textbox.Text.Equals("") &&
                !doublesT2P1Textbox.Text.Equals("") &&
                !doublesT2P2Textbox.Text.Equals("") &&
                !t1ScoreTextbox.Text.Equals("") &&
                !t2ScoreTextbox.Text.Equals(""))
               return "doubles";
            else if (doublesT1P1Textbox.Text.Equals("") &&
                    doublesT1P2Textbox.Text.Equals("") &&
                    doublesT2P1Textbox.Text.Equals("") &&
                    doublesT2P2Textbox.Text.Equals("") &&
                    t1ScoreTextbox.Text.Equals("") &&
                    t2ScoreTextbox.Text.Equals("") &&
                    !singlesP1Textbox.Text.Equals("") &&
                    !singlesP2Textbox.Text.Equals("") &&
                    !p1ScoreTextbox.Text.Equals("") &&
                    !p2ScoreTextbox.Text.Equals(""))
               return "singles";
            else
                throw new Exception("An Error Occured" +
                    "\r\nIf Sinlges, only Singles section can contain text and all fields must be filled." +
                    "\r\nIf Doubles, only Doubles section can contain text and all fields must be filled. \r\n" +
                    "\r\nPlease correct any errors and try again. \r\nIf problem persists, contact cookiE.");                
        }

        private void getCombatants(String gameType)
        {
            if(gameType.Equals("singles"))
            {
                Competitor1 = singlesP1Textbox.Text;
                Competitor2 = singlesP2Textbox.Text;
                Score1 = p1ScoreTextbox.Text;
                Score2 = p2ScoreTextbox.Text;
            }
            else if(gameType.Equals("doubles"))
            {                
                string team1 = doublesT1P1Textbox.Text + " & " + doublesT1P2Textbox.Text;
                string team2 = doublesT2P1Textbox.Text + " & " + doublesT2P2Textbox.Text;

                Competitor1 = team1;
                Competitor2 = team2;
                Score1 = t1ScoreTextbox.Text;
                Score2 = t2ScoreTextbox.Text;
            }
            else
                throw new Exception("An Error Occured"+
                    "\r\ngetCombatants received an invalid parameter"+
                    "\r\nParam can only be \"singles\" or \"doubles\"" + 
                    "\r\nPlease contact cookiE");
        }

        private void verifyTourneyDataFilled()
        {
            if(tourneyNameTextbox.Text.Equals("") || 
                tourneyRoundTextbox.Text.Equals("") ||
                selectedFileLabel.Text.Equals(""))                            
                throw new Exception("An Error Occured" +
                    "\r\nPart C (Tournament Data) not completely filled in or template file not slected." +
                    "\r\nPlease correct fields and try again");
            
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);

                //Show the file name in the program window
                selectedFileLabel.Text = File.Name;
                
                //DEFINE GLOBAL VARIABLE FOR TEMPLATE
                TemplateFile = @File.FullName;
                //DEFINE THE NEW PATH
                string newName = File.DirectoryName + "\\" + tourneyNameTextbox.Text + File.Name;
                //DEFINE THE GLOBAL VARIBLE FOR NEW TEMPLATE
                NewTemplateFile = @newName;                           
            }
        }

        private void checkScoreFormat()
        {
            //CHECK SCORE ONE
            if (!checkIfDigits(Score1) || !checkIfDigits(Score2))
                throw new Exception("A score is formatted incorrectly" +
                    "\r\nPlease make sure that all scores are only numeric");
            else if (Int32.Parse(Score1) < 0 || Int32.Parse(Score1) > 100
                     || Int32.Parse(Score2) < 0 || Int32.Parse(Score2) > 100)
                throw new Exception("A score is not in the acceptable range" +
                    "\r\nPlease make sure that all scores are between 0 and 100");
        }

        private bool checkIfDigits(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void createNewImageFile(string source, string destination)
        {
            //COPY IMAGE TO NEW FILE
            System.IO.File.Copy(source, destination, true);
        }

        private void singlesClearButton_Click(object sender, EventArgs e)
        {
            singlesP1Textbox.Text = "";
            singlesP2Textbox.Text = "";
            p1ScoreTextbox.Text = "";
            p2ScoreTextbox.Text = "";
        }

        private void doublesClearButton_Click(object sender, EventArgs e)
        {
            doublesT1P1Textbox.Text = "";
            doublesT1P2Textbox.Text = "";
            doublesT2P1Textbox.Text = "";
            doublesT2P2Textbox.Text = "";
            t1ScoreTextbox.Text = "";
            t2ScoreTextbox.Text = "";
        }

        private void tourneyClearButton_Click(object sender, EventArgs e)
        {
            tourneyNameTextbox.Text = "";
            tourneyRoundTextbox.Text = "";
            selectedFileLabel.Text = "";
            TemplateFile = "";
            NewTemplateFile = "";
        }
    }
}
