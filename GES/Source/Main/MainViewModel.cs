namespace GES.Source.Main
{
    using GES.Engine.Common;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source.Docking;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using GES.Source.Output;
    using GES.Source.Updater;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using static GES.Source.Main.MainViewModel;

    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel : WindowHostViewModel
    {
        public enum EDetectedVersion
        {
            None,
            JP,
            EN,
            PAL,
        };

        public const String LanguageEN = "English (英語)";
        public const String LanguageJPN = "日本語 (Japanese)";

        private String selectedLanguage;

        /// <summary>
        /// Singleton instance of the <see cref="MainViewModel" /> class
        /// </summary>
        private static Lazy<MainViewModel> mainViewModelInstance = new Lazy<MainViewModel>(
                () => { return new MainViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="MainViewModel" /> class from being created.
        /// </summary>
        private MainViewModel() : base()
        {
            // Attach the logger view model to the engine's output
            Logger.Subscribe(OutputViewModel.GetInstance());
            Logger.Log(LogLevel.Info, "FFCC GES Tools started");

            ApplicationUpdater.UpdateApp();

            this.LanguageList = new List<String>
            {
                LanguageEN,
                LanguageJPN,
            };

            this.SelectedLanguage = LanguageList[LanguageList.Contains(GESSettings.SelectedLanguage) ? LanguageList.IndexOf(GESSettings.SelectedLanguage) : 0];
            this.RaisePropertyChanged(nameof(this.LanguageList));
            this.RaisePropertyChanged(nameof(this.SelectedLanguage));

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        public List<String> LanguageList { get; set; }

        public EDetectedVersion DetectedVersion { get; private set; }

        public String SelectedLanguage
        {
            get
            {
                return this.selectedLanguage;
            }

            set
            {
                this.selectedLanguage = value;
                GESSettings.SelectedLanguage = value;
                this.RaisePropertyChanged(nameof(this.SelectedLanguage));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }


        /// <summary>
        /// Default layout file for browsing cheats.
        /// </summary>
        protected override String DefaultLayoutResource
        {
            get
            {
                return "DefaultLayout.xml";
            }
        }

        /// <summary>
        /// The save file for the docking layout.
        /// </summary>
        protected override String LayoutSaveFile
        {
            get
            {
                return "Layout.xml";
            }
        }
        /// <summary>
        /// Begin the update loop for visualizing the heap.
        /// </summary>
        private void RunUpdateLoop()
        {
            this.CanUpdate = true;

            Task.Run(async () =>
            {
                while (this.CanUpdate)
                {
                    // if (this.IsVisible)
                    {
                        try
                        {
                            if (SessionManager.Session.OpenedProcess != null)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    this.DetectVersion();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(LogLevel.Error, "Error updating the Heap Visualizer", ex);
                        }
                    }

                    await Task.Delay(2500);
                }
            });
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <returns>The singleton instance of the <see cref="MainViewModel" /> class.</returns>
        public static MainViewModel GetInstance()
        {
            return mainViewModelInstance.Value;
        }

        /// <summary>
        /// Closes the main window.
        /// </summary>
        /// <param name="window">The window to close.</param>
        protected override void Close(Window window)
        {
            base.Close(window);
        }

        private Byte[] GameCode = new Byte[6];

        private void DetectVersion()
        {
            UInt64 gameCodeAddress = MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GC", EmulatorType.Dolphin);

            Boolean success;
            MemoryReader.Instance.ReadBytes(
                SessionManager.Session.OpenedProcess,
                this.GameCode,
                gameCodeAddress,
                out success);

            if (success)
            {
                const String GcVersionEn = "GCCE01";
                const String GcVersionJp = "GCCJGC";
                const String GcVersionPal = "GCCP01";

                String gbaGcVersion = Encoding.ASCII.GetString(this.GameCode);

                EDetectedVersion detectedVersion = EDetectedVersion.None;

                if (gbaGcVersion == GcVersionEn)
                {
                    detectedVersion = EDetectedVersion.EN;
                }
                else if (gbaGcVersion == GcVersionJp)
                {
                    detectedVersion = EDetectedVersion.JP;
                }
                else if (gbaGcVersion == GcVersionPal)
                {
                    detectedVersion = EDetectedVersion.PAL;
                }

                if (this.DetectedVersion != detectedVersion)
                {
                    this.DetectedVersion = detectedVersion;
                    this.RefreshAllViews();
                }
            }
            else
            {
                this.DetectedVersion = EDetectedVersion.None;
            }
        }

        private void RefreshAllViews()
        {
            EquipmentViewerViewModel.GetInstance().ExternalRefreshAll();
            InventoryViewerViewModel.GetInstance().ExternalRefreshAll();
        }
    }
    //// End class
}
//// End namesapce