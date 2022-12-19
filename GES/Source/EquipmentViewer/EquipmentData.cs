
namespace GES.Source.EquipmentViewer
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = (1 + 256 + 2048))]
    public class EquipmentData
    {
        [MarshalAs(UnmanagedType.I1)]
        public byte itemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256 + 2048)]
        public byte[] freeSpace;

        public int PlayerIndex { get; set; }

        public EquipmentEntry[] equipmentSlotList;

        public static EquipmentData FromByteArray(byte[] bytes, int playerSlotIndex)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            if (handle.IsAllocated)
            {
                try
                {
                    EquipmentData result = Marshal.PtrToStructure<EquipmentData>(handle.AddrOfPinnedObject());

                    result.PlayerIndex = playerSlotIndex;

                    int remainder = (result.itemCount + 1) % 4;
                    int alignment = result.itemCount + (remainder == 0 ? 0 : 4 - remainder);

                    result.equipmentSlotList = new EquipmentEntry[256];

                    for (int index = 0; index < 256; index++)
                    {
                        byte[] itemProperties = new byte[8];
                        Array.Copy(result.freeSpace, index * 8 + alignment, itemProperties, 0, 8);

                        result.equipmentSlotList[index] = new EquipmentEntry(result, (Byte)index, result.freeSpace[index], itemProperties);
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
