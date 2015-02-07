using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SmashOverlayGeneratorMk2.Objects.Points
{
    class TemplatePoint
    {
        private Int32 x;
        private Int32 y;

        public TemplatePoint() { } 

        public TemplatePoint(Int32 x, Int32 y)
        {
            if (x < 0)
                throw new Exception("Point: X cannot be negative");
            if (y < 0)
                throw new Exception("Point: Y cannot be negative");

            this.x = x;
            this.y = y;
        }

        public Int32 X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public Int32 Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public Point getPoint()
        {
            return new Point(X, Y);
        }
    }
}
