
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

        public UInt16 Index { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x04BA * 48)]
    public class CraftTableDataSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x04BA * 48)]
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
                this.rawItems = new RawCraftTableItemEntry[bytes.Length / 48];
            }

            for (Int32 index = 0; index < bytes.Length / 48; index++)
            {
                if (this.rawItems[index] == null)
                {
                    this.rawItems[index] = new RawCraftTableItemEntry();
                }

                this.rawItems[index].ClavatCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * 48 + 0));
                this.rawItems[index].LiltyCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * 48 + 2));
                this.rawItems[index].YukeCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * 48 + 4));
                this.rawItems[index].SelkieCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * 48 + 6));
                this.rawItems[index].Index = (UInt16)index;
            }
        }
    }
    //// End class
}
//// End namespace
