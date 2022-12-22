namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Converts ProjectItems to an icon format readily usable by the view.
    /// </summary>
    public class ItemToIconConverter : IValueConverter
    {
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

            if (value is UInt16)
            {
                UInt16 itemId  = (UInt16) value;

                if (itemId == 0)
                {
                    return null;
                }
                else if (itemId >= 1 && itemId <= 0x11)
                {
                    return Images.Icon_WeaponClavat;
                }
                else if (itemId >= 0x12 && itemId <= 0x23)
                {
                    return Images.Icon_WeaponLilty;
                }
                else if (itemId >= 0x24 && itemId <= 0x33)
                {
                    return Images.Icon_WeaponYuke;
                }
                else if (itemId >= 0x34 && itemId <= 0x44)
                {
                    return Images.Icon_WeaponSelkie;
                }
                else if (itemId >= 0x45 && itemId <= 0x57)
                {
                    return Images.Icon_Chest;
                }
                else if (itemId >= 0x58 && itemId <= 0x61)
                {
                    return Images.Icon_TribalClavat;
                }
                else if (itemId >= 0x62 && itemId <= 0x6A)
                {
                    return Images.Icon_TribalLilty;
                }
                else if (itemId >= 0x6B && itemId <= 0x74)
                {
                    return Images.Icon_TribalYuke;
                }
                else if (itemId >= 0x75 && itemId <= 0x7E)
                {
                    return Images.Icon_TribalSelkie;
                }
                else if (itemId >= 0x7F && itemId <= 0x9E)
                {
                    return Images.Icon_Accessory;
                }
                else if (itemId >= 0x9F && itemId <= 0xE7)
                {
                    return Images.Icon_Artifact;
                }
                else if (itemId >= 0xE8 && itemId <= 0xFF)
                {
                    return Images.Icon_Recipe;
                }
                else if (itemId >= 0xE8 && itemId <= 0xFF)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId >= 0x0100 && itemId <= 0x0103)
                {
                    return Images.Icon_MagiciteAttack;
                }
                else if (itemId >= 0x0104 && itemId <= 0x0104)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId >= 0x0105 && itemId <= 0x0106)
                {
                    return Images.Icon_MagiciteBuff;
                }
                else if (itemId >= 0x0107 && itemId <= 0x0124)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId == 0x0125)
                {
                    return Images.Icon_PhoenixDown;
                }
                else if (itemId >= 0x0126 && itemId <= 0x0129)
                {
                    return Images.Icon_Metal;
                }
                else if (itemId >= 0x012A && itemId <= 0x0162)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId >= 0x0163 && itemId <= 0x016B)
                {
                    return Images.Icon_Seed;
                }
                else if (itemId >= 0x016C && itemId <= 0x0170)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId >= 0x0171 && itemId <= 0x017C)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId == 0x017D)
                {
                    return Images.Icon_Apple;
                }
                else if (itemId == 0x017E)
                {
                    return Images.Icon_Cherries;
                }
                else if (itemId == 0x017F)
                {
                    return Images.Icon_Berries;
                }
                else if (itemId == 0x0180)
                {
                    return Images.Icon_Carrot;
                }
                else if (itemId == 0x0181)
                {
                    return Images.Icon_Potato;
                }
                else if (itemId == 0x0182)
                {
                    return Images.Icon_Corn;
                }
                else if (itemId == 0x0183)
                {
                    return Images.Icon_Meat;
                }
                else if (itemId == 0x0184)
                {
                    return Images.Icon_Fish;
                }
                else if (itemId == 0x0185)
                {
                    return Images.Icon_Bread;
                }
                else if (itemId >= 0x0186 && itemId <= 0x0188)
                {
                    return Images.Icon_Barrel;
                }
                else if (itemId >= 0x0189 && itemId <= 0x018C)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId == 0x018D)
                {
                    return Images.Icon_Wheat;
                }
                else if (itemId == 0x018E)
                {
                    return Images.Icon_Flour;
                }
                else if (itemId >= 0x018F && itemId <= 0x0190)
                {
                    return Images.Icon_Pouch;
                }
                else if (itemId >= 0x0191 && itemId <= 0x01F4)
                {
                    return Images.Icon_Recipe;
                }
                else if (itemId >= 0x01F5 && itemId <= 0x7FFF)
                {
                    return Images.CollectValues;
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