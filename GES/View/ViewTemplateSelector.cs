namespace GES.View
{
    using GES.Source.CraftViewer;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using GES.Source.Output;
    using GES.Source.ProcessSelector;
    using GES.Source.PropertyViewer;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Provides the template required to view a pane.
    /// </summary>
    public class ViewTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// The template for the Process Selector.
        /// </summary>
        private DataTemplate processSelectorViewTemplate;

        /// <summary>
        /// The template for the Property Viewer.
        /// </summary>
        private DataTemplate propertyViewerViewTemplate;

        /// <summary>
        /// The template for the Output.
        /// </summary>
        private DataTemplate outputViewTemplate;

        /// <summary>
        /// The template for the Inventory Viewer.
        /// </summary>
        private DataTemplate inventoryViewerViewTemplate;

        /// <summary>
        /// The template for the Inventory Viewer.
        /// </summary>
        private DataTemplate equipmentViewerViewTemplate;

        /// <summary>
        /// The template for the Craft Viewer.
        /// </summary>
        private DataTemplate craftViewerViewTemplate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewTemplateSelector" /> class.
        /// </summary>
        public ViewTemplateSelector()
        {
            this.DataTemplates = new Dictionary<Type, DataTemplate>();
        }

        /// <summary>
        /// Gets or sets the template for the Data Template Error display.
        /// </summary>
        public DataTemplate DataTemplateErrorViewTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template for the Process Selector.
        /// </summary>
        public DataTemplate ProcessSelectorViewTemplate
        {
            get
            {
                return this.processSelectorViewTemplate;
            }

            set
            {
                this.processSelectorViewTemplate = value;
                this.DataTemplates[typeof(ProcessSelectorViewModel)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the template for the Property Viewer.
        /// </summary>
        public DataTemplate PropertyViewerViewTemplate
        {
            get
            {
                return this.propertyViewerViewTemplate;
            }

            set
            {
                this.propertyViewerViewTemplate = value;
                this.DataTemplates[typeof(PropertyViewerViewModel)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the template for the Output.
        /// </summary>
        public DataTemplate OutputViewTemplate
        {
            get
            {
                return this.outputViewTemplate;
            }

            set
            {
                this.outputViewTemplate = value;
                this.DataTemplates[typeof(OutputViewModel)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the template for the Inventory Viewer.
        /// </summary>
        public DataTemplate InventoryViewerViewTemplate
        {
            get
            {
                return this.inventoryViewerViewTemplate;
            }

            set
            {
                this.inventoryViewerViewTemplate = value;
                this.DataTemplates[typeof(InventoryViewerViewModel)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the template for the Equipment Viewer.
        /// </summary>
        public DataTemplate EquipmentViewerViewTemplate
        {
            get
            {
                return this.equipmentViewerViewTemplate;
            }

            set
            {
                this.equipmentViewerViewTemplate = value;
                this.DataTemplates[typeof(EquipmentViewerViewModel)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the template for the Craft Viewer.
        /// </summary>
        public DataTemplate CraftViewerViewTemplate
        {
            get
            {
                return this.craftViewerViewTemplate;
            }

            set
            {
                this.craftViewerViewTemplate = value;
                this.DataTemplates[typeof(CraftViewerViewModel)] = value;
            }
        }

        /// <summary>
        /// Gets or sets the mapping for all data templates.
        /// </summary>
        protected Dictionary<Type, DataTemplate> DataTemplates { get; set; }

        /// <summary>
        /// Returns the required template to display the given view model.
        /// </summary>
        /// <param name="item">The view model.</param>
        /// <param name="container">The dependency object.</param>
        /// <returns>The template associated with the provided view model.</returns>
        public override DataTemplate SelectTemplate(Object item, DependencyObject container)
        {
            if (item is ContentPresenter)
            {
                Object content = (item as ContentPresenter).Content;

                if (content != null && this.DataTemplates.ContainsKey(content.GetType()))
                {
                    return this.DataTemplates[content.GetType()];
                }
            }

            if (this.DataTemplates.ContainsKey(item.GetType()))
            {
                return this.DataTemplates[item.GetType()];
            }

            return this.DataTemplateErrorViewTemplate;
        }
    }
    //// End class
}
//// End namespace