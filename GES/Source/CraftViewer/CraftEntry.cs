namespace GES.Source.CraftViewer
{
    using System;
    using System.Buffers.Binary;

    public class CraftEntry
    {
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
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 4)) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem2
        {
            get
            {
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 6)) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem3
        {
            get
            {
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 8)) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem1Count
        {
            get
            {
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 10)) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem2Count
        {
            get
            {
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 12)) : (UInt16)0;
            }
        }

        public UInt16 RequiredItem3Count
        {
            get
            {
                return this.HasProperties ? BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(this.Properties, 14)) : (UInt16)0;
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
                return this.IsClavat && this.IsMale;
            }
        }

        public Boolean IsLiltyM
        {
            get
            {
                return this.IsLilty && this.IsMale;
            }
        }

        public Boolean IsYukeM
        {
            get
            {
                return this.IsYuke && this.IsMale;
            }
        }

        public Boolean IsSelkieM
        {
            get
            {
                return this.IsSelkie && this.IsMale;
            }
        }

        public Boolean IsClavatF
        {
            get
            {
                return (this.IsClavat && this.IsFemale) || (this.IsClassless && this.IsFemale);
            }
        }

        public Boolean IsLiltyF
        {
            get
            {
                return (this.IsLilty && this.IsFemale) || (this.IsClassless && this.IsFemale);
            }
        }

        public Boolean IsYukeF
        {
            get
            {
                return (this.IsYuke && this.IsFemale) || (this.IsClassless && this.IsFemale);
            }
        }

        public Boolean IsSelkieF
        {
            get
            {
                return (this.IsSelkie && this.IsFemale) || (this.IsClassless && this.IsFemale);
            }
        }

        public Boolean IsClassless
        {
            get
            {
                return this.HasProperties && (this.Properties[32] & 0b1111) == 0;
            }
        }

        public Boolean IsClavat
        {
            get
            {
                return this.HasProperties && (this.Properties[24] & 0b0001) != 0;
            }
        }

        public Boolean IsLilty
        {
            get
            {
                return this.HasProperties && (this.Properties[32] & 0b0010) != 0;
            }
        }

        public Boolean IsYuke
        {
            get
            {
                return this.HasProperties && (this.Properties[40] & 0b0100) != 0;
            }
        }

        public Boolean IsSelkie
        {
            get
            {
                return this.HasProperties && (this.Properties[48] & 0b1000) != 0;
            }
        }

        public Boolean IsClavatWeapon
        {
            get
            {
                return this.IsWeapon(25);
            }
        }

        public Boolean IsClavatChest
        {
            get
            {
                return this.IsChest(25);
            }
        }

        public Boolean IsClavatTribal
        {
            get
            {
                return this.IsTribal(25);
            }
        }

        public Boolean IsClavatAccessory
        {
            get
            {
                return this.IsAccessory(25);
            }
        }

        public Boolean IsLiltyWeapon
        {
            get
            {
                return this.IsWeapon(33);
            }
        }

        public Boolean IsLiltyChest
        {
            get
            {
                return this.IsChest(33);
            }
        }

        public Boolean IsLiltyTribal
        {
            get
            {
                return this.IsTribal(33);
            }
        }

        public Boolean IsLiltyAccessory
        {
            get
            {
                return this.IsAccessory(33);
            }
        }

        public Boolean IsYukeWeapon
        {
            get
            {
                return this.IsWeapon(41);
            }
        }

        public Boolean IsYukeChest
        {
            get
            {
                return this.IsChest(41);
            }
        }

        public Boolean IsYukeTribal
        {
            get
            {
                return this.IsTribal(41);
            }
        }

        public Boolean IsYukeAccessory
        {
            get
            {
                return this.IsAccessory(41);
            }
        }

        public Boolean IsSelkieWeapon
        {
            get
            {
                return this.IsWeapon(49);
            }
        }

        public Boolean IsSelkieChest
        {
            get
            {
                return this.IsChest(49);
            }
        }

        public Boolean IsSelkieTribal
        {
            get
            {
                return this.IsTribal(49);
            }
        }

        public Boolean IsSelkieAccessory
        {
            get
            {
                return this.IsAccessory(49);
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

        public Boolean IsFemale
        {
            get
            {
                return this.HasProperties && (this.Properties[32] & 0b100000) != 0;
            }
        }

        public Boolean IsMale
        {
            get
            {
                return this.HasProperties && ((this.Properties[32] & 0b010000) != 0 || !this.IsFemale);
            }
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
