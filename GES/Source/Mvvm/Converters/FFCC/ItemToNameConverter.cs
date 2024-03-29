﻿namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.Main;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ItemToNameConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value is UInt16)
            {
                UInt16 itemId  = (UInt16) value;

                if (MainViewModel.GetInstance().SelectedLanguage == MainViewModel.LanguageJPN)
                {
                    return this.GetItemNameJp(itemId);
                }
                else
                {
                    return this.GetItemNameEn(itemId);
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

        private string GetItemNameJp(UInt16 itemId)
        {
            switch (itemId)
            {
                case 0x0000: return "<ヌル>";
                case 0x1: return "みがるなけん";
                case 0x2: return "アイアンソード";
                case 0x3: return "セーフブレイド";
                case 0x4: return "フェザーセイバー";
                case 0x5: return "バスタードソード";
                case 0x6: return "ディフェンダー";
                case 0x7: return "ルーンブレイド";
                case 0x8: return "エクスカリバー";
                case 0x9: return "ラグナロク";
                case 0xa: return "ひぞうのけん";
                case 0xb: return "オヤジのけん";
                case 0xc: return "マールのけん";
                case 0xd: return "じっけん１";
                case 0xe: return "じっけん２";
                case 0xf: return "アルテマソード";
                case 0x10: return "10 そうびひん１１６";
                case 0x11: return "そうびひん１１７";
                case 0x12: return "アイアンランス";
                case 0x13: return "パルチザン";
                case 0x14: return "ソニックランス";
                case 0x15: return "タイタンランス";
                case 0x16: return "ハルバード";
                case 0x17: return "ハイウィンド";
                case 0x18: return "ひりゅうのやり";
                case 0x19: return "ドラグーンスピア";
                case 0x1a: return "グングニル";
                case 0x1b: return "ロンギヌス";
                case 0x1c: return "ひぞうのやり";
                case 0x1d: return "オヤジのやり";
                case 0x1e: return "マールのやり";
                case 0x1f: return "アルテマランス";
                case 0x20: return "そうびひん１１９";
                case 0x21: return "そうびひん１２０";
                case 0x22: return "そうびひん１２１";
                case 0x23: return "そうびひん１２２";
                case 0x24: return "オークハンマー";
                case 0x25: return "ウェイブハンマー";
                case 0x26: return "ルーンハンマー";
                case 0x27: return "ゴブリンハンマー";
                case 0x28: return "ソニックハンマー";
                case 0x29: return "プリズムハンマー";
                case 0x2a: return "ミスリルハンマー";
                case 0x2b: return "マジカルハンマー";
                case 0x2c: return "ひぞうのハンマー";
                case 0x2d: return "オヤジのハンマー";
                case 0x2e: return "マールのハンマー";
                case 0x2f: return "アルテマハンマー";
                case 0x30: return "そうびひん１２４";
                case 0x31: return "そうびひん１２５";
                case 0x32: return "そうびひん１２６";
                case 0x33: return "そうびひん１２７";
                case 0x34: return "オーラシュート";
                case 0x35: return "ハードスマッシュ";
                case 0x36: return "ダブルシュート";
                case 0x37: return "エレメントパドル";
                case 0x38: return "ハードビート";
                case 0x39: return "プリズムシュート";
                case 0x3a: return "バタフライヘッド";
                case 0x3b: return "クイーンヒール";
                case 0x3c: return "グランドスラム";
                case 0x3d: return "ひぞうのラケット";
                case 0x3e: return "オヤジのラケット";
                case 0x3f: return "マールのラケット";
                case 0x40: return "アルテマラケット";
                case 0x41: return "そうびひん１２９";
                case 0x42: return "そうびひん１３０";
                case 0x43: return "そうびひん１３１";
                case 0x44: return "そうびひん１３２";
                case 0x45: return "たびだちのふく";
                case 0x46: return "ブロンズアーマー";
                case 0x47: return "アイアンアーマー";
                case 0x48: return "ミスリルアーマー";
                case 0x49: return "フレイムメイル";
                case 0x4a: return "アイスメイル";
                case 0x4b: return "サンダーメイル";
                case 0x4c: return "タイムメイル";
                case 0x4d: return "エターナルメイル";
                case 0x4e: return "ブレスメイル";
                case 0x4f: return "セイントメイル";
                case 0x50: return "ゴールドメイル";
                case 0x51: return "クリスタルメイル";
                case 0x52: return "ダイヤアーマー";
                case 0x53: return "ガイアアーマー";
                case 0x54: return "ふしぎのよろい";
                case 0x55: return "ひょうたんのふく";
                case 0x56: return "はじめてのふく";
                case 0x57: return "だぶだぶのふく";
                case 0x58: return "おてせいのたて";
                case 0x59: return "アイアンシールド";
                case 0x5a: return "ミスリルシールド";
                case 0x5b: return "フレイムシールド";
                case 0x5c: return "アイスシールド";
                case 0x5d: return "サンダーシールド";
                case 0x5e: return "セイントシールド";
                case 0x5f: return "ダイヤシールド";
                case 0x60: return "ルーンシールド";
                case 0x61: return "チョコボシールド";
                case 0x62: return "はじめてのこて";
                case 0x63: return "ブロンズウォール";
                case 0x64: return "アイアンウォール";
                case 0x65: return "ミスリルウォール";
                case 0x66: return "フレイムガード";
                case 0x67: return "アイスガード";
                case 0x68: return "サンダーガード";
                case 0x69: return "ゴールドガード";
                case 0x6a: return "ダイヤプロテクト";
                case 0x6b: return "ノーマルメット";
                case 0x6c: return "ブロンズメット";
                case 0x6d: return "アイアンメット";
                case 0x6e: return "ミスリルバイザー";
                case 0x6f: return "フレイムヘルム";
                case 0x70: return "アイスヘルム";
                case 0x71: return "サンダーヘルム";
                case 0x72: return "タイムヘルム";
                case 0x73: return "エターナルヘルム";
                case 0x74: return "ダイヤバイザー";
                case 0x75: return "おふるのベルト";
                case 0x76: return "ブロンズベルト";
                case 0x77: return "アイアンベルト";
                case 0x78: return "ミスリルベルト";
                case 0x79: return "フレイムバックル";
                case 0x7a: return "アイスバックル";
                case 0x7b: return "サンダーバックル";
                case 0x7c: return "ブレスバックル";
                case 0x7d: return "ウィングベルト";
                case 0x7e: return "ダイヤベルト";
                case 0x7f: return "ほのおのバッジ";
                case 0x80: return "こおりのバッジ";
                case 0x81: return "かみなりのバッジ";
                case 0x82: return "せいかくなとけい";
                case 0x83: return "とまらないとけい";
                case 0x84: return "ブルーミサンガ";
                case 0x85: return "ホワイトミサンガ";
                case 0x86: return "きんのネックレス";
                case 0x87: return "ちえのおまもり";
                case 0x88: return "ちえのごふ";
                case 0x89: return "おはなのうでわ";
                case 0x8a: return "はやさのおまもり";
                case 0x8b: return "はやさのごふ";
                case 0x8c: return "シーフエンブレム";
                case 0x8d: return "きあいはちまき";
                case 0x8e: return "デーモンピアス";
                case 0x8f: return "サタンピアス";
                case 0x90: return "ピクシーピアス";
                case 0x91: return "エンジェルピアス";
                case 0x92: return "クリスタルリング";
                case 0x93: return "ぐるぐるめがね";
                case 0x94: return "ぐるぐるスコープ";
                case 0x95: return "いやしのはちまき";
                case 0x96: return "ひすいのうでわ";
                case 0x97: return "パワーゴーグル";
                case 0x98: return "イーグルゴーグル";
                case 0x99: return "ライオンハート";
                case 0x9a: return "ドラゴンハート";
                case 0x9b: return "ウィザードソウル";
                case 0x9c: return "ビショップソウル";
                case 0x9d: return "エレメントソウル";
                case 0x9e: return "フォースリング";
                case 0x9f: return "しゅりけん";
                case 0xa0: return "マンイーター";
                case 0xa1: return "ダブルハーケン";
                case 0xa2: return "あしゅら";
                case 0xa3: return "カイザーナックル";
                case 0xa4: return "フレイムタン";
                case 0xa5: return "アイスブランド";
                case 0xa6: return "イカサマダイス";
                case 0xa7: return "オーガキラー";
                case 0xa8: return "えんげつりん";
                case 0xa9: return "さすけのかたな";
                case 0xaa: return "トールハンマー";
                case 0xab: return "マスカレイド";
                case 0xac: return "アメノムラクモ";
                case 0xad: return "マサムネ";
                case 0xae: return "げっかびじん";
                case 0xaf: return "オニオンソード";
                case 0xb0: return "パワーリスト";
                case 0xb1: return "グリーンベレー";
                case 0xb2: return "きばのおまもり";
                case 0xb3: return "ねじりはちまき";
                case 0xb4: return "ちからだすき";
                case 0xb5: return "きょじんのこて";
                case 0xb6: return "りゅうのひげ";
                case 0xb7: return "メイジマッシャー";
                case 0xb8: return "ルーンのつえ";
                case 0xb9: return "ひかりのじてん";
                case 0xba: return "けんじゃのつえ";
                case 0xbb: return "ワンダーワンド";
                case 0xbc: return "ルーンのベル";
                case 0xbd: return "メイジスタッフ";
                case 0xbe: return "ノアのリュート";
                case 0xbf: return "ガラティーン";
                case 0xc0: return "アルテマのしょ";
                case 0xc1: return "ぎんのうでわ";
                case 0xc2: return "タマのすず";
                case 0xc3: return "フェアリーリング";
                case 0xc4: return "はねつきぼうし";
                case 0xc5: return "キャンディリング";
                case 0xc6: return "クリスナイフ";
                case 0xc7: return "あかいくつ";
                case 0xc8: return "ダークマター";
                case 0xc9: return "きんのかみかざり";
                case 0xca: return "トウテツパターン";
                case 0xcb: return "リボン";
                case 0xcc: return "マインゴーシュ";
                case 0xcd: return "チキンナイフ";
                case 0xce: return "セイブザクィーン";
                case 0xcf: return "ドリル";
                case 0xd0: return "バックラー";
                case 0xd1: return "ぎんぶちめがね";
                case 0xd2: return "きれいなうでわ";
                case 0xd3: return "くろずきん";
                case 0xd4: return "アライのメット";
                case 0xd5: return "エルフのマント";
                case 0xd6: return "ワンダーバングル";
                case 0xd7: return "まもりのゆびわ";
                case 0xd8: return "イージスのたて";
                case 0xd9: return "ねずみのしっぽ";
                case 0xda: return "くまちゃん";
                case 0xdb: return "モーグリポケット";
                case 0xdc: return "チョコボポケット";
                case 0xdd: return "ゴブリンポケット";
                case 0xde: return "ラストポケット";
                case 0xdf: return "ファイアリング";
                case 0xe0: return "ブリザドリング";
                case 0xe1: return "サンダーリング";
                case 0xe2: return "ケアルリング";
                case 0xe3: return "レイズリング";
                case 0xe4: return "アースペンダント";
                case 0xe5: return "ムーンペンダント";
                case 0xe6: return "スターペンダント";
                case 0xe7: return "サンペンダント";
                case 0xe8: return "そうびひん１８７";
                case 0xe9: return "そうびひん１８８";
                case 0xea: return "そうびひん１８９";
                case 0xeb: return "そうびひん１９０";
                case 0xec: return "そうびひん１９１";
                case 0xed: return "そうびひん１９２";
                case 0xee: return "そうびひん１９３";
                case 0xef: return "そうびひん１９４";
                case 0xf0: return "そうびひん１９５";
                case 0xf1: return "そうびひん１９６";
                case 0xf2: return "そうびひん１９７";
                case 0xf3: return "そうびひん１９８";
                case 0xf4: return "そうびひん１９９";
                case 0xf5: return "そうびひん２００";
                case 0xf6: return "そうびひん２０１";
                case 0xf7: return "そうびひん２０２";
                case 0xf8: return "そうびひん２０３";
                case 0xf9: return "そうびひん２０４";
                case 0xfa: return "そうびひん２０５";
                case 0xfb: return "そうびひん２０６";
                case 0xfc: return "そうびひん２０７";
                case 0xfd: return "そうびひん２０８";
                case 0xfe: return "そうびひん２０９";
                case 0xff: return "そうびひん２１０";
                case 0x100: return "ファイア";
                case 0x101: return "ブリザド";
                case 0x102: return "サンダー";
                case 0x103: return "？？？";
                case 0x104: return "スロウ";
                case 0x105: return "ケアル";
                case 0x106: return "クリア";
                case 0x107: return "レイズ";
                case 0x108: return "ホーリー";
                case 0x109: return "ストップ";
                case 0x10a: return "グラビデ";
                case 0x10b: return "のろい";
                case 0x10c: return "びょうき";
                case 0x10d: return "まほうじん";
                case 0x10e: return "アルテマ";
                case 0x10f: return "フレア";
                case 0x110: return "メテオ";
                case 0x111: return "あんこく";
                case 0x112: return "きあい";
                case 0x113: return "ぶんどる";
                case 0x114: return "こんらん";
                case 0x115: return "せんぷう";
                case 0x116: return "しらべる";
                case 0x117: return "おどす";
                case 0x118: return "こうぶつ";
                case 0x119: return "ダイス";
                case 0x11a: return "なすりつけ";
                case 0x11b: return "したがえる";
                case 0x11c: return "ぜんたい";
                case 0x11d: return "おうぎがた";
                case 0x11e: return "えん";
                case 0x11f: return "ついび";
                case 0x120: return "きょうか";
                case 0x121: return "みがわり";
                case 0x122: return "ついか";
                case 0x123: return "はんしゃ";
                case 0x124: return "バリア";
                case 0x125: return "フェニックスのお";
                case 0x126: return "どう";
                case 0x127: return "てつ";
                case 0x128: return "ミスリル";
                case 0x129: return "オリハルコン";
                case 0x12a: return "ダイヤのもと";
                case 0x12b: return "きん";
                case 0x12c: return "ぎん";
                case 0x12d: return "どうのかけら";
                case 0x12e: return "てつのかけら";
                case 0x12f: return "プチクリスタル";
                case 0x130: return "ガラスだま";
                case 0x131: return "ルビー";
                case 0x132: return "ひすい";
                case 0x133: return "こんごうせき";
                case 0x134: return "マグマいわ";
                case 0x135: return "ひんやりゼリー";
                case 0x136: return "かみなりだま";
                case 0x137: return "せいすい";
                case 0x138: return "エンジェルダスト";
                case 0x139: return "きいろいはね";
                case 0x13a: return "ブルーシルク";
                case 0x13b: return "ホワイトシルク";
                case 0x13c: return "あくまのつめ";
                case 0x13d: return "さたんのつめ";
                case 0x13e: return "ようせいのなみだ";
                case 0x13f: return "てんしのなみだ";
                case 0x140: return "ふるびたけん";
                case 0x141: return "のろいのつえ";
                case 0x142: return "オークベルト";
                case 0x143: return "おうのうろこ";
                case 0x144: return "グリーンスフィア";
                case 0x145: return "ドラゴンのきば";
                case 0x146: return "モルボルのたね";
                case 0x147: return "さばくのきば";
                case 0x148: return "かぜのけっしょう";
                case 0x149: return "まかいのオーブ";
                case 0x14a: return "レッドアイ";
                case 0x14b: return "まのほうし";
                case 0x14c: return "ロードガウン";
                case 0x14d: return "グリフォンのはね";
                case 0x14e: return "ケルベロスのキバ";
                case 0x14f: return "はりいっぽん";
                case 0x150: return "がちがちこうら";
                case 0x151: return "ワームアンテナ";
                case 0x152: return "ガマのあぶら";
                case 0x153: return "ざっくりカマ";
                case 0x154: return "オーガのキバ";
                case 0x155: return "キマイラのツノ";
                case 0x156: return "さくもつのたね";
                case 0x157: return "クアールのヒゲ";
                case 0x158: return "ズーのくちばし";
                case 0x159: return "コカうろこ";
                case 0x15a: return "いにしえのひやく";
                case 0x15b: return "きらきらのかけら";
                case 0x15c: return "ギガースのツメ";
                case 0x15d: return "はぐるま";
                case 0x15e: return "おしばな";
                case 0x15f: return "かいふくやく";
                case 0x160: return "めがみぞう";
                case 0x161: return "あくまのおめん";
                case 0x162: return "よび22";
                case 0x163: return "はなのたね";
                case 0x164: return "ふしぎなたね";
                case 0x165: return "くだもののタネ";
                case 0x166: return "くだもののタネ";
                case 0x167: return "くだもののタネ";
                case 0x168: return "やさいのタネ";
                case 0x169: return "やさいのタネ";
                case 0x16a: return "やさいのタネ";
                case 0x16b: return "こむぎのタネ";
                case 0x16c: return "ひろったバンダナ";
                case 0x16d: return "シェラのあかし";
                case 0x16e: return "キランダのいおう";
                case 0x16f: return "サボテンのはな";
                case 0x170: return "よび23";
                case 0x171: return "アルテマイト";
                case 0x172: return "ダークスフィア";
                case 0x173: return "よび24";
                case 0x174: return "よび25";
                case 0x175: return "よび26";
                case 0x176: return "よび27";
                case 0x177: return "よび28";
                case 0x178: return "よび29";
                case 0x179: return "よび30";
                case 0x17a: return "よび31";
                case 0x17b: return "よび32";
                case 0x17c: return "よび33";
                case 0x17d: return "しましまリンゴ";
                case 0x17e: return "すずなりチェリー";
                case 0x17f: return "にじいろブドウ";
                case 0x180: return "ほしがたにんじん";
                case 0x181: return "ひょうたんいも";
                case 0x182: return "まんまるコーン";
                case 0x183: return "ニク";
                case 0x184: return "さかな";
                case 0x185: return "いなかパン";
                case 0x186: return "ミネ";
                case 0x187: return "ミルク";
                case 0x188: return "ふしぎなえきたい";
                case 0x189: return "ほのおのかけら";
                case 0x18a: return "みずのかけら";
                case 0x18b: return "かみなりのかけら";
                case 0x18c: return "やみのかけら";
                case 0x18d: return "こむぎのたば";
                case 0x18e: return "こむぎこ";
                case 0x18f: return "？？？";
                case 0x190: return "アイテムギル";
                case 0x191: return "みならいのぶき";
                case 0x192: return "せんしのぶき";
                case 0x193: return "ゆうきのぶき";
                case 0x194: return "ちからのぶき";
                case 0x195: return "しょうりのぶき";
                case 0x196: return "たつじんのぶき";
                case 0x197: return "でんせつのぶき";
                case 0x198: return "ゆうしゃのぶき";
                case 0x199: return "てんかいのぶき";
                case 0x19a: return "やみのぶき";
                case 0x19b: return "つきのぶき";
                case 0x19c: return "どうのよろい";
                case 0x19d: return "てつのよろい";
                case 0x19e: return "ミスリルのよろい";
                case 0x19f: return "ほのおのよろい";
                case 0x1a0: return "れいきのよろい";
                case 0x1a1: return "でんきのよろい";
                case 0x1a2: return "ときのよろい";
                case 0x1a3: return "とわのよろい";
                case 0x1a4: return "きよめのよろい";
                case 0x1a5: return "せいなるよろい";
                case 0x1a6: return "きんのよろい";
                case 0x1a7: return "ひかりのよろい";
                case 0x1a8: return "ダイヤのよろい";
                case 0x1a9: return "だいちのよろい";
                case 0x1aa: return "てつのたて";
                case 0x1ab: return "ミスリルのたて";
                case 0x1ac: return "ほのおのたて";
                case 0x1ad: return "れいきのたて";
                case 0x1ae: return "でんきのたて";
                case 0x1af: return "せいなるたて";
                case 0x1b0: return "ダイヤのたて";
                case 0x1b1: return "まほうのたて";
                case 0x1b2: return "でんせつのとり";
                case 0x1b3: return "どうのこて";
                case 0x1b4: return "てつのこて";
                case 0x1b5: return "ミスリルのこて";
                case 0x1b6: return "ほのおのこて";
                case 0x1b7: return "れいきのこて";
                case 0x1b8: return "でんきのこて";
                case 0x1b9: return "きんのこて";
                case 0x1ba: return "ダイヤのこて";
                case 0x1bb: return "どうのかぶと";
                case 0x1bc: return "てつのかぶと";
                case 0x1bd: return "ミスリルのかぶと";
                case 0x1be: return "ほのおのかぶと";
                case 0x1bf: return "れいきのかぶと";
                case 0x1c0: return "でんきのかぶと";
                case 0x1c1: return "ときのかぶと";
                case 0x1c2: return "とわのかぶと";
                case 0x1c3: return "ダイヤのかぶと";
                case 0x1c4: return "どうのベルト";
                case 0x1c5: return "てつのベルト";
                case 0x1c6: return "ミスリルのベルト";
                case 0x1c7: return "ほのおのベルト";
                case 0x1c8: return "れいきのベルト";
                case 0x1c9: return "でんきのベルト";
                case 0x1ca: return "きよめのベルト";
                case 0x1cb: return "かぜのベルト";
                case 0x1cc: return "ダイヤのベルト";
                case 0x1cd: return "ほのおのさいく";
                case 0x1ce: return "れいきのさいく";
                case 0x1cf: return "でんきのさいく";
                case 0x1d0: return "とけいざいく";
                case 0x1d1: return "しんとけいざいく";
                case 0x1d2: return "あおいあみひも";
                case 0x1d3: return "しろいあみひも";
                case 0x1d4: return "きんざいく";
                case 0x1d5: return "ちえのしょ";
                case 0x1d6: return "ちえのひでん";
                case 0x1d7: return "おんなのこグッズ";
                case 0x1d8: return "はやさのしょ";
                case 0x1d9: return "はやさのひでん";
                case 0x1da: return "とうぞくのごくい";
                case 0x1db: return "きあいグッズ";
                case 0x1dc: return "あくまグッズ";
                case 0x1dd: return "まおうグッズ";
                case 0x1de: return "ようせいグッズ";
                case 0x1df: return "てんしグッズ";
                case 0x1e0: return "ひかりのリング";
                case 0x1e1: return "メガネぎじゅつ";
                case 0x1e2: return "ブランドメガネ";
                case 0x1e3: return "いやしグッズ";
                case 0x1e4: return "おしゃれグッズ";
                case 0x1e5: return "ゴーグルぎじゅつ";
                case 0x1e6: return "ブランドゴーグル";
                case 0x1e7: return "ししのたましい";
                case 0x1e8: return "りゅうのたましい";
                case 0x1e9: return "まどうしょ";
                case 0x1ea: return "だいまどうしょ";
                case 0x1eb: return "きんじゅのしょ";
                case 0x1ec: return "きゅうきょくぶき";
                case 0x1ed: return "てっぺきのゆびわ";
                case 0x1ee: return "レシピ９４";
                case 0x1ef: return "レシピ９５";
                case 0x1f0: return "レシピ９６";
                case 0x1f1: return "レシピ９７";
                case 0x1f2: return "レシピ９８";
                case 0x1f3: return "レシピ９９";
                case 0x1f4: return "レシピ１００";
                case 0x01F5: return "破壊";
                case 0x01F6: return "KO";
                case 0x01F7: return "???";
                case 0x01F8: return "パワースラッシュ";
                case 0x01F9: return "貫通スイープ";
                case 0x01FA: return "バッシュ";
                case 0x01FB: return "ソウルショット";
                case 0x01FC: return "シャドウブレード";
                case 0x01FD: return "サイクロンスラッシュ";
                case 0x01FE: return "サイブラスト";
                case 0x01FF: return "アバランチ";
                case 0x0200: return "パルススラスト";
                case 0x0201: return "クロススラッシュ";
                case 0x0202: return "ブレードストーム";
                case 0x0203: return "パワーボム";
                case 0x0204: return "ウェーブボム";
                case 0x0205: return "ショックボム";
                case 0x0206: return "魔法爆弾";
                case 0x0207: return "ファイア";
                case 0x0208: return "ファイラ";
                case 0x0209: return "ファイラ +1";
                case 0x020A: return "ファイラ +2";
                case 0x020B: return "ブリザド";
                case 0x020C: return "ブリザラ";
                case 0x020D: return "ブリザラ +1";
                case 0x020E: return "ブリザラ +2";
                case 0x020F: return "サンダー";
                case 0x0210: return "サンダラ";
                case 0x0211: return "サンダラ +1";
                case 0x0212: return "サンダラ +2";
                case 0x0213: return "ヘイスト？（使用不可）";
                case 0x0214: return "スロウ";
                case 0x0215: return "ファイガ +1";
                case 0x0216: return "ファイガ +2";
                case 0x0217: return "スロウ..？";
                case 0x0218: return "ケアル";
                case 0x0219: return "ブリザガ +1";
                case 0x021A: return "ブリザガ +2";
                case 0x021B: return "ケアル..？";
                case 0x021C: return "クリア";
                case 0x021D: return "サンダガ +1";
                case 0x021E: return "サンダガ +2";
                case 0x021F: return "クリア？";
                case 0x0220: return "ライフ";
                case 0x0221: return "聖なる";
                case 0x0222: return "聖なるラ";
                case 0x0223: return "聖なるラ +1";
                case 0x0224: return "聖なるラ +2";
                case 0x0225: return "フルライフ";
                case 0x0226: return "グラビデ";
                case 0x0227: return "グラビガ";
                case 0x0228: return "グラビガ";
                case 0x0229: return "グラビガ";
                case 0x022A: return "ヘイスト";
                case 0x022B: return "ヘイスト？（失効）";
                case 0x022C: return "ヘイスト？（失効）";
                case 0x022D: return "ヘイスガ";
                case 0x022E: return "オーラブラスト";
                case 0x022F: return "スタンピード";
                case 0x0230: return "ファイガ";
                case 0x0231: return "ブリザガ";
                case 0x0232: return "サンダガ";
                case 0x0233: return "グラビガ";
                case 0x0234: return "スロウガ";
                case 0x0235: return "ヘイスガ";
                case 0x0236: return "デュアルブラスト";
                case 0x0237: return "パワーキック";
                case 0x0238: return "ケアルガ";
                case 0x0239: return "クリアガ";
                case 0x023A: return "ピックアップ";
                case 0x023B: return "テレポート";
                case 0x023C: return "メモリーズ";
                case 0x023D: return "メテオブラスト";
                case 0x023E: return "ストップ";
                case 0x023F: return "ストップ？（失効）";
                case 0x0240: return "ストップ？";
                case 0x0241: return "ストップ？";
                case 0x0242: return "フレイムストライク";
                case 0x0243: return "アイスストライク";
                case 0x0244: return "サンダーストライク";
                case 0x0245: return "スペル：貫通スイープ";
                case 0x0274: return "スペル：バイオ/バイオガ（自己ダメージ）";
                case 0x02A5: return "スペル：カース（自己呪い）";
                case 0x02A7: return "スペル：カース（自己呪い）";
                case 0x02B0: return "スペル：カース（自己呪い）";
                case 0x02CE: return "スペル：カース（自己呪い）";
                case 0x03AB: return "巨大カニの大きなハサミの一撃";
                case 0x03AC: return "巨大カニの小さなハサミの一撃";
                case 0x03AD: return "巨大カニのサンダラ";
                case 0x03AE: return "巨大カニの雷撃砲";
                case 0x03AF: return "巨大カニのスロウバブル";
                case 0x03B2: return "巨大カニのサンダガ";
                case 0x03B7: return "魔法：呪い（自己呪い）";
                case 0x03E0: return "魔法：バイオ／バイオガ（自己ダメージ）";
                case 0x0491: return "魔法：燃える油";
                case 0x0492: return "魔法：凍結した水（自己凍結）";
                case 0x0493: return "魔法：帯電した水（自己感電）";
                case 0x04AD: return "魔法：バイオ／バイオガ（自己ダメージ）";
                case 0x04AE: return "魔法：呪い（自己呪い）";
                case 0x04B2: return "魔法：バイオ／バイオガ（自己ダメージ）";
                case 0xFFFF: return "<なし>";
            }

            if (itemId >= 0x01F5 && itemId <= 0x7FFF)
            {
                return "<不明> (遅れ/クラッシュ)";
            }
            else
            {
                return "<不明>";
            }
        }

        private string GetItemNameEn(UInt16 itemId)
        {
            switch (itemId)
            {
                case 0x0000: return "<Null>";
                case 0x0001: return "Copper Sword";
                case 0x0002: return "Iron Sword";
                case 0x0003: return "Steel Blade";
                case 0x0004: return "Feather Saber";
                case 0x0005: return "Bastard Sword";
                case 0x0006: return "Defender";
                case 0x0007: return "Rune Blade";
                case 0x0008: return "Excalibur";
                case 0x0009: return "Ragnarok";
                case 0x000A: return "Treasured Sword";
                case 0x000B: return "Father's Sword";
                case 0x000C: return "Marr Sword";
                case 0x000D: return "Test 1";
                case 0x000E: return "Test 2";
                case 0x000F: return "Ultima Sword";
                case 0x0010: return "Equipment 116";
                case 0x0011: return "Equipment 117";
                case 0x0012: return "Iron Lance";
                case 0x0013: return "Partisan";
                case 0x0014: return "Sonic Lance";
                case 0x0015: return "Titan Lance";
                case 0x0016: return "Halberd";
                case 0x0017: return "Highwind";
                case 0x0018: return "Dragon Lance";
                case 0x0019: return "Dragoon Spear";
                case 0x001A: return "Gungnir";
                case 0x001B: return "Longinus";
                case 0x001C: return "Treasured Spear";
                case 0x001D: return "Father's Spear";
                case 0x001E: return "Marr Spear";
                case 0x001F: return "Ultima Lance";
                case 0x0020: return "Equipment 119 (Unused)";
                case 0x0021: return "Equipment 120 (Unused)";
                case 0x0022: return "Equipment 121 (Unused)";
                case 0x0023: return "Equipment 122 (Unused)";
                case 0x0024: return "Orc Hammer";
                case 0x0025: return "Wave Hammer";
                case 0x0026: return "Rune Hammer";
                case 0x0027: return "Goblin Hammer";
                case 0x0028: return "Sonic Hammer";
                case 0x0029: return "Prism Hammer";
                case 0x002A: return "Mythril Hammer";
                case 0x002B: return "Mystic Hammer";
                case 0x002C: return "Treasured Hammer";
                case 0x002D: return "Father's Hammer";
                case 0x002E: return "Marr Hammer";
                case 0x002F: return "Ultima Hammer";
                case 0x0030: return "Equipment 124 (Unused)";
                case 0x0031: return "Equipment 125 (Unused)";
                case 0x0032: return "Equipment 126 (Unused)";
                case 0x0033: return "Equipment 127 (Unused)";
                case 0x0034: return "Aura Racket";
                case 0x0035: return "Solid Racket";
                case 0x0036: return "Dual Shooter";
                case 0x0037: return "Elemental Cudgel";
                case 0x0038: return "Steel Cudgel";
                case 0x0039: return "Prism Bludgeon";
                case 0x003A: return "Butterfly Head";
                case 0x003B: return "Queen's Heel";
                case 0x003C: return "Dreamcatcher";
                case 0x003D: return "Treasured Maul";
                case 0x003E: return "Father's Maul";
                case 0x003F: return "Marr Maul";
                case 0x0040: return "Ultima Maul";
                case 0x0041: return "Equipment 129 (Unused)";
                case 0x0042: return "Equipment 130 (Unused)";
                case 0x0043: return "Equipment 131 (Unused)";
                case 0x0044: return "Equipment 132 (Unused)";
                case 0x0045: return "Travel Clothes";
                case 0x0046: return "Bronze Plate";
                case 0x0047: return "Iron Plate";
                case 0x0048: return "Mythril Hauberk";
                case 0x0049: return "Flame Mail";
                case 0x004A: return "Frost Mail";
                case 0x004B: return "Storm Mail";
                case 0x004C: return "Time Mail";
                case 0x004D: return "Eternal Mail";
                case 0x004E: return "Blessed Mail";
                case 0x004F: return "Saintly Mail";
                case 0x0050: return "Gold Mail";
                case 0x0051: return "Crystal Mail";
                case 0x0052: return "Diamond Plate";
                case 0x0053: return "Gaia Plate (Clavat)";
                case 0x0054: return "Mystic Armor (Unused)";
                case 0x0055: return "Taterskin Coat (Unused)";
                case 0x0056: return "Coat (Unused)";
                case 0x0057: return "Oversized Coat (Unused)";
                case 0x0058: return "Makeshift Shield";
                case 0x0059: return "Iron Shield";
                case 0x005A: return "Mythril Shield";
                case 0x005B: return "Flame Shield";
                case 0x005C: return "Frost Shield";
                case 0x005D: return "Storm Shield";
                case 0x005E: return "Saintly Shield";
                case 0x005F: return "Diamond Shield";
                case 0x0060: return "Rune Shield";
                case 0x0061: return "Chocobo Shield";
                case 0x0062: return "Gauntlets";
                case 0x0063: return "Bronze Gauntlets";
                case 0x0064: return "Iron Gauntlets";
                case 0x0065: return "Mythril Gauntlets";
                case 0x0066: return "Flame Armlets";
                case 0x0067: return "Frost Armlets";
                case 0x0068: return "Storm Armlets";
                case 0x0069: return "Gold Armlets";
                case 0x006A: return "Diamond Armlets";
                case 0x006B: return "Helm";
                case 0x006C: return "Bronze Helm";
                case 0x006D: return "Iron Helm";
                case 0x006E: return "Mythril Helm";
                case 0x006F: return "Flame Helm";
                case 0x0070: return "Frost Helm";
                case 0x0071: return "Storm Helm";
                case 0x0072: return "Time Helm";
                case 0x0073: return "Eternal Helm";
                case 0x0074: return "Diamond Helm";
                case 0x0075: return "Old Belt";
                case 0x0076: return "Bronze Belt";
                case 0x0077: return "Iron Belt";
                case 0x0078: return "Mythril Belt";
                case 0x0079: return "Flame Sash";
                case 0x007A: return "Frost Sash";
                case 0x007B: return "Storm Sash";
                case 0x007C: return "Blessed Sash";
                case 0x007D: return "Winged Belt";
                case 0x007E: return "Diamond Belt";
                case 0x007F: return "Badge of the Flame";
                case 0x0080: return "Badge of the Frost";
                case 0x0081: return "Badge of the Thunderbolt";
                case 0x0082: return "Accurate Watch";
                case 0x0083: return "Unfaltering Watch";
                case 0x0084: return "Blue Misanga";
                case 0x0085: return "White Misanga";
                case 0x0086: return "Gold Necklace";
                case 0x0087: return "Charm of Wisdom (Yuke)";
                case 0x0088: return "Talisman of Wisdom (Clavat)";
                case 0x0089: return "Flower Bracer (Female)";
                case 0x008A: return "Charm of Speed (Selkie)";
                case 0x008B: return "Talisman of Speed (Selkie)";
                case 0x008C: return "Thief's Emblem (Selkie)";
                case 0x008D: return "Headband of Zeal (Male)";
                case 0x008E: return "Daemon's Earring (Yuke)";
                case 0x008F: return "Devil's Earring (Clavat)";
                case 0x0090: return "Pixie's Earring (Lilty)";
                case 0x0091: return "Angel's Earring (Selkie)";
                case 0x0092: return "Crystal Ring";
                case 0x0093: return "Twisted Spectacles (Yuke)";
                case 0x0094: return "Twisted Scope (Clavat)";
                case 0x0095: return "Headband of Healing (Male)";
                case 0x0096: return "Jade Bracer (Female)";
                case 0x0097: return "Power Goggles (Lilty)";
                case 0x0098: return "Eagle Goggles (Selkie)";
                case 0x0099: return "Lion's Heart (Lilty)";
                case 0x009A: return "Dragon's Heart (Lilty)";
                case 0x009B: return "Wizard's Soul (Yuke)";
                case 0x009C: return "Bishop's Soul (Yuke)";
                case 0x009D: return "Elemental's Soul (Yuke)";
                case 0x009E: return "Force Ring";
                case 0x009F: return "Shuriken";
                case 0x00A0: return "Maneater";
                case 0x00A1: return "Double Axe";
                case 0x00A2: return "Ashura";
                case 0x00A3: return "Kaiser Knuckles";
                case 0x00A4: return "Flametongue";
                case 0x00A5: return "Ice Brand";
                case 0x00A6: return "Loaded Dice";
                case 0x00A7: return "Ogrekiller";
                case 0x00A8: return "Engetsurin";
                case 0x00A9: return "Sasuke's Blade";
                case 0x00AA: return "Mjollnir";
                case 0x00AB: return "Masquerade";
                case 0x00AC: return "Murasame";
                case 0x00AD: return "Masamune";
                case 0x00AE: return "Gekkabijin";
                case 0x00AF: return "Onion Sword";
                case 0x00B0: return "Power Wristband";
                case 0x00B1: return "Green Beret";
                case 0x00B2: return "Fang Charm";
                case 0x00B3: return "Twisted Headband";
                case 0x00B4: return "Heavy Armband";
                case 0x00B5: return "Giant's Glove";
                case 0x00B6: return "Dragon's Whisker";
                case 0x00B7: return "Mage Masher";
                case 0x00B8: return "Rune Staff";
                case 0x00B9: return "Book of Light";
                case 0x00BA: return "Sage's Staff";
                case 0x00BB: return "Wonder Wand";
                case 0x00BC: return "Rune Bell";
                case 0x00BD: return "Mage's Staff";
                case 0x00BE: return "Noah's Lute";
                case 0x00BF: return "Galatyn";
                case 0x00C0: return "Tome of Ultima";
                case 0x00C1: return "Silver Bracer";
                case 0x00C2: return "Cat's Bell";
                case 0x00C3: return "Faerie Ring";
                case 0x00C4: return "Winged Cap";
                case 0x00C5: return "Candy Ring";
                case 0x00C6: return "Kris";
                case 0x00C7: return "Red Slippers";
                case 0x00C8: return "Dark Matter";
                case 0x00C9: return "Gold Hairpin";
                case 0x00CA: return "Taotie Motif";
                case 0x00CB: return "Ribbon";
                case 0x00CC: return "Main Gauche";
                case 0x00CD: return "Chicken Knife";
                case 0x00CE: return "Save the Queen";
                case 0x00CF: return "Drill";
                case 0x00D0: return "Buckler";
                case 0x00D1: return "Silver Spectacles";
                case 0x00D2: return "Sparkling Bracer";
                case 0x00D3: return "Black Hood";
                case 0x00D4: return "Helm of Arai";
                case 0x00D5: return "Elven Mantle";
                case 0x00D6: return "Wonder Bangle";
                case 0x00D7: return "Ring of Protection";
                case 0x00D8: return "Aegis";
                case 0x00D9: return "Rat's Tail";
                case 0x00DA: return "Teddy Bear";
                case 0x00DB: return "Moogle Pocket";
                case 0x00DC: return "Chocobo Pocket";
                case 0x00DD: return "Gobbie Pocket";
                case 0x00DE: return "Ultimate Pocket";
                case 0x00DF: return "Ring of Fire";
                case 0x00E0: return "Ring of Blizzard";
                case 0x00E1: return "Ring of Thunder";
                case 0x00E2: return "Ring of Cure";
                case 0x00E3: return "Ring of Life";
                case 0x00E4: return "Earth Pendant";
                case 0x00E5: return "Moon Pendant";
                case 0x00E6: return "Star Pendant";
                case 0x00E7: return "Sun Pendant";
                case 0x00E8: return "Equipment 187 (Unused)";
                case 0x00E9: return "Equipment 188 (Unused)";
                case 0x00EA: return "Equipment 189 (Unused)";
                case 0x00EB: return "Equipment 190 (Unused)";
                case 0x00EC: return "Equipment 191 (Unused)";
                case 0x00ED: return "Equipment 192 (Unused)";
                case 0x00EE: return "Equipment 193 (Unused)";
                case 0x00EF: return "Equipment 194 (Unused)";
                case 0x00F0: return "Equipment 195 (Unused)";
                case 0x00F1: return "Equipment 196 (Unused)";
                case 0x00F2: return "Equipment 197 (Unused)";
                case 0x00F3: return "Equipment 198 (Unused)";
                case 0x00F4: return "Equipment 199 (Unused)";
                case 0x00F5: return "Equipment 200 (Unused)";
                case 0x00F6: return "Equipment 201 (Unused)";
                case 0x00F7: return "Equipment 202 (Unused)";
                case 0x00F8: return "Equipment 203 (Unused)";
                case 0x00F9: return "Equipment 204 (Unused)";
                case 0x00FA: return "Equipment 205 (Unused)";
                case 0x00FB: return "Equipment 206 (Unused)";
                case 0x00FC: return "Equipment 207 (Unused)";
                case 0x00FD: return "Equipment 208 (Unused)";
                case 0x00FE: return "Equipment 209 (Unused)";
                case 0x00FF: return "Equipment 210 (Unused)";
                case 0x0100: return "Stone of Fire";
                case 0x0101: return "Stone of Blizzard";
                case 0x0102: return "Stone of Thunder";
                case 0x0103: return "Stone of ??? (Ulticite)";
                case 0x0104: return "Stone of Slow (Unused)";
                case 0x0105: return "Stone of Cure";
                case 0x0106: return "Stone of Clear";
                case 0x0107: return "Stone of Life";
                case 0x0108: return "Stone of Holy (Unused)";
                case 0x0109: return "Stone of Stop (Unused)";
                case 0x010A: return "Stone of Gravity (Unused)";
                case 0x010B: return "Curse (Unused)";
                case 0x010C: return "Illness (Unused)";
                case 0x010D: return "Magic Circle (Unused)";
                case 0x0010E: return "Ultima (Unused)";
                case 0x010F: return "Flare (Unused)";
                case 0x0110: return "Meteor (Unused)";
                case 0x0111: return "Darkness (Unused)";
                case 0x0112: return "Zeal (Unused)";
                case 0x0113: return "Seize (Unused)";
                case 0x00114: return "Confuse (Unused)";
                case 0x0115: return "Whirlwind (Unused)";
                case 0x0116: return "Examine (Unused)";
                case 0x0117: return "Threaten (Unused)";
                case 0x0118: return "Favorites (Unused)";
                case 0x0119: return "Dice (Unused)";
                case 0x011A: return "Blame (Unused)";
                case 0x011B: return "Rule (Unused)";
                case 0x011C: return "All (Unused)";
                case 0x011D: return "Fan (Unused)";
                case 0x011E: return "Circle (Unused)";
                case 0x011F: return "Trail (Unused)";
                case 0x0120: return "Enhance (Unused)";
                case 0x0121: return "Double (Unused)";
                case 0x0122: return "Add (Unused)";
                case 0x00123: return "Reflect (Unused)";
                case 0x0124: return "Barrier (Unused)";
                case 0x0125: return "Phoenix Down";
                case 0x0126: return "Bronze";
                case 0x0127: return "Iron";
                case 0x0128: return "Mythril";
                case 0x0129: return "Orichalcum";
                case 0x012A: return "Diamond Ore";
                case 0x012B: return "Gold";
                case 0x012C: return "Silver";
                case 0x012D: return "Bronze Shard";
                case 0x012E: return "Iron Shard";
                case 0x012F: return "Tiny Crystal";
                case 0x0130: return "Crystal Ball";
                case 0x0131: return "Ruby";
                case 0x0132: return "Jade";
                case 0x0133: return "Alloy";
                case 0x0134: return "Magma Rock";
                case 0x0135: return "Chilly Gel";
                case 0x0136: return "Thunderball";
                case 0x0137: return "Holy Water";
                case 0x0138: return "Heavenly Dust";
                case 0x0139: return "Yellow Feather";
                case 0x013A: return "Blue Silk";
                case 0x013B: return "White Silk";
                case 0x013C: return "Fiend's Claw";
                case 0x013D: return "Devil's Claw";
                case 0x013E: return "Faerie's Tear";
                case 0x013F: return "Angel's Tear";
                case 0x0140: return "Ancient Sword";
                case 0x0141: return "Cursed Crook";
                case 0x0142: return "Orc Belt";
                case 0x0143: return "King's Scale";
                case 0x0144: return "Green Sphere";
                case 0x0145: return "Dragon's Fang";
                case 0x0146: return "Malboro Seed";
                case 0x0147: return "Desert Fang";
                case 0x0148: return "Wind Crystal";
                case 0x0149: return "Ethereal Orb";
                case 0x014A: return "Red Eye";
                case 0x014B: return "Dweomer Spore";
                case 0x014C: return "Lord's Robe";
                case 0x014D: return "Griffin's Wing";
                case 0x014E: return "Cerberus Fang";
                case 0x014F: return "Needle";
                case 0x0150: return "Hard Shell";
                case 0x0151: return "Worm Antenna";
                case 0x0152: return "Toad Oil";
                case 0x0153: return "Jagged Scythe";
                case 0x0154: return "Ogre Fang";
                case 0x0155: return "Chimera's Horn";
                case 0x0156: return "Crop Seed (Unused)";
                case 0x0157: return "Coeurl Whisker";
                case 0x0158: return "Zu's Beak";
                case 0x0159: return "Cockatrice Scale";
                case 0x015A: return "Ancient Potion";
                case 0x015B: return "Shiny Shard";
                case 0x015C: return "Gigas Claw";
                case 0x015D: return "Gear";
                case 0x015E: return "Pressed Flower";
                case 0x015F: return "Remedy";
                case 0x0160: return "Goddess Statuette (Unused)";
                case 0x0161: return "Devil's Mask (Unused)";
                case 0x0162: return "Extra 22 (Unused)";
                case 0x0163: return "Flower Seed";
                case 0x0164: return "Strange Seed";
                case 0x0165: return "Fruit Seed (1)";
                case 0x0166: return "Fruit Seed (2)";
                case 0x0167: return "Fruit Seed (3)";
                case 0x0168: return "Vegetable Seed (1)";
                case 0x0169: return "Vegetable Seed (2)";
                case 0x016A: return "Vegetable Seed (3)";
                case 0x016B: return "Wheat Seed";
                case 0x016C: return "Worn Bandanna";
                case 0x016D: return "Shella Mark";
                case 0x016E: return "Kilanda Sulfur";
                case 0x016F: return "Cactus Flower";
                case 0x0170: return "Extra 23 (Unused)";
                case 0x0171: return "Ultimite";
                case 0x0172: return "Dark Sphere";
                case 0x0173: return "Extra 24 (Unused)";
                case 0x0174: return "Extra 25 (Unused)";
                case 0x0175: return "Extra 26 (Unused)";
                case 0x0176: return "Extra 27 (Unused)";
                case 0x0177: return "Extra 28 (Unused)";
                case 0x0178: return "Extra 29 (Unused)";
                case 0x0179: return "Extra 30 (Unused)";
                case 0x017A: return "Extra 31 (Unused)";
                case 0x017B: return "Extra 32 (Unused)";
                case 0x017C: return "Extra 33 (Unused)";
                case 0x017D: return "Striped Apple";
                case 0x017E: return "Cherry Cluster";
                case 0x017F: return "Rainbow Grapes";
                case 0x0180: return "Star Carrot";
                case 0x0181: return "Gourd Potato";
                case 0x0182: return "Round Corn";
                case 0x0183: return "Meat";
                case 0x0184: return "Fish";
                case 0x0185: return "Bannock";
                case 0x0186: return "Spring Water";
                case 0x0187: return "Milk";
                case 0x0188: return "Strange Liquid";
                case 0x0189: return "Flame Shard (Unused)";
                case 0x018A: return "Water Shard (Unused)";
                case 0x018B: return "Lightning Shard (Unused)";
                case 0x018C: return "Darkness Shard (Unused)";
                case 0x018D: return "Wheat";
                case 0x018E: return "Flour";
                case 0x018F: return "??? (Unused)";
                case 0x0190: return "Item Gil (Unused)";
                case 0x0191: return "Novice's Weapon";
                case 0x0192: return "Warrior's Weapon";
                case 0x0193: return "Valiant Weapon";
                case 0x0194: return "Mighty Weapon";
                case 0x0195: return "Victorious Weapon";
                case 0x0196: return "Master's Weapon";
                case 0x0197: return "Legendary Weapon";
                case 0x0198: return "Hero's Weapon";
                case 0x0199: return "Celestial Weapon";
                case 0x019A: return "Dark Weapon";
                case 0x019B: return "Lunar Weapon";
                case 0x019C: return "Bronze Armor";
                case 0x019D: return "Iron Armor";
                case 0x019E: return "Mythril Armor";
                case 0x019F: return "Flame Armor";
                case 0x01A0: return "Frost Armor";
                case 0x01A1: return "Lightning Armor";
                case 0x01A2: return "Time Armor";
                case 0x01A3: return "Eternal Armor";
                case 0x01A4: return "Pure Armor";
                case 0x01A5: return "Holy Armor";
                case 0x01A6: return "Gold Armor";
                case 0x01A7: return "Radiant Armor";
                case 0x01A8: return "Diamond Armor";
                case 0x01A9: return "Earth Armor";
                case 0x01AA: return "Iron Shield";
                case 0x01AB: return "Mythril Shield";
                case 0x01AC: return "Flame Shield";
                case 0x01AD: return "Frost Shield";
                case 0x01AE: return "Lightning Shield";
                case 0x01AF: return "Holy Shield";
                case 0x01B0: return "Diamond Shield";
                case 0x01B1: return "Magic Shield";
                case 0x01B2: return "Legendary Shield";
                case 0x01B3: return "Bronze Gloves";
                case 0x01B4: return "Iron Gloves";
                case 0x01B5: return "Mythril Gloves";
                case 0x01B6: return "Flame Gloves";
                case 0x01B7: return "Frost Gloves";
                case 0x01B8: return "Lightning Gloves";
                case 0x01B9: return "Gold Gloves";
                case 0x01BA: return "Diamond Gloves";
                case 0x01BB: return "Bronze Sallet";
                case 0x01BC: return "Iron Sallet";
                case 0x01BD: return "Mythril Sallet";
                case 0x01BE: return "Flame Sallet";
                case 0x01BF: return "Frost Sallet";
                case 0x01C0: return "Lightning Sallet";
                case 0x01C1: return "Time Sallet";
                case 0x01C2: return "Eternal Sallet";
                case 0x01C3: return "Diamond Sallet";
                case 0x01C4: return "Bronze Belt";
                case 0x01C5: return "Iron Belt";
                case 0x01C6: return "Mythril Belt";
                case 0x01C7: return "Flame Belt";
                case 0x01C8: return "Frost Belt";
                case 0x01C9: return "Lightning Belt";
                case 0x01CA: return "Pure Belt";
                case 0x01CB: return "Wind Belt";
                case 0x01CC: return "Diamond Belt";
                case 0x01CD: return "Flame Craft";
                case 0x01CE: return "Frost Craft";
                case 0x01CF: return "Lightning Craft";
                case 0x01D0: return "Clockwork";
                case 0x01D1: return "New Clockwork";
                case 0x01D2: return "Blue Yarn";
                case 0x01D3: return "White Yarn";
                case 0x01D4: return "Gold Craft";
                case 0x01D5: return "Tome of Wisdom";
                case 0x01D6: return "Secrets of Wisdom";
                case 0x01D7: return "Lady's Accessories";
                case 0x01D8: return "Tome of Speed";
                case 0x01D9: return "Secrets of Speed";
                case 0x01DA: return "Book of Brigandology";
                case 0x01DB: return "Zeal Kit";
                case 0x01DC: return "Fiend Kit";
                case 0x01DD: return "Daemon Kit";
                case 0x01DE: return "Faerie Kit";
                case 0x01DF: return "Angel Kit";
                case 0x01E0: return "Ring of Light";
                case 0x01E1: return "Eyewear Techniques";
                case 0x01E2: return "Designer Glasses";
                case 0x01E3: return "Healing Kit";
                case 0x01E4: return "Fashion Kit";
                case 0x01E5: return "Goggle Techniques";
                case 0x01E6: return "Designer Goggles";
                case 0x01E7: return "Soul of the Lion";
                case 0x01E8: return "Soul of the Dragon";
                case 0x01E9: return "Tome of Magic";
                case 0x01EA: return "Tome of Sorcery";
                case 0x01EB: return "Forbidden Tome";
                case 0x01EC: return "Greatest Weapon";
                case 0x01ED: return "Ring of Invincibility";
                case 0x01EE: return "design 94 (Unused)";
                case 0x01EF: return "design 95 (Unused)";
                case 0x01F0: return "design 96 (Unused)";
                case 0x01F1: return "design 97 (Unused)";
                case 0x01F2: return "design 98 (Unused)";
                case 0x01F3: return "design 99 (Unused)";
                case 0x01F4: return "design 100 (Unused)";
                case 0x01F5: return "Destroy";
                case 0x01F6: return "KO";
                case 0x01F7: return "???";
                case 0x01F8: return "Power Slash";
                case 0x01F9: return "Piercing Sweep";
                case 0x01FA: return "Bash";
                case 0x01FB: return "Soulshot";
                case 0x01FC: return "Shadowblade";
                case 0x01FD: return "Cyclone Slash";
                case 0x01FE: return "Psi Blast";
                case 0x01FF: return "Avalanche";
                case 0x0200: return "Pulse Thrust";
                case 0x0201: return "Cross Slash";
                case 0x0202: return "Bladestorm";
                case 0x0203: return "Power Bomb";
                case 0x0204: return "Wave Bomb";
                case 0x0205: return "Shock Bomb";
                case 0x0206: return "Magic Bomb";
                case 0x0207: return "Fire";
                case 0x0208: return "Fira";
                case 0x0209: return "Fira +1";
                case 0x020A: return "Fira +2";
                case 0x020B: return "Blizzard";
                case 0x020C: return "Blizzara";
                case 0x020D: return "Blizzara +1";
                case 0x020E: return "Blizzara +2";
                case 0x020F: return "Thunder";
                case 0x0210: return "Thundara";
                case 0x0211: return "Thundara +1";
                case 0x0212: return "Thundara +2";
                case 0x0213: return "Haste? (Defunct)";
                case 0x0214: return "Slow";
                case 0x0215: return "Firaga +1";
                case 0x0216: return "Firaga +2";
                case 0x0217: return "Slow..?";
                case 0x0218: return "Cure";
                case 0x0219: return "Blizzaga +1";
                case 0x021A: return "Blizzaga +2";
                case 0x021B: return "Cure..?";
                case 0x021C: return "Clear";
                case 0x021D: return "Thundaga +1";
                case 0x021E: return "Thundaga +2";
                case 0x021F: return "Clear?";
                case 0x0220: return "Life";
                case 0x0221: return "Holy";
                case 0x0222: return "Holyra";
                case 0x0223: return "Holyra +1";
                case 0x0224: return "Holyra +2";
                case 0x0225: return "Full-Life";
                case 0x0226: return "Gravity";
                case 0x0227: return "Gravira";
                case 0x0228: return "Gravira";
                case 0x0229: return "Gravira";
                case 0x022A: return "Haste";
                case 0x022B: return "Haste? (Defunct)";
                case 0x022C: return "Haste? (Defunct)";
                case 0x022D: return "Hastega";
                case 0x022E: return "Aura Blast";
                case 0x022F: return "Stampede";
                case 0x0230: return "Firaga";
                case 0x0231: return "Blizzaga";
                case 0x0232: return "Thundaga";
                case 0x0233: return "Graviga";
                case 0x0234: return "Slowga";
                case 0x0235: return "Hastega";
                case 0x0236: return "Dual Blast";
                case 0x0237: return "Power Kick";
                case 0x0238: return "Curaga";
                case 0x0239: return "Clearaga";
                case 0x023A: return "Pick Up";
                case 0x023B: return "Teleport";
                case 0x023C: return "Memories";
                case 0x023D: return "Meteor Blast";
                case 0x023E: return "Stop";
                case 0x023F: return "Stop? (defunct)";
                case 0x0240: return "Stop?";
                case 0x0241: return "Stop?";
                case 0x0242: return "Flamestrike";
                case 0x0243: return "Icestrike";
                case 0x0244: return "Thunderstrike";
                case 0x0245: return "Spell: Piercing Sweep";
                case 0x0274: return "Spell: Bio/Bioga (Self-Damage)";
                case 0x02A5: return "Spell: Curse (Self-Curse)";
                case 0x02A7: return "Spell: Curse (Self-Curse)";
                case 0x02B0: return "Spell: Curse (Self-Curse)";
                case 0x02CE: return "Spell: Curse (Self-Curse)";
                case 0x03AB: return "Giant Crab Large Claw Slam";
                case 0x03AC: return "Giant Crab Small Claw Swipe";
                case 0x03AD: return "Giant Crab Thundara";
                case 0x03AE: return "Giant Crab Thunder Cannon";
                case 0x03AF: return "Giant Crab Slow Bubble";
                case 0x03B2: return "Giant Crab Thundaga";
                case 0x03B7: return "Spell: Curse (Self-Curse)";
                case 0x03E0: return "Spell: Bio/Bioga (Self-Damage)";
                case 0x0491: return "Spell: Burning Oil";
                case 0x0492: return "Spell: Frozen Water (Self-Freeze)";
                case 0x0493: return "Spell: Electrified Water Self-Shock)";
                case 0x04AD: return "Spell: Bio/Bioga (Self-Damage)";
                case 0x04AE: return "Spell: Curse (Self-Curse)";
                case 0x04B2: return "Spell: Bio/Bioga (Self-Damage)";
                // 682 - self medium fire
                // 683 - self medium thunder
                // 694 - self basic fire (also self dmg)
                // 695 - self basic ice (also self dmg)
                // 696 - self basic thunder (also self dmg)
                // 697 - gravity (does nothing but create a sphere)
                // !! 698 - Strange memories effect
                // 705 - self fire
                // 706 - self ice
                // 707 - self thunder
                // 713 - self ice
                // 714 - self medium ice
                // 725 - self slow
                // 728 - self slow
                // 730 - self thunder
                // 735 - self slow
                // 755 - self fire
                // 756 - self medium ice
                // 757 - self thunder
                // 758 - self slow
                // 775 - self fire
                // 776 - self ice
                // 777 - self thunder
                // 799 - self fire
                // 800 - self ice
                // 801 - self thunder
                // 802 - self medium thunder
                // 815 - self slow
                // 818 - self slow
                // 821 - self ice
                // 825 - self ice
                // 828 - self ice
                // 832 - self medium ice
                // 864 - self medium slow
                // 876 - self medium slow
                // 887 - self ice
                // 888 - self thunder
                // 889 - self fire
                // 890 - self slow
                // !! 913 - self fire + haste
                // 914 - self ice
                // 915 - self thunder
                // 919 - self fire medium
                // 923 - self ice medium
                // 927 - self thunder medium
                // 931 - self slowga
                // 952 - self slowga
                // 954 - self blizzaga
                // 961 - self ice
                // 962 - self thunder
                // !! 963 - self fire + haste
                // 964 - self slowga
                // 991 - self slowga
                // 992 - self poison
                // !! 993 - self fire medium + haste
                // 994 - self thunder medium
                // 1016 - Stop
                // !! 1020 - self fire + haste
                // 1021 - self thunder medium
                // 1036 - self slowga
                // 1037 - self stop
                // !! 1056 - self fire + haste
                // 1057 - self ice
                // 1058 - self thunder
                // 1059 - gravity sphere
                // !! 1059 - self raem
                // !! 1064 - self fire + haste
                // 1065 - self ice
                // 1066 - self thunder
                // 1067 - self thunder (instant death, infinite range)
                // 1068 - self thunder
                // !! 1088 - self fire + haste (weak)
                // 1089 - self ice
                // 1090 - self thunder
                // 1091 - self slowga
                // 1135 - self slowga
                case 0xFFFF: return "<None>";
            }

            if (itemId >= 0x01F5 && itemId <= 0x7FFF)
            {
                return "<Invalid> (Lag/Crash)";
            }
            else
            {
                return "<Invalid>";
            }
        }
    }
    //// End class
}
//// End namespace