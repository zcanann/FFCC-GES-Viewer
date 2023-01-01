
namespace GES.Source.ItemCatalogViewer
{
    using GES.Engine.Common.DataStructures;
    using GES.Source.EquipmentViewer;
    using GES.Source.MonsterTableViewer;
    using System;
    using System.Buffers.Binary;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RawItemCatalogItemEntry : INotifyPropertyChanged
    {
        public UInt16 ClavatCraftedItem { get; set; }

        public UInt16 LiltyCraftedItem { get; set; }

        public UInt16 YukeCraftedItem { get; set; }

        public UInt16 SelkieCraftedItem { get; set; }

        public UInt16 BaseItemId { get; set; }

        public Byte Tribe { get; set; }

        public Byte ModelFx { get; set; } // 18

        public Byte ModelId { get; set; } // 19

        public Byte Slot { get; set; }

        public UInt16 StatBoost { get; set; } // 22, 23

        public UInt16 Focus { get; set; } // 24, 25

        public UInt16 Magic { get; set; }

        public UInt16 Defense { get; set; }

        public UInt16 Strength { get; set; }

        public UInt16 Index { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.ClavatCraftedItem));
            this.RaisePropertyChanged(nameof(this.LiltyCraftedItem));
            this.RaisePropertyChanged(nameof(this.YukeCraftedItem));
            this.RaisePropertyChanged(nameof(this.SelkieCraftedItem));
            this.RaisePropertyChanged(nameof(this.BaseItemId));
            this.RaisePropertyChanged(nameof(this.Tribe));
            this.RaisePropertyChanged(nameof(this.ModelFx));
            this.RaisePropertyChanged(nameof(this.ModelId));
            this.RaisePropertyChanged(nameof(this.Slot));
            this.RaisePropertyChanged(nameof(this.StatBoost));
            this.RaisePropertyChanged(nameof(this.Focus));
            this.RaisePropertyChanged(nameof(this.Magic));
            this.RaisePropertyChanged(nameof(this.Tribe));
            this.RaisePropertyChanged(nameof(this.Defense));
            this.RaisePropertyChanged(nameof(this.Strength));
            this.RaisePropertyChanged(nameof(this.Index));
        }

        /// <summary>
        /// Indicates that a given property in this project item has changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void RaisePropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x04BA * 72)]
    public class ItemCatalogDataSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x04B5 * 72)]
        public Byte[] rawItemCatalogSlots;
    }

    public class ItemCatalogData
    {
        public ItemCatalogData()
        {
            this.SerializableData = new ItemCatalogDataSerializable();
        }

        public ItemCatalogDataSerializable SerializableData { get; set; }

        public FullyObservableCollection<RawItemCatalogItemEntry> rawItems = new FullyObservableCollection<RawItemCatalogItemEntry>();

        const Int32 StructSize = 72;

        public static void Deserialize(ItemCatalogData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new ItemCatalogDataSerializable();
                    }

                    Marshal.PtrToStructure<ItemCatalogDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(Byte[] bytes)
        {
            for (Int32 index = 0; index < bytes.Length / StructSize; index++)
            {
                if (index >= this.rawItems.Count || this.rawItems[index] == null)
                {
                    this.rawItems.Add(new RawItemCatalogItemEntry());
                }

                UInt16 baseItemId = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 8));
                UInt16 statBoost = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 14));

                this.rawItems[index].BaseItemId = baseItemId;
                this.rawItems[index].ModelFx = bytes[index * StructSize + 10];
                this.rawItems[index].ModelId = bytes[index * StructSize + 11];
                this.rawItems[index].Slot = bytes[index * StructSize + 12];
                this.rawItems[index].Tribe = bytes[index * StructSize + 13];
                this.rawItems[index].StatBoost = statBoost;
                this.rawItems[index].Focus = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 16));
                this.rawItems[index].ClavatCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 64));
                this.rawItems[index].LiltyCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 66));
                this.rawItems[index].YukeCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 68));
                this.rawItems[index].SelkieCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 70));
                this.rawItems[index].Index = (UInt16)index;
                this.rawItems[index].Refresh();

                if (baseItemId == 1)
                {
                    this.rawItems[index].Strength = statBoost;
                }
                else if (baseItemId == 0x45)
                {
                    this.rawItems[index].Defense = statBoost;
                }
            }
        }
    }
    //// End class
}
//// End namespace
