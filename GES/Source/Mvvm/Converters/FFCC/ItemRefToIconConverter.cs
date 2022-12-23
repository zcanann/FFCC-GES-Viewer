namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.CraftViewer;
    using GES.Source.EquipmentViewer;
    using GES.Source.InventoryViewer;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public class ItemRefToIconConverter : IValueConverter
    {
        private ItemToIconConverter itemToIconConverter = new ItemToIconConverter();

        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                value = parameter;
            }

            EquipmentEntry equipmentEntry = value as EquipmentEntry;
            CraftEntry craftEntry = value as CraftEntry;

            if ((equipmentEntry == null || equipmentEntry.Parent == null) && (craftEntry == null || craftEntry.Parent == null))
            {
                return null;
            }

            UInt16 inventorySlot = equipmentEntry != null ? equipmentEntry.ItemSlotId : craftEntry.ItemSlotId;
            Int32 playerId = equipmentEntry != null ? equipmentEntry.Parent.PlayerIndex : craftEntry.Parent.PlayerIndex;

            if (InventoryViewerViewModel.GetInstance().PlayerToSlotMap.ContainsKey(playerId))
            {
                Int32 playerSlotId = InventoryViewerViewModel.GetInstance().PlayerToSlotMap[playerId];

                PlayerSlotDataView slotDataView = InventoryViewerViewModel.GetInstance().PlayerSlots.ElementAtOrDefault(playerSlotId);

                if (slotDataView != null && slotDataView.Slot != null)
                {
                    if (slotDataView.Slot.rawItems != null && inventorySlot >= 0 && inventorySlot < slotDataView.Slot.rawItems.Length)
                    {
                        UInt16 itemId = slotDataView.Slot.rawItems[inventorySlot].ItemId;

                        return itemToIconConverter.Convert(itemId, targetType, parameter, culture);
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