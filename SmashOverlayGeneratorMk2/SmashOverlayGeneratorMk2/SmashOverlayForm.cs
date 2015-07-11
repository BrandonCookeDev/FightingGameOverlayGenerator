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
using System.ServiceModel;
using SmashOverlayGeneratorWebServiceLib;
using SmashOverlayGeneratorWebServiceLib.Objects;
using System.Runtime.Serialization;
using System.Threading;
using SmashOverlayGeneratorMk2.ErrorHandling;
using SmashOverlayGeneratorMk2.General;
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
        private string resourceType;
        private bool isError;
        private bool isAutoUpdate;
        private ArrayList templates;
        private ArrayList casterTemplates;
        private Resources _resources = new Resources();
        
        private ISOGControlService service = null;

        //This will define the connection to the service
        private Connection conn = null;

        delegate void SetTextCallback(string text, bool err);

        Assembly myAssembly = Assembly.GetExecutingAssembly();  
        
        #endregion Data Fields

        /* GETTERS AND SETTERS */
        #region Getters and Setters

        [DataMember]
        public bool IsError
        {
            get { return this.isError; }
            set { this.isError = value; }
        }

        [DataMember]
        public string TemplateFile
        {
            get{ return this.templateFile; }
            set{ this.templateFile = value; }
        }

        [DataMember]
        public string NewTemplateFile
        {
            get { return this.newTemplateFile; }
            set { this.newTemplateFile = value; }
        }

        [DataMember]
        public string Competitor1
        {
            get { return this.competitor1; }
            set { this.competitor1 = value; }
        }

        [DataMember]
        public string Competitor2
        {
            get { return this.competitor2; }
            set { this.competitor2 = value; }
        }

        [DataMember]
        public string Score1
        {
            get { return this.score1; }
            set { this.score1 = value; }
        }

        [DataMember]
        public string Score2
        {
            get { return this.score2; }
            set { this.score2 = value; }
        }

        [DataMember]
        public string Caster1
        {
            get { return this.caster1; }
            set { this.caster1 = value; } 
        }

        [DataMember]
        public string Caster2
        {
            get { return this.caster2; }
            set { this.caster2 = value; }
        }

        [DataMember]
        public string Caster1Twitter
        {
            get { return this.caster1Twitter; }
            set { this.caster1Twitter = value; }
        }

        [DataMember]
        public string Caster2Twitter
        {
            get { return this.caster2Twitter; }
            set { this.caster2Twitter = value; }             
        }

        [DataMember]
        public string CasterTemplateFile
        {
            get { return this.casterTemplateFile; }
            set { this.casterTemplateFile = value; }
        }

        [DataMember]
        public string CasterTemplateFileName
        {
            get { return this.casterTemplateFileName; }
            set { this.casterTemplateFileName = value; }
        }

        [DataMember]
        public string TournamentName
        {
            get { return this.tournamentName; }
            set { this.tournamentName = value; }
        }

        [DataMember]
        public string TournamentRound
        {
            get { return this.tournamentRound; }
            set { this.tournamentRound = value; }
        }

        [DataMember]
        public string GameType
        {
            get { return this.gameType; }
            set { this.gameType = value; }
        }

        [DataMember]
        public string TemplateFileName
        {
            get { return this.templateFileName; }
            set { this.templateFileName = value; } 
        }

        [DataMember]
        public string MatchupPicFile
        {
            get { return this.matchupPicFile; }
            set { this.matchupPicFile = value; }
        }

        [DataMember]
        public string MatchupPicFileName
        {
            get { return this.matchupPicFileName; }
            set { this.matchupPicFileName = value; }
        }

        [DataMember]
        public string ResourceType
        {
            get { return this.resourceType; }
            set { this.resourceType = value; }
        }

        [DataMember]
        public bool IsAutoUpdate
        {
            get { return this.isAutoUpdate; }
            set { this.isAutoUpdate = value;}
        }

        [DataMember]
        public ArrayList Templates
        {
            get { return this.templates; }
            set { this.templates = value; }
        }

        [DataMember]
        public ArrayList CasterTemplates
        {
            get { return this.casterTemplates; }
            set { this.casterTemplates = value; }
        }

        [DataMember]
        public Connection Conn
        {
            get { return this.conn; }
            set { this.conn = value; }
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

            debugCompetitorTemplate(resourceType, filePath);
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

            
            debugCasterTemplate(filePath);
            logToUser("Completed without error...", false);
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
            TournamentRound = tournamentRoundCombobox.Text;
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

            GenFcns.populateListView(myAssembly, templateListView, "Templates");
            GenFcns.populateListView(myAssembly, casterTemplateListView, "Caster");
            GenFcns.populateListView(myAssembly, matchupPicListView, "Matchup");

            try
            {
                Thread thread = new Thread(()=> {
                        EndpointAddress address = new EndpointAddress(new Uri("http://192.168.0.5:8081/SOGCS.svc"));
                        //NetTcpBinding binding = new NetTcpBinding();
                        //EndpointAddress address = new EndpointAddress(new Uri("net.tcp://192.168.0.5/SOGCS.svc"));
                        WSHttpBinding binding = new WSHttpBinding();
                        ChannelFactory<ISOGControlService> fac = new ChannelFactory<ISOGControlService>(binding, address);

                        service = fac.CreateChannel();
                        //MessageBox.Show("Connection channel successfully made!");
                        logToUser("Connection Channel successfully made!", false);
                        makeConnection();
                    }
                );
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

        private void matchupPicListView_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                string resource = (string)matchupPicListView.Items[matchupPicListView.SelectedIndex];
                MatchupPicFile = getImageResourcePath("matchup", resource);
                MatchupPicFileName = resource;

                string imageFilePath = MatchupPicFile;
                changePictureInBox(matchupPictureBox, imageFilePath);
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
            //return this.ProductName + ".Images." + type + "." + resourceName;

            if (type.Equals("template"))
            {
                return this.ProductName + ".Images.Templates." + resourceName;
            }
            else if (type.Equals("caster"))
            {
                return this.ProductName + ".Images.Caster." + resourceName;
            }
            else if (type.Equals("matchup"))
            {
                return this.ProductName + ".Images.Matchup." + resourceName;
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

        #region BackendOps

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

        private void incrementPlayer1()
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(singlesP1ScoreTextbox.Text);
                score++;
                singlesP1ScoreTextbox.Text = score.ToString();

                generate();
            }
        }


        private void decrementPlayer1()
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


        private void incrementPlayer2()
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(singlesP2ScoreTextbox.Text);
                score++;
                singlesP2ScoreTextbox.Text = score.ToString();

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
                    score--;
                    singlesP2ScoreTextbox.Text = score.ToString();

                    generate();
                }
            }
        }


        private void incrementTeam1()
        {
            if (dataFilledIn())
            {
                int score = Int32.Parse(doublesT1ScoreTextbox.Text);
                score++;
                doublesT1ScoreTextbox.Text = score.ToString();

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
                    score--;
                    doublesT1ScoreTextbox.Text = score.ToString();

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
                    score--;
                    doublesT2ScoreTextbox.Text = score.ToString();

                    generate();
                }
            }
        }
        #endregion BackendOps

        #region Listeners
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

        private void generate()
        {
            try
            {
                /*
                //VERIFY TOURNAMENT AND ROUND ARE FILLED IN
                verifyTourneyDataFilled();
                //VERIFY THE TOURNAMENT ROUND IS FILLED
                verifyTournamentRoundFilled();
                //VERIFY THE COMBATANTS FIELDS
                verifyCombatantsFilled();
                 */

                if (!dataFilledIn())
                    return;
                

                //GET THE COMBATANTS' NAMES (SET TO THE GLOBAL VARIABLES)
                getCombatants(GameType);
                //VERIFY THE SCORES ARE IN CORRECT FORMAT
                ErrorHandler.checkScoreFormat(Score1, Score2);

                getTournamentData();

                string resource = TemplateFile.Contains(".Images.Templates.") ? "resource" : "file";

                //ALTER THE IMAGE WITH NEW INFORMATION
                paintCompetitorText(TemplateFile, resource);

                
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

        /*
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
        */

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

        public void makeWindowTest()
        {
            MessageBox.Show("It worked!");
        }
        
        #region ServiceMethods

        /** THIS METHOD WILL NEED TO RUN AN ASYNC THREAD IN THE BG **/
        public void refresh(){

            if (Conn != null)
            {
                TourneyData td = service.getRecentTourneyData(Conn);
                updateInfo(td);
            }
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
                int num = service.requestConnectionNumber();
                Conn = new Connection(num, this);
                bool success = service.addConnection(Conn);

                if (success) return true;
                else return false;
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

        public bool updateInfo(TourneyData td)
        {
            if (Conn != null)
            {
                try
                {
                    if (td.GameType.Equals("singles"))
                    {
                        TournamentRound = td.TourneyRound;
                        Competitor1 = td.Player1Name;
                        Competitor2 = td.Player2Name;
                        Score1 = td.Score1;
                        Score2 = td.Score2;

                        generate();
                    }
                    else
                    {
                        TournamentRound = td.TourneyRound;
                        Competitor1 = td.Player1Name + " & " + td.Player2Name;
                        Competitor2 = td.Player3Name + " & " + td.Player4Name;
                        Score1 = td.Score1;
                        Score2 = td.Score2;

                        generate();
                    }

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
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

        private void matchupPicListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void generateMatchupPicBtn_Click(object sender, EventArgs e)
        {

        }

        private void matchupPictureBox_Click(object sender, EventArgs e)
        {

        }

        

    }    
}