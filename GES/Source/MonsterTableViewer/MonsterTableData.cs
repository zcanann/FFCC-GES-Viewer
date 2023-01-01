
namespace GES.Source.MonsterTableViewer
{
    using GES.Engine.Common.DataStructures;
    using GES.Source.EquipmentViewer;
    using System;
    using System.Buffers.Binary;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RawMonsterTableItemEntry : INotifyPropertyChanged
    {
        public Int16 MaxHealth { get; set; } // 26

        public UInt16 Health { get; set; } // 28

        public UInt16 Magic { get; set; }

        public UInt16 Defense { get; set; }

        public UInt16 Strength { get; set; }

        public UInt16 Index { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.MaxHealth));
            this.RaisePropertyChanged(nameof(this.Health));
            this.RaisePropertyChanged(nameof(this.Magic));
            this.RaisePropertyChanged(nameof(this.Defense));
            this.RaisePropertyChanged(nameof(this.Strength));
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

                this.rawMonsters[index].MaxHealth = BinaryPrimitives.ReverseEndianness(BitConverter.ToInt16(bytes, index * StructSize + 26));
                this.rawMonsters[index].Health = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(bytes, index * StructSize + 28));
                this.rawMonsters[index].Index = (UInt16)index;
                this.rawMonsters[index].Refresh();

                // this.rawItems[index].Strength = this.rawItems[index].B16 == 0 ? this.rawItems[index].StatBoost : (UInt16)0;
            }
        }
    }
    //// End class
}
//// End namespace
