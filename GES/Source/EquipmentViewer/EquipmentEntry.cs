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
                return this.Properties != null && this.Properties.Length > 0 && (this.Properties[0] & 0b0001) == 0;
            }
        }

        public Boolean IsLilty
        {
            get
            {
                return this.Properties != null && this.Properties.Length > 0 && (this.Properties[0] & 0b0010) == 0;
            }
        }

        public Boolean IsYuke
        {
            get
            {
                return this.Properties != null && this.Properties.Length > 0 && (this.Properties[0] & 0b0100) == 0;
            }
        }

        public Boolean IsSelkie
        {
            get
            {
                return this.Properties != null && this.Properties.Length > 0 && (this.Properties[0] & 0b1000) == 0;
            }
        }

        public string AsString
        {
            get
            {
                return ToString();
            }
        }
    }
    //// End class
}
//// End namespace
