using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmashOverlayGeneratorMk2;
using SmashOverlayGeneratorWebServiceLib;
using System.Runtime.Serialization;

namespace SmashOverlayGeneratorWebServiceLib.Objects
{
    [DataContract]
    public class Connection
    {
        protected SOGControlService service;
        private int connNumber;
        private SmashOverlayGenerator sog;

        public Connection(int connNumber)
        {
            this.connNumber = connNumber;
        }

        public Connection(int connNumber, SmashOverlayGenerator sog)
        {
            if (connNumber < 0) throw new Exception();
            if (sog == null) throw new Exception();

            this.connNumber = connNumber;
            this.sog = sog;
        }

        [DataMember]
        public int ConnNumber
        {
            get { return this.connNumber; }
            set { this.connNumber = value; }
        }

        //[DataMember]
        //[KnownType(typeof(Form))]
        public SmashOverlayGenerator SOG
        {
            get { return this.sog; }
            set { this.sog = value; }
        }

        public bool initialConnection(Connection c)
        {
            /*
            try{
                SERVICE CALL
                if(Service.requestConnNumber()){
                    Service.addConn(c)
                    return true
                }
                else{
                    return false;
                }
            }catch(Exception e){
                return false;  
            }
             * */
            return false;
        }

        public bool ping()
        {
            return true;
        }

    }
}