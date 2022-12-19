namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.EquipmentViewer;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class EquipmentItemIconConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            EquipmentEntry equipmentEntry = value as EquipmentEntry;

            if (equipmentEntry != null)
            {
                // equipmentEntry.ItemSlotId;
            }

            return null;
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