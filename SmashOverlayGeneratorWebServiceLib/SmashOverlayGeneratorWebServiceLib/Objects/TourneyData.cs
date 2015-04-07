using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SmashOverlayGeneratorWebServiceLib.Objects
{
    [DataContract]
    public class TourneyData
    {
        private string gameType;

        [DataMember]
        public string GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }
        private string tourneyName;
         
        [DataMember]
        public string TourneyName
        {
            get { return tourneyName; }
            set { tourneyName = value; }
        }
        private string tourneyRound;

        [DataMember]
        public string TourneyRound
        {
            get { return tourneyRound; }
            set { tourneyRound = value; }
        }
        private string player1Name;

        [DataMember]
        public string Player1Name
        {
            get { return player1Name; }
            set { player1Name = value; }
        }
        private string player2Name;

        [DataMember]
        public string Player2Name
        {
            get { return player2Name; }
            set { player2Name = value; }
        }
        private string player3Name;

        [DataMember]
        public string Player3Name
        {
            get { return player3Name; }
            set { player3Name = value; }
        }
        private string player4Name;

        [DataMember]
        public string Player4Name
        {
            get { return player4Name; }
            set { player4Name = value; }
        }
        private string team1Name;

        [DataMember]
        public string Team1Name
        {
            get { return team1Name; }
            set { team1Name = value; }
        }
        private string team2Name;

        [DataMember]
        public string Team2Name
        {
            get { return team2Name; }
            set { team2Name = value; }
        }
        private string score1;

        [DataMember]
        public string Score1
        {
            get { return score1; }
            set { score1 = value; }
        }
        private string score2;

        [DataMember]
        public string Score2
        {
            get { return score2; }
            set { score2 = value; }
        }

        //EMPTY CONSTRUCTOR
        public TourneyData() { }

        //Handles Singles
        public TourneyData(string gameType, string tourneyName, string tourneyRound,
                            string player1, string player2, string score1, string score2)
        {
            if (gameType.Length == 0) throw new Exception();
            //Handle Errors

            this.gameType = gameType;
            this.tourneyName = tourneyName;
            this.tourneyRound = tourneyRound;
            this.player1Name = player1;
            this.player2Name = player2;
            this.score1 = score1;
            this.score2 = score2;
        }

        //Handles Doubles
        public TourneyData(string gameType, string tourneyName, string tourneyRound,
                            string player1, string player2, string player3, string player4,
                            string score1, string score2)
        {
            //HANDLE ERRORS

            this.gameType = gameType;
            this.tourneyName = tourneyName;
            this.tourneyRound = tourneyRound;
            this.player1Name = player1;
            this.player2Name = player2;
            this.player3Name = player3;
            this.player4Name = player4;
            this.score1 = score1;
            this.score2 = score2;
        }
        public TourneyData(string gameType, string tourneyName, string tourneyRound,
                            string team1, string team2, string player1, string player2,
                            string player3, string player4, string score1, string score2)
        {
            //HANDLE ERRORS

            this.gameType = gameType;
            this.tourneyName = tourneyName;
            this.tourneyRound = tourneyRound;
            this.team1Name = team1;
            this.team2Name = team2;
            this.player1Name = player1;
            this.player2Name = player2;
            this.player3Name = player3;
            this.player4Name = player4;
            this.score1 = score1;
            this.score2 = score2;
        }
    }
}
