using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Reflection;
using SmashOverlayGeneratorMk2.Objects.Points;
using SmashOverlayGeneratorMk2.Objects;
using XSplit.Core.Properties;
using XSplitBroadcasterLib;

namespace SmashOverlayGeneratorMk2
{
    public partial class SmashOverlayGenerator : Form
    {
        /* GLOBAL VARIABLES */
        #region Data Fields
        private string templateFile;
        private string newTemplateFile;
        private string templateFileName;
        private string competitor1;
        private string competitor2;
        private string score1;
        private string score2;
        private string gameType;
        private string caster1;
        private string caster2;
        private string caster1Twitter;
        private string caster2Twitter;
        private string tournamentName;
        private string tournamentRound;
        private string casterTemplateFile;
        private string casterTemplateFileName;
        private string resourceType;
        private bool isError;
        private bool isAutoUpdate;
        private ArrayList templates;
        private ArrayList casterTemplates;
        private Resources _resources = new Resources();
        
        

        Assembly myAssembly = Assembly.GetExecutingAssembly();  
        #endregion Data Fields

        /* GETTERS AND SETTERS */
        #region Getters and Setters
        public bool IsError
        {
            get { return this.isError; }
            set { this.isError = value; }
        }

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

        public string Caster1
        {
            get { return this.caster1; }
            set { this.caster1 = value; } 
        }

        public string Caster2
        {
            get { return this.caster2; }
            set { this.caster2 = value; }
        }

        public string Caster1Twitter
        {
            get { return this.caster1Twitter; }
            set { this.caster1Twitter = value; }
        }

        public string Caster2Twitter
        {
            get { return this.caster2Twitter; }
            set { this.caster2Twitter = value; }             
        }

        public string CasterTemplateFile
        {
            get { return this.casterTemplateFile; }
            set { this.casterTemplateFile = value; }
        }

        public string CasterTemplateFileName
        {
            get { return this.casterTemplateFileName; }
            set { this.casterTemplateFileName = value; }
        }

        public string TournamentName
        {
            get { return this.tournamentName; }
            set { this.tournamentName = value; }
        }

        public string TournamentRound
        {
            get { return this.tournamentRound; }
            set { this.tournamentRound = value; }
        }

        public string GameType
        {
            get { return this.gameType; }
            set { this.gameType = value; }
        }

        public string TemplateFileName
        {
            get { return this.templateFileName; }
            set { this.templateFileName = value; } 
        }

        public string ResourceType
        {
            get { return this.resourceType; }
            set { this.resourceType = value; }
        }

        public bool IsAutoUpdate
        {
            get { return this.isAutoUpdate; }
            set { this.isAutoUpdate = value;}
        }

        public ArrayList Templates
        {
            get { return this.templates; }
            set { this.templates = value; }
        }

        public ArrayList CasterTemplates
        {
            get { return this.casterTemplates; }
            set { this.casterTemplates = value; }
        }

        /*
        public Resources resources
        {
            get { return this._resources; }
            set { this._resources = value; }
        }
         */
        #endregion Getters and Setters

        /* CONSTRUCTOR */
        #region Constructors
        public SmashOverlayGenerator()
        {
            InitializeComponent();
        }
        #endregion Constructors

        /* METHODS */
        
