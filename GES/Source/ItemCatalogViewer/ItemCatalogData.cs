
namespace GES.Source.ItemCatalogViewer
{
    using GES.Engine.Common.DataStructures;
    using System;
    using System.Buffers.Binary;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RawItemCatalogItemEntry : INotifyPropertyChanged
    {
        ///////////////// UNIVERSAL /////////////////
        public UInt16 BaseItemId { get; set; } // 0, 1

        ///////////////// WEAPON /////////////////
        public Byte ModelFx { get; set; } // 2
        public Byte ModelId { get; set; } // 3
        public Byte Slot { get; set; } // 4
        public Byte Tribe { get; set; } // 5
        public Int16 BonusValue { get; set; } // 6, 7
        public UInt16 BonusType { get; set; } // 8, 9
        public Int16 Focus { get; set; } // 10, 11
        public Byte[] WeaponUnusedByes1 { get; set; } // 12, 13, 14, 15
        public UInt16 ItemFXSize { get; set; } // 16, 17
        public Int16 ItemFX1 { get; set; } // 18, 19
        public Int16 ItemFX2 { get; set; } // 20, 21
        public Int16 ItemFX3 { get; set; } // 22, 23
        public Int16 ItemFX4 { get; set; } // 24, 25
        public Int16 ItemFX5 { get; set; } // 26, 27
        public Int16 ItemFX6 { get; set; } // 28, 29
        public Int16 ItemFX7 { get; set; } // 30, 31
        public Byte[] WeaponUnusedByes3 { get; set; } // 32-55
        public UInt16 SwingSound1 { get; set; } // 56, 57
        public UInt16 SwingSound1Delay { get; set; } // 58, 59
        public UInt16 SwingSound2 { get; set; } // 60, 61
        public UInt16 SwingSound2Delay { get; set; } // 62, 63
        public UInt16 SwingHitSound { get; set; } // 64, 65
        public UInt16 HitSound { get; set; } // 66, 67
        public Byte[] WeaponUnusedByes4 { get; set; } // 68, 69, 70, 71

        ///////////////// ARTIFACTS / ITEMS /////////////////
        // public Byte ModelFx { get; set; } // 2
        // public Byte ModelId { get; set; } // 3
        public Byte[] ItemUnknown1 { get; set; } // 4, 5
        // public UInt16 BonusValue { get; set; } // 6, 7
        public Byte[] ItemUnknown2 { get; set; } // 8, 9
        public Int16 ItemSpell { get; set; } // 10, 11
        public Byte[] ItemUnknown3 { get; set; } // 12, 13, 14, 15
        // public UInt16 ItemFXSize { get; set; } // 16, 17
        // public Int16 ItemFX1 { get; set; } // 18, 19
        // public Int16 ItemFX2 { get; set; } // 20, 21
        // public Int16 ItemFX3 { get; set; } // 22, 23
        // public Int16 ItemFX4 { get; set; } // 24, 25
        // public Int16 ItemFX5 { get; set; } // 26, 27
        // public Int16 ItemFX6 { get; set; } // 28, 29
        // public Int16 ItemFX7 { get; set; } // 30, 31
        public UInt16 ItemPrice { get; set; } // 32, 33
        public Byte[] ItemUnused { get; set; } // 34-71

        ///////////////// RECIPES /////////////////
        public UInt16 ClavatCraftedItem { get; set; } // 56
        public UInt16 LiltyCraftedItem { get; set; } // 58
        public UInt16 YukeCraftedItem { get; set; } // 60
        public UInt16 SelkieCraftedItem { get; set; } // 62

        ///////////////// DERIVATIVE /////////////////
        public Int16 Strength { get; set; }
        public Int16 Defense { get; set; }

        ///////////////// EXTERNAL /////////////////
        public UInt16 Index { get; set; }
        public UInt64 Address { get; set; }
        public UInt64 RawAddress { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            // UNIVERSAL
            this.RaisePropertyChanged(nameof(this.BaseItemId));

            // EQUIPMENT
            this.RaisePropertyChanged(nameof(this.ModelFx));
            this.RaisePropertyChanged(nameof(this.ModelId));
            this.RaisePropertyChanged(nameof(this.Slot));
            this.RaisePropertyChanged(nameof(this.Tribe));
            this.RaisePropertyChanged(nameof(this.BonusValue));
            this.RaisePropertyChanged(nameof(this.BonusType));
            this.RaisePropertyChanged(nameof(this.Focus));
            this.RaisePropertyChanged(nameof(this.WeaponUnusedByes1));
            this.RaisePropertyChanged(nameof(this.ItemFXSize));
            this.RaisePropertyChanged(nameof(this.ItemFX1));
            this.RaisePropertyChanged(nameof(this.ItemFX2));
            this.RaisePropertyChanged(nameof(this.ItemFX3));
            this.RaisePropertyChanged(nameof(this.ItemFX4));
            this.RaisePropertyChanged(nameof(this.ItemFX5));
            this.RaisePropertyChanged(nameof(this.ItemFX6));
            this.RaisePropertyChanged(nameof(this.ItemFX7));
            this.RaisePropertyChanged(nameof(this.WeaponUnusedByes3));
            this.RaisePropertyChanged(nameof(this.SwingSound1));
            this.RaisePropertyChanged(nameof(this.SwingSound1Delay));
            this.RaisePropertyChanged(nameof(this.SwingSound2));
            this.RaisePropertyChanged(nameof(this.SwingSound2Delay));
            this.RaisePropertyChanged(nameof(this.SwingHitSound));
            this.RaisePropertyChanged(nameof(this.HitSound));
            this.RaisePropertyChanged(nameof(this.WeaponUnusedByes4));

            // ITEMS
            this.RaisePropertyChanged(nameof(this.ItemUnknown1));
            this.RaisePropertyChanged(nameof(this.ItemUnknown2));
            this.RaisePropertyChanged(nameof(this.ItemSpell));
            this.RaisePropertyChanged(nameof(this.ItemUnknown3));
            this.RaisePropertyChanged(nameof(this.ItemPrice));
            this.RaisePropertyChanged(nameof(this.ItemUnused));

            // RECIPES
            this.RaisePropertyChanged(nameof(this.ClavatCraftedItem));
            this.RaisePropertyChanged(nameof(this.LiltyCraftedItem));
            this.RaisePropertyChanged(nameof(this.YukeCraftedItem));
            this.RaisePropertyChanged(nameof(this.SelkieCraftedItem));

            this.RaisePropertyChanged(nameof(this.Index));
            this.RaisePropertyChanged(nameof(this.Address));
            this.RaisePropertyChanged(nameof(this.RawAddress));
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

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0xFFFF * 72)] // Intended is 0x4B5
    public class ItemCatalogDataSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xFFFF * 72)] // Intended is 0x4B5
        public Byte[] rawItemCatalogSlots;
    }

    public class ItemCatalogData
    {
        public ItemCatalogData()
        {
            this.SerializableData = new ItemCatalogDataSerializable();
        }

        public ItemCatalogDataSerializable SerializableData { get; set; }

        public UInt64 Address { get; set; }

        public UInt64 RawAddress { get; set; }

        public FullyObservableCollection<RawItemCatalogItemEntry> rawItems = new FullyObservableCollection<RawItemCatalogItemEntry>();

        const Int32 StructSize = 72;

        Byte[] cachedBytes = null;

        public static void Deserialize(ItemCatalogData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new ItemCatalogDataSerializable();
                    }

                    Marshal.PtrToStructure<ItemCatalogDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(UInt64 address, UInt64 rawAddress, Byte[] bytes)
        {
            for (Int32 entryIndex = 0; entryIndex < bytes.Length / StructSize; entryIndex++)
            {
                if (entryIndex >= this.rawItems.Count || this.rawItems[entryIndex] == null)
                {
                    this.rawItems.Add(new RawItemCatalogItemEntry());
                }

                // UNIVERSAL
                this.rawItems[entryIndex].BaseItemId = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 0));

                // EQUIPMENT
                this.rawItems[entryIndex].ModelFx = bytes[entryIndex * StructSize + 2];
                this.rawItems[entryIndex].ModelId = bytes[entryIndex * StructSize + 3];
                this.rawItems[entryIndex].Slot = bytes[entryIndex * StructSize + 4];
                this.rawItems[entryIndex].Tribe = bytes[entryIndex * StructSize + 5];
                this.rawItems[entryIndex].BonusValue = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 6));
                this.rawItems[entryIndex].BonusType = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 8));
                this.rawItems[entryIndex].Focus = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 10));
                this.rawItems[entryIndex].WeaponUnusedByes1 = null; // // 12, 13, 14, 15
                this.rawItems[entryIndex].ItemFXSize = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 16));
                this.rawItems[entryIndex].ItemFX1 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 18));
                this.rawItems[entryIndex].ItemFX2 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 20));
                this.rawItems[entryIndex].ItemFX3 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 22));
                this.rawItems[entryIndex].ItemFX4 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 24));
                this.rawItems[entryIndex].ItemFX5 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 26));
                this.rawItems[entryIndex].ItemFX6 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 28));
                this.rawItems[entryIndex].ItemFX7 = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 30));
                this.rawItems[entryIndex].WeaponUnusedByes3 = null; // 32-55
                this.rawItems[entryIndex].SwingSound1 = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 56));
                this.rawItems[entryIndex].SwingSound1Delay = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 58));
                this.rawItems[entryIndex].SwingSound2 = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 60));
                this.rawItems[entryIndex].SwingSound2Delay = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 62));
                this.rawItems[entryIndex].SwingHitSound = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 64));
                this.rawItems[entryIndex].HitSound = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 66));
                this.rawItems[entryIndex].WeaponUnusedByes4 = null; // 68, 69, 70, 71

                // EQUIPMENT
                // this.rawItems[index].ModelFx = bytes[index * StructSize + 2];
                // this.rawItems[index].ModelId = bytes[index * StructSize + 3];
                this.rawItems[entryIndex].ItemUnknown1 = null; // 4, 5
                this.rawItems[entryIndex].ItemUnknown2 = null; // 8, 9
                this.rawItems[entryIndex].ItemSpell = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, entryIndex * StructSize + 10));
                this.rawItems[entryIndex].ItemUnknown3 = null; // 12, 13, 14, 15
                this.rawItems[entryIndex].ItemPrice = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 32));
                this.rawItems[entryIndex].ItemUnused = null; // 34-71

                // RECIPE
                this.rawItems[entryIndex].ClavatCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 56));
                this.rawItems[entryIndex].LiltyCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 58));
                this.rawItems[entryIndex].YukeCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 60));
                this.rawItems[entryIndex].SelkieCraftedItem = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, entryIndex * StructSize + 62));

                // EXTERNAL
                this.rawItems[entryIndex].Index = (UInt16)entryIndex;
                this.rawItems[entryIndex].Address = address + (UInt64)(entryIndex * StructSize);
                this.rawItems[entryIndex].RawAddress = rawAddress + (UInt64)(entryIndex * StructSize);

                /*
                if (this.cachedBytes != null)
                {
                    for (Int32 byteIndex = entryIndex * StructSize; byteIndex < entryIndex * StructSize + StructSize; byteIndex++)
                    {
                        if (this.cachedBytes[byteIndex] != bytes[byteIndex])
                        {
                            this.rawItems[entryIndex].Refresh();
                            break;
                        }
                    }
                }*/

                // DERIVATIE
                Int16 bonusValue = this.rawItems[entryIndex].BonusValue;
                UInt16 bonusType = this.rawItems[entryIndex].BonusType;
                UInt16 baseItemId = this.rawItems[entryIndex].BaseItemId;

                Boolean resistFire = bonusType == 1;
                Boolean resistCold = bonusType == 2;
                Boolean resistLightning = bonusType == 3;
                Boolean resistSlow = bonusType == 4;
                Boolean resistStatis = bonusType == 5;
                Boolean resistPoison = bonusType == 6;
                Boolean resistCurse = bonusType == 7;
                Boolean resistParalysis = bonusType == 8;
                Boolean castingTime = bonusType == 9;
                Boolean chargeTime = bonusType == 10;
                Boolean spellDuration1 = bonusType == 11;
                Boolean spellDuration2 = bonusType == 12;
                Boolean resistMiasma = bonusType == 13;
                Boolean longSpellRange = bonusType == 14;
                Boolean longFocusAttack = bonusType == 15;
                Boolean regen = bonusType == 16;
                Boolean focusAttacks = bonusType == 17;
                Boolean spellDamage = bonusType == 18;
                Boolean stunProof = bonusType == 19;

                // Nope
                // baseItemId = (UInt16)(baseItemId & (UInt16)0xFF);

                if (baseItemId == 1)
                {
                    this.rawItems[entryIndex].BonusValue = bonusValue;
                }
                else if (baseItemId == 0x45)
                {
                    this.rawItems[entryIndex].Defense = bonusValue;
                }

                // Crystal Mail (Free)
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x51
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x51
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x51
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x51
                    )
                {
                    int bp = 5;
                }

                // Lilty zippies
                if (false
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x0034 // Aura // 52
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x0037 // Cudgel // 55
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x003C // DC // 60
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x003D // TM // 61
                    )
                {
                    int bp = 5;
                }

                // High STR wep
                if (false
                    || this.rawItems[entryIndex].LiltyCraftedItem == 27
                    )
                {
                    int bp = 5;
                }

                // Talisman of Speed
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x008B
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x008B
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x008B
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x008B
                    )
                {
                    int bp = 5;
                }

                // Thiefs Emblem
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x8C
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x8C
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x8C
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x8C
                    )
                {
                    int bp = 5;
                }

                // Gobbie Pocket
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x00DD
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x00DD
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x00DD
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x00DD
                    )
                {
                    int bp = 5;
                }

                // Ultimate Pocket
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x00DE
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x00DE
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x00DE
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x00DE
                    )
                {
                    int bp = 5;
                }

                // Star Pendant
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x00E6
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x00E6
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x00E6
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x00E6
                    )
                {
                    int bp = 5;
                }

                // Sun Pendant
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x00E7
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x00E7
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x00E7
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x00E7
                    )
                {
                    int bp = 5;
                }

                // Lilty Zippies
                if (false
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x0034
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x0037
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x003D
                    )
                {
                    int bp = 5;
                }

                // Abbadon
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x21A
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x21A
                    || this.rawItems[entryIndex].YukeCraftedItem == 0x21A
                    || this.rawItems[entryIndex].SelkieCraftedItem == 0x21A
                    )
                {
                    int bp = 5;
                }

                // Hastega (Dev)
                if (false
                    || this.rawItems[entryIndex].ClavatCraftedItem == 0x22D
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x22D
                    || this.rawItems[entryIndex].YukeCraftedItem == 0x22D
                    || this.rawItems[entryIndex].SelkieCraftedItem == 0x22D
                    )
                {
                    int bp = 5;
                }

                // Curega (Dev)
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x21B
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x21B
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x21B
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x21B
                    )
                {
                    int bp = 5;
                }

                // LizWar
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x216
                    // || this.rawItems[entryIndex].LiltyCraftedItem == 0x216
                    || this.rawItems[entryIndex].YukeCraftedItem == 0x216
                    || this.rawItems[entryIndex].SelkieCraftedItem == 0x216
                    )
                {
                    int bp = 5;
                }

                // Electric Jellyfish
                if (false
                    // || this.rawItems[entryIndex].ClavatCraftedItem == 0x21E
                    || this.rawItems[entryIndex].LiltyCraftedItem == 0x21E
                    // || this.rawItems[entryIndex].YukeCraftedItem == 0x21E
                    // || this.rawItems[entryIndex].SelkieCraftedItem == 0x21E
                    )
                {
                    int bp = 5;
                }

                if (entryIndex > 600)
                {

                    if (baseItemId == 0x45)
                    {
                        int bp = 5;
                    }

                    if (baseItemId == 0x7F)
                    {
                        int bp = 5;
                    }

                    if (baseItemId == 0x1F5)
                    {
                        int bp = 5;
                    }

                    if (baseItemId == 0x17D)
                    {
                        int bp = 5;
                    }

                    if (resistMiasma == true)
                    {
                        int bp = 5;
                    }

                    if (baseItemId == 1
                        // && (this.rawItems[index].ModelId >= 1 && this.rawItems[index].ModelId <= 26)
                        )
                    {
                        int bp = 5;
                    }
                }

                this.Address = address;
                this.RawAddress = rawAddress;
            }

            if (this.cachedBytes == null)
            {
                this.cachedBytes = new Byte[bytes.Length];
            }

            bytes.CopyTo(this.cachedBytes, 0);
        }
    }
    //// End class
}
//// End namespace
