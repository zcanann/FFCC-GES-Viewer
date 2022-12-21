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

        public static String SelectedVersion
        {
            get
            {
                return Properties.Settings.Default.SelectedVersion;
            }

            set
            {
                Properties.Settings.Default.SelectedVersion = value;
            }
        }
    }
    //// End class
}
//// End namespace