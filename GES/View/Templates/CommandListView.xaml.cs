namespace GES.View.Templates
{
	using GES.Source.InventoryViewer;
	using GES.Source.Mvvm.Converters;
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;

	public partial class CommandListView : UserControl
	{
		public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(Int32), typeof(CommandListView));

		private static PlayerIndexToSlotConverter PlayerIndexToSlotConverter = new PlayerIndexToSlotConverter();

		public CommandListView()
		{
			InitializeComponent();

            Loaded += (sender, args) =>
            {
                this.RefreshText();
            };

            InventoryViewerViewModel.GetInstance().PropertyChanged += this.CommandListView_PropertyChanged;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void CommandListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(InventoryViewerViewModel.DisplayPlayerToSlotMap))
			{
				this.RefreshText();
			}
        }

        public Int32 Index
		{
			get { return (Int32)GetValue(IndexProperty); }
			set { SetValue(IndexProperty, value); this.RefreshText(); }
		}

		private void RefreshText()
		{
			TextBlock myTextBlock = (TextBlock)this.FindName("SlotText");

			if (myTextBlock != null)
			{
				myTextBlock.Text = PlayerIndexToSlotConverter.Convert(this.Index, null, null, null)?.ToString();
			}
		}
	}
}
