namespace GES.Source.CraftListViewer
{
    using GalaSoft.MvvmLight.Command;
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.Docking;
    using GES.Source.EquipmentListViewer;
    using GES.Source.Main;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using static GES.Source.Main.MainViewModel;

    /// <summary>
    /// View model for the Craft Visualizer.
    /// </summary>
    public class CraftListViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static CraftListViewerViewModel actorReferenceCountVisualizerInstance = new CraftListViewerViewModel();

        private const Int32 PlayerCount = 4;

        private UInt64 CraftListAddressEN = 0x38800;
        private UInt64 CraftListAddressJP = 0x32800;
        private UInt64 CraftListAddressPal = 0x3A800;

        private UInt64 CommandListItemCountAddressEN = 0x27B4;
        private UInt64 CommandListItemCountAddressJP = 0x2778;
        private UInt64 CommandListItemCountAddressPAL = 0x27B4;



        /// <summary>
        /// Prevents a default instance of the <see cref="CraftListViewerViewModel" /> class from being created.
        /// </summary>
        private CraftListViewerViewModel() : base("[WM] Craft List Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.CopyAddressCommand = new RelayCommand<Object>((obj) => this.CopyAddress(obj));
            this.CopyRawAddressCommand = new RelayCommand<Object>((obj) => this.CopyRawAddress(obj));
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

        public ICommand CopyAddressCommand { get; private set; }

        public ICommand CopyRawAddressCommand { get; private set; }

        /// <summary>
        /// Gets the list of actor reference count slots.
        /// </summary>
        public FullyObservableCollection<CraftDataView> PlayerCraftData { get; private set; }

        private Byte[][] CachedPlayerSlotData { get; set; }

        private Byte[] RawCraftData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="CraftListViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static CraftListViewerViewModel GetInstance()
        {
            return CraftListViewerViewModel.actorReferenceCountVisualizerInstance;
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
                                    UpdateCraftListData();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(LogLevel.Error, "Error updating the Craft List / Command List Visualizer", ex);
                        }
                    }

                    await Task.Delay(200);
                }
            });
        }

        private Boolean ForceRefresh { get; set; }

        public void ExternalRefreshAll()
        {
            this.ForceRefresh = true;
        }

        public void ExternalRefresh(Int32 playerIndex)
        {
            if (playerIndex >= 0 && playerIndex < this.PlayerCraftData.Count)
            {
                this.PlayerCraftData[playerIndex].RefreshAllProperties();
                this.ForceRefresh = true;
            }
        }

        private unsafe void UpdateCraftListData()
        {
            UInt64[] gbaWMMemoryBases = new UInt64[PlayerCount]
            {
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_0", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_1", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_2", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_WM_3", EmulatorType.Dolphin),
            };
            UInt64[] gbaIMMemoryBases = new UInt64[PlayerCount]
            {
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_IM_0", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_IM_1", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_IM_2", EmulatorType.Dolphin),
                MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GBA_IM_3", EmulatorType.Dolphin),
            };

            UInt64 craftListAddress;
            UInt64 commandListItemCountAddress;

            switch (MainViewModel.GetInstance().DetectedVersion)
            {
                default: return;
                case EDetectedVersion.JP: craftListAddress = CraftListAddressJP; break;
                case EDetectedVersion.EN: craftListAddress = CraftListAddressEN; break;
                case EDetectedVersion.PAL: craftListAddress = CraftListAddressPal; break;
            }

            switch (MainViewModel.GetInstance().DetectedVersion)
            {
                default: return;
                case EDetectedVersion.JP: commandListItemCountAddress = CommandListItemCountAddressJP; break;
                case EDetectedVersion.EN: commandListItemCountAddress = CommandListItemCountAddressEN; break;
                case EDetectedVersion.PAL: commandListItemCountAddress = CommandListItemCountAddressPAL; break;
            }

            for (Int32 playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
            {
                UInt64 slotPointer = gbaWMMemoryBases[playerIndex] + craftListAddress;

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

                if (!success)
                {
                    continue;
                }

                Byte commandListCount = MemoryReader.Instance.Read<Byte>(
                    SessionManager.Session.OpenedProcess,
                    gbaIMMemoryBases[playerIndex] + commandListItemCountAddress,
                    out success);

                if (!success)
                {
                    continue;
                }

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
                if (!this.CachedPlayerSlotData[playerIndex].SequenceEqual(this.RawCraftData) || this.ForceRefresh)
                {
                    this.PlayerCraftData[playerIndex].CraftData.Refresh(craftListAddress, slotPointer, this.RawCraftData, playerIndex, commandListCount);
                    this.PlayerCraftData[playerIndex].RefreshAllProperties();
                }

                this.RawCraftData.CopyTo(this.CachedPlayerSlotData[playerIndex], 0);
            }

            this.ForceRefresh = false;
        }

        private void CopyAddress(Object itemObj)
        {
            if (itemObj is CraftDataView)
            {
                CraftDataView rawItem = (CraftDataView)itemObj;

                Clipboard.SetText(rawItem.Address.ToString("X"));
            }
        }

        private void CopyRawAddress(Object itemObj)
        {
            if (itemObj is CraftDataView)
            {
                CraftDataView rawItem = (CraftDataView)itemObj;

                Clipboard.SetText(rawItem.RawAddress.ToString("X"));
            }
        }
    }
    //// End class
}
//// End namespace