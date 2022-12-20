namespace GES.Source.EquipmentViewer
{
    using System;
    using System.ComponentModel;

    public class EquipmentDataView : INotifyPropertyChanged
    {
        public EquipmentDataView(EquipmentData equipmentData)
        {
            EquipmentData = equipmentData;
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

        public void RefreshAllProperties()
        {
            RaisePropertyChanged(nameof(PlayerIndex));
            RaisePropertyChanged(nameof(ItemCount));
            RaisePropertyChanged(nameof(EquipmentSlotList));
            RaisePropertyChanged(nameof(JISText));
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
