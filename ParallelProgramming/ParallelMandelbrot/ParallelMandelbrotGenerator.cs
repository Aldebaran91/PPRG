using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Media;

namespace ParallelMandelbrot
{
    public class ParallelMandelbrotGenerator
    {
        
        public Image Mandelbrot(bool useParallel, int width, int height, double realMin, double imagMin, double realMax, double imagMax, int MaxIterations, ref TimeSpan calcTime)
        {
            

            Color[,] ResultColors = new Color[width, height];

            ColorTable colTable = new ColorTable(MaxIterations);


            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (useParallel)
            {
                //parallel
                Parallel.For(0, width * height, delegate (int i)
                {
                    int ix = i % width;
                    int iy = (int)(i / height);
                    int colIndex = calcPixValue(ix, iy, realMin, imagMin, realMax, imagMax, width, height, MaxIterations);
                    ResultColors[ix, iy] = colTable.getColor(colIndex);
                });
            }
            else
            {
                //sequential
                for(int i = 0; i < width * height; i++)
                {
                    int ix = i % width;
                    int iy = (int)(i / height);
                    int colIndex = calcPixValue(ix, iy, realMin, imagMin, realMax, imagMax, width, height, MaxIterations);
                    ResultColors[ix, iy] = colTable.getColor(colIndex);
                }
            }

            sw.Stop();
            calcTime = sw.Elapsed;



            return ResultFromColorMatrix(ResultColors);
        }

        unsafe Bitmap ResultFromColorMatrix(Color[,] ColorData)
        {
            int width = ColorData.GetLength(1);
            int height = ColorData.GetLength(0);

            Bitmap Image = new Bitmap(width, height);
            BitmapData bitmapData = Image.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb
            );
            byte* Scan0 = (byte*)bitmapData.Scan0.ToPointer();


            for (int i = 0; i < width * height * 4; i+= 4)
            {
                int ix = (int)(i/4) % width;
                int iy = (int)((int)i/4 / height);
                Scan0[i + 0] = ColorData[ix, iy].B;
                Scan0[i + 1] = ColorData[ix, iy].G;
                Scan0[i + 2] = ColorData[ix, iy].R;
                Scan0[i + 3] = ColorData[ix, iy].A;
            }

            Image.UnlockBits(bitmapData);
            return Image;
        }

        int calcPixValue(int px, int py, double realMin, double imagMin, double realMax, double imagMax, int width, int height, int MaxIterations)
        {
            double cX = 0;
            double cY = 0;

           normalizeToViewRectangle(px, py, realMin, imagMin, realMax, imagMax, width, height, ref cX, ref cY);

            double zX = cX;
            double zY = cY;

            for(int n = 0; n < MaxIterations; n++)
            {
                double x = (zX * zX - zY * zY) + cX;
                double y = (zY * zX + zX * zY) + cY;
                if ((x*x + y*y) > 4)
                {
                    return n;
                }
                zX = x;
                zY = y;
            }
            return 0;

        }

        void normalizeToViewRectangle(double pX, double pY, double realMin, double imagMin, double realMax, double imagMax, double width, double height, ref double cX, ref double cY)
        {
            cX = realMin + pX * ((realMax - realMin) / width);
            cY = imagMin + pY * ((imagMin - imagMax) / height);
        }

    }
}
