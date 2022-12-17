
namespace GES.Source.GESViewer
{
    using System;
    using System.Buffers.Binary;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = (1 + 256 + 2048))]
    public class EquipmentListData
    {
        [MarshalAs(UnmanagedType.I1)]
        public Byte itemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256 + 2048)]
        public Byte[] freeSpace;

        public Int32 PlayerIndex { get; set; }

        public EquipmentEntry[] equipmentSlotList = new EquipmentEntry[256];

        public static EquipmentListData FromByteArray(Byte[] bytes, Int32 playerSlotIndex)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            if (handle.IsAllocated)
            {
                try
                {
                    EquipmentListData result = Marshal.PtrToStructure<EquipmentListData>(handle.AddrOfPinnedObject());

                    result.PlayerIndex = playerSlotIndex;

                    Int32 remainder = (result.itemCount + 1) % 4;
                    Int32 alignment = result.itemCount + (remainder == 0 ? 0 : (4 - remainder));

                    result.equipmentSlotList = new EquipmentEntry[256];

                    for (Int32 index = 0; index < 256; index++)
                    {
                        Byte[] itemProperties = new Byte[8];
                        Array.Copy(result.freeSpace, index * 8 + alignment, itemProperties, 0, 8);

                        result.equipmentSlotList[index] = new EquipmentEntry(result.freeSpace[index], itemProperties);
                    }

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
