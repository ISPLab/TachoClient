using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using TachoData;
using TachoHelper.Services;
using TachoServiceClient.Views;
using Xamarin.Forms;

namespace TachoServiceClient.ViewModels
{
    public class TachoControllerViewModel : BindableObject, INotifyPropertyChanged 
    {
        TachoParms tparms;
        public TachoService tachocardservice = TachoService.Instance;
        public ICommand ProcessCommand { private set; get; }
        public static readonly BindableProperty CurrentContentProperty = BindableProperty.Create("CurrentContent", typeof(object), typeof(MainPage), null, propertyChanged: OnEventNameChanged);
        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Property changed implementation goes here
        }
        public object CurrentContent
        {
            get { return GetValue(CurrentContentProperty); }
            set { SetValue(CurrentContentProperty, value); }
        }

        public string MainHeader
        {
            get { return "Режим труда и отдыха водителя"; }
        }
        public static readonly BindableProperty PinProperty =

        BindableProperty.Create("PrbuttonText", typeof(string), typeof(TachoControllerViewModel), "");
        public string Pin
        {
            get { return (string)GetValue(PinProperty); }
            set { SetValue(PinProperty, value); }
        }

        public static readonly BindableProperty PrButtonTextProperty =
            BindableProperty.Create("PrbuttonText", typeof(string), typeof(TachoControllerViewModel), "Проверить карту");

        public string PrbuttonText
        {
            get { return (string)GetValue(PrButtonTextProperty); }
            set { SetValue(PrButtonTextProperty, value); }
        }

        public static readonly BindableProperty SmartButtonVisibleProperty =
            BindableProperty.Create("SmartButtonVisible", typeof(bool), typeof(TachoControllerViewModel), false);
        public bool SmartButtonVisible
        {
            get { return (bool)GetValue(SmartButtonVisibleProperty); }
            set { SetValue(SmartButtonVisibleProperty, value); }
        }

        public List<TachoLegislation> Legislations { get; set; }
        public TachoLegislation CurrentLegislation { get; internal set; }

        public TachoControllerViewModel()
        {
            SetWaitView();
            tachocardservice.P8Event += (s, e) =>
            {
                if (e.EventName == "tachoinfo" && (string)e.Parametr == "connected")
                {
                    Device.BeginInvokeOnMainThread(() => { 
                    
                    SmartButtonVisible = true;
                    PrbuttonText = "далее";
                    CurrentContent = new CardPinView { BindingContext = this};
                    });
                }

                if (e.EventName == "tachoerror" && (string)e.Parametr == "connected false")
                {
                    Label prlabel = new Label() { Style = (Style)Application.Current.Resources["H1Header"], TextColor = Color.Red };
                    prlabel.Text = "Ошибка подключения к тахографическому сервису. Обратитесь к администратору.";
                    SmartButtonVisible = false;
                    CurrentContent = new ContentView()
                    {
                        BindingContext = this,
                        Content = new StackLayout()
                        {
                            Children = {
                           prlabel
                        }
                        }
                    };
                }

                if (e.EventName == "tachoerror" && (((string)e.Parametr == "Licence_Premission_error") || (string)e.Parametr == "Check_license_error"))
                {
                    SetWaitView();
                    Label prlabel = new Label() { Style = (Style)Application.Current.Resources["H1Header"], TextColor = Color.Red };
                    prlabel.Text = e.Description;
                    SmartButtonVisible = false;
                    CurrentContent = new ContentView()
                    {
                        BindingContext = this,
                        Content = new StackLayout()
                        {
                            Children = {
                           prlabel
                        }
                        }
                    };
                }
            };

            ProcessCommand = new Command(
                execute: () =>
                {
                    var c = Legislations;
                    if (CurrentContent is Views.CardPinView)
                    {
                        Legislations = tachocardservice.GetLegislations();
                        CurrentContent = new TachoLegislationParentView() { BindingContext = this }; ;
                        PrbuttonText = "проверить карту";
                    }
                    else
                    if (CurrentContent is TachoLegislationParentView)
                    {
                        CurrentContent = new Views.TachoProcessView() { BindingContext = this };
                        PrbuttonText = "отменить";
                        tparms = GetTachoParms();
                        tachocardservice.GetTachodata(tparms);
                    }
                    else
                    if (CurrentContent is Views.TachoProcessView || CurrentContent is TachoResultView)
                    {
                        tachocardservice.StopTachoProcess();
                        Pin = "";
                        CurrentContent = new Views.CardPinView() { BindingContext = this }; ;
                        PrbuttonText = "далее";
                    }
                    ProcessCommand.CanExecute(false);
                },
                canExecute: () =>
                {
                    return true;
                });
            tachocardservice.StartService();
        }

        private void SetWaitView()
        {
            var indicator = new ActivityIndicator() { WidthRequest = 200, HeightRequest = 200 };//{ HorizontalOptions = LayoutOptions.FillAndExpand,  VerticalOptions = LayoutOptions.FillAndExpand };
            Label prlabel = new Label() { Text = "Ожидание подключения к тахографическому сервису", Style = (Style)Application.Current.Resources["H2Header"] };
            prlabel.HorizontalTextAlignment = TextAlignment.Center;
            indicator.IsRunning = true;
            SmartButtonVisible = false;
            CurrentContent = new ContentView()
            {
                BindingContext = this,
                Content = new StackLayout()
                {
                    Children = {
                        indicator,
                        prlabel
                    }
                }
            };
        }

        public TachoParms GetTachoParms()
        {
            TachoParms param = new TachoParms();
            param.Beginreportdatetime = new DateTime(2014, 01, 01).ToUniversalTime();
            param.Endreportdatetime = new DateTime(2018, 01, 01).ToUniversalTime();
            param.Legislation = CurrentLegislation;
           
             //not implement yet for tachoclient
            /*param.MaxCarAnalisis = 0;
            //param.MaxShiftNumber = 0;
            //param.MaxVioaltionNumber = 0;*/
            param.Pin = Pin;
            return param;
        }
    }
}
