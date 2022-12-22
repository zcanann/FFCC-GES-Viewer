namespace GES.Source.Mvvm.Converters
{
    using GES.Source.Controls;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ExtendedInventorySlotToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RawItemEntry equipmentEntry = value as RawItemEntry;

            if (equipmentEntry != null)
            {
                if (equipmentEntry.Index >= 128)
                {
                    return Brushes.Red;
                }
                else if (equipmentEntry.Index >= 64)
                {
                    return Brushes.OrangeRed;
                }
            }

            return Brushes.White;
        }

        private Color ToMediaColor(System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
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