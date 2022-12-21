namespace GES.Source.Main
{
    using GES.Engine.Common.Logging;
    using GES.Source.Docking;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using GES.Source.Output;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows;

    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel : WindowHostViewModel
    {
        public const String VersionJP = "JP (日本語)";
        public const String VersionEN = "EN (英語)";
        public const String VersionPAL = "PAL (欧州)";

        public const String LanguageEN = "English (英語)";
        public const String LanguageJPN = "日本語 (Japanese)";

        private String selectedVersion;
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

            this.VersionList = new List<String>
            {
                VersionJP,
                VersionEN,
                VersionPAL,
            };
            this.LanguageList = new List<String>
            {
                LanguageEN,
                LanguageJPN,
            };
            this.SelectedVersion = VersionList[VersionList.Contains(GESSettings.SelectedVersion) ? VersionList.IndexOf(GESSettings.SelectedVersion) : 0];
            this.SelectedLanguage = LanguageList[LanguageList.Contains(GESSettings.SelectedLanguage) ? LanguageList.IndexOf(GESSettings.SelectedLanguage) : 0];
            this.RaisePropertyChanged(nameof(this.VersionList));
            this.RaisePropertyChanged(nameof(this.LanguageList));
            this.RaisePropertyChanged(nameof(this.SelectedVersion));
            this.RaisePropertyChanged(nameof(this.SelectedLanguage));
        }

        public List<String> VersionList { get; set; }
        public List<String> LanguageList { get; set; }

        public String SelectedVersion
        {
            get
            {
                return this.selectedVersion;
            }

            set
            {
                this.selectedVersion = value;
                GESSettings.SelectedVersion = value;
                this.RaisePropertyChanged(nameof(this.SelectedVersion));
            }
        }

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

                // Doesn't quite work.
                // EquipmentViewerViewModel.GetInstance().ExternalRefreshAll();
                // InventoryViewerViewModel.GetInstance().ExternalRefreshAll();
            }
        }


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
    }
    //// End class
}
//// End namesapce