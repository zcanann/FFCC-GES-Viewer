namespace GES.Source.InventoryViewer
{
    using GES.Source.Docking;

    /// <summary>
    /// View model for the Heap Visualizer.
    /// </summary>
    public class SlotDataViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="SlotDataViewerViewModel" /> class.
        /// </summary>
        private static SlotDataViewerViewModel slotDataViewerinstance = new SlotDataViewerViewModel();

        /// <summary>
        /// Prevents a default instance of the <see cref="SlotDataViewerViewModel" /> class from being created.
        /// </summary>
        private SlotDataViewerViewModel() : base("Slot Data Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);
        }

        public static SlotDataViewerViewModel GetInstance()
        {
            return SlotDataViewerViewModel.slotDataViewerinstance;
        }
    }
    //// End class
}
//// End namespace