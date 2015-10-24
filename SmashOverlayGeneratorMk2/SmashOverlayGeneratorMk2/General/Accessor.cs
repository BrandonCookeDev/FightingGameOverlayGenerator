using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmashOverlayGeneratorMk2.General
{
    class Accessor
    {
        private SmashOverlayGenerator form;

        public Accessor() { }
        public Accessor(SmashOverlayGenerator form)
        {
            if (form == null)
                throw new Exception("Form parameter cannot be null");
            this.form = form;
        }

        public SmashOverlayGenerator Form
        {
            get { return this.form; }
            set { this.form = value; }
        }
    }
}
