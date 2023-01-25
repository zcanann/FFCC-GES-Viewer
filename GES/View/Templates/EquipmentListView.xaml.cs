namespace GES.View.Templates
{
	using GES.Source.InventoryViewer;
	using GES.Source.Mvvm.Converters;
	using System;
	using System.ComponentModel;
	using System.Windows;
	using System.Windows.Controls;
	using static System.Windows.Forms.VisualStyles.VisualStyleElement;

	public partial class EquipmentListView : UserControl
	{
		public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(Int32), typeof(EquipmentListView));

        private static readonly PlayerIndexToSlotConverter PlayerIndexToSlotConverter = new PlayerIndexToSlotConverter();
        private static readonly PlayerIndexToPortConverter PlayerIndexToPortConverter = new PlayerIndexToPortConverter();

        public EquipmentListView()
		{
			InitializeComponent();

			Loaded += (sender, args) =>
			{
				this.RefreshText();
			};

			InventoryViewerViewModel.GetInstance().PropertyChanged += this.EquipmentListView_PropertyChanged;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void EquipmentListView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
            TextBlock slotText = (TextBlock)this.FindName("SlotText");

            if (slotText != null)
            {
                slotText.Text = PlayerIndexToSlotConverter.Convert(this.Index, null, null, null)?.ToString();
            }

            TextBlock portText = (TextBlock)this.FindName("PortText");

            if (portText != null)
            {
                portText.Text = PlayerIndexToPortConverter.Convert(this.Index, null, null, null)?.ToString();
            }
        }
	}
}