        #region GeneratePicture
        private void paintCompetitorText(string filePath, string resourceType)
        {
            
            Image template = null;
            Bitmap bMap = null;
            Stream imageStream = null;

            if(resourceType.Equals("file"))
            {
                //DEFINE THE IMAGE TO BE ALTERED
                template = Image.FromFile(@filePath);
                //DEFINE THE BITS MAPPING OF THE IMAGE
                bMap = new Bitmap(template);
            }
            else if (resourceType.Equals("resource"))
            {
                myAssembly = Assembly.GetExecutingAssembly();
                imageStream = myAssembly.GetManifestResourceStream(filePath);
                bMap = new Bitmap(imageStream);
            }

            ResourceType = resourceType;

            /*
            //DEFINE THE GENERIC GRAPHICS OBJECT FOR THE IMAGE
            Graphics g = Graphics.FromImage(bMap);

            //DETERMINE FONT SIZE
            int[] fontSize = determineFontSize();

            Font name1Font = null;
            Font name2Font = null;
            Font scoreFont = null;
            Font tourneyFont = null;

            //DEFINE THE FONT TO USE
            if (resourceType.Equals("file"))
            {
                name1Font = new Font(FontFamily.GenericSansSerif, fontSize[0], FontStyle.Bold);
                name2Font = new Font(FontFamily.GenericSansSerif, fontSize[1], FontStyle.Bold);
                scoreFont = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                tourneyFont = new Font(FontFamily.GenericSansSerif, 23, FontStyle.Bold);
            }
            else
            {
                name1Font = new Font(FontFamily.GenericSansSerif, fontSize[0] + 8, FontStyle.Bold);
                name2Font = new Font(FontFamily.GenericSansSerif, fontSize[1] + 8, FontStyle.Bold);
                scoreFont = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
                tourneyFont = new Font(FontFamily.GenericSansSerif, 23, FontStyle.Bold);
            }

            //CENTER NAME TEXT
            StringFormat nameFormat = new StringFormat();
            nameFormat.LineAlignment = StringAlignment.Center;
            nameFormat.Alignment = StringAlignment.Center;

            /* DEFINE LOCATIONS ON TEMPLATE 
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
            
            /* DRAW THE TEXT 
            //PLAYER 1
            g.DrawString(Competitor1, name1Font, Brushes.White, p1, nameFormat);
            g.DrawString(Competitor1, name1Font, Brushes.White, p1Cam, nameFormat);
            //PLAYER 2
            g.DrawString(Competitor2, name2Font, Brushes.White, p2, nameFormat);
            g.DrawString(Competitor2, name2Font, Brushes.White, p2Cam, nameFormat);
            //PLAYER 1 SCORE
            g.DrawString(Score1, scoreFont, Brushes.White, p1Score);
            //PLAYER 2 SCORE
            g.DrawString(Score2, scoreFont, Brushes.White, p2Score);
            //TOURNAMENT NAME AND ROUND
            g.DrawString(tournamentNameTextbox.Text + " | " + tourneyRoundTextbox.Text,
                            tourneyFont, Brushes.White, pTourneyData, nameFormat);

            String fileName;
            //RELOAD THE NEW TEMPLATE
            if (resourceType.Equals("file"))
            {
                fileName = NewTemplateFile.Substring(0, NewTemplateFile.Length - 4) + "_official.png";
                template.Dispose();
            }
            else
            {
                fileName = TemplateFileName.Substring(0, TemplateFileName.Length - 4) + tournamentNameTextbox.Text + "_official.png";
                imageStream.Close();
            }
            bMap.Save(fileName);
             */
            debugCompetitorTemplate(resourceType, filePath);
        }

        private void paintCasterText(string filePath)
        {
            Bitmap bMap = null;
            Stream imageStream = null;

            myAssembly = Assembly.GetExecutingAssembly();
            imageStream = myAssembly.GetManifestResourceStream(filePath);
            bMap = new Bitmap(imageStream);

            /*
            //DEFINE THE GENERIC GRAPHICS OBJECT FOR THE IMAGE
            Graphics g = Graphics.FromImage(bMap);

            //DETERMINE FONT SIZE
            //int[] fontSize = determineFontSize();

            //DEFINE THE FONT TO USE
            Font name1Font = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            Font name2Font = new Font(FontFamily.GenericSansSerif, 60, FontStyle.Bold);
            Font twitterFont = new Font(FontFamily.GenericSansSerif, 40, FontStyle.Bold);

            //CENTER NAME TEXT
            StringFormat nameFormat = new StringFormat();
            nameFormat.LineAlignment = StringAlignment.Center;
            nameFormat.Alignment = StringAlignment.Center;

            /* DEFINE LOCATIONS ON TEMPLATE */
            /*
            //PLAYER 1: 416, 1050 or 501, 1050 and 1521, 351 or 1686, 351
            Point p1 = new Point(450,890);
            
            
            //PLAYER 2: 1075, 1050 or 976, 1050 and 1521, 717 or 1686, 717
            Point p2 = new Point(1320, 890);
            
            //PLAYER 1 SCORE: 702, 1050
            Point twitter1Point = new Point(430, 910);
            
            //PLAYER 2 SCORE: 775, 1050
            Point twitter2Point = new Point(1300, 910);
                        
            /* DRAW THE TEXT */
            /*
            //PLAYER 1
            g.DrawString(Caster1, name1Font, Brushes.White, p1, nameFormat);
            //PLAYER 2
            g.DrawString(Caster2, name2Font, Brushes.White, p2, nameFormat);
            //PLAYER 1 SCORE
            g.DrawString(Caster1Twitter, twitterFont, Brushes.White, twitter1Point);
            //PLAYER 2 SCORE
            g.DrawString(Caster2Twitter, twitterFont, Brushes.White, twitter2Point);
            
            String fileName;
            //RELOAD THE NEW TEMPLATE
            fileName = CasterTemplateFileName.Substring(0, CasterTemplateFileName.Length - 4) + "_official.png";
            imageStream.Close();
            bMap.Save(fileName);        
            */
            debugCasterTemplate(filePath);
        }

