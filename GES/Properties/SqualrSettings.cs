namespace GES
{
    using System;
    
    public static class GESSettings
    {
        public static Boolean AutomaticUpdates
        {
            get
            {
                return Properties.Settings.Default.AutomaticUpdates;
            }

            set
            {
                Properties.Settings.Default.AutomaticUpdates = value;
            }
        }

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