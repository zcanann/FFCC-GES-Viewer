namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.EquipmentListViewer;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class EquipmentSlotIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EquipmentEntry equipmentEntry = value as EquipmentEntry;

            string paramString = parameter as string;

            if (equipmentEntry != null && paramString != null)
            {
                if (paramString.Equals("Weapon", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsWeapon)
                    {
                        /*
                        if (equipmentEntry.IsClavat)
                        {
                            return Images.Icon_WeaponClavat;
                        }
                        else if (equipmentEntry.IsLilty)
                        {
                            return Images.Icon_WeaponLilty;
                        }
                        else if (equipmentEntry.IsYuke)
                        {
                            return Images.Icon_WeaponYuke;
                        }
                        else if (equipmentEntry.IsSelkie)
                        {
                            return Images.Icon_WeaponSelkie;
                        }*/

                        // Default
                        return Images.Icon_WeaponClavat;
                    }
                }
                else if (paramString.Equals("Chest", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsChest)
                    {
                        return Images.Icon_Chest;
                    }
                }
                else if (paramString.Equals("Tribal", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsTribal)
                    {
                        /*
                        if (equipmentEntry.IsClavat)
                        {
                            return Images.Icon_TribalClavat;
                        }
                        else if (equipmentEntry.IsLilty)
                        {
                            return Images.Icon_TribalLilty;
                        }
                        else if (equipmentEntry.IsYuke)
                        {
                            return Images.Icon_TribalYuke;
                        }
                        else if (equipmentEntry.IsSelkie)
                        {
                            return Images.Icon_TribalSelkie;
                        }*/

                        // Default
                        return Images.Icon_TribalClavat;
                    }
                }
                else if (paramString.Equals("Accessory", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsAccessory)
                    {
                        return Images.Icon_Accessory;
                    }
                }
            }

            if (value is Byte)
            {
                Byte slotValue = (Byte) value;

                if (slotValue != 255)
                {
                    if ((slotValue & 1) != 0)
                    {
                        return Images.Icon_WeaponClavat;
                    }
                    else if ((slotValue & 4) != 0)
                    {
                        return Images.Icon_Chest;
                    }
                    else if ((slotValue & 2) != 0)
                    {
                        return Images.Icon_TribalClavat;
                    }
                    else if (slotValue == 16)
                    {
                        return Images.Icon_Accessory;
                    }
                    else if ((slotValue & 8) != 0)
                    {
                        return Images.Icon_TribalClavat;
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