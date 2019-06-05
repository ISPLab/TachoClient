using System;
using TachoData;
using TachoServiceClient.ViewModels;
using Xamarin.Forms;

namespace TachoServiceClient.Views
{
	public class TachoLegislationParentView : ContentView
	{
		public TachoLegislationParentView()
		{
			Label legheader = new Label { Text = "Законодательство", Style = (Style)Application.Current.Resources["H2Header"], FontSize=16 };
			Picker picker = new Picker();
			Content = new StackLayout
			{
				Children = { legheader, picker, new TachoLegislationView() { BindingContext = picker.SelectedItem } }
			};
			this.BindingContextChanged += (sender, e) =>
			{
				picker.ItemsSource = (this.BindingContext as TachoControllerViewModel).Legislations;
				picker.SelectedItem = picker.Items[0];
				picker.SelectedIndex = 0;
                picker.FontSize = 14;
				Content = new StackLayout
				{
					Children = { legheader, picker, new TachoLegislationView() { BindingContext = picker.SelectedItem } }
				};
			};
			picker.SelectedIndexChanged += (sender, e) =>
			{
				picker.ItemsSource = (this.BindingContext as TachoControllerViewModel).Legislations;
				(this.BindingContext as TachoControllerViewModel).CurrentLegislation = (TachoLegislation)picker.SelectedItem;
				picker.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
				Content = new StackLayout
				{
					Children = { legheader, picker, new TachoLegislationView() { BindingContext = picker.SelectedItem } }
				};
			};
		}
	}
}



