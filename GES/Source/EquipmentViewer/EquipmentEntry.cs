namespace GES.Source.EquipmentViewer
{
    using System;

    public class EquipmentEntry
    {
        public EquipmentEntry(EquipmentData parent, Byte slotId, Byte itemSlotId, Byte[] properties)
        {
            this.Parent = parent;
            this.SlotId = slotId;
            this.ItemSlotId = itemSlotId; // BinaryPrimitives.ReverseEndianness(slotId);
            this.Properties = properties;
        }

        public EquipmentData Parent { get; set; }

        public Byte SlotId { get; set; }

        public Byte ItemSlotId { get; set; }

        // Byte 0: Tribe(1 = Clavat, 2 = Lilty, 4 = Yuke 8 = Selkie)
        // Byte 0: Gender(32 = female, 16 = male). Female flag requries no tribe flag set (?)
        // Byte 1: Slot(1 = weapon, 4 = armor, 8 = belt, 16-48 = accessory)
        public Byte[] Properties { get; set; }

        public Boolean IsClavatM
        {
            get
            {
                return this.IsClavat && (this.IsMaleOnly || !this.IsFemaleOnly);
            }
        }

        public Boolean IsLiltyM
        {
            get
            {
                return this.IsLilty && (this.IsMaleOnly || !this.IsFemaleOnly);
            }
        }

        public Boolean IsYukeM
        {
            get
            {
                return this.IsYuke && (this.IsMaleOnly || !this.IsFemaleOnly);
            }
        }

        public Boolean IsSelkieM
        {
            get
            {
                return this.IsSelkie && (this.IsMaleOnly || !this.IsFemaleOnly);
            }
        }

        public Boolean IsClavatF
        {
            get
            {
                return (this.IsClavat && !this.IsMaleOnly) || (this.IsClassless && this.IsFemaleOnly);
            }
        }

        public Boolean IsLiltyF
        {
            get
            {
                return (this.IsLilty && !this.IsMaleOnly) || (this.IsClassless && this.IsFemaleOnly);
            }
        }

        public Boolean IsYukeF
        {
            get
            {
                return (this.IsYuke && !this.IsMaleOnly) || (this.IsClassless && this.IsFemaleOnly);
            }
        }

        public Boolean IsSelkieF
        {
            get
            {
                return (this.IsSelkie && !this.IsMaleOnly) || (this.IsClassless && this.IsFemaleOnly);
            }
        }

        public Boolean IsClassless
        {
            get
            {
                return this.HasProperties1 && (this.Properties[0] & 0b1111) == 0;
            }
        }

        public Boolean IsClavat
        {
            get
            {
                return this.HasProperties1 && (this.Properties[0] & 0b0001) != 0;
            }
        }

        public Boolean IsLilty
        {
            get
            {
                return this.HasProperties1 && (this.Properties[0] & 0b0010) != 0;
            }
        }

        public Boolean IsYuke
        {
            get
            {
                return this.HasProperties1 && (this.Properties[0] & 0b0100) != 0;
            }
        }

        public Boolean IsSelkie
        {
            get
            {
                return this.HasProperties1 && (this.Properties[0] & 0b1000) != 0;
            }
        }

        public Boolean IsWeapon
        {
            get
            {
                return this.HasProperties2 && (this.Properties[1] & 0b0001) != 0;
            }
        }

        public Boolean IsChest
        {
            get
            {
                return this.HasProperties2 && (this.Properties[1] & 0b0100) != 0;
            }
        }

        public Boolean IsTribal
        {
            get
            {
                return this.HasProperties2 && ((this.Properties[1] & 0b0010) != 0 || (this.Properties[1] & 0b1000) != 0);
            }
        }

        public Boolean IsAccessory
        {
            get
            {
                return this.HasProperties2 && ((this.Properties[1] & 16) != 0 || (this.Properties[1] & 32) != 0);
            }
        }

        public Boolean IsFemaleOnly
        {
            get
            {
                return this.Properties != null
                    && this.Properties.Length >= 1
                    && (this.Properties[0] & 0b100000) != 0;
            }
        }

        public Boolean IsMaleOnly
        {
            get
            {
                return this.Properties != null
                    && this.Properties.Length >= 1
                    && (this.Properties[0] & 0b100000) != 0;
            }
        }

        private Boolean HasProperties1
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 1;
            }
        }

        private Boolean HasProperties2
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 2;
            }
        }
    }
    //// End class
}
//// End namespace
