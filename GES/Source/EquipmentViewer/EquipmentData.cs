
namespace GES.Source.EquipmentViewer
{
    using GES.Source.InventoryViewer;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = (1 + 256 + 2048))]
    public class EquipmentDataSerializable
    {
        [MarshalAs(UnmanagedType.I1)]
        public Byte itemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256 + 2048)]
        public Byte[] equipmentListData;
    }

    public class EquipmentData
    {
        public EquipmentData()
        {
            this.SerializableData = new EquipmentDataSerializable();
        }

        public EquipmentDataSerializable SerializableData;

        public Int32 PlayerIndex { get; set; }

        public EquipmentEntry[] equipmentSlotList;

        public String JISText { get; set; }

        public String BytesText { get; set; }

        public UInt64 Address { get; set; }

        public UInt64 RawAddress { get; set; }

        public static void Deserialize(EquipmentData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new EquipmentDataSerializable();
                    }

                    Marshal.PtrToStructure<EquipmentDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(UInt64 address, UInt64 rawAddress, Byte[] bytes, Int32 playerSlotIndex)
        {
            this.PlayerIndex = playerSlotIndex;

            Int32 remainder = (this.SerializableData.itemCount + 1) % 4;
            Int32 propertiesStart = this.SerializableData.itemCount + (remainder == 0 ? 0 : 4 - remainder);

            // if (this.equipmentSlotList == null)
            {
                this.equipmentSlotList = new EquipmentEntry[256];
            }

            for (Int32 index = 0; index < 256; index++)
            {
                Byte[] itemProperties = new Byte[8];
                Array.Copy(this.SerializableData.equipmentListData, propertiesStart + index * 8, itemProperties, 0, 8);

                if (this.equipmentSlotList[index] == null)
                {
                    this.equipmentSlotList[index] = new EquipmentEntry();
                }

                this.equipmentSlotList[index].Parent = this;
                this.equipmentSlotList[index].SlotId = (Byte)index;
                this.equipmentSlotList[index].ItemSlotId = this.SerializableData.equipmentListData[index];
                this.equipmentSlotList[index].Properties = itemProperties;
            }

            this.Address = address;
            this.RawAddress = rawAddress;
            this.BytesText = BitConverter.ToString(bytes).Replace("-", " ");
            this.JISText = System.Text.Encoding.GetEncoding("shift-jis").GetString(bytes);
        }
    }
    //// End class
}
//// End namespace
