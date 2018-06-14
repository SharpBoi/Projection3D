using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projection3D
{
    class Canvas : Panel
    {
        public Canvas() : base()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;

            BackColor = Color.White;
        }

        protected override void OnSizeChanged(EventArgs e)
        { 
            Refresh();
            Update();
        }
    }
}
