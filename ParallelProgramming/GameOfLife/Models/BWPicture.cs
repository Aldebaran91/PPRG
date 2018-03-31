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
        private const byte deadPixel = 255, alivePixel = 0;

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
        }

        #endregion Init

        #region Public methods

        public void GenerateRandomPicture()
        {
            Random rdm = new Random();
            rdm.NextBytes(pixels);
        }

        public BitmapSource GetImage => BitmapSource.Create(width, height, 96, 96, System.Windows.Media.PixelFormats.Indexed8, BitmapPalettes.Gray256, pixels, width);
        
        #region Set position x/y
        
        public void SetDeadPixel(int x, int y)
        {
            pixels[width * y + x] = deadPixel;
        }

        public void SetAlivePixel(int x, int y)
        {
            pixels[width * y + x] = alivePixel;
        }

        #endregion Set position x/y

        #region Set position px
        
        public void SetDeadPixel(int px)
        {
            pixels[px] = deadPixel;
        }

        public void SetAlivePixel(int px)
        {
            pixels[px] = alivePixel;
        }

        #endregion Set position px

        #endregion Public methods
    }
}