        private int[] determineFontSize()
        {
            int[] fontSizeArr = new int[2];
            int MAX = 20, MEDIUM = 17, SMALL = 15, SMALL_PLUS = 13;

            if (Competitor1.Length >= 23) fontSizeArr[0] = SMALL_PLUS;
            else if (Competitor1.Length >= 20) fontSizeArr[0] = SMALL;
            else if (Competitor1.Length >= 17) fontSizeArr[0] = MEDIUM;
            else fontSizeArr[0] = MAX;

            if (Competitor2.Length >= 23) fontSizeArr[1] = SMALL_PLUS;
            else if (Competitor2.Length >= 20) fontSizeArr[1] = SMALL;
            else if (Competitor2.Length >= 17) fontSizeArr[1] = MEDIUM;
            else fontSizeArr[1] = MAX;

            return fontSizeArr;
        }

        private void getCombatants(String gameType)
        {
            if (gameType.Equals("singles"))
            {
                Competitor1 = singlesP1Textbox.Text;
                Competitor2 = singlesP2Textbox.Text;
                Score1 = singlesP1ScoreTextbox.Text;
                Score2 = singlesP2ScoreTextbox.Text;
            }
            else if (gameType.Equals("doubles"))
            {
                string team1 = doublesT1P1Textbox.Text + " & " + doublesT1P2Textbox.Text;
                string team2 = doublesT2P1Textbox.Text + " & " + doublesT2P2Textbox.Text;

                Competitor1 = team1;
                Competitor2 = team2;
                Score1 = doublesT1ScoreTextbox.Text;
                Score2 = doublesT2ScoreTextbox.Text;
            }
            else
                throw new Exception("An Error Occured" +
                    "\r\ngetCombatants received an invalid parameter" +
                    "\r\nParam can only be \"singles\" or \"doubles\"" +
                    "\r\nPlease contact cookiE");
        }

        private void getCasters()
        {
            Caster1 = caster1Textbox.Text;
            Caster2 = caster2Textbox.Text;
            Caster1Twitter = caster1TwitterTextbox.Text;
            Caster2Twitter = caster2TwitterTextbox.Text;
        }

        private void getTournamentData()
        {
            TournamentName = tournamentNameTextbox.Text;
            TournamentRound = tournamentRoundTextbox.Text;
        }

        private void createNewImageFile(string source, string destination)
        {
            //COPY IMAGE TO NEW FILE
            System.IO.File.Copy(source, destination, true);
        }
        #endregion GeneratePicture

        #region FormOperations
        private void SmashOverlayGeneratorMk2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;

            greyOutDoubles(true);
            greyOutSingles(true);
            greyOutCaster(1, true);
            greyOutCaster(2, true);

            singlesP1ScoreTextbox.Text = 0.ToString();
            singlesP2ScoreTextbox.Text = 0.ToString();
            doublesT1ScoreTextbox.Text = 0.ToString();
            doublesT2ScoreTextbox.Text = 0.ToString();

            populateTemplatesList();
            populateCasterTemplatesList();
        }

        public void populateTemplatesList()
        {
            ArrayList temp = new ArrayList();                      
            string[] resources = myAssembly.GetManifestResourceNames();

            foreach(string resource in resources)
            {
                if(resource.Contains(".Templates"))
                {
                    string resourceName = resource.Substring(resource.LastIndexOf("Templates.")+10);
                    temp.Add(resource);
                    templateListView.Items.Add(resourceName);
                }
            }
            Templates = temp;            
        }

