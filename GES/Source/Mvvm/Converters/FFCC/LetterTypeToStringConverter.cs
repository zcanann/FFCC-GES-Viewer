namespace GES.Source.Mvvm.Converters
{
    using GES.Engine.Common;
    using GES.Source.Main;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts an Int32 value to a hexedecimal value.
    /// </summary>
    public class LetterTypeToStringConverter : IValueConverter
    {
        private static readonly ItemToNameConverter ItemToNameConverter = new ItemToNameConverter();

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

            String hexString = Conversions.ParsePrimitiveAsHexString(value.GetType(), value, signHex: true);
            Boolean isEn = MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageEN;

            if (value is UInt16)
            {
                UInt16 intVal = (UInt16)value;

                if ((intVal & 0x0001) != 0)
                {
                    hexString += " | Clavat";
                }
                if ((intVal & 0x0002) != 0)
                {
                    hexString += " | Lilty";
                }
                if ((intVal & 0x0004) != 0)
                {
                    hexString += " | Yuke";
                }
                if ((intVal & 0x0008) != 0)
                {
                    hexString += " | Selkie";
                }
                if ((intVal & 0x0010) != 0)
                {
                    hexString += " | Male";
                }
                if ((intVal & 0x0020) != 0)
                {
                    hexString += " | Female";
                }
                if ((intVal & 0x0040) != 0)
                {
                    // Striped Apple
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x17D, null, null, null);
                }
                if ((intVal & 0x0080) != 0)
                {
                    // Cherry Cluster
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x17E, null, null, null);
                }
                if ((intVal & 0x0100) != 0)
                {
                    // Rainbow Grapes
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x17F, null, null, null);
                }
                if ((intVal & 0x0200) != 0)
                {
                    // Star Carrot
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x180, null, null, null);
                }
                if ((intVal & 0x0400) != 0)
                {
                    // Gourd Potato
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x181, null, null, null);
                }
                if ((intVal & 0x0800) != 0)
                {
                    // Round Corn
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x182, null, null, null);
                }
                if ((intVal & 0x1000) != 0)
                {
                    // Meat
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x183, null, null, null);
                }
                if ((intVal & 0x2000) != 0)
                {
                    // Fish
                    hexString += " | " + ItemToNameConverter.Convert((UInt16)0x184, null, null, null);
                }
            }

            return hexString;
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