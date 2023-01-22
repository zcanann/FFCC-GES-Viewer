
namespace GES.Source.LetterTableViewer
{
    using GES.Engine.Common.DataStructures;
    using System;
    using System.ComponentModel;

    public class LetterTableDataView : INotifyPropertyChanged
    {
        public LetterTableDataView(LetterTableData LetterTableData)
        {
            this.LetterTableData = LetterTableData;
        }

        public LetterTableData LetterTableData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public FullyObservableCollection<RawLetterTableItemEntry> RawLetters
        {
            get
            {
                return this.LetterTableData.rawLetters;
            }

            set
            {
                this.LetterTableData.rawLetters = value;
            }
        }

        public void RefreshAllProperties()
        {
            this.RaisePropertyChanged(nameof(this.RawLetters));
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
