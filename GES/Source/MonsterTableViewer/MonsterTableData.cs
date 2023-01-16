
namespace GES.Source.MonsterTableViewer
{
    using GES.Engine.Common.DataStructures;
    using System;
    using System.Buffers.Binary;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RawMonsterTableItemEntry : INotifyPropertyChanged
    {
        public Int16 MonsterId { get; set; } // 20

        public Int16 MaxHealth { get; set; } // 26

        public UInt16 Health { get; set; } // 28

        public UInt16 Strength { get; set; } // 30

        public UInt16 Magic { get; set; } // 32

        public UInt16 Defense { get; set; } // 34

        public UInt32 Unknown { get; set; } // 36-40

        public UInt16 Physical { get; set; } // 40

        public UInt16 Fire { get; set; } // 42

        public UInt16 Freeze { get; set; } // 44

        public UInt16 Stun { get; set; } // 46

        public UInt16 Slow { get; set; } // 48

        public UInt16 Stop { get; set; } // 50

        public UInt16 Gravity { get; set; } // 52

        public UInt16 Holy { get; set; } // 54

        public UInt16[] StatusTimers { get; set; } // 56 - 134

        public UInt16 AttackType { get; set; } // 136

        public UInt16 Index { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.MonsterId));
            this.RaisePropertyChanged(nameof(this.MaxHealth));
            this.RaisePropertyChanged(nameof(this.Health));
            this.RaisePropertyChanged(nameof(this.Strength));
            this.RaisePropertyChanged(nameof(this.Magic));
            this.RaisePropertyChanged(nameof(this.Defense));
            this.RaisePropertyChanged(nameof(this.Unknown));
            this.RaisePropertyChanged(nameof(this.Physical));
            this.RaisePropertyChanged(nameof(this.Fire));
            this.RaisePropertyChanged(nameof(this.Freeze));
            this.RaisePropertyChanged(nameof(this.Stun));
            this.RaisePropertyChanged(nameof(this.Slow));
            this.RaisePropertyChanged(nameof(this.Stop));
            this.RaisePropertyChanged(nameof(this.Gravity));
            this.RaisePropertyChanged(nameof(this.Holy));
            this.RaisePropertyChanged(nameof(this.StatusTimers));
            this.RaisePropertyChanged(nameof(this.AttackType));
            this.RaisePropertyChanged(nameof(this.Index));
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

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 242 * 72)]
    public class MonsterTableDataSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 242 * 72)]
        public Byte[] rawMonsterTableSlots;
    }

    public class MonsterTableData
    {
        public MonsterTableData()
        {
            this.SerializableData = new MonsterTableDataSerializable();
        }

        public MonsterTableDataSerializable SerializableData { get; set; }

        public FullyObservableCollection<RawMonsterTableItemEntry> rawMonsters = new FullyObservableCollection<RawMonsterTableItemEntry>();

        const Int32 StructSize = 272;

        public static void Deserialize(MonsterTableData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new MonsterTableDataSerializable();
                    }

                    Marshal.PtrToStructure<MonsterTableDataSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(Byte[] bytes)
        {
            for (Int32 index = 0; index < bytes.Length / StructSize; index++)
            {
                if (index >= this.rawMonsters.Count || this.rawMonsters[index] == null)
                {
                    this.rawMonsters.Add(new RawMonsterTableItemEntry());
                }

                this.rawMonsters[index].MonsterId = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 20));
                this.rawMonsters[index].MaxHealth = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 26));
                this.rawMonsters[index].Health = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 28));
                this.rawMonsters[index].Strength = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 30));
                this.rawMonsters[index].Magic = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 32));
                this.rawMonsters[index].Defense = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 34));

                this.rawMonsters[index].Physical = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 40));
                this.rawMonsters[index].Fire = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 42));
                this.rawMonsters[index].Freeze = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 44));
                this.rawMonsters[index].Stun = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 46));
                this.rawMonsters[index].Slow = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 48));
                this.rawMonsters[index].Stop = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 50));
                this.rawMonsters[index].Gravity = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 52));
                this.rawMonsters[index].Holy = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 54));
                // TODO: Status timers if we care
                this.rawMonsters[index].AttackType = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 136));

                this.rawMonsters[index].Index = (UInt16)index;
                this.rawMonsters[index].Refresh();
            }
        }
    }
    //// End class
}
//// End namespace
