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

        // Byte 0: Race(1 = Clavat, 2 = Lilty, 4 = Yuke 8 = Selkie)
        // Byte 1: Slot(1 = weapon, 4 = armor, 8 = belt, 16-48 = accessory)
        public Byte[] Properties { get; set; }

        public Boolean IsClavat
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 1 && ((this.Properties[0] & 0b0001) != 0 || (this.Properties[0] & 0b1111) == 0);
            }
        }

        public Boolean IsLilty
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 1 && ((this.Properties[0] & 0b0010) != 0 || (this.Properties[0] & 0b1111) == 0);
            }
        }

        public Boolean IsYuke
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 1 && ((this.Properties[0] & 0b0100) != 0 || (this.Properties[0] & 0b1111) == 0);
            }
        }

        public Boolean IsSelkie
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 1 && ((this.Properties[0] & 0b1000) != 0 || (this.Properties[0] & 0b1111) == 0);
            }
        }

        public Boolean IsWeapon
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 2 && (this.Properties[1] & 0b0001) != 0;
            }
        }

        public Boolean IsChest
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 2 && (this.Properties[1] & 0b0100) != 0;
            }
        }

        public Boolean IsTribal
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 2 && ((this.Properties[1] & 0b0010) != 0 || (this.Properties[1] & 0b1000) != 0);
            }
        }

        public Boolean IsAccessory
        {
            get
            {
                return this.Properties != null && this.Properties.Length >= 2 && ((this.Properties[1] & 16) != 0 || (this.Properties[1] & 32) != 0);
            }
        }

        public Boolean IsFemaleOnly
        {
            get
            {
                return this.Properties != null
                    && this.Properties.Length >= 1
                    && ((this.Properties[0] >= 0x20 && this.Properties[0] <= 0x2F)
                        || (this.Properties[0] >= 0x60 && this.Properties[0] <= 0x6F)
                        || (this.Properties[0] >= 0xA0 && this.Properties[0] <= 0xAF)
                        || (this.Properties[0] >= 0xE0 && this.Properties[0] <= 0xEF));
            }
        }

        public Boolean IsMaleOnly
        {
            get
            {
                return this.Properties != null
                    && this.Properties.Length >= 1
                    && ((this.Properties[0] >= 0x10 && this.Properties[0] <= 0x1F)
                        || (this.Properties[0] >= 0x50 && this.Properties[0] <= 0x5F)
                        || (this.Properties[0] >= 0x90 && this.Properties[0] <= 0x9F)
                        || (this.Properties[0] >= 0xD0 && this.Properties[0] <= 0xDF));
            }
        }

        public Boolean IsMale
        {
            get
            {
                return !this.IsFemaleOnly;
            }
        }

        public Boolean IsFemale
        {
            get
            {
                return !this.IsMaleOnly;
            }
        }
    }
    //// End class
}
//// End namespace
