namespace GES.Source.InventoryViewer
{
    using GalaSoft.MvvmLight.Command;
    using GES.Engine.Common;
    using GES.Engine.Common.DataStructures;
    using GES.Engine.Common.Logging;
    using GES.Engine.Memory;
    using GES.Source;
    using GES.Source.CraftViewer;
    using GES.Source.Docking;
    using GES.Source.EquipmentViewer;
    using GES.Source.Main;
    using GES.View;
    using GES.Source.Editors.InventoryItemEditor;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using WpfHexaEditor.Core.MethodExtention;
    using System.Buffers.Binary;

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
            0x220290,
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

        private Int32 activeSlot;

        private Boolean forceRefresh;

        /// <summary>
        /// Prevents a default instance of the <see cref="InventoryViewerViewModel" /> class from being created.
        /// </summary>
        private InventoryViewerViewModel() : base("Inventory Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);

            this.CopyArtifactListCommand = new RelayCommand<Object>((obj) => this.CopyArtifactList(obj));
            this.CopyAddressCommand = new RelayCommand<Object>((obj) => this.CopyAddress(obj));
            this.CopyRawAddressCommand = new RelayCommand<Object>((obj) => this.CopyRawAddress(obj));
            this.EditItemCommand = new RelayCommand<Object>((obj) => this.EditItem(obj));

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

        public ICommand CopyArtifactListCommand { get; private set; }

        public ICommand CopyAddressCommand { get; private set; }

        public ICommand CopyRawAddressCommand { get; private set; }

        public ICommand EditItemCommand { get; private set; }

        public UInt16 SelectedItem { get;  set; }

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

        public Int32 ActiveSlot
        {
            get
            {
                return this.activeSlot;
            }

            set
            {
                this.activeSlot = value;
                this.forceRefresh = true;
            }
        }

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
                UInt32 slotPointerBase = 0x80000000 + (UInt32)slotDataAddresses[slotIndex];
                UInt64 rawSlotPointerBase = gameCubeMemoryBase + slotDataAddresses[slotIndex];

                if (this.RawPlayerSlotData == null)
                {
                    this.RawPlayerSlotData = new Byte[typeof(PlayerSlotDataSerializable).StructLayoutAttribute.Size];
                }

                // Read the entire actor reference counting table
                Boolean success;
                MemoryReader.Instance.ReadBytes(
                    SessionManager.Session.OpenedProcess,
                    this.RawPlayerSlotData,
                    rawSlotPointerBase,
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
                    if (!this.CachedSlotData[slotIndex].SequenceEqual(this.RawPlayerSlotData) || (this.forceRefresh && slotIndex == this.ActiveSlot))
                    {
                        bool shouldRefresh = slotIndex == this.ActiveSlot;

                        this.PlayerSlots[slotIndex].Slot.Refresh(rawSlotPointerBase + PlayerSlotData.InventoryOffset, slotPointerBase + PlayerSlotData.InventoryOffset, this.RawPlayerSlotData, slotIndex, shouldRefresh);
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
                    // this.ReportHackCandidates(slotPointer, gameCubeMemoryBase, slotIndex);
                }
            }

            this.forceRefresh = false;
        }

        static HashSet<UInt32> SeenValues = new HashSet<UInt32>();

        private void ReportHackCandidates(UInt64 slotPointer, UInt64 gameCubeMemoryBase, Int32 slotIndex)
        {
            String FileName = "CLES.txt";

            if (!File.Exists(FileName))
            {
                File.Create(FileName);
            }

            for (Int32 index = 0; index < this.PlayerSlots[slotIndex].Slot.rawItems.Count; index++)
            {
                if ((this.PlayerSlots[slotIndex].Slot.rawItems[index].ItemId >= 0x17D
                    && this.PlayerSlots[slotIndex].Slot.rawItems[index].ItemId <= 0x188)
                    || this.PlayerSlots[slotIndex].Slot.rawItems[index].ItemId == 0x0103
                    || this.PlayerSlots[slotIndex].Slot.rawItems[index].ItemId == 0x0125)
                {
                    const UInt64 InvOffset = 214;

                    UInt32 address = (UInt32)(0x80000000 + (slotPointer - gameCubeMemoryBase) + InvOffset + (UInt64)(index * 2));

                    if (SeenValues.Contains(address))
                    {
                        continue;
                    }

                    SeenValues.Add(address);

                    using (StreamWriter w = File.AppendText(FileName))
                    {
                        w.WriteLine(address.ToString("X"));
                    }
                }
            }
        }

