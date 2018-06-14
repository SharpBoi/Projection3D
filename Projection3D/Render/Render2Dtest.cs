using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projection3D.Render
{
    public class Render2Dtest
    {
        #region Fields
        private Control canvas;
        private Graphics gViewport;

        private Task viewportThread;
        private List<Task> renderThreads = new List<Task>();
        private List<Bitmap> renderTextures = new List<Bitmap>();
        private List<Graphics> gRenders = new List<Graphics>();
        #endregion

        #region Funcs
        public Render2Dtest(Form canvas, int width, int height, int renderThreadsCount = 1)
        {
            this.canvas = canvas;
            //canvas.Width = width;
            //canvas.Height = height;
            gViewport = canvas.CreateGraphics();

            int rtHeight = height / renderThreadsCount;
            for (int i = 0; i < renderThreadsCount; i++)
            {
                Bitmap rt = new Bitmap(width, rtHeight);
                rt.Tag = i;
                Graphics rg = Graphics.FromImage(rt);
                renderTextures.Add(rt);
                gRenders.Add(rg);

                int tid = i;
                renderThreads.Add(new Task(() => render(tid)));
            }

            viewportThread = new Task(renderViewport);
        }

        public void StartRender()
        {
            for (int i = 0; i < renderThreads.Count; i++)
                renderThreads[i].Start();
            //viewportThread.Start();
            IsRendering = true;
        }
        public void StopRender()
        {
            IsRendering = false;
        }

        private void render(int tid)
        {
            while (IsRendering)
            {
                // clear render textures
                //for (int i = 0; i < gRenders.Count; i++)
                {
                    gRenders[tid].Clear(Color.White);

                    Render(gRenders[tid]);
                }

                lock(viewportThread)
                gViewport.DrawImage(renderTextures[tid], 0, renderTextures[tid].Height * tid);

                Thread.Sleep(1);
            }
        }
        private void renderViewport()
        {
            while (IsRendering)
            {
                for (int i = 0; i < renderTextures.Count; i++)
                    gViewport.DrawImage(renderTextures[i], 0, renderTextures[i].Height * i);

                Thread.Sleep(1);
            }
        }
        protected virtual void Render(Graphics g) { }
        #endregion

        #region Props
        public bool IsRendering { get; private set; }
        #endregion
    }
}
