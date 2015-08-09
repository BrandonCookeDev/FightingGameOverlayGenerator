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
using System.Runtime.Serialization;
using System.Threading;
using System.ServiceModel;
using SmashOverlayGeneratorMk2.BackendOps;
using SmashOverlayGeneratorMk2.BMVersion;
using SmashOverlayGeneratorMk2.Objects.Points;
using SmashOverlayGeneratorMk2.Objects;
//using SmashOverlayGeneratorWebServiceLib;
//using SmashOverlayGeneratorWebServiceLib.Objects;
using SmashOverlayGeneratorMk2.ErrorHandling;
using SmashOverlayGeneratorMk2.General;
using XSplit.Core.Properties;
//using XSplitBroadcasterLib;

namespace SmashOverlayGeneratorMk2
{
    [DataContract]
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
        private string matchupPicFile;
        private string matchupPicFileName;
        private string matchupCompetitor1;
        private string matchupCompetitor2;
        private string matchupCharacter1File;
        private string matchupCharacter2File;
        private string matchupCharacter1;
        private string matchupCharacter2;
        private string tournamentLogoFile;
        private string resourceType;
        private bool nameSwap;
        private bool isError;
        private bool isAutoUpdate;
        private ArrayList templates;
        private ArrayList casterTemplates;

        private string serviceUrl = "http://192.168.0.5:8081/SOGCS.svc";
        public string overlayDirectory = @"C:\\OverlayGenerator";
        private Resources _resources = new Resources();
        
        //private ISOGControlService service = null;

        //This will define the connection to the service
        //private Connection conn = null;

        delegate void SetTextCallback(string text, bool err);

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

        
        public string MatchupPicFile
        {
            get { return this.matchupPicFile; }
            set { this.matchupPicFile = value; }
        }

        
        public string MatchupPicFileName
        {
            get { return this.matchupPicFileName; }
            set { this.matchupPicFileName = value; }
        }

        
        public string MatchupCompetitor1
        {
            get { return this.matchupCompetitor1; }
            set { this.matchupCompetitor1 = value; }
        }

        
        public string MatchupCompetitor2
        {
            get { return this.matchupCompetitor2; }
            set { this.matchupCompetitor2 = value; }
        }

        
        public string MatchupCharacter1File
        {
            get { return this.matchupCharacter1File; }
            set { this.matchupCharacter1File = value; }
        }

        
        public string MatchupCharacter2File
        {
            get { return this.matchupCharacter2File; }
            set { this.matchupCharacter2File = value; }
        }

        public string MatchupCharacter1
        {
            get { return this.matchupCharacter1; }
            set { this.matchupCharacter1 = value; }
        }

        public string MatchupCharacter2
        {
            get { return this.matchupCharacter2; }
            set { this.matchupCharacter2 = value; }
        }

