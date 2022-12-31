
namespace GES.Source.ItemCatalogViewer
{
    using GES.Source.Mvvm.Converters;
    using System;
    using System.ComponentModel;

    public class ItemCatalogDataView : INotifyPropertyChanged
    {
        public ItemCatalogDataView(ItemCatalogData ItemCatalogData)
        {
            this.ItemCatalogData = ItemCatalogData;
        }

        public ItemCatalogData ItemCatalogData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RawItemCatalogItemEntry[] RawItems
        {
            get
            {
                return this.ItemCatalogData.rawItems;
            }

            set
            {
                this.ItemCatalogData.rawItems = value;
            }
        }

        public void RefreshAllProperties()
        {
            this.RaisePropertyChanged(nameof(this.RawItems));
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
    //// End class
}
//// End namespace
