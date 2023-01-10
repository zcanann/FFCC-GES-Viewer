namespace GES.Source.CraftViewer
{
    using System;
    using System.ComponentModel;

    public class CraftDataView : INotifyPropertyChanged
    {
        public CraftDataView(CraftData CraftData)
        {
            this.CraftData = CraftData;
        }

        public CraftData CraftData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int PlayerIndex
        {
            get
            {
                return CraftData.PlayerIndex + 1;
            }

            set
            {
                CraftData.PlayerIndex = value;
            }
        }

        public Byte ItemCount
        {
            get
            {
                return CraftData.SerializableData.itemCount;
            }

            set
            {
                CraftData.SerializableData.itemCount = value;
            }
        }

        public CraftEntry[] CraftSlotList
        {
            get
            {
                return CraftData.craftSlotList;
            }

            set
            {
                CraftData.craftSlotList = value;
            }
        }

        public String JISText
        {
            get
            {
                return CraftData.JISText;
            }

            set
            {
                CraftData.JISText = value;
            }
        }

        public String BytesText
        {
            get
            {
                return CraftData.BytesText;
            }

            set
            {
                CraftData.BytesText = value;
            }
        }

        public UInt64 Address
        {
            get
            {
                return CraftData.Address;
            }

            set
            {
                CraftData.Address = value;
            }
        }

        public UInt64 RawAddress
        {
            get
            {
                return CraftData.RawAddress;
            }

            set
            {
                CraftData.RawAddress = value;
            }
        }

        public void RefreshAllProperties()
        {
            RaisePropertyChanged(nameof(PlayerIndex));
            RaisePropertyChanged(nameof(ItemCount));
            RaisePropertyChanged(nameof(CraftSlotList));
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