        public void populateCasterTemplatesList()
        {
            ArrayList temp = new ArrayList();
            string[] resources = myAssembly.GetManifestResourceNames();

            foreach (string resource in resources)
            {
                if (resource.Contains(".Caster"))
                {
                    string resourceName = resource.Substring(resource.LastIndexOf(".Caster.") + 8);
                    temp.Add(resource);
                    casterTemplateListView.Items.Add(resourceName);
                }
            }
            CasterTemplates = temp;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void clearSinglesFields()
        {
            singlesP1Textbox.Text = "";
            singlesP2Textbox.Text = "";
            singlesP1ScoreTextbox.Text = "0";
            singlesP2ScoreTextbox.Text = "0";
        }

        private void clearDoublesFields()
        {
            doublesT1P1Textbox.Text = "";
            doublesT1P2Textbox.Text = "";
            doublesT2P1Textbox.Text = "";
            doublesT2P2Textbox.Text = "";
            doublesT1ScoreTextbox.Text = "0";
            doublesT2ScoreTextbox.Text = "0";
        }

        private void clearTourneyFields()
        {
            tournamentNameTextbox.Text = "";
            //tourneyRoundTextbox.Text = "";
            selectedFileLabel.Text = "";
            TemplateFile = "";
            NewTemplateFile = "";
            templateListView.ClearSelected();
            templatePictureBox.Image = null;
        }

        public void greyOutDoubles(Boolean grey)
        {
            if (grey == true)
            {
                doublesT1P1Textbox.Enabled = false;
                doublesT1P2Textbox.Enabled = false;
                doublesT2P1Textbox.Enabled = false;
                doublesT2P2Textbox.Enabled = false;

                doublesT2ScoreTextbox.Enabled = false;
                doublesT1ScoreTextbox.Enabled = false;

                doublesClearButton.Enabled = false;

                doublesT1P1Label.Enabled = false;
                doublesT1P2Label.Enabled = false;
                doublesT2P1Label.Enabled = false;
                doublesT2P2Label.Enabled = false;
                teamOneLabel.Enabled = false;
                teamTwoLabel.Enabled = false;
                doublesT1ScoreLabel.Enabled = false;
                doublesT2ScoreLabel.Enabled = false;

                incrementTeam1Button.Enabled = false;
                decrementTeam1Button.Enabled = false;
                incrementTeam2Button.Enabled = false;
                decrementTeam2Button.Enabled = false;
            }
            else
            {
                doublesT1P1Textbox.Enabled = true;
                doublesT1P2Textbox.Enabled = true;
                doublesT2P1Textbox.Enabled = true;
                doublesT2P2Textbox.Enabled = true;

                doublesT2ScoreTextbox.Enabled = true;
                doublesT1ScoreTextbox.Enabled = true;

                doublesClearButton.Enabled = true;

                doublesT1P1Label.Enabled = true;
                doublesT1P2Label.Enabled = true;
                doublesT2P1Label.Enabled = true;
                doublesT2P2Label.Enabled = true;
                teamOneLabel.Enabled = true;
                teamTwoLabel.Enabled = true;
                doublesT1ScoreLabel.Enabled = true;
                doublesT2ScoreLabel.Enabled = true;

                incrementTeam1Button.Enabled = true;
                decrementTeam1Button.Enabled = true;
                incrementTeam2Button.Enabled = true;
                decrementTeam2Button.Enabled = true;
            }
        }

        public void greyOutSingles(Boolean grey)
        {
            if (grey == true)
            {
                singlesP1ScoreTextbox.Enabled = false;
                singlesP2ScoreTextbox.Enabled = false;

                singlesP1Textbox.Enabled = false;
                singlesP2Textbox.Enabled = false;

                singlesClearButton.Enabled = false;

                singlesP1Label.Enabled = false;
                singlesP2Label.Enabled = false;
                singlesP1ScoreLabel.Enabled = false;
                singlesP2ScoreLabel.Enabled = false;

                incrementPlayer1Button.Enabled = false;
                decrementPlayer1Button.Enabled = false;
                incrementPlayer2Button.Enabled = false;
                decrementPlayer2Button.Enabled = false;
            }
            else
            {
                singlesP1ScoreTextbox.Enabled = true;
                singlesP2ScoreTextbox.Enabled = true;

                singlesP1Textbox.Enabled = true;
                singlesP2Textbox.Enabled = true;

                singlesClearButton.Enabled = true;

                singlesP1Label.Enabled = true;
                singlesP2Label.Enabled = true;
                singlesP1ScoreLabel.Enabled = true;
                singlesP2ScoreLabel.Enabled = true;

                incrementPlayer1Button.Enabled = true;
                decrementPlayer1Button.Enabled = true;
                incrementPlayer2Button.Enabled = true;
                decrementPlayer2Button.Enabled = true;
            }
        }

        public void greyOutCaster(int num, bool grey)
        {
            if (num == 1)
            {
                if (grey)
                {
                    caster1Textbox.Enabled = false;
                    caster1Label.Enabled = false;
                    caster1TwitterLabel.Enabled = false;
                    caster1TwitterTextbox.Enabled = false;
                }
                else
                {
                    caster1Textbox.Enabled = true;
                    caster1Label.Enabled = true;
                    caster1TwitterLabel.Enabled = true;
                    caster1TwitterTextbox.Enabled = true;
                }
            }
            else if (num == 2)
            {
                if (grey)
                {
                    caster2Textbox.Enabled = false;
                    caster2Label.Enabled = false;
                    caster2TwitterLabel.Enabled = false;
                    caster2TwitterTextbox.Enabled = false;
                }
                else
                {
                    caster2Textbox.Enabled = true;
                    caster2Label.Enabled = true;
                    caster2TwitterLabel.Enabled = true;
                    caster2TwitterTextbox.Enabled = true;
                }
            }
            else
                throw new Exception("Number parameter can either be 1 or 2");
        }

        private void gameTypeTab_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void templatesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TemplateFile != null) { TemplateFile = null; }
                if (!selectedFileLabel.Text.Equals("")) { selectedFileLabel.Text = ""; }

