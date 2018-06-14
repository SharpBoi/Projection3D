using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection3D.Common
{
    public struct Vector2
    {
        #region Statics
        public static PointF[] ToPoints(Vector2[] vectors)
        {
            PointF[] pnts = new PointF[vectors.Length];
            for (int i = 0; i < pnts.Length; i++)
                pnts[i] = vectors[i].PointF;

            return pnts;
        }
        #endregion

        #region Fields
        public float x;
        public float y;
        #endregion

        #region Funcs
        public Vector2(float X, float Y)
        {
            x = X;
            y = Y;
        }
        public Vector2(float Radian)
        {
            x = y = 0;
            SetAngle(Radian);
        }

        /// <summary>
        /// Нормализует этот вектор
        /// </summary>
        public Vector2 Normalize()
        {
            float mag = Magnitude;
            x /= mag;
            y /= mag;

            return this;
        }
        public void SetAngle(float radian)
        {
            float srcMag = Magnitude;
            x = (float)Math.Cos(radian) * srcMag;
            y = (float)Math.Sin(radian) * srcMag;
        }
        public void SetMagnitude(float value)
        {
            Vector2 dir = Normalized;
            dir *= value;
            x = dir.x;
            y = dir.y;
        }

        public override string ToString()
        {
            return x + "; " + y;
        }
        #endregion

        #region Props
        /// <summary>
        /// Возвращает или задает магнитуду этого вектора
        /// </summary>
        public float Magnitude { get { return (float)Math.Sqrt(x * x + y * y); } }

        /// <summary>
        /// Возвращает нормализованную копию этого вектора
        /// </summary>
        public Vector2 Normalized
        {
            get
            {
                float mag = Magnitude;
                return new Vector2(x / mag, y / mag);
            }
        }

        /// <summary>
        /// Возвращает нормаль к этому вектору
        /// </summary>
        public Vector2 Normal { get { return new Vector2(-y, x).Normalize(); } }

        /// <summary>
        /// Возвращает или задает угол этого вектора. Задача сохраняет магнитуду
        /// </summary>
        public float AngleRad { get { return (float)Math.Atan2(y, x); } }

        public Point Point { get { return new Point((int)x, (int)y); } }
        public PointF PointF { get { return new PointF(x, y); } }
        #endregion

        #region Operators
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator *(float b, Vector2 a)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }
        #endregion
    }
}
