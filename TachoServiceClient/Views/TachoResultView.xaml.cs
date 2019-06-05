using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TachoData;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TachoServiceClient.Views
{
    public class TachoViolationCell : TextCell
    {
        public TachoViolation Data { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TachoResultView : ContentView
    {
		public TachoResultView ()
		{
                this.BindingContextChanged += (s, e) =>
              {
                  try
				  {
					TachoResult result = (TachoResult)this.BindingContext;

                      StackLayout stack = new StackLayout();
                      TableView table = new TableView
                      {
                          Intent = TableIntent.Form
                      };
                      TableRoot tableroot = new TableRoot();

                      TableSection section = new TableSection();

                      section.Title = "Карта водителя";
                      TachoResult tacho = new TachoResult();
                      
                      var cell = new TextCell();
                      cell.Text = "Владелeц";
                      cell.Detail = $"{result.HolderSurname} {result.HolderFirstNames}";
                      section.Add(cell);

                      cell = new TextCell();
                      cell.Text = "Дата рождения";
                      cell.Detail = $"{result.CardHolderBirthDate.ToString("dd.MM.yyyy")}";
                      section.Add(cell);

                      cell = new TextCell();
                      cell.Text = "Номер карты";
                      cell.Detail = $"{result.CardNumber}";
                      section.Add(cell);

                      cell = new TextCell();
                      cell.Text = "Действительна до";
                      cell.Detail = $"{result.CardExpiryDate.ToString("dd.MM.yyyy")}";
                      section.Add(cell);

                      cell = new TextCell();
                      cell.Text = "Дата предыдущего использования";
                      cell.Detail = $"{result.VehicleLastUsed.ToString("dd.MM.yyyy HH:mm")}";
                      section.Add(cell);
                      tableroot.Add(section);

                      TableSection ViolationSection = new TableSection();
                      tableroot.Add(ViolationSection);

                      table.Root = tableroot;
                      stack.Children.Add(table);
                      this.Content = stack;

                      var tx = new TextCell();

                      if (result.Violations == null) return;
                      foreach (TachoViolation tv in result.Violations)
                      {
                          var tc = new TachoViolationCell()
                          {
                              Text = tv.Description,
                              TextColor = Color.Black,
                              Detail = String.Format("c {0} по {1} : {2}", tv.BeginTime.ToString("dd.MM.yyyy HH:mm:ss"), tv.EndTime.ToString("dd.MM.yyyy HH:mm:ss"), tv.ActualValue),
                              Data = tv
                          };
                          tc.Tapped += Tx_Tapped;
                          ViolationSection.Add(tc);
                      }
                   
                      /* result.Violations?.ForEach(c =>
					   ViolationSection.Add(new TextCell()
					   {
						   Text = c.Description,
						   TextColor = Color.Black,
                          
						  Detail = String.Format("c {0} по {1} : {2}", c.BeginTime.ToString("dd.MM.yyyy HH:mm:ss") , c.EndTime.ToString("dd.MM.yyyy HH:mm:ss"), c.ActualValue)
                       }));*/
				  }
				catch(Exception ex)
				{
					//todo log
				}
              };
		}

        private async void Tx_Tapped(object sender, EventArgs e)
        {
           /* var tc = (sender as TachoViolationCell);
            await Navigation.PushAsync(new DetailViolationView((tc.Data)));*/
        }
    }
}