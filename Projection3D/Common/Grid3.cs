using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection3D.Common
{
    public class Grid3
    {
        #region Fields
        private RotationGrid2D oxy;
        private RotationGrid2D oyz;
        private RotationGrid2D ozx;

        private Vector3[] srcVerts;
        private Vector3[] rsltVerts;

        private Vector3 scale;
        private Vector3 eulerRad;
        private Vector3 pos;
        #endregion

        #region Funcs
        public Grid3()
        {
            oxy = new RotationGrid2D();
            oyz = new RotationGrid2D();
            ozx = new RotationGrid2D();

            scale = new Vector3(1);
        }

        private void updateScaleRotPos()
        {
            for (int i = 0; i < srcVerts.Length; i++)
            {
                // scale
                rsltVerts[i].x = srcVerts[i].x * scale.x;
                rsltVerts[i].y = srcVerts[i].y * scale.y;
                rsltVerts[i].z = srcVerts[i].z * scale.z;

                // rot
                Vector2 rslt;


                // around X axis
                oyz.AngleRad = eulerRad.x;
                rslt = oyz.Rotate(rsltVerts[i].y, rsltVerts[i].z);
                rsltVerts[i].y = rslt.x;
                rsltVerts[i].z = rslt.y;

                // around Y axis
                ozx.AngleRad = eulerRad.y;
                rslt = ozx.Rotate(rsltVerts[i].z, rsltVerts[i].x);
                rsltVerts[i].z = rslt.x;
                rsltVerts[i].x = rslt.y;

                // around Z axis
                oxy.AngleRad = eulerRad.z;
                rslt = oxy.Rotate(rsltVerts[i].x, rsltVerts[i].y);
                rsltVerts[i].x = rslt.x;
                rsltVerts[i].y = rslt.y;
                // pos
                rsltVerts[i] += pos;
            }
        }
        #endregion

        #region Props
        public Vector3 Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                updateScaleRotPos();
            }
        }
        public Vector3 EulerRad
        {
            get { return eulerRad; }
            set
            {
                eulerRad = value;
                updateScaleRotPos();
            }
        }
        public Vector3 Pos
        {
            get { return pos; }
            set
            {
                pos = value;
                updateScaleRotPos();
            }
        }

        public Vector3[] SrcVerts
        {
            get { return srcVerts; }
            set { srcVerts = value; rsltVerts = new Vector3[srcVerts.Length]; updateScaleRotPos(); }
        }
        public Vector3[] RsltVerts { get { return rsltVerts; } }
        #endregion

        #region classes
        private class RotationGrid2D
        {
            #region MyRegion
            private Vector2 basis;
            private Vector2 vert;
            private float angleRad;
            #endregion

            #region Funcs
            public RotationGrid2D()
            {
                basis = new Vector2(1, 0);
            }

            public Vector2 Rotate(float vertX, float vertY)
            {
                vert = vertX * basis + vertY * basis.Normal;

                return vert;
            }
            #endregion

            #region Props
            public float AngleRad
            {
                get { return angleRad; }
                set
                {
                    angleRad = value;
                    basis.SetAngle(angleRad);
                    basis.Normalize();
                    //basis.x = (float)Math.Cos(angleRad);
                    //basis.y = (float)Math.Sin(angleRad);
                }
            }
            #endregion
        }
        #endregion
    }
}
