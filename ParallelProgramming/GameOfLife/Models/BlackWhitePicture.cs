using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameOfLife.Models
{
    public class BlackWhitePicture
    {
        #region Fields

        private byte[] pixels;
        private int width;
        private int height;

        public int Width => width;
        public int Height => height;
        public Size Size => new Size(width, height);

        #endregion Variables

        #region Init

        public BlackWhitePicture(int width, int height)
        {
            this.width = width;
            this.height = height;
            pixels = new byte[width * height];
        }

        #endregion Init

        #region Public methods

        public void GenerateRandomPicture()
        {
            Random rdm = new Random();
            rdm.NextBytes(pixels);
        }

        public BitmapSource GetImage()
        {
            return BitmapSource.Create(width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed8, BitmapPalettes.Gray256, pixels, height);
        }

        #region Get/Set position x/y

        public void GetPixel(int x, int y, out byte bw)
        {
            bw = pixels[width * y + x];
        }

        public byte GetPixelAvg(int x, int y)
        {
            return pixels[width * y + x];
        }

        public void SetPixel(int x, int y, byte bw)
        {
            pixels[width * y + x] = bw;
        }

        #endregion Get/Set position x/y

        #region Get/Set position px
        
        public void GetPixel(int px, out byte bw)
        {
            bw = pixels[px];
        }

        public void SetPixel(int px, byte bw)
        {
            pixels[px] = bw;
        }

        #endregion Get/Set position px

        #endregion Public methods
    }
}
