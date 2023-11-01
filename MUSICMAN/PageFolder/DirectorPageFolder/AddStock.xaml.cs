using MUSICMAN.ClassFolder;
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
using MUSICMAN.DataFolder;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity.Validation;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStock.xaml
    /// </summary>
    public partial class AddStock : Window
    {
        Notifier notifier;
        public AddStock()
        {
            InitializeComponent();
            notifier = App.GetWindowNotifer(this);
        }
        private void StockAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Stocks = new Stocks()
                {
                    StockNumber = GenerateStockNumber(),
                    IndividualAdress = AdressTb.Text,

                };
                DBEntities.GetContext().Stocks.Add(Stocks);
                DBEntities.GetContext().SaveChanges();


            }
        }

        private string GenerateStockNumber()
        {
            int maxStockNumber = 9999; // Максимальное значение номера склада (4 символа)
            int minStockNumber = 0; // Минимальное значение номера склада (4 символа)
            Random random = new Random();
            int stockNumber = random.Next(minStockNumber, maxStockNumber + 1);
            string formattedStockNumber = string.Format("475{0:D4}", stockNumber); // Добавляем "475" в начало номера склада
            if (DBEntities.GetContext()
                        .Stocks
                        .FirstOrDefault(
                u => u.StockNumber == formattedStockNumber) != null)
            {
                return formattedStockNumber;
            }
            return formattedStockNumber;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddStockBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    StockAdd();
                    notifier.ShowSuccess("Склад добавлен");
                    ElementsToolsClass.ClearAllControls(this);
                }
                catch (DbEntityValidationException ex)
                {
                    MBClass.ErrorMB(ex);
                }
            }
            else
            {
                notifier.ShowError("Вы не ввели все нужные данные!");
            }
        }
    }
}
