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

        public byte ItemCount
        {
            get
            {
                return EquipmentData.itemCount;
            }

            set
            {
                EquipmentData.itemCount = value;
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

        public void RefreshAllProperties()
        {
            RaisePropertyChanged(nameof(PlayerIndex));
            RaisePropertyChanged(nameof(ItemCount));
            RaisePropertyChanged(nameof(EquipmentSlotList));
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
