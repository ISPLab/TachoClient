using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachoData;
using TachoHelper.Services.Serializing;
using TachoServiceClient.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TachoServiceClient.Views
{
   

    public class TachoProcessView : ContentView
    {
        ProgressBar prBar = new ProgressBar();
        public TachoProcessView()
        {
            StackLayout layout = new StackLayout();
            Label label = new Label {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = "Анализ карты" };
            layout.Children.Add(label);
            Editor preditor = new Editor
            {
                HeightRequest = 400,
                WidthRequest = 300,
                IsSpellCheckEnabled = false,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };
            layout.Children.Add(preditor);
            this.Content = layout;
            layout.Children.Add(prBar);
            Label ProgressLabel = new Label();
            layout.Children.Add(ProgressLabel);
            preditor.Focused += (sender, e) => { preditor.Unfocus(); };
            preditor.TextColor = Color.Black;
            BindingContextChanged += (sender, e) =>
            {
               (this.BindingContext as TachoControllerViewModel).tachocardservice.P8Event += async (s, ev) =>
                {
                    try
                    {
                        switch (ev.EventName)
                        {

                            case ("tachoinfo"):
                                preditor.Text += ev.Description + Environment.NewLine;
                                ProgressLabel.Text = ev.Description;
                                break;

                            case ("tachoprogress"):
                                // RFC1123 pattern  ddd, dd MMM yyyy HH':'mm':'ss 'UTC'
                                preditor.Text += ev.EventUtcTime.ToString("dd.MM.yyyy HH:mm:ss") + " : " + ev.Description + Environment.NewLine;
                                ProgressLabel.Text = ev.Description;
                                if (!String.IsNullOrEmpty(ev.Parametr.ToString()))
                                {
                                    double k = Convert.ToDouble((string)ev.Parametr);
                                    await prBar.ProgressTo(k, 300, Easing.CubicInOut);
                                }
                                break;

                            case ("tachoerror"):
                                (this.BindingContext as TachoControllerViewModel).PrbuttonText = "Ok";
                                preditor.Text += ev.Description + Environment.NewLine;
                                ProgressLabel.Text = ev.Description;
                                await prBar.ProgressTo(0, 100, Easing.CubicInOut);
                                break;
                                /* obsolete
                            case ("tachoresultp5"):
                                 // FISPoliceContext p5result = (FISPoliceContext)ev.Parametr;
                                // your logic

                                break;*/
                            case ("tachoresult"):
                                P8Serializer p8Serializer = new P8Serializer();
                                var res = p8Serializer.DeserializeFromBase64<TachoResult>(ev.Parametr);
                                (this.BindingContext as TachoControllerViewModel).CurrentContent = new TachoResultView() { BindingContext = res };
                                (this.BindingContext as TachoControllerViewModel).PrbuttonText = "Ok";
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        preditor.Text += "Exception :" + ex.Message;
                    }
                };
            };
        }

        private async Task ProgressInfo()
        {
            await prBar.ProgressTo(1, (uint)TimeSpan.FromMinutes(4).TotalMilliseconds, Easing.Linear);
        }
    }
}
