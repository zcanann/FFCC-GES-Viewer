namespace GES.Source.Editors.ApplyArtifactsEditor
{
    using GalaSoft.MvvmLight;
    using GES.Engine.Memory;
    using GES.Source.InventoryViewer;
    using System;
    using System.Buffers.Binary;
    using System.Media;
    using System.Threading;
    using System.Windows;

    /// <summary>
    /// View model for the Offset Editor.
    /// </summary>
    public class ApplyArtifactsEditorViewModel : ViewModelBase
    {
        private UInt16 selectedItem;

        /// <summary>
        /// Singleton instance of the <see cref="ApplyArtifactsEditorViewModel" /> class.
        /// </summary>
        private static readonly Lazy<ApplyArtifactsEditorViewModel> ApplyArtifactsEditorViewModelInstance = new Lazy<ApplyArtifactsEditorViewModel>(
                () => { return new ApplyArtifactsEditorViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplyArtifactsEditorViewModel" /> class.
        /// </summary>
        public ApplyArtifactsEditorViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the selected data type.
        /// </summary>
        public UInt16 SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                this.selectedItem = value;
                this.RaisePropertyChanged(nameof(this.SelectedItem));
            }
        }

        public RawItemEntry CurrentEntry { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ApplyArtifactsEditorViewModel" /> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ApplyArtifactsEditorViewModel GetInstance()
        {
            return ApplyArtifactsEditorViewModel.ApplyArtifactsEditorViewModelInstance.Value;
        }

        public void ApplyArtifactSet(Int32 setIndex)
        {
            RawItemEntry itemEntry = this.CurrentEntry;

            if (itemEntry == null)
            {
                return;
            }

            UInt64 artifactListBaseAddress = itemEntry.Parent.RawAddress + (UInt64)PlayerSlotData.ArtifactsOffset;
            Int32 max = setIndex == 4 ? 9 : 16;

            for (Int32 artifactIndex = 0; artifactIndex < max; artifactIndex++)
            {
                UInt16 realIndex = (UInt16)(setIndex * 16 + artifactIndex);
                UInt16 artifactId = (UInt16)(0x009F + realIndex);
                UInt16 idToWrite = IsBitSet((UInt16)itemEntry.Index, artifactIndex) ? artifactId : (UInt16)0xFFFF;

                MemoryWriter.Instance.Write<UInt16>(
                    SessionManager.Session.OpenedProcess,
                    artifactListBaseAddress + (UInt64)realIndex * 2,
                    BinaryPrimitives.ReverseEndianness(idToWrite));
            }
        }

        bool IsBitSet(UInt16 b, Int32 pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public void EquipCommandListSlot(Int32 index)
        {
            RawItemEntry itemEntry = this.CurrentEntry;

            if (itemEntry == null)
            {
                return;
            }

            UInt64 commandSlotAddress = itemEntry.Parent.RawAddress + (UInt64)PlayerSlotData.CommandListOffset + (UInt64)index * 2;

            MemoryWriter.Instance.Write<UInt16>(
                SessionManager.Session.OpenedProcess,
                commandSlotAddress,
                BinaryPrimitives.ReverseEndianness((UInt16)itemEntry.Index));
        }

        public Boolean Show(Window owner, RawItemEntry itemEntry)
        {
            if (itemEntry == null)
            {
                return false;
            }

            View.Editors.ApplyArtifactsEditor itemEditor = new View.Editors.ApplyArtifactsEditor() { Owner = owner };

            SystemSounds.Asterisk.Play();

            this.CurrentEntry = itemEntry;
            itemEditor.Topmost = true;

            // Explicit compare against true since this is nullable
            return itemEditor.ShowDialog() == true;
        }
    }
    //// End class
}
//// End namespace