        public string TournamentLogoFile
        {
            get { return this.tournamentLogoFile; }
            set { this.tournamentLogoFile = value; }
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

        public bool NameSwap
        {
            get { return this.nameSwap; }
            set { this.nameSwap = value; }
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
        #region LOADCLOSEFORM
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

            ListBoxFcns.populateListView(myAssembly, templateListView, "Templates");
            ListBoxFcns.populateListView(myAssembly, casterTemplateListView, "Caster");
            ListBoxFcns.populateListView(myAssembly, matchupPicListView, "Matchup");

            //CHECK FOR TEMPORARY TOURNAMENT INFORMATION FILE
            if (!Directory.Exists(overlayDirectory))
            {
                Directory.CreateDirectory(@"C:\\OverlayGenerator");
            }

            if (File.Exists(overlayDirectory + "\\overlay.temp"))
            {
                bool debug = true;
                switch (MessageBox.Show(this, "Previous Tournament Info Exists! Import data?", "Import", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        //LOAD FILE INFO INTO GENERATOR
                        GenFcns.loadDataFile(this);
                        tournamentNameTextbox.Text = TournamentName;
                        tournamentRoundCombobox.SelectedItem = TournamentRound;
                        singlesP1Textbox.Text = Competitor1;
                        singlesP1ScoreTextbox.Text = Score1;
                        singlesP2Textbox.Text = Competitor2;
                        singlesP2ScoreTextbox.Text = Score2;
                        templateListView.SelectedItem = TemplateFileName;
                        switch (GameType)
                        {
                            case "singles":
                                greyOutSingles(false);
                                greyOutDoubles(true);
                                break;
                            default:
                                greyOutSingles(true);
                                greyOutDoubles(false);
                                break;
                        }

                        if (debug)
                        {
                            matchupCompetitor1Textbox.Text = MatchupCompetitor1;
                            matchupCompetitor2Textbox.Text = MatchupCompetitor2;
                            matchupCharacter1Combobox.SelectedItem = MatchupCharacter1File.Replace(".png", "");
                            matchupCharacter2Combobox.SelectedItem = MatchupCharacter2File.Replace(".png", "");
                            matchupPicListView.SelectedItem = MatchupPicFileName;
                        }

                        break;
                    default:
                        break;
                }
            }

            //ATTEMPT TO CONNECT TO RUNNING SMASH OVERLAY GENERATOR WEB SERVICE
            try
            {
                Thread thread = new Thread(() =>
                {
                    EndpointAddress address = new EndpointAddress(new Uri(serviceUrl));
                    WSHttpBinding binding = new WSHttpBinding();
                    //ChannelFactory<ISOGControlService> fac = new ChannelFactory<ISOGControlService>(binding, address);

                    //service = fac.CreateChannel();
                    logToUser("Channel successfully made!", false);
                    makeConnection();
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Connection was not made");
                logToUser("Connection was not made", true);
            }
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
                    try
                    {
                        GenFcns.createDataFile(this);
                    }
                    catch (Exception ex) { }
                    break;
            }
        }
        #endregion LOADCLOSEFORM

        #region GeneratePicture
        private void paintCompetitorText(string filePath, string resourceType, bool nameSwap)
        {
            logToUser("GENERATING...", false);

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
            
            hardcodedCompetitorTemplate(resourceType, filePath, nameSwap);
            logToUser("Completed without error...", false);
        }

        private void paintCasterText(string filePath)
        {
            logToUser("GENERATING...", false);

            Bitmap bMap = null;
            Stream imageStream = null;

            myAssembly = Assembly.GetExecutingAssembly();
            imageStream = myAssembly.GetManifestResourceStream(filePath);
            bMap = new Bitmap(imageStream);

            
            hardcodedCasterTemplate(filePath);
            logToUser("Completed without error...", false);
        }

        private void paintMatchupPicture(string filePath, 
                                         string character1File,
                                         string character2File)
        {
            logToUser("GENERATING...", false);

            Bitmap bMap = null;
            Stream imageStream = null;

            //Get Background
            myAssembly = Assembly.GetExecutingAssembly();                                    
            imageStream = myAssembly.GetManifestResourceStream(filePath);
            bMap = new Bitmap(imageStream);

            hardCodedMatchupPicture(filePath, character1File, character2File);
            logToUser("Completed without error...", false);
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
            TournamentRound = tournamentRoundCombobox.Text;
        }

        private void getMatchupCompetitorData()
        {
            MatchupCompetitor1 = matchupCompetitor1Textbox.Text;
            //MatchupCharacter1File = matchupCharacter1Combobox.Text;
            MatchupCompetitor2 = matchupCompetitor2Textbox.Text;
            //MatchupCharacter2File = matchupCharacter2Combobox.Text;
        }
        #endregion GeneratePicture

        #region FormOperations
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
                TemplateFile = ListBoxFcns.getResourcePath(this.ProductName, "template", resource);
                TemplateFileName = resource;

                string imageFilePath = TemplateFile;
                ListBoxFcns.changePictureInBox(myAssembly, templatePictureBox, imageFilePath);
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
                CasterTemplateFile = ListBoxFcns.getResourcePath(this.ProductName, "caster", resource);
                CasterTemplateFileName = resource;

                string imageFilePath = CasterTemplateFile;
                ListBoxFcns.changePictureInBox(myAssembly, casterTemplatePictureBox, imageFilePath);
            }
            catch (Exception ex)
            {

            }
        }


        private void matchupPicListView_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                string resource = (string)matchupPicListView.Items[matchupPicListView.SelectedIndex];
                MatchupPicFile = ListBoxFcns.getResourcePath(this.ProductName, "matchup", resource);
                MatchupPicFileName = resource;

                string imageFilePath = MatchupPicFile;
                ListBoxFcns.changePictureInBox(myAssembly, matchupPictureBox, imageFilePath);
            }
            catch (Exception ex)
            {
            }
        }

        private void matchupCharacter1Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string resource = (string)matchupCharacter1Combobox.Items[matchupCharacter1Combobox.SelectedIndex];
                MatchupCharacter1File = ListBoxFcns.getResourcePath(this.ProductName, "character", resource);
                MatchupCharacter1 = resource;
            }
            catch (Exception ex)
            {
                logToUser("ERROR GETTING CHARACTER PIC....", true);
            }
        }

        private void matchupCharacter2Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string resource = (string)matchupCharacter2Combobox.Items[matchupCharacter2Combobox.SelectedIndex];
                MatchupCharacter2File = ListBoxFcns.getResourcePath(this.ProductName, "character", resource);
                MatchupCharacter2 = resource;
            }
            catch (Exception ex)
            {
                logToUser("ERROR GETTING CHARACTER PIC....", true);
            }
        }
        #endregion FormOperations

        #region BackendOps
        private void generate()
        {
            try
            {
                if (!dataFilledIn())
                    return;

                //GET THE COMBATANTS' NAMES (SET TO THE GLOBAL VARIABLES)
                getCombatants(GameType);
                //VERIFY THE SCORES ARE IN CORRECT FORMAT
                ErrorHandler.checkScoreFormat(Score1, Score2);

                getTournamentData();

                string resource = TemplateFile.Contains(".Images.Templates.") ? "resource" : "file";

                //ALTER THE IMAGE WITH NEW INFORMATION
                paintCompetitorText(TemplateFile, resource, NameSwap);


            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
            }
        }

        

        #region Score Management
        private void incrementPlayer1()
        {
            if (dataFilledIn())
            {
                ScoreManager.incrementPlayer(singlesP1ScoreTextbox);
                generate();
            }
        }
        private void decrementPlayer1()
        {
            int score = Int32.Parse(singlesP1ScoreTextbox.Text);
            //if (!BMVersion.BMMode)
            //{
                if (score > 0)
                {
                    if (dataFilledIn())
                    {
                        ScoreManager.decrementPlayer(score, singlesP1ScoreTextbox);
                        generate();
                    }
                }
            //}
       }
        private void incrementPlayer2()
        {
            if (dataFilledIn())
            {
                ScoreManager.incrementPlayer(singlesP2ScoreTextbox);
                generate();
            }
        }
        private void decrementPlayer2()
        {
            int score = Int32.Parse(singlesP2ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    ScoreManager.decrementPlayer(score, singlesP2ScoreTextbox);
                    generate();
                }
            }
        }
        private void incrementTeam1()
        {
            if (dataFilledIn())
            {
                ScoreManager.incrementPlayer(doublesT1ScoreTextbox);
                generate();
            }
        }
        private void decrementTeam1()
        {
            int score = Int32.Parse(doublesT1ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    ScoreManager.decrementPlayer(score, doublesT1ScoreTextbox);
                    generate();
                }
            }
        }
        private void incrementTeam2()
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(doublesT2ScoreTextbox.Text);
                score++;
                doublesT2ScoreTextbox.Text = score.ToString();

                generate();
            }
        }
        private void decrementTeam2()
        {
            int score = Int32.Parse(doublesT2ScoreTextbox.Text);
            if (score > 0)
            {
                if (dataFilledIn())
                {
                    ScoreManager.decrementPlayer(score, doublesT2ScoreTextbox);
                    generate();
                }
            }
        }
        #endregion ScoreManagement
        #endregion BackendOps

        #region Listeners
        #region Score Button Listeners
        private void incrementPlayer1Button_Click(object sender, EventArgs e)
        {
            incrementPlayer1();
        }
        private void decrementPlayer1Button_Click(object sender, EventArgs e)
        {
            decrementPlayer1();
        }
        private void incrementPlayer2Button_Click(object sender, EventArgs e)
        {
            incrementPlayer2();
        }
        private void decrementPlayer2Button_Click(object sender, EventArgs e)
        {
            decrementPlayer2();
        }
        private void incrementTeam1Button_Click(object sender, EventArgs e)
        {
            incrementTeam1();
        }
        private void decrementTeam1Button_Click(object sender, EventArgs e)
        {
            decrementTeam1();
        }
        private void incrementTeam2Button_Click(object sender, EventArgs e)
        {
            incrementTeam2();
        }
        private void decrementTeam2Button_Click(object sender, EventArgs e)
        {
            decrementTeam2();
        }
        #endregion Score Button Listeners

        #region Generate Button Listeners
        private void generateButton_Click(object sender, EventArgs e)
        {
            try
            {
                generate();
            }
            catch (Exception ex)
            {
                // Need to put this in front of the form
                logToUser(ex.Message.ToString(), true);
                //MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        private void generateMatchupPicBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!matchupDataFilledIn())
                    return;

                getMatchupCompetitorData();
                paintMatchupPicture(MatchupPicFile, MatchupCharacter1File, MatchupCharacter2File);

            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
            }
        }

        private void generateCasterOverlayButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(!casterDataFilledIn())
                    return;

                getCasters();
                paintCasterText(CasterTemplateFile);
            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
            }
        }

        private void swapNamesBtn_Click(object sender, EventArgs e)
        {
            if (NameSwap) NameSwap = false;
            else NameSwap = true;
            generate();
        }
        #endregion Generate Button Listeners

        #region Clear Button Listeners
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

        private void clearImageSelection_Click(object sender, EventArgs e)
        {
            selectedFileLabel.Text = "";
            TemplateFile = "";
            NewTemplateFile = "";
        }
        #endregion Clear Button Listeners

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
        
        private void caster2Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            greyOutCaster(2, !caster2Checkbox.Checked);
        }

        private void caster1Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            greyOutCaster(1, !caster1Checkbox.Checked);
        }

        #region Picture Preview 
        private void casterTemplatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                myAssembly = Assembly.GetExecutingAssembly();
                Stream imageStream = myAssembly.GetManifestResourceStream(CasterTemplateFile);
                Bitmap image = new Bitmap(imageStream);

                ListBoxFcns.showPicturePreviewForm(image);
            }
            catch (Exception ex)
            {
                //Do nothing. User hit White Space
            }
        }

        private void templatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                myAssembly = Assembly.GetExecutingAssembly();
                Stream imageStream = myAssembly.GetManifestResourceStream(TemplateFile);
                Bitmap image = new Bitmap(imageStream);

                ListBoxFcns.showPicturePreviewForm(image);
            }
            catch (Exception ex)
            {
                //Do nothing. User Hit WHITE SPACE
            }
        }

        private void matchupPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                myAssembly = Assembly.GetExecutingAssembly();
                Stream imageStream = myAssembly.GetManifestResourceStream(MatchupPicFile);
                Bitmap image = new Bitmap(imageStream);

                ListBoxFcns.showPicturePreviewForm(image);
            }
            catch (Exception ex)
            {
                //Do nothing. User hit White Space.
            }
        }
        #endregion Picture Preview

        //SHORTCUT FOR SCORE UP AND DOWN
        private void SmashOverlayGenerator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                incrementPlayer1();
            }
            else if (e.KeyCode == Keys.F6)
            {
                decrementPlayer1();
            }
            else if (e.KeyCode == Keys.F7)
            {
                incrementPlayer2();
            }
            else if (e.KeyCode == Keys.F8)
            {
                decrementPlayer2();
            }
            else if (e.KeyCode == Keys.F1)
            {
                GenFcns.putCursorInBox(singlesP1Textbox);
                singlesP1Textbox.SelectAll();
            }
            else if (e.KeyCode == Keys.F2)
            {
                GenFcns.putCursorInBox(singlesP2Textbox);
                singlesP2Textbox.SelectAll();
            }
        }
        #endregion Listeners             

        #region ErrorHandling
        private bool matchupDataFilledIn()
        {
            try
            {
                ErrorHandler.verifyMatchupDataFilled(
                    MatchupPicFile, matchupPicListView, matchupCompetitor1Label,
                    matchupCompetitor2Label, matchupCompetitor1Textbox,
                    matchupCharacter1Combobox, matchupCompetitor2Textbox,
                    matchupCharacter2Combobox);
            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
                return false;
            }
            return true;
        }

        private bool casterDataFilledIn()
        {
            try
            {
                ErrorHandler.verifyCasterDataFilledIn(CasterTemplateFile,
                                                     caster1Label, caster2Label,
                                                     caster1Checkbox, caster2Checkbox,
                                                     caster1Textbox, caster2Textbox,
                                                     smashOverlayTabControl,
                                                     casterTab);
            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
                return false;
            }
            return true;            
        }

        private bool dataFilledIn()
        {
            try
            {
                //verifyTourneyDataFilled();
                ErrorHandler.verifyTourneyDataFilled(TemplateFile, tournamentNameLabel,
                                             tournamentNameTextbox, browseButton,
                                             smashOverlayTabControl, tourneyDataTab);

                //verifyCombatantsFilled();
                ErrorHandler.verifyCombatantsFilled(GameType,
                                             singlesP1Label, singlesP2Label,
                                             singlesP1ScoreLabel, singlesP2ScoreLabel,
                                             singlesP1Textbox, singlesP2Textbox,
                                             singlesP1ScoreTextbox, singlesP2ScoreTextbox,

                                             doublesT1P1Label, doublesT1P2Label, doublesT2P1Label,
                                             doublesT2P2Label, doublesT1ScoreLabel, doublesT2ScoreLabel,
                                             doublesT1P1Textbox, doublesT1P2Textbox,
                                             doublesT2P1Textbox, doublesT2P2Textbox,
                                             doublesT1ScoreTextbox, doublesT2ScoreTextbox);
                
                //verifyTournamentRoundFilled();
                ErrorHandler.verifyTournamentRoundFilled(tournamentRoundCombobox,
                                                  tournamentRoundLabel);
            }
            catch (Exception ex)
            {
                logToUser(ex.Message.ToString(), true);
                return false;
            }
            return true;
        }
        #endregion ErrorHandling

        #region Hardcoded Generation
        private CasterTemplate hardcodedCasterTemplate(string fileName)
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

        private CompetitorTemplate hardcodedCompetitorTemplate(string resourceType, 
                                                           string fileName, bool nameSwap)
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
            Bitmap image = cTemplate.drawTextOnImage(this, nameSwap);
            cTemplate.saveImage(image);

            return cTemplate;
        }

        private MatchupPictureTemplate hardCodedMatchupPicture(string fileName, 
                                                               string character1File, string character2File)
        {
            CompetitorPoint matchupC1P = new CompetitorPoint(1920/4, 920);
            CompetitorPoint matchupC2P = new CompetitorPoint(1920/4*3, 920);
            TournamentPoint tournamentP = new TournamentPoint(700, 150);
            TournamentPoint tournamentRoundP = new TournamentPoint(1920/2, 600);
            CharacterPoint character1P = new CharacterPoint(75, 75);
            CharacterPoint character2P = new CharacterPoint(1250, 75);

            MatchupPictureTemplate mpTemplate = new MatchupPictureTemplate(fileName, GameType, 
                   tournamentP, tournamentRoundP, MatchupCharacter1File, character1P, 
                   matchupC1P, MatchupCharacter2File, character2P, matchupC2P);

            Bitmap image = mpTemplate.drawTextOnImage(this);
            mpTemplate.saveImage(image);
            return mpTemplate;
        }
        
        #endregion Hardcoded Generation
        
        #region ServiceMethods
        /** THIS METHOD WILL NEED TO RUN AN ASYNC THREAD IN THE BG **/
        public void refresh(){
            /*
            if (Conn != null)
            {
                //TourneyData td = service.getRecentTourneyData(Conn);
                //updateInfo(td);
            }
             */
        }

        public void makeWindowTest()
        {
            MessageBox.Show("It worked!");
        }

        private void makeConnectionBtn_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = makeConnection();
                if (success)
                {
                    makeWindowTest();
                }
                else
                {
                    MessageBox.Show("Connection Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR");
            }
        }

        public bool makeConnection()
        {
            try
            {
                //int num = service.requestConnectionNumber();
                //Conn = new Connection(num, this);
                //bool success = service.addConnection(Conn);

                //if (success) return true;
                //else return false;
                return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show("Error on makeConnection()");
                logToUser("Error on makeConnection()", true);
                return false;
            }
        }

        private void autoUpdateCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        public bool transferData(string data)
        {
            /*
            if (Conn != null)
            {
                try
                {
                    
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
             */
            return false;
        }
        #endregion ServiceMethods

        #region Threads
        public static Thread connThread(Action action)
        {
            //Thread thread = new Thread();
            return null;
        }
        #endregion Threads

        private void logToUser(string msg, bool err)
        {
            if (err)
            {
                logMessageLabel.ForeColor = Color.Red;
            }
            else
            {
                logMessageLabel.ForeColor = Color.Navy;
            }
            if (logMessageLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(logToUser);
                logMessageLabel.Invoke(d, new object[] { msg, err });
            }
            else
            {
                logMessageLabel.Text = "..." + msg;
            }
        }

        private void matchupBGPictureBrowseBtn_Click(object sender, EventArgs e)
        {
            
            if (matchupPicListView.SelectedIndex != -1)
            {
                MatchupPicFile = null;
                matchupPicListView.ClearSelected();
                matchupPictureBox.Image = null;
            }

            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);

                //Show the file name in the program window
                selectedMatchupFileLabel.Text = File.Name;

                //DEFINE GLOBAL VARIABLE FOR TEMPLATE
                MatchupPicFile = @File.FullName;
                //DEFINE THE NEW PATH
                string newName = File.DirectoryName + "\\" + tournamentNameTextbox.Text + "_matchup" + File.Name;
                //DEFINE THE GLOBAL VARIBLE FOR NEW TEMPLATE
                NewTemplateFile = @newName;
            }
        }

        private void matchupTournamentLogoBrowse_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
                
                //DEFINE GLOBAL VARIABLE FOR TEMPLATE
                TournamentLogoFile = @File.FullName;
            }
        }

        private void clearMatchupDataBtn_Click(object sender, EventArgs e)
        {
            matchupCompetitor1Textbox.Text = "";
            MatchupCompetitor1 = "";
            matchupCharacter1Combobox.SelectedItem = -1;
            MatchupCharacter1 = "";
            MatchupCharacter1File = "";

            matchupCompetitor2Textbox.Text = "";
            MatchupCompetitor2 = "";
            matchupCharacter2Combobox.SelectedItem = -1;
            MatchupCharacter2 = "";
            MatchupCharacter2File = "";

            matchupPicListView.SelectedItem = -1;
            MatchupPicFile = "";
        }

        private void tournamentRoundCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            getTournamentData();
        }
    }    
}