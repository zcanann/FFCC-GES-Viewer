
namespace GES.Source.CraftListViewer
{
    using GES.Engine.Common.DataStructures;
    using GES.Source.EquipmentListViewer;
    using GES.Source.InventoryViewer;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = (1 + 256 + 256 * 56))]
    public class CraftDataSerializable
    {
        [MarshalAs(UnmanagedType.I1)]
        public Byte itemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256 + 256 * 56)]
        public Byte[] craftListRawData;
    }

    public class RawCommandListEntry : INotifyPropertyChanged
    {
        public UInt16 PlayerSlotId { get; set; }

        public UInt16 InventorySlotId { get; set; }

        public Int32 Index { get; set; }

        public UInt32 Address { get; set; }

        public UInt64 RawAddress { get; set; }

        public CraftData Parent { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.PlayerSlotId));
            this.RaisePropertyChanged(nameof(this.InventorySlotId));
            this.RaisePropertyChanged(nameof(this.Index));
            this.RaisePropertyChanged(nameof(this.Address));
            this.RaisePropertyChanged(nameof(this.RawAddress));
            this.RaisePropertyChanged(nameof(this.Parent));
        }

        /// <summary>
        /// Indicates that a given property in this project item has changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void RaisePropertyChanged(String propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CraftData
    {
        public CraftData()
        {
            this.SerializableData = new CraftDataSerializable();
        }

        public CraftDataSerializable SerializableData;

        public Int32 PlayerIndex { get; set; }

        public Int32 CommandListCount { get; set; }

        public CraftEntry[] craftSlotList;

        public RawCommandListEntry[] commandListItems;

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

        public void Refresh(UInt64 address, UInt64 rawAddress, Byte[] bytes, Int32 playerSlotIndex, Int32 commandListCount)
        {
            this.CommandListCount = commandListCount;
            this.PlayerIndex = playerSlotIndex;

            Int32 remainder = (this.SerializableData.itemCount + 1) % 4;
            Int32 propertiesStart = this.SerializableData.itemCount + (remainder == 0 ? 0 : 4 - remainder);

            // if (this.craftSlotList == null)
            {
                this.craftSlotList = new CraftEntry[256];
            }

            // if (this.CommandListItems == null)
            {
                this.commandListItems = new RawCommandListEntry[64];
            }

            for (Int32 index = 0; index < 64; index++)
            {
                if (this.commandListItems[index] == null)
                {
                    this.commandListItems[index] = new RawCommandListEntry();
                }

                this.commandListItems[index].Index = index;
                this.commandListItems[index].InventorySlotId = BitConverter.ToUInt16(bytes, index * sizeof(UInt16));
                this.commandListItems[index].PlayerSlotId = (UInt16)playerSlotIndex;
                this.commandListItems[index].Parent = this;
                this.commandListItems[index].Address = (UInt32)address + (UInt32)(index * 2);
                this.commandListItems[index].RawAddress = rawAddress + (UInt32)(index * 2);
                this.commandListItems[index].Parent = this;
                this.RawAddress = rawAddress;
                this.commandListItems[index].Refresh();
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
