namespace GES.View.Editors
{
    using GES.Source.Editors.ApplyArtifactsEditor;
    using GES.Source.InventoryViewer;
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Interaction logic for ApplyArtifactsEditor.xaml.
    /// </summary>
    public partial class ApplyArtifactsEditor : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplyArtifactsEditor" /> class.
        /// </summary>
        public ApplyArtifactsEditor()
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

        private void EquipCommandListSlot1Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().EquipCommandListSlot(0);

            this.DialogResult = true;
        }

        private void EquipCommandListSlot2Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().EquipCommandListSlot(1);

            this.DialogResult = true;
        }

        private void EquipCommandListSlot3Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().EquipCommandListSlot(2);

            this.DialogResult = true;
        }

        private void EquipCommandListSlot4Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().EquipCommandListSlot(3);

            this.DialogResult = true;
        }

        private void EquipCommandListSlot5Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().EquipCommandListSlot(4);

            this.DialogResult = true;
        }

        private void EquipCommandListSlot6Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().EquipCommandListSlot(5);

            this.DialogResult = true;
        }

        private void EquipArtifactSet1Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().ApplyArtifactSet(0);

            this.DialogResult = true;
        }

        private void EquipArtifactSet2Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().ApplyArtifactSet(1);

            this.DialogResult = true;
        }

        private void EquipArtifactSet3Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().ApplyArtifactSet(2);

            this.DialogResult = true;
        }

        private void EquipArtifactSet4Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().ApplyArtifactSet(3);

            this.DialogResult = true;
        }

        private void EquipArtifactSet5Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyArtifactsEditorViewModel.GetInstance().ApplyArtifactSet(4);

            this.DialogResult = true;
        }
    }
    //// End class
}
//// End namespace