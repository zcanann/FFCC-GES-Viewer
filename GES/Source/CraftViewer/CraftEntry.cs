namespace GES.Source.CraftViewer
{
    using System;
    using System.Buffers.Binary;

    public class CraftEntry
    {
        const Int32 IndexClavat = 24;
        const Int32 IndexLilty = 32;
        const Int32 IndexYuke = 40;
        const Int32 IndexSelkie = 48;

        public CraftEntry(CraftData parent, Byte slotId, Byte itemSlotId, Byte[] properties)
        {
            this.Parent = parent;
            this.SlotId = slotId;
            this.ItemSlotId = itemSlotId; // BinaryPrimitives.ReverseEndianness(slotId);
            this.Properties = properties;
        }

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
        public Byte[] Properties { get; set; }

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

        public UInt16 CraftedItemIdVisualFake
        {
            get
            {
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 18)) : (UInt16)0;
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
    }
    //// End class
}
//// End namespace
