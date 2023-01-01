namespace GES.Source.Mvvm.Converters
{
    using GES.Engine.Common;
    using System;
    using System.Buffers.Binary;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts an Int32 value to a hexedecimal value.
    /// </summary>
    public class ArtifactGlitchVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts an Int32 to a Hex string.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>A hex string. If conversion cannot take place, returns null.</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is String))
            {
                return String.Empty;
            }

            Byte bit = (Byte)Conversions.ParsePrimitiveStringAsPrimitive(ScannableType.Byte, parameter?.ToString());
            // UInt16 newValue = BinaryPrimitives.ReverseEndianness((UInt16)(Int32)value);
            UInt16 newValue = (UInt16)(Int32)value;

            if (IsBitSet(newValue, bit))
            {
                return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        bool IsBitSet(UInt16 b, Byte pos)
        {
            return (b & (1 << pos)) != 0;
        }

        /// <summary>
        /// Hex string to an Int32.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>An Int32. If conversion cannot take place, returns 0.</returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
    //// End class
}
//// End namespace