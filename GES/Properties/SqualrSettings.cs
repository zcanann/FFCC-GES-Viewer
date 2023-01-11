namespace GES
{
    using System;
    
    public static class GESSettings
    {
        public static String SelectedLanguage
        {
            get
            {
                return Properties.Settings.Default.SelectedLanguage;
            }

            set
            {
                Properties.Settings.Default.SelectedLanguage = value;
            }
        }
    }
    //// End class
}
//// End namespace