        private void CopyArtifactList(Object itemIdObj)
        {
            const UInt16 SetEMask = 0b1111111000000000;
            UInt16 itemId = (UInt16)(Int32)itemIdObj;
            Boolean allowSetE = (itemId & SetEMask) == 0;
            String clipboardResult = String.Empty;

            List<String>[] options = allowSetE
                ? new List<String>[5] { new List<String>(), new List<String>(), new List<String>(), new List<String>(), new List<String>() }
                : new List<String>[4] { new List<String>(), new List<String>(), new List<String>(), new List<String>() };
            List<String> prefixString;


            if (MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageEN)
            {
                prefixString = new List<String> { "Option 1: ", "Option 2: ", "Option 3: ", "Option 4: ", "Option 5: " };
            }
            else
            {
                prefixString = new List<String> { "オプション 1: ", "オプション 2: ", "オプション 3: ", "オプション 4: ", "オプション 5: " };
            }

            for (Int32 index = 0; index < 16; index++)
            {
                if (!IsBitSet(itemId, index))
                {
                    continue;
                }

                List<String> artifactNames = GetArtifactNames(index, allowSetE);

                for (Int32 optionIndex = 0; optionIndex < options.Length; optionIndex++)
                {
                    if (artifactNames.Count > optionIndex)
                    {
                        options[optionIndex].Add(artifactNames[optionIndex]);
                    }
                }
            }

            for (Int32 optionIndex = 0; optionIndex < options.Length; optionIndex++)
            {
                clipboardResult += prefixString[optionIndex] + String.Join(", ", options[optionIndex]) + Environment.NewLine;
            }

            Clipboard.SetText(clipboardResult);
        }

        private void CopyAddress(Object itemObj)
        {
            if (itemObj is RawItemEntry)
            {
                RawItemEntry rawItem = (RawItemEntry)itemObj;

                Clipboard.SetText(rawItem.Address.ToString("X"));
            }
        }

        private void CopyRawAddress(Object itemObj)
        {
            if (itemObj is RawItemEntry)
            {
                RawItemEntry rawItem = (RawItemEntry)itemObj;

                Clipboard.SetText(rawItem.RawAddress.ToString("X"));
            }
        }

        private void EditItem(Object itemObj)
        {
            if (itemObj is RawItemEntry)
            {
                RawItemEntry itemEntry = itemObj as RawItemEntry;
                Window mainWindow = Application.Current.MainWindow;
                InventoryItemEditorViewModel.GetInstance().Show(owner: mainWindow, itemEntry);
                UInt16 selectedItem = InventoryItemEditorViewModel.GetInstance().SelectedItem;

                MemoryWriter.Instance.Write<UInt16>(
                    SessionManager.Session.OpenedProcess,
                    itemEntry.RawAddress,
                    BinaryPrimitives.ReverseEndianness(selectedItem));
            }
        }

