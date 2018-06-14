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
    public class Render2D
    {
        #region Fields
        private Form canvas;
        private Bitmap renderTexture;
        private Graphics gViewport;
        private Graphics gRender;
        
        private Thread renderThread;

        #endregion

        #region Funcs
        public Render2D(Form canvas, int width, int height)
        {
            renderThread = new Thread(render);
            renderThread.Priority = ThreadPriority.Highest;

            this.canvas = canvas;

            canvas.Width = width;
            canvas.Height = height;

            canvas.Refresh();
            gViewport = canvas.CreateGraphics();
            initRenderTexture(width, height);
        }

        public void StartRender()
        {
            IsRendering = true;
            renderThread.Start();
        }
        public void StopRender()
        {
            IsRendering = false;
        }
        
        private void initRenderTexture(int width, int height)
        {
            renderTexture = new Bitmap(width, height);
            gRender = Graphics.FromImage(renderTexture);
            gRender.TranslateTransform(renderTexture.Width / 2, renderTexture.Height / 2);

            gRender.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            gRender.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            gRender.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            gRender.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            gRender.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
        }

        private void render()
        {
            Thread.Sleep(10);
            while(IsRendering)
            {
                gRender.Clear(Color.White);

                // TODO : render stuff
                //gRender.DrawRectangle(new Pen(Color.Red), 0, 0, renderTexture.Width-1, renderTexture.Height-1);
                //gRender.DrawLine(new Pen(Color.Red), 0, 0, renderTexture.Width, renderTexture.Height);
                Render(gRender);

                gViewport.DrawImage(renderTexture, 0, 0);

                Thread.Sleep(1);
            }
        }
        protected virtual void Render(Graphics graph) { }
        #endregion

        #region Props
        public bool IsRendering { get; private set; }
        #endregion
    }
}
