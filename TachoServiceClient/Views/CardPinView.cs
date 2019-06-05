using System;

using Xamarin.Forms;

namespace TachoServiceClient.Views
{
    public class CardPinView : ContentView
    {
        public CardPinView()
        {
            this.BindingContextChanged += (s,ev) => { 
            StackLayout stack = new StackLayout();
            Label label = new Label { Text = "Если установлен на карте PIN",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
                stack.Children.Add(label);
            label = new Label { Text = "Введите PIN:",
               FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
                stack.Children.Add(label);
            Entry entry = new Entry
            {
                Keyboard= Keyboard.Numeric,
                IsPassword = true,
            };
            entry.BindingContext = this.BindingContext;
            entry.SetBinding(Entry.TextProperty, "Pin");
            stack.Children.Add(entry);
                Content = stack;
            };
        }
    }
}

