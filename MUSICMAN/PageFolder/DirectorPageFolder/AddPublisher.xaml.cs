using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MUSICMAN.PageFolder.DirectorPageFolder
{
    /// <summary>
    /// Логика взаимодействия для AddPublisher.xaml
    /// </summary>
    public partial class AddPublisher : Window
    {
        Notifier notifier;
        public AddPublisher()
        {
            InitializeComponent();
            notifier = App.GetWindowNotifer(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PublisherAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Publisher = new Publisher()
                {
                    Rating = RatingTb.Text,
                    PublisherName = NameTb.Text,
                    PublisherContractTil = DateContractTb.SelectedDate.Value,

                };
                DBEntities.GetContext().Publisher.Add(Publisher);
                DBEntities.GetContext().SaveChanges();


            }
        }
        private void AddPublisherBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    PublisherAdd();
                    notifier.ShowSuccess("Поставщик добавлен");
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

        private void RatingTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, является ли вводимый символ цифрой
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                // Если символ не является цифрой, отменяем его ввод
                e.Handled = true;
            }

            // Проверяем, чтобы вводимое число находилось в диапазоне от 1 до 100
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text + e.Text;
            int number;
            if (int.TryParse(newText, out number))
            {
                if (number < 1 || number > 100)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
