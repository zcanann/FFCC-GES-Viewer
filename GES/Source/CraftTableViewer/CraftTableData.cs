
namespace GES.Source.CraftTableViewer
{
    using GES.Source.EquipmentViewer;
    using System;
    using System.Buffers.Binary;
    using System.Runtime.InteropServices;

    public class RawCraftTableItemEntry
    {
        public UInt16 ClavatCraftedItem { get; set; }

        public UInt16 LiltyCraftedItem { get; set; }

        public UInt16 YukeCraftedItem { get; set; }

        public UInt16 SelkieCraftedItem { get; set; }

        public Byte B16 { get; set; }

        public Byte B17 { get; set; }

        public Byte ModelFx { get; set; } // 18

        public Byte ModelId { get; set; } // 19

        public Byte Slot { get; set; }

        public UInt16 StatBoost { get; set; } // 22, 23

        public UInt16 Focus { get; set; } // 24, 25

        public UInt16 Magic { get; set; }

        public UInt16 Defense { get; set; }

        public UInt16 Strength { get; set; }

        public UInt16 Index { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x04BA * 72)]
    public class CraftTableDataSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x04BA * 72)]
        public Byte[] rawCraftTableSlots;
    }

    public class CraftTableData
    {
        public CraftTableData()
        {
            this.SerializableData = new CraftTableDataSerializable();
        }

        public CraftTableDataSerializable SerializableData { get; set; }

        public RawCraftTableItemEntry[] rawItems;

        const Int32 StructSize = 72;

        public static void Deserialize(CraftTableData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new CraftTableDataSerializable();
                    }

                    Marshal.PtrToStructure<CraftTableDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(Byte[] bytes)
        {
            if (this.rawItems == null)
            {
                this.rawItems = new RawCraftTableItemEntry[bytes.Length / StructSize];
            }

            for (Int32 index = 0; index < bytes.Length / StructSize; index++)
            {
                if (this.rawItems[index] == null)
                {
                    this.rawItems[index] = new RawCraftTableItemEntry();
                }

                this.rawItems[index].B16 = bytes[index * StructSize + 8];
                this.rawItems[index].B17 = bytes[index * StructSize + 9];
                this.rawItems[index].ModelFx = bytes[index * StructSize + 10];
                this.rawItems[index].ModelId = bytes[index * StructSize + 11];
                this.rawItems[index].Slot = bytes[index * StructSize + 12];
                this.rawItems[index].StatBoost = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 14));
                this.rawItems[index].Focus = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 16));
                this.rawItems[index].ClavatCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 64));
                this.rawItems[index].LiltyCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 66));
                this.rawItems[index].YukeCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 68));
                this.rawItems[index].SelkieCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 70));
                this.rawItems[index].Index = (UInt16)index;

                this.rawItems[index].Strength = this.rawItems[index].B16 == 0 ? this.rawItems[index].StatBoost : (UInt16)0;
            }
        }
    }
    //// End class
}
//// End namespace
