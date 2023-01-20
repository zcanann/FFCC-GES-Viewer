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

        enum LetterSender
        {
            Mother,
            Father,
            Sister,
            Brother,
            Roland,
        };

        private string GetNotes(UInt32 address)
        {
            address -= 0x80000000;;
            Boolean isEn = MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageEN;
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

                    if (offset <= 63)
                    {
                        return (isEn ? "Inventory Slot " : "キャラクタースロット ") + offset.ToString() + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset <= 136)
                    {
                        return (isEn ? "Artifact Slot " : "アーティファクトスロット ") + (offset - 64).ToString() + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 137 && offset <= 159)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 160 && offset <= 163)
                    {
                        UInt16 commandListOffset = (UInt16)(4 - (163 - offset));
                        return (isEn ? "Treasure slot " : "宝スロット ") + commandListOffset.ToString() + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 163)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 164)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 165)
                    {
                        return (isEn ? "Gil High Bytes (65535+)" : "ギルハイバイト (65535+)") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 166)
                    {
                        return (isEn ? "Gil Low Bytes (0-65535)" : "ギル低バイト (0-65535)") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 167 && offset <= 168)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 169 && offset <= 174)
                    {
                        UInt16 commandListOffset = (UInt16)(6 - (174 - offset));

                        return (isEn ? "Command list slot " : "コマンド・リスト・スロット ") + commandListOffset.ToString() + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 175 && offset <= 375)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 376)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 377)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 378)
                    {
                        return (isEn ? "? Sometimes Blizzard" : "?") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 379)
                    {
                        return this.Constant(isEn, 0x0000);
                    }
                    else if (offset == 380)
                    {
                        return (isEn ? "Family Type (0-7)" : "ファミリータイプ (0-7)") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 381)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 382)
                    {
                        return (isEn ? "Resistances from Equipment / Watched family cutscene" : "機器からの抵抗 / 見た家族のカットシーン") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 383)
                    {
                        return this.Constant(isEn, 0x0000, 0xFFFF) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 384)
                    {
                        return (isEn ? "GBA Port (0-4)" : "ゲームボーイアドバンス ポート 0-4") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 385 && offset <= 392)
                    {
                        UInt16 favoritesOffset = (UInt16)(7 - ((UInt16)392 - offset));
                        UInt16 itemId = (UInt16)((UInt16)0x017D + favoritesOffset);
                        String foodName = ItemToNameConverter.Convert(itemId, null, null, null)?.ToString();

                        return (isEn ? "Favorites (" + foodName + ")" : "お気に入り(" + foodName + ")") + " 0-64" + this.GetSlotSuffix(inventoryAddress);
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

                        return isEn ? "Name " + asciiRange + " | Name " + jisRange : "名前 " + asciiRange + " | 名前 " + jisRange + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 401 && offset <= 403)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 404)
                    {
                        return (isEn ? "Memories" : "思い出") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 405)
                    {
                        return (isEn ? "Tribe (0-3)" : "部族") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 406)
                    {
                        return (isEn ? "Gender (0-1)" : "性別") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 407)
                    {
                        return (isEn ? "Skin (0-3)" : "肌") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 408 && offset <= 409)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 410)
                    {
                        return (isEn ? "Letter Count" : "文字数") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 411 && offset <= 1010)
                    {
                        const UInt16 baseOffset = 411;
                        UInt16 letterOffset = (UInt16)((offset - baseOffset) % 6);
                        UInt16 letterIndex = (UInt16)((offset - baseOffset) / 6);
                        String prefix = (isEn ? "Letter #" : " 手紙 #") + (letterIndex + 1).ToString() + " - ";
                        String suffix = String.Empty;

                        switch(letterOffset)
                        {
                            case 0: return prefix + (isEn ? "Letter State (Opened, Replied) (Byte 0) / Letter Id (Byte 1)" : "文字の状態 (開封済み、返信済み) (バイト 0) / 文字 ID (バイト 1)") + suffix + this.GetSlotSuffix(inventoryAddress);
                            case 1: return prefix + (isEn ? "Item Attached (Byte 0) / Item Attached (Byte 1)" : "アイテム添付 (バイト 0) / アイテム添付 (バイト 1)") + suffix + this.GetSlotSuffix(inventoryAddress);
                            case 2: return prefix + (isEn ? "Attached Item ID 1" : "添付アイテム ID 1") + suffix + this.GetSlotSuffix(inventoryAddress);
                            case 3: return prefix + (isEn ? "Attached Item ID 2" : "添付アイテム ID 2") + suffix + this.GetSlotSuffix(inventoryAddress);
                            case 4: return prefix + this.Constant(isEn, 0x0000, 0x0003) + suffix + this.GetSlotSuffix(inventoryAddress);
                            case 5: return prefix + (isEn ? "? Rarely an id like 0x89 (Flower Bracer)" : "?") + suffix + this.GetSlotSuffix(inventoryAddress);
                        }
                    }
                    else if (offset == 1011)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1012)
                    {
                        return this.Constant(isEn, 0x0000, 0x0108, 0x0128) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1013 && offset <= 1014)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1015)
                    {
                        return this.Constant(isEn, 0x0000, 0x0008) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1016)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1017)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1018)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1019)
                    {
                        return this.Constant(isEn, 0x8000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1020 && offset <= 1114)
                    {
                        return this.LetterData(isEn, null) + this.GetSlotSuffix(inventoryAddress);
                    }
                    /*
                    else if (offset == 1021)
                    {
                        return this.LetterData(isEn,
                            this.RespondedLetterWithOption(isEn, 0x0001, 2, "母のしんぱい", "母のしんぱい", LetterSender.Mother) + this.GetSlotSuffix(slotAddress),
                            this.RespondedLetterWithOption(isEn, 0x0002, 1, "母のしんぱい", "母のしんぱい", LetterSender.Mother) + this.GetSlotSuffix(slotAddress),
                            this.RespondedLetterWithOption(isEn, 0x0003, 3, "母のしんぱい", "母のしんぱい", LetterSender.Mother) + this.GetSlotSuffix(slotAddress),
                            this.RecievedLetter(isEn, 0x0004, "スキキライ", "スキキライ", LetterSender.Mother) + this.GetSlotSuffix(slotAddress),
                            this.RespondedLetterWithOption(isEn, 0x0008, 2, "スキキライ", "スキキライ", LetterSender.Mother) + this.GetSlotSuffix(slotAddress),
                            this.RespondedLetterWithOption(isEn, 0x0010, 1, "スキキライ", "スキキライ", LetterSender.Mother) + this.GetSlotSuffix(slotAddress),
                            this.RecievedLetter(isEn, 0x8000, "母のしんぱい", "母のしんぱい", LetterSender.Mother) + this.GetSlotSuffix(slotAddress)
                        );
                    }
                    else if (offset == 1022)
                    {
                        // this.RespondedLetterWithItemOrGil(isEn, 0x0080, "あわいさゃんい", "あわいさゃんい", "Father", "ちち") + this.GetSlotSuffix(slotAddress),
                        // this.RecievedLetter(isEn, 0x0800, "あわいさゃんい", "あわいさゃんい", "Father", "ちち") + this.GetSlotSuffix(slotAddress)
                        return this.LetterData(isEn,
                            this.RespondedLetterWithItemOrGil(isEn, 0x0080, "あわいさゃんい", "あわいさゃんい", LetterSender.Sister) + this.GetSlotSuffix(slotAddress),
                            this.RecievedLetter(isEn, 0x0800, "あわいさゃんい", "あわいさゃんい", LetterSender.Sister) + this.GetSlotSuffix(slotAddress)
                        );
                    }
                    else if (offset == 1024)
                    {
                        return this.LetterData(isEn,
                            this.RespondedLetterWithNothing(isEn, 0x0001, "われらのキボウたち", "われらのキボウたち", LetterSender.Roland) + this.GetSlotSuffix(slotAddress),
                            this.RespondedLetterWithItemOrGil(isEn, 0x0002, "われらのキボウたち", "われらのキボウたち", LetterSender.Roland) + this.GetSlotSuffix(slotAddress),
                            this.RecievedLetter(isEn, 0x8000, "われらのキボウたち", "われらのキボウたち", LetterSender.Roland) + this.GetSlotSuffix(slotAddress)
                        );
                    }*/
                    else if (offset >= 1115 && offset <= 1126)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1127)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1128 && offset <= 1144)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1145)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001, 0x0005, 0x000E) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1146 && offset <= 1148)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1149)
                    {
                        return (isEn ? "Gil during letter reply Bytes (max 30000 / 0x7530)" : "レター返信中のギルバイト(最大30000 / 0x7530)") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1150)
                    {
                        return this.Constant(isEn, 0x0000, 0x3A98) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1151)
                    {
                        return this.Constant(isEn, 0x0000, 0x0001) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1152)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1153 && offset <= 1161)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1162 && offset <= 1167)
                    {
                        return (isEn ? "? 0, or Roughly in range of 0x80-0xFF" : "?") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1168 && offset <= 1171)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1172)
                    {
                        return this.Constant(isEn, 0x0000, 0x0004) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1173)
                    {
                        return (isEn ? "Gil to family / 10" : "ギルから家族へ / 10") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1174)
                    {
                        return this.Constant(isEn, 0x0000, 0x0005) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1175)
                    {
                        return this.Constant(isEn, 0x0000, 0x000A) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1176)
                    {
                        return this.Constant(isEn, 0x0000, 0x000A) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1177 && offset <= 1192)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1193)
                    {
                        return this.Constant(isEn, 0x0000, 0x0036) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1194 && offset <= 1222)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1223 && offset <= 1226)
                    {
                        return (isEn ? "Last eaten food" : "最後に食べた食べ物") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1227 && offset <= 1346)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    // <Slot rollover starts here!>
                    else if (offset >= 1347 && offset <= 1398)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1399)
                    {
                        return (isEn ? "? Large value ranges observed" : "?") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1400)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1401)
                    {
                        return this.Constant(isEn, 0x0004) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1402)
                    {
                        return this.Constant(isEn, 0x0004, 0x0006) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1403 && offset <= 1417)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1418)
                    {
                        return this.Constant(isEn, 0x0000, 0x64) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1419)
                    {
                        return this.Constant(isEn, 0x0000, 0x0019) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1420 && offset <= 1421)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1422)
                    {
                        return (isEn ? "Food buff timer" : "フードバフタイマー") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1423)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1424)
                    {
                        return (isEn ? "Last eaten food" : "最後に食べた食べ物") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1425 && offset <= 1428)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1429)
                    {
                        return (isEn ? "Merchant item count" : "販売者のアイテム数") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1430 && offset <= 1449)
                    {
                        return (isEn ? "Merchant shop item" : "加盟店アイテム") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1450)
                    {
                        return this.Constant(isEn, 0x0000, 0x07FF) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1451 && offset <= 1456)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1457)
                    {
                        return this.Constant(isEn, 0x0000, 0x0017, 0x002C) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1458)
                    {
                        return this.Constant(isEn, 0x0000, 0x000A, 0x000D) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1459)
                    {
                        return this.Constant(isEn, 0x0000, 0x0017, 0x002E) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1460)
                    {
                        return this.Constant(isEn, 0x0000, 0x0100) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1461)
                    {
                        return this.Constant(isEn, 0x0000, 0x0100) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1462)
                    {
                        return (isEn ? "? 192A observed value" : "?") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1463 && offset <= 1468)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1469)
                    {
                        return (isEn ? "? 8022 observed value" : "?") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1470)
                    {
                        return (isEn ? "? CCBC observed value" : "?") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1471 && offset <= 1472)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1473 && offset <= 1474)
                    {
                        return this.Constant(isEn, 0xFFFF) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset >= 1475 && offset <= 1478)
                    {
                        return this.Constant(isEn, 0x0000) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1479)
                    {
                        return (isEn ? "Tribe (0-3)" : "部族") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1480)
                    {
                        return this.Constant(isEn, 0x0000, 0x002F) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1481)
                    {
                        return this.Constant(isEn, 0x0000, 0x0012) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1482)
                    {
                        return this.Constant(isEn, 0x0000, 0x002F) + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1482)
                    {
                        return (isEn ? "Health" : "健康") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1483)
                    {
                        return (isEn ? "Max Health" : "最大体力") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1484)
                    {
                        return (isEn ? "ATK" : "こうげき") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1485)
                    {
                        return (isEn ? "MAG" : "まほう") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1486)
                    {
                        return (isEn ? "DEF" : "まうぎよ") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1487)
                    {
                        return (isEn ? "Hitbox 1" : "ヒットボックス 1") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1488)
                    {
                        return (isEn ? "Hitbox 2 (Big Chungus Glitch)" : "ヒットボックス 2") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1555)
                    {
                        return (isEn ? "Equipped Weapon" : "装備武器") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1556)
                    {
                        return (isEn ? "Equipped Armor" : "装備された装甲") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1557)
                    {
                        return (isEn ? "Equipped Tribal" : "装備された部族") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1558)
                    {
                        return (isEn ? "Equipped Accessory" : "装備アクセサリー") + this.GetSlotSuffix(inventoryAddress);
                    }
                    else if (offset == 1559)
                    {
                        return (isEn ? "Inventory Item Count" : "在庫品目数") + this.GetSlotSuffix(inventoryAddress);
                    }
                }
            }

            UInt32 jpAddress = this.ConvertToJpAddress(address);
            switch (jpAddress)
            {
            }

            return String.Empty;
        }

        private String LetterData(Boolean isEn, params String[] letters)
        {
            // return String.Join(Environment.NewLine, letters);

            return isEn ? "Letter Data (Recieved / Response)" : "レターデータ(受信/応答)";
        }

        private String LetterSenderToString(Boolean isEn, LetterSender letterSender)
        {
            switch (letterSender)
            {
                case LetterSender.Sister: return isEn ? "Sister" : "いもうと";
                case LetterSender.Mother: return isEn ? "Mother" : "はは";
                case LetterSender.Brother: return isEn ? "Brother" : "おとうと";
                case LetterSender.Father: return isEn ? "Father" : "ちち";
                case LetterSender.Roland: return isEn ? "Roland" : "ローラン";
            }

            return String.Empty;
        }

        private String GetLetterInfo(Boolean isEn, String letterNameEn, String letterNameJp, LetterSender letterSender)
        {
            String letterInfoEn = letterNameEn + " from " + this.LetterSenderToString(isEn, letterSender);
            String letterInfoJp = this.LetterSenderToString(isEn, letterSender) + " の " + letterNameJp;

            return isEn ? letterInfoEn : letterInfoJp;
        }

        private String BuildLetterFlagPrefix(UInt16 flag)
        {
            return flag.ToString("X4") + " - ";
        }

        private String RespondedLetterWithOption(Boolean isEn, UInt16 flag, Int32 optionIndex, String letterNameEn, String letterNameJp, LetterSender letterSender)
        {
            String letterInfo = this.GetLetterInfo(isEn, letterNameEn, letterNameJp, letterSender);

            return this.BuildLetterFlagPrefix(flag) + (isEn ? ("Responded with option #" + optionIndex.ToString() + " to ") : ("オプションで応答# : " + letterInfo));
        }

        private String RespondedLetterWithGil(Boolean isEn, UInt16 flag, UInt16 minGil, String letterNameEn, String letterNameJp, LetterSender letterSender)
        {
            String letterInfo = this.GetLetterInfo(isEn, letterNameEn, letterNameJp, letterSender);

            return this.BuildLetterFlagPrefix(flag) + (isEn ? ("Responded to Letter with " + minGil.ToString() + " Gil: ") : (minGil.ToString() + "ギルで手紙に返信 : ")) + letterInfo;
        }

        private String RespondedLetterWithNothing(Boolean isEn, UInt16 flag, String letterNameEn, String letterNameJp, LetterSender letterSender)
        {
            String letterInfo = this.GetLetterInfo(isEn, letterNameEn, letterNameJp, letterSender);

            return this.BuildLetterFlagPrefix(flag) + (isEn ? ("Responded to Letter with nothing ") : ("何もせずに手紙に返信した ")) + letterInfo;
        }

        private String RespondedLetterWithItemOrGil(Boolean isEn, UInt16 flag, String letterNameEn, String letterNameJp, LetterSender letterSender)
        {
            String letterInfo = this.GetLetterInfo(isEn, letterNameEn, letterNameJp, letterSender);

            return this.BuildLetterFlagPrefix(flag) + (isEn ? ("Responded to Letter with item or Gil ") : ("アイテムまたはギルで手紙に返信しました ")) + letterInfo;
        }

        private String RecievedLetter(Boolean isEn, UInt16 flag, String letterNameEn, String letterNameJp, LetterSender letterSender)
        {
            String letterInfo = this.GetLetterInfo(isEn, letterNameEn, letterNameJp, letterSender);

            return this.BuildLetterFlagPrefix(flag) + (isEn ? ("Recieved Letter ") : ("受け取った手紙: ")) + letterInfo;
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

        private String GetSlotSuffix(UInt32 address)
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

                        prefix += (slotIndex + 1).ToString();
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