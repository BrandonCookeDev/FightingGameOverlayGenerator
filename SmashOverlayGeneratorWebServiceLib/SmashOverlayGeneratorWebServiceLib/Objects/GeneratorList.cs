using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SmashOverlayGeneratorWebServiceLib.Objects
{
    [DataContract]
    class GeneratorList
    {
        private List<Generator> activeList;

        [DataMember]
        public List<Generator> ActiveList
        {
            get { return this.activeList; }
            set { this.activeList = value; }
        }

        public bool addToList(Generator g)
        {
            bool success = false;
            int iCount = ActiveList.Count;
            List<Generator> temp = ActiveList;
            temp.Add(g);
            ActiveList = temp;

            if (ActiveList.Count == iCount + 1)
                success = true;
            
            //MAY NEED TO THROW ERROR ON ELSE

            return success;
        }

        public bool removeFromList(Generator g)
        {
            bool success = false;
            int iCount = ActiveList.Count;
            List<Generator> temp = ActiveList;
            temp.Remove(g);
            ActiveList = temp;

            if (ActiveList.Count == iCount - 1)
                success = true;

            //MAY NEED TO THROW ERROR ON ELSE

            return success;
        }
    }
}
