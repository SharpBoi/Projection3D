using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection3D.Common
{
    public class Grid2
    {
        #region Fields
        private Vector2[] srcVerts;
        private Vector2[] rsltVerts;

        private Vector2 pos;
        private float angleRad;
        private Vector2 scale;
        private Vector2 right;
        #endregion

        #region Funcs
        public Grid2()
        {
            right = new Vector2(1, 0);

            scale = new Vector2(1, 1);
        }

        private void updateVertsScaleRotPos()
        {
            for (int i = 0; i < rsltVerts.Length; i++)
            {
                // scale
                rsltVerts[i].x = srcVerts[i].x * scale.x;
                rsltVerts[i].y = srcVerts[i].y * scale.y;

                // rot
                //rsltVerts[i] = srcVerts[i].x * Right + srcVerts[i].y * Up;
                rsltVerts[i] = rsltVerts[i].x * right + rsltVerts[i].y * right.Normal;

                // pos
                rsltVerts[i] += pos;
            }
        }
        #endregion

        #region Props
        public Vector2 Pos
        {
            get { return pos; }
            set
            {
                pos = value;
                updateVertsScaleRotPos();
            }
        }
        public float AngleRad
        {
            get { return angleRad; }
            set
            {
                angleRad = value;

                right.SetAngle(angleRad);
                right.Normalize();

                updateVertsScaleRotPos();
            }
        }
        public Vector2 Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                updateVertsScaleRotPos();
            }
        }

        public Vector2 Right { get { return right; } }
        public Vector2 Up { get { return right.Normal; } }

        public Vector2[] SourceVerts { get { return srcVerts; } set { srcVerts = value; rsltVerts = new Vector2[value.Length]; updateVertsScaleRotPos(); } }
        public Vector2[] ResultVerts { get { return rsltVerts; } }
        #endregion
    }
}
