namespace GES.Source.Mvvm.Converters
{
    using GES.Source.Controls;
    using GES.Source.EquipmentViewer;
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class EquipmentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EquipmentEntry equipmentEntry = value as EquipmentEntry;

            if (equipmentEntry != null && equipmentEntry.Parent != null)
            {
                if (equipmentEntry.SlotId >= equipmentEntry.Parent.SerializableData.itemCount)
                {
                    return System.Windows.Media.Brushes.Red;
                }
            }

            return System.Windows.Media.Brushes.White;
        }

        private System.Windows.Media.Color ToMediaColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Not used or implemented.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>Throws see <see cref="NotImplementedException" />.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace