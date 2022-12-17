
namespace GES.Source.GESViewer
{
    using System;

    public class EquipmentEntry
    {
        public Byte SlotId { get; set; }

        // Byte 0: Race(1 = Clavat, 2 = Lilty, 4 = Yuke 8 = Selkie)
        // Byte 1: Slot(1 = weapon, 4 = armor, 8 = belt, 16-48 = accessory)
        public Byte[] Properties { get; set; }

        public String AsString
        {
            get
            {
                return this.ToString();
            }
        }

        public EquipmentEntry(Byte slotId, Byte[] properties)
        {
            this.SlotId = slotId; // BinaryPrimitives.ReverseEndianness(slotId);
            this.Properties = properties;
        }

        public override string ToString()
        {
            String result = this.SlotId.ToString() + " ";
            Boolean hasRace = (this.Properties[0] & 0b1111) != 0;
            Boolean hasSlot = (this.Properties[1] & 0b0001) != 0
                 || (this.Properties[1] & 0b0100) != 0
                 || (this.Properties[1] & 0b1000) != 0
                 || (this.Properties[1] & 16) != 0
                 || (this.Properties[1] & 32) != 0;

            if (hasRace || hasSlot)
            {
                result += "(";

                if (hasRace)
                {
                    if ((this.Properties[0] & 0b0001) != 0)
                    {
                        result += "C";
                    }
                    if ((this.Properties[0] & 0b0010) != 0)
                    {
                        result += "L";
                    }
                    if ((this.Properties[0] & 0b0100) != 0)
                    {
                        result += "Y";
                    }
                    if ((this.Properties[0] & 0b1000) != 0)
                    {
                        result += "S";
                    }
                    if (hasSlot)
                    {
                        result += "|";
                    }
                }
                if ((this.Properties[1] & 0b0001) != 0)
                {
                    result += "W";
                }
                if ((this.Properties[1] & 0b0100) != 0)
                {
                    result += "C";
                }
                if ((this.Properties[1] & 0b1000) != 0)
                {
                    result += "T";
                }
                if ((this.Properties[1] & 16) != 0 || (this.Properties[1] & 32) != 0)
                {
                    result += "A";
                }
                result += ")";
            }

            return result.TrimEnd();
        }
    }
    //// End class
}
//// End namespace
