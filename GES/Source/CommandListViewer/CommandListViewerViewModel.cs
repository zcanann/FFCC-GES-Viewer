namespace GES.Source.SlotDataViewer
{
    using GES.Source.Docking;

    /// <summary>
    /// View model for the Command List Viewer. IMPORTANT: You probably want <see cref="CraftListViewerViewModel"/> instead.
    /// </summary>
    public class CommandListViewerViewModel : ToolViewModel
    {
        /// <summary>
        /// Singleton instance of the <see cref="CommandListViewerViewModel" /> class.
        /// </summary>
        private static readonly CommandListViewerViewModel Instance = new CommandListViewerViewModel();

        /// <summary>
        /// Prevents a default instance of the <see cref="CommandListViewerViewModel" /> class from being created.
        /// </summary>
        private CommandListViewerViewModel() : base("[CLES] Command List Viewer")
        {
            DockingViewModel.GetInstance().RegisterViewModel(this);
        }

        public static CommandListViewerViewModel GetInstance()
        {
            return Instance;
        }
    }
    //// End class
}
//// End namespace