
namespace GES.Source.InventoryViewer
{
    using GES.Engine.Common.DataStructures;
    using GES.Source.Mvvm.Converters;
    using System;
    using System.ComponentModel;

    public class PlayerSlotDataView : INotifyPropertyChanged
    {
        public PlayerSlotDataView(PlayerSlotData slot)
        {
            this.Slot = slot;
        }

        public PlayerSlotData Slot { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Int32 PlayerSlotIndex
        {
            get
            {
                return this.Slot.PlayerSlotIndex + 1;
            }

            set
            {
                this.Slot.PlayerSlotIndex = value;
            }
        }

        public Byte Health
        {
            get
            {
                return this.Slot.SerializableData.health;
            }

            set
            {
                this.Slot.SerializableData.health = value;
            }
        }

        public UInt16 EquipmentWeapon
        {
            get
            {
                return this.Slot.SerializableData.equipmentWeapon;
            }

            set
            {
                this.Slot.SerializableData.equipmentWeapon = value;
            }
        }

        public UInt16 EquipmentArmor
        {
            get
            {
                return this.Slot.SerializableData.equipmentArmor;
            }

            set
            {
                this.Slot.SerializableData.equipmentArmor = value;
            }
        }

        public UInt16 EquipmentTribal
        {
            get
            {
                return this.Slot.SerializableData.equipmentTribal;
            }

            set
            {
                this.Slot.SerializableData.equipmentTribal = value;
            }
        }

        public UInt16 EquipmentAccessory
        {
            get
            {
                return this.Slot.SerializableData.equipmentAccessory;
            }

            set
            {
                this.Slot.SerializableData.equipmentAccessory = value;
            }
        }

        public UInt16[] Items
        {
            get
            {
                return this.Slot.SerializableData.items;
            }

            set
            {
                this.Slot.SerializableData.items = value;
            }
        }

        public UInt16[] Artifacts
        {
            get
            {
                return this.Slot.SerializableData.artifacts;
            }

            set
            {
                this.Slot.SerializableData.artifacts = value;
            }
        }

        public FullyObservableCollection<RawItemEntry> RawItems
        {
            get
            {
                return this.Slot.rawItems;
            }

            set
            {
                this.Slot.rawItems = value;
            }
        }

        public RawEquipmentEntry[] RawEquipment
        {
            get
            {
                return this.Slot.rawEquipment;
            }

            set
            {
                this.Slot.rawEquipment = value;
            }
        }

        public void RefreshAllProperties()
        {
            this.RaisePropertyChanged(nameof(this.PlayerSlotIndex));
            this.RaisePropertyChanged(nameof(this.Health));
            this.RaisePropertyChanged(nameof(this.EquipmentWeapon));
            this.RaisePropertyChanged(nameof(this.EquipmentArmor));
            this.RaisePropertyChanged(nameof(this.EquipmentTribal));
            this.RaisePropertyChanged(nameof(this.EquipmentAccessory));
            this.RaisePropertyChanged(nameof(this.Items));
            this.RaisePropertyChanged(nameof(this.Artifacts));
            this.RaisePropertyChanged(nameof(this.RawItems));
            this.RaisePropertyChanged(nameof(this.RawEquipment));
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
    //// End class
}
//// End namespace
