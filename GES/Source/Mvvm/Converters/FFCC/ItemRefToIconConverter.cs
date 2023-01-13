namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Engine.Common;
    using GES.Source.CraftListViewer;
    using GES.Source.EquipmentListViewer;
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
            EquipmentEntry equipmentEntry = value as EquipmentEntry;
            CraftEntry craftEntry = value as CraftEntry;
            RawEquipmentEntry rawEquipmentEntry = value as RawEquipmentEntry;

            if ((equipmentEntry == null || equipmentEntry.Parent == null)
                && (rawEquipmentEntry == null || rawEquipmentEntry.Parent == null)
                && (craftEntry == null || craftEntry.Parent == null)
                && value is not UInt16
                && value is not Int32)
            {
                return null;
            }

            UInt16 inventorySlot = (value is UInt16)
                ? (UInt16)value
                : ((value is Int32)
                    ? (UInt16)(Int32)value
                    : (UInt16)0xFFFF);
            Int32 playerId = SyntaxChecker.CanParseValue(ScannableType.Int32, parameter?.ToString())
                ? (Int32)Conversions.ParsePrimitiveStringAsPrimitive(ScannableType.Int32, parameter?.ToString())
                : -1;

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
                    String paramString = (String) parameter;

                    switch(paramString)
                    {
                        case "Req1": return itemToIconConverter.Convert(craftEntry.RequiredItem1, targetType, null, culture);
                        case "Req2": return itemToIconConverter.Convert(craftEntry.RequiredItem2, targetType, null, culture);
                        case "Req3": return itemToIconConverter.Convert(craftEntry.RequiredItem3, targetType, null, culture);
                        case "CraftedClavat": return itemToIconConverter.Convert(craftEntry.CraftedItemIdVisualFakeClavat, targetType, null, culture);
                        case "CraftedLilty": return itemToIconConverter.Convert(craftEntry.CraftedItemIdVisualFakeLilty, targetType, null, culture);
                        case "CraftedYuke": return itemToIconConverter.Convert(craftEntry.CraftedItemIdVisualFakeYuke, targetType, null, culture);
                        case "CraftedSelkie": return itemToIconConverter.Convert(craftEntry.CraftedItemIdVisualFakeSelkie, targetType, null, culture);
                        default:break;
                    }
                }
            }

            Int32 playerSlotId = InventoryViewerViewModel.GetInstance().PlayerToSlotMap.ContainsKey(playerId)
                   ? InventoryViewerViewModel.GetInstance().PlayerToSlotMap[playerId]
                   : -1;

            if (rawEquipmentEntry != null)
            {
                inventorySlot = rawEquipmentEntry.InventorySlotId;
                playerSlotId = rawEquipmentEntry.Parent.PlayerSlotIndex;
            }

            PlayerSlotDataView slotDataView = InventoryViewerViewModel.GetInstance().PlayerSlots.ElementAtOrDefault(playerSlotId);

            if (slotDataView != null && slotDataView.Slot != null)
            {
                if (slotDataView.Slot.rawItems != null && inventorySlot >= 0 && inventorySlot < slotDataView.Slot.rawItems.Count)
                {
                    UInt16 itemId = slotDataView.Slot.rawItems[inventorySlot].ItemId;

                    return itemToIconConverter.Convert(itemId, targetType, parameter, culture);
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