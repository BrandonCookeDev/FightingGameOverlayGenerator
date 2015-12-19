using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace SmashOverlayGeneratorMk2
{
    public partial class OverlayDesignerForm : Form
    {
        private Point mouseLocation;
        private Image image;
        private Hashtable imageStorage;
        private bool squareCutOn = false;
        private bool freeCutOn = false;
        private bool isEditing = false;

        private Rectangle Rect = new Rectangle();

        public Hashtable ImageStorage
        {
            get { return this.imageStorage; }
            set { this.imageStorage = value; }
        }

        public Image GetImage
        {
            get { return this.image; }
            set { this.image = value; }
        }

        public Point MouseLocation
        {
            get { return this.mouseLocation; }
            set { this.mouseLocation = value; }
        }

        public bool IsEditing
        {
            get { return this.isEditing; }
            set { this.isEditing = value; }
        }

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
            #region DataGrid
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
            #endregion DataGrid
        }

        private void addToStorage(Point p, Color c)
        {
            ImageStorage.Add(p, c);
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

        #region Browse
        private void browsePicturesButton_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);
                browsePictureTextbox.Text = fileToOpen;
                Bitmap image = new Bitmap(Image.FromFile(fileToOpen));
                GetImage = image;

                changePictureInBox(picturePreviewBox, image);
            }
        }
        #endregion Browse

        #region CheckBoxButtons
        private void squareCutBtn_Click(object sender, EventArgs e)
        {
            if (FreeCutOn)
            {
                FreeCutOn = false;
                pointSelectBtn.Checked = false;
            }
            else
            {
                FreeCutOn = true;
                pointSelectBtn.Checked = true;
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
        #endregion CheckBoxButtons

        private void picturePreviewBox_Click(object sender, EventArgs e)
        {

        }

        #region MouseOverPictureBoxEvents
        public Point getCoordinates(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            return coordinates;
        }

        private void picturePreviewBox_PaintPerpendicularLines(object sender, PaintEventArgs e)
        {
            Point point = MouseLocation;
        }

        private void picturePreviewBox_MouseOver(object sender, EventArgs e)
        {
            MouseLocation = getCoordinates(sender, e);

            if (picturePreviewBox.Image != null)
            {
                using (Graphics g = Graphics.FromImage(picturePreviewBox.Image))
                {
                    //UNDRAW GREY LINE
                    //for (int i = 0; i < ImageStorage.Count; i++ )
                    //{
                        
                    //}
                    g.DrawImage(GetImage, 0, 0);

                    //DRAW GREY LINE

                    //DRAW LINE TO THE BOTTOM OF BOX
                    g.DrawLine(
                        new Pen(Color.Gray, 2f),
                        MouseLocation, new Point(MouseLocation.X, picturePreviewBox.Size.Height));

                    //DRAW LINE TO THE TOP OF BOX
                    g.DrawLine(
                        new Pen(Color.Gray, 2f),
                        MouseLocation, new Point(MouseLocation.X, 0));

                    //DRAW LINE TO THE RIGHT SIDE OF BOX
                    g.DrawLine(
                        new Pen(Color.Gray, 2f),
                        MouseLocation, new Point(picturePreviewBox.Size.Width, MouseLocation.Y));

                    //DRAW LINE TO THE LEFT SIDE OF THE BOX
                    g.DrawLine(
                        new Pen(Color.Gray, 2f),
                        MouseLocation, new Point(0, MouseLocation.Y));
                }
                picturePreviewBox.Invalidate();
            }
        }
        #endregion MouseOverPictureBoxEvents

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

        private void pointSelectBtn_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
