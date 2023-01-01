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

            String result = String.Empty;

            if (value is Byte)
            {
                result = System.Convert.ToString((Byte)value, 2);
            }
            else if (value is Int16)
            {
                result = System.Convert.ToString((Int16)value, 2);
            }
            else if (value is UInt16)
            {
                result = System.Convert.ToString((UInt16)value, 2);
            }
            else if (value is Int32)
            {
                result = System.Convert.ToString((Int32)value, 2);
            }
            else if (value is UInt32)
            {
                result = System.Convert.ToString((UInt32)value, 2);
            }

            result = result.PadLeft(result.Length + (result.Length % 4 == 0 ? 0 : (4 - result.Length % 4)), '0');
            result = System.Text.RegularExpressions.Regex.Replace(result, ".{4}", "$0 ");

            return result;
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