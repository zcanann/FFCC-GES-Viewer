namespace GES.Source.CraftTableViewer
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
    /// View model for the Craft Table Visualizer.
    /// </summary>
    public class CraftTableViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static CraftTableViewerViewModel actorReferenceCountVisualizerInstance = new CraftTableViewerViewModel();

        private UInt64 craftTableAddressEN = 0x00954B78 - 72; // TODO: Verify -72
        private UInt64 craftTableAddressJP = 0x00979FB8;
        private UInt64 craftTableAddressPAL = 0x00955BF8 - 72; // TODO: Verify -72

        /// <summary>
        /// Prevents a default instance of the <see cref="CraftTableViewerViewModel" /> class from being created.
        /// </summary>
        private CraftTableViewerViewModel() : base("Craft Table Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        public CraftTableDataView CraftTable { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        private Byte[] CachedRawCraftDataBytes { get; set; }

        private Byte[] RawCraftDataBytes { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="CraftTableViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static CraftTableViewerViewModel GetInstance()
        {
            return CraftTableViewerViewModel.actorReferenceCountVisualizerInstance;
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

            UInt64 craftTableAddress;

            switch (MainViewModel.GetInstance().SelectedVersion)
            {
                default:
                case MainViewModel.VersionJP: craftTableAddress = craftTableAddressJP; break;
                case MainViewModel.VersionEN: craftTableAddress = craftTableAddressEN; break;
                case MainViewModel.VersionPAL: craftTableAddress = craftTableAddressPAL; break;
            }

            UInt64 slotPointer = gameCubeMemoryBase + craftTableAddress;

            if (this.RawCraftDataBytes == null)
            {
                this.RawCraftDataBytes = new Byte[typeof(CraftTableDataSerializable).StructLayoutAttribute.Size];
            }

            // Read the entire actor reference counting table
            Boolean success;
            MemoryReader.Instance.ReadBytes(
                SessionManager.Session.OpenedProcess,
                this.RawCraftDataBytes,
                slotPointer,
                out success);

            if (success)
            {
                if (this.CachedRawCraftDataBytes == null)
                {
                    this.CachedRawCraftDataBytes = new Byte[typeof(CraftTableDataSerializable).StructLayoutAttribute.Size];
                }

                if (this.CraftTable == null)
                {
                    this.CraftTable = new CraftTableDataView(new CraftTableData());
                }

                CraftTableData.Deserialize(this.CraftTable.CraftTableData, this.RawCraftDataBytes);

                // Notify changes if new bytes differ from cached
                if (!this.RawCraftDataBytes.SequenceEqual(this.CachedRawCraftDataBytes))
                {
                    this.CraftTable.CraftTableData.Refresh(this.RawCraftDataBytes);
                    this.CraftTable.RefreshAllProperties();

                    CraftViewerViewModel.GetInstance().ExternalRefreshAll();
                }

                this.RawCraftDataBytes.CopyTo(this.CachedRawCraftDataBytes, 0);
            }
        }
    }
    //// End class
}
//// End namespace