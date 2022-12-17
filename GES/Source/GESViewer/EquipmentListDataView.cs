
namespace GES.Source.GESViewer
{
    using System;
    using System.ComponentModel;

    public class EquipmentListDataView : INotifyPropertyChanged
    {
        public EquipmentListDataView(EquipmentListData equipmentList)
        {
            this.EquipmentList = equipmentList;
        }

        public EquipmentListData EquipmentList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Int32 PlayerIndex
        {
            get
            {
                return this.EquipmentList.PlayerIndex + 1;
            }

            set
            {
                this.EquipmentList.PlayerIndex = value;
            }
        }

        public Byte ItemCount
        {
            get
            {
                return this.EquipmentList.itemCount;
            }

            set
            {
                this.EquipmentList.itemCount = value;
            }
        }

        public EquipmentEntry[] EquipmentSlotList
        {
            get
            {
                return this.EquipmentList.equipmentSlotList;
            }

            set
            {
                this.EquipmentList.equipmentSlotList = value;
            }
        }

        public void RefreshAllProperties()
        {
            this.RaisePropertyChanged(nameof(this.PlayerIndex));
            this.RaisePropertyChanged(nameof(this.ItemCount));
            this.RaisePropertyChanged(nameof(this.EquipmentSlotList));
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
