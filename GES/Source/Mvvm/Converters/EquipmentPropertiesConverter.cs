﻿namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.EquipmentViewer;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class EquipmentPropertiesConverter : IValueConverter
    {
        public Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            EquipmentEntry equipmentEntry = value as EquipmentEntry;
            String paramString = parameter as String;

            if (equipmentEntry != null && paramString != null)
            {
                if (paramString.Equals("Clavat_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsClavat && !equipmentEntry.IsFemaleOnly)
                    {
                        return Images.Icon_ClavatM;
                    }
                }
                else if (paramString.Equals("Clavat_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsClavat && !equipmentEntry.IsMaleOnly)
                    {
                        return Images.Icon_ClavatF;
                    }
                }
                else if (paramString.Equals("Lilty_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsLilty && !equipmentEntry.IsFemaleOnly)
                    {
                        return Images.Icon_LiltyM;
                    }
                }
                else if (paramString.Equals("Lilty_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsLilty && !equipmentEntry.IsMaleOnly)
                    {
                        return Images.Icon_LiltyF;
                    }
                }
                else if (paramString.Equals("Yuke_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsYuke && !equipmentEntry.IsFemaleOnly)
                    {
                        return Images.Icon_YukeM;
                    }
                }
                else if (paramString.Equals("Yuke_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsYuke && !equipmentEntry.IsMaleOnly)
                    {
                        return Images.Icon_YukeF;
                    }
                }
                else if (paramString.Equals("Selkie_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsSelkie && !equipmentEntry.IsFemaleOnly)
                    {
                        return Images.Icon_SelkieM;
                    }
                }
                else if (paramString.Equals("Selkie_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsSelkie && !equipmentEntry.IsMaleOnly)
                    {
                        return Images.Icon_SelkieF;
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
        public Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace