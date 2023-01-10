
namespace GES.Source.CraftViewer
{
    using GES.Engine.Common.DataStructures;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = (1 + 256 + 256 * 56))]
    public class CraftDataSerializable
    {
        [MarshalAs(UnmanagedType.I1)]
        public Byte itemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256 + 256 * 56)]
        public Byte[] craftListRawData;
    }

    public class CraftData
    {
        public CraftData()
        {
            this.SerializableData = new CraftDataSerializable();
        }

        public CraftDataSerializable SerializableData;

        public Int32 PlayerIndex { get; set; }

        public CraftEntry[] craftSlotList;

        public String JISText { get; set; }

        public String BytesText { get; set; }

        public UInt64 Address { get; set; }

        public UInt64 RawAddress { get; set; }

        public static void Deserialize(CraftData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new CraftDataSerializable();
                    }

                    Marshal.PtrToStructure<CraftDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
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

            // if (this.craftSlotList == null)
            {
                this.craftSlotList = new CraftEntry[256];
            }

            for (Int32 index = 0; index < 256; index++)
            {
                Byte[] recipeProperties = new Byte[56];
                Array.Copy(this.SerializableData.craftListRawData, propertiesStart + index * 56, recipeProperties, 0, 56);

                if (this.craftSlotList[index] == null)
                {
                    this.craftSlotList[index] = new CraftEntry();
                }

                this.craftSlotList[index].Parent = this;
                this.craftSlotList[index].ItemSlotId = this.SerializableData.craftListRawData[index];
                this.craftSlotList[index].Properties = recipeProperties;
                this.craftSlotList[index].SlotId = (Byte)index;
                this.craftSlotList[index].Refresh();
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
