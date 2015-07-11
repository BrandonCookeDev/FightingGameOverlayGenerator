using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmashOverlayGeneratorMk2.BMVersion
{
    public class BMVersion
    {
        /** GLOBAL **/
        public static bool _BMMode = false;


        /** GETTERS AND SETTERS **/
        
        public static bool BM
        {
            get { return _BMMode; }
            set { _BMMode = value; }
        }
        
    }
}