                string resource = (string)templateListView.Items[templateListView.SelectedIndex];
                TemplateFile = getImageResourcePath("template", resource);
                TemplateFileName = resource;

                string imageFilePath = TemplateFile;
                changePictureInBox(templatePictureBox, imageFilePath);
            }
            catch (Exception ex)
            {
                //Do nothing. User hit white space in box
            }            
        }

        private void casterTemplatesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string resource = (string)casterTemplateListView.Items[casterTemplateListView.SelectedIndex];
                CasterTemplateFile = getImageResourcePath("caster", resource);
                CasterTemplateFileName = resource;

                string imageFilePath = CasterTemplateFile;
                changePictureInBox(casterTemplatePictureBox, imageFilePath);
            }
            catch (Exception ex)
            {

            }
        }

        private void changePictureInBox(PictureBox box, string resourcePath)
        {
            Stream imageStream = myAssembly.GetManifestResourceStream(resourcePath);
            Bitmap image = new Bitmap(imageStream);                        

            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.Image = image;
            imageStream.Close();
        }

        private string getImageResourcePath(string type, string resourceName)
        {
            if (type.Equals("template"))
            {
                return this.ProductName + ".Images.Templates." + resourceName;
            }
            else if (type.Equals("caster"))
            {
                return this.ProductName + ".Images.Caster." + resourceName;
            }
            else
                throw new Exception("String type agrument can only be 'template' or 'caster'");

        }

        public void showPicturePreviewForm(Image image)
        {
            PicturePreviewForm picturePreviewForm = new PicturePreviewForm(image);
            picturePreviewForm.Location = new Point(500, 100);
            picturePreviewForm.Show();
        }
        #endregion FormOperations

        #region ButtonListeners
        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                generate();
            }
            catch (Exception ex)
            {
                // Need to put this in front of the form
                MessageBox.Show(ex.Message);
                //MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void generate()
        {
            try
            {
                //VERIFY TOURNAMENT AND ROUND ARE FILLED IN
                verifyTourneyDataFilled();
                //VERIFY THE TOURNAMENT ROUND IS FILLED
                verifyTournamentRoundFilled();
                //VERIFY THE COMBATANTS FIELDS
                verifyCombatantsFilled();

                //GET THE COMBATANTS' NAMES (SET TO THE GLOBAL VARIABLES)
                getCombatants(GameType);
                //VERIFY THE SCORES ARE IN CORRECT FORMAT
                checkScoreFormat();

                getTournamentData();

                string resource = TemplateFile.Contains(".Images.Templates.") ? "resource" : "file";

                //ALTER THE IMAGE WITH NEW INFORMATION
                paintCompetitorText(TemplateFile, resource);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void generateCasterOverlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                verifyCasterDataFilledIn();
                getCasters();
                paintCasterText(CasterTemplateFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void singlesClearButton_Click_1(object sender, EventArgs e)
        {
            clearSinglesFields();
        }

        private void doublesClearButton_Click(object sender, EventArgs e)
        {
            clearDoublesFields();
        }

        private void tourneyClearButton_Click(object sender, EventArgs e)
        {
            clearTourneyFields();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (templateListView.SelectedIndex != -1) 
            { 
                TemplateFile = null;
                templateListView.ClearSelected();
                templatePictureBox.Image = null;
            }

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
                string newName = File.DirectoryName + "\\" + tournamentNameTextbox.Text + File.Name;
                //DEFINE THE GLOBAL VARIBLE FOR NEW TEMPLATE
                NewTemplateFile = @newName;
            }
        }

        private void singlesButtonBig_Click(object sender, EventArgs e)
        {
            clearDoublesFields();
            GameType = "singles";
            smashOverlayTabControl.SelectedTab = competitorsTab;
            greyOutSingles(false);
            greyOutDoubles(true);
        }

        private void doublesButtonBig_Click(object sender, EventArgs e)
        {
            clearSinglesFields();
            GameType = "doubles";
            smashOverlayTabControl.SelectedTab = competitorsTab;
            greyOutSingles(true);
            greyOutDoubles(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            smashOverlayTabControl.SelectedTab = gameTypeTabTab;
        }

        private void clearImageSelection_Click(object sender, EventArgs e)
        {
            selectedFileLabel.Text = "";
            TemplateFile = "";
            NewTemplateFile = "";
        }

        private void caster2Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            greyOutCaster(2, !caster2Checkbox.Checked);
        }

        private void caster1Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            greyOutCaster(1, !caster1Checkbox.Checked);
        }

        private void clearCasterButton_Click(object sender, EventArgs e)
        {
            caster1Textbox.Text = "";
            caster2Textbox.Text = "";
            caster1TwitterTextbox.Text = "";
            caster2TwitterTextbox.Text = "";
            casterTemplateListView.SelectedIndex = -1;
            casterTemplatePictureBox.Image = null;

            Caster1 = null;
            Caster2 = null;
            Caster1Twitter = null;
            Caster2Twitter = null;
            CasterTemplateFile = null;
            CasterTemplateFileName = null;
        }

        private void casterTemplatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                myAssembly = Assembly.GetExecutingAssembly();
                Stream imageStream = myAssembly.GetManifestResourceStream(CasterTemplateFile);
                Bitmap image = new Bitmap(imageStream);

                showPicturePreviewForm(image);
            }
            catch (Exception ex)
            {
                //Do nothing
            }
        }

        private void templatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                myAssembly = Assembly.GetExecutingAssembly();
                Stream imageStream = myAssembly.GetManifestResourceStream(TemplateFile);
                Bitmap image = new Bitmap(imageStream);

                showPicturePreviewForm(image);
            }
            catch (Exception ex)
            {
                //Do nothing
            }
        }
        
        #endregion ButtonListeners             

        #region ErrorHandling
        private void verifyCombatantsFilled()
        {
            if(GameType.Equals("singles")){

                singlesP1Label.ForeColor = Color.Black;
                singlesP2Label.ForeColor = Color.Black;
                singlesP1ScoreLabel.ForeColor = Color.Black;
                singlesP2ScoreLabel.ForeColor = Color.Black;

                if (isEmpty(singlesP1Textbox))
                {
                    singlesP1Label.ForeColor = Color.Red;
                    putCursorInBox(singlesP1Textbox);
                    throw new Exception("Player 1 must have a name");
                }
                else if (isEmpty(singlesP2Textbox))
                {
                    singlesP2Label.ForeColor = Color.Red;
                    putCursorInBox(singlesP2Textbox);
                    throw new Exception("Player 2 must have a name");
                }
                else if (isEmpty(singlesP1ScoreTextbox))
                {
                    singlesP1ScoreLabel.ForeColor = Color.Red;
                    putCursorInBox(singlesP1ScoreTextbox);
                    throw new Exception("Player 1 must have a score");
                }
                else if (isEmpty(singlesP2ScoreTextbox))
                {
                    singlesP2ScoreLabel.ForeColor = Color.Red;
                    putCursorInBox(singlesP2ScoreTextbox);
                    throw new Exception("Player 2 must have a score");
                }
            }
            else if (GameType.Equals("doubles"))
            {
                doublesT1P1Label.ForeColor = Color.Black;
                doublesT1P2Label.ForeColor = Color.Black;
                doublesT2P1Label.ForeColor = Color.Black;
                doublesT2P2Label.ForeColor = Color.Black;
                doublesT1ScoreLabel.ForeColor = Color.Black;
                doublesT2ScoreLabel.ForeColor = Color.Black;

                if (isEmpty(doublesT1P1Textbox))
                {
                    doublesT1P1Label.ForeColor = Color.Red;
                    putCursorInBox(doublesT1P1Textbox);
                    throw new Exception("Team 1: Member 1 must have a name");
                }
                else if (isEmpty(doublesT1P2Textbox))
                {
                    doublesT1P2Label.ForeColor = Color.Red;
                    putCursorInBox(doublesT1P2Textbox);
                    throw new Exception("Team 1: Member 2 must have a name");
                }
                else if (isEmpty(doublesT2P1Textbox))
                {
                    doublesT2P1Label.ForeColor = Color.Red;
                    putCursorInBox(doublesT2P1Textbox);
                    throw new Exception("Team 2: Member 1 must have a name");
                }
                else if (isEmpty(doublesT2P2Textbox))
                {
                    doublesT2P2Label.ForeColor = Color.Red;
                    putCursorInBox(doublesT2P2Textbox);
                    throw new Exception("Team 2: Member 2 must have a name");
                }
                else if (isEmpty(doublesT1ScoreTextbox))
                {
                    doublesT1ScoreLabel.ForeColor = Color.Red;
                    putCursorInBox(doublesT1ScoreTextbox);
                    throw new Exception("Team 1 must have a score");
                }
                else if (isEmpty(doublesT2ScoreTextbox))
                {
                    doublesT2ScoreLabel.ForeColor = Color.Red;
                    putCursorInBox(doublesT2ScoreTextbox);
                    throw new Exception("Team 2 must have a score");
                }
            }
            else
               throw new Exception("An Error Occured" +
                    "\r\nIf Sinlges, only Singles section can contain text and all fields must be filled." +
                    "\r\nIf Doubles, only Doubles section can contain text and all fields must be filled. \r\n" +
                    "\r\nPlease correct any errors and try again. \r\nIf problem persists, contact cookiE.");
        }

        private void verifyTournamentRoundFilled()
        {
            if (tournamentRoundTextbox.Text.Equals(""))
            {
                tournamentRoundLabel.ForeColor = Color.Red;
                tournamentRoundTextbox.Focus();
                tournamentRoundTextbox.Select(0, 0);
                throw new Exception("An Error Occured" +
                    "\r\nTournament Round not filled in." +
                    "\r\nPlease correct field and try again");
            }
            else
                tournamentRoundLabel.ForeColor = Color.Black;
        }

        private void verifyTourneyDataFilled()
        {
            tournamentNameLabel.ForeColor = Color.Black;
            browseButton.ForeColor = Color.Black;

            if (tournamentNameTextbox.Text.Equals(""))
            {
                smashOverlayTabControl.SelectedTab = tourneyDataTab;
                tournamentNameLabel.ForeColor = Color.Red;
                tournamentNameTextbox.Focus();
                tournamentNameTextbox.Select(0, 0);
                throw new Exception("An Error Occured" +
                    "\r\nTournament Name not completely filled in" +
                    "\r\nPlease correct fields and try again");
            }
            else if (TemplateFile == null)
            {
                smashOverlayTabControl.SelectedTab = tourneyDataTab;
                browseButton.ForeColor = Color.Red;
                browseButton.Focus();
                throw new Exception("An Error Occured" +
                    "\r\nImage Overlay not selected" +
                    "\r\nPlease select image and try again");
            }
        }

        private void verifyCasterDataFilledIn()
        {
            caster1Label.ForeColor = Color.Black;
            caster2Label.ForeColor = Color.Black;

            if (!caster1Checkbox.Checked && !caster2Checkbox.Checked)
                throw new Exception("No Casters Selected");

            if (caster1Checkbox.Checked && caster1Textbox.Text.Equals(""))
            {
                smashOverlayTabControl.SelectedTab = casterTab;
                caster1Label.ForeColor = Color.Red;
                putCursorInBox(caster1Textbox);
                throw new Exception("Caster 1's Name cannot be blank");
            }
                

            if (caster2Checkbox.Checked && caster2Textbox.Text.Equals(""))
            {
                smashOverlayTabControl.SelectedTab = casterTab;
                caster2Label.ForeColor = Color.Red;
                putCursorInBox(caster2Textbox);
                throw new Exception("Caster 2's Name cannot be blank");
            }

            if (CasterTemplateFile == null || CasterTemplateFile.Equals(""))
            {
                throw new Exception("Caster Template File not selected!");
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
        #endregion ErrorHandling

        #region General
        public void putCursorInBox(TextBox box)
        {
            box.Focus();
            box.Select(0, 0);
        }

        public bool isEmpty(TextBox str)
        {
            return str.Text.Equals("");
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
        #endregion General

        

        #region DEBUG
        private CasterTemplate debugCasterTemplate(string fileName)
        {
            bool caster1Checked = caster1Checkbox.Checked;
            bool caster2Checked = caster2Checkbox.Checked;

            CasterPoint p1 = new CasterPoint(350, 850);
            CasterPoint p2 = new CasterPoint(1250, 850);

            TwitterPoint twitter1Point = new TwitterPoint(400, 910);
            TwitterPoint twitter2Point = new TwitterPoint(1300, 910);

            CasterTemplate cTemplate = 
                new CasterTemplate(fileName, 
                                caster1Checked, caster2Checked,
                                p1, p2, twitter1Point, twitter2Point);
            Bitmap image = cTemplate.drawTextOnImage(this);
            cTemplate.saveImage(image);

            return cTemplate;
        }

        private CompetitorTemplate debugCompetitorTemplate(string resourceType, 
                                                           string fileName)
        {
            CompetitorPoint player1P = new CompetitorPoint(501, 1063);
            CompetitorPoint player1CamP = new CompetitorPoint(1686, 354);

            CompetitorPoint player2P = new CompetitorPoint(976, 1063);
            CompetitorPoint player2CamP = new CompetitorPoint(1686, 720);

            ScorePoint player1ScoreP = new ScorePoint(680, 1030);
            ScorePoint player2ScoreP = new ScorePoint(800, 1030);

            TournamentPoint tournamentP = new TournamentPoint(900, 25);

            CompetitorTemplate cTemplate =
                new CompetitorTemplate(fileName, player1P, player2P, 
                                player1CamP, player2CamP, player1ScoreP, 
                                player2ScoreP, tournamentP);
            Bitmap image = cTemplate.drawTextOnImage(this);
            cTemplate.saveImage(image);

            return cTemplate;
        }
        #endregion DEBUG

        private bool dataFilledIn()
        {
            try
            {
                verifyTourneyDataFilled();
                verifyCombatantsFilled();
                verifyTournamentRoundFilled();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        private void incrementPlayer1Button_Click(object sender, EventArgs e)
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(singlesP1ScoreTextbox.Text);
                score++;
                singlesP1ScoreTextbox.Text = score.ToString();

                generate();
            }
            
        }

        private void decrementPlayer1Button_Click(object sender, EventArgs e)
        {
            int score = Int32.Parse(singlesP1ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    score--;
                    singlesP1ScoreTextbox.Text = score.ToString();

                    generate();
                }
            }
        }

        private void incrementPlayer2Button_Click(object sender, EventArgs e)
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(singlesP2ScoreTextbox.Text);
                score++;
                singlesP2ScoreTextbox.Text = score.ToString();

                generate();
            }
        }

        private void decrementPlayer2Button_Click(object sender, EventArgs e)
        {
            int score = Int32.Parse(singlesP2ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    score--;
                    singlesP2ScoreTextbox.Text = score.ToString();

                    generate();
                }
            }
        }

        private void incrementTeam1Button_Click(object sender, EventArgs e)
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(doublesT1ScoreTextbox.Text);
                score++;
                doublesT1ScoreTextbox.Text = score.ToString();

                generate();
            }
        }

        private void decrementTeam1Button_Click(object sender, EventArgs e)
        {
            int score = Int32.Parse(doublesT1ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    score--;
                    doublesT1ScoreTextbox.Text = score.ToString();

                    generate();
                }
            }
        }

        private void incrementTeam2Button_Click(object sender, EventArgs e)
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(doublesT2ScoreTextbox.Text);
                score++;
                doublesT2ScoreTextbox.Text = score.ToString();

                generate();
            }
        }

        private void decrementTeam2Button_Click(object sender, EventArgs e)
        {
            int score = Int32.Parse(doublesT2ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    score--;
                    doublesT2ScoreTextbox.Text = score.ToString();

                    generate();
                }
            }
        }

        private void autoUpdateCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
