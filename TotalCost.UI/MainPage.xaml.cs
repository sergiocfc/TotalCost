using System;
using System.Collections.Generic;
using System.Linq;
using TotalCost.UI.Entity;
using TotalCost.UI.Logic;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TotalCost.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public List<Bill> Bills { get; set; }
        public List<Group> Groups { get; set; }
        public List<Payment> Payments { get; set; }
        private IDataRepository repo;

        public MainPage()
        {
            this.InitializeComponent();

            using (var c = new Context())
            {
                Bills = c.Bills.ToList();
                Groups = c.Groups.ToList();
                Payments = c.Payments.ToList();
            }

            repo = RepositoryFactory.Default.GetRepository();
        }

        private void buttonOk_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            double sum = 0;
            if (!double.TryParse(textBoxSum.Text, out sum))
            {
                var dialog = new MessageDialog("Неправильный формат суммы").ShowAsync();
                return;
            }

            var p = new Payment
            {
                Date = DateTime.Now,
                Bill = comboBoxBill.SelectedItem as Bill,
                Group = comboBoxGroup.SelectedItem as Group,
                Sum = double.Parse(textBoxSum.Text),
                Note = textBoxNote.Text
            };
            try
            {
                repo.AddPayment(p);
                listBoxPayments.Items.Add(p);
            }
            catch (Exception ex)
            {
                var dialog = new MessageDialog(ex.Message).ShowAsync();
            }

            CleanFields();
        }

        private void CleanFields()
        {
            textBoxSum.Text = "";
        }
    }
}
