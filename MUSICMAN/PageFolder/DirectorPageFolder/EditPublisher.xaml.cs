using MUSICMAN.ClassFolder;
using MUSICMAN.DataFolder;
using MUSICMAN.WindowFolder.DirectorFolder;
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
    /// Логика взаимодействия для EditPublisher.xaml
    /// </summary>
    public partial class EditPublisher : Window
    {
        private Notifier notifier;

        public EditPublisher(Publisher publisher)
        {
            notifier = App.GetWindowNotifer(this);
            Publisher = publisher;
            InitializeComponent();
        }
        public Publisher Publisher { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Закрытие окна
        }

        private void ProviderEdit()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                Publisher.PublisherName = NameTb.Text;
                Publisher.PublisherContractTil = DateContractTb.SelectedDate.Value;
                Publisher.Rating = RatingTb.Text;
                DBEntities.GetContext().SaveChanges();
                DirectorMainWindow.notifier.ShowSuccess("Поставщик изменен!");
            }
            else
            {
                DirectorMainWindow.notifier.ShowError("Вы не заполнили все поля!");
            }
        }


        private void EditPublisherBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    ProviderEdit();
                    notifier.ShowSuccess("Издатель изменен");
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }
    }
}
