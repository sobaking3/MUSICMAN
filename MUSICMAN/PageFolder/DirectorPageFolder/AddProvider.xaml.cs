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
    /// Логика взаимодействия для AddProvider.xaml
    /// </summary>
    public partial class AddProvider : Window
    {
        Notifier notifier;
        public AddProvider()
        {
            notifier = App.GetWindowNotifer(this);
            InitializeComponent();
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
        private void ProviderAdd()
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                var Provider = new Provider()
                {
                    ProviderUrAdress = AdressTb.Text,
                    ProviderName = NameTb.Text,
                    ContractTil = DateContractTb.SelectedDate.Value,

                };
                DBEntities.GetContext().Provider.Add(Provider);
                DBEntities.GetContext().SaveChanges();


            }
        }
        private void AddProviderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ElementsToolsClass.AllFieldsFilled(this))
            {
                try
                {
                    ProviderAdd();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowTransitionHelper.OpenWindow(this, this);
        }

        private void AdressTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
