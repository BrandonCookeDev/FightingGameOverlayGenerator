using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SmashOverlayGeneratorMk2.General;

namespace SmashOverlayGeneratorMk2.Objects
{
    abstract class Template
    {
        private string parentPath = @"C:\\OverlayGenerator\\OverlayImages";

        private string filePath;

        public Template() { }
        public Template(string filePath)
        {
            if (filePath == null || filePath.Length == 0)
                throw new Exception("Template: File Path missing");

            this.filePath = filePath;
        }

        public string ParentPath
        {
            get { return this.parentPath; }
            set { this.parentPath = value; }
        }

        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }

        public Bitmap getImage()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream imageStream = myAssembly.GetManifestResourceStream(FilePath);
            Bitmap image = new Bitmap(imageStream);

            imageStream.Close();
            return image;
        }

        public Bitmap getImage(string _filePath)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream imageStream = myAssembly.GetManifestResourceStream(_filePath);
            Bitmap image = new Bitmap(imageStream);

            imageStream.Close();
            return image;

        }

        public abstract Bitmap drawTextOnImage(SmashOverlayGenerator form);

        public abstract void saveImage(Bitmap image);
    }
}
