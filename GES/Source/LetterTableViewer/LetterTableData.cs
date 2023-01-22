
namespace GES.Source.LetterTableViewer
{
    using GES.Engine.Common.DataStructures;
    using System;
    using System.Buffers.Binary;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class RawLetterTableItemEntry : INotifyPropertyChanged
    {
        public UInt16 LetterType { get; set; } // 24

        public UInt16 Profession { get; set; } // 26

        public UInt16 Rng { get; set; } // 28

        public String LetterName { get; set; }

        public String LetterContents { get; set; }

        public String LetterChoices { get; set; }

        public UInt16 Index { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.LetterType));
            this.RaisePropertyChanged(nameof(this.Profession));
            this.RaisePropertyChanged(nameof(this.Rng));
            this.RaisePropertyChanged(nameof(this.LetterName));
            this.RaisePropertyChanged(nameof(this.LetterContents));
            this.RaisePropertyChanged(nameof(this.LetterChoices));
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

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 62 * 512)]
    public class LetterDataTableSerializable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 62 * 512)]
        public Byte[] rawLetterTableSlots;
    }

    public class LetterTableData
    {
        public LetterTableData()
        {
            this.SerializableData = new LetterDataTableSerializable();
        }

        public LetterDataTableSerializable SerializableData { get; set; }

        public FullyObservableCollection<RawLetterTableItemEntry> rawLetters = new FullyObservableCollection<RawLetterTableItemEntry>();

        const Int32 StructSize = 62;

        public static void Deserialize(LetterTableData entry, Byte[] bytes)
        {
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try
            {
                if (handle.IsAllocated)
                {
                    if (entry.SerializableData == null)
                    {
                        entry.SerializableData = new LetterDataTableSerializable();
                    }

                    Marshal.PtrToStructure<LetterDataTableSerializable>(handle.AddrOfPinnedObject(), entry.SerializableData);
                }
            }
            finally
            {
                handle.Free();
            }
        }

        public void Refresh(Byte[] dataBytes, Byte[] nameBytes, Byte[] contentsBytes)
        {
            Span<Byte> nameSpan = new Span<Byte>(nameBytes);
            Span<Byte> contentsSpan = new Span<Byte>(contentsBytes);

            // Letter contents table is padded with 16 junk strings. Skip them.
            for (Int32 skipIndex = 0; skipIndex < 16; skipIndex++)
            {
                Int32 endContentsIndex = this.GetNextNullTerminator(contentsSpan);
                Int32 newContentsStartIndex = endContentsIndex + 1;
                contentsSpan = contentsSpan.Slice(newContentsStartIndex, contentsSpan.Length - newContentsStartIndex);
            }

            for (Int32 index = 0; index < dataBytes.Length / StructSize; index++)
            {
                if (index >= this.rawLetters.Count || this.rawLetters[index] == null)
                {
                    this.rawLetters.Add(new RawLetterTableItemEntry());
                }

                this.rawLetters[index].LetterType = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(dataBytes, index * StructSize + 24));
                this.rawLetters[index].Profession = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(dataBytes, index * StructSize + 26));
                this.rawLetters[index].Rng = BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt16(dataBytes, index * StructSize + 28));

                // Name
                Int32 endNameIndex = this.GetNextNullTerminator(nameSpan);
                Int32 newNameStartIndex = endNameIndex + 1;

                this.rawLetters[index].LetterName = System.Text.Encoding.GetEncoding("shift-jis").GetString(nameSpan.Slice(0, endNameIndex));

                nameSpan = nameSpan.Slice(newNameStartIndex, nameSpan.Length - newNameStartIndex);

                // Contents
                Int32 endContentsIndex = this.GetNextNullTerminator(contentsSpan);
                Int32 newContentsStartIndex = endContentsIndex + 1;

                this.rawLetters[index].LetterContents = System.Text.Encoding.GetEncoding("shift-jis").GetString(contentsSpan.Slice(0, endContentsIndex));

                contentsSpan = contentsSpan.Slice(newContentsStartIndex, contentsSpan.Length - newContentsStartIndex);

                // Choices
                endContentsIndex = this.GetNextNullTerminator(contentsSpan);
                newContentsStartIndex = endContentsIndex + 1;

                this.rawLetters[index].LetterChoices = System.Text.Encoding.GetEncoding("shift-jis").GetString(contentsSpan.Slice(0, endContentsIndex));

                contentsSpan = contentsSpan.Slice(newContentsStartIndex, contentsSpan.Length - newContentsStartIndex);

                this.rawLetters[index].Index = (UInt16)index;
                this.rawLetters[index].Refresh();
            }
        }

        public Int32 GetNextNullTerminator(Span<Byte> bytes)
        {
            Int32 index = bytes.IndexOf(new ReadOnlySpan<Byte>(0));

            return index >= 0 ? index : bytes.Length - 1;
        }
    }
    //// End class
}
//// End namespace
