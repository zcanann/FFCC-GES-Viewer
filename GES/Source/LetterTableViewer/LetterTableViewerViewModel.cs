namespace GES.Source.LetterTableViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.CraftListViewer;
    using GES.Source.Docking;
    using GES.Source.Main;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using static GES.Source.Main.MainViewModel;

    /// <summary>
    /// View model for the Letter Table Visualizer.
    /// </summary>
    public class LetterTableViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="LetterTableViewerViewModel" /> class.
        /// </summary>
        private static readonly LetterTableViewerViewModel Instance = new LetterTableViewerViewModel();

        private UInt64 LetterDataTableAddressEN = 0x0099CB80;
        private UInt64 LetterDataTableAddressJP = 0x009B84C0;
        private UInt64 LetterDataTableAddressPAL = 0x0099DDC0;

        private UInt64 LetterNameTableAddressEN = 0x00977E40;
        private UInt64 LetterNameTableAddressJP = 0x00994FC0;
        private UInt64 LetterNameTableAddressPAL = 0x00979000;

        private UInt64 LetterContentsTableAddressEN = 0x00985080;
        private UInt64 LetterContentsTableAddressJP = 0x009A1240;
        private UInt64 LetterContentsTableAddressPAL = 0x009862C0;

        private Int32 LetterNameTableSizeEN = 6265;
        private Int32 LetterNameTableSizeJP = 6440;
        private Int32 LetterNameTableSizePAL = 6306;

        private Int32 LetterContentsTableSizeEN = 91822;
        private Int32 LetterContentsTableSizeJP = 94804;
        private Int32 LetterContentsTableSizePAL = 92242;

        /// <summary>
        /// Prevents a default instance of the <see cref="LetterTableViewerViewModel" /> class from being created.
        /// </summary>
        private LetterTableViewerViewModel() : base("Letter List")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        public LetterTableDataView LetterTable { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        private Byte[] CachedRawLetterDataTableBytes { get; set; }

        private Byte[] CachedRawLetterNameTableBytes { get; set; }

        private Byte[] CachedRawLetterContentsTableBytes { get; set; }

        private Byte[] RawLetterDataTableBytes { get; set; }

        private Byte[] RawLetterNameTableBytes { get; set; }

        private Byte[] RawLetterContentsTableBytes { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="LetterTableViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static LetterTableViewerViewModel GetInstance()
        {
            return LetterTableViewerViewModel.Instance;
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
                                    UpdateLetterTable();
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

        private unsafe void UpdateLetterTable()
        {
            UInt64 gameCubeMemoryBase = MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GC", EmulatorType.Dolphin);

            UInt64 letterDataTableAddress;
            UInt64 letterNameTableAddress;
            UInt64 letterContentsTableAddress;
            Int32 letterNameTableSize;
            Int32 letterContentsTableSize;
            Int32 contentsSkipCount;

            switch (MainViewModel.GetInstance().DetectedVersion)
            {
                default: return;
                case EDetectedVersion.JP:
                    letterDataTableAddress = LetterDataTableAddressJP;
                    letterNameTableAddress = LetterNameTableAddressJP;
                    letterContentsTableAddress = LetterContentsTableAddressJP;
                    letterNameTableSize = LetterNameTableSizeJP;
                    letterContentsTableSize = LetterContentsTableSizeJP;
                    contentsSkipCount = 16;
                    break;
                case EDetectedVersion.EN:
                    letterDataTableAddress = LetterDataTableAddressEN;
                    letterNameTableAddress = LetterNameTableAddressEN;
                    letterContentsTableAddress = LetterContentsTableAddressEN;
                    letterNameTableSize = LetterNameTableSizeEN;
                    letterContentsTableSize = LetterContentsTableSizeEN;
                    contentsSkipCount = 16;
                    break;
                case EDetectedVersion.PAL:
                    letterDataTableAddress = LetterDataTableAddressPAL;
                    letterNameTableAddress = LetterNameTableAddressPAL;
                    letterContentsTableAddress = LetterContentsTableAddressPAL;
                    letterNameTableSize = LetterNameTableSizePAL;
                    letterContentsTableSize = LetterContentsTableSizePAL;
                    contentsSkipCount = 16;
                    break;
            }

            UInt64 LetterDataTablePointer = gameCubeMemoryBase + letterDataTableAddress;
            UInt64 LetterNameTablePointer = gameCubeMemoryBase + letterNameTableAddress;
            UInt64 LetterContentsTablePointer = gameCubeMemoryBase + letterContentsTableAddress;

            if (this.RawLetterDataTableBytes == null)
            {
                this.RawLetterDataTableBytes = new Byte[typeof(LetterDataTableSerializable).StructLayoutAttribute.Size];
                this.CachedRawLetterDataTableBytes = new Byte[typeof(LetterDataTableSerializable).StructLayoutAttribute.Size];
            }
            if (this.RawLetterNameTableBytes == null)
            {
                this.RawLetterNameTableBytes = new Byte[letterNameTableSize];
                this.CachedRawLetterNameTableBytes = new Byte[letterNameTableSize];
            }
            if (this.RawLetterContentsTableBytes == null)
            {
                this.RawLetterContentsTableBytes = new Byte[letterContentsTableSize];
                this.CachedRawLetterContentsTableBytes = new Byte[letterContentsTableSize];
            }

            // Read the entire actor reference counting table
            Boolean success;
            MemoryReader.Instance.ReadBytes(
                SessionManager.Session.OpenedProcess,
                this.RawLetterDataTableBytes,
                LetterDataTablePointer,
                out success);

            if (!success)
            {
                return;
            }

            MemoryReader.Instance.ReadBytes(
                SessionManager.Session.OpenedProcess,
                this.RawLetterNameTableBytes,
                LetterNameTablePointer,
                out success);

            if (!success)
            {
                return;
            }

            MemoryReader.Instance.ReadBytes(
                SessionManager.Session.OpenedProcess,
                this.RawLetterContentsTableBytes,
                LetterContentsTablePointer,
                out success);

            if (!success)
            {
                return;
            }

            if (this.LetterTable == null)
            {
                this.LetterTable = new LetterTableDataView(new LetterTableData());
            }

            LetterTableData.Deserialize(this.LetterTable.LetterTableData, this.RawLetterDataTableBytes);

            // Notify changes if new bytes differ from cached
            if (!this.RawLetterDataTableBytes.SequenceEqual(this.CachedRawLetterDataTableBytes)
                || !this.RawLetterNameTableBytes.SequenceEqual(this.CachedRawLetterNameTableBytes)
                || !this.RawLetterContentsTableBytes.SequenceEqual(this.CachedRawLetterContentsTableBytes))
            {
                this.LetterTable.LetterTableData.Refresh(this.RawLetterDataTableBytes, this.RawLetterNameTableBytes, this.RawLetterContentsTableBytes, contentsSkipCount);
                this.LetterTable.RefreshAllProperties();
                this.RaisePropertyChanged(nameof(this.LetterTable));

                CraftListViewerViewModel.GetInstance().ExternalRefreshAll();

                this.RawLetterDataTableBytes.CopyTo(this.CachedRawLetterDataTableBytes, 0);
            }
        }
    }
    //// End class
}
//// End namespace