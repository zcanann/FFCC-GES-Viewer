namespace GES.Source.Mvvm.Converters
{
    using GES.Content;
    using GES.Source.EquipmentListViewer;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public class EquipmentPropertiesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EquipmentEntry equipmentEntry = value as EquipmentEntry;
            string paramString = parameter as string;

            if (equipmentEntry != null && paramString != null)
            {
                if (paramString.Equals("Clavat_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsClavatM)
                    {
                        return Images.Icon_ClavatM;
                    }
                }
                else if (paramString.Equals("Clavat_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsClavatF)
                    {
                        return Images.Icon_ClavatF;
                    }
                }
                else if (paramString.Equals("Lilty_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsLiltyM)
                    {
                        return Images.Icon_LiltyM;
                    }
                }
                else if (paramString.Equals("Lilty_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsLiltyF)
                    {
                        return Images.Icon_LiltyF;
                    }
                }
                else if (paramString.Equals("Yuke_M", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsYukeM)
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
                    if (equipmentEntry.IsSelkieF)
                    {
                        return Images.Icon_SelkieM;
                    }
                }
                else if (paramString.Equals("Selkie_F", StringComparison.OrdinalIgnoreCase))
                {
                    if (equipmentEntry.IsSelkieF)
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //// End class
}
//// End namespace