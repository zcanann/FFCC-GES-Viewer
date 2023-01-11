namespace GES.Source.ItemCatalogViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.CraftViewer;
    using GES.Source.Docking;
    using GES.Source.Main;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using static GES.Source.Main.MainViewModel;

    /// <summary>
    /// View model for the Item Catalog Visualizer.
    /// </summary>
    public class ItemCatalogViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="ActorReferenceCountVisualizer" /> class.
        /// </summary>
        private static ItemCatalogViewerViewModel actorReferenceCountVisualizerInstance = new ItemCatalogViewerViewModel();

        private UInt64 ItemCatalogAddressEN = 0x00954B40;
        private UInt64 ItemCatalogAddressJP = 0x00979FC0;
        private UInt64 ItemCatalogAddressPAL = 0x00955BC0;

        /// <summary>
        /// Prevents a default instance of the <see cref="ItemCatalogViewerViewModel" /> class from being created.
        /// </summary>
        private ItemCatalogViewerViewModel() : base("Item Catalog")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        public ItemCatalogDataView ItemCatalog { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        private Byte[] CachedRawCraftDataBytes { get; set; }

        private Byte[] RawCraftDataBytes { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ItemCatalogViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ItemCatalogViewerViewModel GetInstance()
        {
            return ItemCatalogViewerViewModel.actorReferenceCountVisualizerInstance;
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
            UInt64 ItemCatalogAddress;

            switch (MainViewModel.GetInstance().DetectedVersion)
            {
                default: return;
                case EDetectedVersion.JP: ItemCatalogAddress = ItemCatalogAddressJP; break;
                case EDetectedVersion.EN: ItemCatalogAddress = ItemCatalogAddressEN; break;
                case EDetectedVersion.PAL: ItemCatalogAddress = ItemCatalogAddressPAL; break;
            }

            UInt64 slotPointer = gameCubeMemoryBase + ItemCatalogAddress;

            if (this.RawCraftDataBytes == null)
            {
                this.RawCraftDataBytes = new Byte[typeof(ItemCatalogDataSerializable).StructLayoutAttribute.Size];
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
                    this.CachedRawCraftDataBytes = new Byte[typeof(ItemCatalogDataSerializable).StructLayoutAttribute.Size];
                }

                if (this.ItemCatalog == null)
                {
                    this.ItemCatalog = new ItemCatalogDataView(new ItemCatalogData());
                }

                ItemCatalogData.Deserialize(this.ItemCatalog.ItemCatalogData, this.RawCraftDataBytes);

                // Notify changes if new bytes differ from cached
                if (!this.RawCraftDataBytes.SequenceEqual(this.CachedRawCraftDataBytes))
                {
                    this.ItemCatalog.ItemCatalogData.Refresh(0x80000000 + ItemCatalogAddress, slotPointer, this.RawCraftDataBytes);
                    this.ItemCatalog.RefreshAllProperties();

                    CraftViewerViewModel.GetInstance().ExternalRefreshAll();
                }

                this.RawCraftDataBytes.CopyTo(this.CachedRawCraftDataBytes, 0);
            }
        }
    }
    //// End class
}
//// End namespace