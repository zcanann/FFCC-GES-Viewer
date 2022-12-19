namespace GES.Source.InventoryViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.Docking;
    using GES.Source.EquipmentViewer;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// View model for the Heap Visualizer.
    /// </summary>
    public class InventoryViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static InventoryViewerViewModel actorReferenceCountVisualizerInstance = new InventoryViewerViewModel();

        private const Int32 SlotCount = 8;

        private UInt64[] slotDataAddresses = new UInt64[SlotCount]
        {
            0x23BB90,
            0x23C7C0,
            0x23D3F0,
            0x23E020,
            0x23EC50,
            0x23F880,
            0x2404B0,
            0x2410E0,
        };

        /// <summary>
        /// Prevents a default instance of the <see cref="InventoryViewerViewModel" /> class from being created.
        /// </summary>
        private InventoryViewerViewModel() : base("Inventory Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.PlayerSlots = new FullyObservableCollection<PlayerSlotDataView>();
            this.CachedSlotData = new Byte[SlotCount][];

            for (int index = 0; index < SlotCount; index++)
            {
                this.PlayerSlots.Add(new PlayerSlotDataView(new PlayerSlotData()));
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
        public FullyObservableCollection<PlayerSlotDataView> PlayerSlots { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        private Byte[] RawPlayerSlotData { get; set; }
        private Byte[][] CachedSlotData { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="InventoryViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static InventoryViewerViewModel GetInstance()
        {
            return InventoryViewerViewModel.actorReferenceCountVisualizerInstance;
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

                    await Task.Delay(2500);
                }
            });
        }

        private unsafe void UpdateActorSlots()
        {
            UInt64 gameCubeMemoryBase = MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GC", EmulatorType.Dolphin);

            for (Int32 slotIndex = 0; slotIndex < SlotCount; slotIndex++)
            {
                UInt64 slotPointer = gameCubeMemoryBase + slotDataAddresses[slotIndex];

                if (this.RawPlayerSlotData == null)
                {
                    this.RawPlayerSlotData = new Byte[typeof(PlayerSlotData).StructLayoutAttribute.Size];
                }

                // Read the entire actor reference counting table
                Boolean success;
                MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    this.RawPlayerSlotData,
                    slotPointer,
                    out success);

                if (success)
                {
                    if (this.CachedSlotData[slotIndex] == null)
                    {
                        this.CachedSlotData[slotIndex] = new Byte[typeof(PlayerSlotData).StructLayoutAttribute.Size];
                    }

                    if (this.PlayerSlots[slotIndex].Slot == null)
                    {
                        this.PlayerSlots[slotIndex].Slot = new PlayerSlotData();
                    }

                    PlayerSlotData.Deserialize(this.PlayerSlots[slotIndex].Slot, this.RawPlayerSlotData);
                    this.PlayerSlots[slotIndex].Slot.Refresh(this.RawPlayerSlotData, slotIndex);

                    // Notify changes if new bytes differ from cached
                    if (!this.CachedSlotData[slotIndex].SequenceEqual(this.RawPlayerSlotData))
                    {
                        this.PlayerSlots[slotIndex].RefreshAllProperties();
                    }

                    this.RawPlayerSlotData.CopyTo(this.CachedSlotData[slotIndex], 0);
                }
            }
        }
    }
    //// End class
}
//// End namespace