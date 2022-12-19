namespace GES.Source.EquipmentViewer
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
    public class EquipmentViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static EquipmentViewerViewModel actorReferenceCountVisualizerInstance = new EquipmentViewerViewModel();

        private const Int32 PlayerCount = 4;

        private UInt64 equipmentListAddress = 0x35800;

        /// <summary>
        /// Prevents a default instance of the <see cref="HeapVisualizerViewModel" /> class from being created.
        /// </summary>
        private EquipmentViewerViewModel() : base("Equipment Visualizer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.EquipmentData = new FullyObservableCollection<EquipmentDataView>();

            for (int index = 0; index < PlayerCount; index++)
            {
                this.EquipmentData.Add(new EquipmentDataView(new EquipmentData()));
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
        public FullyObservableCollection<EquipmentDataView> EquipmentData { get; private set; }

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

                    await Task.Delay(250);
                }
            });
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

            for (Int32 playerIndex = 0; playerIndex < PlayerCount; playerIndex++)
            {
                UInt64 slotPointer = gbaCubeMemoryBases[playerIndex] + equipmentListAddress;

                // Read the entire actor reference counting table
                Boolean success;
                Byte[] playerSlotData = MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    slotPointer,
                    typeof(EquipmentData).StructLayoutAttribute.Size,
                    out success);

                if (success)
                {
                    EquipmentData result = EquipmentViewer.EquipmentData.FromByteArray(playerSlotData, playerIndex);

                    if (result != null)
                    {
                        this.EquipmentData[playerIndex].EquipmentData = result;
                        this.EquipmentData[playerIndex].RefreshAllProperties();
                    }
                }
            }
        }
    }
    //// End class
}
//// End namespace