namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.CraftListViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class CraftSlotIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CraftEntry CraftEntry = value as CraftEntry;

            string paramString = parameter as string;

            if (CraftEntry != null && paramString != null)
            {
                if (paramString.Equals("Clavat", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsClavatWeapon)
                    {
                        return Images.Icon_WeaponClavat;
                    }
                    else if (CraftEntry.IsClavatChest)
                    {
                        return Images.Icon_Chest;
                    }
                    else if (CraftEntry.IsClavatTribal)
                    {
                        return Images.Icon_TribalClavat;
                    }
                    else if (CraftEntry.IsClavatAccessory)
                    {
                        return Images.Icon_Accessory;
                    }
                }
                else if (paramString.Equals("Lilty", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsLiltyWeapon)
                    {
                        return Images.Icon_WeaponClavat;
                    }
                    else if (CraftEntry.IsLiltyChest)
                    {
                        return Images.Icon_Chest;
                    }
                    else if (CraftEntry.IsLiltyTribal)
                    {
                        return Images.Icon_TribalClavat;
                    }
                    else if (CraftEntry.IsLiltyAccessory)
                    {
                        return Images.Icon_Accessory;
                    }
                }
                else if (paramString.Equals("Yuke", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsYukeWeapon)
                    {
                        return Images.Icon_WeaponClavat;
                    }
                    else if (CraftEntry.IsYukeChest)
                    {
                        return Images.Icon_Chest;
                    }
                    else if (CraftEntry.IsYukeTribal)
                    {
                        return Images.Icon_TribalClavat;
                    }
                    else if (CraftEntry.IsYukeAccessory)
                    {
                        return Images.Icon_Accessory;
                    }
                }
                else if (paramString.Equals("Selkie", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsSelkieWeapon)
                    {
                        return Images.Icon_WeaponClavat;
                    }
                    else if (CraftEntry.IsSelkieChest)
                    {
                        return Images.Icon_Chest;
                    }
                    else if (CraftEntry.IsSelkieTribal)
                    {
                        return Images.Icon_TribalClavat;
                    }
                    else if (CraftEntry.IsSelkieAccessory)
                    {
                        return Images.Icon_Accessory;
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace