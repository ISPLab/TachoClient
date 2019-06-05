using System;
using TachoServiceClient.ViewModels;
using Xamarin.Forms;

namespace TachoServiceClient.Views
{
    public class TachoLegislationView : ContentView
    {
        public TachoLegislationView()
        {
            this.BindingContextChanged += (s, ev) => {

                ListView listView = new ListView();
                listView.BindingContext = this.BindingContext;
                listView.SetBinding(ListView.ItemsSourceProperty, "Parms");
                DataTemplate template = new DataTemplate(() => {
                ViewCell cell = new ViewCell();
                StackLayout stack = new StackLayout { Orientation = StackOrientation.Horizontal };
                var sw = new Switch();
                sw.SetBinding(Switch.IsToggledProperty, "Checked");
                stack.Children.Add(sw);
                Label label = new Label();
                label.SetBinding(Label.TextProperty, "Description");
                stack.Children.Add(label);
                cell.View = stack;
                    return cell;
                });
                listView.ItemTemplate = template;
                this.Content = listView;
            };
        }
    }
}

