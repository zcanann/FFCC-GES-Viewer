
namespace GES.Source.EquipmentViewer
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = (1 + 256 + 2048))]
    public class EquipmentData
    {
        [MarshalAs(UnmanagedType.I1)]
        public Byte itemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256 + 2048)]
        public Byte[] freeSpace;

        public Int32 PlayerIndex { get; set; }

        public EquipmentEntry[] equipmentSlotList;

        public void Refresh(Byte[] bytes, Int32 playerSlotIndex)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    Marshal.PtrToStructure<EquipmentData>(handle.AddrOfPinnedObject(), this);

                    this.PlayerIndex = playerSlotIndex;

                    Int32 remainder = (this.itemCount + 1) % 4;
                    Int32 propertiesStart = this.itemCount + (remainder == 0 ? 0 : 4 - remainder);

                    this.equipmentSlotList = new EquipmentEntry[256];

                    for (Int32 index = 0; index < 256; index++)
                    {
                        Byte[] itemProperties = new Byte[8];
                        Array.Copy(this.freeSpace, propertiesStart + index * 8, itemProperties, 0, 8);

                        this.equipmentSlotList[index] = new EquipmentEntry(this, (Byte)index, this.freeSpace[index], itemProperties);
                    }
                }
            }
            catch(Exception)
            {
            }
            finally
            {
                handle.Free();
            }
        }
    }
    //// End class
}
//// End namespace
