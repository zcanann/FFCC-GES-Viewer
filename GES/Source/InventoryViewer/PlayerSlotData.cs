
namespace GES.Source.InventoryViewer
{
    using GES.Source.EquipmentViewer;
    using System;
    using System.Buffers.Binary;
    using System.Runtime.InteropServices;

    public class RawItemEntry
    {
        public UInt16 ItemId { get; set; }

        public Byte Index { get; set; }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 724)]
    public class PlayerSlotData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public Byte[] unknown;

        [MarshalAs(UnmanagedType.I1)]
        public Byte isLoading;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public Byte[] unknown1;

        [MarshalAs(UnmanagedType.I1)]
        public Byte health;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 142)]
        public Byte[] unknown2;

        [MarshalAs(UnmanagedType.I2)]
        public Int16 equipmentWeapon;

        [MarshalAs(UnmanagedType.I2)]
        public Int16 equipmentArmor;

        [MarshalAs(UnmanagedType.I2)]
        public Int16 equipmentTribal;

        [MarshalAs(UnmanagedType.I2)]
        public Int16 equipmentAccessory;

        [MarshalAs(UnmanagedType.I2)]
        public Int16 inventoryItemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)] // 128 bytes
        public Int16[] items;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)] // 192 bytes
        public Int16[] artifacts;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)] // 8 bytes
        public Int16[] treasures;

        [MarshalAs(UnmanagedType.I2)]
        public UInt16 unknown4;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 gil;

        [MarshalAs(UnmanagedType.I4)]
        public Int32 unknown5;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)] // 12 bytes
        public UInt16[] commandListInventorySlotRef;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 160)]
        public Byte[] unknown6;

        public Int32 PlayerSlotIndex { get; set; }

        public RawItemEntry[] rawItems;

        public static void Deserialize(PlayerSlotData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    Marshal.PtrToStructure<PlayerSlotData>(handle.AddrOfPinnedObject(), entry);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(Byte[] bytes, Int32 playerSlotIndex)
        {
            // Pull out the full "out of bounds inventory" range that bleeds into other memory (artifacts, gil, etc)
            Span<Byte> inventoryBytes = new Span<Byte>(bytes).Slice(212);
            Span<UInt16> inventoryBytesRaw = MemoryMarshal.Cast<Byte, UInt16>(inventoryBytes);

            if (this.rawItems == null)
            {
                this.rawItems = new RawItemEntry[inventoryBytesRaw.Length];
            }

            for (Int32 index = 0; index < inventoryBytesRaw.Length; index++)
            {
                if (this.rawItems[index] == null)
                {
                    this.rawItems[index] = new RawItemEntry();
                }

                this.rawItems[index].ItemId = BinaryPrimitives.ReverseEndianness(inventoryBytesRaw[index]);
                this.rawItems[index].Index = (Byte)index;
            }

            this.PlayerSlotIndex = playerSlotIndex;

            this.equipmentWeapon = BinaryPrimitives.ReverseEndianness(this.equipmentWeapon);
            this.equipmentArmor = BinaryPrimitives.ReverseEndianness(this.equipmentArmor);
            this.equipmentTribal = BinaryPrimitives.ReverseEndianness(this.equipmentTribal);
            this.equipmentAccessory = BinaryPrimitives.ReverseEndianness(this.equipmentAccessory);

            // Fix GC endianness
            for (Int32 index = 0; index < this.items.Length; index++)
            {
                this.items[index] = BinaryPrimitives.ReverseEndianness(this.items[index]);
            }

            for (Int32 index = 0; index < this.artifacts.Length; index++)
            {
                this.artifacts[index] = BinaryPrimitives.ReverseEndianness(this.artifacts[index]);
            }

            for (Int32 index = 0; index < this.commandListInventorySlotRef.Length; index++)
            {
                this.commandListInventorySlotRef[index] = BinaryPrimitives.ReverseEndianness(this.commandListInventorySlotRef[index]);
            }

            this.gil = BinaryPrimitives.ReverseEndianness(this.gil);
        }
    }
    //// End class
}
//// End namespace
