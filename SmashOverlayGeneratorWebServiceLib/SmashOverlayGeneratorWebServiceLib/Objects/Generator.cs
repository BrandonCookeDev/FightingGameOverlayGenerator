using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SmashOverlayGeneratorWebServiceLib.Objects
{
    [DataContract]
    public class Generator
    {
        private bool active;
        private string ip;

        [DataMember]
        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        [DataMember]
        public string IP
        {
            get { return this.ip; }
            set { this.ip = value; }
        }
    }
}
