namespace GES
{
    using System;
    
    /// <summary>
    /// A static class for interfacing with GES non-engine settings.
    /// </summary>
    public static class GESSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether GES should check for automatic updates.
        /// </summary>
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
    }
    //// End class
}
//// End namespace