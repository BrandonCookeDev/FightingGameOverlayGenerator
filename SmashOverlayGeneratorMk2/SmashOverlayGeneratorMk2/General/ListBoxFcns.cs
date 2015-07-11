using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace SmashOverlayGeneratorMk2.General
{
    class ListBoxFcns
    {
        public static void showPicturePreviewForm(Image image)
        {
            PicturePreviewForm picturePreviewForm = new PicturePreviewForm(image);
            picturePreviewForm.Location = new Point(500, 100);
            picturePreviewForm.Show();
        }

        public static void changePictureInBox(Assembly myAssembly, PictureBox box, string resourcePath)
        {
            Stream imageStream = myAssembly.GetManifestResourceStream(resourcePath);
            Bitmap image = new Bitmap(imageStream);

            box.SizeMode = PictureBoxSizeMode.Zoom;
            box.Image = image;
            imageStream.Close();
        }

        public static string getImageResourcePath(string productName, string type, string resourceName)
        {
            //return this.ProductName + ".Images." + type + "." + resourceName;

            if (type.Equals("template"))
            {
                return productName + ".Images.Templates." + resourceName;
            }
            else if (type.Equals("caster"))
            {
                return productName + ".Images.Caster." + resourceName;
            }
            else if (type.Equals("matchup"))
            {
                return productName + ".Images.Matchup." + resourceName;
            }
            else
                throw new Exception("String type agrument can only be 'template' or 'caster'");

        }

        public static void populateListView(Assembly myAssembly, ListBox listBox, string suffix)
        {
            ArrayList temp = new ArrayList();
            string[] resources = myAssembly.GetManifestResourceNames();

            foreach (string resource in resources)
            {
                if (resource.Contains("." + suffix))
                {
                    string resourceName = resource.Substring(resource.LastIndexOf("." + suffix + ".") + (suffix.Length + 2));
                    temp.Add(resource);
                    listBox.Items.Add(resourceName);
                }
            }
        }

    }
}
