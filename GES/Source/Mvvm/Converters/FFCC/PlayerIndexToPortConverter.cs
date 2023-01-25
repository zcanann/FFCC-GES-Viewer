namespace GES.Source.Mvvm.Converters
{
    using GES.Engine.Common;
    using GES.Source.InventoryViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts an Int32 value to a hexedecimal value.
    /// </summary>
    public class PlayerIndexToPortConverter : IValueConverter
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

            if (value is Int32 && InventoryViewerViewModel.GetInstance().DisplayGbaToPortMap.ContainsKey((Int32)value))
            {
                return InventoryViewerViewModel.GetInstance().DisplayGbaToPortMap[(Int32)value];
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
            if (value != null)
            {
                if (SyntaxChecker.CanParseHex(targetType, value.ToString()))
                {
                    return Conversions.ParseHexStringAsPrimitive(targetType, value.ToString());
                }
            }

            return 0;
        }
    }
    //// End class
}
//// End namespace