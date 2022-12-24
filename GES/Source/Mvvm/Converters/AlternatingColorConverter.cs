namespace GES.Source.Mvvm.Converters
{
    using GES.Source.Controls;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class AlternatingColorConverter : IValueConverter
    {
        Brush AltBrush1 = new SolidColorBrush(ToMediaColor(DarkBrushes.GESColorPanel));
        Brush AltBrush2 = new SolidColorBrush(ToMediaColor(DarkBrushes.GESColorPanelAlt));
        Brush AltBrush2HighContract = new SolidColorBrush(ToMediaColor(DarkBrushes.GESColorFrame));

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is Byte)
            {
                Byte slotId = (Byte)value;

                if (slotId % 2 == 0)
                {
                    return AltBrush1;
                }
                else
                {
                    if (parameter as String == "HighContrast")
                    {
                        return AltBrush2HighContract;
                    }

                    return AltBrush2;
                }
            }
            if (value is UInt16)
            {
                UInt16 slotId = (UInt16)value;

                if (slotId % 2 == 0)
                {
                    return AltBrush1;
                }
                else
                {
                    if (parameter as String == "HighContrast")
                    {
                        return AltBrush2HighContract;
                    }

                    return AltBrush2;
                }
            }

            return System.Windows.Media.Brushes.White;
        }

        private static System.Windows.Media.Color ToMediaColor(System.Drawing.Color color)
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