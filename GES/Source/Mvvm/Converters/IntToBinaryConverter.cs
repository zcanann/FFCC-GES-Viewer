namespace GES.Source.Mvvm.Converters
{
    using GES.Engine.Common;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts an Int32 value to a hexedecimal value.
    /// </summary>
    public class IntToBinaryConverter : IValueConverter
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
            if (value == null)
            {
                return String.Empty;
            }

            if (value is Byte)
            {
                return System.Convert.ToString((Byte)value, 2);
            }

            if (value is Int16)
            {
                return System.Convert.ToString((Int16)value, 2);
            }

            if (value is UInt16)
            {
                return System.Convert.ToString((UInt16)value, 2);
            }

            if (value is Int32)
            {
                return System.Convert.ToString((Int32)value, 2);
            }

            if (value is UInt32)
            {
                return System.Convert.ToString((UInt32)value, 2);
            }

            return String.Empty;
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