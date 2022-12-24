namespace GES.Source.CraftViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.Docking;
    using GES.Source.Main;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// View model for the Craft Visualizer.
    /// </summary>
    public class CraftViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static CraftViewerViewModel actorReferenceCountVisualizerInstance = new CraftViewerViewModel();

        private const Int32 PlayerCount = 4;

        private UInt64 CraftListAddressEN = 0x38800;
        private UInt64 CraftListAddressJP = 0x32800;
        private UInt64 CraftListAddressPal = 0x3A800;

        /// <summary>
        /// Prevents a default instance of the <see cref="CraftViewerViewModel" /> class from being created.
        /// </summary>
        private CraftViewerViewModel() : base("Craft List Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.PlayerCraftData = new FullyObservableCollection<CraftDataView>();
            this.CachedPlayerSlotData = new Byte[PlayerCount][];

            for (int index = 0; index < PlayerCount; index++)
            {
                this.PlayerCraftData.Add(new CraftDataView(new CraftData()));
            }

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        /// <summary>
        /// Gets the list of actor reference count slots.
        /// </summary>
        public FullyObservableCollection<CraftDataView> PlayerCraftData { get; private set; }

        private Byte[][] CachedPlayerSlotData { get; set; }

        private Byte[] RawCraftData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="CraftViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static CraftViewerViewModel GetInstance()
        {
            return CraftViewerViewModel.actorReferenceCountVisualizerInstance;
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
                    if (this.IsVisible)
                    {
                        try
                        {
                            if (SessionManager.Session.OpenedProcess != null)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    UpdateActorSlots();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(LogLevel.Error, "Error updating the Heap Visualizer", ex);
                        }
                    }

                    await Task.Delay(200);
                }
            });
        }

        public void ExternalRefreshAll()
        {
            foreach(CraftDataView CraftDataView in this.PlayerCraftData)
            {
                CraftDataView.RefreshAllProperties();
            }
        }

        public void ExternalRefresh(Int32 playerIndex)
        {
            if (playerIndex >= 0 && playerIndex < this.PlayerCraftData.Count)
            {
                this.PlayerCraftData[playerIndex].RefreshAllProperties();
            }
        }

        private unsafe void UpdateActorSlots()
        {
            UInt64[] gbaCubeMemoryBases = new UInt64[PlayerCount]
            {
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_0", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_1", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_2", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_3", EmulatorType.Dolphin),
            };

            UInt64 CraftListAddress;
            
            switch(MainViewModel.GetInstance().SelectedVersion)
            {
                default:
                case MainViewModel.VersionJP: CraftListAddress = CraftListAddressJP; break;
                case MainViewModel.VersionEN: CraftListAddress = CraftListAddressEN; break;
                case MainViewModel.VersionPAL: CraftListAddress = CraftListAddressPal; break;
            }

            for (Int32 playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
            {
                UInt64 slotPointer = gbaCubeMemoryBases[playerIndex] + CraftListAddress;

                if (this.RawCraftData == null)
                {
                    this.RawCraftData = new Byte[typeof(CraftDataSerializable).StructLayoutAttribute.Size];
                }

                // Read the entire actor reference counting table
                Boolean success;
                MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    this.RawCraftData,
                    slotPointer,
                    out success);

                if (success)
                {
                    if (this.CachedPlayerSlotData[playerIndex] == null)
                    {
                        this.CachedPlayerSlotData[playerIndex] = new Byte[typeof(CraftDataSerializable).StructLayoutAttribute.Size];
                    }

                    if (this.PlayerCraftData[playerIndex].CraftData == null)
                    {
                        this.PlayerCraftData[playerIndex].CraftData = new CraftData();
                    }

                    CraftData.Deserialize(this.PlayerCraftData[playerIndex].CraftData, this.RawCraftData);

                    // Notify changes if new bytes differ from cached
                    if (!this.CachedPlayerSlotData[playerIndex].SequenceEqual(this.RawCraftData))
                    {
                        this.PlayerCraftData[playerIndex].CraftData.Refresh(this.RawCraftData, playerIndex);
                        this.PlayerCraftData[playerIndex].RefreshAllProperties();
                    }

                    this.RawCraftData.CopyTo(this.CachedPlayerSlotData[playerIndex], 0);
                }
            }
        }
    }
    //// End class
}
//// End namespace