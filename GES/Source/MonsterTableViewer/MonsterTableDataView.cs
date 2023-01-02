
namespace GES.Source.MonsterTableViewer
{
    using GES.Engine.Common.DataStructures;
    using GES.Source.Mvvm.Converters;
    using System;
    using System.ComponentModel;

    public class MonsterTableDataView : INotifyPropertyChanged
    {
        public MonsterTableDataView(MonsterTableData MonsterTableData)
        {
            this.MonsterTableData = MonsterTableData;
        }

        public MonsterTableData MonsterTableData { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public FullyObservableCollection<RawMonsterTableItemEntry> RawMonsters
        {
            get
            {
                return this.MonsterTableData.rawMonsters;
            }

            set
            {
                this.MonsterTableData.rawMonsters = value;
            }
        }

        public void RefreshAllProperties()
        {
            this.RaisePropertyChanged(nameof(this.RawMonsters));
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
