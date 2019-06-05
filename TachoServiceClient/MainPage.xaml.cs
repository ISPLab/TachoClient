using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachoServiceClient.ViewModels;
//using TachoServiceClient.ViewModels;
using Xamarin.Forms;

namespace TachoServiceClient
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        TachoControllerViewModel TCViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
            TCViewModel = new TachoControllerViewModel();
            this.BindingContext = TCViewModel;
        }
    }
}
