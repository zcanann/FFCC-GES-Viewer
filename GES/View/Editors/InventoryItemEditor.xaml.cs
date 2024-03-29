﻿namespace GES.View.Editors
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for InventoryItemEditor.xaml.
    /// </summary>
    public partial class InventoryItemEditor : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryItemEditor" /> class.
        /// </summary>
        public InventoryItemEditor()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the modal has been attempted to be closed.
        /// </summary>
        private Boolean HasClosed { get; set; }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            this.HasClosed = true;
        }

        protected override void OnDeactivated(EventArgs eventArgs)
        {
            base.OnDeactivated(eventArgs);

            if (!this.HasClosed)
            {
                this.HasClosed = true;
                this.Close();
            }
        }

         private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Automatically calls OnDeactivated(), closing the window
            this.DialogResult = false;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            // Automatically calls OnDeactivated(), closing the window
            this.DialogResult = true;
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Automatically calls OnDeactivated(), closing the window
            this.DialogResult = true;
        }
    }
    //// End class
}
//// End namespace