namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.CraftViewer;
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
                if (paramString.Equals("Weapon", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsWeapon)
                    {
                        /*
                        if (CraftEntry.IsClavat)
                        {
                            return Images.Icon_WeaponClavat;
                        }
                        else if (CraftEntry.IsLilty)
                        {
                            return Images.Icon_WeaponLilty;
                        }
                        else if (CraftEntry.IsYuke)
                        {
                            return Images.Icon_WeaponYuke;
                        }
                        else if (CraftEntry.IsSelkie)
                        {
                            return Images.Icon_WeaponSelkie;
                        }*/

                        // Default
                        return Images.Icon_WeaponClavat;
                    }
                }
                else if (paramString.Equals("Chest", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsChest)
                    {
                        return Images.Icon_Chest;
                    }
                }
                else if (paramString.Equals("Tribal", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsTribal)
                    {
                        /*
                        if (CraftEntry.IsClavat)
                        {
                            return Images.Icon_TribalClavat;
                        }
                        else if (CraftEntry.IsLilty)
                        {
                            return Images.Icon_TribalLilty;
                        }
                        else if (CraftEntry.IsYuke)
                        {
                            return Images.Icon_TribalYuke;
                        }
                        else if (CraftEntry.IsSelkie)
                        {
                            return Images.Icon_TribalSelkie;
                        }*/

                        // Default
                        return Images.Icon_TribalClavat;
                    }
                }
                else if (paramString.Equals("Accessory", StringComparison.OrdinalIgnoreCase))
                {
                    if (CraftEntry.IsAccessory)
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