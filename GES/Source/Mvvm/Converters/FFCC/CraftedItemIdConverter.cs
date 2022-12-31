namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.ItemCatalogViewer;
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
                RawItemCatalogItemEntry[] ItemCatalog = ItemCatalogViewerViewModel.GetInstance()?.ItemCatalog?.RawItems;

                if (paramString != null && ItemCatalog != null && itemId < ItemCatalog.Length)
                {
                    if (paramString.Equals("Clavat", StringComparison.OrdinalIgnoreCase))
                    {
                        return ItemCatalog[itemId].ClavatCraftedItem;
                    }
                    else if (paramString.Equals("Lilty", StringComparison.OrdinalIgnoreCase))
                    {
                        return ItemCatalog[itemId].LiltyCraftedItem;
                    }
                    else if (paramString.Equals("Yuke", StringComparison.OrdinalIgnoreCase))
                    {
                        return ItemCatalog[itemId].YukeCraftedItem;
                    }
                    else if (paramString.Equals("Selkie", StringComparison.OrdinalIgnoreCase))
                    {
                        return ItemCatalog[itemId].SelkieCraftedItem;
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