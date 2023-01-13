namespace GES.Source.SlotDataViewer
{
    using GES.Source.Docking;

    /// <summary>
    /// View model for the Slot Data Viewer. IMPORTANT: You probably want <see cref="InventoryViewerViewModel"/> instead.
    /// </summary>
    public class SlotDataViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="SlotDataViewerViewModel" /> class.
        /// </summary>
        private static readonly SlotDataViewerViewModel Instance = new SlotDataViewerViewModel();

        /// <summary>
        /// Prevents a default instance of the <see cref="SlotDataViewerViewModel" /> class from being created.
        /// </summary>
        private SlotDataViewerViewModel() : base("Slot Data Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);
        }

        public static SlotDataViewerViewModel GetInstance()
        {
            return Instance;
        }
    }
    //// End class
}
//// End namespace