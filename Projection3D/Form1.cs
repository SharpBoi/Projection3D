using Projection3D.Common;
using Projection3D.Entities;
using Projection3D.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projection3D {
    public partial class Form1 : Form {
        Render3D renderer;

        Grid3 grid;
        Mesh mesh;

        List<Grid3> grids = new List<Grid3>();

        Vector3 euler;

        Keys pressedKey;
        float tick = 0;

        public Form1 () {
            DoubleBuffered = true;
            InitializeComponent();

            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            Timer t = new Timer();
            t.Tick += T_Tick;
            t.Interval = 1;
            t.Start();

            // init model
            mesh = new Mesh();
            mesh = MeshGenerator.CreateCube();

            grid = new Grid3();
            grid.SrcVerts = mesh.Verts;
            grid.Scale = new Vector3(50 * 1, 50, 50);


            renderer = new Render3D(this, 1920 / 1, 1080 / 1);
            renderer.StartRender();

            renderer.AddToRender(grid, mesh);
            for (int i = 0; i < 10; i++) {
                Mesh m = MeshGenerator.CreateCube();

                Grid3 g = new Grid3();
                g.SrcVerts = m.Verts;
                g.Scale = new Vector3(25, 25, 25);
                g.Pos = new Vector3((i - 5) * 40, 0, i * 10);
                grids.Add(g);

                renderer.AddToRender(g, m);
            }
        }

        private void Form1_KeyUp (object sender, KeyEventArgs e) {
            pressedKey = Keys.None;
        }

        private void Form1_KeyDown (object sender, KeyEventArgs e) {
            pressedKey = e.KeyCode;
        }

        private void T_Tick (object sender, EventArgs e) {
            tick += 0.01f;

            if (pressedKey == Keys.X)
                euler.x += 0.02f;
            if (pressedKey == Keys.Y)
                euler.y += 0.02f;
            if (pressedKey == Keys.Z)
                euler.z += 0.02f;

            for (int i = 0; i < grids.Count; i++) {
                //grids[i].EulerRad += new Vector3(0.01f * i, 0, 0);
                Vector3 pos = grids[i].Pos;
                pos.z = (float)Math.Sin(tick * i) * 100;
                grids[i].Pos = pos;

                grids[i].EulerRad = euler;
            }

            //euler.x += 0.05f;
            //euler.y += 0.02f;
            //euler.z += 0.02f;

            grid.EulerRad = euler;
            //grid.Pos += new Vector3(0, 0, 1);
        }

        private void Form1_FormClosing (object sender, FormClosingEventArgs e) {
            renderer.StopRender();
        }
    }
}
