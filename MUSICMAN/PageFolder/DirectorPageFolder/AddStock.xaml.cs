using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddStock.xaml
    /// </summary>
    public partial class AddStock : Window
    {
        private Notifier notifier;

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
            Task.Delay(500).ContinueWith(_ => // Задержка в 1 секунду
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Close(); // Закрытие окна
                });
            });
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void AdressTb_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}