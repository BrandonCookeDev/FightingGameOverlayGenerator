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
    public partial class PicturePreviewForm : Form
    {
        public PicturePreviewForm()
        {
            InitializeComponent();

        }

        public PicturePreviewForm(Image image)
        {
            InitializeComponent();
            Size newSize = new Size(image.Width / 2, image.Height / 2);
            Image newImage = new Bitmap(image, newSize);

            this.Width = newImage.Width;
            this.Height = newImage.Height;
            this.MaximizeBox = false;

            PictureBox box = new PictureBox();
            box.Width = newImage.Width;
            box.Height = newImage.Height;
            box.Image = newImage;

            this.Controls.Add(box);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(500, 500);
            this.Focus();
            this.BringToFront();
        }

        private void PicturePreviewForm_Load(object sender, EventArgs e)
        {

        }

        private void PicturePreviewForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
