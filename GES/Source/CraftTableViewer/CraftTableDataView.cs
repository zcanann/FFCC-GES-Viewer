
namespace GES.Source.CraftTableViewer
{
    using GES.Source.Mvvm.Converters;
    using System;
    using System.ComponentModel;

    public class CraftTableDataView : INotifyPropertyChanged
    {
        public CraftTableDataView(CraftTableData craftTableData)
        {
            this.CraftTableData = craftTableData;
        }

        public CraftTableData CraftTableData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RawCraftTableItemEntry[] RawItems
        {
            get
            {
                return this.CraftTableData.rawItems;
            }

            set
            {
                this.CraftTableData.rawItems = value;
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
