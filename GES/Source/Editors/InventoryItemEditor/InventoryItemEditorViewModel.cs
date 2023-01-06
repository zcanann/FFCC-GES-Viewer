namespace GES.Source.Editors.InventoryItemEditor
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GES.Source.InventoryViewer;
    using System;
    using System.Media;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Offset Editor.
    /// </summary>
    public class InventoryItemEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// Singleton instance of the <see cref="InventoryItemEditorViewModel" /> class.
        /// </summary>
        private static readonly Lazy<InventoryItemEditorViewModel> InventoryItemEditorViewModelInstance = new Lazy<InventoryItemEditorViewModel>(
                () => { return new InventoryItemEditorViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemEditorViewModel" /> class.
        /// </summary>
        public InventoryItemEditorViewModel()
        {
            this.UpdateItemIdCommand = new RelayCommand<UInt16>((itemId) => Task.Run(() => this.UpdateItemId(itemId)), (offset) => true);
        }

        /// <summary>
        /// Gets a command to update the active data type for the item being edited.
        /// </summary>
        public ICommand UpdateItemIdCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected data type.
        /// </summary>
        public UInt16 ItemId { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="InventoryItemEditorViewModel" /> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static InventoryItemEditorViewModel GetInstance()
        {
            return InventoryItemEditorViewModel.InventoryItemEditorViewModelInstance.Value;
        }

        public Boolean Show(Window owner, RawItemEntry itemEntry)
        {
            if (itemEntry == null)
            {
                return false;
            }

            View.Editors.InventoryItemEditor itemEditor = new View.Editors.InventoryItemEditor() { Owner = owner };

            SystemSounds.Asterisk.Play();

            this.ItemId = itemEntry.ItemId;
            itemEditor.Topmost = true;

            // Explicit compare against true since this is nullable
            return itemEditor.ShowDialog() == true;
        }

        /// <summary>
        /// Updates the active item id.
        /// </summary>
        /// <param name="itemId">The new item id.</param>
        private void UpdateItemId(UInt16 itemId)
        {
            this.ItemId = itemId;
            this.RaisePropertyChanged(nameof(this.ItemId));
        }
    }
    //// End class
}
//// End namespace