
namespace GES.Source.GESViewer
{
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

        public Byte Health
        {
            get
            {
                return this.Slot.health;
            }

            set
            {
                this.Slot.health = value;
            }
        }

        public Int16 EquipmentWeapon
        {
            get
            {
                return this.Slot.equipmentWeapon;
            }

            set
            {
                this.Slot.equipmentWeapon = value;
            }
        }

        public Int16 EquipmentArmor
        {
            get
            {
                return this.Slot.equipmentArmor;
            }

            set
            {
                this.Slot.equipmentArmor = value;
            }
        }

        public Int16 EquipmentTribal
        {
            get
            {
                return this.Slot.equipmentTribal;
            }

            set
            {
                this.Slot.equipmentTribal = value;
            }
        }

        public Int16 EquipmentAccessory
        {
            get
            {
                return this.Slot.equipmentAccessory;
            }

            set
            {
                this.Slot.equipmentAccessory = value;
            }
        }

        public Int16[] Items
        {
            get
            {
                return this.Slot.items;
            }

            set
            {
                this.Slot.items = value;
            }
        }

        public void RefreshAllProperties()
        {
            this.RaisePropertyChanged(nameof(this.Health));
            this.RaisePropertyChanged(nameof(this.EquipmentWeapon));
            this.RaisePropertyChanged(nameof(this.EquipmentArmor));
            this.RaisePropertyChanged(nameof(this.EquipmentTribal));
            this.RaisePropertyChanged(nameof(this.EquipmentAccessory));
            this.RaisePropertyChanged(nameof(this.Items));
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
