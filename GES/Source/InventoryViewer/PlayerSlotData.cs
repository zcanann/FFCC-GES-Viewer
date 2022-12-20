
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
    public class PlayerSlotDataSerializable
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
    }

    public class PlayerSlotData
    {
        public PlayerSlotData()
        {
            this.SerializableData = new PlayerSlotDataSerializable();
        }

        public PlayerSlotDataSerializable SerializableData { get; set; }

        public Int32 PlayerSlotIndex { get; set; }

        public RawItemEntry[] rawItems;

        public static void Deserialize(PlayerSlotData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new PlayerSlotDataSerializable();
                    }

                    Marshal.PtrToStructure<PlayerSlotDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(Byte[] bytes, Int32 playerSlotIndex)
        {
            // Pull out the full "out of bounds inventory" range that bleeds into other memory (artifacts, gil, etc)
            Span<Byte> inventoryBytes = new Span<Byte>(bytes).Slice(214);
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

            this.SerializableData.equipmentWeapon = BinaryPrimitives.ReverseEndianness(this.SerializableData.equipmentWeapon);
            this.SerializableData.equipmentArmor = BinaryPrimitives.ReverseEndianness(this.SerializableData.equipmentArmor);
            this.SerializableData.equipmentTribal = BinaryPrimitives.ReverseEndianness(this.SerializableData.equipmentTribal);
            this.SerializableData.equipmentAccessory = BinaryPrimitives.ReverseEndianness(this.SerializableData.equipmentAccessory);
            this.SerializableData.inventoryItemCount = BinaryPrimitives.ReverseEndianness(this.SerializableData.inventoryItemCount);

            // Fix GC endianness
            for (Int32 index = 0; index < this.SerializableData.items.Length; index++)
            {
                this.SerializableData.items[index] = BinaryPrimitives.ReverseEndianness(this.SerializableData.items[index]);
            }

            for (Int32 index = 0; index < this.SerializableData.artifacts.Length; index++)
            {
                this.SerializableData.artifacts[index] = BinaryPrimitives.ReverseEndianness(this.SerializableData.artifacts[index]);
            }

            for (Int32 index = 0; index < this.SerializableData.commandListInventorySlotRef.Length; index++)
            {
                this.SerializableData.commandListInventorySlotRef[index] = BinaryPrimitives.ReverseEndianness(this.SerializableData.commandListInventorySlotRef[index]);
            }

            this.SerializableData.gil = BinaryPrimitives.ReverseEndianness(this.SerializableData.gil);
        }
    }
    //// End class
}
//// End namespace