        private List<String> GetArtifactNames(Int32 index, Boolean allowSetE)
        {
            List<String> artifactNames = new List<String>();

            if (MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageEN)
            {
                switch (index)
                {
                    case 0:
                        artifactNames.AddRange(new String[]{ "Shuriken", "Onion Sword", "Galatyn", "Drill"});
                        if (allowSetE)
                        {
                            artifactNames.Add("Fire Ring");
                        }
                        break;
                    case 1:
                        artifactNames.AddRange(new String[] { "Maneater", "Power Wristbands", "Ultima Tome", "Buckler" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Blizzard Ring");
                        }
                        break;
                    case 2:
                        artifactNames.AddRange(new String[] { "Double Axe", "Green Beret", "Silver Bracer", "Silver Spectacles" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Thunder Ring");
                        }
                        break;
                    case 3:
                        artifactNames.AddRange(new String[] { "Ashura", "Fang Charm", "Cat's Bell", "Sparkling Bracer" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Cure Ring");
                        }
                        break;
                    case 4:
                        artifactNames.AddRange(new String[] { "Kaiser Knuckles", "Twisted Headband", "Faerie Ring", "Black Hood" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Life Ring");
                        }
                        break;
                    case 5:
                        artifactNames.AddRange(new String[] { "Flametongue", "Heavy Armband", "Winged Cap", "Arai Helm" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Earth Pendant");
                        }
                        break;
                    case 6:
                        artifactNames.AddRange(new String[] { "Ice Brand", "Giant's Glove", "Candy Ring", "Elven Mantle" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Moon Pendant");
                        }
                        break;
                    case 7:
                        artifactNames.AddRange(new String[] { "Loaded Dice", "Dragon's Whisker", "Kris", "Wonder Bangle" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Star Pendant");
                        }
                        break;
                    case 8:
                        artifactNames.AddRange(new String[] { "Ogrekiller", "Mage Masher", "Red Slippers", "Protection Ring" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Sun Pendant");
                        }
                        break;
                    case 9:
                        artifactNames.AddRange(new String[] { "Engetsurin", "Rune Staff", "Dark Matter", "Aegis" });
                        break;
                    case 10:
                        artifactNames.AddRange(new String[] { "Sasuke Blade", "Book of Light", "Gold Hairpin", "Rat's Tail" });
                        break;
                    case 11:
                        artifactNames.AddRange(new String[] { "Mjollnir", "Sage's Staff", "Taotie Morif", "Teddy Bear" });
                        break;
                    case 12:
                        artifactNames.AddRange(new String[] { "Masquerade", "Wonder Wand", "Ribbon", "Moogle Pocket" });
                        break;
                    case 13:
                        artifactNames.AddRange(new String[] { "Murasame", "Rune Bell", "Main Gauche", "Chocobo Pocket" });
                        break;
                    case 14:
                        artifactNames.AddRange(new String[] { "Masamune", "Mage's Staff", "Chicken Knife", "Gobbie Pocket" });
                        break;
                    case 15:
                        artifactNames.AddRange(new String[] { "Gekkabijin", "Noah's Lute", "Save the Queen", "Ultimate Pocket" });
                        break;
                }
            }
            else
            {
                // TODO: Japanese localizations
                switch (index)
                {
                    case 0:
                        artifactNames.AddRange(new String[] { "Shuriken", "Onion Sword", "Galatyn", "Drill" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Fire Ring");
                        }
                        break;
                    case 1:
                        artifactNames.AddRange(new String[] { "Maneater", "Power Wristbands", "Ultima Tome", "Buckler" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Blizzard Ring");
                        }
                        break;
                    case 2:
                        artifactNames.AddRange(new String[] { "Double Axe", "Green Beret", "Silver Bracer", "Silver Spectacles" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Thunder Ring");
                        }
                        break;
                    case 3:
                        artifactNames.AddRange(new String[] { "Ashura", "Fang Charm", "Cat's Bell", "Sparkling Bracer" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Cure Ring");
                        }
                        break;
                    case 4:
                        artifactNames.AddRange(new String[] { "Kaiser Knuckles", "Twisted Headband", "Faerie Ring", "Black Hood" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Life Ring");
                        }
                        break;
                    case 5:
                        artifactNames.AddRange(new String[] { "Flametongue", "Heavy Armband", "Winged Cap", "Arai Helm" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Earth Pendant");
                        }
                        break;
                    case 6:
                        artifactNames.AddRange(new String[] { "Ice Brand", "Giant's Glove", "Candy Ring", "Elven Mantle" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Moon Pendant");
                        }
                        break;
                    case 7:
                        artifactNames.AddRange(new String[] { "Loaded Dice", "Dragon's Whisker", "Kris", "Wonder Bangle" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Star Pendant");
                        }
                        break;
                    case 8:
                        artifactNames.AddRange(new String[] { "Ogrekiller", "Mage Masher", "Red Slippers", "Protection Ring" });
                        if (allowSetE)
                        {
                            artifactNames.Add("Sun Pendant");
                        }
                        break;
                    case 9:
                        artifactNames.AddRange(new String[] { "Engetsurin", "Rune Staff", "Dark Matter", "Aegis" });
                        break;
                    case 10:
                        artifactNames.AddRange(new String[] { "Sasuke Blade", "Book of Light", "Gold Hairpin", "Rat's Tail" });
                        break;
                    case 11:
                        artifactNames.AddRange(new String[] { "Mjollnir", "Sage's Staff", "Taotie Morif", "Teddy Bear" });
                        break;
                    case 12:
                        artifactNames.AddRange(new String[] { "Masquerade", "Wonder Wand", "Ribbon", "Moogle Pocket" });
                        break;
                    case 13:
                        artifactNames.AddRange(new String[] { "Murasame", "Rune Bell", "Main Gauche", "Chocobo Pocket" });
                        break;
                    case 14:
                        artifactNames.AddRange(new String[] { "Masamune", "Mage's Staff", "Chicken Knife", "Gobbie Pocket" });
                        break;
                    case 15:
                        artifactNames.AddRange(new String[] { "Gekkabijin", "Noah's Lute", "Save the Queen", "Ultimate Pocket" });
                        break;
                }
            }

            return artifactNames;
        }

        bool IsBitSet(UInt16 b, Int32 pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
    //// End class
}
//// End namespace