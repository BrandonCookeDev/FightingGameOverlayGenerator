using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmashOverlayGeneratorMk2.Objects
{
    class DataPoint
    {
        private string data;
        private Point point;

        public DataPoint() { }

        public DataPoint(string data, Point point)
        {
            if (data == null || data.Length == 0)
                throw new Exception("Data Point: Data cannot be empty");
            if (point == null)
                throw new Exception("Data Point: Point must exist");

            this.data = data;
            this.point = point;
        }

        public string Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public Point Point
        {
            get { return this.point; }
            set { this.point = value; }
        }
    }
}
