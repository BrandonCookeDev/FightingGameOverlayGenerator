using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;
using SmashOverlayGeneratorMk2;
using SmashOverlayGeneratorWebServiceLib.Objects;

namespace SmashOverlayGeneratorWebServiceLib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [DataContract]
    public class SOGControlService : ISOGControlService
    {

        private Hashtable map = new Hashtable();
        public Hashtable Map
        {
            get { return this.map; }
            set { this.map = value; }
        }

        private ArrayList connectionList = new ArrayList();
        public ArrayList ConnectionList
        {
            get { return connectionList; }
            set { connectionList = value; }
        }
        //private SmashOverlayGenerator sog;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public bool addConnection(Connection c)
        {
            try{
                if (c.ConnNumber == 0 && c.SOG == null)                
                    throw new Exception("Bad Connection");
                
                ArrayList temp = ConnectionList;
                temp.Add(c);
                ConnectionList = temp;

                mapConnection(c);
                return true;
            }
            catch(Exception e){
                return false;
            }
        }

        public bool removeConnection(Connection c)
        {
            try
            {
                ArrayList temp = ConnectionList;
                temp.Remove(c);
                ConnectionList = temp;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void pingConnections()
        {
            ArrayList temp = ConnectionList;
            foreach(Connection conn in temp)
            {
                if (!(conn.ping()))
                {
                    removeConnection(conn);
                    
                }
            }
            
        }

        public int requestConnectionNumber()
        {
            int count = ConnectionList.Count;
            count++;
            return count;            
        }

        public bool updateScore(int connNum, string score1, string score2)
        {
            try
            {
                foreach(Connection conn in ConnectionList)
                {
                    if (conn.ConnNumber == connNum)
                        conn.SOG.makeWindowTest();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool updateInfo(Connection conn, TourneyData td)
        {
            try
            {
                Map[conn] = td;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool authenticate(string uName, string password)
        {
            

            return false;
        }


        /** NON-INTERFACE METHODS **/
        public void mapConnection(Connection c)
        {
            TourneyData td = new TourneyData();
            Map.Add(c, td);
        }

        public void updateConnMap(Connection c, TourneyData td)
        {
            TourneyData old = (TourneyData)Map[c];
            if (old != td)
            {
                Map[c] = td;
            }
        }

        public TourneyData getRecentTourneyData(Connection c)
        {
            TourneyData td = (TourneyData)Map[c];
            return td;
        }
    }
}
