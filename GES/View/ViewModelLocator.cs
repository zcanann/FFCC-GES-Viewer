﻿namespace GES.View
{
    using GES.Source.Controls;
    using GES.Source.CraftListViewer;
    using GES.Source.ItemCatalogViewer;
    using GES.Source.Docking;
    using GES.Source.EquipmentListViewer;
    using GES.Source.InventoryViewer;
    using GES.Source.Output;
    using GES.Source.ProcessSelector;
    using GES.Source.PropertyViewer;
    using GES.Source.Tasks;
    using Source.Main;
    using GES.Source.MonsterTableViewer;
    using GES.Source.Editors.InventoryItemEditor;
    using GES.Source.Editors.ApplyArtifactsEditor;
    using GES.Source.SlotDataViewer;
    using GES.Source.LetterTableViewer;

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
        }

        /// <summary>
        /// Gets the Docking view model.
        /// </summary>
        public DockingViewModel DockingViewModel
        {
            get
            {
                return DockingViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Action Scheduler view model.
        /// </summary>
        public TaskTrackerViewModel TaskTrackerViewModel
        {
            get
            {
                return TaskTrackerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Process Selector view model.
        /// </summary>
        public ProcessSelectorViewModel ProcessSelectorViewModel
        {
            get
            {
                return ProcessSelectorViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Property Viewer view model.
        /// </summary>
        public PropertyViewerViewModel PropertyViewerViewModel
        {
            get
            {
                return PropertyViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets a Output view model.
        /// </summary>
        public OutputViewModel OutputViewModel
        {
            get
            {
                return OutputViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Main view model.
        /// </summary>
        public MainViewModel MainViewModel
        {
            get
            {
                return MainViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Inventory Viewer view model.
        /// </summary>
        public InventoryViewerViewModel InventoryViewerViewModel
        {
            get
            {
                return InventoryViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Slot Data Viewer view model.
        /// </summary>
        public SlotDataViewerViewModel SlotDataViewerViewModel
        {
            get
            {
                return SlotDataViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Equipment Viewer view model.
        /// </summary>
        public EquipmentListViewerViewModel EquipmentListViewerViewModel
        {
            get
            {
                return EquipmentListViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Monster Table Viewer view model.
        /// </summary>
        public MonsterTableViewerViewModel MonsterTableViewerViewModel
        {
            get
            {
                return MonsterTableViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Letter Table Viewer view model.
        /// </summary>
        public LetterTableViewerViewModel LetterTableViewerViewModel
        {
            get
            {
                return LetterTableViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Item Catalog Viewer view model.
        /// </summary>
        public ItemCatalogViewerViewModel ItemCatalogViewerViewModel
        {
            get
            {
                return ItemCatalogViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Craft Viewer view model.
        /// </summary>
        public CraftListViewerViewModel CraftListViewerViewModel
        {
            get
            {
                return CraftListViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Command List Viewer view model.
        /// </summary>
        public CommandListViewerViewModel CommandListViewerViewModel
        {
            get
            {
                return CommandListViewerViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets a HexDec box view model.
        /// </summary>
        public HexDecBoxViewModel HexDecBoxViewModel
        {
            get
            {
                return new HexDecBoxViewModel();
            }
        }

        /// <summary>
        /// Gets the Inventory Item Editor view model.
        /// </summary>
        public InventoryItemEditorViewModel InventoryItemEditorViewModel
        {
            get
            {
                return InventoryItemEditorViewModel.GetInstance();
            }
        }

        /// <summary>
        /// Gets the Apply Artifacts Editor view model.
        /// </summary>
        public ApplyArtifactsEditorViewModel ApplyArtifactsEditorViewModel
        {
            get
            {
                return ApplyArtifactsEditorViewModel.GetInstance();
            }
        }
    }
    //// End class
}
//// End namespace