namespace GES.Source.Mvvm.Converters
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
                if (paramString.Equals("Clavat", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsClavat)
                    {
                        if (equipmentEntry.IsFemaleOnly)
                        {
                            return Images.Icon_ClavatF;
                        }

                        return Images.Icon_ClavatM;
                    }
                }
                else if (paramString.Equals("Lilty", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsLilty)
                    {
                        if (equipmentEntry.IsFemaleOnly)
                        {
                            return Images.Icon_LiltyF;
                        }

                        return Images.Icon_LiltyM;
                    }
                }
                else if (paramString.Equals("Yuke", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsYuke)
                    {
                        if (equipmentEntry.IsFemaleOnly)
                        {
                            return Images.Icon_YukeF;
                        }

                        return Images.Icon_YukeM;
                    }
                }
                else if (paramString.Equals("Selkie", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsSelkie)
                    {
                        if (equipmentEntry.IsFemaleOnly)
                        {
                            return Images.Icon_SelkieF;
                        }

                        return Images.Icon_SelkieM;
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