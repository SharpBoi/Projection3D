using Projection3D.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection3D.Entities
{
    public class Mesh
    {
        #region Fields
        private Vector3[] verts;
        private int[] inds;
        #endregion

        #region Funcs

        #endregion

        #region Props
        public Vector3[] Verts { get { return verts; } set { verts = value; } }
        public int[] Inds { get { return inds; } set { inds = value; } }
        #endregion
    }
}
