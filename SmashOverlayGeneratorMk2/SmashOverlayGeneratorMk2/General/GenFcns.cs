using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SmashOverlayGeneratorMk2.General
{
    class GenFcns
    {
        #region General
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
        #endregion General 
    }
}
