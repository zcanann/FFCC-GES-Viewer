namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    /// <summary>
    /// Converts ProjectItems to an icon format readily usable by the view.
    /// </summary>
    public class ItemRefToNameConverter : IValueConverter
    {
        private ItemToNameConverter itemToNameConverter = new ItemToNameConverter();

        /// <summary>
        /// Converts an Icon to a BitmapSource.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="targetType">Type to convert to.</param>
        /// <param name="parameter">Optional conversion parameter.</param>
        /// <param name="culture">Globalization info.</param>
        /// <returns>Object with type of BitmapSource. If conversion cannot take place, returns null.</returns>
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                value = parameter;
            }

            EquipmentEntry equipmentEntry = value as EquipmentEntry;

            if (equipmentEntry != null && equipmentEntry.Parent != null)
            {
                Int32 playerId = equipmentEntry.Parent.PlayerIndex;

                if (InventoryViewerViewModel.GetInstance().PlayerToSlotMap.ContainsKey(playerId))
                {
                    UInt16 inventorySlot = equipmentEntry.ItemSlotId;
                    Int32 playerSlotId = InventoryViewerViewModel.GetInstance().PlayerToSlotMap[playerId];

                    PlayerSlotDataView slotDataView = InventoryViewerViewModel.GetInstance().PlayerSlots.ElementAtOrDefault(playerSlotId);

                    if (slotDataView != null && slotDataView.Slot != null)
                    {
                        if (slotDataView.Slot.rawItems != null && inventorySlot >= 0 && inventorySlot < slotDataView.Slot.rawItems.Length)
                        {
                            UInt16 itemId = slotDataView.Slot.rawItems[inventorySlot].ItemId;

                            return itemToNameConverter.Convert(itemId, targetType, parameter, culture);
                        }
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
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace