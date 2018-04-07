using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameOfLife.Models
{
    public class BWPicture
    {
        #region Fields

        private byte[] pixels;
        private int width;
        private int height;
        private const byte deadPixel = 0, alivePixel = 255;

        public int Width => width;
        public int Height => height;
        public Size Size => new Size(width, height);

        #endregion Variables

        #region Init

        public BWPicture(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixels = new byte[width * height];
            Array.Clear(pixels, 0, width * height);
        }

        #endregion Init

        #region Public methods

        public void GenerateRandomPicture()
        {
            Random rmd = new Random();

            for (int x = width / 2 - width / 5; x < width / 2 + width / 5; x++)
            {
                for (int y = height / 2 - height / 5; y < height / 2 + height / 5; y++)
                {
                    if (rmd.Next() % 2 == 0)
                        SetPixel(x, y, true);
                }
            }
        }

        public void GenerateDiamantPicture()
        {
            Point middle = new Point(width / 2, height / 2);

            SetPixel((int)middle.X - 1, (int)middle.Y + 1, true);
            SetPixel((int)middle.X, (int)middle.Y + 2, true);
            SetPixel((int)middle.X + 1, (int)middle.Y + 1, true);


            SetPixel((int)middle.X - 1, (int)middle.Y - 1, true);
            SetPixel((int)middle.X, (int)middle.Y - 2, true);
            SetPixel((int)middle.X + 1, (int)middle.Y - 1, true);


            SetPixel((int)middle.X - 2, (int)middle.Y, true);
            SetPixel((int)middle.X + 2, (int)middle.Y, true);
        }

        public BitmapSource GetImage => BitmapSource.Create(width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed8, BitmapPalettes.Gray256, pixels, width);

        #region Set position x/y

        public void SetPixel(int x, int y, bool alive)
        {
            pixels[width * y + x] = alive ? alivePixel : deadPixel;
        }

        public int GetAliveNeighbours(int x, int y)
        {
            int count = 0;
            int size = 1;

            for (int xi = x - size; xi <= x + size; xi++)
            {
                for (int yi = y - size; yi <= y + size; yi++)
                {
                    if (xi < 0 || xi >= width || yi < 0 || yi >= height)
                        continue;

                    if (xi == x && yi == y)
                        continue;

                    if (IsAlive(xi, yi))
                        count++;
                }
            }
            return count;
        }

        public bool IsAlive(int x, int y)
        {
            return pixels[width * y + x] == alivePixel;
        }

        #endregion Set position x/y

        //#region Set position px

        //public void SetDeadPixel(int px)
        //{
        //    pixels[px] = deadPixel;
        //}

        //public void SetAlivePixel(int px)
        //{
        //    pixels[px] = alivePixel;
        //}

        //#endregion Set position px

        #endregion Public methods
    }
}
