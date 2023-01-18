
namespace GES.Source.ItemCatalogViewer
{
    using GES.Engine.Common.DataStructures;
    using System;
    using System.Buffers.Binary;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RawItemCatalogItemEntry : INotifyPropertyChanged
    {
        ///////////////// UNIVERSAL /////////////////
        public UInt16 BaseItemId { get; set; } // 0, 1

        ///////////////// WEAPON /////////////////
        public Byte ModelFx { get; set; } // 2
        public Byte ModelId { get; set; } // 3
        public Byte Slot { get; set; } // 4
        public Byte Tribe { get; set; } // 5
        public Int16 BonusValue { get; set; } // 6, 7
        public Int16 BonusType { get; set; } // 8, 9
        public Int16 Focus { get; set; } // 10, 11
        public Byte[] WeaponUnusedByes1 { get; set; } // 12, 13, 14, 15
        public UInt16 ItemFXSize { get; set; } // 16, 17
        public Int16 ItemFX1 { get; set; } // 18, 19
        public Int16 ItemFX2 { get; set; } // 20, 21
        public Int16 ItemFX3 { get; set; } // 22, 23
        public Int16 ItemFX4 { get; set; } // 24, 25
        public Int16 ItemFX5 { get; set; } // 26, 27
        public Int16 ItemFX6 { get; set; } // 28, 29
        public Int16 ItemFX7 { get; set; } // 30, 31
        public Byte[] WeaponUnusedByes3 { get; set; } // 32-55
        public UInt16 SwingSound1 { get; set; } // 56, 57
        public UInt16 SwingSound1Delay { get; set; } // 58, 59
        public UInt16 SwingSound2 { get; set; } // 60, 61
        public UInt16 SwingSound2Delay { get; set; } // 62, 63
        public UInt16 SwingHitSound { get; set; } // 64, 65
        public UInt16 HitSound { get; set; } // 66, 67
        public Byte[] WeaponUnusedByes4 { get; set; } // 68, 69, 70, 71

        ///////////////// ARTIFACTS / ITEMS /////////////////
        // public Byte ModelFx { get; set; } // 2
        // public Byte ModelId { get; set; } // 3
        public Byte[] ItemUnknown1 { get; set; } // 4, 5
        // public UInt16 BonusValue { get; set; } // 6, 7
        public Byte[] ItemUnknown2 { get; set; } // 8, 9
        public Int16 ItemSpell { get; set; } // 10, 11
        public Byte[] ItemUnknown3 { get; set; } // 12, 13, 14, 15
        // public UInt16 ItemFXSize { get; set; } // 16, 17
        // public Int16 ItemFX1 { get; set; } // 18, 19
        // public Int16 ItemFX2 { get; set; } // 20, 21
        // public Int16 ItemFX3 { get; set; } // 22, 23
        // public Int16 ItemFX4 { get; set; } // 24, 25
        // public Int16 ItemFX5 { get; set; } // 26, 27
        // public Int16 ItemFX6 { get; set; } // 28, 29
        // public Int16 ItemFX7 { get; set; } // 30, 31
        public UInt16 ItemPrice { get; set; } // 32, 33
        public Byte[] ItemUnused { get; set; } // 34-71

        ///////////////// RECIPES /////////////////
        public UInt16 ClavatCraftedItem { get; set; } // 56
        public UInt16 LiltyCraftedItem { get; set; } // 58
        public UInt16 YukeCraftedItem { get; set; } // 60
        public UInt16 SelkieCraftedItem { get; set; } // 62

        ///////////////// DERIVATIVE /////////////////
        public Int16 Strength { get; set; }
        public Int16 Defense { get; set; }

        ///////////////// EXTERNAL /////////////////
        public UInt16 Index { get; set; }
        public UInt64 Address { get; set; }
        public UInt64 RawAddress { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            // UNIVERSAL
            this.RaisePropertyChanged(nameof(this.BaseItemId));

            // EQUIPMENT
            this.RaisePropertyChanged(nameof(this.ModelFx));
            this.RaisePropertyChanged(nameof(this.ModelId));
            this.RaisePropertyChanged(nameof(this.Slot));
            this.RaisePropertyChanged(nameof(this.Tribe));
            this.RaisePropertyChanged(nameof(this.BonusValue));
            this.RaisePropertyChanged(nameof(this.BonusType));
            this.RaisePropertyChanged(nameof(this.Focus));
            this.RaisePropertyChanged(nameof(this.WeaponUnusedByes1));
            this.RaisePropertyChanged(nameof(this.ItemFXSize));
            this.RaisePropertyChanged(nameof(this.ItemFX1));
            this.RaisePropertyChanged(nameof(this.ItemFX2));
            this.RaisePropertyChanged(nameof(this.ItemFX3));
            this.RaisePropertyChanged(nameof(this.ItemFX4));
            this.RaisePropertyChanged(nameof(this.ItemFX5));
            this.RaisePropertyChanged(nameof(this.ItemFX6));
            this.RaisePropertyChanged(nameof(this.ItemFX7));
            this.RaisePropertyChanged(nameof(this.WeaponUnusedByes3));
            this.RaisePropertyChanged(nameof(this.SwingSound1));
            this.RaisePropertyChanged(nameof(this.SwingSound1Delay));
            this.RaisePropertyChanged(nameof(this.SwingSound2));
            this.RaisePropertyChanged(nameof(this.SwingSound2Delay));
            this.RaisePropertyChanged(nameof(this.SwingHitSound));
            this.RaisePropertyChanged(nameof(this.HitSound));
            this.RaisePropertyChanged(nameof(this.WeaponUnusedByes4));

            // ITEMS
            this.RaisePropertyChanged(nameof(this.ItemUnknown1));
            this.RaisePropertyChanged(nameof(this.ItemUnknown2));
            this.RaisePropertyChanged(nameof(this.ItemSpell));
            this.RaisePropertyChanged(nameof(this.ItemUnknown3));
            this.RaisePropertyChanged(nameof(this.ItemPrice));
            this.RaisePropertyChanged(nameof(this.ItemUnused));

            // RECIPES
            this.RaisePropertyChanged(nameof(this.ClavatCraftedItem));
            this.RaisePropertyChanged(nameof(this.LiltyCraftedItem));
            this.RaisePropertyChanged(nameof(this.YukeCraftedItem));
            this.RaisePropertyChanged(nameof(this.SelkieCraftedItem));

            this.RaisePropertyChanged(nameof(this.Index));
            this.RaisePropertyChanged(nameof(this.Address));
            this.RaisePropertyChanged(nameof(this.RawAddress));
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

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0xFFFF * 72)] // Intended is 0x4B5
    public class ItemCatalogDataSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xFFFF * 72)] // Intended is 0x4B5
        public Byte[] rawItemCatalogSlots;
    }

    public class ItemCatalogData
    {
        public ItemCatalogData()
        {
            this.SerializableData = new ItemCatalogDataSerializable();
        }

        public ItemCatalogDataSerializable SerializableData { get; set; }

        public UInt64 Address { get; set; }

        public UInt64 RawAddress { get; set; }

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

        public void Refresh(UInt64 address, UInt64 rawAddress, Byte[] bytes)
        {
            for (Int32 index = 0; index < bytes.Length / StructSize; index++)
            {
                if (index >= this.rawItems.Count || this.rawItems[index] == null)
                {
                    this.rawItems.Add(new RawItemCatalogItemEntry());
                }

                // UNIVERSAL
                this.rawItems[index].BaseItemId = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 0));

                // EQUIPMENT
                this.rawItems[index].ModelFx = bytes[index * StructSize + 2];
                this.rawItems[index].ModelId = bytes[index * StructSize + 3];
                this.rawItems[index].Slot = bytes[index * StructSize + 4];
                this.rawItems[index].Tribe = bytes[index * StructSize + 5];
                this.rawItems[index].BonusValue = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 6));
                this.rawItems[index].BonusType = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 8));
                this.rawItems[index].Focus = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 10));
                this.rawItems[index].WeaponUnusedByes1 = null; // // 12, 13, 14, 15
                this.rawItems[index].ItemFXSize = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 16));
                this.rawItems[index].ItemFX1 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 18));
                this.rawItems[index].ItemFX2 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 20));
                this.rawItems[index].ItemFX3 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 22));
                this.rawItems[index].ItemFX4 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 24));
                this.rawItems[index].ItemFX5 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 26));
                this.rawItems[index].ItemFX6 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 28));
                this.rawItems[index].ItemFX7 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 30));
                this.rawItems[index].WeaponUnusedByes3 = null; // 32-55
                this.rawItems[index].SwingSound1 = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 56));
                this.rawItems[index].SwingSound1Delay = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 58));
                this.rawItems[index].SwingSound2 = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 60));
                this.rawItems[index].SwingSound2Delay = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 62));
                this.rawItems[index].SwingHitSound = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 64));
                this.rawItems[index].HitSound = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 66));
                this.rawItems[index].WeaponUnusedByes4 = null; // 68, 69, 70, 71

                // EQUIPMENT
                // this.rawItems[index].ModelFx = bytes[index * StructSize + 2];
                // this.rawItems[index].ModelId = bytes[index * StructSize + 3];
                this.rawItems[index].ItemUnknown1 = null; // 4, 5
                this.rawItems[index].ItemUnknown2 = null; // 8, 9
                this.rawItems[index].ItemSpell = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 10));
                this.rawItems[index].ItemUnknown3 = null; // 12, 13, 14, 15
                this.rawItems[index].ItemPrice = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 32));
                this.rawItems[index].ItemUnused = null; // 34-71

                // RECIPE
                this.rawItems[index].ClavatCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 56));
                this.rawItems[index].LiltyCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 58));
                this.rawItems[index].YukeCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 60));
                this.rawItems[index].SelkieCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 62));

                if (false
                    || this.rawItems[index].ClavatCraftedItem == 0x021A
                    //|| this.rawItems[index].LiltyCraftedItem == 0x022D
                    // || this.rawItems[index].YukeCraftedItem == 0x022D
                    // || this.rawItems[index].SelkieCraftedItem == 0x022D
                    )
                {
                    int bp = 5;
                }

                // EXTERNAL
                this.rawItems[index].Index = (UInt16)index;
                this.rawItems[index].Address = address + (UInt64)(index * StructSize);
                this.rawItems[index].RawAddress = rawAddress + (UInt64)(index * StructSize);
                // this.rawItems[index].Refresh();

                // DERIVATIE
                Int16 bonusValue = this.rawItems[index].BonusValue;
                UInt16 baseItemId = this.rawItems[index].BaseItemId;

                if (baseItemId == 1)
                {
                    this.rawItems[index].BonusValue = bonusValue;
                }
                else if (baseItemId == 0x45)
                {
                    this.rawItems[index].Defense = bonusValue;
                }
             }

            this.Address = address;
            this.RawAddress = rawAddress;
        }
    }
    //// End class
}
//// End namespace
