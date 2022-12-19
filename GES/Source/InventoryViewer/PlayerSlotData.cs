
namespace GES.Source.InventoryViewer
{
    using System;
    using System.Buffers.Binary;
    using System.Runtime.InteropServices;

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

        public UInt16[] rawItems;

        public static PlayerSlotData FromByteArray(Byte[] bytes, Int32 playerSlotIndex)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            if (handle.IsAllocated)
            {
                try
                {
                    PlayerSlotData result = Marshal.PtrToStructure<PlayerSlotData>(handle.AddrOfPinnedObject());

                    // Pull out the full "out of bounds inventory" range that bleeds into other memory (artifacts, gil, etc)
                    Span<Byte> inventoryBytes = new Span<Byte>(bytes).Slice(212);
                    Span<UInt16> inventoryBytesRaw = MemoryMarshal.Cast<Byte, UInt16>(inventoryBytes);
                    result.rawItems = inventoryBytesRaw.ToArray();

                    for (Int32 index = 0; index < result.rawItems.Length; index++)
                    {
                        result.rawItems[index] = BinaryPrimitives.ReverseEndianness(result.rawItems[index]);
                    }

                    result.PlayerSlotIndex = playerSlotIndex;

                    result.equipmentWeapon = BinaryPrimitives.ReverseEndianness(result.equipmentWeapon);
                    result.equipmentArmor = BinaryPrimitives.ReverseEndianness(result.equipmentArmor);
                    result.equipmentTribal = BinaryPrimitives.ReverseEndianness(result.equipmentTribal);
                    result.equipmentAccessory = BinaryPrimitives.ReverseEndianness(result.equipmentAccessory);

                    // Fix GC endianness
                    for (Int32 index = 0; index < result.items.Length; index++)
                    {
                        result.items[index] = BinaryPrimitives.ReverseEndianness(result.items[index]);
                    }

                    for (Int32 index = 0; index < result.artifacts.Length; index++)
                    {
                        result.artifacts[index] = BinaryPrimitives.ReverseEndianness(result.artifacts[index]);
                    }

                    for (Int32 index = 0; index < result.commandListInventorySlotRef.Length; index++)
                    {
                        result.commandListInventorySlotRef[index] = BinaryPrimitives.ReverseEndianness(result.commandListInventorySlotRef[index]);
                    }

                    result.gil = BinaryPrimitives.ReverseEndianness(result.gil);

                    return result;
                }
                finally
                {
                    handle.Free();
                }
            }

            return null;
        }
    }
    //// End class
}
//// End namespace
