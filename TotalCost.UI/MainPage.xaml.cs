﻿using TotalCost.UI.Entity;
using TotalCost.UI.Logic;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TotalCost.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            using (Repository repo = new Repository())
            {
                //Bill b = new Bill
                //{
                //    Name = "Сбер",
                //    Type = BillType.Card,
                //    Sum = 3378.05
                //};
                //repo.AddBill(b);
            }
        }
    }
}
