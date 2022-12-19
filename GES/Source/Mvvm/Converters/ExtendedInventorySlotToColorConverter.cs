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
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            RawItemEntry equipmentEntry = value as RawItemEntry;

            if (equipmentEntry != null)
            {
                if (equipmentEntry.Index >= 128)
                {
                    return System.Windows.Media.Brushes.Red;
                }
                else if (equipmentEntry.Index >= 64)
                {
                    return System.Windows.Media.Brushes.OrangeRed;
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
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace