using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;
using SmashOverlayGeneratorMk2;

namespace SmashOverlayGeneratorMk2.Backend
{
    class PictureGen
    {
        public static void paintCasterText(string filePath,
                                           Assembly myAssembly)
        {
            SmashOverlayGenerator.logToUser("GENERATING...", false);

            Bitmap bMap = null;
            Stream imageStream = null;

            myAssembly = Assembly.GetExecutingAssembly();
            imageStream = myAssembly.GetManifestResourceStream(filePath);
            bMap = new Bitmap(imageStream);


            //debugCasterTemplate(filePath);
            //SmashOverlayGenerator.logToUser("Completed without error...", false);
        }

        public static int[] determineFontSize(string Competitor1, string Competitor2)
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

        public static void getSinglesCombatants(string Competitor1, string Competitor2,
                                         TextBox singlesP1Textbox, TextBox singlesP2Textbox,

                                         string Score1, string Score2,
                                         TextBox singlesP1ScoreTextbox, TextBox singlesP2ScoreTextbox)
        {
            Competitor1 = singlesP1Textbox.Text;
            Competitor2 = singlesP2Textbox.Text;
            Score1 = singlesP1ScoreTextbox.Text;
            Score2 = singlesP2ScoreTextbox.Text;
        }

        public static void getDoubleCombatants(TextBox doublesT1P1Textbox, TextBox doublesT1P2Textbox,
                                               TextBox doublesT2P1Textbox, TextBox doublesT2P2Textbox,
            
                                               string Competitor1, string Competitor2,
                                               string Score1, string Score2,
                                               TextBox doublesT1ScoreTextbox, TextBox doublesT2ScoreTextbox)
        {
            string team1 = doublesT1P1Textbox.Text + " & " + doublesT1P2Textbox.Text;
            string team2 = doublesT2P1Textbox.Text + " & " + doublesT2P2Textbox.Text;

            Competitor1 = team1;
            Competitor2 = team2;
            Score1 = doublesT1ScoreTextbox.Text;
            Score2 = doublesT2ScoreTextbox.Text;
        }

        public static void getCasters(string Caster1, string Caster2, 
                                TextBox caster1Textbox, TextBox caster2Textbox,
            
                                string Caster1Twitter, string Caster2Twitter,
                                TextBox caster1TwitterTextbox, TextBox caster2TwitterTextbox)
        {
            Caster1 = caster1Textbox.Text;
            Caster2 = caster2Textbox.Text;
            Caster1Twitter = caster1TwitterTextbox.Text;
            Caster2Twitter = caster2TwitterTextbox.Text;
        }

        public static void getTournamentData(string TournamentName, string TournamentRound,
                                       TextBox tournamentNameTextbox, ComboBox tournamentRoundCombobox)
        {
            TournamentName = tournamentNameTextbox.Text;
            TournamentRound = tournamentRoundCombobox.Text;
        }

        public static void createNewImageFile(string source, string destination)
        {
            //COPY IMAGE TO NEW FILE
            System.IO.File.Copy(source, destination, true);
        }
    }
}
