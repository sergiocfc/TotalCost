using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TotalCost.UI.Lib;

namespace TotalCost.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Payment> Payments { get; set; }
        public List<Group> Groups { get; set; }
        public List<Bill> Bills { get; set; }

        private IDataRepository repo;

        public MainWindow()
        {
            InitializeComponent();
            using (var c = new Context())
            {
                Payments = c.Payments.ToList();
                Groups = c.Groups.ToList();
                Bills = c.Bills.ToList();
            }
            comboBoxBill.ItemsSource = Bills;
            comboBoxGroup.ItemsSource = Groups;
            foreach (var item in Payments)
                listBoxPayments.Items.Add(item);

            repo = RepositoryFactory.Default.GetRepository();

            repo.OnPaymentAdd += (p) => listBoxPayments.Items.Add(p);
            repo.OnPaymentRemove += (p) => listBoxPayments.Items.Remove(p);
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                repo.AddPayment(new Payment()
                {
                    Note = textBoxNote.Text,
                    Date = DateTime.Now,
                    Group = comboBoxGroup.SelectedItem as Group,
                    Bill = comboBoxBill.SelectedItem as Bill,
                    Sum = int.Parse(textBoxSum.Text)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                repo.RemovePayment(listBoxPayments.SelectedItem as Payment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
