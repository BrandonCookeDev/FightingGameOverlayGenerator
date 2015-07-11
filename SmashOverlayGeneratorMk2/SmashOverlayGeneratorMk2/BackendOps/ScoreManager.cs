using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmashOverlayGeneratorMk2.BackendOps
{
    class ScoreManager
    {
        public static void decrementPlayer(Int32 score, TextBox playerTextbox)
        {
            score--;
            playerTextbox.Text = score.ToString();
        }

        public static void incrementPlayer(TextBox playerTextbox)
        {
            int score = Int32.Parse(playerTextbox.Text);
            score++;
            playerTextbox.Text = score.ToString();
        }
    }
}
