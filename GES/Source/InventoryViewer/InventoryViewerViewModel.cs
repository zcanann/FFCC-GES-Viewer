namespace GES.Source.InventoryViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.CraftViewer;
    using GES.Source.Docking;
    using GES.Source.EquipmentViewer;
    using GES.Source.Main;
    using System;
    using System.Collections.Generic;
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

        private const Int32 PlayerCount = 4;
        private const Int32 SlotCount = 8;

        private UInt64[] slotDataAddressesJP = new UInt64[SlotCount]
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

        private UInt64[] slotDataAddressesEN = new UInt64[SlotCount]
        {
            0x21F250,
            0x21FE80,
            0x220AB0,
            0x2216E0,
            0x222310,
            0x222F40,
            0x223B70,
            0x2247A0,
        };

        private UInt64[] slotDataAddressesPAL = new UInt64[SlotCount]
        {
            0x223350,
            0x220EC0,
            0x221AF0,
            0x222720,
            0x223350,
            0x223F80,
            0x224BB0,
            0x2257E0,
        };

        private UInt64[] slotMappingAddressesJP = new UInt64[PlayerCount]
        {
            0x23A7E3,
            0x23A7E7,
            0x23A7EB,
            0x23A7EF,
        };

        private UInt64[] slotMappingAddressesEN = new UInt64[PlayerCount]
        {
            0x21DEA3,
            0x21DEA7,
            0x21DEAB,
            0x21DEAF,
        };

        private UInt64[] slotMappingAddressesPAL = new UInt64[PlayerCount]
        {
            0x21EEE3,
            0x21EEE7,
            0x21EEEB,
            0x21EEEF,
        };

        /// <summary>
        /// Prevents a default instance of the <see cref="InventoryViewerViewModel" /> class from being created.
        /// </summary>
        private InventoryViewerViewModel() : base("Inventory Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.PlayerSlots = new FullyObservableCollection<PlayerSlotDataView>();
            this.DisplayPlayerToSlotMap = new Dictionary<Int32, Int32>();
            this.PlayerToSlotMap = new Dictionary<Int32, Int32>();
            this.CachedSlotData = new Byte[SlotCount][];

            for (Int32 index = 0; index < SlotCount; index++)
            {
                this.PlayerSlots.Add(new PlayerSlotDataView(new PlayerSlotData()));
            }

            for (Int32 index = 0; index < PlayerCount; index++)
            {
                this.PlayerToSlotMap[index] = 99;
                this.DisplayPlayerToSlotMap[index] = 99;
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

        public Dictionary<Int32, Int32> DisplayPlayerToSlotMap { get; private set; }

        public Dictionary<Int32, Int32> PlayerToSlotMap { get; private set; }

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

        public void ExternalRefreshAll()
        {
            foreach (PlayerSlotDataView playerSlotDataView in this.PlayerSlots)
            {
                playerSlotDataView.RefreshAllProperties();
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

            UInt64[] slotMappingAddresses;
            UInt64[] slotDataAddresses;

            switch (MainViewModel.GetInstance().SelectedVersion)
            {
                default:
                case MainViewModel.VersionJP: slotMappingAddresses = slotMappingAddressesJP; slotDataAddresses = slotDataAddressesJP; break;
                case MainViewModel.VersionEN: slotMappingAddresses = slotMappingAddressesEN; slotDataAddresses = slotDataAddressesEN; break;
                case MainViewModel.VersionPAL: slotMappingAddresses = slotMappingAddressesPAL; slotDataAddresses = slotDataAddressesPAL; break;
            }

            for (Int32 playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
            {
                UInt64 slotPointer = gameCubeMemoryBase + slotMappingAddresses[playerIndex];
                Boolean success;
                Byte result = MemoryReader.Instance.Read<Byte>(
                    SessionManager.Session.OpenedProcess,
                    slotPointer,
                    out success);

                if (success && this.PlayerToSlotMap[playerIndex] != result)
                {
                    this.PlayerToSlotMap[playerIndex] = result;
                    this.DisplayPlayerToSlotMap[playerIndex] = result + 1;
                    this.RaisePropertyChanged(nameof(this.DisplayPlayerToSlotMap));
                }
            }

            for (Int32 slotIndex = 0; slotIndex < SlotCount; slotIndex++)
            {
                UInt64 slotPointer = gameCubeMemoryBase + slotDataAddresses[slotIndex];

                if (this.RawPlayerSlotData == null)
                {
                    this.RawPlayerSlotData = new Byte[typeof(PlayerSlotDataSerializable).StructLayoutAttribute.Size];
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
                        this.CachedSlotData[slotIndex] = new Byte[typeof(PlayerSlotDataSerializable).StructLayoutAttribute.Size];
                    }

                    if (this.PlayerSlots[slotIndex].Slot == null)
                    {
                        this.PlayerSlots[slotIndex].Slot = new PlayerSlotData();
                    }

                    PlayerSlotData.Deserialize(this.PlayerSlots[slotIndex].Slot, this.RawPlayerSlotData);

                    // Notify changes if new bytes differ from cached
                    if (!this.CachedSlotData[slotIndex].SequenceEqual(this.RawPlayerSlotData))
                    {
                        this.PlayerSlots[slotIndex].Slot.Refresh(this.RawPlayerSlotData, slotIndex);
                        this.PlayerSlots[slotIndex].RefreshAllProperties();

                        foreach (var Next in this.PlayerToSlotMap)
                        {
                            Int32 nextPlayerIndex = Next.Key;
                            Int32 nextSlotIndex = Next.Value;

                            if (nextSlotIndex == slotIndex)
                            {
                                EquipmentViewerViewModel.GetInstance().ExternalRefresh(this.PlayerToSlotMap[nextPlayerIndex]);
                                CraftViewerViewModel.GetInstance().ExternalRefresh(this.PlayerToSlotMap[nextPlayerIndex]);
                            }
                        }
                    }

                    this.RawPlayerSlotData.CopyTo(this.CachedSlotData[slotIndex], 0);
                }
            }
        }
    }
    //// End class
}
//// End namespace