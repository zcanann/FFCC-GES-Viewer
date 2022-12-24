namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.CraftTableViewer;
    using GES.Source.CraftViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class CraftedItemIdConverter : IValueConverter
    {
        private ItemRefToIdConverter itemRefToIdConverter = new ItemRefToIdConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Object itemIdObj = itemRefToIdConverter.Convert(value, targetType, parameter, culture);

            if (itemIdObj is UInt16)
            {
                UInt16 itemId = (UInt16) itemIdObj;

                string paramString = parameter as string;
                RawCraftTableItemEntry[] craftTable = CraftTableViewerViewModel.GetInstance()?.CraftTable?.RawItems;

                if (paramString != null && craftTable != null && itemId < craftTable.Length)
                {
                    if (paramString.Equals("Clavat", StringComparison.OrdinalIgnoreCase))
                    {
                        return craftTable[itemId].ClavatCraftedItem;
                    }
                    else if (paramString.Equals("Lilty", StringComparison.OrdinalIgnoreCase))
                    {
                        return craftTable[itemId].LiltyCraftedItem;
                    }
                    else if (paramString.Equals("Yuke", StringComparison.OrdinalIgnoreCase))
                    {
                        return craftTable[itemId].YukeCraftedItem;
                    }
                    else if (paramString.Equals("Selkie", StringComparison.OrdinalIgnoreCase))
                    {
                        return craftTable[itemId].SelkieCraftedItem;
                    }
                }
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace