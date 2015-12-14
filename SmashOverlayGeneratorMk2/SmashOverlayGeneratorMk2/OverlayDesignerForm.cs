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
        private bool squareCutOn = false;
        private bool freeCutOn = false;

        private Rectangle Rect = new Rectangle();

        public bool SquareCutOn
        {
            get { return this.squareCutOn; }
            set { this.squareCutOn = value; }
        }

        public bool FreeCutOn
        {
            get { return this.freeCutOn; }
            set { this.freeCutOn = value; }
        }

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

        private void squareCutBtn_Click(object sender, EventArgs e)
        {
            if (FreeCutOn)
            {
                FreeCutOn = false;
                freeSelectBtn.Checked = false;
            }
            else
            {
                FreeCutOn = true;
                freeSelectBtn.Checked = true;
            }            
        }

        private void squareSelectBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (SquareCutOn)
            {
                SquareCutOn = false;
                squareSelectBtn.Checked = false;
            }
            else 
            { 
                SquareCutOn = true; 
                squareSelectBtn.Checked = true; 
            }
        }

        private void picturePreviewBox_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (SquareCutOn || FreeCutOn)
            {
                // Determine the initial rectangle coordinates...
                RectStartPoint = e.Location;
                Invalidate();
            }
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (SquareCutOn)
            {
                if (e.Button != MouseButtons.Left)
                    return;
                Point tempEndPoint = e.Location;
                Rect.Location = new Point(
                    Math.Min(RectStartPoint.X, tempEndPoint.X),
                    Math.Min(RectStartPoint.Y, tempEndPoint.Y));
                Rect.Size = new Size(
                    Math.Abs(RectStartPoint.X - tempEndPoint.X),
                    Math.Abs(RectStartPoint.Y - tempEndPoint.Y));
                picturePreviewBox.Invalidate();
            }
        }

        public Point RectStartPoint { get; set; }
    }
}
