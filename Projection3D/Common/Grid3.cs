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
        private Vector3 prevEulerRad;
        private Vector3 eulerRad;
        private Vector3 pos;

        private Vector2 rotResult;
        private RotConf latestRotConf = RotConf.xyz;
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
                // SCALE
                rsltVerts[i].x = srcVerts[i].x * scale.x;
                rsltVerts[i].y = srcVerts[i].y * scale.y;
                rsltVerts[i].z = srcVerts[i].z * scale.z;


                // ROTATION
                // NEW
                if (eulerRad.x != prevEulerRad.x)
                    latestRotConf = RotConf.xyz;
                else if (eulerRad.y != prevEulerRad.y)
                    latestRotConf = RotConf.yxz;
                else if (eulerRad.z != prevEulerRad.z)
                    latestRotConf = RotConf.zxy;

                rotAroundX(i, eulerRad.x);
                rotAroundY(i, eulerRad.y);
                rotAroundZ(i, eulerRad.z);

                //if (latestRotConf == RotConf.xyz)
                //{
                //    rotAroundX(i, eulerRad.x);
                //    rotAroundY(i, eulerRad.y);
                //    rotAroundZ(i, eulerRad.z);
                //}
                //else if (latestRotConf == RotConf.yxz)
                //{
                //    rotAroundX(i, eulerRad.x);
                //    rotAroundZ(i, eulerRad.z);
                //    rotAroundY(i, eulerRad.y);
                //}
                //else if (latestRotConf == RotConf.zxy)
                //{
                //    rotAroundZ(i, eulerRad.z);
                //    rotAroundX(i, eulerRad.x);
                //    rotAroundY(i, eulerRad.y);
                //}

                // OLD(work well)
                //Vector2 rslt;

                //// around X axis
                //oyz.AngleRad = eulerRad.x;
                //rslt = oyz.Rotate(rsltVerts[i].y, rsltVerts[i].z);
                //rsltVerts[i].y = rslt.x;
                //rsltVerts[i].z = rslt.y;

                //// around Y axis
                //ozx.AngleRad = eulerRad.y;
                //rslt = ozx.Rotate(rsltVerts[i].z, rsltVerts[i].x);
                //rsltVerts[i].z = rslt.x;
                //rsltVerts[i].x = rslt.y;

                //// around Z axis
                //oxy.AngleRad = eulerRad.z;
                //rslt = oxy.Rotate(rsltVerts[i].x, rsltVerts[i].y);
                //rsltVerts[i].x = rslt.x;
                //rsltVerts[i].y = rslt.y;

                // POSITION
                rsltVerts[i] += pos;
            }
        }
        private void rotAroundX(int vertexIndex, float angleRad)
        {
            oyz.AngleRad = angleRad;
            rotResult = oyz.Rotate(rsltVerts[vertexIndex].y, rsltVerts[vertexIndex].z);
            rsltVerts[vertexIndex].y = rotResult.x;
            rsltVerts[vertexIndex].z = rotResult.y;
        }
        private void rotAroundY(int vertexIndex, float angleRad)
        {
            ozx.AngleRad = angleRad;
            rotResult = ozx.Rotate(rsltVerts[vertexIndex].z, rsltVerts[vertexIndex].x);
            rsltVerts[vertexIndex].z = rotResult.x;
            rsltVerts[vertexIndex].x = rotResult.y;
        }
        private void rotAroundZ(int vertexIndex, float angleRad)
        {
            oxy.AngleRad = angleRad;
            rotResult = oxy.Rotate(rsltVerts[vertexIndex].x, rsltVerts[vertexIndex].y);
            rsltVerts[vertexIndex].x = rotResult.x;
            rsltVerts[vertexIndex].y = rotResult.y;
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
                prevEulerRad = eulerRad;
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
        private enum RotConf { xyz, yxz, zxy }

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
            public Vector2 Basis { get { return basis; } }
            #endregion
        }
        #endregion
    }
}
