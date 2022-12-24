﻿namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.CraftViewer;
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
            EquipmentEntry equipmentEntry = value as EquipmentEntry;
            CraftEntry craftEntry = value as CraftEntry;

            if ((equipmentEntry == null || equipmentEntry.Parent == null) && (craftEntry == null || craftEntry.Parent == null))
            {
                return null;
            }


            UInt16 inventorySlot = 0xFFFF;
            Int32 playerId = -1;

            if (equipmentEntry != null)
            {
                inventorySlot = equipmentEntry.ItemSlotId;
                playerId = equipmentEntry.Parent.PlayerIndex;
            }

            if (craftEntry != null)
            {
                inventorySlot = craftEntry.ItemSlotId;
                playerId = craftEntry.Parent.PlayerIndex;

                if (parameter is String)
                {
                    String paramString = (String)parameter;

                    switch (paramString)
                    {
                        case "Req1": return itemToNameConverter.Convert(craftEntry.RequiredItem1, targetType, null, culture);
                        case "Req2": return itemToNameConverter.Convert(craftEntry.RequiredItem2, targetType, null, culture);
                        case "Req3": return itemToNameConverter.Convert(craftEntry.RequiredItem3, targetType, null, culture);
                        case "CraftedClavat": return itemToNameConverter.Convert(craftEntry.CraftedItemIdVisualFakeClavat, targetType, null, culture);
                        case "CraftedLilty": return itemToNameConverter.Convert(craftEntry.CraftedItemIdVisualFakeLilty, targetType, null, culture);
                        case "CraftedYuke": return itemToNameConverter.Convert(craftEntry.CraftedItemIdVisualFakeYuke, targetType, null, culture);
                        case "CraftedSelkie": return itemToNameConverter.Convert(craftEntry.CraftedItemIdVisualFakeSelkie, targetType, null, culture);
                        default: break;
                    }
                }
            }

            if (InventoryViewerViewModel.GetInstance().PlayerToSlotMap.ContainsKey(playerId))
            {
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