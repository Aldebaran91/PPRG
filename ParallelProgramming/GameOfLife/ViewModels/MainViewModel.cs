using GameOfLife.Helper;
using GameOfLife.Models;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameOfLife.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static MainViewModel instance;
        public static MainViewModel GetInstance => instance ?? (instance = new MainViewModel());

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public MainViewModel()
        {
            Width = Height = 200;
        }

        #region Fields

        private bool running;
        private int width, height, picsRendered;
        private ICommand buttonCommand;
        private BWPicture bwPicture;
        private DateTime lastDate = DateTime.Now;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task updateFPS, demo;

        #endregion

        #region Properties

        public ICommand ButtonCommand => buttonCommand ?? (buttonCommand = new CommandHandler(() => StartStopButton()));

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    NotifyPropertyChanged(nameof(Width));
                }
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    NotifyPropertyChanged(nameof(Height));
                }
            }
        }

        public bool Running
        {
            get
            {
                return running;
            }
            set
            {
                if (running != value)
                {
                    running = value;
                    NotifyPropertyChanged(nameof(Running));
                }
            }
        }

        #endregion

        #region Methods

        public void StartStopButton()
        {
            if (Running)
            {
                cts.Cancel();
                demo?.Wait();
                updateFPS?.Wait();

                picsRendered = 0;
                NotifyPropertyChanged(nameof(FPS));
                cts = new CancellationTokenSource();

                Running = false;
            }
            else
            {
                var pic = new BWPicture(Width, Height);
                pic.GenerateRandomPicture();
                Picture = pic;

                updateFPS = Task.Run(() =>
                {
                    while (!cts.IsCancellationRequested)
                    {
                        NotifyPropertyChanged(nameof(FPS));
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }, cts.Token);

                demo = Task.Run(() =>
                {
                    Random rdm = new Random();
                    while (!cts.IsCancellationRequested)
                    {
                        picsRendered += rdm.Next() % 10;
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    }
                }, cts.Token);

                Running = true;
            }
        }

        public BWPicture Picture
        {
            get
            {
                return bwPicture;
            }
            set
            {
                bwPicture = value;
                NotifyPropertyChanged(nameof(Picture));
                picsRendered++;
            }
        }

        public int FPS
        {
            get
            {
                try
                {
                    return picsRendered;
                }
                finally
                {
                    picsRendered = 0;
                }
            }
        }

        #endregion
    }
}
