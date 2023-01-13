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

    public class ItemRefToIdConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            EquipmentEntry equipmentEntry = value as EquipmentEntry;
            CraftEntry craftEntry = value as CraftEntry;
            RawEquipmentEntry rawEquipmentEntry = value as RawEquipmentEntry;
            RawCommandListEntry rawCommandListEntry = value as RawCommandListEntry;

            if ((equipmentEntry == null || equipmentEntry.Parent == null)
                && (rawEquipmentEntry == null || rawEquipmentEntry.Parent == null)
                && (craftEntry == null || craftEntry.Parent == null)
                && (rawCommandListEntry == null || rawCommandListEntry.Parent == null)
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
            }

            Int32 playerSlotId = InventoryViewerViewModel.GetInstance().PlayerToSlotMap.ContainsKey(playerId)
                   ? InventoryViewerViewModel.GetInstance().PlayerToSlotMap[playerId]
                   : -1;

            if (rawEquipmentEntry != null)
            {
                inventorySlot = rawEquipmentEntry.InventorySlotId;
                playerSlotId = rawEquipmentEntry.Parent.PlayerSlotIndex;
            }

            if (rawCommandListEntry != null)
            {
                inventorySlot = rawCommandListEntry.InventorySlotId;

                // parameter for this case will be slot id, and if set, don't pull from the associated slot (used for CLES view)
                if (playerId == -1)
                {
                    playerSlotId = rawCommandListEntry.Parent.PlayerIndex;
                }
                else
                {
                    playerSlotId = playerId;
                }
            }

            PlayerSlotDataView slotDataView = InventoryViewerViewModel.GetInstance().PlayerSlots.ElementAtOrDefault(playerSlotId);

            if (slotDataView != null && slotDataView.Slot != null)
            {
                if (slotDataView.Slot.rawItems != null && inventorySlot >= 0 && inventorySlot < slotDataView.Slot.rawItems.Count)
                {
                    return slotDataView.Slot.rawItems[inventorySlot].ItemId;
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