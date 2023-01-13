namespace GES.Source.MonsterTableViewer
{
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.CraftListViewer;
    using GES.Source.Docking;
    using GES.Source.EquipmentListViewer;
    using GES.Source.Main;
    using System;
    using System.Buffers.Binary;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using static GES.Source.Main.MainViewModel;

    /// <summary>
    /// View model for the Monster Table Visualizer.
    /// </summary>
    public class MonsterTableViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="MonsterTableViewerViewModel" /> class.
        /// </summary>
        private static MonsterTableViewerViewModel monsterTableViewerInstance = new MonsterTableViewerViewModel();

        private UInt64 MonsterTableAddressEN = 0x00226020;
        private UInt64 MonsterTableAddressJP = 0x00242960;
        private UInt64 MonsterTableAddressPAL = 0x00227060;

        private UInt64 MonsterCountAddressEN = 0x002A932E;
        private UInt64 MonsterCountAddressJP = 0x002C5C6E;
        private UInt64 MonsterCountAddressPAL = 0x002AA36E;

        /// <summary>
        /// Prevents a default instance of the <see cref="MonsterTableViewerViewModel" /> class from being created.
        /// </summary>
        private MonsterTableViewerViewModel() : base("Monster List")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            Application.Current.Exit += this.OnAppExit;

            this.RunUpdateLoop();
        }

        private void OnAppExit(object sender, ExitEventArgs e)
        {
            this.CanUpdate = false;
        }

        public UInt16 MonsterCount { get; private set; }

        public MonsterTableDataView MonsterTable { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the actor reference count visualizer update loop can run.
        /// </summary>
        private bool CanUpdate { get; set; }

        private Byte[] CachedRawMonsterTableBytes { get; set; }

        private Byte[] RawMonsterTableBytes { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="MonsterTableViewerViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static MonsterTableViewerViewModel GetInstance()
        {
            return MonsterTableViewerViewModel.monsterTableViewerInstance;
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
                                    UpdateMonsterTable();
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

        private unsafe void UpdateMonsterTable()
        {
            UInt64 gameCubeMemoryBase = MemoryQueryer.Instance.ResolveModule(SessionManager.Session.OpenedProcess, "GC", EmulatorType.Dolphin);

            UInt64 monsterTableAddress;
            UInt64 monsterCountAddress;

            switch (MainViewModel.GetInstance().DetectedVersion)
            {
                default: return;
                case EDetectedVersion.JP: monsterTableAddress = MonsterTableAddressJP; monsterCountAddress = MonsterCountAddressJP; break;
                case EDetectedVersion.EN: monsterTableAddress = MonsterTableAddressEN; monsterCountAddress = MonsterCountAddressEN; break;
                case EDetectedVersion.PAL: monsterTableAddress = MonsterTableAddressPAL; monsterCountAddress = MonsterCountAddressPAL; break;
            }

            UInt64 monsterTablePointer = gameCubeMemoryBase + monsterTableAddress;
            UInt64 monsterCountPointer = gameCubeMemoryBase + monsterCountAddress;

            if (this.RawMonsterTableBytes == null)
            {
                this.RawMonsterTableBytes = new Byte[typeof(MonsterTableDataSerializable).StructLayoutAttribute.Size];
            }

            // Read the entire actor reference counting table
            Boolean success;
            MemoryReader.Instance.ReadBytes(
                SessionManager.Session.OpenedProcess,
                this.RawMonsterTableBytes,
                monsterTablePointer,
                out success);

            if (!success)
            {
                return;
            }

            UInt16 newMonsterCount = BinaryPrimitives.ReverseEndianness(MemoryReader.Instance.Read<UInt16>(
                SessionManager.Session.OpenedProcess,
                monsterCountPointer,
                out success));

            bool forceRefresh = false;

            if (newMonsterCount != this.MonsterCount)
            {
                this.MonsterCount = newMonsterCount;
                this.RaisePropertyChanged(nameof(this.MonsterCount));
                forceRefresh = true;
            }

            if (!success)
            {
                return;
            }

            if (this.CachedRawMonsterTableBytes == null)
            {
                this.CachedRawMonsterTableBytes = new Byte[typeof(MonsterTableDataSerializable).StructLayoutAttribute.Size];
            }

            if (this.MonsterTable == null)
            {
                this.MonsterTable = new MonsterTableDataView(new MonsterTableData());
            }

            MonsterTableData.Deserialize(this.MonsterTable.MonsterTableData, this.RawMonsterTableBytes);

            // Notify changes if new bytes differ from cached
            if (!this.RawMonsterTableBytes.SequenceEqual(this.CachedRawMonsterTableBytes) || forceRefresh)
            {
                this.MonsterTable.MonsterTableData.Refresh(this.RawMonsterTableBytes);
                this.MonsterTable.RefreshAllProperties();
                this.RaisePropertyChanged(nameof(this.MonsterTable));

                CraftListViewerViewModel.GetInstance().ExternalRefreshAll();
            }

            this.RawMonsterTableBytes.CopyTo(this.CachedRawMonsterTableBytes, 0);
        }
    }
    //// End class
}
//// End namespace