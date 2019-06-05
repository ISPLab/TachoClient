using System;
using Xamarin.Forms;

namespace TachoServiceClient.Views
{
    public class VioalationView :ContentView
    {
        public VioalationView()
        {
            TableView table = new TableView
            {
                Intent = TableIntent.Form
            };
            TableRoot tableroot = new TableRoot();
          
            TableSection section = new TableSection();
            
            section.Title = "Нарушение";

            var cell = new TextCell();
            cell.Text = "Регламентирующий докумен";
            cell.SetBinding(TextCell.DetailProperty, "ArticleReference");
            section.Add(cell);

            cell = new TextCell();
            cell.Text = "Регламентирующий докумен";
            cell.SetBinding(TextCell.DetailProperty, "ArticleReference");
            section.Add(cell);

            cell = new TextCell();
            cell.Text = "Дата и время начала нарушения";
            cell.SetBinding(TextCell.DetailProperty, "BeginTime");
            section.Add(cell);

            cell = new TextCell();
            cell.Text = "Дата и время окончания нарушения";
            cell.SetBinding(TextCell.DetailProperty, "EndTime");
            section.Add(cell);

            cell = new TextCell();
            cell.Text = "Описание нарушения";
            cell.SetBinding(TextCell.DetailProperty, "Description");
            section.Add(cell);

            cell = new TextCell();
            cell.Text = "Регламентированное значение";
            cell.SetBinding(TextCell.DetailProperty, "RegimentedValue");
            section.Add(cell);

            cell = new TextCell();
            cell.Text = "Фактическое значение";
            cell.SetBinding(TextCell.DetailProperty, "ActualValue");
            section.Add(cell);
            tableroot.Add(section);
            table.Root = tableroot;
            this.Content = table;
        }
}
}
