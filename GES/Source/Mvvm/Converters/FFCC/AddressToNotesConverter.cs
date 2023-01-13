namespace GES.Source.Mvvm.Converters
{
    using GES.Source.InventoryViewer;
    using GES.Source.Main;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;
    using static GES.Source.Main.MainViewModel;

    public class AddressToNotesConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is UInt32)
            {
                UInt32 address = (UInt32)value;

                return this.GetNotes(address);
            }

            return null;
        }

        const Int32 SlotDataSize = 3120; // Slot data are UInt16

        private static readonly ItemToNameConverter ItemToNameConverter = new ItemToNameConverter();

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

        private string GetNotes(UInt32 address)
        {
            address -= 0x80000000;;
            Boolean isEn = MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageEN;
            UInt32[] slotDataAddresses = InventoryViewerViewModel.GetInstance().GetSlotDataAddresses();

            if (slotDataAddresses != null)
            {
                // Inventory items take 2 bytes each, thus two addresses. Floor to nearest even address.
                UInt32 slotAddress = address & ~(UInt32)0x1;

                for (Int32 slotIndex = 0; slotIndex < 8; slotIndex++)
                {
                    if (slotAddress < slotDataAddresses[slotIndex] || slotAddress >= slotDataAddresses[slotIndex] + SlotDataSize)
                    {
                        continue;
                    }

                    UInt32 slotOffset = slotAddress - slotDataAddresses[slotIndex];
                    UInt32 offset = (slotOffset - PlayerSlotData.InventoryOffset) / 2;

                    if (offset <= 63)
                    {
                        return (isEn ? "Inventory Slot " : "キャラクタースロット ") + offset.ToString() + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset <= 136)
                    {
                        return (isEn ? "Artifact Slot " : "アーティファクトスロット ") + (offset - 64).ToString() + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 163)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 164)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 165)
                    {
                        return (isEn ? "Gil High Bytes (65535+)" : "ギルハイバイト (65535+)") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 166)
                    {
                        return (isEn ? "Gil Low Bytes (0-65535)" : "ギル低バイト (0-65535)") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 167 && offset <= 168)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 169 && offset <= 174)
                    {
                        UInt16 commandListOffset = (UInt16)(6 - (174 - offset));

                        return (isEn ? "Command list slot " : "コマンド・リスト・スロット ") + commandListOffset.ToString() + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 175 && offset <= 375)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 376)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 377)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 378)
                    {
                        return (isEn ? "? Sometimes Blizzard" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 379)
                    {
                        return this.Constant(isEn, 0x0000);
                    }
                    else if (offset <= 380)
                    {
                        return (isEn ? "? Iron Sword, Steel Sword, Bastard Sword, Defender" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 381)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 382)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 383)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 384)
                    {
                        return (isEn ? "GBA Port (0-4)" : "ゲームボーイアドバンス ポート 0-4") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 385 && offset <= 392)
                    {
                        UInt16 favoritesOffset = (UInt16)((UInt16)392 - offset);
                        UInt16 itemId = (UInt16)((UInt16)0x017D + favoritesOffset);
                        String foodName = ItemToNameConverter.Convert(itemId, null, null, null)?.ToString();

                        return (isEn ? "Favorites (" + foodName + ")" : "お気に入り(" + foodName + ")") + " 0-64" + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 393)
                    {
                        return this.Constant(isEn, 0x0000);
                    }
                    else if (offset >= 394 && offset <= 400)
                    {
                        UInt32 nameOffset = 400 - offset;
                        String asciiRange = "[" + (nameOffset / 2).ToString() + "-" + (nameOffset / 2 + 1).ToString() + "] (ASCII)";
                        String jisRange = "[" + (nameOffset).ToString() + "] (JIS)";

                        return isEn ? "Name " + asciiRange + " | Name " + jisRange : "名前 " + asciiRange + " | 名前 " + jisRange + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 401 && offset <= 403)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 404)
                    {
                        return (isEn ? "? 0-27 observed values" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 405)
                    {
                        return (isEn ? "? 0-3 observed values" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 406)
                    {
                        return (isEn ? "? 0-1 observed values" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 407)
                    {
                        return (isEn ? "? 0-1 observed values" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 408 && offset <= 409)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 410 && offset <= 494)
                    {
                        return (isEn ? "? Various food related?" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 495 && offset <= 1011)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1012)
                    {
                        return this.Constant(isEn, 0x0000, 0x0128) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1013 && offset <= 1014)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1015)
                    {
                        return this.Constant(isEn, 0x0000, 0x0008) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1016)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1017)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1018)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1019)
                    {
                        return this.Constant(isEn, 0x0000, 0x8000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1020)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1021)
                    {
                        return this.Constant(isEn, 0x0000, 0x8000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1022 && offset <= 1023)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1024)
                    {
                        return this.Constant(isEn, 0x0000, 0x8000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1025)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1026)
                    {
                        return this.Constant(isEn, 0x0000, 0x2000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1027 && offset <= 1028)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1029)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1030)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1031)
                    {
                        return this.Constant(isEn, 0x0000, 0x0002, 0x0802) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1032)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1033)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1034)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1035)
                    {
                        return this.Constant(isEn, 0x0000, 0x0008) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1036 && offset <= 1058)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1059)
                    {
                        return this.Constant(isEn, 0x0000, 0x2001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1061 && offset <= 1063)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1064)
                    {
                        return this.Constant(isEn, 0x0000, 0x0800) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1065 && offset <= 1069)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1070)
                    {
                        return this.Constant(isEn, 0x0000, 0x0080) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1071 && offset <= 1075)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1076)
                    {
                        return this.Constant(isEn, 0x0000, 0x0010, 0x0090) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1077 && offset <= 1079)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1080)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1081 && offset <= 1082)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1083)
                    {
                        return this.Constant(isEn, 0x0000, 0x2000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1084 && offset <= 1086)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1087)
                    {
                        return this.Constant(isEn, 0x0000, 0x1000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1088)
                    {
                        return this.Constant(isEn, 0x0000, 0x0100) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1089 && offset <= 1126)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1127)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1128 && offset <= 1144)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1145)
                    {
                        return this.Constant(isEn, 0x0000, 0x0005, 0x000E) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1146 && offset <= 1148)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1149)
                    {
                        return this.Constant(isEn, 0x0000, 0x7530) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1150)
                    {
                        return this.Constant(isEn, 0x0000, 0x3A98) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1151)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1152)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1153 && offset <= 1161)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1162 && offset <= 1167)
                    {
                        return (isEn ? "? 0, or Roughly in range of 0xE0-0xFF" : "?") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1168 && offset <= 1171)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1172)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1173)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1174)
                    {
                        return this.Constant(isEn, 0x0000, 0x0005) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1175)
                    {
                        return this.Constant(isEn, 0x0000, 0x000A) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1176)
                    {
                        return this.Constant(isEn, 0x0000, 0x000A) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1177 && offset <= 1192)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset == 1193)
                    {
                        return this.Constant(isEn, 0x0000, 0x0036) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1194 && offset <= 1222)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1223 && offset <= 1226)
                    {
                        return (isEn ? "Last eaten food" : "最後に食べた食べ物") + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1227 && offset <= 1346)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress);
                    }
                    else if (offset >= 1347 && offset <= 1398)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1399)
                    {
                        return (isEn ? "? Large value ranges observed" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1400)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1401)
                    {
                        return this.Constant(isEn, 0x0004) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1402)
                    {
                        return this.Constant(isEn, 0x0004, 0x0006) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1403 && offset <= 1418)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1419)
                    {
                        return this.Constant(isEn, 0x0000, 0x0019) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1420 && offset <= 1421)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1422)
                    {
                        return (isEn ? "Food buff timer" : "フードバフタイマー") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1423)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1424)
                    {
                        return (isEn ? "Last eaten food" : "最後に食べた食べ物") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1425 && offset <= 1456)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1457)
                    {
                        return (isEn ? "? 2C observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1458)
                    {
                        return (isEn ? "? D observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1459)
                    {
                        return (isEn ? "? 2E observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1460)
                    {
                        return (isEn ? "? 100 observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1461)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1462)
                    {
                        return (isEn ? "? 192A observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1463 && offset <= 1468)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1469)
                    {
                        return (isEn ? "? 8022 observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1470)
                    {
                        return (isEn ? "? CCBC observed value" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1471 && offset <= 1472)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1473 && offset <= 1474)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset >= 1475 && offset <= 1478)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1479)
                    {
                        return (isEn ? "? Player data" : "?") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1487)
                    {
                        return (isEn ? "Hitbox 1" : "ヒットボックス 1") + this.GetSlotSuffix(slotAddress, true);
                    }
                    else if (offset == 1488)
                    {
                        return (isEn ? "Hitbox 2" : "ヒットボックス 2") + this.GetSlotSuffix(slotAddress, true);
                    }
                }
            }

            UInt32 jpAddress = this.ConvertToJpAddress(address);
            switch (jpAddress)
            {
            }

            return String.Empty;
        }

        private String Constant(Boolean isEn, params UInt16[] constant)
        {
            return (isEn ? "Constant " : "定数 ") + String.Join(", ", constant.Select(x => x.ToString("X")));
        }

        private UInt32 ConvertToJpAddress(UInt32 address)
        {
            switch(MainViewModel.GetInstance().DetectedVersion)
            {
                default:
                case EDetectedVersion.JP: return address;
                case EDetectedVersion.EN: return address + 0x1C940;
                case EDetectedVersion.PAL: return address + 0x1B900;
            }
        }

        private String GetSlotSuffix(UInt32 address, Boolean isNextSlot = false)
        {
            UInt32[] slotDataAddresses = InventoryViewerViewModel.GetInstance().GetSlotDataAddresses();
            Boolean isEn = MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageEN;

            if (slotDataAddresses != null)
            {
                for (Int32 slotIndex = 0; slotIndex < 8; slotIndex++)
                {
                    if (address >= slotDataAddresses[slotIndex] && address < slotDataAddresses[slotIndex] + SlotDataSize)
                    {
                        String prefix = isEn ? " - (Character Slot " : "キャラクタースロット ";

                        prefix += (slotIndex + (isNextSlot ? 2 : 1)).ToString();
                        prefix += ")";

                        return prefix;
                    }
                }
            }

            return String.Empty;
        }
    }
    //// End class
}
//// End namespace