﻿namespace GES.Source.EquipmentListViewer
{
    using System;
    using System.ComponentModel;

    public class EquipmentDataView : INotifyPropertyChanged
    {
        public EquipmentDataView(EquipmentData equipmentData)
        {
            this.EquipmentData = equipmentData;
        }

        public EquipmentData EquipmentData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int PlayerIndex
        {
            get
            {
                return EquipmentData.PlayerIndex + 1;
            }

            set
            {
                EquipmentData.PlayerIndex = value;
            }
        }

        public Byte ItemCount
        {
            get
            {
                return EquipmentData.SerializableData.itemCount;
            }

            set
            {
                EquipmentData.SerializableData.itemCount = value;
            }
        }

        public EquipmentEntry[] EquipmentSlotList
        {
            get
            {
                return EquipmentData.equipmentSlotList;
            }

            set
            {
                EquipmentData.equipmentSlotList = value;
            }
        }

        public String JISText
        {
            get
            {
                return EquipmentData.JISText;
            }

            set
            {
                EquipmentData.JISText = value;
            }
        }

        public String BytesText
        {
            get
            {
                return EquipmentData.BytesText;
            }

            set
            {
                EquipmentData.BytesText = value;
            }
        }

        public UInt64 Address
        {
            get
            {
                return EquipmentData.Address;
            }

            set
            {
                EquipmentData.Address = value;
            }
        }

        public UInt64 RawAddress
        {
            get
            {
                return EquipmentData.RawAddress;
            }

            set
            {
                EquipmentData.RawAddress = value;
            }
        }

        public void RefreshAllProperties()
        {
            RaisePropertyChanged(nameof(PlayerIndex));
            RaisePropertyChanged(nameof(ItemCount));
            RaisePropertyChanged(nameof(EquipmentSlotList));
            RaisePropertyChanged(nameof(JISText));
            RaisePropertyChanged(nameof(BytesText));
            RaisePropertyChanged(nameof(Address));
            RaisePropertyChanged(nameof(RawAddress));
        }

        /// <summary>
        /// Indicates that a given property in this project item has changed.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //// End class
}
//// End namespace
