using Projection3D.Common;
using Projection3D.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projection3D.Render
{
    public class Render3D : Render2D
    {
        public event Action<Graphics> OnRender;

        private List<Grid3> grids = new List<Grid3>();
        private List<Mesh> meshes = new List<Mesh>();

        private Random rnd = new Random();

        public Render3D(Form canvas, int width, int height) : base(canvas, width, height)
        {
            
        }

        Pen pen = new Pen(Color.Black);
        Brush brush = new SolidBrush(Color.Red);
        protected override void Render(Graphics g)
        {
            for (int gi = 0; gi < grids.Count; gi++)
            {
                Grid3 grid = grids[gi];
                Mesh mesh = meshes[gi];

                Vector3 v0, v1, v2;
                PointF[] poly = new PointF[3];

                for(int i = 2; i < mesh.Inds.Length; i += 3)
                {
                    v0 = grid.RsltVerts[mesh.Inds[i - 0]];
                    v1 = grid.RsltVerts[mesh.Inds[i - 1]];
                    v2 = grid.RsltVerts[mesh.Inds[i - 2]];

                    /*деление - угол обзора
                     * прибавление - дистанция от вершины до камеры
                     */
                    if (v0.z != 0) v0 *= (-v0.z / 100 + 2);
                    if (v1.z != 0) v1 *= (-v1.z / 100 + 2);
                    if (v2.z != 0) v2 *= (-v2.z / 100 + 2);

                    // wireframe
                    g.DrawLine(pen, v0.x, v0.y, v1.x, v1.y);
                    g.DrawLine(pen, v1.x, v1.y, v2.x, v2.y);
                    g.DrawLine(pen, v2.x, v2.y, v0.x, v0.y);

                    // solid mode
                    poly[0].X = v0.x;
                    poly[0].Y = v0.y;

                    poly[1].X = v1.x;
                    poly[1].Y = v1.y;

                    poly[2].X = v2.x;
                    poly[2].Y = v2.y;

                    int clr = (byte)(((float)i / mesh.Inds.Length) * 255) + 1;
                    if (clr > 255) clr = 255;
                    brush = new SolidBrush(Color.FromArgb(clr, clr, 255));

                    g.FillPolygon(brush, poly);
                }
            }
        }

        public void AddToRender(Grid3 grid, Mesh mesh)
        {
            grids.Add(grid);
            meshes.Add(mesh);
        }
    }
}
