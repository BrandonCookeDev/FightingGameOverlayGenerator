using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;
using System.Reflection;
using SmashOverlayGeneratorMk2.General;
using SmashOverlayGeneratorMk2.Objects;
using SmashOverlayGeneratorMk2.Objects.Points;

namespace SmashOverlayGeneratorMk2.General
{
    class Database
    {
        private static string dataDir;
        private static string dbPath;
        private static SQLiteConnection conn;

        public static string CreateDatabase(string productName)
        {
            string retStr = "";
            dataDir = @"C:\OverlayGenerator\Data"; 
            dbPath  = @"C:\OverlayGenerator\Data\Generator.sqlite";

            if(!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            if (!File.Exists(dbPath))
            {
                string dbFile = ListBoxFcns.getResourcePath(productName, "database", "Generator.sqlite");
                if (dbFile != null)
                {
                    Stream dbStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(dbFile);
                    FileStream outStream = new FileStream(@"C:\OverlayGenerator\Data\Generator.sqlite", FileMode.Create);
                    for (int i = 0; i < dbStream.Length; i++)
                    {
                        outStream.WriteByte((byte)dbStream .ReadByte());
                    }
                }
                else
                {
                    SQLiteConnection.CreateFile(dbPath);
                    if (conn == null)
                        GetConnection();
                    bool create = CreateTables();
                    if (create) retStr += "DB Successfully Created\r\n";
                    else retStr += "DB Creation Failed\r\n";
                    bool initialInsert = InitialFillData();
                    if (initialInsert) retStr += "DB Successfully Filled\r\n";
                    else retStr += "DB Failed Initial Data Fill\r\n";              
                }                
            }
            
            if(conn == null)
                GetConnection();
            return retStr;
        }

        

        public static bool DeleteTableData()
        {
            string deleteTableDataQry =
                "DELETE FROM tbl_CompetitorTemplates";
            SQLiteCommand cmd = new SQLiteCommand(deleteTableDataQry, conn);
            try
            {
                DBOpen();
                cmd.ExecuteNonQuery();
                DBClose();
                return true;
            }
            catch (Exception ex)
            {
                DBClose();
                return false;
            }
        }

        public static void ShowAllFromDB()
        {
            string qry =
                "SELECT * FROM tbl_CompetitorTemplates;";
            SQLiteCommand cmd = new SQLiteCommand(qry, conn);
            try{
                DBOpen();
                SQLiteDataReader cur = cmd.ExecuteReader();
                while (cur.Read())
                {
                    object[] o = new object[16];
                    Object line = cur.GetValues(o);
                    string linestr = o[1].ToString();
                }
                DBClose();
            }
            catch{
                DBClose();
            }
        }

        public static CompetitorTemplate GetCompTemplate(string fileName)
        {
            string qry =
                "SELECT * FROM tbl_CompetitorTemplates WHERE FileName = '" + fileName + "';";
            SQLiteCommand cmd = new SQLiteCommand(qry, conn);
            try
            {
                DBOpen();
                SQLiteDataReader cur = cmd.ExecuteReader();
                while (cur.Read())
                {
                    string name = cur.GetString(0);
                    int tpx = cur.GetInt32(1);
                    int tpy = cur.GetInt32(2);
                    int rpx = cur.GetInt32(3);
                    int rpy = cur.GetInt32(4);
                    int p1px = cur.GetInt32(5);
                    int p1py = cur.GetInt32(6);
                    int p2px = cur.GetInt32(7);
                    int p2py = cur.GetInt32(8);
                    int p1cpx = cur.GetInt32(9);
                    int p1cpy = cur.GetInt32(10);
                    int p2cpx = cur.GetInt32(11);
                    int p2cpy = cur.GetInt32(12);
                    int p1spx = cur.GetInt32(13);
                    int p1spy = cur.GetInt32(14);
                    int p2spx = cur.GetInt32(15);
                    int p2spy = cur.GetInt32(16);
                    int dpx = cur.GetInt32(17);
                    int dpy = cur.GetInt32(18);
                    string opt = cur.GetString(19);

                    CompetitorPoint player1pt = new CompetitorPoint(p1px, p1py);
                    CompetitorPoint player2pt = new CompetitorPoint(p2px, p2py);
                    CompetitorPoint player1CamPt = new CompetitorPoint(p1cpx,p1cpy);
                    CompetitorPoint player2CamPt = new CompetitorPoint(p2cpx, p2cpy);
                    ScorePoint score1pt = new ScorePoint(p1spx, p1spy);
                    ScorePoint score2pt = new ScorePoint(p2spx, p2spy);
                    RoundPoint roundpt =  new RoundPoint(rpx, rpy);
                    TournamentPoint tourneypt = new TournamentPoint(tpx,tpy);
                    DatePoint datePoint = new DatePoint(dpx, dpy);

                    DBClose();
                    return new CompetitorTemplate(name, player1pt, player2pt, player1CamPt, player2CamPt, score1pt, score2pt, roundpt, tourneypt, datePoint, opt);
                }
                return null;

            }
            catch (Exception ex)
            {
                DBClose();
                return null;
            }
        }

        public static void GetConnection()
        {
            conn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
        }

        public static void DBOpen()
        {
            conn.Open();
        }

        public static void DBClose()
        {
            conn.Close();
        }

        public static void CopyDBToProjDir(string productName)
        {
            string dbFile = ListBoxFcns.getResourcePath(productName, "database", "Generator.sqlite");
            if (dbFile != null)
            {
                Stream outStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(dbFile);
                FileStream dbStream = new FileStream(@"C:\OverlayGenerator\Data\Generator.sqlite", FileMode.Open);
                for (int i = 0; i < dbStream.Length; i++)
                {
                    outStream.WriteByte((byte)dbStream.ReadByte());
                }
            }

            bool debug = false;
            if (debug)
            {
                if (Directory.Exists(dataDir))
                {
                    if (File.Exists(dbPath))
                        File.Copy(dbPath, @"C:\Users\BrandonADMIN\Documents\FightingGameOverlayGenerator\SmashOverlayGeneratorMk2\Generator.sqlite", true);
                }
            }
        }

        #region HardcodedGeneration
        public static bool InitialFillData()
        {
            string competitorInsert =
                "INSERT INTO tbl_CompetitorTemplates " +
                "(FileName,TournamentPointX,TournamentPointY,RoundPointX,RoundPointY,Player1PointX,Player1PointY," +
                "Player2PointX, Player2PointY, Player1CamPointX, Player1CamPointY, Player2CamPointX, Player2CamPointY," +
                "Player1ScorePointX, Player1ScorePointY, Player2ScorePointX, Player2ScorePointY, DatePointX, DatePointY, Options)";
            string opt1 = "'nameFormat:Center," +
                    "scoreFormat:Center," +
                    "roundFormat:Center," +
                    "player1Format:Near," +
                    "player2Format:Far," +
                    "tourneyFormat:Far," +
                    "dateFormat:Near," +
                    "Color:White,'";
            string opt2 = "'nameFormat:Center," +
                    "scoreFormat:Center," +
                    "roundFormat:Center," +
                    "player1Format:Center," +
                    "player2Format:Center," +
                    "tourneyFormat:Center," +
                    "dateFormat:Center," +
                    "Color:White,'";


            string initialFillQry1 =
                competitorInsert + " VALUES " +
                "('FireOverlayNew.png',900,25,0,0,501,1063,976,1063,1686,364,1686,730,680,1030,800,1030, 0, 0, " + opt2 + ");";
            string initialFillQry2 =
                competitorInsert + " VALUES " +
                "('FlashbackOverlayRedux.png',1340,1033,685,45,100,35,1270,35,1635,265,1635,568,580,45,790,45,30,1033," + opt1 + ");";
            string initialFillQry3 =
                competitorInsert + " VALUES " +
                "('FlashbackOverlayRedux2.png',1340,1033,685,45,100,35,1270,35,1635,265,1635,568,580,45,790,45,30,1033," + opt1 + ");";
            SQLiteCommand cmd1 = new SQLiteCommand(initialFillQry1, conn);
            SQLiteCommand cmd2 = new SQLiteCommand(initialFillQry2, conn);
            SQLiteCommand cmd3 = new SQLiteCommand(initialFillQry3, conn);

            DBOpen();
            try
            {
                cmd1.ExecuteNonQuery();
            }
            catch (Exception e) { string msg = e.Message; }

            try
            {
                cmd2.ExecuteNonQuery();
            }
            catch (Exception e) { string msg = e.Message; }

            try
            {
                cmd3.ExecuteNonQuery();
            }
            catch (Exception e) { string msg = e.Message; }
            DBClose();
            return true;
        }

        public static bool CreateTables()
        {
            string createCompTblQry =
                "CREATE TABLE IF NOT EXISTS tbl_CompetitorTemplates(" +
                "FileName varchar(400) PRIMARY KEY," +
                "TournamentPointX int, TournamentPointY int," +
                "RoundPointX int, RoundPointY int," +
                "Player1PointX int, Player1PointY int," +
                "Player2PointX int, Player2PointY int," +
                "Player1CamPointX int, Player1CamPointY int," +
                "Player2CamPointX int, Player2CamPointY int," +
                "Player1ScorePointX int, Player1ScorePointY int," +
                "Player2ScorePointX int, Player2ScorePointY int," +
                "DatePointX int, DatePointY int, Options varchar(400))";
            SQLiteCommand cmd = new SQLiteCommand(createCompTblQry, conn);
            try
            {
                DBOpen();
                cmd.ExecuteNonQuery();
                DBClose();
                return true;
            }
            catch (Exception e)
            {
                DBClose();
                return false;
            }
        }
        #endregion HardcodedGeneration
    }
}
