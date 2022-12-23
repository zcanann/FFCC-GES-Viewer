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

    public class ItemRefToIdConverter : IValueConverter
    {
        private IntToHexConverter hexConverter = new IntToHexConverter();

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
            }

            if (InventoryViewerViewModel.GetInstance().PlayerToSlotMap.ContainsKey(playerId))
            {
                Int32 playerSlotId = InventoryViewerViewModel.GetInstance().PlayerToSlotMap[playerId];

                PlayerSlotDataView slotDataView = InventoryViewerViewModel.GetInstance().PlayerSlots.ElementAtOrDefault(playerSlotId);

                if (slotDataView != null && slotDataView.Slot != null)
                {
                    if (slotDataView.Slot.rawItems != null && inventorySlot >= 0 && inventorySlot < slotDataView.Slot.rawItems.Length)
                    {
                        return hexConverter.Convert(slotDataView.Slot.rawItems[inventorySlot].ItemId, targetType, parameter, culture);
                    }
                }
            }

            return 0;
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