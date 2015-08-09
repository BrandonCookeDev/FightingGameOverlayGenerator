using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;
using SmashOverlayGeneratorMk2;
using System.IO;

namespace SmashOverlayGeneratorMk2.General
{
    class GenFcns
    {

        private static bool debug = true;
        
        #region General
        public static void createDataFile(SmashOverlayGenerator form)
        {
            

            string file = form.overlayDirectory + "\\overlay.temp";
            StringBuilder sb = new StringBuilder(
                "TournamentName:"+form.TournamentName);
            sb.Append("|TournamentRound:" + form.TournamentRound);
            sb.Append("|GameType:" + form.GameType);
            sb.Append("|Competitor1:" + form.Competitor1);
            sb.Append("|Score1:" + form.Score1);
            sb.Append("|Competitor2:" + form.Competitor2);            
            sb.Append("|Score2:" + form.Score2);
            sb.Append("|NameSwap:" + form.NameSwap);
            sb.Append("|TemplateFileName:" + form.TemplateFileName);

            if (debug)
            {
                sb.Append("|Matchup1:" + form.MatchupCompetitor1);
                sb.Append("|MatchupChar1:" + form.MatchupCharacter1);
                sb.Append("|Matchup2:" + form.MatchupCompetitor2);
                sb.Append("|MatchupChar2:" + form.MatchupCharacter2);
                sb.Append("|MatchupPic:" + form.MatchupPicFileName);
            }

            if (!sb.ToString().Contains("null"))
            {
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
                File.WriteAllText(file, sb.ToString(), Encoding.Default);
            }
        }

        public static bool loadDataFile(SmashOverlayGenerator form)
        {
            string file = form.overlayDirectory + "\\overlay.temp";
            string content = "";
            foreach (string s in (File.ReadAllLines(file)))
            {
                content += s;
            }
            string[] titles = content.Split('|');
            foreach (string s in titles)
            {
                string[] data = s.Split(':');
                switch (data[0])
                {
                    case "TournamentName":
                        form.TournamentName = data[1];
                        break;
                    case "TournamentRound":
                        form.TournamentRound = data[1];
                        break;
                    case "GameType":
                        form.GameType = data[1];
                        break;
                    case "Competitor1":
                        form.Competitor1 = data[1];
                        break;
                    case "Competitor2":
                        form.Competitor2 = data[1];
                        break;
                    case "Score1":
                        form.Score1 = data[1];
                        break;
                    case "Score2":
                        form.Score2 = data[1];
                        break;
                    case "NameSwap":
                        form.NameSwap = Boolean.Parse(data[1]);
                        break;
                    case "TemplateFileName":
                        form.TemplateFileName = data[1];
                        break;
                    case "Matchup1":
                        form.MatchupCompetitor1 = data[1];
                        break;
                    case "Matchup2":
                        form.MatchupCompetitor2 = data[1];
                        break;
                    case "MatchupChar1":
                        form.MatchupCharacter1File = data[1];
                        break;
                    case "MatchupChar2":
                        form.MatchupCharacter2File = data[1];
                        break;
                    case "MatchupPic":
                        form.MatchupPicFileName = data[1];
                        break;
                }
            }

            return false;
        }

        public static bool isNullOrEmpty(string s)
        {
            return s == null || s.Equals("");
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

        private void createNewImageFile(string source, string destination)
        {
            //COPY IMAGE TO NEW FILE
            System.IO.File.Copy(source, destination, true);
        }

        public static void putCursorInBox(TextBox box)
        {
            box.Focus();
            box.Select(0, 0);
        }

        public static bool isEmpty(TextBox str)
        {
            return str.Text.Equals("");
        }

        public static bool checkIfDigits(string s)
        {
            foreach (char c in s)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        public static void installFonts(SmashOverlayGenerator form, string fontName)
        {
            // Try install the font
            string fontPath = ListBoxFcns.getResourcePath(form.ProductName, "font", fontName+".TTF".ToUpper());

            var result = AddFontResource(fontPath);
            var error = Marshal.GetLastWin32Error();
            if (error != 0)
            {
                throw new Exception(error.ToString());
            }
        }
        #endregion General 
    }
}
