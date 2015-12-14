using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmashOverlayGeneratorMk2
{
    public partial class OverlayDesignerForm : Form
    {

        delegate void SetTextCallback(string text, bool err);

        public OverlayDesignerForm()
        {
            InitializeComponent();
            populateDataGrid();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < overlayDesignerDataGrid.Rows.Count; i++)
                {
                    overlayDesignerDataGrid.Rows[i].Cells[1].Value = "";
                }
                this.logToUser("...Data Cleared", false);
            }
            catch (Exception ex)
            {
                logToUser(ex.Message, true);
            }
        }

        private void populateDataGrid()
        {
            overlayDesignerDataGrid.Rows.Add("FileName", "");
            overlayDesignerDataGrid.Rows.Add("TournamentPointX", "");
            overlayDesignerDataGrid.Rows.Add("TournamentPointY", "");
            overlayDesignerDataGrid.Rows.Add("RoundPointX", "");
            overlayDesignerDataGrid.Rows.Add("RoundPointY", "");
            overlayDesignerDataGrid.Rows.Add("Player1PointX", "");
            overlayDesignerDataGrid.Rows.Add("Player1PointY", "");
            overlayDesignerDataGrid.Rows.Add("Player2PointX", "");
            overlayDesignerDataGrid.Rows.Add("Player2PointY", "");
            overlayDesignerDataGrid.Rows.Add("Player1CamPointX", "");
            overlayDesignerDataGrid.Rows.Add("Player1CamPointY", "");
            overlayDesignerDataGrid.Rows.Add("Player2CamPointX", "");
            overlayDesignerDataGrid.Rows.Add("Player2CamPointY", "");
            overlayDesignerDataGrid.Rows.Add("Player1ScorePointX", "");
            overlayDesignerDataGrid.Rows.Add("Player1ScorePointY", "");
            overlayDesignerDataGrid.Rows.Add("Player2ScorePointX", "");
            overlayDesignerDataGrid.Rows.Add("Player2ScorePointY", "");
            overlayDesignerDataGrid.Rows.Add("DatePointX", "");
            overlayDesignerDataGrid.Rows.Add("DatePointY", "");
            overlayDesignerDataGrid.Rows.Add("nameFormat", "");
            overlayDesignerDataGrid.Rows.Add("scoreFormat", "");
            overlayDesignerDataGrid.Rows.Add("roundFormat", "");
            overlayDesignerDataGrid.Rows.Add("player1Format", "");
            overlayDesignerDataGrid.Rows.Add("player2Format", "");
            overlayDesignerDataGrid.Rows.Add("tourneyFormat", "");
            overlayDesignerDataGrid.Rows.Add("dateFormat", "");
            overlayDesignerDataGrid.Rows.Add("Color", "");
        }

        public static void changePictureInBox(PictureBox box, Bitmap image)
        {
            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.Image = image;
        }

        private void logToUser(string msg, bool err)
        {
            if (err)
            {
                designerLogLabel.ForeColor = Color.Red;
            }
            else
            {
                designerLogLabel.ForeColor = Color.Navy;
            }
            if (designerLogLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(logToUser);
                designerLogLabel.Invoke(d, new object[] { msg, err });
            }
            else
            {
                designerLogLabel.Text = "..." + msg;
            }
        }

        private void browsePicturesButton_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
                browsePictureTextbox.Text = fileToOpen;
                Bitmap image = new Bitmap(Image.FromFile(fileToOpen));
                changePictureInBox(picturePreviewBox, image);
            }
        }
    }
}
