namespace GES.Source.Mvvm.Converters
{
    using GES.Source.InventoryViewer;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts an Int32 value to a hexedecimal value.
    /// </summary>
    public class LetterListGlitchVisibilityConverter : IValueConverter
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

            if (value is UInt32)
            {
                UInt32 address = (UInt32)value;

                return this.GetLetterListGlitchVisibility(address);
            }

            return Visibility.Hidden;
        }

        const Int32 SlotDataSize = 3120; // Slot data are UInt16

        public Visibility GetLetterListGlitchVisibility(UInt32 address)
        {
            address -= 0x80000000; ;
            UInt32[] inventoryAddresses = InventoryViewerViewModel.GetInstance().GetInventoryAddresses();

            if (inventoryAddresses != null)
            {
                // Inventory items take 2 bytes each, thus two addresses. Floor to nearest even address.
                UInt32 inventoryAddress = address & ~(UInt32)0x1;

                for (Int32 inventoryIndex = 0; inventoryIndex < 8; inventoryIndex++)
                {
                    if (inventoryAddress < inventoryAddresses[inventoryIndex] || inventoryAddress >= inventoryAddresses[inventoryIndex] + SlotDataSize)
                    {
                        continue;
                    }

                    UInt32 offset = (inventoryAddress - inventoryAddresses[inventoryIndex]) / 2;

                    // Next slot range
                    if (offset >= 3 && offset <= 381 && (offset - 3) % 6 == 0)
                    {
                        return Visibility.Visible;
                    }

                    // Current slot range
                    if (offset >= 411 && (offset - 411) % 6 == 0) // 412+
                    {
                        if (offset >= 1452)
                        {
                            return Visibility.Visible;
                        }
                        else
                        {
                            return Visibility.Visible;
                        }
                    }
                }
            }

            return Visibility.Hidden;
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