using Projection3D.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection3D.Entities
{
    class MeshGenerator
    {
        public static Mesh CreateQuad()
        {
            Mesh mesh = new Mesh();

            mesh.Verts = new Vector3[]
            {
                new Vector3(-1, -1, 0),
                new Vector3(1, -1, 0),
                new Vector3(1, 1, 0),
                new Vector3(-1, 1, 0)
            };
            mesh.Inds = new int[]
            {
                0,1,2,
                2,3,0
            };

            return mesh;
        }
        public static Mesh CreateCube()
        {
            Mesh mesh = new Mesh();

            mesh.Verts = new Vector3[]
            {
                new Vector3(-1, -1, -1),
                new Vector3(1, -1, -1),
                new Vector3(1, 1, -1),
                new Vector3(-1, 1, -1),

                new Vector3(-1, -1, 1),
                new Vector3(1, -1, 1),
                new Vector3(1, 1, 1),
                new Vector3(-1, 1, 1)
            };

            mesh.Inds = new int[]
            {
                // front
                0,1,2,
                2,3,0,

                // back
                4,5,6,
                6,7,4,

                // top
                3,2,6,
                6,7,3,

                // left
                0,4,7,
                7,3,0,

                // bottom
                0,1,5,
                5,4,0,

                //right
                1,5,6,
                6,2,1
            };

            return mesh;
        }
    }
}
