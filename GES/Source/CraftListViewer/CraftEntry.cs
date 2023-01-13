namespace GES.Source.CraftListViewer
{
    using System;
    using System.ComponentModel;

    public class CraftEntry : INotifyPropertyChanged
    {
        const Int32 IndexClavat = 24;
        const Int32 IndexLilty = 32;
        const Int32 IndexYuke = 40;
        const Int32 IndexSelkie = 48;

        private Byte[] properties;

        public CraftEntry()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public CraftData Parent { get; set; }

        public Byte SlotId { get; set; }

        public Byte ItemSlotId { get; set; }

        // Bytes 0-4: craft price in Gil
        // Bytes 5-6: Required item #1
        // Bytes 6-7: Required item #2
        // Bytes 7-8: Required item #3
        // Bytes 9-10: Required item #1 count
        // Bytes 10-11: Required item #2 count
        // Bytes 12-13: Required item #3 count

        // Byte ?: Gender(32 = female, 16 = male). Female flag requries no tribe flag set (?)
        // Byte 32: Tribe(1 = Clavat, 2 = Lilty, 4 = Yuke 8 = Selkie)
        // Byte 32: Slot(1 = weapon , 2 = tribal, 4 = armor, 0-1 = accessory) EXACT MATCH
        public Byte[] Properties
        {
            get
            {
                return properties;
            }
            set
            {
                properties = value;
            }
        }

        public Int32 CraftPrice
        {
            get
            {
                return this.HasProperties ? BitConverter.ToInt32(this.Properties, 0) : 0;
            }
        }

        public UInt16 RequiredItem1
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 4) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem2
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 6) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem3
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 8) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem1Count
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 10) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem2Count
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 12) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem3Count
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 14) : (UInt16)0;
            }
        }

        public UInt16 CraftedItemIdVisualFakeClavat
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 16) : (UInt16)0;
            }
        }

        public UInt16 CraftedItemIdVisualFakeLilty
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 18) : (UInt16)0;
            }
        }

        public UInt16 CraftedItemIdVisualFakeYuke
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 20) : (UInt16)0;
            }
        }

        public UInt16 CraftedItemIdVisualFakeSelkie
        {
            get
            {
                return this.HasProperties ? BitConverter.ToUInt16(this.Properties, 22) : (UInt16)0;
            }
        }

        public Boolean IsClavatM
        {
            get
            {
                return this.IsClavat(IndexClavat) && this.IsMale(IndexClavat);
            }
        }

        public Boolean IsLiltyM
        {
            get
            {
                return this.IsLilty(IndexLilty) && this.IsMale(IndexLilty);
            }
        }

        public Boolean IsYukeM
        {
            get
            {
                return this.IsYuke(IndexYuke) && this.IsMale(IndexYuke);
            }
        }

        public Boolean IsSelkieM
        {
            get
            {
                return this.IsSelkie(IndexSelkie) && this.IsMale(IndexSelkie);
            }
        }

        public Boolean IsClavatF
        {
            get
            {
                return this.IsClavat(IndexClavat) && this.IsFemale(IndexClavat);
            }
        }

        public Boolean IsLiltyF
        {
            get
            {
                return this.IsLilty(IndexLilty) && this.IsFemale(IndexLilty);
            }
        }

        public Boolean IsYukeF
        {
            get
            {
                return this.IsYuke(IndexYuke) && this.IsFemale(IndexYuke);
            }
        }

        public Boolean IsSelkieF
        {
            get
            {
                return this.IsSelkie(IndexSelkie) && this.IsFemale(IndexSelkie);
            }
        }

        public Boolean IsClassless(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b1111) == 0;
        }

        public Boolean IsClavat(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b0001) != 0;
        }

        public Boolean IsLilty(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b0010) != 0;
        }

        public Boolean IsYuke(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b0100) != 0;
        }

        public Boolean IsSelkie(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b1000) != 0;
        }

        public Boolean IsClavatWeapon
        {
            get
            {
                return this.IsWeapon(IndexClavat + 1);
            }
        }

        public Boolean IsClavatChest
        {
            get
            {
                return this.IsChest(IndexClavat + 1);
            }
        }

        public Boolean IsClavatTribal
        {
            get
            {
                return this.IsTribal(IndexClavat + 1);
            }
        }

        public Boolean IsClavatAccessory
        {
            get
            {
                return this.IsAccessory(IndexClavat + 1);
            }
        }

        public Boolean IsLiltyWeapon
        {
            get
            {
                return this.IsWeapon(IndexLilty + 1);
            }
        }

        public Boolean IsLiltyChest
        {
            get
            {
                return this.IsChest(IndexLilty + 1);
            }
        }

        public Boolean IsLiltyTribal
        {
            get
            {
                return this.IsTribal(IndexLilty + 1);
            }
        }

        public Boolean IsLiltyAccessory
        {
            get
            {
                return this.IsAccessory(IndexLilty + 1);
            }
        }

        public Boolean IsYukeWeapon
        {
            get
            {
                return this.IsWeapon(IndexYuke + 1);
            }
        }

        public Boolean IsYukeChest
        {
            get
            {
                return this.IsChest(IndexYuke + 1);
            }
        }

        public Boolean IsYukeTribal
        {
            get
            {
                return this.IsTribal(IndexYuke + 1);
            }
        }

        public Boolean IsYukeAccessory
        {
            get
            {
                return this.IsAccessory(IndexYuke + 1);
            }
        }

        public Boolean IsSelkieWeapon
        {
            get
            {
                return this.IsWeapon(IndexSelkie + 1);
            }
        }

        public Boolean IsSelkieChest
        {
            get
            {
                return this.IsChest(IndexSelkie + 1);
            }
        }

        public Boolean IsSelkieTribal
        {
            get
            {
                return this.IsTribal(IndexSelkie + 1);
            }
        }

        public Boolean IsSelkieAccessory
        {
            get
            {
                return this.IsAccessory(IndexSelkie + 1);
            }
        }

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.Parent));
            this.RaisePropertyChanged(nameof(this.SlotId));
            this.RaisePropertyChanged(nameof(this.ItemSlotId));
            this.RaisePropertyChanged(nameof(this.Properties));
            this.RaisePropertyChanged(nameof(this.CraftPrice));
            this.RaisePropertyChanged(nameof(this.RequiredItem1));
            this.RaisePropertyChanged(nameof(this.RequiredItem2));
            this.RaisePropertyChanged(nameof(this.RequiredItem3));
            this.RaisePropertyChanged(nameof(this.RequiredItem1Count));
            this.RaisePropertyChanged(nameof(this.RequiredItem2Count));
            this.RaisePropertyChanged(nameof(this.RequiredItem3Count));
            this.RaisePropertyChanged(nameof(this.CraftedItemIdVisualFakeClavat));
            this.RaisePropertyChanged(nameof(this.CraftedItemIdVisualFakeLilty));
            this.RaisePropertyChanged(nameof(this.CraftedItemIdVisualFakeYuke));
            this.RaisePropertyChanged(nameof(this.CraftedItemIdVisualFakeSelkie));
            this.RaisePropertyChanged(nameof(this.IsClavatM));
            this.RaisePropertyChanged(nameof(this.IsLiltyM));
            this.RaisePropertyChanged(nameof(this.IsYukeM));
            this.RaisePropertyChanged(nameof(this.IsSelkieM));
            this.RaisePropertyChanged(nameof(this.IsClavatF));
            this.RaisePropertyChanged(nameof(this.IsLiltyF));
            this.RaisePropertyChanged(nameof(this.IsYukeF));
            this.RaisePropertyChanged(nameof(this.IsSelkieF));
            this.RaisePropertyChanged(nameof(this.IsClavatWeapon));
            this.RaisePropertyChanged(nameof(this.IsClavatChest));
            this.RaisePropertyChanged(nameof(this.IsClavatTribal));
            this.RaisePropertyChanged(nameof(this.IsClavatAccessory));
            this.RaisePropertyChanged(nameof(this.IsLiltyWeapon));
            this.RaisePropertyChanged(nameof(this.IsLiltyChest));
            this.RaisePropertyChanged(nameof(this.IsLiltyTribal));
            this.RaisePropertyChanged(nameof(this.IsLiltyAccessory));
            this.RaisePropertyChanged(nameof(this.IsYukeWeapon));
            this.RaisePropertyChanged(nameof(this.IsYukeChest));
            this.RaisePropertyChanged(nameof(this.IsYukeTribal));
            this.RaisePropertyChanged(nameof(this.IsYukeAccessory));
            this.RaisePropertyChanged(nameof(this.IsSelkieWeapon));
            this.RaisePropertyChanged(nameof(this.IsSelkieChest));
            this.RaisePropertyChanged(nameof(this.IsSelkieTribal));
            this.RaisePropertyChanged(nameof(this.IsSelkieAccessory));
        }

        /// <summary>
        /// Highest priority
        /// </summary>
        public Boolean IsWeapon(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b0001) != 0;
        }

        /// <summary>
        /// Two flags represent chest. One is higher priority than tribal, but one is less.
        /// </summary>
        public Boolean IsChest(Int32 index)
        {
            return this.HasProperties
                && !this.IsWeapon(index)
                && ((this.Properties[index] & 0b0100) != 0 // Higher priority flag (4)
                    || (((this.Properties[index] & 0b1000) != 0) && (this.Properties[index] & 0b0010) == 0)); // Lower priority flag (8), contingent on no tribal flag (2)
        }

        public Boolean IsTribal(Int32 index)
        {
            return this.HasProperties 
                && !this.IsWeapon(index)
                && !this.IsChest(index)
                && (this.Properties[index] & 0b0010) != 0;
        }

        public Boolean IsAccessory(Int32 index)
        {
            return this.HasProperties && (this.Properties[index] & 0b1111) == 0;
        }

        public Boolean IsMale(Int32 index)
        {
            Boolean maleOnly = (this.Properties[index] & 0b010000) != 0;
            Boolean femaleOnly = (this.Properties[index] & 0b100000) != 0;

            return this.HasProperties && (maleOnly || !femaleOnly);
        }

        public Boolean IsFemale(Int32 index)
        {
            Boolean maleOnly = (this.Properties[index] & 0b010000) != 0;
            Boolean femaleOnly = (this.Properties[index] & 0b100000) != 0;

            return this.HasProperties && (femaleOnly || !maleOnly);
        }

        private Boolean HasProperties
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 56;
            }
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
