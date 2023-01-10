namespace GES.Source.EquipmentViewer
{
    using GalaSoft.MvvmLight.Command;
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.Docking;
    using GES.Source.InventoryViewer;
    using GES.Source.Main;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Heap Visualizer.
    /// </summary>
    public class EquipmentViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static EquipmentViewerViewModel actorReferenceCountVisualizerInstance = new EquipmentViewerViewModel();

        private const Int32 PlayerCount = 4;

        private UInt64 equipmentListAddressEN = 0x3B800;
        private UInt64 equipmentListAddressJP = 0x35800;
        private UInt64 equipmentListAddressPal = 0x3D800;

        /// <summary>
        /// Prevents a default instance of the <see cref="HeapVisualizerViewModel" /> class from being created.
        /// </summary>
        private EquipmentViewerViewModel() : base("[GES] Equipment List Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.CopyAddressCommand = new RelayCommand<Object>((obj) => this.CopyAddress(obj));
            this.CopyRawAddressCommand = new RelayCommand<Object>((obj) => this.CopyRawAddress(obj));
            this.PlayerEquipmentData = new FullyObservableCollection<EquipmentDataView>();
            this.CachedPlayerSlotData = new Byte[PlayerCount][];

            for (int index = 0; index < PlayerCount; index++)
            {
                this.PlayerEquipmentData.Add(new EquipmentDataView(new EquipmentData()));
            }

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        public ICommand CopyAddressCommand { get; private set; }

        public ICommand CopyRawAddressCommand { get; private set; }

        /// <summary>
        /// Gets the list of actor reference count slots.
        /// </summary>
        public FullyObservableCollection<EquipmentDataView> PlayerEquipmentData { get; private set; }

        private Byte[][] CachedPlayerSlotData { get; set; }

        private Byte[] RawEquipmentData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="EquipmentViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static EquipmentViewerViewModel GetInstance()
        {
            return EquipmentViewerViewModel.actorReferenceCountVisualizerInstance;
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
            foreach(EquipmentDataView equipmentDataView in this.PlayerEquipmentData)
            {
                equipmentDataView.RefreshAllProperties();
            }
        }

        public void ExternalRefresh(Int32 playerIndex)
        {
            if (playerIndex >= 0 && playerIndex < this.PlayerEquipmentData.Count)
            {
                this.PlayerEquipmentData[playerIndex].RefreshAllProperties();
            }
        }

        private unsafe void UpdateActorSlots()
        {
            UInt64[] gbaMemoryBases = new UInt64[PlayerCount]
            {
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_0", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_1", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_2", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_3", EmulatorType.Dolphin),
            };

            UInt64 equipmentListAddress;
            
            switch(MainViewModel.GetInstance().SelectedVersion)
            {
                default:
                case MainViewModel.VersionJP: equipmentListAddress = equipmentListAddressJP; break;
                case MainViewModel.VersionEN: equipmentListAddress = equipmentListAddressEN; break;
                case MainViewModel.VersionPAL: equipmentListAddress = equipmentListAddressPal; break;
            }

            for (Int32 playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
            {
                UInt64 slotPointer = gbaMemoryBases[playerIndex] + equipmentListAddress;

                if (this.RawEquipmentData == null)
                {
                    this.RawEquipmentData = new Byte[typeof(EquipmentDataSerializable).StructLayoutAttribute.Size];
                }

                // Read the entire actor reference counting table
                Boolean success;
                MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    this.RawEquipmentData,
                    slotPointer,
                    out success);

                if (success)
                {
                    if (this.CachedPlayerSlotData[playerIndex] == null)
                    {
                        this.CachedPlayerSlotData[playerIndex] = new Byte[typeof(EquipmentDataSerializable).StructLayoutAttribute.Size];
                    }

                    if (this.PlayerEquipmentData[playerIndex].EquipmentData == null)
                    {
                        this.PlayerEquipmentData[playerIndex].EquipmentData = new EquipmentData();
                    }

                    EquipmentData.Deserialize(this.PlayerEquipmentData[playerIndex].EquipmentData, this.RawEquipmentData);

                    // Notify changes if new bytes differ from cached
                    if (!this.CachedPlayerSlotData[playerIndex].SequenceEqual(this.RawEquipmentData))
                    {
                        this.PlayerEquipmentData[playerIndex].EquipmentData.Refresh(equipmentListAddress, slotPointer, this.RawEquipmentData, playerIndex);
                        this.PlayerEquipmentData[playerIndex].RefreshAllProperties();
                    }

                    this.RawEquipmentData.CopyTo(this.CachedPlayerSlotData[playerIndex], 0);
                }
            }
        }

        private void CopyAddress(Object itemObj)
        {
            if (itemObj is EquipmentDataView)
            {
                EquipmentDataView rawItem = (EquipmentDataView)itemObj;

                Clipboard.SetText(rawItem.Address.ToString("X"));
            }
        }

        private void CopyRawAddress(Object itemObj)
        {
            if (itemObj is EquipmentDataView)
            {
                EquipmentDataView rawItem = (EquipmentDataView)itemObj;

                Clipboard.SetText(rawItem.RawAddress.ToString("X"));
            }
        }
    }
    //// End class
}
//// End namespace