using GameOfLife.Helper;
using GameOfLife.Models;
using System;
using System.Collections.Generic;
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
            Height = 200;
            Width = 200;
            CurrentMode = Modes[0];
        }

        #region Fields

        private bool running;
        private int width, height, picsRendered, generation;
        private ICommand buttonCommand;
        private BWPicture bwPicture;
        private GameOfLifeCore GoLInstance;
        private DateTime lastDate = DateTime.Now;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task updateFPS, demo;
        private string currentMode;

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

        public string CurrentMode
        {
            get
            {
                return currentMode;
            }
            set
            {
                if (currentMode != value)
                {
                    currentMode = value;
                    NotifyPropertyChanged(nameof(CurrentMode));
                }
            }
        }

        public List<string> Modes { get; set; } = new List<string>()
        {
            "Zufällig", "Diamant"
        };

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
                Generation = 0;

                switch (CurrentMode)
                {
                    case "Zufällig":
                        {
                            pic.GenerateRandomPicture();
                            break;
                        }
                    case "Diamant":
                        {
                            pic.GenerateDiamantPicture();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                GoLInstance = new GameOfLifeCore(pic);
                Picture = GoLInstance.CurrentGeneration;

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
                    bool first = true;
                    do
                    {
                        if (first)
                        {
                            GoLInstance.BeginnGeneration();
                            first = false;
                        }
                        else
                            GoLInstance.UpdateGeneration();
                        Picture = GoLInstance.CurrentGeneration;
                        //Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    }
                    while (!cts.IsCancellationRequested);

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
                Generation++;
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

        public int Generation
        {
            get
            {
                return generation;
            }
            set
            {
                generation = value;
                NotifyPropertyChanged(nameof(Generation));
            }
        }

        #endregion
    }
}
