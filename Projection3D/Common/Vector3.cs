using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection3D.Common
{
    public struct Vector3
    {
        #region Fields
        public float x;
        public float y;
        public float z;
        #endregion

        #region Funcs
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(float x, float y) : this(x, y, 0) { }
        public Vector3(float value) : this(value, value, value) { }

        public void Normalize()
        {
            float mag = Magnitude;
            x /= mag;
            y /= mag;
            z /= mag;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }

        public override string ToString()
        {
            return x + "; " + y + "; " + z;
        }
        #endregion

        #region Props
        public float Magnitude { get { return (float)Math.Sqrt(x * x + y * y + z * z); } }
        public Vector3 Normalized
        {
            get
            {
                float mag = Magnitude;
                return new Vector3(x / mag, y / mag, z / mag);
            }
        }
        #endregion
    }
}
