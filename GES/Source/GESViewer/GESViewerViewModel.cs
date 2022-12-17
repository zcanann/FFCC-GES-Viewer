namespace GES.Source.GESViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.Docking;
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// View model for the Heap Visualizer.
    /// </summary>
    public class GESViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static GESViewerViewModel actorReferenceCountVisualizerInstance = new GESViewerViewModel();

        private const Int32 SlotCount = 8;

        private const Int32 PlayerCount = 4;

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

        private UInt64 equipmentListAddress = 0x35800;

        /// <summary>
        /// Prevents a default instance of the <see cref="HeapVisualizerViewModel" /> class from being created.
        /// </summary>
        private GESViewerViewModel() : base("GES Visualizer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.PlayerSlots = new FullyObservableCollection<PlayerSlotDataView>();
            this.EquipmentLists = new FullyObservableCollection<EquipmentListDataView>();

            for (int index = 0; index < SlotCount; index++)
            {
                this.PlayerSlots.Add(new PlayerSlotDataView(new PlayerSlotData()));
            }

            for (int index = 0; index < PlayerCount; index++)
            {
                this.EquipmentLists.Add(new EquipmentListDataView(new EquipmentListData()));
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
        /// Gets the list of actor reference count slots.
        /// </summary>
        public FullyObservableCollection<EquipmentListDataView> EquipmentLists { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="GESViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static GESViewerViewModel GetInstance()
        {
            return GESViewerViewModel.actorReferenceCountVisualizerInstance;
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

                    await Task.Delay(250);
                }
            });
        }

        private unsafe void UpdateActorSlots()
        {
            UInt64 gameCubeMemoryBase = MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GC", EmulatorType.Dolphin);
            UInt64[] gbaCubeMemoryBases = new UInt64[PlayerCount]
            {
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_0", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_1", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_2", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_3", EmulatorType.Dolphin),
            };

            for (Int32 slotIndex = 0; slotIndex < SlotCount; slotIndex++)
            {
                UInt64 slotPointer = gameCubeMemoryBase + slotDataAddresses[slotIndex];

                // Read the entire actor reference counting table
                Boolean success;
                Byte[] playerSlotData = MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    slotPointer,
                    typeof(PlayerSlotData).StructLayoutAttribute.Size,
                    out success);

                if (success)
                {
                    PlayerSlotData result = PlayerSlotData.FromByteArray(playerSlotData, slotIndex);

                    if (result != null)
                    {
                        this.PlayerSlots[slotIndex].Slot = result;
                        this.PlayerSlots[slotIndex].RefreshAllProperties();
                    }
                }
            }

            for (Int32 playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
            {
                UInt64 slotPointer = gbaCubeMemoryBases[playerIndex] + equipmentListAddress;

                // Read the entire actor reference counting table
                Boolean success;
                Byte[] playerSlotData = MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    slotPointer,
                    typeof(EquipmentListData).StructLayoutAttribute.Size,
                    out success);

                if (success)
                {
                    EquipmentListData result = EquipmentListData.FromByteArray(playerSlotData, playerIndex);

                    if (result != null)
                    {
                        this.EquipmentLists[playerIndex].EquipmentList = result;
                        this.EquipmentLists[playerIndex].RefreshAllProperties();
                    }
                }
            }
        }
    }
    //// End class
}
//// End namespace